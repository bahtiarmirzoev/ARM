using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<PermissionEntity, PermissionDto>();
        
        CreateMap<CreatePermissionDto, PermissionEntity>();
        
        CreateMap<UpdatePermissionDto, PermissionEntity>();
    }
}