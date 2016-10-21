using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesDiscount.DonutsWebService;
using EmployeesDiscount.Models;

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
        public ActionResult PaymentPost(PaymentViewModel model, string returnUrl)
        {
            var restanrantslist = webservice.InsertPaymentTransaction(Convert.ToDouble(model.PaymentAmount), Convert.ToInt32(Session["Restaurantkey"]), Convert.ToInt32(Session["UserKey"]));
            //ViewData["restanrantslist"] = restanrantslist;
            return View("Restaurants");
        }
    }
}