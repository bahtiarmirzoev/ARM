using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Auth;
using AutoMapper;

namespace ARM.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<BlackListedEntity, BlackListedDto>();
        CreateMap<CreateBlackListedDto, BlackListedEntity>();
        CreateMap<UpdateBlackListedDto, BlackListedEntity>();
    }
}