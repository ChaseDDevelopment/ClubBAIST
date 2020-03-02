using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist
{
    [UnAuthorized]
    public class CancelStandingTeeTimeRequestModel : PageModel
    {
        [TempData]
        public bool Confirmation { get; set; }
        [TempData]
        public string Alert { get; set; }

        [BindProperty]
        public StandingTeeTime StandingTeeTimeRequest { get; set; }

        [BindProperty]
        public string StartDate { get; set; }

        [BindProperty]
        public string EndDate { get; set; }
        [BindProperty]
        public string time { get; set; }



        public void OnGet()
        {
            
            CBS RequestDirector = new CBS();

            var MemberNum = User.Claims.FirstOrDefault(c => c.Type == "MemberNumber").Value;
            Int32.TryParse(MemberNum, out int MemberNumber);

            

            StandingTeeTimeRequest = RequestDirector.FindStandingTeeTimeRequest(MemberNumber);
            if (StandingTeeTimeRequest.MemberNumber1 != MemberNumber) 
            {
                TempData["Danger"] = true;
                Alert = $"You don't have a Standing Tee Time Request";
                StandingTeeTimeRequest = null;

            }
            else
            {

                StartDate = StandingTeeTimeRequest.StartDate.ToString("yyyy-MM-dd");
                EndDate = StandingTeeTimeRequest.EndDate.ToString("yyyy-MM-dd");
                time = StandingTeeTimeRequest.Time.ToString("t");
                TempData.Put("key",StandingTeeTimeRequest);

            }

        }

        public IActionResult OnPostCancel()
        {
            if (ModelState.IsValid)
            {
                StandingTeeTimeRequest = TempData.Get<StandingTeeTime>("key");
                Confirmation = false;
                CBS RequestDirector = new CBS();
                Confirmation = RequestDirector.CancelStandingTeeTimeRequest(StandingTeeTimeRequest.StandingTeeTimeID);
                if (Confirmation)
                {
                    TempData["Alert"] = $"Successfully Cancelled Standing Tee Time Request";
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["Danger"] = true;
                    TempData["Alert"] = $"Could Not Cancel Standing Tee Time Request";
                    RedirectToPage("/CancelStandingTeeTimeRequest");
                }
            }


            return Page();
        }


        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}