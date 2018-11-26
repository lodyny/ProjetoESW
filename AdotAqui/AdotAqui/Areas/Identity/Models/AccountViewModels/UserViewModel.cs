using AdotAqui.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Areas.Identity.Models.AccountViewModels
{
    public class UserViewModel : User
    {
        [NotMapped]
        [Required(ErrorMessage = "Error_ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [Compare("PasswordHash")]
        [Display(Prompt = "Label_ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
