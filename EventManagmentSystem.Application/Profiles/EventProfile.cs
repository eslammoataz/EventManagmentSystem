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
            .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.OrganizerId))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl))
            .ForMember(dest => dest.MeetingUrl, opt => opt.MapFrom(src => src.MeetingUrl))
            .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organizer.Name))
            .ForMember(dest => dest.TicketsNumber, opt => opt.MapFrom(src => src.Tickets.Count()))
            .ForMember(dest => dest.BookedTicketsNumber, opt => opt.MapFrom(src => src.Tickets.Count(t => t.ApplicationUserId != null)));

            // Reverse mapping: Mapping from EventDto to Event entity
            CreateMap<EventDto, Event>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<EventCategory>(src.Category)))
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => Enum.Parse<EventType>(src.EventType)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<EventStatus>(src.Status)))
                .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.OrganizationId));

        }
    }
}
