using System.Security.Claims;
using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Common;
using ARM.Core.Dtos.Auth;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using static BCrypt.Net.BCrypt;

namespace ARM.Application.Services.Main;

public class UserService(
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository,
    IRoleService roleService,
    IValidator<CreateUserDto> createUserValidator,
    IValidator<UpdateUserDto> updateUserValidator,
    IUnitOfWork unitOfWork)
    : IUserService
{
    private readonly IMapper _mapper = mapper;
    private readonly IRoleService _roleService = roleService;
    private readonly IValidator<CreateUserDto> _createUserValidator = createUserValidator;
    private readonly IValidator<UpdateUserDto> _updateUserValidator = updateUserValidator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private string GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    
    public async Task<PublicUserDto> GetCurrentUserAsync()
    {
        var userId = GetUserId();
        var user = await userRepository.GetByIdAsync(userId);
        return _mapper.Map<PublicUserDto>(user);
    }

    public async Task<UserDto> UpdateProfileAsync(UpdateUserDto updateDto)
    {
        var userId = GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        _mapper.Map(updateDto, user);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await userRepository.UpdateAsync(new[] { user });
            
            return _mapper.Map<UserDto>(user);
        });
    }
    
    public async Task<string> GetUserPasswordHashAsync(string userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        return user.Password;
    }

    public async Task UpdateUserPasswordAsync(string userId, string newPassword)
    {
        var user = await userRepository.GetByIdAsync(userId);
        user.Password = newPassword;
        await userRepository.UpdateAsync(new[]{user});
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = (await userRepository.FindAsync(u => u.Email == email))
            .FirstOrDefault();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserCredentialsDto?> GetUserCredentialsByIdAsync(string id)
    {
        var user = await userRepository.GetByIdAsync(id);

        return new UserCredentialsDto
        {
            Id = user.Id,
            Password = user.Password,
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var role = await _roleService.GetRoleByNameAsync("Client");
        await _createUserValidator.ValidateAndThrowAsync(createUserDto);
        
        var existingUser = (await userRepository.FindAsync(
                u => u.Email == createUserDto.Email))
            .FirstOrDefault();
        
        if (existingUser is not null)
            throw new AppException(ExceptionType.Conflict, "UserAlreadyExists");
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var user = _mapper.Map<UserEntity>(createUserDto);
            user.Email = createUserDto.Email.ToLowerInvariant();
            user.RoleId = role.Id;
            user.Password = HashPassword(createUserDto.Password);
            
            await userRepository.AddAsync(user);
            
            return _mapper.Map<UserDto>(user);
        });
    }

    public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
    {
        var existingUser = await userRepository.GetByIdAsync(id);
        
        await _updateUserValidator.ValidateAndThrowAsync(updateUserDto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(updateUserDto, existingUser);
            await userRepository.UpdateAsync(new[] { existingUser });
            
            return _mapper.Map<UserDto>(existingUser);
        });
    }

    public async Task<UserDto> ConfirmEmailAsync()
    {
        var userId = GetUserId();
        var user = await userRepository.GetByIdAsync(userId);
        user.EmailVerified = true;
        await userRepository.UpdateAsync([user]);
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await userRepository.DeleteAsync(userId);

            return true;
        });
    }

    public async Task<PaginatedResponse<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize)
    {
        var totalUsers = await userRepository.GetAllAsync();

        if (pageNumber <= 0 || pageSize <= 0)
            throw new AppException(ExceptionType.BadRequest, "PaginationError");

        int totalItems = totalUsers.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var pagedUsers = totalUsers
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var userDtos = _mapper.Map<IEnumerable<UserDto>>(pagedUsers);
        
        return new PaginatedResponse<UserDto>
        {
            Data = userDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }
}