using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookProviders.App.ViewModels
{
    public class CatererViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(14, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Document { get; set; }

        [Display(Name = "Type")]
        public int CatererType { get; set; }
        public AddressViewModels Adress { get; set; }

        [Display(Name = "Active?")]
        public bool Active { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
