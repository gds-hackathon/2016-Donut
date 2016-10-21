using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace DBAccess
{
    public class DBUtils
    {
        private MySqlConnection con;

        public DBUtils(String connectionString)
        {
            con = new MySqlConnection(connectionString);
        }

        public DBUtils()
        {
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
        }
        public DataTable GetTableByQuery(String queryString)
        {
            MySqlCommand cmd;

            try
            {
                con.Open();
                Console.WriteLine(String.Format("Conneted to" + con.ConnectionString));
            }

            catch (Exception e)
            {
                throw new Exception("Connection Failed to " + con, e);
            }

            try
            {
                Console.WriteLine(" -- Exceuting Query: " + queryString + " --");
                cmd = new MySqlCommand(queryString, con);
                cmd.CommandTimeout = 360;
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                con.Close();
                da.Fill(ds);
                return ds.Tables[0];
            }

            catch (Exception e)
            {
                throw new Exception("Data Not Found" + con, e);
            }


        }

        public int UpdateOrInsertTableByQuery(string cmdstring)
        {
            MySqlCommand cmd;
            int excuteResult;

            try
            {
                con.Open();
                Console.WriteLine(String.Format("Conneted to" + con.ConnectionString));
            }

            catch (Exception e)
            {
                throw new Exception("Connection Failed to " + con, e);
            }

            try
            {
                Console.WriteLine(" -- Exceuting Query: " + cmdstring + " --");
                cmd = new MySqlCommand(cmdstring, con);
                excuteResult = cmd.ExecuteNonQuery();
                con.Close();
                return excuteResult;
            }

            catch (Exception e)
            {
                throw new Exception("Data Not Found" + con, e);
            }
        }

        public int CallRegistration(string firstname, string lastname, string email, string phone, string password)
        {
            try
            {
                con.Open(); ;
                MySqlCommand command = new MySqlCommand("register", con);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@lastname", lastname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@password", password);

                int res = command.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return 0;
            }
        }

        public DataTable CallLogin(string username, string passwd)
        {
            try
            {
                con.Open(); ;
                MySqlCommand command = new MySqlCommand("login", con);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@passwd", passwd);

                MySqlParameter parInOutput = command.Parameters.Add("@customerkey", MySqlDbType.Int16);  //定义输出参数  
                parInOutput.Direction = ParameterDirection.InputOutput;
                parInOutput.Value = 0;


                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt); ;
                con.Close();
                return dt;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }

        public DataTable CallGetRestaurantList(int count)
        {

            con.Open();
            MySqlCommand cmd = new MySqlCommand("getRestaurantList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlParameter parInOutput = cmd.Parameters.Add("@count", MySqlDbType.Int16);  //定义输出参数  
            parInOutput.Direction = ParameterDirection.InputOutput;  //参数类型为Output  
            parInOutput.Value = count;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;

        }

        public DataTable CallGetTransactionsPerRestaurant(int restuarantkey, ref int count)
        {

            con.Open();
            MySqlCommand cmd = new MySqlCommand("getTransactionsPerRestaurant", con);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlParameter parIn1 = cmd.Parameters.Add("@restaurantKey", MySqlDbType.Int16);
            parIn1.Direction = ParameterDirection.Input;
            parIn1.Value = restuarantkey;
            MySqlParameter parIn2 = cmd.Parameters.Add("@count", MySqlDbType.Int16);
            parIn2.Direction = ParameterDirection.InputOutput;
            parIn2.Value = count;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            count = int.Parse(parIn2.Value.ToString());
            return dt;

        }



        public DataTable CallGetTransactionsPerUser(int restuarantkey, ref int count)
        {

            con.Open();
            MySqlCommand cmd = new MySqlCommand("getTransactionsPerUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlParameter parIn1 = cmd.Parameters.Add("@CustomerKey", MySqlDbType.Int16);
            parIn1.Direction = ParameterDirection.Input;
            parIn1.Value = restuarantkey;
            MySqlParameter parIn2 = cmd.Parameters.Add("@count", MySqlDbType.Int16);
            parIn2.Direction = ParameterDirection.InputOutput;
            parIn2.Value = count;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            count = int.Parse(parIn2.Value.ToString());
            return dt;

        }

        public int CallPaymentTransaction(double amount, int restaurantkey, int customerkey)
        {

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter parIn1 = cmd.Parameters.Add("@amount", MySqlDbType.Double);
                parIn1.Direction = ParameterDirection.Input;
                parIn1.Value = amount;
                MySqlParameter parIn2 = cmd.Parameters.Add("@restaurantkey", MySqlDbType.Int16);
                parIn2.Direction = ParameterDirection.Input;
                parIn2.Value = restaurantkey;

                MySqlParameter parIn3 = cmd.Parameters.Add("@customerkey", MySqlDbType.Int16);
                parIn3.Direction = ParameterDirection.Input;
                parIn3.Value = customerkey;

                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return 0;

            }


        }

        public int CallInsertRestaurant(string name, string discount)
        {

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertRestaurant", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter parIn1 = cmd.Parameters.Add("@name", MySqlDbType.String);
                parIn1.Direction = ParameterDirection.Input;
                parIn1.Value = name;
                MySqlParameter parIn2 = cmd.Parameters.Add("@discount", MySqlDbType.String);
                parIn2.Direction = ParameterDirection.Input;
                parIn2.Value = discount;

                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return 0;

            }


        }
    }
}
