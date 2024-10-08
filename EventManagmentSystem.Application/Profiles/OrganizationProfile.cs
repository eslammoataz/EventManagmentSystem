using AutoMapper;
using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            // Mapping from Organization (domain model) to OrganizationDto (DTO)
            CreateMap<Organization, OrganizationDto>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AdminUserId, opt => opt.MapFrom(src => src.AdminUserId));

            // If needed, map from OrganizationDto back to Organization
            CreateMap<OrganizationDto, Organization>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrganizationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrganizationName))
                .ForMember(dest => dest.AdminUserId, opt => opt.MapFrom(src => src.AdminUserId));
        }
    }
}
