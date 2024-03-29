﻿using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Authentication;

        public static partial class Auth
        {
            public static AuthenticationProperties ExpireIn(this AuthenticationProperties authenticationProperties, TimeSpan timeSpan)
            {
                if (!authenticationProperties.IssuedUtc.HasValue)
                    throw new ArgumentNullException($"{nameof(authenticationProperties)}.{nameof(authenticationProperties.IssuedUtc)}");

                authenticationProperties.ExpiresUtc = authenticationProperties.IssuedUtc.Value.Add(timeSpan);
                return authenticationProperties;
            }

            public static AuthenticationProperties ExpireIn(this AuthenticationProperties authenticationProperties, Int32 days = 0, Int32 hours = 0, Int32 minutes = 0, Int32 seconds = 0, Int32 milliseconds = 0)
                => ExpireIn(authenticationProperties, new TimeSpan(days, hours, minutes, seconds, milliseconds));
        }
    }
}
