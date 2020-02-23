using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginDemo.CustomAttributes;
using Microsoft.AspNetCore.Mvc;

namespace LoginDemo.Controllers
{
    [UnAuthorized]
    public class DashboardController : Controller
    {
        [Authorize(Roles.DIRECTOR)]
        public IActionResult DirectorPage()
        {
            ViewBag.UserRole = GetRole();
            ViewBag.Message = "Permission controlled through Authorize Attribute";
            return View("DirectorPage");
        }

        [Authorize(Roles.SUPERVISOR)]
        public IActionResult SupervisorPage()
        {
            ViewBag.UserRole = GetRole();
            ViewBag.Message = "Permission controlled through Authorize Attribute";
            return View("SupervisorPage");
        }

        [Authorize(Roles.ANALYST)]
        public IActionResult AnalystPage()
        {
            ViewBag.UserRole = GetRole();
            ViewBag.Message = "Permission controlled through Authorize Attribute";
            return View("AnalystPage");
        }

        public IActionResult SupervisorAnalystPage()
        {
            ViewBag.UserRole = GetRole();
            ViewBag.Message = "Permission controlled inside action method in-line code";
            if (this.HavePermission(Roles.SUPERVISOR))
                return View("SupervisorPage");
            
            if (this.HavePermission(Roles.ANALYST))
                return View("AnalystPage");
            
            return new RedirectResult("~/Dashboard/NoPermission");
        }

        [Authorize(Roles.DIRECTOR, Roles.SUPERVISOR, Roles.ANALYST )]
        public IActionResult PageLevelPermission()
        {
            ViewBag.UserRole = GetRole();
            return View("PageLevelPermission");
        }

        public IActionResult CheckAjaxCalls()
        {
            ViewBag.UserRole = GetRole();
            return View("CheckAjaxCalls");
        }

        public JsonResult AuthenticateAjaxCalls()
        {
            return Json(new {result = "success" });
        }

        [Authorize(Roles.DIRECTOR, Roles.SUPERVISOR)]
        public JsonResult AuthorizeAjaxCalls()
        {
            return Json(new { result = "success" });
        }

        public IActionResult NoPermission()
        {
            ViewBag.UserRole = GetRole();
            return View("NoPermission");
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