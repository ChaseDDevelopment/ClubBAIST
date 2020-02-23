using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist.Pages
{
    //Require that the user be logged in at all times. 
    [UnAuthorized]
    public class CreateTeeTimeModel : PageModel
    {
        [BindProperty]
        [Required]
        [RegularExpression(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", ErrorMessage =
            "Must be in YYYY-MM-DD format.")]
        [StringLength(10)]
        public string date { get; set; }
        [TempData] public bool Confirmation { get; set; }
        [TempData] public string Alert { get; set; }
        [TempData] public DateTime selectedDateTemp { get; set; }
        [BindProperty] public DailyTeeSheet dailyTeeSheet { get; set; }
        [BindProperty] public List<string> availableTeeTimes { get; set; }
        [BindProperty] public string Golfer1 { get; set; }
        [BindProperty] public string Golfer2 { get; set; }

        [BindProperty] public string Golfer3 { get; set; }
        [BindProperty] public string Golfer4 { get; set; }
        [BindProperty]
        [DisplayName("Desired Time")]
        public string selectedTime { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostFind()
        {

            if (ModelState.IsValid)
            {
                CBS RequestDirector = new CBS();

                DateTime.TryParse(date, out DateTime selectedDate);

                if (selectedDate > DateTime.Today + TimeSpan.FromDays(7) || selectedDate < DateTime.Today)
                {
                    TempData["Danger"] = true;
                    if (selectedDate < DateTime.Today)
                    {
                        Alert = $"Cannot create a tee time for a passed date.";
                    }
                    else
                    {
                        Alert = $"Cannot create a tee time more than 7 days in advance.";
                    }
                    
                    dailyTeeSheet = null;
                    return Page();
                }


                dailyTeeSheet = RequestDirector.ViewDailyTeeSheet(selectedDate);


                if (dailyTeeSheet.TeeTimes.Count == 0)
                {
                    dailyTeeSheet = null;
                    Alert = $"Could Not Find Daily Tee Sheet for {date}";
                    TempData["Danger"] = true;
                }
                else
                {
                    foreach (var t in dailyTeeSheet.TeeTimes)
                    {
                        DateTime time = new DateTime((t.Time.Ticks));
                        if (t.Golfer1 == null)
                            availableTeeTimes.Add(time.ToString("t"));
                    }

                    selectedDateTemp = selectedDate;

                    return Page();
                }
            }




            return Page();
        }

        public IActionResult OnPostCreateTeeTime()
        {
            Confirmation = false;
            CBS RequestDirector = new CBS();

            DailyTeeSheet verifyTeeSheet = new DailyTeeSheet();

            TeeTime newTeeTime = new TeeTime();

            TimeSpan time = Convert.ToDateTime(selectedTime).TimeOfDay;

            newTeeTime.Date = selectedDateTemp;
            newTeeTime.Time = time;
            newTeeTime.Golfer1 = Golfer1;
            newTeeTime.Golfer2 = Golfer2;
            newTeeTime.Golfer3 = Golfer3;
            newTeeTime.Golfer4 = Golfer4;

            Confirmation = RequestDirector.CreateTeeTime(newTeeTime);

            if (Confirmation)
            {
                TempData["Alert"] = $"Successfully Created Tee Time";
                return RedirectToPage("/Index");
            }
            else
            {
                TempData["Danger"] = true;
                Alert = $"Could Not Create Tee Time";
                dailyTeeSheet = null;
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