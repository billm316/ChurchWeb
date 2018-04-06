using Microsoft.AspNetCore.Authorization;

// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies
namespace ChurchWebAuthorization
{
    public class ChurchMember : IAuthorizationRequirement
    {
    }
}
