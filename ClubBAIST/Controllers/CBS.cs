using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Managers;
using ClubBaist.Models;

namespace ClubBaist.Controllers
{
    public class CBS
    {
        public DailyTeeSheet ViewDailyTeeSheet(DateTime date)
        {
            DailyTeeSheet dailyTeeSheet = new DailyTeeSheet();
            TeeTimes dataManager = new TeeTimes();
            dailyTeeSheet = dataManager.FindDailyTeeSheet(date);

            return dailyTeeSheet;
        }

        public bool CreateTeeTime(TeeTime selectedTeeTime)
        {
            bool Success = false;
            TeeTimes dataManager = new TeeTimes();
            Success = dataManager.CreateTeeTime(selectedTeeTime);
            return Success;
        }
    }
}
