﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Auth
        {
            public static (IEnumerable<Claim> Roles, IEnumerable<Claim> Others) GetClaims(this ClaimsIdentity claimsIdentity)
            {
                Boolean _isRole(Claim claim)
                    => claim.Type.Equals(claimsIdentity.RoleClaimType, StringComparison.OrdinalIgnoreCase);

                var roles = new List<Claim>();
                var others = new List<Claim>();
                foreach (var claim in claimsIdentity.Claims)
                    if (_isRole(claim))
                        roles.Add(claim);
                    else
                        others.Add(claim);

                return (Roles: roles, Others: others);
            }

        }
    }
}
