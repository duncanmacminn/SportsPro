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

        public JsonResult CheckEmail(int customerID)
        {
            var validate = new Validate(TempData);
            validate.CheckEmail(customerID, customerData);
            if (validate.IsValid)
            {
               // validate.MarkGenreChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

      


    }
}