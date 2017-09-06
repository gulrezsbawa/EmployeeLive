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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [WebMethod]
        public JsonResult GetData()
        {
            Products p = null;
            List<Products> ProductsList = new List<Products>();
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(@"SELECT  [ProductID],[Name],[UnitPrice],[Quantity] FROM [SignalRDemo].[dbo].[Products]", connection);
            command.Notification = null;
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            SqlDependency dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                p = new Products();
        p.id = reader.GetInt32(0);
                p.Name = reader.GetString(1);
                p.PricDecimal = reader.GetDecimal(2);
                p.QuantDecimal = reader.GetDecimal(3);
                ProductsList.Add(p);
            }
            connection.Close();
            return Json(ProductsList);
            
        }

    
        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            MyHub.Show();
        }
    }
}