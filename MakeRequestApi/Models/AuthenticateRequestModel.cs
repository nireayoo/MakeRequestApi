using System.ComponentModel.DataAnnotations;

namespace MakeRequestApi.Models
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
