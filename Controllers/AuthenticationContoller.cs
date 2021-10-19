using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RESTAPI_Northwind.Models;
using RESTAPI_Northwind.Services.Interfaces;

namespace RESTAPI_Northwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateService _authenticateService;

        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginCredentials lc)
        {
            var user = _authenticateService.Authenticate(lc.Username, lc.Password);

            if (user == null)
                return BadRequest(new { message = "Käyttäjätunus tai salasana on virheellinen" });

            return Ok(user); // Palautus front endiin
        }
    }
}
