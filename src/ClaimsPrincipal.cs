using System;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Collections.Generic;


namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Auth
        {
            public static ClaimsPrincipal AppendIdentities(this ClaimsPrincipal claimsPrincipal, IEnumerable<ClaimsIdentity> additionalIdentities)
            {
                claimsPrincipal.AddIdentities(identities: additionalIdentities);
                return claimsPrincipal;
            }

            public static ClaimsPrincipal AppendIdentities(this ClaimsPrincipal claimsPrincipal, params ClaimsIdentity[] additionalIdentities)
                => AppendIdentities(claimsPrincipal, additionalIdentities ?? Enumerable.Empty<ClaimsIdentity>());

            public static ClaimsPrincipal AsClaimsPrincipal(this IIdentity primaryIdentity)
                => new ClaimsPrincipal(primaryIdentity);

            public static ClaimsPrincipal AsClaimsPrincipal(this IIdentity primaryIdentity, IEnumerable<ClaimsIdentity> additionalIdentities)
                => AppendIdentities(new ClaimsPrincipal(primaryIdentity), additionalIdentities);

            public static ClaimsPrincipal AsClaimsPrincipal(this (IIdentity Primary, IEnumerable<ClaimsIdentity> Additional) identities)
                => AsClaimsPrincipal(identities.Primary, identities.Additional);

        }
    }
}
