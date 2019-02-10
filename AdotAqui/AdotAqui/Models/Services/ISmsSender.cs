using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Interface used to representate the SMS Service
    /// </summary>
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
