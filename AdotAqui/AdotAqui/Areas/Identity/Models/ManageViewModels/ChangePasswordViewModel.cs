using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Areas.Identity.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Error_PasswordRequired")]
        [StringLength(100, ErrorMessage = "Error_PasswordLengthInvalid", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Error_PasswordRequired")]
        [StringLength(100, ErrorMessage = "Error_PasswordLengthInvalid", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Label_NewPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Error_ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Prompt = "Label_ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
