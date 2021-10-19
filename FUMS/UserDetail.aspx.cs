using FloraSoft.Cps.UserManager.BLL;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using FloraSoft.Cps.UserMgr.DAC;

namespace FloraSoft.Cps.UserManager
{
    public partial class UserDetail : System.Web.UI.Page
    {
        protected int UserID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FloraSoft.BankSettingsDB bsdb = new BankSettingsDB();
                FloraSoft.BankSettings bs = bsdb.GetBankSettings();

                try
                {
                    UserID = Int32.Parse(Request.QueryString["UserID"]);
                }
                catch
                {
                    Response.Redirect("Users.aspx");
                }

                BindBranchList();
                BindZone();
                BindUMRoleList();
                if (bs.Modules.Contains(",RTGS"))
                {
                    RTGSPanel.Visible = true;
                    BindRTGSRoleList();
                }

                if (bs.Modules.Contains(",CPS"))
                {
                    CPSPanel.Visible = true;
                    BindCPSRoleList();
                }
                if (bs.Modules.Contains(",EFTN"))
                {
                    EFTNPanel.Visible = true;
                    BindEFTNRoleList();
                }
                BindUser();
                SetRoles();
                SetZones();

                txtEmail.Enabled = false;
                txtcontact.Enabled      = false;
                ddlDepartment.Enabled   = false;
                txtloginid.Enabled      = false;
                txtname.Enabled         = false;

            }
        }

        private void BindZone()
        {
            ZonesDB db = new ZonesDB();
            ZoneList.DataSource = db.GetZones();
            ZoneList.DataBind();

        }
        private void BindUser()
        {
            UserDB user = new UserDB();
            SqlDataReader dr = user.GetSingleUser(UserID);
            while (dr.Read())
            {
                txtloginid.Text = dr["LoginID"].ToString();
                txtname.Text = dr["UserName"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtcontact.Text = dr["ContactNo"].ToString();
                string BranchId = dr["BranchID"].ToString();
                ddlbranch.SelectedValue = BranchId;
                BindDepartmentList();
                ddlDepartment.SelectedValue = dr["DeptID"].ToString();
                BindSubBranchList(dr["RoutingNo"].ToString());
                ddlSubBranch.SelectedValue = dr["SubBranchCD"].ToString();
                chkAllBranch.Checked = (bool)dr["AllBranch"];
            }
            dr.Close();
            dr.Dispose();
        }

        private void BindDepartmentList()
        {
            DepartmentDB db1 = new DepartmentDB();
            ddlDepartment.DataSource = db1.GetDepartment();
            ddlDepartment.DataBind();
        }

        private void SetZones()
        {
            ZonesDB zone = new ZonesDB();
            SqlDataReader dr = zone.GetZonesByUserID(UserID);
            while (dr.Read())
            {
                string ZoneID = dr["ZoneID"].ToString();

                //int ModuleId = (int)dr["ModuleID"];

                int n = ZoneList.Items.Count;
                //int m = RTGSRoleList.Items.Count;
                //int j = CPSRoleList.Items.Count;
                //int k = EFTNRoleList.Items.Count;

                //if (ModuleId == 1)

                for (int i = 0; i < n; i++)
                {
                    if (ZoneList.Items[i].Value == ZoneID)
                    {
                        ZoneList.Items[i].Selected = true;
                    }
                }
            }
        }
        private void SetRoles()
        {
            RoleDB role = new RoleDB();
            SqlDataReader dr = role.GetRolesOfAUser(UserID);
            while (dr.Read())
            {
                string roleid = dr["RoleID"].ToString();

                int ModuleId = (int) dr["ModuleID"];

                int n = UMRoleList.Items.Count;
                int m = RTGSRoleList.Items.Count;
                int j = CPSRoleList.Items.Count; 
                int k = EFTNRoleList.Items.Count;

                if (ModuleId == 1)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (UMRoleList.Items[i].Value == roleid)
                        {
                            UMRoleList.Items[i].Selected = true;
                        }
                    }
                }

                if (ModuleId == 2)
                {
                    for (int i = 0; i < m; i++)
                    {
                        if (RTGSRoleList.Items[i].Value == roleid)
                        {
                            RTGSRoleList.Items[i].Selected = true;
                        }
                    }
                }

                if (ModuleId == 3)
                {
                    for (int i = 0; i < j; i++)
                    {
                        if (CPSRoleList.Items[i].Value == roleid)
                        {
                            CPSRoleList.Items[i].Selected = true;
                        }
                    }
                }

                if (ModuleId == 4)
                {
                    for (int i = 0; i < k; i++)
                    {
                        if (EFTNRoleList.Items[i].Value == roleid)
                        {
                            EFTNRoleList.Items[i].Selected = true;
                        }
                    }
                }
            }
            dr.Close();
            dr.Dispose();
        }
        private void BindBranchList()
        {
            BranchesDB db1 = new BranchesDB();
            ddlbranch.DataSource = db1.GetBranches();
            ddlbranch.DataBind();
        }
        private void BindSubBranchList(string routingNo)
        {
            SubBranchDB db = new SubBranchDB();
            ddlSubBranch.DataSource = db.GetSubBranchesByRoutingNo(routingNo);
            ddlSubBranch.DataBind();
            ddlSubBranch.Items.Insert(0, new ListItem("", ""));
        }

        private void BindRTGSRoleList()
        {
            RTGSRoleList.DataSource = new RoleDB().GetAllRolesByModuleID(2);
            RTGSRoleList.DataBind();
        }
        private void BindUMRoleList()
        {
            UMRoleList.DataSource = new RoleDB().GetAllRolesByModuleID(1);
            UMRoleList.DataBind();
        }
        private void BindCPSRoleList()
        {
            CPSRoleList.DataSource = new RoleDB().GetAllRolesByModuleID(3);
            CPSRoleList.DataBind();
        }
        private void BindEFTNRoleList()
        {
            EFTNRoleList.DataSource = new RoleDB().GetAllRolesByModuleID(4);
            EFTNRoleList.DataBind();
        }

    }
}