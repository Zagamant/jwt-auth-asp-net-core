using System.ComponentModel.DataAnnotations;

namespace RNDR.Services.Models.ModelManagement
{
    public class UserAuthenticate
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
