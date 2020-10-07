using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;
using SportsPro.DataLayer;

namespace SportsPro.ViewComponents
{
    public class CopyrightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var timeNow = DateTime.Now;

            return View(timeNow);       
        }
    } 
} 
