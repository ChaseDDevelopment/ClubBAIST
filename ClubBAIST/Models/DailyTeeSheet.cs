using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaist.Models
{
    public class DailyTeeSheet
    {
        public DateTime Date { get; set; }

        public List<TeeTime> TeeTimes { get; set; }
    }
}
