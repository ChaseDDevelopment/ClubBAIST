using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaist
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Golfer golfer { get; set; }
        [Required]
        [BindProperty]
        public int MemberNumber { get; set; }
        [Required]
        [BindProperty]
        public string Password { get; set; }
        [TempData] public bool Confirmation { get; set; }
        [TempData] public string Alert { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                TokenProvider _tokenProvider = new TokenProvider();
                var userToken = _tokenProvider.LoginUser(MemberNumber, Password);
                if (userToken != null)
                {
                    HttpContext.Session.SetString("JWToken", userToken);
                    Confirmation = true;
                    TempData["Alert"] = $"Successfully Logged in";
                    return RedirectToPage("Index");
                }
                else
                {
                    TempData["Danger"] = true;
                    Alert = $"Member Number or Password wrong";
                    return Page();
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