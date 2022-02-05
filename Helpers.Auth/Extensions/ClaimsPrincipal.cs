using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Auth
        {
            public static ClaimsPrincipal AsClaimsPrincipal(this IIdentity identity)
                => new ClaimsPrincipal(identity);

            public static ClaimsPrincipal AsClaimsPrincipal(this IEnumerable<ClaimsIdentity> identities)
                => new ClaimsPrincipal(identities);

            public static ClaimsPrincipal AppendIdentities(this ClaimsPrincipal claimsPrincipal, IEnumerable<ClaimsIdentity> identities)
            {
                claimsPrincipal.AddIdentities(identities);
                return claimsPrincipal;
            }

            public static ClaimsPrincipal AppendIdentities(this ClaimsPrincipal claimsPrincipal, params ClaimsIdentity[] identities)
                => AppendIdentities(claimsPrincipal, identities ?? Enumerable.Empty<ClaimsIdentity>());
        }
    }
}
