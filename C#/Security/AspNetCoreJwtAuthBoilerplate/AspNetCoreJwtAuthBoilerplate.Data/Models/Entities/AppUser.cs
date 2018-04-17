using Microsoft.AspNetCore.Identity;

namespace AspNetCoreJwtAuthBoilerplate.Data.Models.Entities
{
    public class AppUser : IdentityUser
    {
        // extended properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
    }
}