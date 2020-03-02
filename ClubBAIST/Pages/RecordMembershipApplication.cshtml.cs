using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Controllers;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist
{
    [BindProperties]
    [UnAuthorized]
    public class RecordMembershipApplicationModel : PageModel
    {

        [TempData]
        public bool Confirmation { get; set; }
        [TempData]
        public string Alert { get; set; }


        public Golfer Golfer { get; set; }

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


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (_Shareholder == "1")
            {
                Golfer.Shareholder = true;
            }
            else
            {
                Golfer.Shareholder = false;
            }

            if (_Shareholder == "1" && MembershipLevel != 1)
            {
                Alert = $"A member cannot be a shareholder unless they are a Gold member";
                TempData["Danger"] = true;
                return Page();
            }

            if (DateOfBirth != "")
            {
                DateTime.TryParse(DateOfBirth, out DateTime DOB);
                Golfer.DateOfBirth = DOB;
            }
            

            


            Confirmation = false;
            if (ModelState.IsValid)
            {
                CBS RequestDirector = new CBS();

                Golfer.MembershipLevel = MembershipLevel;
                Golfer.FirstName = FirstName;
                Golfer.LastName = LastName;
                Golfer.Password = Password;
                Golfer.Address = Address;
                Golfer.PostalCode = PostalCode;
                Golfer.Phone = Phone;
                Golfer.AltPhone = AltPhone;
                Golfer.Email = Email;

                Golfer.Occupation = Occupation;
                Golfer.CompanyName = CompanyName;
                Golfer.CompanyAddress = CompanyAddress;
                Golfer.CompanyPostalCode = CompanyPostalCode;
                Golfer.CompanyPhone = CompanyPhone;
                Golfer.Sponser1 = Sponser1;
                Golfer.Sponser2 = Sponser2;

                Confirmation = RequestDirector.RecordMembershipApplication(Golfer);

                if (Confirmation)
                {
                    TempData["Alert"] = $"Successfully Recorded Membership Application";
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["Danger"] = true;
                    Alert = $"Could Not Record Membership Application";
                }
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