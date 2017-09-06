using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Services;
using EmployeeLive.Controllers;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EmployeeLive
{
    
    public class PrivateHub : Hub
    {
        public static void Show3()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PrivateHub>();
            context.Clients.All.displayStatus();
        }
        public override Task OnConnected()
        {

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {


            return base.OnDisconnected(stopCalled);
        }
    }
}