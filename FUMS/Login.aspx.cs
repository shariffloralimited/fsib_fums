using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Web.Security;
using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserManager.BLL.Objects;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace FloraSoft.Cps.UserManager
{
    public partial class Login : System.Web.UI.Page
    {
        public bool IsAuthenticated(string srvr, string usr, string pwd)
        {
            bool authenticated = false;
            try
            {
                using (PrincipalContext Context = new PrincipalContext(ContextType.Domain, srvr))
                {
                    if (Context == null)
                    {
                        authenticated = false;
                        MyMessage.Text = "Login failed: Please try again (AD)";
                    }
                    else
                    {
                        authenticated = Context.ValidateCredentials(usr, pwd);
                        //authenticated = true;
                    }
                    MyMessage.Text = "";
                }
            }
            catch (DirectoryServicesCOMException cex)
            {
                MyMessage.Text = "Error: " + cex.Message;
            }
            catch (Exception ex)
            {
                MyMessage.Text = "Error: " + ex.Message;
            }
            return authenticated;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string UserID = "0";

            UserDB db = new UserDB();
            UserInfo uinfo = new UserInfo();

            string ADLogin = ConfigurationManager.AppSettings["ADLogin"].ToUpper();
            if (ADLogin != "TRUE")
            {
                uinfo = db.Login(txtUserID.Text, txtPass.Text, Request.UserHostAddress);
                UserID = uinfo.UserID;
            }
            else
            {
                if (IsAuthenticated(ConfigurationManager.AppSettings["ADServer"], txtUserID.Text, txtPass.Text))
                {
                    uinfo = db.ADLogin(txtUserID.Text, Request.UserHostAddress);
                    UserID = uinfo.UserID;

                }
            }

            if (UserID == "0")
            {
                string LoginTries = Tried.Value;
                if (LoginTries == "")
                {
                    LoginTries = "0";
                }
                int NewVal = Int32.Parse(LoginTries) + 1;
                Tried.Value = NewVal.ToString();
                if (NewVal > 2)
                {
                    db.LockUser(txtUserID.Text.Trim());
                    MyMessage.Text = txtUserID.Text + " account has been locked.";
                }
                else
                {
                    MyMessage.Text = uinfo.ExpMsg;
                }
            }
            else
            {
                FormsAuthentication.SetAuthCookie(UserID, false);

                Response.Cookies["UserName"].Value      = uinfo.UserName;
                Response.Cookies["RoleName"].Value      = uinfo.RoleName;
                Response.Cookies["BranchName"].Value    = uinfo.BranchName;
                Response.Cookies["LoginID"].Value       = txtUserID.Text;
                Response.Cookies["RoleID"].Value        = uinfo.RoleID;
                Response.Cookies["RoleCD"].Value        = uinfo.RoleCD;
                Response.Cookies["UserID"].Value      = uinfo.UserID;
                Response.Cookies["BranchID"].Value      = uinfo.BranchID;
                Response.Cookies["RoutingNo"].Value     = uinfo.RoutingNo;
                Response.Cookies["BranchName"].Value    = uinfo.BranchName;
                Response.Cookies["DaysPassed"].Value    = uinfo.DaysPassed;
                Response.Cookies["ChangePwdNow"].Value  = uinfo.ChangePwdNow;
                Response.Cookies["EpxMsg"].Value        = uinfo.ExpMsg;

                if (uinfo.ChangePwdNow == "TRUE")
                {
                    Response.Redirect("ChangePassword.aspx");
                }

                Response.Redirect("Default.aspx");
            }
        }
    }
}

