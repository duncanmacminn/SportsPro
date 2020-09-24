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
    public class IncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public IncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Incidents")]
        public ViewResult List()
        {
            string? FilterString = HttpContext.Session.GetString("FilterString");
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
            if (FilterString != null)
            {
                if (FilterString == "unassigned")
                    query = query.Where(i => i.TechnicianID == null);
                if (FilterString == "open")
                    query = query.Where(i => i.DateClosed == null);
            }
            model.Incidents = query.ToList();
            return View(model);
        }

        [HttpGet]
        public ViewResult Add()
        {
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var model = new IncidentAddEditViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = context.Incidents.ToList(), //not sure if necessary
                Technicians = context.Technicians.ToList(),
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList(),
                Action = "Add"
            };
            IQueryable<Incident> query = context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(i => i.IncidentID == activeIncident.IncidentID);
            if (activeTechnician.TechnicianID != 0)
                query = query.Where(i => i.Technician.TechnicianID == activeTechnician.TechnicianID);
            model.Incidents = query.ToList();
            return View("AddEdit", model);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var model = new IncidentAddEditViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = context.Incidents.ToList(),
                Technicians = context.Technicians.ToList(),
                Customers = context.Customers.ToList(),
                Products = context.Products.ToList(),
                Action = "Edit"
            };
            IQueryable<Incident> query = context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(i => i.IncidentID == activeIncident.IncidentID);
            if (activeTechnician.TechnicianID != 0)
                query = query.Where(i => i.Technician.TechnicianID == activeTechnician.TechnicianID);
            model.Incidents = query.ToList();
            model.ActiveIncident = context.Incidents.Find(id);
            return View("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ActiveIncident.IncidentID == 0)
                    context.Incidents.Add(model.ActiveIncident);
                else
                    context.Incidents.Update(model.ActiveIncident);
                context.SaveChanges();
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
