using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class AutoServiceProfile : Profile
{
    public AutoServiceProfile()
    {
        CreateMap<BrandEntity, BrandDto>();
        CreateMap<CreateBrandDto, BrandEntity>();
        CreateMap<UpdateBrandDto, BrandEntity>();
    }
} 