using System;
using System.Web.UI.WebControls;
namespace FloraSoft.Cps.UserManager
{
    public partial class TransactionLimit : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["ChangePwdNow"].Value == "TRUE")
            {
                Response.Redirect("~/ChangePassword.aspx");
            }
            if (Request.Cookies["RoleID"].Value != "99")
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTransactionLimit();
            }
        }
        private void BindTransactionLimit()
        {
            StatusDB DB = new StatusDB();
            MyDataGrid.DataSource = DB.GetTransactionLimit();
            MyDataGrid.DataBind();
        }

        protected void gvwTransLimit_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {
            Response.Redirect("~/PendingTransactionLimit.aspx");
        }

        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TransLimitDB db = new TransLimitDB();
            if (e.CommandName == "Cancel")
            {
                MyDataGrid.EditItemIndex = -1;
                BindTransactionLimit();
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid.EditItemIndex = e.Item.ItemIndex;
                BindTransactionLimit();
            }
            if (e.CommandName == "Insert")
            {
                TextBox txtLevelName = (TextBox)e.Item.FindControl("addLevelName");
                TextBox txtTransLimit = (TextBox)e.Item.FindControl("addTransLimit");
                TextBox txtLimitWord = (TextBox)e.Item.FindControl("addLimitWord");
                try
                {
                    double limit = Convert.ToDouble(txtTransLimit.Text);
                    db.InsertTransactionLimit(txtLevelName.Text, limit, txtLimitWord.Text);
                    lblErrMsg.Text = "Added successfully";
                    lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                    BindTransactionLimit();
                }
                catch 
                {
                    lblErrMsg.Text = "Incorrect transacton limit";
                    lblErrMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (e.CommandName == "Update")
            {
                int LevelID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];

                TextBox txtLevelName = (TextBox)e.Item.FindControl("LevelName");
                TextBox txtTransLimit = (TextBox)e.Item.FindControl("TransLimit");
                TextBox txtLimitWord = (TextBox)e.Item.FindControl("LimitWord");

                decimal translimit = 0;
                try
                {
                    translimit = Decimal.Parse(txtTransLimit.Text);
                }
                catch
                {
                }
                if (translimit != 0)
                {
                    db.UpdateTransactionLimit(LevelID, txtLevelName.Text, translimit, txtLimitWord.Text);

                    MyDataGrid.EditItemIndex = -1;
                    //lblErrMsg.Text = "Updated successfully";
                    //lblErrMsg.ForeColor = System.Drawing.Color.Blue;

                    BindTransactionLimit();
                }
            }
            if (e.CommandName == "Delete")
            {
                lblErrMsg.Text = "Deleted successfully";
                lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                BindTransactionLimit();
            }
        }
    }
}