using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Areas.Admin.Controllers
{
    //[Area("Admin")]
    public class ValidationController : Controller
    {
        private Repository<Customer> customerData { get; set; }
     

        public ValidationController(SportsProContext ctx)
        {
            customerData = new Repository<Customer>(ctx);
           
        }

        public JsonResult CheckEmail(string email)
        {
            string action = HttpContext.Session.GetString("action");
            int duplication;
            if (action == "Add") { duplication = 0; }
            else { duplication = 1; }

            var validate = new Validate(TempData);
            validate.CheckEmail(email, customerData, duplication);
            if (validate.IsValid)
            {
               validate.MarkEmailChecked();
               return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

      


    }
}