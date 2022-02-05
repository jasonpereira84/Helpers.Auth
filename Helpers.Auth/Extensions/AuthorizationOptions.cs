using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Authorization;

        public static partial class Auth
        {
            public static void AddPolicy(this AuthorizationOptions authorizationOptions, Policy policy)
                => authorizationOptions.AddPolicy(policy.Name, policy.ConfigurePolicy);
        }
    }
}