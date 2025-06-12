using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class VenueProfile : Profile
{
    public VenueProfile()
    {
        CreateMap<CreateVenueDto, VenueEntity>();
        CreateMap<UpdateVenueDto, VenueEntity>();
        CreateMap<VenueEntity, VenueDto>();
    }
}