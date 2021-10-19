using Microsoft.AspNetCore.Mvc;
using RESTAPI_Northwind.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


 
namespace RESTAPI_Northwind.Controllers

{
        [Route("api/[controller]")]
        [ApiController]
        // # Rakenna tietokannasta malli luokiksi syöttämällä Package Manager Consoleen seuraava rivi.
        // Tämä toimii paikalliseen tietokantaan:
        // Scaffold-DbContext "Server=DESKTOP-7V0M0JG\SQLEXPRESS;Database=NorthwindOriginal;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
        // Azure-versio:
        // Scaffold-DbContext "Server=duuniserver.database.windows.net,1433;Database=tuntidb;User ID = sirensimo; Password=salasana;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

        public class ProductsController : Controller
        {
            private static NorthwindOriginalContext db = new NorthwindOriginalContext();

            [HttpGet]
            public ActionResult GetAllProducts()

            {
                try
                {
                    var customers = db.Products.ToList();

                    return Ok(customers);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }

            }

            [HttpGet]
            [Route("from/{from}/to/{to}")]
            // https://localhost:5001/api/products/from/{numero}/to/{numero}
            public ActionResult GetProductsFromTo(int from, int to)
            {

                var products = db.Products.ToList();

                List<Product> productsReturned = new List<Product>();

                for (int i = 0; i < products.Count; i++)
                {
                    if (i >= from & i <= to)
                    {
                        productsReturned.Add(products[i]);
                    }
                }

                return Ok(productsReturned);

            }


        [HttpGet]
            [Route("id/{id}")]
            // https://localhost:5001/api/products/id/{numero}/
            public ActionResult GetOneCustomerById(string id)
            {

            var product = from p in db.Products
                          where p.ProductId == Convert.ToInt32(id)
                          select p;
                return Ok(product);

            }


            [HttpGet]
            [Route("getByPrice/min/{min}/max/{max}")]
            // https://localhost:5001/api/products/getByPrice/min/1/max/200
            public ActionResult GetByPrice(string min, string max)
            {

                try 
                { 
                    var productList = (from p in db.Products
                                      where p.UnitPrice > Convert.ToInt32(min) & p.UnitPrice < Convert.ToInt32(max)
                                      select p).ToList();
                    return Ok(productList);
                    
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }

            }


            [HttpPost]
            public ActionResult AddProduct([FromBody] Product prod)
            {
            //try
            //{
            //    db.Products.Add(prod);
            //    db.SaveChanges();
            //    return Ok("Added new product: " + prod.ProductName);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest("Virhe" + e);
            //}

            try
            {
                
                Product prodInsert = new Product();
                if (prod != null)
                {
                    prodInsert.ProductName = prod.ProductName;
                    prodInsert.SupplierId = prod.SupplierId;
                    prodInsert.CategoryId = prod.CategoryId;
                    prodInsert.QuantityPerUnit = prod.QuantityPerUnit;
                    prodInsert.UnitPrice = prod.UnitPrice;
                    prodInsert.UnitsInStock = prod.UnitsInStock;
                    prodInsert.UnitsOnOrder = prod.UnitsOnOrder;
                    prodInsert.ReorderLevel = prod.ReorderLevel;
                    prodInsert.Discontinued = prod.Discontinued;
                    prodInsert.Category = prod.Category;
                    prodInsert.Supplier = prod.Supplier;
                    prodInsert.OrderDetails = prod.OrderDetails;

                    db.Products.Add(prod);
                    db.SaveChanges();
                    return Ok("Update succeeded for product: " + prod.ProductName);
                }
                else
                {
                    return NotFound("Requested product couldn't be found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Delete request resulted an error: " + e);
            }


        }

        [HttpDelete]
        [Route("{id}")]
            public ActionResult DeleteCustomer(string id)
            {
                var prod = db.Products.Find(Convert.ToInt32(id));
                if (prod != null)
                {
                    try
                    {
                        db.Products.Remove(prod);
                        db.SaveChanges();
                        return Ok("Product " + prod.ProductName + " deleted.");
                    }
                    catch (Exception e)
                    {
                        return BadRequest("Delete action failed: " + e);
                    }
                }
                else
                {
                    return NotFound("Product " + id + " not found.");
                }
            }

            [HttpPut]
            [Route("{id}")]

            public ActionResult Update(string id, [FromBody] Product prod)
            {


            try
                {
                    Product prodInsert = db.Products.Find(Convert.ToInt32(id));

                    if (prodInsert != null)
                    {
                        prodInsert.ProductName = prod.ProductName;
                        prodInsert.SupplierId = prod.SupplierId;
                        prodInsert.CategoryId = prod.CategoryId;
                        prodInsert.QuantityPerUnit = prod.QuantityPerUnit;
                        prodInsert.UnitPrice = prod.UnitPrice;
                        prodInsert.UnitsInStock = prod.UnitsInStock;
                        prodInsert.UnitsOnOrder = prod.UnitsOnOrder;
                        prodInsert.ReorderLevel = prod.ReorderLevel;
                        prodInsert.Discontinued = prod.Discontinued;
                        prodInsert.Category = prod.Category;
                        prodInsert.Supplier = prod.Supplier;
                        prodInsert.OrderDetails = prod.OrderDetails;

                    db.SaveChanges();
                        return Ok("Update succeeded for product: " + prod.ProductName);
                    }
                    else
                    {
                        return NotFound("Requested product couldn't be found.");
                    }
                }
                catch (Exception e)
                {
                    return BadRequest("Update request resulted an error: " + e);
                }




            }

        }
}
