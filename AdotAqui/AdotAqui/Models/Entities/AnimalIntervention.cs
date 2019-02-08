using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Entities
{
    public class AnimalIntervention
    {
        [Key]
        public int InterventionId { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int Date { get; set; }
        public bool Completed { get; set; }
        public string Details { get; set; }
    }
}
