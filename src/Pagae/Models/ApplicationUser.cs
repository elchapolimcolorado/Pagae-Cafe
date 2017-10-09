using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Pagae.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int Credits { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
