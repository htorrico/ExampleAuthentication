using System.ComponentModel.DataAnnotations;

namespace ExampleLogin.Models
{
    public class AuthenticateViewModel
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

    }
}