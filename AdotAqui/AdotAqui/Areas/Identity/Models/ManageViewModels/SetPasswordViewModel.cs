using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Areas.Identity.Models.ManageViewModels
{
    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Error_PasswordRequired")]
        [RegularExpression(@"^(?=.*\W)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\W]{6,}$", ErrorMessage = "Error_PasswordInvalid")]
        [DataType(DataType.Password)]
        [Display(Name = "Label_NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Label_NewConfirmationPassword")]
        [Compare("NewPassword", ErrorMessage = "Error_PasswordMismatch")]
        public string ConfirmPassword { get; set; }
    }
}
