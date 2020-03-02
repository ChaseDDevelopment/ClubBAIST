using ClubBaist.Controllers;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClubBaist.Managers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

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

        [BindProperty]
        public int memberLevel { get; set; }
        [BindProperty]
        public List<Golfer> golfers { get; set; }
        [BindProperty]
        public int selectedGolferNumber { get; set; }


        public void OnGet()
        {
            //memberName = User.Claims.FirstOrDefault(c => c.Type == Nam).Value;
        }

        public IActionResult OnPostFind()
        {

            if (ModelState.IsValid)
            {
                CBS RequestDirector = new CBS();

                var MembershipLevel = User.Claims.FirstOrDefault(c => c.Type == "MembershipLevel").Value;
                Int32.TryParse(MembershipLevel, out int memlevel);
                memberLevel = memlevel;

                DateTime.TryParse(date, out DateTime selectedDate);

                if (User.Identity.Name == "Club Clerk" || User.Identity.Name == "Club ProShop")
                {
                    golfers = RequestDirector.GetGolfers();
                }
                TempData.Put("key", golfers);

                if (selectedDate == DateTime.Today && User.Identity.Name != "Club ProShop")
                {
                    TempData["Danger"] = true;
                    dailyTeeSheet = null;
                    Alert = $"Can not create Tee Times the day of the Tee Time unless you are Pro Shop Staff";
                    return Page();
                }

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
                    List<TeeTime> teeTimesToInclude = new List<TeeTime>();
                    foreach (TeeTime t in dailyTeeSheet.TeeTimes)
                    {
                        DateTime time = new DateTime((t.Time.Ticks));
                        TeeTime teeTime = new TeeTime();
                        if (t.Golfer1 == null)
                        {
                            if (memlevel == 3)
                            {
                                if (selectedDate.DayOfWeek == DayOfWeek.Saturday ||
                                    selectedDate.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    if (time.TimeOfDay >= TimeSpan.FromHours(11))
                                    {
                                        teeTimesToInclude.Add(t);
                                    }
                                }
                                else
                                { 
                                    if (time.TimeOfDay <= TimeSpan.FromHours(15) || time.TimeOfDay >= TimeSpan.FromHours(17.5))
                                    {
                                        teeTimesToInclude.Add(t);
                                    }
                                }


                            }
                            else if(memlevel == 4)
                            {
                                if (selectedDate.DayOfWeek == DayOfWeek.Saturday ||
                                    selectedDate.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    if (time.TimeOfDay >= TimeSpan.FromHours(13))
                                    {
                                        teeTimesToInclude.Add(t);
                                    }
                                }
                                else
                                {
                                    if (time.TimeOfDay <= TimeSpan.FromHours(15) || time.TimeOfDay >= TimeSpan.FromHours(18))
                                    {
                                        teeTimesToInclude.Add(t);
                                    }
                                }

                            }
                            else
                            {
                                teeTimesToInclude.Add(t);
                            }

                        }
                            
                    }

                    dailyTeeSheet.TeeTimes = teeTimesToInclude;
                    foreach (var t in dailyTeeSheet.TeeTimes)
                    {
                        DateTime time = new DateTime(t.Time.Ticks);
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

            int SelectTest = selectedGolferNumber;

            Golfer selectedGolfer = new Golfer();
            var tempGolfers = TempData.Get<List<Golfer>>("key");
            foreach (var g in tempGolfers)
            {
                if (g.MemberNumber == selectedGolferNumber)
                {
                    selectedGolfer = g;
                }
            }


            DailyTeeSheet verifyTeeSheet = new DailyTeeSheet();

            TeeTime newTeeTime = new TeeTime();

            TimeSpan time = Convert.ToDateTime(selectedTime).TimeOfDay;

            if (User.Identity.Name == "Club Clerk" || User.Identity.Name == "Club ProShop")
            {
                newTeeTime.CreatedBy = selectedGolfer.MemberNumber;
            }
            else
            {
                Int32.TryParse(User.Claims.SingleOrDefault(c => c.Type == "MemberNumber").Value, out int createdBy);
                newTeeTime.CreatedBy = createdBy;
            }
            if (User.Identity.Name == "Club Clerk" || User.Identity.Name == "Club ProShop")
            {
                newTeeTime.Golfer1 = selectedGolfer.FirstName + " " + selectedGolfer.LastName;
            }
            else
            {
                newTeeTime.Golfer1 = User.Identity.Name;
            }

            newTeeTime.Date = selectedDateTemp;
            newTeeTime.Time = time;
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