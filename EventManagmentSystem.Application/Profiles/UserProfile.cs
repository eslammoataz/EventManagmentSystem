using AutoMapper;
using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Mapping from ApplicationUser (domain model) to UserDto (DTO)
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            // Mapping from UserDto to ApplicationUser if you want two-way mapping
            CreateMap<UserDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
