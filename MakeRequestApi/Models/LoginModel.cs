using System.ComponentModel.DataAnnotations;

namespace MakeRequestApi.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
