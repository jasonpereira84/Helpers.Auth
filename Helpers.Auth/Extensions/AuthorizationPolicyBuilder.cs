using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Authorization;

        public static partial class Auth
        {
            public static AuthorizationPolicyBuilder RequireThatClaims(this AuthorizationPolicyBuilder authorizationPolicyBuilder, Func<IEnumerable<Claim>, Boolean> predicate)
                => authorizationPolicyBuilder
                    .RequireAssertion(context => predicate.Invoke(context.User.Claims));

            public static AuthorizationPolicyBuilder RequireClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, IEnumerable<String> requiredClaimTypes)
            {
                if (requiredClaimTypes == null)
                    throw new ArgumentNullException(nameof(requiredClaimTypes));

                if (!requiredClaimTypes.Any())
                    throw new ArgumentException($"The '{nameof(requiredClaimTypes)}' cannot be empty.", nameof(requiredClaimTypes));

                return authorizationPolicyBuilder
                    .RequireAssertion(context =>
                        requiredClaimTypes
                            .All(requiredClaimType =>
                                HasClaimsOfType(context.User.Claims, requiredClaimType)));
            }

            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, params String[] requiredClaimTypes)
                => RequireClaimTypes(authorizationPolicyBuilder, requiredClaimTypes ?? Enumerable.Empty<String>());

            public static AuthorizationPolicyBuilder RequireThatClaim(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String claimType, Func<Claim, Boolean> predicate)
                => authorizationPolicyBuilder
                    .RequireAssertion(context => predicate.Invoke(context.User.Claims.GetClaimsOfType(claimType).FirstOrDefault()));
        }
    }
}