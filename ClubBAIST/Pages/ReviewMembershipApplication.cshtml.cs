using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist
{
    [BindProperties]
    [UnAuthorized]
    public class ReviewMembershipApplicationModel : PageModel
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

        }

        public IActionResult OnPostFind()
        {
            CBS RequestDirector = new CBS();


            ModelState.Remove("MembershipLevel");
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Password");
            ModelState.Remove("DateOfBirth");
            ModelState.Remove("Sponser1");
            ModelState.Remove("Sponser2");
            ModelState.Remove("MembershipStartDate");
            if (ModelState.IsValid)
            {
                Golfer = RequestDirector.GetGolfer(MemberNumber);

                if (Golfer.FirstName == null)
                {
                    Golfer = null;
                    Alert = $"Could Not Find Member";
                    TempData["Danger"] = true;
                }
                else
                {
                    Golfer.MembershipStartDate = DateTime.Now;
                    TempData["MemberNumber"] = MemberNumber;
                }

            }


            return Page();
        }

        public IActionResult OnPostReview()
        {
            Confirmation = false;
            CBS RequestDirecter = new CBS();

            Golfer.Approved = Approved.ToString();
            DateTime.TryParse(MembershipStartDate, out DateTime date);
            Golfer.MembershipStartDate = date;
            Golfer.MemberNumber = (int)TempData["MemberNumber"];

            Confirmation = RequestDirecter.ReviewMembershipApplication(Golfer);

            if (Confirmation)
            {
                TempData["Alert"] = $"Successfully Updated Membership Application";
                return RedirectToPage("/Index");
            }
            else
            {
                TempData["Danger"] = true;
                Alert = $"Could Not Update Membership Application";
                Golfer = null;
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