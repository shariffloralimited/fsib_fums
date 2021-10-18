using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserMgr.DAC;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
namespace FloraSoft.Cps.UserManager
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected int UserID = 0;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["RoleCD"].Value != "UMMK")
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserID = Int32.Parse(Request.QueryString["UserID"]);
            }
            catch
            {
                Response.Redirect("Users.aspx");
            }
            if (!IsPostBack)
            {
                BindUser();
            }   
        }

        private void BindUser()
        {
            try
            {
                UserDB user = new UserDB();
                SqlDataReader dr = user.GetSingleUser(UserID);
                while (dr.Read())
                {
                    lblname.Text = dr["UserName"].ToString();
                    lblLoginID.Text = dr["LoginID"].ToString();
                }
                dr.Close();
                dr.Dispose();
            }
            catch
            {
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            PasswordPolicy pol = new PasswordPolicy();
            string result = pol.PasswordPolicyTest(txtPassword.Text, UserID);
            if (result == "OK")
            {
                UserDB user = new UserDB();

                user.ResetPassword(UserID, txtPassword.Text, Int32.Parse(Context.User.Identity.Name), Request.UserHostAddress);
                lblMsg.Text = "Password Successfully Reset.";
            }
            else
            {
                lblMsg.Text = result;
            }
        }
    }
}