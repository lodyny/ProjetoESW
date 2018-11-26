using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models
{
    /// <summary>
    /// Class used to represent the User
    /// </summary>
    public class User : IdentityUser<int>
    {
        [Required(ErrorMessage = "Error_EmailRequired")]
        [EmailAddress(ErrorMessage = "Error_EmailInvalid")]
        [Display(Prompt = "Label_Email")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Error_PasswordRequired")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Error_PasswordLengthInvalid", MinimumLength = 6)]
        [Display(Prompt = "Label_Password")]
        public override string PasswordHash { get; set; }

        [Required(ErrorMessage = "Error_NameRequired")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Error_NameLengthInvalid")]
        [Display(Prompt = "Label_Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Error_BirthdateRequired")]
        [DataType(DataType.DateTime)]
        [Display(Prompt = "Label_Birthday")]
        public string Birthday { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Prompt = "Label_Phone")]
        public override string PhoneNumber { get; set; }

        public bool Banned { get; set; }
    }
}
