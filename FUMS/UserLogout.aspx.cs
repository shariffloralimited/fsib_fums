using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserMgr.DAC;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
namespace FloraSoft.Cps.UserManager
{
    public partial class UserLogout : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["RoleCD"].Value != "UMMK")
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
            try
            {
                int ModuleID = Int32.Parse(Request.QueryString["ModuleID"]);
                int UserID = Int32.Parse(Request.QueryString["UserID"]);

                UserDB user = new UserDB();
                user.UserLogout(UserID, ModuleID);
            }
            catch
            {
            }
            Response.Redirect("Users.aspx");
        }
    }
}