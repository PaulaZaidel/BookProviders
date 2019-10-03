using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookProviders.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(1000, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Required")]
        public decimal Price { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }

        [Display(Name = "Active?")]
        public bool Active { get; set; }

        public CatererViewModel Caterer { get; set; }
    }
}
