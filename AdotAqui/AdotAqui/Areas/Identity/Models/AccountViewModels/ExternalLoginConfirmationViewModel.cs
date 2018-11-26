using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Areas.Identity.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Error_EmailRequired")]
        [EmailAddress(ErrorMessage = "Error_EmailInvalid")]
        [Display(Prompt = "Label_Email")]
        public string Email { get; set; }
    }
}
