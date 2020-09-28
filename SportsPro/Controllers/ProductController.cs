﻿using System;
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
        private Repository<Product> data { get; set; }
        public ProductController(SportsProContext ctx) => data = new Repository<Product>(ctx); // tried changing to match ch15 soln, did not work??
        
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

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    ViewBag.Action = "Edit";
        //    var product = context.Products.Find(id);
        //    return View("AddEdit", product);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (product.ProductID == 0)
        //        {
        //            context.Products.Add(product);
        //            TempData["message"] = $"{product.Name} was successfully added.";
        //        }
        //        else
        //        {
        //            context.Products.Update(product);
        //            TempData["message"] = $"{product.Name} was successfully edited.";
        //        }
        //        context.SaveChanges();
        //        return RedirectToAction("List", "Product");

        //    }
        //    else
        //    {
        //        ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
        //        return View(product);
        //    }
        //}

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var product = context.Products.Find(id);
        //    return View(product);
        //}

        //[HttpPost]
        //public IActionResult Delete(Product product)
        //{

        //    TempData["message"] = $"{product.Name} was successfully deleted.";
        //    context.Products.Remove(product);
        //    context.SaveChanges();
        //    return RedirectToAction("List", "Product");
        //}

        
    }
    
}
