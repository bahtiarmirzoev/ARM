using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerEntity, CustomerDto>();
        CreateMap<CreateCustomerDto, CustomerEntity>();
        CreateMap<UpdateCustomerDto, CustomerDto>();
    }
}