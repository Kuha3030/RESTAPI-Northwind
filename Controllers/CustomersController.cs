using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTAPI_Northwind.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI_Northwind.Controllers

{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    // # Rakenna tietokannasta malli luokiksi syöttämällä Package Manager Consoleen seuraava rivi.
    // Tämä toimii paikalliseen tietokantaan:
    // Scaffold-DbContext "Server=DESKTOP-7V0M0JG\SQLEXPRESS;Database=NorthwindOriginal;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    // Jos ajaa uudestaan (päivittää taulut tietokannasta) lisää vielä parametri -force
    // Azure-versio:
    // Scaffold-DbContext "Server=duuniserver.database.windows.net,1433;Database=tuntidb;User ID = sirensimo; Password=salasana;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

    public class CustomersController : ControllerBase
    {
        private static NorthwindOriginalContext db = new NorthwindOriginalContext();

        [HttpGet]
        public ActionResult GetAllCustomers()

        {
            try
            {
                var customers = db.Customers.ToList();

                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("from/{from}/to/{to}")]
        // https://localhost:5001/api/customers/from/{numero}/to/{numero}
        public ActionResult GetCustomersFromTo(int from, int to)
        {

            var customers = db.Customers.ToList();

            List<Customer> customersReturned = new List<Customer>();

            for (int i = 0; i < customers.Count; i++)
            {
                if (i >= from & i <= to)
                {
                    customersReturned.Add(customers[i]);
                }
            }

            return Ok(customersReturned);

        }


        [HttpGet]
        [Route("id/{id}")]
        // https://localhost:5001/api/customers/id/blaus
        public ActionResult GetOneCustomerById(string id)
        {
            Customer customer = db.Customers.Find(id);
            return Ok(customer);

        }


        [HttpGet]
        [Route("country/{country}")]
        // https://localhost:5001/api/customers/country/GERMANY
        public ActionResult GetOneCustomerByCountry(string country)
        {
            Stopwatch stopwatchLinq = new Stopwatch();

            stopwatchLinq.Start();

            var asiakkaat = (from c in db.Customers
                             where c.Country == country
                             select c).ToList();
            stopwatchLinq.Stop();

            string result = "Linq: " + stopwatchLinq.ElapsedMilliseconds;
            return Ok(result);
	 

        }
        // # Vaihtehtoinen tapa foreach loopilla
        [HttpGet]
        [Route("countryForeach/{country}")]
        // https://localhost:5001/api/customers/country/GERMANY
        public ActionResult GetOneCustomerByCountry1(string country)
        {
            // # Tämä toimii myös!
            Stopwatch stopwatchForEach = new Stopwatch();


            stopwatchForEach.Start();
            var customers = db.Customers.ToList();

            List<Customer> returnList = new List<Customer>();

            foreach (var line in customers)
            {
                if (line.Country == country)
                    returnList.Add(line);
            }

            stopwatchForEach.Stop();

            string result = "Foreach: " + stopwatchForEach.ElapsedMilliseconds;
            return Ok(result);
        }


        [HttpGet]
        [Route("documentation/{keycode}")]
        // https://localhost:5001/api/customers/documentation/g00gl3/
        public ActionResult GetDocumentation(string keycode)
        {


            var documentation = db.Documentations.ToList();

            var returnRouteDescription = db.Documentations.Where(d => keycode.Contains(d.Keycode))
                      .Select(x => new { Route = x.Route, Description = x.Description }).ToList();

            //var returnRoute = from d in db.Documentations
            //                  where d.Keycode == keycode
            //                  select d.Route;


            // var keycodeComparison = "null";
            //foreach (var item in documentation)
            //{
            //    if (item.Keycode == keycode)
            //    {
            //        keycodeComparison = item.Keycode;
            //        returnRoute = item.Route;
            //    }
            //}

            //var juttu = returnRoute.;
            //var juttu2 = returnRouteDescription[0];


            if (returnRouteDescription.Count == 0)
            {
                return BadRequest();
            }
            else
            { 
            return Ok(returnRouteDescription);
            }
        }


        [HttpPost]
        //[Route("addcustomer/{customer}")]

        public ActionResult AddCustomer([FromBody] Customer cust) 
        { 
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok("Lisättiin asiaks id:llä: " + cust.CustomerId);
            }
            catch (Exception e)
            {
                return BadRequest("Virhe" + e);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public ActionResult DeleteCustomer(string id)
        {
            var cust = db.Customers.Find(id);
            if (cust != null)
            { 
                try
                {
                    db.Customers.Remove(cust);
                    db.SaveChanges();
                    return Ok("Customer: " + cust.CompanyName + " deleted.");
                }
                catch (Exception e)
                {
                    return BadRequest("Delete action failed: " + e);
                }
            }
            else
            {
                return NotFound("Asiakasta " + id + " ei löytynyt.");
            }
        }

        [HttpPut]
        [Route("{id}")]

        public ActionResult Update(string id, [FromBody] Customer customerInput)
        {

            try
            {
                Customer cust = db.Customers.Find(id);
                if (cust != null)
                {
                    cust.CompanyName = customerInput.CompanyName;
                    cust.ContactName = customerInput.ContactName;
                    cust.ContactTitle = customerInput.ContactTitle;
                    cust.Country = customerInput.Country;
                    cust.Address = customerInput.Address;
                    cust.City = customerInput.City;
                    cust.PostalCode = customerInput.PostalCode;
                    cust.Phone = customerInput.Phone;
                    cust.Fax = customerInput.Fax;

                    db.SaveChanges();
                    return Ok("Update succeeded for customer: " + cust.CompanyName);
                }
                else
                {
                    return NotFound("Requested customer couldn't be found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Delete request resulted an error: " + e);
            }

                

            
        }

    }
}
