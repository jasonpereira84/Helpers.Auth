using Microsoft.AspNetCore.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JasonPereira84.Helpers.Auth.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        [TestClass]
        public class Test_AuthenticationProperties
        {
            [TestMethod]
            public void ExpireIn()
            {
                {
                    var authenticationProperties = new AuthenticationProperties
                    {
                        IssuedUtc = default(DateTimeOffset)
                    };

                    Assert.AreSame(
                        expected: authenticationProperties,
                        actual: authenticationProperties.ExpireIn(default(TimeSpan)));
                }

                {
                    var authenticationProperties = new AuthenticationProperties();

                    Assert.ThrowsException<ArgumentNullException>(
                        () => authenticationProperties.ExpireIn(default(TimeSpan)));
                }

                {
                    var issuedUtc = default(DateTimeOffset);
                    var timeSpan = TimeSpan.FromHours(1);
                    var authenticationProperties = new AuthenticationProperties
                    {
                        IssuedUtc = issuedUtc
                    };

                    authenticationProperties.ExpireIn(timeSpan);
                    Assert.IsNotNull(authenticationProperties.ExpiresUtc);
                    Assert.AreEqual(
                        expected: issuedUtc.Add(timeSpan),
                        actual: authenticationProperties.ExpiresUtc.Value);
                }

            }

        }
    }
}
