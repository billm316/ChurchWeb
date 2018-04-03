using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChurchWebAuthorization
{
    public class ChurchMemberHandler : AuthorizationHandler<ChurchMember>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ChurchMember requirement)
        {
            if (context.User.HasClaim(c => c.Type == CustomClaimTypes.Permission &&
                 c.Value == CustomClaims.ChurchMember))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
