using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// SMS Service
    /// </summary>
    public class SmsSender : ISmsSender
    {
        /// <summary>
        /// Used to send SMS async
        /// </summary>
        /// <param name="number">User Number</param>
        /// <param name="message">Message</param>
        /// <returns>Task</returns>
        public Task SendSmsAsync(string number, string message)
        {
            throw new NotImplementedException();
        }
    }
}
