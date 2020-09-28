using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.DataLayer;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private SportsProContext context { get; set; }

        public TechnicianController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Technicians")]
        public IActionResult List()
        {
            var technicians = context.Technicians.ToList();
            return View(technicians);
        }
        //public ViewResult List(string activeIncident = "All", string activeTechnician = "All")
        //{
        //    var model = new IncidentViewModel
        //    {
        //        ActiveIncident = activeIncident,
        //        ActiveTechnicain = activeTechnician,
        //        Incidents = context.Incidents.ToList(),
        //        Technicians = context.Technicians.ToList()
        //    };
        //    IQueryable<Incident> query = context.Incidents;
        //    if (activeIncident != "All")
        //        query = query.Where(i => i.IncidentID.ToString() == activeIncident);
        //    if (activeTechnician != "All")
        //        query = query.Where(i => i.Technician.TechnicianID.ToString() == activeTechnician);
        //    model.Incidents = query.ToList();
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Technician());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var technician = context.Technicians.Find(id);
            return View("AddEdit", technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                {
                    context.Technicians.Add(technician);
                }
                else
                    context.Technicians.Update(technician);
                context.SaveChanges();
                return RedirectToAction("List", "Technician");
            }
            else
            {
                ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
                return View(technician);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            context.Technicians.Remove(technician);
            context.SaveChanges();
            return RedirectToAction("List", "Technician");
        }


    }

}
