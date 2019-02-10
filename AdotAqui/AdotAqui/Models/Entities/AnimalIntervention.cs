using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Entities
{
    public class AnimalIntervention
    {
        [Key]
        public int InterventionId { get; set; }

        [Required]
        public virtual Animal Animal { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Completed { get; set; }

        public string Details { get; set; }
    }
}
