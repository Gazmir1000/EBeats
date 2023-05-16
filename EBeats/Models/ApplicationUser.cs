using Microsoft.AspNetCore.Identity;

namespace EBeats.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Firstname { get; set; }   
        public string? Lastname { get; set; }
        public DateTime Birthday { get; set; }
    }
}
