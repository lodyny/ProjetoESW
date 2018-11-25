using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.ViewModels
{
    public class StatusMessage
    {
        public MessageType Type { get; set; }
        public string Value { get; set; }
    }

    public enum MessageType
    {
        Success,
        Warning,
        Error,
        Info
    }
}
