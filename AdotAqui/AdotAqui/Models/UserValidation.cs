using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models
{
    public class UserValidation
    {
        [Key]
        public int UserID { get; set; }
        
        public string ActivationKey { get; set; }
    }
}
