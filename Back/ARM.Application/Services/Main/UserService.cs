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
    IPermissionService permissionService,
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

    private HttpContext httpContext => httpContextAccessor.HttpContext;


    public async Task<UserDto> GetCurrentUserAsync()
    {
        var userId = httpContext.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateProfileAsync(UpdateUserDto updateDto)
    {
        var userId = httpContext.GetUserId();
        var user = await userRepository.GetByIdAsync(userId);

        _mapper.Map(updateDto, user);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await userRepository.UpdateAsync([user]);

            return _mapper.Map<UserDto>(user);
        });
    }

    public async Task<string> GetUserPasswordHashAsync(string userId)
    {
        //await permissionService.EnsurePermissionAsync("user_password_view");
        var user = await userRepository.GetByIdAsync(userId);
        return user.Password;
    }

    public async Task UpdateUserPasswordAsync(string userId, string newPassword)
    {
        await permissionService.EnsurePermissionAsync("user_password_update");
        var user = await userRepository.GetByIdAsync(userId);
        user.Password = newPassword;
        await userRepository.UpdateAsync([user]);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        //await permissionService.EnsurePermissionAsync("user_view");
        var user = (await userRepository.FindAsync(u => u.Email == email))
            .FirstOrDefault();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserCredentialsDto?> GetUserCredentialsByIdAsync(string id)
    {
        //await permissionService.EnsurePermissionAsync("user_credentials_view");
        var user = await userRepository.GetByIdAsync(id);

        return new UserCredentialsDto
        {
            Id = user.Id,
            Password = user.Password,
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        await permissionService.EnsurePermissionAsync("user_create");
        var role = await _roleService.GetRoleByNameAsync("Admin");
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
        await permissionService.EnsurePermissionAsync("user_update");
        var existingUser = await userRepository.GetByIdAsync(id);

        await _updateUserValidator.ValidateAndThrowAsync(updateUserDto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(updateUserDto, existingUser);
            await userRepository.UpdateAsync([existingUser]);

            return _mapper.Map<UserDto>(existingUser);
        });
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        await permissionService.EnsurePermissionAsync("user_delete");
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await userRepository.DeleteAsync(userId);
            return true;
        });
    }

    public async Task<PaginatedResponse<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize)
    {
        await permissionService.EnsurePermissionAsync("user_view");
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
        await permissionService.EnsurePermissionAsync("user_view");
        var user = await userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }
}