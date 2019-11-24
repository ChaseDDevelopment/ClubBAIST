using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist.Pages
{
    public class CreateTeeTimeModel : PageModel
    {

        [BindProperty] 
        public string date { get; set; }

        [TempData]
        public string Alert { get; set; }

        [TempData]
        public DateTime selectedDateTemp { get; set; }

        [BindProperty]
        public DailyTeeSheet dailyTeeSheet { get; set; }

        [BindProperty]
        public List<string> availableTeeTimes { get; set; }


        [BindProperty]
        public string Golfer1 { get; set; }
        [BindProperty]
        public string Golfer2 { get; set; }

        [BindProperty]
        public string Golfer3 { get; set; }
        [BindProperty]
        public string Golfer4 { get; set; }
        [BindProperty]
        public string selectedTime { get; set; }


        //[TempData]
        //public DailyTeeSheet dailyTeeSheet { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPostFind()
        {
            CBS RequestDirector = new CBS();

            DateTime.TryParse(date, out DateTime selectedDate);

            //DailyTeeSheet dailyTeeSheet = new DailyTeeSheet();

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



            return Page();
        }

        public IActionResult OnPostCreateTeeTime()
        {
            bool Confirmation = false;
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

            //Used to Check if the Tee Time has been taken during the time it took to post
            verifyTeeSheet = RequestDirector.ViewDailyTeeSheet(selectedDateTemp);
            foreach (var t in verifyTeeSheet.TeeTimes)
            {
                if (t.Time == time && t.Golfer1 != null)
                    Confirmation = false;
                dailyTeeSheet = null;
                return Page();
            }

            Confirmation = RequestDirector.CreateTeeTime(newTeeTime);

            if (Confirmation)
            {
                dailyTeeSheet = null;

                return Page();
            }

            return Page();
        }
    }
}