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
          .ForMember(dest => dest.AdminUserId, opt => opt.MapFrom(src => src.AdminUserId))
          .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
          .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
          .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
          .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.ManagerName))
          .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
          .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.Sector))
          .ForMember(dest => dest.SocialMediaLinks, opt => opt.MapFrom(src => src.SocialMediaLinks.Select(link => new OrganizationSocialMediaLinkDto
          {
              Id = link.Id,
              Platform = link.Platform,
              Url = link.Url
          }).ToList()));

            // If needed, map from OrganizationDto back to Organization
            CreateMap<OrganizationDto, Organization>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrganizationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrganizationName))
                .ForMember(dest => dest.AdminUserId, opt => opt.MapFrom(src => src.AdminUserId));
        }
    }
}
