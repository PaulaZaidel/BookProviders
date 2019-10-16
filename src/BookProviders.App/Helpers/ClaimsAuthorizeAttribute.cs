using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookProviders.App.Helpers
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
            : base(typeof(ClaimFilterRequirement))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
