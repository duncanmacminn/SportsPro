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
        private IRepository<Technician> data { get; set; }

        public TechnicianController(IRepository<Technician> ctx)
        {
            data = ctx;
        }

        [Route("Technicians")]
        public IActionResult List()
        {
            var technicians = data.List(new QueryOptions<Technician>());
            return View(technicians);
        }
        
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
            var technician = data.Get(id);
            return View("AddEdit", technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                {
                    data.Insert(technician);
                    TempData["message"] = $"{technician.Name} was successfully added.";
                }
                else
                {
                    data.Update(technician);
                    TempData["message"] = $"{technician.Name} was successfully updated.";
                }
                data.Save();
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
            var technician = data.Get(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            data.Delete(technician);
            data.Save();
            return RedirectToAction("List", "Technician");
        }


    }

}
