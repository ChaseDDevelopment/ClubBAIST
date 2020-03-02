using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaist.Models
{
    public class MemberAccountEntries
    {
        public int MemberNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentDescription { get; set; }
        public decimal PaymentAmount { get; set; }

    }
}
