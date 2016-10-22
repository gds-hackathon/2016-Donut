using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesDiscount.DonutsWebService;
using EmployeesDiscount.Models;

namespace EmployeesDiscount.Controllers
{
    //[Authorize]
    public class RestaurantsController : Controller
    {
         
        RestaurantS2GD webservice = new RestaurantS2GD();
        //public RestaurantController() {}
        // GET: Restautants
        //[AllowAnonymous]
        public ActionResult Restaurants()
        {
            var restanrantslist = webservice.GetRestaurant();
            ViewData["restanrantslist"] = restanrantslist;
            return View("Restaurants");
        }

        [HttpPost]
        public ActionResult Restaurant(RestautantViewModel model)
        {
            var res = webservice.InsertRestaurant(model.RestautantName, model.Discount);
            ViewData["res"] = res;
            return new RedirectResult("/Restaurants/Restaurants");
        }

        [HttpGet]
        public ActionResult Restaurant()
        {
            var restanrantslist = webservice.GetRestaurant();
            ViewData["restanrantslist"] = restanrantslist;
            return View("Restaurant");
            //return View();
        }

        // Get: /Restaurants/Payment
        //[AllowAnonymous]
        public ActionResult Payment(int Id)
        {
            Session["Restaurantkey"] = Id;
            //var restanrantslist = webservice.GetRestaurant(10);
            //ViewData["restanrantslist"] = restanrantslist;
            return View();
        }

        // POST: /Restaurants/PaymentPost
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(PaymentViewModel model, string returnUrl)
        {
            if (Session["UserKey"] != null)
            {
                var res = webservice.InsertPaymentTransaction(Convert.ToDouble(model.PaymentAmount), Convert.ToInt32(Session["Restaurantkey"]), Convert.ToInt32(Session["UserKey"]));
                //ViewData["restanrantslist"] = restanrantslist;
                if (res != 0)
                {
                    return View("Success");
                    //return new RedirectResult("/Restaurants/Success");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid order");
                    return View(model);
                }
            }
            else
            {
                return new RedirectResult("/Account/Login");
            }
        }

        // GET: Orders
        //[AllowAnonymous]
        public ActionResult Orders()
        {
            if (Session["UserKey"] != null)
            {
                var orderlist = webservice.GetTransactionsPerUser(Convert.ToInt32(Session["UserKey"]));
                ViewData["orderlist"] = orderlist;
                return View();
            }
            else
            {
                return new RedirectResult("/Account/Login");
            }
        }

        [HttpPost]
        public ActionResult Settlement(int id)
        {
            var res = webservice.GetTransactionsPerRestaurant(id);
            ViewData["SettlementList"] = res;
            return View();
        }

        public ActionResult Success()
        {
            return View("Success");
        }
    }
}