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
    public class TechIncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public TechIncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        public ViewResult Get()
        {
            HttpContext.Session.Remove("sessionID");
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var model = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = context.Incidents.ToList(),
                Technicians = context.Technicians.ToList(),
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList()
            };
            IQueryable<Incident> query = context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(i => i.IncidentID == activeIncident.IncidentID);
            if (activeTechnician.TechnicianID != 0)
                query = query.Where(i => i.Technician.TechnicianID == activeTechnician.TechnicianID);
            model.Incidents = query.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult List(int technicianId)
        {
            int? sessionID = HttpContext.Session.GetInt32("sessionID");
            if (technicianId == 0 && sessionID == null)
            {
                TempData["message"] = "Please select a technician";
                return RedirectToAction("Get");
            }
            else
            {
                if (technicianId == 0) technicianId = (int)sessionID;
                var model = new IncidentViewModel();
                model.ActiveTechnician = context.Technicians.Find(technicianId);
                model.Customers = context.Customers.ToList();
                model.Products = context.Products.ToList();

                IQueryable<Incident> query = context.Incidents;
                query = query.Where(i => i.TechnicianID == model.ActiveTechnician.TechnicianID);
                query = query.Where(i => i.DateClosed == null);
                model.Incidents = query.ToList();

                if (model.Incidents.Count == 0)
                    TempData["message"] = $"No open incidents for this technician.";

                return View(model);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Incident activeIncident = context.Incidents.Find(id);
            var model = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                Incidents = context.Incidents.ToList(),
                Technicians = context.Technicians.ToList(),
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList(),
                Action = "Edit"
            };
            IQueryable<Incident> query = context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(i => i.IncidentID == activeIncident.IncidentID);
            model.Incidents = query.ToList();
            model.ActiveIncident = context.Incidents.Find(id);
            HttpContext.Session.SetInt32("sessionID", (int)model.ActiveIncident.TechnicianID);
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = $"Incident updated successfully.";
                context.Incidents.Update(model.ActiveIncident);
                context.SaveChanges();
                return RedirectToAction("List", "TechIncident");
            }
            else
            {
                model.ActiveIncident = context.Incidents.Find(model.ActiveIncident.IncidentID);
                return View("Edit", model);
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
