using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginDemo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using LoginDemo.CustomAttributes;

namespace LoginDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            User objLoggedInUser = new User();
            if (User.Identity.IsAuthenticated)
            {
                var claimsIndentity = HttpContext.User.Identity as ClaimsIdentity;
                var userClaims = claimsIndentity.Claims;

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    foreach (var claim in userClaims)
                    {
                        var cType = claim.Type;
                        var cValue = claim.Value;
                        switch (cType)
                        {
                            case "USERID":
                                objLoggedInUser.USERID = cValue;
                                break;
                            case "EMAILID":
                                objLoggedInUser.EMAILID = cValue;
                                break;
                            case "PHONE":
                                objLoggedInUser.PHONE = cValue;
                                break;
                            case "DIRECTOR":
                                objLoggedInUser.ACCESS_LEVEL = cValue;
                                break;
                            case "SUPERVISOR":
                                objLoggedInUser.ACCESS_LEVEL = cValue;
                                break;
                            case "ANALYST":
                                objLoggedInUser.ACCESS_LEVEL = cValue;
                                break;
                        }
                    }
                    ViewBag.UserRole = GetRole();
                }
            }
            return View("Index", objLoggedInUser);
        }

        public IActionResult LoginUser(User user)
        {
            TokenProvider _tokenProvider = new TokenProvider();
            var userToken = _tokenProvider.LoginUser(user.USERID.Trim(), user.PASSWORD.Trim());
            if (userToken != null)
            {
                //Save token in session object
                HttpContext.Session.SetString("JWToken", userToken);
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
        }

        public JsonResult EndSession()
        {
            HttpContext.Session.Clear();
            return Json(new {result = "success"});
        }
        private string GetRole()
        {
            if (this.HavePermission(Roles.DIRECTOR))
                return " - DIRECTOR";
            if (this.HavePermission(Roles.SUPERVISOR))
                return " - SUPERVISOR";
            if (this.HavePermission(Roles.ANALYST))
                return " - ANALYST";
            return null;
        }
    }
}
