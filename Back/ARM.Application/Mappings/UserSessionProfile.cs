using ARM.Core.Dtos.Create;
using ARM.Core.Entities.Auth;
using AutoMapper;

namespace ARM.Application.Mappings;

public class UserSessionProfile : Profile
{
    public UserSessionProfile()
    {
        CreateMap<CreateUserActiveSessionDto, UserActiveSessionsEntity>();
    }
}