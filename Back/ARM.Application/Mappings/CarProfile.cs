using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarEntity, CarDto>();
        CreateMap<CreateCarDto, CarEntity>();
        CreateMap<UpdateCarDto, CarDto>();
    }
}