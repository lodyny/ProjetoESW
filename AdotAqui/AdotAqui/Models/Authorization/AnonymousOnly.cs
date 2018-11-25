using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Authorization
{
    
    public class AnonymousOnly : AuthorizationHandler<AnonymousOnly>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AnonymousOnly requirement)
        {
            if (!context.User.Identity.IsAuthenticated) {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
