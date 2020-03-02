using System;
using System.Collections.Generic;
using System.Data;
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

        public StandingTeeTime FindStandingTeeTimeRequest(int MemberNumber)
        {
            StandingTeeTimes dataManager = new StandingTeeTimes();
            var request = dataManager.FindStandingTeeTimeRequest(MemberNumber);
            return request;
        }

        public bool ModifyTeeTime(TeeTime selectedTeeTime)
        {
            bool Confirmation = false;
            TeeTimes dataManager = new TeeTimes();
            Confirmation = dataManager.ModifyTeeTime(selectedTeeTime);
            return Confirmation;
        }

        public bool DeleteTeeTime(TeeTime selectedTeeTime)
        {
            bool Confirmation = false;
            TeeTimes dataManager = new TeeTimes();
            Confirmation = dataManager.DeleteTeeTime(selectedTeeTime);
            return Confirmation;
        }


        public TeeTime FindTeeTime(DateTime Date, TimeSpan time)
        {
            TeeTimes dataManager = new TeeTimes();
            var teeTime = dataManager.FindTeeTime(Date, time);
            return teeTime;
        }

        #region NOT DONE
        //NOT DONE





        //NOT DONE


        public bool CancelStandingTeeTimeRequest(int StandingTeeTimeID)
        {
            bool Confirmation = false;
            StandingTeeTimes dataManager = new StandingTeeTimes();
            Confirmation = dataManager.CancelStandingTeeTimeRequest(StandingTeeTimeID);
            return Confirmation;
        }

        public Golfer ViewMemberAccount(int MemberNumber)
        {
            Golfers dataManager = new Golfers();
            var Account = dataManager.ViewMemberAccount(MemberNumber);
            return Account;
        }
        public bool RecordMembershipApplication(Golfer golfer)
        {
            bool Confirmation = false;
            Golfers dataManager = new Golfers();
            Confirmation = dataManager.RecordMembershipApplication(golfer);
            return Confirmation;
        }

        public bool UpdateMemberAccount(Golfer golfer)
        {
            bool Confirmation = false;
            Golfers dataManager = new Golfers();
            Confirmation = dataManager.UpdateMemberAccount(golfer);
            return Confirmation;
        }

        #endregion

        public Golfer GetGolfer(int MemberNumber)
        {
            Golfers dataManager = new Golfers();
            var Account = dataManager.GetGolfer(MemberNumber);
            return Account;
        }

        public List<Golfer> GetGolfers()
        {
            Golfers dataManager = new Golfers();
            var Golfers = dataManager.GetGolfers();
            return Golfers;
        }


        public bool CreateStandingTeeTimeRequest(StandingTeeTime request)
        {
            bool Confirmation = false;
            StandingTeeTimes dataManager = new StandingTeeTimes();
            Confirmation = dataManager.CreateStandingTeeTimeRequest(request);
            return Confirmation;

        }
    }
}
