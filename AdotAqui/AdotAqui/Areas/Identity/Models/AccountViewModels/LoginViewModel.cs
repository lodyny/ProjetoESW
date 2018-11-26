using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Areas.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Error_EmailRequired")]
        [EmailAddress(ErrorMessage = "Error_EmailInvalid")]
        [Display(Prompt = "Label_Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Error_PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Label_Password")]
        public string Password { get; set; }

        [Display(Name = "Label_RememberMe")]
        public bool RememberMe { get; set; }
    }
}
