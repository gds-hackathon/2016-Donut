using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesDiscount.DonutsWebService;

namespace EmployeesDiscount.Controllers
{
    public class RestaurantsController : Controller
    {
        RestaurantS2GD webservice = new RestaurantS2GD();

        // GET: Restautants
        [AllowAnonymous]
        public ActionResult Restaurants()
        {
            var restanrantslist = webservice.GetRestaurant(10);
            ViewData["restanrantslist"] = restanrantslist;
            return View("Restaurants");
        }

        // POST: /Restaurants/Payment
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Payment()
        {
            //var restanrantslist = webservice.GetRestaurant(10);
            //ViewData["restanrantslist"] = restanrantslist;
            return View("Restaurants");
        }
    }
}