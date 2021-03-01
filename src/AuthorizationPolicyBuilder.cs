using System;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Authorization;

        public static partial class Auth
        {
            public static AuthorizationPolicyBuilder RequireThatClaims(this AuthorizationPolicyBuilder authorizationPolicyBuilder, Func<IEnumerable<Claim>, Boolean> predicate)
                => authorizationPolicyBuilder.RequireAssertion(context => predicate.Invoke(context.User.Claims));

            #region RequireClaimTypes
            private static AuthorizationPolicyBuilder requireClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, IEnumerable<String> requiredClaimTypes)
            {
                if (requiredClaimTypes == null)
                    throw new ArgumentNullException(nameof(requiredClaimTypes));

                if (!requiredClaimTypes.Any())
                    throw new ArgumentException($"The {nameof(requiredClaimTypes)} cannot be NONE");

                return authorizationPolicyBuilder
                    .RequireAssertion(context =>
                        requiredClaimTypes
                            .All(requiredClaimType =>
                                HasClaimOfType(context.User.Claims, requiredClaimType, out Claim claim) && claim.Value.IsNotNullOrEmptyOrWhiteSpace()));
            }
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, IEnumerable<String> requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, requiredClaimTypes);
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, params String[] requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, requiredClaimTypes);
            #endregion RequireClaimTypes

            #region RequireAuthenticatedClaimTypes
            private static AuthorizationPolicyBuilder requireAuthenticatedClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, IEnumerable<String> requiredClaimTypes)
            {
                if (requiredClaimTypes == null)
                    throw new ArgumentNullException(nameof(requiredClaimTypes));

                if (!requiredClaimTypes.Any())
                    throw new ArgumentException($"The {nameof(requiredClaimTypes)} cannot be NONE");

                return authorizationPolicyBuilder
                    .RequireAssertion(context =>
                    {
                        var authenticatedClaims = GetAuthenticatedClaims(context.User.Claims, issuer);

                        return requiredClaimTypes
                            .All(requiredClaimType =>
                                HasClaimOfType(authenticatedClaims, requiredClaimType, out Claim claim) && claim.Value.IsNotNullOrEmptyOrWhiteSpace());
                    });
            }
            public static AuthorizationPolicyBuilder RequireAuthenticatedClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, IEnumerable<String> requiredClaimTypes)
                => requireAuthenticatedClaimTypes(authorizationPolicyBuilder, issuer, requiredClaimTypes);
            public static AuthorizationPolicyBuilder RequireAuthenticatedClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, params String[] requiredClaimTypes)
                => requireAuthenticatedClaimTypes(authorizationPolicyBuilder, issuer, requiredClaimTypes);
            #endregion RequireAuthenticatedClaimTypes
        }
    }
}