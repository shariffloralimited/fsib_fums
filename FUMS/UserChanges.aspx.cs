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

namespace FloraSoft.Cps.UserManager
{
    public partial class UserChanges : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBranchList();
                if ((Request.Params["BranchListID"] != null) && (Request.Params["BranchListID"] != ""))
                {
                    BranchList.SelectedValue = Request.Params["BranchListID"];
                }
                BindData();
                if (Request.Cookies["RoleID"].Value == "99")
                {
                    MyDataGrid2.Columns[0].Visible = true;
                    MyDataGrid2.Columns[7].Visible = true;
                }
                else if (Request.Cookies["RoleID"].Value == "100")
                {
                    MyDataGrid2.Columns[1].Visible = true;
                }
                
            }
        }
        private void BindData()
        {
            UserDB db = new UserDB();

            MyDataGrid2.DataSource = db.GetTempUserByBranchID(Int32.Parse(BranchList.SelectedValue));
            MyDataGrid2.DataBind();
        }
        private void BindBranchList()
        {
            BranchesDB db = new BranchesDB();
            BranchList.DataSource = db.GetBranches();
            BranchList.DataBind();
        }


        protected void MyDataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            UserDB db = new UserDB();
            
            if (e.CommandName == "Edit")
            {
                //MyDataGrid2.EditItemIndex = e.Item.ItemIndex;
                //int UserID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                //int Branchid = Int32.Parse(BranchList.SelectedValue);
                //Session["OpUserID"] = UserID;
                //Response.Redirect("EditUser.aspx");
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
            if (e.CommandName == "Suspend")
            {
                int UserID = (int)MyDataGrid2.DataKeys[e.Item.ItemIndex];
                Response.Redirect("~/ActivateUsers.aspx?UserID=" + UserID);
            }
            
        }
        protected void ddlBankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranchList();
        }
    }
}

