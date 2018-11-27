using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Areas.Identity.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Error_EmailRequired")]
        [EmailAddress(ErrorMessage ="Error_EmailInvalid")]
        [Display(Prompt = "Label_Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Error_PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Label_Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,100}$", ErrorMessage = "Error_PasswordInvalid")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Prompt = "Label_ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Error_PasswordMismatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
