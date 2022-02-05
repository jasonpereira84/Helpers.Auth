using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JasonPereira84.Helpers.Auth.Tests
{
    internal sealed class Principal : _Principal { public Principal(IEnumerable<Claim> claims) : base(claims) { } }

    [TestClass]
    public class Test_Principal
    {
        [TestMethod]
        public void ctor()
        {
            {
                var claims = new[] {
                    new Claim("t1","v1"),
                    new Claim("t2","v2"),
                    new Claim("t3","v3_1"),
                    new Claim("t3","v3_2"),
                };

                var principal = new Principal(claims);
                Assert.IsNotNull(principal._claimMap);
                Assert.IsTrue(principal._claimMap.Keys.Count() == 3);
                Assert.IsTrue(principal._claimMap.Values.Count() == 3);
                Assert.IsTrue(principal._claimMap["t3"].Count() == 2);
            }

            {
                var claims = default(IEnumerable<Claim>);

                Assert.ThrowsException<ArgumentNullException>(
                    () => new Principal(claims));
            }

            {
                {
                    var claims = new[] {
                        default(Claim)
                    };

                    var principal = new Principal(claims);
                    Assert.IsNotNull(principal._claimMap);
                    Assert.IsFalse(principal._claimMap.Keys.Any());
                }

                {
                    var claims = new[] {
                        new Claim("","v1")
                    };

                    var principal = new Principal(claims);
                    Assert.IsFalse(principal._claimMap.Keys.Any());
                }

                {
                    var claims = new[] {
                        new Claim("t1",""),
                    };

                    var principal = new Principal(claims);
                    Assert.IsFalse(principal._claimMap.Keys.Any());
                }

            }

            {
                {
                    var claims = new[] {
                        new Claim("t1 ","v1")
                    };

                    var principal = new Principal(claims);
                    Assert.IsFalse(principal._claimMap.ContainsKey("t1 "));
                    Assert.IsTrue(principal._claimMap.ContainsKey("t1"));
                }

                {
                    var claims = new[] {
                        new Claim("t1","v1 ")
                    };

                    var principal = new Principal(claims);
                    Assert.IsTrue(principal._claimMap["t1"].First() == "v1");
                }

            }

            {
                var claims = new[] {
                    new Claim("t1","v1"),
                    new Claim("t1","v1")
                };

                var principal = new Principal(claims);
                Assert.IsTrue(principal._claimMap["t1"].Count() == 1);
            }

        }

        [TestMethod]
        public void GetClaimValues()
        {
            var principal = new Principal(new[] {
                    new Claim("t1","v1"),
                    new Claim("t2","v2"),
                    new Claim("t3","v3_1"),
                    new Claim("t3","v3_2"),
                });

            Assert.IsTrue(
                new[] { "v3_1", "v3_2" }
                    .SequenceEqual(principal.GetClaimValues("t3")));

            Assert.IsNotNull(principal.GetClaimValues("t4"));

            Assert.IsFalse(principal.GetClaimValues("t4").Any());
        }

        [TestMethod]
        public void GetClaimValueInt64()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","1")
                });

                var claimValue = principal.GetClaimValueInt64("t1");
                Assert.AreEqual(
                    expected: 1L,
                    actual: claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1")
                });

                var claimValue = principal.GetClaimValueInt64("t1");
                Assert.IsNull(claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","1"),
                    new Claim("t1","2")
                });

                var claimValue = principal.GetClaimValueInt64("t1");
                Assert.AreEqual(
                    expected: 1L,
                    actual: claimValue);
            }

        }

        [TestMethod]
        public void GetClaimValueInt32()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","1")
                });

                var claimValue = principal.GetClaimValueInt32("t1");
                Assert.AreEqual(
                    expected: 1,
                    actual: claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1")
                });

                var claimValue = principal.GetClaimValueInt32("t1");
                Assert.IsNull(claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","1"),
                    new Claim("t1","2")
                });

                var claimValue = principal.GetClaimValueInt32("t1");
                Assert.AreEqual(
                    expected: 1,
                    actual: claimValue);
            }

        }

        [TestMethod]
        public void GetClaimValueInt16()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","1")
                });

                var claimValue = principal.GetClaimValueInt16("t1");
                Assert.AreEqual(
                    expected: (Int16)1,
                    actual: claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1")
                });

                var claimValue = principal.GetClaimValueInt16("t1");
                Assert.IsNull(claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","1"),
                    new Claim("t1","2")
                });

                var claimValue = principal.GetClaimValueInt16("t1");
                Assert.AreEqual(
                    expected: (Int16)1,
                    actual: claimValue);
            }

        }

        [TestMethod]
        public void GetClaimValueDateTime()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","0001-01-01T01:01:01")
                });

                var claimValue = principal.GetClaimValueDateTime("t1");
                Assert.AreEqual(
                    expected: new DateTime(1, 1, 1, 1, 1, 1),
                    actual: claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1")
                });

                var claimValue = principal.GetClaimValueDateTime("t1");
                Assert.IsNull(claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","0001-01-01T01:01:01"),
                    new Claim("t1","0002-01-01T01:01:01")
                });

                var claimValue = principal.GetClaimValueDateTime("t1");
                Assert.AreEqual(
                    expected: new DateTime(1, 1, 1, 1, 1, 1),
                    actual: claimValue);
            }

        }

        [TestMethod]
        public void GetClaimValueDateTimeOffset()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","0001-01-01T01:01:01 +1:00")
                });

                var claimValue = principal.GetClaimValueDateTimeOffset("t1");
                Assert.AreEqual(
                    expected: new DateTimeOffset(1, 1, 1, 1, 1, 1, TimeSpan.FromHours(1)),
                    actual: claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1")
                });

                var claimValue = principal.GetClaimValueDateTimeOffset("t1");
                Assert.IsNull(claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","0001-01-01T01:01:01 +1:00"),
                    new Claim("t1","0002-01-01T01:01:01 +1:00")
                });

                var claimValue = principal.GetClaimValueDateTimeOffset("t1");
                Assert.AreEqual(
                    expected: new DateTimeOffset(1, 1, 1, 1, 1, 1, TimeSpan.FromHours(1)),
                    actual: claimValue);
            }

        }

        [TestMethod]
        public void GetClaimValueUnixTimeSeconds()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","1")
                });

                var claimValue = principal.GetClaimValueUnixTimeSeconds("t1");
                Assert.AreEqual(
                    expected: DateTimeOffset.FromUnixTimeSeconds(1),
                    actual: claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1")
                });

                var claimValue = principal.GetClaimValueUnixTimeSeconds("t1");
                Assert.IsNull(claimValue);
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","1"),
                    new Claim("t1","2")
                });

                var claimValue = principal.GetClaimValueUnixTimeSeconds("t1");
                Assert.AreEqual(
                    expected: DateTimeOffset.FromUnixTimeSeconds(1),
                    actual: claimValue);
            }

        }

        [TestMethod]
        public void HasClaims()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1"),
                    new Claim("t1","v1_2"),
                });

                Assert.IsTrue(principal.HasClaims("t1", "v1_1", "v1_2"));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1"),
                    new Claim("t1","v1_2"),
                });

                Assert.ThrowsException<ArgumentNullException>(
                    () => principal.HasClaims("t1", default(IEnumerable<String>)));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1"),
                    new Claim("t1","v1_2"),
                });

                Assert.ThrowsException<ArgumentException>(
                    () => principal.HasClaims("t1", new String[0]));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1"),
                    new Claim("t1","v1_2"),
                });

                Assert.IsFalse(principal.HasClaims("t2", "v1_1", "v1_2"));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1")
                });

                Assert.IsFalse(principal.HasClaims("t1", "v1_1", "v1_2"));
            }

        }

        [TestMethod]
        public void AnyClaims()
        {
            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1")
                });

                Assert.IsTrue(principal.AnyClaims("t1", "v1_1", "v1_2"));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1"),
                    new Claim("t1","v1_2"),
                });

                Assert.ThrowsException<ArgumentNullException>(
                    () => principal.AnyClaims("t1", default(IEnumerable<String>)));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1"),
                    new Claim("t1","v1_2"),
                });

                Assert.ThrowsException<ArgumentException>(
                    () => principal.AnyClaims("t1", new String[0]));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1")
                });

                Assert.IsFalse(principal.AnyClaims("t2", "v1_1", "v1_2"));
            }

            {
                var principal = new Principal(new[] {
                    new Claim("t1","v1_1")
                });

                Assert.IsTrue(principal.AnyClaims("t1", "v1_1", "v1_2"));
            }

        }

    }
}
