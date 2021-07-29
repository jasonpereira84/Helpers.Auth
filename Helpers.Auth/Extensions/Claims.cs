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
            public static Boolean IsAuthenticated(this Claim claim) => claim.Subject.IsAuthenticated;
            public static Boolean IsNotAuthenticated(this Claim claim) => !IsAuthenticated(claim);


            public static Boolean IsAuthenticated(this Claim claim, String issuer, Boolean ignoreCase = false)
                => String.Compare(issuer, claim.Issuer, ignoreCase).Equals(0) && IsAuthenticated(claim);
            public static Boolean IsNotAuthenticated(this Claim claim, String issuer, Boolean ignoreCase = false)
                => !IsAuthenticated(claim, issuer, ignoreCase);


            public static IEnumerable<Claim> GetAuthenticatedClaims(this IEnumerable<Claim> claims)
                => claims.Where(claim => IsAuthenticated(claim));
            public static Boolean HasAuthenticatedClaims(this IEnumerable<Claim> claims, out IEnumerable<Claim> authenticatedClaims)
                => (authenticatedClaims = GetAuthenticatedClaims(claims)).Any();
            public static Boolean HasAuthenticatedClaims(this IEnumerable<Claim> claims)
                => claims.Any(claim => IsAuthenticated(claim));


            public static IEnumerable<Claim> GetAuthenticatedClaims(this IEnumerable<Claim> claims, String issuer, Boolean ignoreCase = false)
                => claims.Where(claim => IsAuthenticated(claim, issuer, ignoreCase));
            public static Boolean HasAuthenticatedClaims(this IEnumerable<Claim> claims, String issuer, out IEnumerable<Claim> authenticatedClaims, Boolean ignoreCase = false)
                => (authenticatedClaims = GetAuthenticatedClaims(claims, issuer, ignoreCase)).Any();
            public static Boolean HasAuthenticatedClaims(this IEnumerable<Claim> claims, String issuer, Boolean ignoreCase = false)
                => claims.Any(claim => IsAuthenticated(claim, issuer, ignoreCase));


            public static IEnumerable<Claim> GetClaimsOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false)
                => claims.Where(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0));
            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType, out IEnumerable<Claim> claimsOfType, Boolean ignoreCase = false)
                => (claimsOfType = GetClaimsOfType(claims, claimType, ignoreCase)).Any();
            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false)
                => claims.Any(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0));


            public static Claim GetClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false, Claim defaultValue = default)
                => claims.FirstOrDefault(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0)) ?? defaultValue;
            public static Boolean HasClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, out Claim claimOfType, Boolean ignoreCase = false, Claim defaultValue = default)
            {
                claimOfType = GetClaimOfTypeOrDefault(claims, claimType, ignoreCase, null);
                if (claimOfType != null)
                    return true;

                claimOfType = defaultValue;
                return false;
            }


            public static Claim GetClaimOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false)
                => GetClaimOfTypeOrDefault(claims, claimType, ignoreCase, default);
            public static Boolean HasClaimOfType(this IEnumerable<Claim> claims, String claimType, out Claim claimOfType, Boolean ignoreCase = false)
                => HasClaimOfTypeOrDefault(claims, claimType, out claimOfType, ignoreCase, default);


            public static IEnumerable<Claim> GetAuthenticatedClaimsOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false)
                => claims.Where(claim => IsAuthenticated(claim) && String.Compare(claimType, claim.Type, ignoreCase).Equals(0));
            public static Boolean HasAuthenticatedClaimsOfType(this IEnumerable<Claim> claims, String claimType, out IEnumerable<Claim> claimsOfType, Boolean ignoreCase = false)
                => (claimsOfType = GetAuthenticatedClaimsOfType(claims, claimType, ignoreCase)).Any();
            public static Boolean HasAuthenticatedClaimsOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false)
                => claims.Any(claim => IsAuthenticated(claim) && String.Compare(claimType, claim.Type, ignoreCase).Equals(0));


            public static Claim GetAuthenticatedClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false, Claim defaultValue = default)
                => claims.FirstOrDefault(claim => IsAuthenticated(claim) && String.Compare(claimType, claim.Type, ignoreCase).Equals(0)) ?? defaultValue;
            public static Boolean HasAuthenticatedClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, out Claim claimOfType, Boolean ignoreCase = false, Claim defaultValue = default)
            {
                claimOfType = GetAuthenticatedClaimOfTypeOrDefault(claims, claimType, ignoreCase, null);
                if (claimOfType != null)
                    return true;

                claimOfType = defaultValue;
                return false;
            }


            public static Claim GetAuthenticatedClaimOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = false)
                => GetAuthenticatedClaimOfTypeOrDefault(claims, claimType, ignoreCase, default);
            public static Boolean HasAuthenticatedClaimOfType(this IEnumerable<Claim> claims, String claimType, out Claim claimOfType, Boolean ignoreCase = false)
                => HasAuthenticatedClaimOfTypeOrDefault(claims, claimType, out claimOfType, ignoreCase, default);


            public static IEnumerable<Claim> GetAuthenticatedClaimsOfType(this IEnumerable<Claim> claims, String claimType, String issuer, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false)
                => claims.Where(claim => IsAuthenticated(claim, issuer, ignoreCaseForIssuer) && String.Compare(claimType, claim.Type, ignoreCaseForClaimType).Equals(0));
            public static Boolean HasAuthenticatedClaimsOfType(this IEnumerable<Claim> claims, String claimType, String issuer, out IEnumerable<Claim> claimsOfType, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false)
                => (claimsOfType = GetAuthenticatedClaimsOfType(claims, claimType, issuer, ignoreCaseForIssuer, ignoreCaseForClaimType)).Any();
            public static Boolean HasAuthenticatedClaimsOfType(this IEnumerable<Claim> claims, String claimType, String issuer, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false)
                => claims.Any(claim => IsAuthenticated(claim, issuer, ignoreCaseForIssuer) && String.Compare(claimType, claim.Type, ignoreCaseForClaimType).Equals(0));


            public static Claim GetAuthenticatedClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, String issuer, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false, Claim defaultValue = default)
                => claims.FirstOrDefault(claim => IsAuthenticated(claim, issuer, ignoreCaseForIssuer) && String.Compare(claimType, claim.Type, ignoreCaseForClaimType).Equals(0)) ?? defaultValue;
            public static Boolean HasAuthenticatedClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, String issuer, out Claim claimOfType, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false, Claim defaultValue = default)
            {
                claimOfType = GetAuthenticatedClaimOfTypeOrDefault(claims, claimType, issuer, ignoreCaseForIssuer, ignoreCaseForClaimType, null);
                if (claimOfType != null)
                    return true;

                claimOfType = defaultValue;
                return false;
            }


            public static Claim GetAuthenticatedClaimOfType(this IEnumerable<Claim> claims, String claimType, String issuer, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false)
                => GetAuthenticatedClaimOfTypeOrDefault(claims, claimType, issuer, ignoreCaseForIssuer, ignoreCaseForClaimType, default);
            public static Boolean HasAuthenticatedClaimOfType(this IEnumerable<Claim> claims, String claimType, String issuer, out Claim claimOfType, Boolean ignoreCaseForIssuer = false, Boolean ignoreCaseForClaimType = false)
                => HasAuthenticatedClaimOfTypeOrDefault(claims, claimType, issuer, out claimOfType, ignoreCaseForIssuer, ignoreCaseForClaimType, default);

        }
    }
}
