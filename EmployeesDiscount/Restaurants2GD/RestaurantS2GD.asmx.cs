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

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public List<User> GetUsers()
        {
           
            List<User> ls_user = new List<User>();
            DBUtils du = new DBUtils();
            DataTable dt= du.GetTableByQuery("select * from user");
            foreach (DataRow dr in dt.Rows)
            {
                User user = new User();
                user.UserName = dr["userName"].ToString();
                ls_user.Add(user);
            }

            return ls_user;
        }

        [WebMethod]
        public int Registration(string firstname, string lastname, string email, string phone, string password)
        {
            DBUtils du = new DBUtils();
            int res = du.CallRegistration(firstname, lastname, email, phone, password);
            return res;
        }

        [WebMethod]
        public string Login(string userName, string passwd)
        {
            DBUtils du = new DBUtils();
            string res = du.CallLogin(userName,passwd);
            return res;
        }
    }
}
