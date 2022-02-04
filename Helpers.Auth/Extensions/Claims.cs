using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.Globalization;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Auth
        {
            public static IEnumerable<Claim> GetClaimsOfType(this IEnumerable<Claim> claims, String claimType)
                => claims
                    .Where(c =>
                        c != null &&
                        !String.IsNullOrWhiteSpace(c.Type) &&
                        !String.IsNullOrWhiteSpace(c.Value) &&
                        c.Type.Equals(claimType));

            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType, out IEnumerable<Claim> claimsOfType)
                => (claimsOfType = GetClaimsOfType(claims, claimType)).Any();
            public static Boolean NotHasClaimsOfType(this IEnumerable<Claim> claims, String claimType, out IEnumerable<Claim> claimsOfType)
                => !HasClaimsOfType(claims, claimType, out claimsOfType);

            public static Boolean HasClaimsOfType(this IEnumerable<Claim> claims, String claimType)
                => claims.HasClaimsOfType(claimType, out _);
            public static Boolean NotHasClaimsOfType(this IEnumerable<Claim> claims, String claimType)
                => !HasClaimsOfType(claims, claimType);

            public static Nullable<Int64> GetClaimValueInt64(this IEnumerable<Claim> claims, String claimType)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? Int64.TryParse(claimsOfType.First().Value.Trim(), out Int64 value)
                        ? value
                        : default(Nullable<Int64>)
                    : default(Nullable<Int64>);

            public static Nullable<Int32> GetClaimValueInt32(this IEnumerable<Claim> claims, String claimType)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? Int32.TryParse(claimsOfType.First().Value.Trim(), out Int32 value)
                        ? value
                        : default(Nullable<Int32>)
                    : default(Nullable<Int32>);

            public static Nullable<DateTime> GetClaimValueDateTime(this IEnumerable<Claim> claims, String claimType)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? DateTime.TryParse(claimsOfType.First().Value.Trim(), out DateTime value)
                        ? value
                        : default(Nullable<DateTime>)
                    : default(Nullable<DateTime>);

            public static Nullable<DateTime> GetClaimValueDateTime(this IEnumerable<Claim> claims, String claimType, IFormatProvider formatProvider, DateTimeStyles styles)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? DateTime.TryParse(claimsOfType.First().Value.Trim(), formatProvider, styles, out DateTime value)
                        ? value
                        : default(Nullable<DateTime>)
                    : default(Nullable<DateTime>);

            public static Nullable<DateTimeOffset> GetClaimValueDateTimeOffset(this IEnumerable<Claim> claims, String claimType)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? DateTimeOffset.TryParse(claimsOfType.First().Value.Trim(), out DateTimeOffset value)
                        ? value
                        : default(Nullable<DateTimeOffset>)
                    : default(Nullable<DateTimeOffset>);

            public static Nullable<DateTimeOffset> GetClaimValueDateTimeOffset(this IEnumerable<Claim> claims, String claimType, IFormatProvider formatProvider, DateTimeStyles styles)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? DateTimeOffset.TryParse(claimsOfType.First().Value.Trim(), formatProvider, styles, out DateTimeOffset value)
                        ? value
                        : default(Nullable<DateTimeOffset>)
                    : default(Nullable<DateTimeOffset>);

            public static Nullable<DateTimeOffset> GetClaimValueUnixTimeSeconds(this IEnumerable<Claim> claims, String claimType)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? Int64.TryParse(claimsOfType.First().Value.Trim(), out Int64 value)
                        ? DateTimeOffset.FromUnixTimeSeconds(value)
                        : default(Nullable<DateTimeOffset>)
                    : default(Nullable<DateTimeOffset>);

            public static Boolean HasClaims(this IEnumerable<Claim> claims, String claimType, IEnumerable<String> expectedValues)
            {
                if (expectedValues == null)
                    throw new ArgumentNullException(nameof(expectedValues));

                if (!expectedValues.Any())
                    throw new ArgumentException($"The '{nameof(expectedValues)}' cannot be empty.", nameof(expectedValues));

                if (NotHasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType))
                    return false;

                foreach (var claimValue in claimsOfType.Select(c => c.Value.Trim()))
                    if (!expectedValues.Contains(claimValue))
                        return false;
                return true;
            }
            public static Boolean NotHasClaims(this IEnumerable<Claim> claims, String claimType, IEnumerable<String> expectedValues)
                => !HasClaims(claims, claimType, expectedValues);

            public static Boolean HasClaims(this IEnumerable<Claim> claims, String claimType, params String[] expectedValues)
                => HasClaims(claims, claimType, expectedValues ?? Enumerable.Empty<String>());
            public static Boolean NotHasClaims(this IEnumerable<Claim> claims, String claimType, params String[] expectedValues)
                => NotHasClaims(claims, claimType, expectedValues ?? Enumerable.Empty<String>());

            public static Boolean HasClaim(this IEnumerable<Claim> claims, String claimType, String expectedValue)
                => HasClaims(claims, claimType, new[] { expectedValue });
            public static Boolean NotHasClaim(this IEnumerable<Claim> claims, String claimType, String expectedValue)
                => !HasClaim(claims, claimType, expectedValue);

            public static Boolean AnyClaims(this IEnumerable<Claim> claims, String claimType, IEnumerable<String> expectedValues)
            {
                if (expectedValues == null)
                    throw new ArgumentNullException(nameof(expectedValues));

                if (!expectedValues.Any())
                    throw new ArgumentException($"The '{nameof(expectedValues)}' cannot be empty.", nameof(expectedValues));

                if (NotHasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType))
                    return false;

                foreach (var claimValue in claimsOfType.Select(c => c.Value.Trim()))
                    if (expectedValues.Contains(claimValue))
                        return true;
                return false;
            }
            public static Boolean NotAnyClaims(this IEnumerable<Claim> claims, String claimType, IEnumerable<String> expectedValues)
                => !AnyClaims(claims, claimType, expectedValues);

            public static Boolean AnyClaims(this IEnumerable<Claim> claims, String claimType, params String[] expectedValues)
                => AnyClaims(claims, claimType, expectedValues ?? Enumerable.Empty<String>());
            public static Boolean NotAnyClaims(this IEnumerable<Claim> claims, String claimType, params String[] expectedValues)
                => NotAnyClaims(claims, claimType, expectedValues ?? Enumerable.Empty<String>());
        }
    }
}
