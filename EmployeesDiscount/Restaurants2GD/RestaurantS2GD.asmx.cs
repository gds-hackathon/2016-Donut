using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Restaurants2GD.Models;
using DBAccess;
using System.Data;


namespace Restaurants2GD
{
    /// <summary>
    /// RestaurantS2GD 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class RestaurantS2GD : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}


        //[WebMethod]
        //public List<User> GetUsers()
        //{
           
        //    List<User> ls_user = new List<User>();
        //    DBUtils du = new DBUtils();
        //    DataTable dt= du.GetTableByQuery("select * from user");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        User user = new User();
        //        user.UserName = dr["userName"].ToString();
        //        ls_user.Add(user);
        //    }

        //    return ls_user;
        //}

        [WebMethod]
        public int Registration(string firstname, string lastname, string email, string phone, string password)
        {
            DBUtils du = new DBUtils();
            int res = du.CallRegistration(firstname, lastname, email, phone, password);
            return res;
        }

        [WebMethod]
        public Cusomer Login(string userName, string passwd)
        {
            Cusomer customer = new Cusomer();
            DBUtils du = new DBUtils();
            DataTable dt = du.CallLogin(userName,passwd);
            foreach (DataRow dr in dt.Rows)
            {
                customer.FirstName = dr["FirstName"].ToString();
                customer.LastName = dr["LastName"].ToString();
                customer.Email = dr["Email"].ToString();
                customer.Mobile = dr["Mobile"].ToString();
                customer.BalanceAmount = double.Parse(dr["Balance"].ToString());
                customer.FrozenAmount = double.Parse(dr["FrozenAmount"].ToString());
            }
            return customer;
        }

        [WebMethod]
        public List<Restaurant> GetRestaurant(int count)
        {
            DBUtils du = new DBUtils();
           DataTable dt= du.CallGetRestaurantList(count);
            List<Restaurant> res_ls = new List<Restaurant>();
            foreach (DataRow dr in dt.Rows)
            {
                Restaurant res = new Restaurant();               
                res.Name =dr["name"].ToString();
                res.Discount = dr["discount"].ToString();
                res_ls.Add(res);
            }
            return  res_ls;
        }

        [WebMethod]
        public TransactionList GetTransactionsPerRestaurant(int restaurantKey, ref int count)
        {           
            DBUtils du = new DBUtils();
            DataTable  dt= du.CallGetTransactionsPerRestaurant(restaurantKey,ref count);
            List<Transaction> res_ls = new List<Transaction>();
            foreach (DataRow dr in dt.Rows)
            {
                Transaction res = new Transaction();
                res.TransactionNumber = dr["TransactionNumber"].ToString();
                res.RestaurantName = dr["RestaurantName"].ToString();
                res.EmployeeName = dr["EmployeeName"].ToString();
                res.Amount = dr["Amount"].ToString();
                res.CreateDate= dr["CreateDate"].ToString();
                res_ls.Add(res);
            }
            TransactionList trans_ls = new TransactionList();
            trans_ls.Trans = res_ls;
            trans_ls.ReturnCount = count;
            return trans_ls;
        }
    }
}
