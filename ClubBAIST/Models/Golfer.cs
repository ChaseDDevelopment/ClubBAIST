using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaist.Models
{
    public class Golfer
    {
        public int MemberNumber { get; set; }
        public int MembershipLevel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyPhone { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public int Sponser1 { get; set; }
        public int Sponser2 { get; set; }
        public bool Shareholder { get; set; }
        public string Approved { get; set; }

        public List<MemberAccountEntries> AccountEntries { get; set; }
    }


}
