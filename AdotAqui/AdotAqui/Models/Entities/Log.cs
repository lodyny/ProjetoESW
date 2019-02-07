using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Entities
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }

        public string LogType { get; set; }
        public string LogValue { get; set; }
        public DateTime LogDate { get; set; }
    }
}
