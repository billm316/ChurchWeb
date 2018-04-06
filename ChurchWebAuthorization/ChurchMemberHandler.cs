using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ChurchWebAuthorization
{
    // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies
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
