// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies

namespace ChurchWebAuthorization
{
    public class CustomClaimTypes
    {
        public const string Permission = "ChurchWeb/permission";
    }

    public class CustomClaims
    {
        public const string ChurchMember = "ChurchMember";
    }
}
