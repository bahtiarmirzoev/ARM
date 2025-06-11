using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceDto, ServiceEntity>();
        CreateMap<UpdateServiceDto, ServiceEntity>();
        CreateMap<ServiceEntity, ServiceDto>();
    }
}