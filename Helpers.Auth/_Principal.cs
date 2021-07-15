using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace JasonPereira84.Helpers
{
    public abstract class _Principal
    {
        protected ImmutableDictionary<String, IEnumerable<String>> claimMap;

        protected _Principal(IEnumerable<Claim> claims)
        {
            claimMap = ImmutableDictionary.CreateRange(
                claims
                    .Where(c =>
                        c.IsNotNull() &&
                        c.Type.IsNotNullOrEmptyOrWhiteSpace() &&
                        c.Value.IsNotNullOrEmptyOrWhiteSpace())
                    .Select(c => new
                    {
                        Type = c.Type.Trim(),
                        Value = c.Value.Trim()
                    })
                    .GroupBy(a => a.Type)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(a => a.Value)));
        }

        public IEnumerable<String> GetClaimValues(String claimType)
            => claimMap.ContainsKey(claimType) ? claimMap[claimType] : Enumerable.Empty<string>();

        public String GetClaimValue(String claimType)
            => GetClaimValues(claimType).FirstOrDefault(default);

        public Int64? GetClaimValueInt64(String claimType)
            => GetClaimValue(claimType).ParseOrDefault((Int64?)null);

        public Int32? GetClaimValueInt32(String claimType)
            => GetClaimValue(claimType).ParseOrDefault((Int32?)null);

        public DateTime? GetClaimValueDate(String claimType)
            => GetClaimValue(claimType).ParseOrDefault((DateTime?)null);

        public Boolean HasClaims(String claimType, IEnumerable<String> expectedValues)
            => GetClaimValues(claimType).ContainsAll(expectedValues);

        public Boolean HasClaim(String claimType, String expectedValue)
            => HasClaims(claimType, new[] { expectedValue });

        public Boolean HasClaimType(String claimType)
            => claimMap.ContainsKey(claimType);

        public Boolean AnyClaims(String claimType, IEnumerable<String> expectedValues)
            => GetClaimValues(claimType).ContainsAny(expectedValues);
    }
}