using System.ComponentModel.DataAnnotations;
using AdotAqui.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdotAqui.Areas.Identity.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public StatusMessage StatusMessage { get; set; }

        public InputModel Input { get; set; }

    }

    public class InputModel
    {
        [Required(ErrorMessage = "Error_EmailRequired")]
        [EmailAddress(ErrorMessage = "Error_EmailInvalid")]
        public string Email { get; set; }

        [RegularExpression("^\\d{9}$", ErrorMessage = "Error_PhoneInvalid")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Label_Phone")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Error_NameRequired")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Error_NameLengthInvalid")]
        [Display(Name = "Label_Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Error_BirthdateRequired")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Label_Birthday")]
        public string Birthday { get; set; }

        public string ImageURL { get; set;}
    }
}
