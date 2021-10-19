using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTAPI_Northwind.Models;

namespace RESTAPI_Northwind.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    // # Rakenna tietokannasta malli luokiksi syöttämällä Package Manager Consoleen seuraava rivi.
    // Tämä toimii paikalliseen tietokantaan:
    // Scaffold-DbContext "Server=DESKTOP-7V0M0JG\SQLEXPRESS;Database=NorthwindOriginal;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    // Azure-versio:
    // Scaffold-DbContext "Server=duuniserver.database.windows.net,1433;Database=tuntidb;User ID = sirensimo; Password=salasana;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

    public class EmployeesController : Controller
    {
        private static NorthwindOriginalContext db = new NorthwindOriginalContext();

        [HttpGet]
        public ActionResult GetAllEmployees()

        {
            try
            {
                var employees = db.Employees.ToList();

                return Ok(employees);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("{id}")]
        // https://localhost:5001/api/employees/id/{numero}/
        public ActionResult GetOneEmployeeById(string id)
        {

            var employee = from e in db.Employees
                          where e.EmployeeId == Convert.ToInt32(id)
                          select e;
            return Ok(employee);

        }
        [HttpGet]
        [Route("from/{from}/to/{to}")]
        // https://localhost:5001/api/employees/from/{numero}/to/{numero}
        public ActionResult GetEmployeesFromTo(int from, int to)
        {

            var employees = db.Employees.ToList();

            List<Employee> employeesReturned = new List<Employee>();

            for (int i = 0; i < employees.Count; i++)
            {
                //if (i > from & i < to)
                //{
                    employeesReturned.Add(employees[i]);
                //}
            }

            return Ok(employeesReturned);

        }

        [HttpGet]
        [Route("startsWith/{letter}")]
        // https://localhost:5001/api/employees/startsWith/{letter}/
        // # Testaa "D" kirjaimella, saat kaksi vastausta.
        public ActionResult GetByPrice(string letter)
        {

            try
            {
                var employeeList = (from e in db.Employees
                                   where e.LastName.StartsWith(letter + "")
                                   select e).ToList();
                return Ok(employeeList);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // # ERROR: Microsoft.Data.SqlClient.SqlException (0x80131904): Cannot insert explicit value for identity column in table 'employees' when IDENTITY_INSERT is set to OFF.
        //[HttpPost]
        //public ActionResult AddEmployee([FromBody] Employee emp)
        //{
        //    try
        //    {
        //        db.Employees.Add(emp);
        //        db.SaveChanges();
        //        return Ok("Added new Employee: " + emp.EmployeeId);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Error" + e);
        //    }
        //}

        [HttpPost]
        // https://localhost:5001/api/employees/

        public ActionResult AddEmployee([FromBody] Employee emp)
        {
            try
            {

                Employee empInsert = new Employee();
                if (emp != null)
                {
                    empInsert.LastName = emp.LastName;
                    empInsert.FirstName = emp.FirstName;
                    empInsert.Title = emp.Title;
                    empInsert.TitleOfCourtesy = emp.TitleOfCourtesy;
                    empInsert.BirthDate = emp.BirthDate;
                    empInsert.HireDate = emp.HireDate;
                    empInsert.Address = emp.Address;
                    empInsert.City = emp.City;
                    empInsert.Region = emp.Region;
                    empInsert.PostalCode = emp.PostalCode;
                    empInsert.Country = emp.Country;
                    empInsert.HomePhone = emp.HomePhone;
                    empInsert.Extension = emp.Extension;
                    empInsert.Notes = emp.Notes;
                    empInsert.ReportsTo = emp.ReportsTo;
                    empInsert.PhotoPath = emp.PhotoPath;
                    empInsert.ReportsToNavigation = emp.ReportsToNavigation;
                    empInsert.EmployeeTerritories = emp.EmployeeTerritories;
                    empInsert.InverseReportsToNavigation = emp.InverseReportsToNavigation;
                    empInsert.Orders = emp.Orders;
                    

                    db.Employees.Add(empInsert);
                    db.SaveChanges();
                    return Ok("Added new employee: " + emp.LastName + " " + emp.FirstName);
                }
                else
                {
                    return NotFound("Requested employee couldn't be found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Delete request resulted an error: " + e);
            }
        }

        // https://localhost:5001/api/employees/{id}/

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteEmployee(string id)
        {
            var emp = db.Employees.Find(Convert.ToInt32(id));
            if (emp != null)
            {
                try
                {
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                    return Ok("Employee " + emp.FirstName + " " + emp.LastName + " deleted.");
                }
                catch (Exception e)
                {
                    return BadRequest("Delete action failed: " + e);
                }
            }
            else
            {
                return NotFound("Employee " + id + " not found.");
            }
        }

        [HttpPut]
        [Route("{id}")]
        // https://localhost:5001/api/employees/{id}
        public ActionResult Update(string id, [FromBody] Employee emp)
        {
            try
            {
                Employee empInsert = db.Employees.Find(Convert.ToInt32(id));

                if (empInsert != null)
                {
                    empInsert.LastName = emp.LastName;
                    empInsert.FirstName = emp.FirstName;
                    empInsert.Title = emp.Title;
                    empInsert.TitleOfCourtesy = emp.TitleOfCourtesy;
                    empInsert.BirthDate = emp.BirthDate;
                    empInsert.HireDate = emp.HireDate;
                    empInsert.Address = emp.Address;
                    empInsert.City = emp.City;
                    empInsert.Region = emp.Region;
                    empInsert.PostalCode = emp.PostalCode;
                    empInsert.Country = emp.Country;
                    empInsert.HomePhone = emp.HomePhone;
                    empInsert.Extension = emp.Extension;
                    empInsert.Notes = emp.Notes;
                    empInsert.ReportsTo = emp.ReportsTo;
                    empInsert.PhotoPath = emp.PhotoPath;
                    empInsert.ReportsToNavigation = emp.ReportsToNavigation;
                    empInsert.EmployeeTerritories = emp.EmployeeTerritories;
                    empInsert.InverseReportsToNavigation = emp.InverseReportsToNavigation;
                    empInsert.Orders = emp.Orders;

                    db.SaveChanges();
                    return Ok("Update succeeded for employee: " + emp.FirstName + " " + emp.LastName);
                }
                else
                {
                    return NotFound("Requested employee couldn't be found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Update request resulted an error: " + e);
            }




        }

    }
}
