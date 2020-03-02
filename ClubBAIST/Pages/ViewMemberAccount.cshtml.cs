using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist
{
    [BindProperties]
    [UnAuthorized]
    public class ViewMemberAccountModel : PageModel
    {
        [TempData]
        public bool Confirmation { get; set; }
        [TempData]
        public string Alert { get; set; }

        public Golfer Golfer { get; set; }

        [Required, RegularExpression(@"[1-9]", ErrorMessage = "Must be greater than 0")]
        public int MemberNumber { get; set; }

        [Required, RegularExpression(@"[1-9]", ErrorMessage = "Must be greater than 0")]
        public int MembershipLevel { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Address { get; set; }
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Must be like A#A#A#")]
        public string PostalCode { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
        [StringLength(10)]
        public string AltPhone { get; set; }
        public string Email { get; set; }
        [RegularExpression(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", ErrorMessage =
            "Must be in YYYY-MM-DD format.")]
        [StringLength(10)]
        [Required]
        public string DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Must be like A#A#A#")]
        public string CompanyPostalCode { get; set; }
        [StringLength(10)]
        public string CompanyPhone { get; set; }
        [Required, RegularExpression(@"[1-9]", ErrorMessage = "Must be greater than 0")]
        public int Sponser1 { get; set; }
        [Required, RegularExpression(@"[1-9]", ErrorMessage = "Must be greater than 0")]
        public int Sponser2 { get; set; }
        public string _Shareholder { get; set; }
        public char Approved { get; set; }
        [RegularExpression(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", ErrorMessage =
            "Must be in YYYY-MM-DD format.")]
        [StringLength(10)]
        [Required]
        public string MembershipStartDate { get; set; }
        public void OnGet()
        {
            Golfer golfer = new Golfer();
            CBS RequestDirector = new CBS();
            var MemberNum = User.Claims.FirstOrDefault(c => c.Type == "MemberNumber").Value;
            Int32.TryParse(MemberNum, out int MemberNumber);

            Golfer = RequestDirector.ViewMemberAccount(MemberNumber);
            if (Golfer.FirstName == null)
            {
                TempData["Danger"] = true;
                Alert = $"Catastrophic Failure! How are you logged in?!";
                Golfer = null;

            }
            else
            {
                Golfer = golfer;
            }


        }


        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}