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
        CreateMap<PermissionEntity, PermissionDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));
        
        CreateMap<CreatePermissionDto, PermissionEntity>();
        
        CreateMap<UpdatePermissionDto, PermissionEntity>();
    }
}