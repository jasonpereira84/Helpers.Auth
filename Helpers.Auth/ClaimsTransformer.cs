using System;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Authentication;

    public abstract class _ClaimsTransformer : IClaimsTransformation
    {
        protected readonly Func<ClaimsPrincipal, Task<ClaimsPrincipal>> _transformer;

        protected _ClaimsTransformer(Func<ClaimsPrincipal, Task<ClaimsPrincipal>> transformer)
            => _transformer = transformer;

        protected _ClaimsTransformer(Func<ClaimsPrincipal, ClaimsPrincipal> claimsPrincipalTransformer)
            : this(claimsPrincipal => Task.FromResult(claimsPrincipalTransformer.Invoke(claimsPrincipal))) { }

        protected _ClaimsTransformer(Func<ClaimsIdentity, ClaimsPrincipal> claimsIdentityTransformer)
            : this(claimsPrincipal => claimsIdentityTransformer.Invoke(claimsPrincipal.Identity as ClaimsIdentity)) { }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
            => _transformer.Invoke(principal);
    }

    public sealed class ClaimsTransformer : _ClaimsTransformer, IClaimsTransformation
    {
        public ClaimsTransformer(Func<ClaimsIdentity, ClaimsPrincipal> transformer)
            : base(transformer) { }

        public ClaimsTransformer(Func<ClaimsIdentity, ClaimsIdentity> ctor)
            : this(claimsIdentity => new ClaimsPrincipal(ctor.Invoke(claimsIdentity))) { }

        public static ClaimsTransformer From(Func<ClaimsIdentity, ClaimsPrincipal> transformer)
            => new ClaimsTransformer(transformer);

        public static ClaimsTransformer From(Func<ClaimsIdentity, ClaimsIdentity> ctor)
            => new ClaimsTransformer(ctor);

        public static ClaimsTransformer From(Func<(IEnumerable<Claim> Claims, String AuthenticationType, String NameClaimType, String RoleClaimType), ClaimsIdentity> ctor)
            => new ClaimsTransformer(claimsIdentity => ctor.Invoke((claimsIdentity.Claims, claimsIdentity.AuthenticationType, claimsIdentity.NameClaimType, claimsIdentity.RoleClaimType)));

    }
}