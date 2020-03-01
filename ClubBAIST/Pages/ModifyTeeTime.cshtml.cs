using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist
{
    [UnAuthorized]
    public class ModifyTeeTimeModel : PageModel
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
        //[BindProperty] public DailyTeeSheet dailyTeeSheet { get; set; }
        public List<string> availableTimesString = new List<string>();
        [BindProperty] public TeeTime selectedTeeTime { get; set; }
        [BindProperty] public bool availableListPopulated { get; set; }
        [BindProperty] public string Golfer1 { get; set; }
        [BindProperty] public string Golfer2 { get; set; }

        [BindProperty] public string Golfer3 { get; set; }
        [BindProperty] public string Golfer4 { get; set; }
        [BindProperty]
        [DisplayName("Desired Time")]
        public string selectedTime { get; set; }

        [BindProperty]
        public int memberLevel { get; set; }

        [BindProperty]
        public int golferSearch { get; set; }

        public void OnGet()
        {
            List<TimeSpan> availableTimes = new List<TimeSpan>();
            var startTime = TimeSpan.FromHours(7);
            for (var i = 0; i < 97; i++)
            {

                availableTimes.Add(startTime);
                if (i % 2 == 0)
                    startTime += TimeSpan.FromMinutes(7);
                else
                    startTime += TimeSpan.FromMinutes(8);
            }
            foreach (var t in availableTimes)
            {
                DateTime time = new DateTime(t.Ticks);
                availableTimesString.Add(time.ToString("t"));
            }


            
        }

        public IActionResult OnPostFind()
        {
            if (ModelState.IsValid)
            {
                Golfer golfer = new Golfer();
                CBS RequestDirector = new CBS();

                DateTime.TryParse(date, out DateTime selectedDate);

                if (selectedDate > DateTime.Today + TimeSpan.FromDays(7) || selectedDate < DateTime.Today)
                {
                    TempData["Danger"] = true;
                    if (selectedDate < DateTime.Today)
                    {
                        Alert = $"Cannot Modify a tee time for a passed date.";
                    }
                    else
                    {
                        Alert = $"Cannot Modify a tee time more than 7 days in advance.";
                    }

                    selectedTeeTime = null;
                    return Page();
                }

                TimeSpan time = Convert.ToDateTime(selectedTime).TimeOfDay;
                string[] Name = User.Identity.Name.Split(' ');

                golfer.FirstName = Name[0];
                golfer.LastName = Name[1];

                selectedTeeTime = RequestDirector.FindTeeTime(selectedDate, time);

                if (selectedTeeTime.Time == null)
                {
                    selectedTeeTime = null;
                    Alert = $"Could Not find Tee Time for {time}";
                    TempData["Danger"] = true;
                }
                else
                {
                    return Page();
                }


            }

            return Page();
        }
    }
}