using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        CreateMap<UserEntity, PublicCustomerDto>(); 
        CreateMap<CreateUserDto, UserEntity>();
        CreateMap<UpdateUserDto, UserEntity>();
    }
}