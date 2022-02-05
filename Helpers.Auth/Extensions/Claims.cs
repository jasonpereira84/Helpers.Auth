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

            public static Boolean HasAllClaimsOfType(this IEnumerable<Claim> claims, IEnumerable<String> requiredClaimTypes) 
            {
                if (requiredClaimTypes == null)
                    throw new ArgumentNullException(nameof(requiredClaimTypes));

                if (!requiredClaimTypes.Any())
                    throw new ArgumentException($"The '{nameof(requiredClaimTypes)}' cannot be empty.", nameof(requiredClaimTypes));

                var claimTypes = claims
                    .Where(c =>
                        c != null &&
                        !String.IsNullOrWhiteSpace(c.Type) &&
                        !String.IsNullOrWhiteSpace(c.Value))
                    .Select(c => c.Type.Trim())
                    .Distinct();

                return requiredClaimTypes
                    .All(requiredClaimType => claimTypes.Contains(requiredClaimType));
            }

            public static Boolean HasAllClaimsOfType(this IEnumerable<Claim> claims, params String[] requiredClaimTypes)
                => HasAllClaimsOfType(claims, requiredClaimTypes ?? Enumerable.Empty<String>());

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

            public static Nullable<Int16> GetClaimValueInt16(this IEnumerable<Claim> claims, String claimType)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? Int16.TryParse(claimsOfType.First().Value.Trim(), out Int16 value)
                        ? value
                        : default(Nullable<Int16>)
                    : default(Nullable<Int16>);

            public static Nullable<DateTime> GetClaimValueDateTime(this IEnumerable<Claim> claims, String claimType, IFormatProvider formatProvider, DateTimeStyles styles)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? DateTime.TryParse(claimsOfType.First().Value.Trim(), formatProvider, styles, out DateTime value)
                        ? value
                        : default(Nullable<DateTime>)
                    : default(Nullable<DateTime>);

            public static Nullable<DateTime> GetClaimValueDateTime(this IEnumerable<Claim> claims, String claimType)
                => GetClaimValueDateTime(claims, claimType, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);

            public static Nullable<DateTimeOffset> GetClaimValueDateTimeOffset(this IEnumerable<Claim> claims, String claimType, IFormatProvider formatProvider, DateTimeStyles styles)
                => HasClaimsOfType(claims, claimType, out IEnumerable<Claim> claimsOfType)
                    ? DateTimeOffset.TryParse(claimsOfType.First().Value.Trim(), formatProvider, styles, out DateTimeOffset value)
                        ? value
                        : default(Nullable<DateTimeOffset>)
                    : default(Nullable<DateTimeOffset>);

            public static Nullable<DateTimeOffset> GetClaimValueDateTimeOffset(this IEnumerable<Claim> claims, String claimType)
                => GetClaimValueDateTimeOffset(claims, claimType, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);

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

                var claimValues = claimsOfType.Select(c => c.Value.Trim());
                foreach (var expectedValue in expectedValues)
                    if (!claimValues.Contains(expectedValue))
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

                var claimValues = claimsOfType.Select(c => c.Value.Trim());
                foreach (var expectedValue in expectedValues)
                    if (claimValues.Contains(expectedValue))
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
