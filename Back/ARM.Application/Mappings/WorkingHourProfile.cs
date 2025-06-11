using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;
using DeviceDetectorNET;

namespace ARM.Application.Mappings;

public class WorkingHourProfile : Profile
{
    public WorkingHourProfile()
    {
        CreateMap<CreateWorkingHourDto, WorkingHourEntity>()
            .ForMember(dest => dest.Day, opt =>
                opt.MapFrom(src => src.DayOfWeek.ToString()));
        CreateMap<UpdateWorkingHourDto, WorkingHourEntity>();
        CreateMap<WorkingHourEntity, WorkingHourDto>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToString()));
    }
}