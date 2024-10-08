using AutoMapper;
using EventManagmentSystem.Application.Commands.EventCommands.EditEvent;
using EventManagmentSystem.Application.Dto.Events;
using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EditEventCommand, Event>()
              .ForMember(dest => dest.StartDate, opt => opt.Ignore())
              .ForMember(dest => dest.EndDate, opt => opt.Ignore())

              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Event, EventDto>()
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
               .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organizer.Name))
               .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.OrganizerId));
        }
    }
}
