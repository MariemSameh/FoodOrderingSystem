using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrderingSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Customer")]
        [Required(ErrorMessage = "You have to provide a valid name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 charecters.")]
        [MaxLength(40, ErrorMessage = "Name mustn't exeed 40 charecters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to provide a valid Address.")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "You have to provide a valid Email.")]
        [MinLength(5, ErrorMessage = "You have to provide a valid Email.")]
        [MaxLength(30, ErrorMessage = "You have to provide a valid Email.")]
        public string Email { get; set; }
    }
}
