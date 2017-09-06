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
    public class ChatRoomController : Controller
    {
        // GET: ChatRoom
        public ActionResult Index()
        {
            
            return View();
        }

        [WebMethod]
        public JsonResult GetData()
        {
            ChatLog p = null;
            List<ChatLog> ChatLogList = new List<ChatLog>();
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(@"SELECT  [ChatID],[SenderName],[Message],[RecieverName] FROM [SignalRDemo].[dbo].[ChatLog] where [RecieverName]='Pramod' order by CreatedDate", connection);
            command.Notification = null;
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            SqlDependency dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange2);

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                p = new ChatLog();
                p.ChatID = reader.GetInt32(0);
                p.SenderName = reader.GetString(1);
                p.Message = reader.GetString(2);
                p.RecieverName = reader.GetString(3);
                ChatLogList.Add(p);
            }
            connection.Close();
            return Json(ChatLogList);

        }

        [HttpPost]
        public int SendMessage(string Message)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(@"insert into [SignalRDemo].[dbo].[ChatLog] values('Pramod','"+ Message + "','Pramod',GetDate())", connection);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
           SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

            }
                return 1;
        }


        private static void dependency_OnChange2(object sender, SqlNotificationEventArgs e)
        {
            ChatHub.Show2();
        }
    }
}