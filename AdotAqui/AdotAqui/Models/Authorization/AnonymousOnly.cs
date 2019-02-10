using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Authorization Models
/// </summary>
namespace AdotAqui.Models.Authorization
{
    /// <summary>
    /// Class used to allow only anonymous
    /// </summary>
    public class AnonymousOnly : AuthorizationHandler<AnonymousOnly>, IAuthorizationRequirement
    {
        /// <summary>
        /// Handle user auth
        /// </summary>
        /// <param name="context">Authorization Handler Context</param>
        /// <param name="requirement">Requirement</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AnonymousOnly requirement)
        {
            if (!context.User.Identity.IsAuthenticated) {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
