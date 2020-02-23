using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist.Pages
{
    public class CreateStandingTeeTimeRequestModel : PageModel
    {
        [Required]
        [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage="must be a positive integer")]
        [BindProperty] public string MemberNumber1 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage = "must be a positive integer")]
        [BindProperty] public string MemberNumber2 { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage = "must be a positive integer")]
        [BindProperty] public string MemberNumber3 { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage = "must be a positive integer")]
        [BindProperty] public string MemberNumber4 { get; set; }
        [Required]
        [MaxLength(25)]
        [BindProperty] public string MemberName1 { get; set; }
        [Required]
        [MaxLength(25)]
        [BindProperty] public string MemberName2 { get; set; }
        [Required]
        [MaxLength(25)]
        [BindProperty] public string MemberName3 { get; set; }
        [Required]
        [MaxLength(25)]
        [BindProperty] public string MemberName4 { get; set; }

        [Required]
        [BindProperty]
        [DisplayName("Desired Time")]
        public string Time { get; set; }
        [BindProperty] public List<string> AvailableTimes { get; set; }
           
        [Required]
        [RegularExpression(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", ErrorMessage =
            "Must be in YYYY-MM-DD format.")]
        [StringLength(10)]
        
        [BindProperty] public string StartDate { get; set; }
              
        [Required]
        [RegularExpression(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", ErrorMessage =
            "Must be in YYYY-MM-DD format.")]
        [StringLength(10)]
        [Compare(nameof(StartDate))]
        [BindProperty] public string EndDate { get; set; }



        [TempData]
        public bool Confirmation { get; set; }
        [TempData]
        public string Alert { get; set; }

        public void OnGet()
        {
            List<string> availableTimes = new List<string>();
            var generatedTime = TimeSpan.FromHours(7);
            for (var i = 0; i < 97; i++)
            {
                DateTime timeToString = new DateTime(generatedTime.Ticks);
                availableTimes.Add(timeToString.ToString("t"));
                if (i % 2 == 0)
                    generatedTime += TimeSpan.FromMinutes(7);
                else
                    generatedTime += TimeSpan.FromMinutes(8);
            }

            AvailableTimes = availableTimes;
        }

        public IActionResult OnPost()
        {
            Confirmation = false;
            StandingTeeTime request = new StandingTeeTime();

            if (ModelState.IsValid)
            {
                CBS RequestDirector = new CBS();

                DateTime.TryParse(StartDate, out DateTime startDate);
                DateTime.TryParse(EndDate, out DateTime endDate);
                TimeSpan time = Convert.ToDateTime(Time).TimeOfDay;

                int memberNumber1;
                int memberNumber2;
                int memberNumber3;
                int memberNumber4;
                int.TryParse(MemberNumber1, out memberNumber1);
                int.TryParse(MemberNumber2, out memberNumber2);
                int.TryParse(MemberNumber3, out memberNumber3);
                int.TryParse(MemberNumber4, out memberNumber4);

                request.MemberNumber1 = memberNumber1;
                request.MemberNumber2 = memberNumber2;
                request.MemberNumber3 = memberNumber3;
                request.MemberNumber4 = memberNumber4;
                request.MemberName1 = MemberName1;
                request.MemberName2 = MemberName2;
                request.MemberName3 = MemberName3;
                request.MemberName4 = MemberName4;
                request.DayOfWeek = startDate;
                request.Time = time;
                request.StartDate = startDate;
                request.EndDate = endDate;


                Confirmation = RequestDirector.CreateStandingTeeTimeRequest(request);

                if (Confirmation)
                {
                    TempData["Alert"] = $"Successfully Created Standing Tee Time Request";
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["Danger"] = true;
                    Alert = $"Could Not Create Standing Tee Time Request";
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