using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FloraSoft.Cps.UserManager;

namespace FloraSoft.Cps.UserMgr
{
    public partial class SubBranch : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["ChangePwdNow"].Value == "TRUE")
            {
                Response.Redirect("~/ChangePassword.aspx");
            }
            //if (Request.Cookies["RoleID"].Value != "99")
            //{
            //    Response.Redirect("~/AccessDenied.aspx");
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            SubBranchDB db = new SubBranchDB();
            MyDataGrid.DataSource = db.GetAllSubBranches();
            MyDataGrid.DataBind();
            if (Request.Cookies["RoleID"].Value != "99")
            {
                MyDataGrid.ShowFooter = false;
                //for(int i = 0; i < MyDataGrid.Items.Count; i++)
                //{
                //    MyDataGrid.Items[i].Cells[0].Text = "";
                //}
            }
        }

        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            SubBranchDB db = new SubBranchDB();
            if (e.CommandName == "Cancel")
            {
                MyDataGrid.EditItemIndex = -1;
                BindData();
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid.EditItemIndex = e.Item.ItemIndex;
                BindData();
                if (Request.Cookies["RoleID"].Value != "99")
                {
                    //MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[1].Enabled = false;
                    //MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[2].Enabled = false;
                    //MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[3].Enabled = false;
                }
            }
            if (e.CommandName == "Insert")
            {
                if (Request.Cookies["RoleID"].Value != "99")
                {
                    Response.Redirect("~/AccessDenied.aspx");
                }
                lblErrMsg.ForeColor = System.Drawing.Color.Red;

                TextBox txtBranchName = (TextBox)e.Item.FindControl("addSubBranchName");
                TextBox txtBranchCD = (TextBox)e.Item.FindControl("addSubBranchCD");
                DropDownList ddlRoutingNo = (DropDownList)e.Item.FindControl("addDdlBranch");

                if (txtBranchName.Text.Trim() == "")
                {
                    lblErrMsg.Text = "Branch Name is missing.";
                    return;
                }

                if (txtBranchCD.Text.Trim() == "")
                {
                    lblErrMsg.Text = "Branch CD is missing.";
                    return;
                }
                string RoleCD = Request.Cookies["RoleCD"].Value;
                string dbMessage = string.Empty;
                int n = db.InsertSubBranch(txtBranchName.Text, txtBranchCD.Text, ddlRoutingNo.Text, RoleCD, out dbMessage);
                lblErrMsg.Text = dbMessage;
                if (n > 0)
                {
                    lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                    BindData();
                }
                else
                {
                    lblErrMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            if (e.CommandName == "Update")
            {
                lblErrMsg.ForeColor = System.Drawing.Color.Red;

                int subBranchID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];

                TextBox txtSubBranchName = (TextBox)e.Item.FindControl("SubBranchName");
                DropDownList ddlRoutingNo = (DropDownList)e.Item.FindControl("editDdlBranch");
                TextBox txtBranchCD = (TextBox)e.Item.FindControl("SubBranchCD");

                if (txtSubBranchName.Text.Trim() == "")
                {
                    lblErrMsg.Text = "Sub Branch Name is missing.";
                    return;
                }
                if (txtBranchCD.Text.Trim() == "")
                {
                    lblErrMsg.Text = "Branch CD is missing.";
                    return;
                }
                string RoleCD = Request.Cookies["RoleCD"].Value;
                string LastEditingUser = Request.Cookies["UserName"].Value;
                string dbMessage = string.Empty;
                db.UpdateSubBranch(subBranchID, txtSubBranchName.Text, txtBranchCD.Text, ddlRoutingNo.Text, RoleCD, out dbMessage);

                lblErrMsg.Text = dbMessage;
                lblErrMsg.ForeColor = System.Drawing.Color.Red;
                if (dbMessage.ToLower().Contains("success"))
                {
                    MyDataGrid.EditItemIndex = -1;
                    lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                    BindData();
                }
            }
            if (e.CommandName == "Delete")
            {
                lblErrMsg.Text = "Deleted successfully";
                lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                BindData();
            }
        }
        private void FailedMessage()
        {
            lblErrMsg.Text = "Failed to upload invalid file. Please upload valid excel file";
            lblErrMsg.ForeColor = System.Drawing.Color.Red;
            lblErrMsg.Visible = true;
        }

        private void SuccessMessage()
        {
            lblErrMsg.Text = "uploaded file successfully";
            lblErrMsg.ForeColor = System.Drawing.Color.Blue;
            lblErrMsg.Visible = true;
        }

        public object LoadBranchDdlData()
        {
            var db1 = new BranchesDB();
            var branches = db1.GetBranches();
            return branches;
        }
    }
}