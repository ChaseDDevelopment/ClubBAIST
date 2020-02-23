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
            bool Confirmation = false;
            TeeTimes dataManager = new TeeTimes();
            Confirmation = dataManager.CreateTeeTime(selectedTeeTime);
            return Confirmation;
        }

        //public bool ModifyTeeTime(TeeTime selectedTeeTime)
        //{
        //    bool Confirmation = false;
        //    TeeTimes dataManager = new TeeTimes();
        //    Confirmation = dataManager.ModifyTeeTime(selectedTeeTime);
        //    return Confirmation;
        //}

        public bool CreateStandingTeeTimeRequest(StandingTeeTime request)
        {
            bool Confirmation = false;
            StandingTeeTimes dataManager = new StandingTeeTimes();
            Confirmation = dataManager.CreateStandingTeeTimeRequest(request);
            return Confirmation;

        }

        public Golfer getGolfer(int MemberNumber)
        {
            Golfer golfer = new Golfer();
            Golfers dataManager = new Golfers();
            golfer = dataManager.GetGolfer(MemberNumber);
            return golfer;
        }
    }
}
