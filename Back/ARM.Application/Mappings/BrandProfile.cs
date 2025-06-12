using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandEntity, BrandDto>();
        CreateMap<CreateBrandDto, BrandEntity>()
            .ForMember(dest => dest.Venues, opt => opt.Ignore());;
        CreateMap<UpdateBrandDto, BrandEntity>();
        CreateMap<BrandEntity, VenueEntity>();
    }
}