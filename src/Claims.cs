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
            public static IEnumerable<Claim> GetClaimsOfType(this IEnumerable<Claim> claims, String claimType, Func<String, Boolean> predicate)
                => claims.Where(claim => predicate.Invoke(claim.Type));
            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType, Func<String, Boolean> predicate, out IEnumerable<Claim> claimsOfType)
                => (claimsOfType = GetClaimsOfType(claims, claimType, predicate)).Any();

            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType, Func<String, Boolean> predicate)
                => claims.Any(claim => predicate.Invoke(claim.Type));


            public static IEnumerable<Claim> GetClaimsOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = true)
                => claims.Where(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0));
            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType, out IEnumerable<Claim> claimsOfType, Boolean ignoreCase = true)
                => (claimsOfType = GetClaimsOfType(claims, claimType, ignoreCase)).Any();

            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = true)
                => claims.Any(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0));



            public static Claim GetClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, Func<String, Boolean> predicate, Claim defaultValue = default)
                => claims.FirstOrDefault(claim => predicate.Invoke(claim.Type)) ?? defaultValue;

            public static Boolean HasClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, Func<String, Boolean> predicate, out Claim claimOfType, Claim defaultValue = default)
            {
                claimOfType = claims.FirstOrDefault(claim => predicate.Invoke(claim.Type));
                if (claimOfType != null)
                    return true;

                claimOfType = defaultValue;
                return false;
            }


            public static Claim GetClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = true, Claim defaultValue = default)
                => claims.FirstOrDefault(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0)) ?? defaultValue;

            public static Boolean HasClaimOfTypeOrDefault(this IEnumerable<Claim> claims, String claimType, out Claim claimOfType, Boolean ignoreCase = true, Claim defaultValue = default)
            {
                claimOfType = claims.FirstOrDefault(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0));
                if (claimOfType != null)
                    return true;

                claimOfType = defaultValue;
                return false;
            }



            public static Boolean HasClaimOfType(this IEnumerable<Claim> claims, String claimType, Func<String, Boolean> predicate, out Claim claimOfType)
            {
                claimOfType = claims.FirstOrDefault(claim => predicate.Invoke(claim.Type));
                return claimOfType != null;
            }

            public static Claim GetClaimOfType(this IEnumerable<Claim> claims, String claimType, Boolean ignoreCase = true)
                => claims.First(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0));

            public static Boolean HasClaimOfType(this IEnumerable<Claim> claims, String claimType, out Claim claimOfType, Boolean ignoreCase = true)
            {
                claimOfType = claims.FirstOrDefault(claim => String.Compare(claimType, claim.Type, ignoreCase).Equals(0));
                return claimOfType != null;
            }



            public static Boolean IsAuthenticated(this Claim claim) => claim.Subject.IsAuthenticated;
            public static Boolean IsNotAuthenticated(this Claim claim) => !IsAuthenticated(claim);


            public static IEnumerable<Claim> GetAuthenticatedClaims(this IEnumerable<Claim> claims, String issuer, Boolean ignoreCase = true)
                => claims.Where(claim => (claim != null) && String.Compare(issuer, claim.Type, ignoreCase).Equals(0) && IsAuthenticated(claim));

            public static Boolean HasAuthenticatedClaims(this IEnumerable<Claim> claims, String issuer, out IEnumerable<Claim> authenticatedClaims, Boolean ignoreCase = true)
            {
                authenticatedClaims = GetAuthenticatedClaims(claims, issuer, ignoreCase);
                return authenticatedClaims?.Any() ?? false;
            }
            public static Boolean HasAuthenticatedClaims(this IEnumerable<Claim> claims, String issuer, Boolean ignoreCase = true)
            {
                var authenticatedClaims = GetAuthenticatedClaims(claims, issuer, ignoreCase);
                return authenticatedClaims?.Any() ?? false;
            }
            
        }
    }
}
