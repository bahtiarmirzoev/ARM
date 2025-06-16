using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;

namespace ARM.Application.Mappings;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<ReviewEntity, ReviewDto>()
            .ForMember(dest => dest.Customer,opt  => opt.MapFrom(src => src.Customer));
        CreateMap<CreateReviewDto, ReviewEntity>();
        CreateMap<UpdateReviewDto, ReviewEntity>();
        
        CreateMap<CustomerEntity, PublicCustomerDto>();
    }
}