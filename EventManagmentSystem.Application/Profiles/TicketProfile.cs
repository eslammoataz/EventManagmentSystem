using AutoMapper;
using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            // Define mapping from Ticket entity to TicketDto
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.IsCheckedIn, opt => opt.MapFrom(src => src.IsCheckedIn))
                .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.ApplicationUserId))
                .ForMember(dest => dest.TicketSenderUserId, opt => opt.MapFrom(src => src.ticketSender))
                .ForMember(dest => dest.isGift, opt => opt.MapFrom(src => src.isGift));
        }
    }
}
