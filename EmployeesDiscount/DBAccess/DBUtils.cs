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

        public int CallRegistration(string firstname,string lastname,string email,string phone,string password)
        {
            try
            {
                con.Open(); ;
                MySqlCommand command = new MySqlCommand("register",con);
              
                command.CommandType = CommandType.StoredProcedure;
               
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@lastname", lastname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@password", password);
              
                int res= command.ExecuteNonQuery();
                con.Close();
                return res;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return 0;
            }
        }

        public string CallLogin(string username,string passwd)
        {
            try
            {
                con.Open(); ;
                MySqlCommand command = new MySqlCommand("login", con);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@passwd", passwd);

                MySqlParameter parOutput = command.Parameters.Add("@customerkey", MySqlDbType.Int16);  //定义输出参数  
                parOutput.Direction = ParameterDirection.Output;  //参数类型为Output  
               // MySqlParameter parReturn = new MySqlParameter("@return", SqlDbType.Int);
               // parReturn.Direction = ParameterDirection.ReturnValue;   //参数类型为ReturnValue                     
               // command.Parameters.Add(parReturn);
                
                 command.ExecuteNonQuery();
                string res = parOutput.Value.ToString();
                con.Close();
                return res;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return "0";
            }
        }
    }
}
