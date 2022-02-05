using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace JasonPereira84.Helpers
{
    public abstract class _Principal
    {
        protected internal ImmutableDictionary<String, IEnumerable<String>> _claimMap;

        protected _Principal(IEnumerable<Claim> claims)
        {
            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            _claimMap = ImmutableDictionary.CreateRange(
                claims
                    .Where(c =>
                        c != null &&
                        !String.IsNullOrWhiteSpace(c.Type) &&
                        !String.IsNullOrWhiteSpace(c.Value))
                    .Select(c => new
                    {
                        Type = c.Type.Trim(),
                        Value = c.Value.Trim()
                    })
                    .GroupBy(a => a.Type)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(a => a.Value).Distinct()));
        }

        public IEnumerable<String> GetClaimValues(String claimType)
            => _claimMap.ContainsKey(claimType) 
                ? _claimMap[claimType]
                : Enumerable.Empty<String>();

        public Nullable<Int64> GetClaimValueInt64(String claimType)
            => _claimMap.ContainsKey(claimType)
                ? Int64.TryParse(_claimMap[claimType].First(), out Int64 value)
                    ? value
                    : default(Nullable<Int64>)
                : default(Nullable<Int64>);

        public Nullable<Int32> GetClaimValueInt32(String claimType)
            => _claimMap.ContainsKey(claimType)
                ? Int32.TryParse(_claimMap[claimType].First(), out Int32 value)
                    ? value
                    : default(Nullable<Int32>)
                : default(Nullable<Int32>);

        public Nullable<Int16> GetClaimValueInt16(String claimType)
            => _claimMap.ContainsKey(claimType)
                ? Int16.TryParse(_claimMap[claimType].First(), out Int16 value)
                    ? value
                    : default(Nullable<Int16>)
                : default(Nullable<Int16>);

        public Nullable<DateTime> GetClaimValueDateTime(String claimType, IFormatProvider formatProvider, DateTimeStyles styles)
            => _claimMap.ContainsKey(claimType)
                ? DateTime.TryParse(_claimMap[claimType].First(), formatProvider, styles, out DateTime value)
                    ? value
                    : default(Nullable<DateTime>)
                : default(Nullable<DateTime>);

        public Nullable<DateTime> GetClaimValueDateTime(String claimType)
            => GetClaimValueDateTime(claimType, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);

        public Nullable<DateTimeOffset> GetClaimValueDateTimeOffset(String claimType, IFormatProvider formatProvider, DateTimeStyles styles)
            => _claimMap.ContainsKey(claimType)
                ? DateTimeOffset.TryParse(_claimMap[claimType].First(), formatProvider, styles, out DateTimeOffset value)
                    ? value
                    : default(Nullable<DateTimeOffset>)
                : default(Nullable<DateTimeOffset>);

        public Nullable<DateTimeOffset> GetClaimValueDateTimeOffset(String claimType)
            => GetClaimValueDateTimeOffset(claimType, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);

        public Nullable<DateTimeOffset> GetClaimValueUnixTimeSeconds(String claimType)
            => _claimMap.ContainsKey(claimType)
                ? Int64.TryParse(_claimMap[claimType].First(), out Int64 value)
                    ? DateTimeOffset.FromUnixTimeSeconds(value)
                    : default(Nullable<DateTimeOffset>)
                : default(Nullable<DateTimeOffset>);

        public Boolean HasClaims(String claimType, IEnumerable<String> expectedValues)
        {
            if (expectedValues == null)
                throw new ArgumentNullException(nameof(expectedValues));

            if (!expectedValues.Any())
                throw new ArgumentException($"The '{nameof(expectedValues)}' cannot be empty.", nameof(expectedValues));

            var claimValues = GetClaimValues(claimType);
            if (!claimValues.Any())
                return false;

            foreach (var expectedValue in expectedValues)
                if (!claimValues.Contains(expectedValue))
                    return false;
            return true;
        }
        public Boolean NotHasClaims(String claimType, IEnumerable<String> expectedValues)
            => !HasClaims(claimType, expectedValues);

        public Boolean HasClaims(String claimType, params String[] expectedValues)
            => HasClaims(claimType, expectedValues ?? Enumerable.Empty<String>());
        public Boolean NotHasClaims(String claimType, params String[] expectedValues)
            => NotHasClaims(claimType, expectedValues ?? Enumerable.Empty<String>());

        public Boolean HasClaim(String claimType, String expectedValue)
            => HasClaims(claimType, new[] { expectedValue });
        public Boolean NotHasClaim(String claimType, String expectedValue)
            => !HasClaim(claimType, expectedValue);

        public Boolean HasClaimType(String claimType)
            => _claimMap.ContainsKey(claimType);
        public Boolean NotHasClaimType(String claimType)
            => !HasClaimType(claimType);

        public Boolean AnyClaims(String claimType, IEnumerable<String> expectedValues)
        {
            if (expectedValues == null)
                throw new ArgumentNullException(nameof(expectedValues));

            if (!expectedValues.Any())
                throw new ArgumentException($"The '{nameof(expectedValues)}' cannot be empty.", nameof(expectedValues));

            var claimValues = GetClaimValues(claimType);
            if (!claimValues.Any())
                return false;

            foreach (var expectedValue in expectedValues)
                if (claimValues.Contains(expectedValue))
                    return true;
            return false;
        }
        public Boolean NotAnyClaims(String claimType, IEnumerable<String> expectedValues)
            => !AnyClaims(claimType, expectedValues);

        public Boolean AnyClaims(String claimType, params String[] expectedValues)
            => AnyClaims(claimType, expectedValues ?? Enumerable.Empty<String>());
        public Boolean NotAnyClaims(String claimType, params String[] expectedValues)
            => NotAnyClaims(claimType, expectedValues ?? Enumerable.Empty<String>());

    }
}