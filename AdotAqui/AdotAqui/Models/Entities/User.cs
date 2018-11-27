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
        [Display(Prompt = "Label_Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,100}$", ErrorMessage = "Error_PasswordInvalid")]
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
        [RegularExpression("^\\d{9}$", ErrorMessage = "Error_PhoneInvalid")]
        public override string PhoneNumber { get; set; }

        public bool Banned { get; set; }
    }
}
