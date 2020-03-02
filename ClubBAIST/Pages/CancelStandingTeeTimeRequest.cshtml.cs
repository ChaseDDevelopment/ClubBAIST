using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist
{
    public class CancelStandingTeeTimeRequestModel : PageModel
    {
        [TempData]
        public bool Confirmation { get; set; }
        [TempData]
        public string Alert { get; set; }

        
        public void OnGet()
        {
            StandingTeeTime standingTeeTimeRequest = new StandingTeeTime();
            CBS RequestDirector = new CBS();

            var MemberNum = User.Claims.FirstOrDefault(c => c.Type == "MemberNumber").Value;
            Int32.TryParse(MemberNum, out int MemberNumber);

            

            standingTeeTimeRequest = RequestDirector.FindStandingTeeTimeRequest(MemberNumber);






        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}