using System.ComponentModel.DataAnnotations;

namespace RNDR.Services.Models.ModelManagement
{
    public class UserRegister
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
