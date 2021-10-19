using Microsoft.AspNetCore.Mvc;
using RESTAPI_Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI_Northwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : Controller
    {
        // https://localhost:5001/api/logins

        private static NorthwindOriginalContext db = new NorthwindOriginalContext();


        [HttpGet]
        public ActionResult GetLoginsWithoutPassword(int from, int to)
        {

            var logins = (from x in db.Logins
                          select new
                          {
                              LoginID = x.LoginId,
                              Firstname = x.Firstname,
                              Lastname = x.Lastname,
                              Username = x.Username,
                              Email = x.Email,
                              Accesslevel = x.AccesslevelId
                          }).ToList();

            return Ok(logins);

        }


        [HttpPost]
        public ActionResult AddLogin([FromBody] Login login)
        {

                try
                {

                    Login loginInsert = new Login();
                    if (login != null)
                    {
                        loginInsert.Firstname = login.Firstname;
                        loginInsert.Lastname = login.Lastname;
                        loginInsert.Email = login.Email;
                        loginInsert.Username = login.Username;
                        loginInsert.Password = login.Password;
                        loginInsert.AccesslevelId = login.AccesslevelId;

                        db.Logins.Add(login);
                        db.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound("Requested product couldn't be found.");
                    }
                }
                catch (Exception e)
                {
                    return BadRequest("Login create resulted an error: " + e);
                }
            }
    }
}