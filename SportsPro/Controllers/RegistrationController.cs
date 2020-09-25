﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsProContext context { get; set; }

        public RegistrationController(SportsProContext ctx)
        {
            context = ctx;
        }

        public ViewResult GetCustomer()
        {
            HttpContext.Session.Remove("sessionID");
            Customer activeCustomer = new Customer();
            Product activeProduct = new Product();
            var model = new RegistrationViewModel
            {
                ActiveCustomer = activeCustomer,
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList()
            };
            IQueryable<Customer> query = context.Customers;
            model.Customers = query.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult List(int customerId)
        {
            HttpContext.Session.SetInt32("sessionID", customerId);
            int? sessionID = HttpContext.Session.GetInt32("sessionID");
            if (customerId == 0 && sessionID == null)
            {
                TempData["message"] = "Please select a customer";
                return RedirectToAction("GetCustomer");
            }
            else
            {
                if (customerId == 0) customerId = (int)sessionID;
                var model = new RegistrationViewModel();
                model.ActiveCustomer = context.Customers.Find(customerId);
                model.Customers = context.Customers.ToList();
                model.Products = context.Products.ToList();

                IQueryable<Registration> query = context.Registrations;
                query = query.Where(p => p.CustomerID == model.ActiveCustomer.CustomerID);
                model.Registrations = query.ToList();

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
                context.Registrations.Add(registration);
                context.SaveChanges();
                return RedirectToAction("List", registration);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = context.Incidents.Find(id);
            ViewBag.Customers = context.Customers.OrderBy(c => c.FirstName).ToList();
            ViewBag.Products = context.Products.OrderBy(p => p.Name).ToList();
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            return RedirectToAction("List", "Incident");
        }


    }

}