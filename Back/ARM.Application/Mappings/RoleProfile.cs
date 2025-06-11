using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEntity, RoleDto>();
        CreateMap<CreateRoleDto, RoleEntity>();
        CreateMap<UpdateReviewDto, RoleEntity>();
    }
}