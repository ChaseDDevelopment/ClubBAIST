﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClubBaist.CustomAtrritutes
{
    public class UnAuthorizedAttribute : TypeFilterAttribute
    {
        public UnAuthorizedAttribute() : base(typeof(UnAuthorizedFilter))
        {
            //Empty
        }

        public class UnAuthorizedFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                bool IsAutenticated = context.HttpContext.User.Identity.IsAuthenticated;
                if (!IsAutenticated)
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
        }
    }
}
