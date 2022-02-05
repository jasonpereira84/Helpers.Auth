using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace JasonPereira84.Helpers.Auth.Tests
{
    internal static class Helpers 
    {
        public static Boolean ValuesAre(this IEnumerable<Claim> claims, IEnumerable<String> expected)
            => claims.Select(c => c.Value).SequenceEqual(expected);

    }
}
