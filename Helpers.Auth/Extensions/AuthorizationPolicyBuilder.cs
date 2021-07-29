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
            private static AuthorizationPolicyBuilder requireClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, IEnumerable<String> requiredClaimTypes, Boolean ignoreCase)
            {
                if (requiredClaimTypes == null)
                    throw new ArgumentNullException(nameof(requiredClaimTypes));

                if (!requiredClaimTypes.Any())
                    throw new ArgumentException($"The {nameof(requiredClaimTypes)} cannot be NONE");

                return authorizationPolicyBuilder
                    .RequireAssertion(context =>
                        requiredClaimTypes
                            .All(requiredClaimType =>
                                HasClaimOfType(context.User.Claims, requiredClaimType, out Claim claim, ignoreCase) && claim.Value.IsNotNullOrEmptyOrWhiteSpace()));
            }
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, IEnumerable<String> requiredClaimTypes, Boolean ignoreCase = false)
                => requireClaimTypes(authorizationPolicyBuilder, requiredClaimTypes, ignoreCase);
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, Boolean ignoreCase = false, params String[] requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, requiredClaimTypes, ignoreCase);
            #endregion RequireClaimTypes

            #region RequireAuthenticatedClaimTypes
            private static AuthorizationPolicyBuilder requireAuthenticatedClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, IEnumerable<String> requiredClaimTypes, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimTypes = false)
            {
                if (requiredClaimTypes == null)
                    throw new ArgumentNullException(nameof(requiredClaimTypes));

                if (!requiredClaimTypes.Any())
                    throw new ArgumentException($"The {nameof(requiredClaimTypes)} cannot be NONE");

                return authorizationPolicyBuilder
                    .RequireAssertion(context =>
                    {
                        var authenticatedClaims = GetAuthenticatedClaims(context.User.Claims, issuer, ignoreCaseForIssuer);

                        return requiredClaimTypes
                            .All(requiredClaimType =>
                                HasClaimOfType(authenticatedClaims, requiredClaimType, out Claim claim, ignoreCaseForClaimTypes) && claim.Value.IsNotNullOrEmptyOrWhiteSpace());
                    });
            }
            public static AuthorizationPolicyBuilder RequireAuthenticatedClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, IEnumerable<String> requiredClaimTypes, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimTypes = false)
                => requireAuthenticatedClaimTypes(authorizationPolicyBuilder, issuer, requiredClaimTypes, ignoreCaseForIssuer, ignoreCaseForClaimTypes);
            public static AuthorizationPolicyBuilder RequireAuthenticatedClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimTypes = false, params String[] requiredClaimTypes)
                => requireAuthenticatedClaimTypes(authorizationPolicyBuilder, issuer, requiredClaimTypes, ignoreCaseForIssuer, ignoreCaseForClaimTypes);
            #endregion RequireAuthenticatedClaimTypes

            public static AuthorizationPolicyBuilder RequireThatClaim(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String claimType, Func<Claim, Boolean> predicate, Boolean ignoreCase = false, Claim defaultValue = default)
                => authorizationPolicyBuilder.RequireAssertion(context => predicate.Invoke(context.User.Claims.GetClaimOfTypeOrDefault(claimType, ignoreCase, defaultValue)));

            public static AuthorizationPolicyBuilder RequireThatAuthenticatedClaim(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String claimType, String issuer, Func<Claim, Boolean> predicate, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false, Claim defaultValue = default)
                => authorizationPolicyBuilder.RequireAssertion(context => predicate.Invoke(context.User.Claims.GetAuthenticatedClaimOfTypeOrDefault(claimType, issuer, ignoreCaseForIssuer, ignoreCaseForClaimType, defaultValue)));

        }
    }
}