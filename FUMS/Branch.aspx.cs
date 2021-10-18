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
    public partial class Branch : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["ChangePwdNow"].Value == "TRUE")
            {
                Response.Redirect("~/ChangePassword.aspx");
            }
            //if (Request.Cookies["RoleCD"].Value != "UMMK")
            //{
            //    Response.Redirect("~/AccessDenied.aspx");
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              
                BindZone();
            }
        }
        protected void Page_PreRender(object sender,EventArgs e)
        {
            BindData();
        }
        private void BindZone()
        {
            ZonesDB db = new ZonesDB();
            ddlZone.DataSource = db.GetZones();
            ddlZone.DataBind();

        }
        private void BindData()
        {
            BranchesDB db = new BranchesDB();
            MyDataGrid.DataSource = db.GetBranches();
            //MyDataGrid.DataSource = db.GetBranchesByZoneID(Int32.Parse(ddlZone.SelectedValue));
            MyDataGrid.DataBind();
        }
        protected void MyDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            BranchesDB db = new BranchesDB();
            if (e.CommandName == "Cancel")
            {
                MyDataGrid.EditItemIndex = -1;
                BindData();
            }
            if (e.CommandName == "Edit")
            {
                MyDataGrid.EditItemIndex = e.Item.ItemIndex;
                BindData();

                if (Request.Cookies["RoleCD"].Value != "UMMK")
                {
                    MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[1].Enabled = false;
                    MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[2].Enabled = false;
                    MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[3].Enabled = false;
                    MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[4].Enabled = false;
                    MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[5].Enabled = false;

                    CheckBox ChkApproved = (CheckBox)MyDataGrid.Items[MyDataGrid.EditItemIndex].FindControl("ChkApproved");
                    if (ChkApproved != null && ChkApproved.Checked == true)
                    {
                        //MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[6].Enabled = false;
                    }
                    else
                    {
                        //MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[6].Enabled = true;
                    }
                }
                else
                {
                    //MyDataGrid.Items[MyDataGrid.EditItemIndex].Cells[6].Visible = false;

                }
            }
            if (e.CommandName == "Insert")
            {
                lblErrMsg.ForeColor = System.Drawing.Color.Red;

                TextBox txtBranchName = (TextBox)e.Item.FindControl("addBranchName");
                TextBox txtRoutingNo = (TextBox)e.Item.FindControl("addRoutingNo");

                TextBox txtBranchNumonic = (TextBox)e.Item.FindControl("addBranchNumonic");
                TextBox txtBranchCD = (TextBox)e.Item.FindControl("addBranchCD");

                if (txtRoutingNo.Text.Trim().Length < 9)
                {
                    lblErrMsg.Text = "Routing No is 9 digits.";
                    return;
                }

                if (txtBranchName.Text.Trim() == "")
                {
                    lblErrMsg.Text = "Branch Name is missing.";
                    return;
                }
                if (txtBranchNumonic.Text.Trim().Length > 4)
                {
                    lblErrMsg.Text = "Branch Numonic is max 4 digits.";
                    return;
                }
                if (txtBranchCD.Text.Trim() == "")
                {
                    lblErrMsg.Text = "Branch CD is missing.";
                    return;
                }

                string result = db.InsertBranches(txtBranchName.Text, txtRoutingNo.Text, txtBranchNumonic.Text, txtBranchCD.Text, Int32.Parse(Request.Cookies["UserID"].Value), Request.UserHostAddress);
                if (result.IndexOf('s')!=-1)
                {
                    lblErrMsg.Text = "Added successfully";
                    lblErrMsg.ForeColor = System.Drawing.Color.Blue;
                    BindData();
                }
                else
                {
                    lblErrMsg.Text = "Duplicate Data";
                    lblErrMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            if (e.CommandName == "Update")
            {
                lblErrMsg.ForeColor = System.Drawing.Color.Red;

                int BranchID = (int)MyDataGrid.DataKeys[e.Item.ItemIndex];

                TextBox txtBranchName = (TextBox)e.Item.FindControl("BranchName");
                TextBox txtRoutingNo = (TextBox)e.Item.FindControl("RoutingNo");

                TextBox txtBranchNumonic = (TextBox)e.Item.FindControl("BranchNumonic");
                TextBox txtBranchCD = (TextBox)e.Item.FindControl("BranchCD");

                CheckBox ChkApproved = (CheckBox)e.Item.FindControl("ChkApproved");

                if (txtRoutingNo.Text.Trim().Length < 9)
                {
                    lblErrMsg.Text = "Routing No is 9 digits.";
                    return;
                }
                if (txtBranchName.Text.Trim() == "")
                {
                    lblErrMsg.Text = "Branch Name is missing.";
                    return;
                }
                if (txtBranchCD.Text.Trim()== "")
                {
                    lblErrMsg.Text = "Branch CD is missing.";
                    return;
                }
                if (txtRoutingNo.Text.Trim().Length < 9)
                {
                    lblErrMsg.Text = "Routing No is 9 digits.";
                    return;
                }

                string msg=db.UpdateBranch(BranchID, txtBranchName.Text, txtRoutingNo.Text, txtBranchNumonic.Text, txtBranchCD.Text, ChkApproved.Checked, Request.Cookies["RoleCD"].Value);

                MyDataGrid.EditItemIndex = -1;
                //lblErrMsg.Text = "Updated successfully";
                //lblErrMsg.ForeColor = System.Drawing.Color.Blue;

                //MyDataGrid.EditItemIndex = -1;
                lblErrMsg.Text = msg;
                lblErrMsg.ForeColor = System.Drawing.Color.Blue;

                BindData();
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

    }
}