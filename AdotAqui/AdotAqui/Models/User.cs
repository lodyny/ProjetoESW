using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [Display(Prompt = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter the password")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Palavra-chave")]
        public string Password { get; set; }

        [Required]
        [Display(Prompt = "Nome")]
        public string Name { get; set; }

        [Display(Prompt = "Data de nascimento")]
        public string Birthday { get; set; }

        [Display(Prompt = "Telefone")]
        public string Phone { get; set; }
    }
}
