using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
    public class User
    {
        [Key] public int Id { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MaxLength(20, ErrorMessage = "This field must have between 3 at 60 characters")]
        [MinLength(3, ErrorMessage = "This field must have between 3 at 60 characters")]
        public string Username { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MaxLength(20, ErrorMessage = "This field must have between 3 at 60 characters")]
        [MinLength(3, ErrorMessage = "This field must have between 3 at 60 characters")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
