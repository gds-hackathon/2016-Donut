using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesDiscount.DonutsWebService;

namespace EmployeesDiscount.Controllers
{
    public class RestautantsController : Controller
    {
        RestaurantS2GD webservice = new RestaurantS2GD();

        // GET: Restautants
        [AllowAnonymous]
        public ActionResult RestautantsList()
        {
            var restantantslist = webservice.GetRestaurant(10);
            ViewData["restantantslist"] = restantantslist;
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}