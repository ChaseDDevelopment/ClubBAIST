using System;
using System.Collections.Generic;
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
        [BindProperty]
        public int MemberNumber { get; set; }
        [BindProperty]
        public string Password { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            
            TokenProvider _tokenProvider = new TokenProvider();
            var userToken = _tokenProvider.LoginUser(MemberNumber, Password);
            if (userToken != null)
            {
                HttpContext.Session.SetString("JWToken", userToken);
            }

            return RedirectToPage("Index");

        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}