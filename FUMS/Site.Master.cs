using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FloraSoft.Cps.UserManager
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblwelcomeusername.Text = Request.Cookies["UserName"].Value + "<br/>" + Request.Cookies["EpxMsg"].Value;

            if (Request.Cookies["ChangePwdNow"].Value == "TRUE")
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                if(path.IndexOf("ChangePassword.aspx") == -1)
                    Response.Redirect("ChangePassword.aspx");
            }
        }
    }
}