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

        [BindProperty]
        public InputModel Input { get; set; }
    }
    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Data Nascimento")]
        public string Birthday { get; set; }
    }
}
