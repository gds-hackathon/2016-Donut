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

        // Get: /Restaurants/Payment
        [AllowAnonymous]
        public ActionResult Payment(int Id)
        {
            Session["Restaurantkey"] = Id;
            //var restanrantslist = webservice.GetRestaurant(10);
            //ViewData["restanrantslist"] = restanrantslist;
            return View();
        }

        // POST: /Restaurants/Payment
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentPost(int restaurantkey)
        {
            //var restanrantslist = webservice.GetRestaurant(10);
            //ViewData["restanrantslist"] = restanrantslist;
            return View("Restaurants");
        }
    }
}