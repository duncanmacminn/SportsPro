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
     public class ProductController : Controller
    {
        private IRepository<Product> data { get; set; }

        public ProductController(IRepository<Product> ctx) => data = ctx;

        [Route("Products")]
        public IActionResult List()
        {
            var productsOptions = new QueryOptions<Product>();
            return View(data.List(productsOptions));
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = data.Get(id);
            return View("AddEdit", product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    data.Insert(product);
                    TempData["message"] = $"{product.Name} was successfully added.";
                }
                else
                {
                    data.Update(product);
                    TempData["message"] = $"{product.Name} was successfully edited.";
                }
                data.Save();
                return RedirectToAction("List", "Product");

            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = data.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {

            TempData["message"] = $"{product.Name} was successfully deleted.";
            data.Delete(product);
            data.Save();
            return RedirectToAction("List", "Product");
        }


    }
    
}
