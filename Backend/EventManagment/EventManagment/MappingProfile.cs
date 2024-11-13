using AutoMapper;
using EventManagment.Dtos;
using EventManagment.Models;
using AutoMapper;
using EventManagment.Models;
namespace EventManagment
{


public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget));

            CreateMap<EventDto, Event>()
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget));

            CreateMap<Budget, BudgetDto>()
                .ReverseMap(); 
        }
    }

}
