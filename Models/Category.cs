namespace ShopAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key] public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(60, ErrorMessage = "This field must have between 3 at 60 characters")]
        [MinLength(3, ErrorMessage = "This field must have between 3 at 60 characters")]
        public string Title { get; set; }
    }
}