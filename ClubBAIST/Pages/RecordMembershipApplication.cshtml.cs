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

        [Required]
        public int MembershipLevel { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyPhone { get; set; }
        [Required]
        public int Sponser1 { get; set; }
        [Required]
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
                DateTime.TryParse(DateOfBirth, out DateTime DOB);
                Golfer.DateOfBirth = DOB;
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