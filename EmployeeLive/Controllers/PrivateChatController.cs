using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace EmployeeLive.Controllers
{
    public class PrivateChatController : Controller
    {
        // GET: PrivateChat
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrivateChat(string RecieverName)
        {
            Session["RecieverName"] = RecieverName;
            Session["SenderName"] = "Pramod";

            return View();
        }

        [WebMethod]
        public JsonResult GetData()
        {
            PrivateLog p = null;
            List<PrivateLog> PrivateLogList = new List<PrivateLog>();
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(@"GetPrivateChat", connection);
            command.CommandType= CommandType.StoredProcedure;
            command.Notification = null;
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            SqlDependency dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange3);

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                p = new PrivateLog();
                p.Name = reader.GetString(0);
                p.Message = reader.GetString(1);
                p.CreatedDate = reader.GetDateTime(2);

                PrivateLogList.Add(p);
            }
            connection.Close();
            return Json(PrivateLogList);

        }

        [HttpPost]
        public int SendMessage(string Message)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(@"insert into [SignalRDemo].[dbo].[ChatLog] values('"+Session["SenderName"].ToString()+"','" + Message + "','"+ Session["RecieverName"].ToString() + "',GetDate())", connection);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

            }
            return 1;
        }


        private static void dependency_OnChange3(object sender, SqlNotificationEventArgs e)
        {
            PrivateHub.Show3();
        }
    }
}