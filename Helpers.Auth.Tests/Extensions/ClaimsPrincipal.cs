using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Claims;

namespace JasonPereira84.Helpers.Auth.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        [TestClass]
        public class Test_ClaimsPrincipal
        {
            [TestMethod]
            public void AppendIdentities()
            {
                var claimsPrincipal = new ClaimsPrincipal();

                Assert.AreSame(
                    expected: claimsPrincipal,
                    actual: claimsPrincipal.AppendIdentities(new ClaimsIdentity[0]));
            }

        }
    }
}
