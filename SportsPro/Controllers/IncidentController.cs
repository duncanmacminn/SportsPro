using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.DataLayer;
using Microsoft.EntityFrameworkCore.Query;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }

        public IncidentController(ISportsProUnitOfWork unit)
        {
            data = unit;
        }

        [Route("Incidents")]
        public ViewResult List()
        {
            string? FilterString = HttpContext.Session.GetString("FilterString");
            var IncidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };
            if (FilterString == "unassigned") { IncidentOptions.Where = i => i.TechnicianID == null; };
            if (FilterString == "open") { IncidentOptions.Where = i => i.DateClosed == null; };
            var incidents = data.Incidents.List(IncidentOptions);

            return View(incidents);
        }
        
        [HttpGet]
        public ViewResult Add()
        {
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var incidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };
            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();
            var model = new IncidentAddEditViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = data.Incidents.List(incidentOptions),
                Technicians = data.Technicians.List(technicianOptions),
                Customers = data.Customers.List(customerOptions),
                Products = data.Products.List(productOptions),
                Action = "Add"
            };

            return View("AddEdit", model);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var incidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };
            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();
            var model = new IncidentAddEditViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = data.Incidents.List(incidentOptions),
                Technicians = data.Technicians.List(technicianOptions),
                Customers = data.Customers.List(customerOptions),
                Products = data.Products.List(productOptions),
                Action = "Edit"
            };
            model.ActiveIncident = data.Incidents.Get(id);
            return View("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ActiveIncident.IncidentID == 0)
                    data.Incidents.Insert(model.ActiveIncident);
                else
                    data.Incidents.Update(model.ActiveIncident);
                data.Incidents.Save();
                return RedirectToAction("List", "Incident");
            }
            else
            {
                ViewBag.Action = model.ActiveIncident.IncidentID == 0 ? "Add" : "Edit";
                return View("AddEdit", model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = data.Incidents.Get(id);
            //ViewBag.Customers = data.Customers.List(new QueryOptions<Customer>());
            //ViewBag.Products = data.Products.List(new QueryOptions<Product>());
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            data.Incidents.Delete(incident);
            data.Incidents.Save();
            return RedirectToAction("List", "Incident");
        }

        private void LoadViewBag(string operation)
        {
            ViewBag.Customers = data.Customers.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            });
            ViewBag.Products = data.Products.List(new QueryOptions<Product>
            {
                OrderBy = p => p.Name
            });
            ViewBag.Incidents = data.Incidents.List(new QueryOptions<Incident>
            {
                OrderBy = i => i.Title
            });
            ViewBag.Operation = operation;
        }

        public IActionResult FilterAll()
        {
            HttpContext.Session.SetString("FilterString", "null");
            return RedirectToAction("List");
        }

        public IActionResult FilterUnassigned()
        {
            HttpContext.Session.SetString("FilterString", "unassigned");
            return RedirectToAction("List");
        }
        
        public IActionResult FilterOpen()
        {
            HttpContext.Session.SetString("FilterString", "open");
            return RedirectToAction("List");
        }

    }
}
