using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI.Services
{
    public class UserService
    {
        public bool Login(string userName, string password, out string token, out string errMsg)
        {
            token = "";
            errMsg = "";
            string role = "";
            if (userName == "admin")
            {
                if (password != "admin123")
                {
                    errMsg = "Invalid User Name or Password.";
                    return false;
                }
                role = "admin";
            }
            else  if (userName == "user")
            {
                if (password != "user123")
                {
                    errMsg = "Invalid User Name or Password.";
                    return false;
                }
                role = "user";
            }
            else
            {
                errMsg = "Invalid User Name or Password.";
                return false;
            }


            // authentication successful so generate jwt token
            token = generateJwtToken(userName, role);

            return true;
        }

        private string generateJwtToken(string userName, string role)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING
            var secret = "RHkSPzqWmpklBUh6BNLX";

            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("userName", userName),
                    new Claim(ClaimTypes.Role, role)
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}