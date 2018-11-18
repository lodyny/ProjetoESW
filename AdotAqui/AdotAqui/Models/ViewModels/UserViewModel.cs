﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models
{
    public class UserViewModel : User
    {
        [NotMapped]
        [Required(ErrorMessage = "Confirm required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Prompt = "Confirmar Palavra-chave")]
        public string ConfirmPassword { get; set; }

    }
}