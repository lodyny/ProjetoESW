using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Entities
{
    public class AnimalComment
    {
        [Key]
        public int CommentId { get; set; }

        public int AnimalId { get; set; }
        public int UserId { get; set; }
        public string Commentary { get; set; }
        public DateTime InsertDate { get; set; }

    }
}
