using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClubBaist.Models;
using Microsoft.IdentityModel.Tokens;

namespace ClubBaist
{
    public class TokenProvider
    {
        public string LoginUser(int MemberNumber, string Password)
        {

            //var user = golfers.SingleOrDefault(x => x.MemberNumber == MemberNumber);

            BCS


            if (user == null)
            {
                return null;
            }

            if (Password == user.Password)
            {
                var key = Encoding.ASCII.GetBytes("as;dlkfjas;ldkfja;sldkfjas98a8***p0a9sdfa");

                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:5000/",
                    audience: "http://localhost:5000/",
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature));
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
            {
                return null;
            }
              
        }


        private IEnumerable<Claim> GetUserClaims(Golfer golfer)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, golfer.FirstName + " " + golfer.LastName), 
                new Claim(ClaimTypes.Email, golfer.Email),
                new Claim("MembershipLevel", golfer.MembershipLevel.ToString())
            };
            return claims;
        }


        //private List<Golfer> golfers = new List<Golfer>
        //{
        //    new Golfer
        //    {
        //        MemberNumber = 1, MembershipLevel = 6, FirstName = "Chase", LastName = "Dubauskas",
        //        Password = "Password1", Email = "Chase.D.Dubauskas@gmail.com"
        //    }
        //};


    }
}
