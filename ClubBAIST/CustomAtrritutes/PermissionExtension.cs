using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClubBaist.CustomAtrritutes
{
    public static class PermissionExtension
    {
        public static bool HavePermission(this Controller c, string claimValue)
        {
            var user = c.HttpContext.User as ClaimsPrincipal;
            var havePer = user.HasClaim(claimValue, claimValue);
            return havePer;
        }

        public static bool HavePermission(this IIdentity claims, string claimValue)
        {
            var userClaims = claims as ClaimsIdentity;
            var havePer = userClaims.HasClaim(claimValue, claimValue);
            return havePer;
        }
    }
}
 