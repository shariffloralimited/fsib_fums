using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using FloraSoft.Cps.UserManager;
using System.Configuration;

namespace FloraSoft.Cps.UserManager
{
    public class AppVariable
    {
        public static bool IsConnected;
        public static string ServerLogin = "server=" + ConfigurationManager.AppSettings["DBServer"] + ";database=ACHUser;uid=floraweb;pwd=platinumfloor967";
    }
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            //AuthConfig.RegisterOpenAuth();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }
    }
}
