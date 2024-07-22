using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrderingSystem.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Restaurant")]
        [Required(ErrorMessage = "You have to provide a valid name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 charecters.")]
        [MaxLength(50, ErrorMessage = "Name mustn't exeed 50 charecters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to provide a valid Category.")]
        [MinLength(2, ErrorMessage = "Category mustn't be less than 2 charecters.")]
        [MaxLength(50, ErrorMessage = "Category mustn't exeed 50 charecters.")]
        public string Category { get; set; }

        [DisplayName("Annual Budget")]
        [Required(ErrorMessage = "You have to provide a valid annual budget.")]
        [Range(0, 500000, ErrorMessage = "Annual Budget must be between 0 & 500000 EGP")]
        public decimal AnnualBudget { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [ValidateNever]
        public List<DeliveryMan> DeliveryMen { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }
    }
}

