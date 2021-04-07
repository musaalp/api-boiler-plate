using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Sdk.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sdk.Api.Authorization.Permission
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var endpoint = context.Resource as Endpoint;

            var permissionAttributes = endpoint?.Metadata.OfType<PermissionAttribute>();

            return HandleRequirementAsync(context, requirement, permissionAttributes);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement,
            IEnumerable<PermissionAttribute> attributes);
    }

    public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement,
        PermissionAttribute>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement, IEnumerable<PermissionAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                bool isAuthorized = Authorize(context.User, attribute.Names);

                if (isAuthorized) context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private static bool Authorize(ClaimsPrincipal user, List<string> permissions)
        {
            var claims = user.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            if (claims.Count < 1) return false;

            // Admin kontrolü.
            if (claims.Contains(PermissionName.Admin)) return true;

            // Beklenen izin ve kullanıcı claimi direkt eşleşiyorsa.
            if (permissions.Any(p => claims.Any(c => c == p))) return true;

            return false;
        }
    }
}
