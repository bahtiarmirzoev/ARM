using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using AutoMapper;
using FluentValidation;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Common;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Application.Services.Main;

public class PermissionService : IPermissionService
{
    private readonly IMapper _mapper;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IValidator<CreatePermissionDto> _createPermissionValidator;
    private readonly IValidator<UpdatePermissionDto> _updatePermissionValidator;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionService(
        IMapper mapper,
        IPermissionRepository permissionRepository,
        IValidator<CreatePermissionDto> createPermissionValidator,
        IValidator<UpdatePermissionDto> updatePermissionValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _permissionRepository = permissionRepository;
        _createPermissionValidator = createPermissionValidator;
        _updatePermissionValidator = updatePermissionValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<PermissionDto> CreateAsync(CreatePermissionDto dto)
    {
        await _createPermissionValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var permissionEntity = _mapper.Map<PermissionEntity>(dto);
            await _permissionRepository.AddAsync(permissionEntity);
            
            return _mapper.Map<PermissionDto>(permissionEntity);
        });
    }

    public async Task<PermissionDto> UpdateAsync(string id, UpdatePermissionDto dto)
    {
        var existingPermission = await _permissionRepository.GetByIdAsync(id);
        await _updatePermissionValidator.ValidateAndThrowAsync(dto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingPermission);
            await _permissionRepository.UpdateAsync(new[] { existingPermission });
            
            return _mapper.Map<PermissionDto>(existingPermission);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _permissionRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<PermissionDto> GetByIdAsync(string id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task<IEnumerable<PermissionDto>> GetAllAsync()
    {
        var permissions = await _permissionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto> GetByNameAsync(string name)
    {
        var permission = (await _permissionRepository.FindAsync(p => p.Name == name))
            .FirstOrDefault()
            .EnsureFound("PermissionNotFound");
            
        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task<IEnumerable<PermissionDto>> GetByRoleIdAsync(string roleId)
    {
        var permissions = await _permissionRepository.FindAsync(p => p.Roles.Any(r => r.Id == roleId));
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }
} 