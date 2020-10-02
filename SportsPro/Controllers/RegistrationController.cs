using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.DataLayer;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }

        public RegistrationController(ISportsProUnitOfWork ctx)
        {
            data = ctx;
        }

        public ViewResult GetCustomer()
        {
            HttpContext.Session.Remove("sessionID");
            Customer activeCustomer = new Customer();
            Product activeProduct = new Product();
            var model = new RegistrationViewModel
            {
                ActiveCustomer = activeCustomer,
                Customers = data.Customers.List(new QueryOptions<Customer> ()),
                Products = data.Products.List(new QueryOptions<Product>())
            };
            
          
            return View(model);
        }

        [HttpGet]
        public IActionResult List(int customerId)
        {
            HttpContext.Session.SetInt32("sessionID", customerId);
            int? sessionID = HttpContext.Session.GetInt32("sessionID");
            if (customerId == 0 || sessionID == null)
            {
                TempData["message"] = "Please select a customer";
                return RedirectToAction("GetCustomer");
            }
            else
            {
                if (customerId == 0) customerId = (int)sessionID;
                var model = new RegistrationViewModel();
                model.ActiveCustomer = data.Customers.Get(customerId);
                model.Customers = data.Customers.List(new QueryOptions<Customer>());
                model.Products = data.Products.List(new QueryOptions<Product>());
                model.Registrations = data.Registrations.List(new QueryOptions<Registration>
                {
                    Includes = "Customer,Product",
                    WhereClauses = new WhereClauses<Registration>
                    {
                        {t => t.CustomerID == model.ActiveCustomer.CustomerID}

                    }


                });

                if (model.Registrations.Count == 0)
                    TempData["message"] = $"No products are registered for this customer.";

                return View(model);
            }
        }


        [HttpPost]
        public IActionResult Add(Registration registration)
        {
            int? sessionID = HttpContext.Session.GetInt32("sessionID");
            if (registration.ProductID == 0 && sessionID == null)
            {
                TempData["message"] = "Please select a product to register.";
                return RedirectToAction("List", "Registration");
            }
            else
            {
                data.Registrations.Insert(registration);
                data.Registrations.Save();
                return RedirectToAction("List", registration);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)

        {
            

            int? CustomerSessionID = HttpContext.Session.GetInt32("sessionID");

            var registrations = data.Registrations.Get(new QueryOptions<Registration>
            {
                Includes = "Customer,Product",
                WhereClauses = new WhereClauses<Registration>
                    {
                        {t => t.CustomerID == CustomerSessionID},
                        {g => g.ProductID == id}

                }


            });


            


            data.Registrations.Delete(registrations);

            data.Registrations.Save();

            return RedirectToAction("List", registrations);
        }

        //[HttpPost]
        //public IActionResult DeleteRegistration(int id)

        //{

        //    int? sessionID = HttpContext.Session.GetInt32("sessionID");

        //    var id2 = Request.Form["ProductID"];
        //    var registration = context.Registrations.Find(sessionID, id2);



        //    context.Registrations.Remove(registration);


        //    context.SaveChanges();

        //    return RedirectToAction("List",sessionID);
        //}



    }

}
