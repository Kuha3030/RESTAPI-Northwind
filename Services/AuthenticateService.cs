using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using RESTAPI_Northwind.Models;
using RESTAPI_Northwind.Services.Interfaces;
using System.Text;

namespace RESTAPI_Northwind.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;

        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private NorthwindOriginalContext db = new NorthwindOriginalContext();
        public LoggedInUser loggedInUser = new LoggedInUser();
        public LoggedInUser Authenticate(string userName, string password)
        {

            var user = db.Logins.SingleOrDefault(x => x.Username == userName && x.Password == password);

            // Jos ei käyttäjää löydy palautetaan null
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.LoginId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2), // Montako päivää token on voimassa

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //user.Token = tokenHandler.WriteToken(token);

            //user.Password = null; // poistetaan salasana ennenkuin palautetaan

            //return user; // Palautetaan kutsuvalle controllerimetodille user ilman salasanaa
            loggedInUser.Username = user.Username;
            loggedInUser.Token = tokenHandler.WriteToken(token);
            loggedInUser.AccesslevelId = user.AccesslevelId;

            return loggedInUser;


        }

    }
}
