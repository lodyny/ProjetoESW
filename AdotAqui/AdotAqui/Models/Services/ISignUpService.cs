using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public interface ISignUpService
    {
        Task SignUpAsync(User user);
    }
}
