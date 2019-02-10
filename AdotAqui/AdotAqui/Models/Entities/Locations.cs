using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Entities
{
    /// <summary>
    /// Represents a user location
    /// </summary>
    public class Locations
    {
        [Key]
        public int LocationID { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal longitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal latitude { get; set; }

        public string country { get; set; }
    }
}
