using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserMgr.DAC;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using System.Web;
namespace FloraSoft.Cps.UserManager
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private void Signout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (NewPassword.Text != ConfirmPassword.Text)
                {
                    lblMsg.Text = "New password was not confirmed.";
                }
                else
                {
                    PasswordPolicy policy = new PasswordPolicy();
                    string result = policy.PasswordPolicyTest(NewPassword.Text, Int32.Parse(Context.User.Identity.Name));
                    if (result == "OK")
                    {
                        UserDB db = new UserDB();
                        int Status = db.ChangePassword(Int32.Parse(Context.User.Identity.Name), db.Encrypt(OldPassword.Text), db.Encrypt(NewPassword.Text), Request.UserHostAddress);
                        if (Status == 1)
                        {
                            Response.Cookies["ChangePwdNow"].Value = "FALSE";
                            Signout();
                            btnUpdate.Visible = false;

                            lblMsg.Text = "Password Successfully Changed. Please log back in again.";

                        }
                        if (Status == 0)
                        {
                            lblMsg.Text = "Old Password was not correct.";
                        }
                    }
                    else
                    {
                        lblMsg.Text = result;
                    }
                }
            }
        }
    }
}