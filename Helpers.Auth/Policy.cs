using System;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Authorization;

    public class Policy
    {
        public String Name { get; set; }

        public Action<AuthorizationPolicyBuilder> ConfigurePolicy { get; set; }
    }
}