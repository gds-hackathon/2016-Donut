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
            var restanrantslist = webservice.GetRestaurant();
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

        // POST: /Restaurants/PaymentPost
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(PaymentViewModel model, string returnUrl)
        {
            var res = webservice.InsertPaymentTransaction(Convert.ToDouble(model.PaymentAmount), Convert.ToInt32(Session["Restaurantkey"]), Convert.ToInt32(Session["UserKey"]));
            //ViewData["restanrantslist"] = restanrantslist;
            if (res != 0)
            {
                return new RedirectResult("/Restaurants/Restaurants");
            }
            else
            {
                ModelState.AddModelError("", "Invalid order");
                return View(model);
            }
        }

        // GET: Orders
        [AllowAnonymous]
        public ActionResult Orders()
        {
            var orderlist = webservice.GetTransactionsPerUser(Convert.ToInt32(Session["UserKey"]));
            ViewData["orderlist"] = orderlist;
            return View();
        }

        public ActionResult Success()
        {
            return View("Success");
        }
    }
}