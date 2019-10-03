using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookProviders.App.ViewModels
{
    public class AddressViewModels
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(400, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
        public string Number { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(8, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string State { get; set; }

        [HiddenInput]
        public Guid CatererId { get; set; }
    }
}
