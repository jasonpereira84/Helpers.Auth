﻿using System;
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
            private static AuthorizationPolicyBuilder requireClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, Func<IEnumerable<Claim>, String, Boolean> predicate, IEnumerable<String> requiredClaimTypes)
                => RequireThatClaims(
                    authorizationPolicyBuilder,
                    predicate: claims =>
                    {
                        if (requiredClaimTypes == null) throw new ArgumentNullException(nameof(requiredClaimTypes));
                        if (!requiredClaimTypes.Any()) throw new ArgumentException($"The {nameof(requiredClaimTypes)} cannot be NONE");

                        return requiredClaimTypes.All(claimType => predicate.Invoke(claims, claimType));
                    });
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, Func<IEnumerable<Claim>, String, Boolean> predicate, IEnumerable<String> requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, predicate, requiredClaimTypes);
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, Func<IEnumerable<Claim>, String, Boolean> predicate, params String[] requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, predicate, requiredClaimTypes);

            private static AuthorizationPolicyBuilder requireClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, IEnumerable<String> requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, (claims, requiredClaimType) => HasClaimOfType(claims, requiredClaimType, out Claim claim), requiredClaimTypes);
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, IEnumerable<String> requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, (claims, requiredClaimType) => HasClaimOfType(claims, requiredClaimType, out Claim claim), requiredClaimTypes);
            public static AuthorizationPolicyBuilder RequireClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, params String[] requiredClaimTypes)
                => requireClaimTypes(authorizationPolicyBuilder, requiredClaimTypes);
            #endregion RequireClaimTypes

            #region RequireAuthenticatedClaimTypes
            private static AuthorizationPolicyBuilder requireAuthenticatedClaimTypes(AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, IEnumerable<String> requiredClaimTypes, Boolean ignoreCase)
                => requireClaimTypes(authorizationPolicyBuilder, (claims, requiredClaimType) => HasClaimOfType(GetAuthenticatedClaims(claims, issuer, ignoreCase), requiredClaimType, out Claim claim), requiredClaimTypes);
            public static AuthorizationPolicyBuilder RequireAuthenticatedClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, IEnumerable<String> requiredClaimTypes, Boolean ignoreCase = true)
                => requireAuthenticatedClaimTypes(authorizationPolicyBuilder, issuer, requiredClaimTypes, ignoreCase);
            public static AuthorizationPolicyBuilder RequireAuthenticatedClaimTypes(this AuthorizationPolicyBuilder authorizationPolicyBuilder, String issuer, Boolean ignoreCase = true, params String[] requiredClaimTypes)
                => requireAuthenticatedClaimTypes(authorizationPolicyBuilder, issuer, requiredClaimTypes, ignoreCase);
            #endregion RequireAuthenticatedClaimTypes
        }
    }
}