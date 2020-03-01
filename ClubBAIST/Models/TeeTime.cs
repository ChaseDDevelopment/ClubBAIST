using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaist.Models
{
    public class TeeTime
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int CreatedBy { get; set; }
        public string Golfer1 { get; set; }

        public string Golfer2 { get; set; }

        public string Golfer3 { get; set; }

        public string Golfer4 { get; set; }
        public bool Checkin { get; set; }
    }
}
