using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrderingSystem.Models
{
    public class DeliveryMan
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Delivery Man")]
        [Required(ErrorMessage = "You have to provide a valid name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 charecters.")]
        [MaxLength(50, ErrorMessage = "Name mustn't exeed 50 charecters.")]
        public string Name { get; set; }

        [DisplayName("Years of Experiencr")]
        [Required(ErrorMessage = "You have to provide a valid Years.")]
        [Range(2, 7, ErrorMessage = "Years of Experience must be between 2 & 7 Years")]
        public int YearsOfExperience { get; set; }

        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }

        [Range(0, 25000, ErrorMessage = "Salary must be between 0 & 25000 EGP")]
        public decimal Salary { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Select a valid restaurant")]
        [DisplayName("Restaurant")]
        public int RestaurantId { get; set; }

        [ValidateNever]
        public Restaurant Restaurant { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }
    }
}
