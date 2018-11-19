﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [EmailAddress]
        [Display(Prompt = "E-mail")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Please Enter the password")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Palavra-chave")]
        public override string PasswordHash { get; set; }

        [Required]
        [Display(Prompt = "Nome")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Prompt = "Data de nascimento")]
        public string Birthday { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Prompt = "Telefone")]
        public override string PhoneNumber { get; set; }
    }
}
