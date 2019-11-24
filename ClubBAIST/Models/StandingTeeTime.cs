using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaist.Models
{
    public class StandingTeeTime
    {
        public int StandingTeeTimeID { get; set; }
        public int MemberNumber1 { get; set; }
        public int MemberNumber2 { get; set; }
        public int MemberNumber3 { get; set; }
        public int MemberNumber4 { get; set; }

        public string MemberName1 { get; set; }
        public string MemberName2 { get; set; }
        public string MemberName3 { get; set; }
        public string MemberName4 { get; set; }
        public DateTime DayOfWeek { get; set; }
        public DateTime Time { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
