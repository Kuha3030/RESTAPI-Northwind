using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTAPI_Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI_Northwind.Controllers
{

    // # Ei liity palautukseen mitenkään. Omaa harjoittelua ensimmäisen tunnin aluksi.

    [Route("api/[controller]")]
    [ApiController]
    public class HirviötController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAll()
        { 
        List<hirviö> hirvioLista = new List<hirviö>();
        hirvioLista.Add(new hirviö { Id = 0001, Nimi = "Vampyyri", Voima = 500 });
        hirvioLista.Add(new hirviö { Id = 0002, Nimi = "Lohikäärme", Voima = 6000 });
        hirvioLista.Add(new hirviö { Id = 0003, Nimi = "Jouto-Pekka", Voima = 1.1M });
        hirvioLista.Add(new hirviö { Id = 0004, Nimi = "Entti", Voima = 550 });

        return Ok(hirvioLista);


        }
    }
}
