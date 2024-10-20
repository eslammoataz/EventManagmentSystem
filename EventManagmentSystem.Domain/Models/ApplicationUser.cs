﻿using Microsoft.AspNetCore.Identity;

namespace EventManagmentSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        // Navigation property to keep track of organizations this user belongs to
        public ICollection<Organization> Organizations { get; set; } = new List<Organization>();

        // Other properties related to the user
        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<UserSocialMediaLink> SocialMediaLinks { get; set; } = new List<UserSocialMediaLink>();

    }

}
