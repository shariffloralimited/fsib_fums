using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserMgr.DAC;

namespace FloraSoft.Cps.UserManager
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindModuleList();
                BindBranchList();
                BindStatusList();
                BindStatusList1();
                if (Request.Cookies["RoleID"].Value == "100")
                {
                    MakerPanel.Visible = false;
                    ChkIsPending.Checked = true;
                }
                else
                {
                    MakerPanel.Visible = true;
                    CheckerPanel.Visible = false;
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Request.Cookies["RoleID"].Value == "100") 
            {
                chkCheckAll.Visible = false;
                if (ChkIsPending.Checked)
                {
                    chkCheckAll.Visible = true;
                    CheckerPanel.Visible = true;
                }
                else
                {
                    CheckerPanel.Visible = false;
                }
            }
            else
            {
                CheckerPanel.Visible = false;
                if (ChkIsPending.Checked == true)
                {
                    chkCheckAll.Visible = false;
                    MakerPanel.Visible = false;
                }
                else
                {
                    chkCheckAll.Visible = true;
                    MakerPanel.Visible = true;
                }
            }
            BindData();
        }
        private void BindModuleList()
        {
            ModuleDB db = new ModuleDB();
            ddlModule.DataSource = db.GetAllModules();
            ddlModule.DataBind();
            ddlModule.SelectedValue = "3";

        }
        private void BindData()
        {
            UserDB db = new UserDB();
            MyDataGrid2.DataSource = db.GetUserByBranchID(Int32.Parse(ddlModule.SelectedValue), Int32.Parse(BranchList.SelectedValue), Int32.Parse(StatusList.SelectedValue), txtLoginID.Text, ChkIsPending.Checked);
            MyDataGrid2.DataBind();
            ToggleSelect();
        }
        private void BindBranchList()
        {
            BranchesDB db = new BranchesDB();
            BranchList.DataSource = db.GetBranches();
            BranchList.DataBind();
            BranchList.Items.Add(new ListItem("All", "0"));
            BranchList.SelectedValue = "0";
        }
        private void BindStatusList()
        {
            StatusDB db = new StatusDB();
            DataTable dt = db.GetStatus();
            StatusList.DataSource = dt;
            StatusList.DataBind();
        }
        private void BindStatusList1()
        {
            StatusDB db = new StatusDB();
            DataTable dt = db.GetMakerStatus();
            ddlStatus1.DataSource = dt;
            ddlStatus1.DataBind();
        }
        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            UserDB db = new UserDB();
            if (e.CommandName == "Edit")
            {
                MyDataGrid2.EditItemIndex = e.Item.ItemIndex;
                int UserID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                int Branchid = Int32.Parse(BranchList.SelectedValue);

                Response.Redirect("EditUser.aspx?UserID=" + UserID.ToString());
            }
            if (e.CommandName == "Insert")
            {
                Response.Redirect("InsertUser.aspx");
            }
            if (e.CommandName == "Delete")
            {
               
                int UserID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                Response.Redirect("DeleteUser.aspx?UserID=" + UserID.ToString());
            }
            if (e.CommandName == "Detail")
            {
                int UserID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                Response.Redirect("~/UserDetail.aspx?UserID=" + UserID.ToString());
            }
            
        }
        protected void btnChangeStatus_Click(object sender, EventArgs e)
        {
            UserDB db = new UserDB();
            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                if (((CheckBox)(MyDataGrid2.Items[i].FindControl("chkActive"))).Checked)
                {
                    int userId = Convert.ToInt32(((Label)(MyDataGrid2.Items[i].FindControl("LabelUserID"))).Text);
                    string EditingUser = Request.Cookies["UserName"].Value;
                    db.ChangeUserStatus(userId, Convert.ToInt16(ddlStatus1.SelectedValue), Convert.ToInt32(Context.User.Identity.Name), Request.UserHostAddress, EditingUser);
                }
            }
            BindData();
            chkCheckAll.Checked = false;
        }
        protected void ToggleSelect()
        {
            bool status = chkCheckAll.Checked;

            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                ((CheckBox)(MyDataGrid2.Items[i].FindControl("chkActive"))).Checked = status;
            }
        }
        protected void btnAprove_Click(object sender, EventArgs e)
        {
            UserDB db = new UserDB();
            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                if (((CheckBox)(MyDataGrid2.Items[i].FindControl("chkActive"))).Checked)
                {
                    int userId = Convert.ToInt32(((Label)(MyDataGrid2.Items[i].FindControl("LabelUserID"))).Text);
                    db.ApproveUser(userId, Convert.ToInt32(Context.User.Identity.Name), Request.UserHostAddress);
                }
            }
            BindData();
            chkCheckAll.Checked = false;
        }

        protected void btnDisapprove_Click(object sender, EventArgs e)
        {
            UserDB db = new UserDB();
            for (int i = 0; i < MyDataGrid2.Items.Count; i++)
            {
                if (((CheckBox)(MyDataGrid2.Items[i].FindControl("chkActive"))).Checked)
                {
                    int userId = Convert.ToInt32(((Label)(MyDataGrid2.Items[i].FindControl("LabelUserID"))).Text);
                    db.DisapproveUser(userId, Convert.ToInt32(Context.User.Identity.Name), Request.UserHostAddress, Request.Cookies["RoleName"].Value);
                }
            }
            BindData();
            chkCheckAll.Checked = false;
        }

    }
}

