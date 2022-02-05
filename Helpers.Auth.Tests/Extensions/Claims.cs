using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JasonPereira84.Helpers.Auth.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        [TestClass]
        public class Test_Claims
        {
            [TestMethod]
            public void GetClaimsOfType()
            {
                var claims = new[] {
                    new Claim("t1","v1"),
                    new Claim("t2","v2"),
                    new Claim("t3","v3_1"),
                    new Claim("t3","v3_2"),
                };

                Assert.IsTrue(
                    claims.GetClaimsOfType("t3").ValuesAre(new[] { "v3_1", "v3_2" }));

                Assert.IsNotNull(claims.GetClaimsOfType("t4"));

                Assert.IsFalse(claims.GetClaimsOfType("t4").Any());
            }

            [TestMethod]
            public void HasClaimsOfType()
            {
                var claims = new[] {
                    new Claim("t1","v1"),
                    new Claim("t2","v2"),
                    new Claim("t3","v3_1"),
                    new Claim("t3","v3_2"),
                };

                Assert.IsTrue(claims.HasClaimsOfType("t3", out IEnumerable<Claim> claimsOfType));
                Assert.IsTrue(claimsOfType.ValuesAre(new[] { "v3_1", "v3_2" }));
            }

            [TestMethod]
            public void HasAllClaimsOfType()
            {
                {
                    var claims = new[] {
                        new Claim("t1","v1"),
                        new Claim("t2","v2")
                    };

                    Assert.IsTrue(claims.HasAllClaimsOfType("t1", "t2"));
                }

                {
                    var claims = new Claim[0];

                    Assert.ThrowsException<ArgumentNullException>(
                        () => claims.HasAllClaimsOfType(default(IEnumerable<String>)));
                }

                {
                    var claims = new Claim[0];

                    Assert.ThrowsException<ArgumentException>(
                        () => claims.HasAllClaimsOfType(new String[0]));
                }

                {
                    var claims = new Claim[0];

                    Assert.IsFalse(claims.HasAllClaimsOfType("t1"));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1"),
                        new Claim("t2","v2"),
                    };

                    Assert.IsFalse(claims.HasAllClaimsOfType("t1", "t3"));
                }

            }

            [TestMethod]
            public void GetClaimValueInt64()
            {
                {
                    var claims = new[] {
                        new Claim("t1","1")
                    };

                    var claimValue = claims.GetClaimValueInt64("t1");
                    Assert.AreEqual(
                        expected: 1L,
                        actual: claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1")
                    };

                    var claimValue = claims.GetClaimValueInt64("t1");
                    Assert.IsNull(claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","1"),
                        new Claim("t1","2")
                    };

                    var claimValue = claims.GetClaimValueInt64("t1");
                    Assert.AreEqual(
                        expected: 1L,
                        actual: claimValue);
                }

            }

            [TestMethod]
            public void GetClaimValueInt32()
            {
                {
                    var claims = new[] {
                        new Claim("t1","1")
                    };

                    var claimValue = claims.GetClaimValueInt32("t1");
                    Assert.AreEqual(
                        expected: 1,
                        actual: claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1")
                    };

                    var claimValue = claims.GetClaimValueInt32("t1");
                    Assert.IsNull(claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","1"),
                        new Claim("t1","2")
                    };

                    var claimValue = claims.GetClaimValueInt32("t1");
                    Assert.AreEqual(
                        expected: 1,
                        actual: claimValue);
                }

            }

            [TestMethod]
            public void GetClaimValueInt16()
            {
                {
                    var claims = new[] {
                        new Claim("t1","1")
                    };

                    var claimValue = claims.GetClaimValueInt16("t1");
                    Assert.AreEqual(
                        expected: (Int16)1,
                        actual: claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1")
                    };

                    var claimValue = claims.GetClaimValueInt16("t1");
                    Assert.IsNull(claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","1"),
                        new Claim("t1","2")
                    };

                    var claimValue = claims.GetClaimValueInt16("t1");
                    Assert.AreEqual(
                        expected: (Int16)1,
                        actual: claimValue);
                }

            }

            [TestMethod]
            public void GetClaimValueDateTime()
            {
                {
                    var claims = new[] {
                        new Claim("t1","0001-01-01T01:01:01")
                    };

                    var claimValue = claims.GetClaimValueDateTime("t1");
                    Assert.AreEqual(
                        expected: new DateTime(1, 1, 1, 1, 1, 1),
                        actual: claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1")
                    };

                    var claimValue = claims.GetClaimValueDateTime("t1");
                    Assert.IsNull(claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","0001-01-01T01:01:01"),
                        new Claim("t1","0002-01-01T01:01:01")
                    };

                    var claimValue = claims.GetClaimValueDateTime("t1");
                    Assert.AreEqual(
                        expected: new DateTime(1, 1, 1, 1, 1, 1),
                        actual: claimValue);
                }

            }

            [TestMethod]
            public void GetClaimValueDateTimeOffset()
            {
                {
                    var claims = new[] {
                        new Claim("t1","0001-01-01T01:01:01 +1:00")
                    };

                    var claimValue = claims.GetClaimValueDateTimeOffset("t1");
                    Assert.AreEqual(
                        expected: new DateTimeOffset(1, 1, 1, 1, 1, 1, TimeSpan.FromHours(1)),
                        actual: claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1")
                    };

                    var claimValue = claims.GetClaimValueDateTimeOffset("t1");
                    Assert.IsNull(claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","0001-01-01T01:01:01 +1:00"),
                        new Claim("t1","0002-01-01T01:01:01 +1:00")
                    };

                    var claimValue = claims.GetClaimValueDateTimeOffset("t1");
                    Assert.AreEqual(
                        expected: new DateTimeOffset(1, 1, 1, 1, 1, 1, TimeSpan.FromHours(1)),
                        actual: claimValue);
                }

            }

            [TestMethod]
            public void GetClaimValueUnixTimeSeconds()
            {
                {
                    var claims = new[] {
                        new Claim("t1","1")
                    };

                    var claimValue = claims.GetClaimValueUnixTimeSeconds("t1");
                    Assert.AreEqual(
                        expected: DateTimeOffset.FromUnixTimeSeconds(1),
                        actual: claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1")
                    };

                    var claimValue = claims.GetClaimValueUnixTimeSeconds("t1");
                    Assert.IsNull(claimValue);
                }

                {
                    var claims = new[] {
                        new Claim("t1","1"),
                        new Claim("t1","2")
                    };

                    var claimValue = claims.GetClaimValueUnixTimeSeconds("t1");
                    Assert.AreEqual(
                        expected: DateTimeOffset.FromUnixTimeSeconds(1),
                        actual: claimValue);
                }

            }

            [TestMethod]
            public void HasClaims()
            {
                {
                    var claims = new[] {
                        new Claim("t1","v1_1"),
                        new Claim("t1","v1_2"),
                    };

                    Assert.IsTrue(claims.HasClaims("t1", "v1_1", "v1_2"));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1"),
                        new Claim("t1","v1_2"),
                    };

                    Assert.ThrowsException<ArgumentNullException>(
                        () => claims.HasClaims("t1", default(IEnumerable<String>)));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1"),
                        new Claim("t1","v1_2"),
                    };

                    Assert.ThrowsException<ArgumentException>(
                        () => claims.HasClaims("t1", new String[0]));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1"),
                        new Claim("t1","v1_2"),
                    };

                    Assert.IsFalse(claims.HasClaims("t2", "v1_1", "v1_2"));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1")
                    };

                    Assert.IsFalse(claims.HasClaims("t1", "v1_1", "v1_2"));
                }

            }

            [TestMethod]
            public void AnyClaims()
            {
                {
                    var claims = new[] {
                        new Claim("t1","v1_1")
                    };

                    Assert.IsTrue(claims.AnyClaims("t1", "v1_1", "v1_2"));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1"),
                        new Claim("t1","v1_2"),
                    };

                    Assert.ThrowsException<ArgumentNullException>(
                        () => claims.AnyClaims("t1", default(IEnumerable<String>)));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1"),
                        new Claim("t1","v1_2"),
                    };

                    Assert.ThrowsException<ArgumentException>(
                        () => claims.AnyClaims("t1", new String[0]));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1")
                    };

                    Assert.IsFalse(claims.AnyClaims("t2", "v1_1", "v1_2"));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1_1")
                    };

                    Assert.IsTrue(claims.AnyClaims("t1", "v1_1", "v1_2"));
                }

            }

        }
    }
}
