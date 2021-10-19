using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserMgr.DAC;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
namespace FloraSoft.Cps.UserManager
{
    public partial class EditUser : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                BindBranchList();
                BindDepartmentList();
                BindStatusList();
                BindUser();
                BindUMRoleList();
                BindZone();
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

                if(!bs.AllActionToChecker)
                {
                    ddlStatus.Enabled = false;
                }
                SetRoles();
                SetZones();
            
            }
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
                
        private void BindZone()
        {
            ZonesDB db = new ZonesDB();
            ZoneList.DataSource = db.GetZones();
            ZoneList.DataBind();

        }
        private void BindDepartmentList()
        {
            DepartmentDB db1 = new DepartmentDB();
            ddlDepartment.DataSource = db1.GetDepartment();
            ddlDepartment.DataBind();
        }

        private void BindStatusList()
        {
            StatusDB db = new StatusDB();
            ddlStatus.DataSource = db.GetMakerStatus();
            ddlStatus.DataBind();
        }
        private void BindUser()
        {
            UserDB user = new UserDB();
            SqlDataReader dr = user.GetSingleUser(UserID);
            while (dr.Read())
            {
                ddlbranch.SelectedValue = dr["RoutingNo"].ToString();
                BindSubBranchList(ddlbranch.SelectedValue);
                ddlSubBranch.SelectedValue = dr["SubBranchCD"].ToString();
                txtloginid.Text = dr["LoginID"].ToString();
                txtname.Text = dr["UserName"].ToString();
                ddlDepartment.SelectedValue = dr["DeptID"].ToString();
                ddlStatus.SelectedValue = dr["StatusID"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtcontact.Text = dr["ContactNo"].ToString();
                ChkAllBranch.Checked = (bool)dr["AllBranch"];

                if ((bool)dr["isPending"])
                {
                    Response.Redirect("AccessDenied.aspx");
                }
            }
            dr.Close();
            dr.Dispose();
        }

        private void BindSubBranchList(string routingNo)
        {
            ddlSubBranch.Items.Clear();
            SubBranchDB db = new SubBranchDB();
            ddlSubBranch.DataSource = db.GetSubBranchesByRoutingNo(routingNo);
            ddlSubBranch.DataBind();
            ddlSubBranch.Items.Insert(0, new ListItem("--SELECT--", ""));
        }

        private void SetRoles()
        {
            RoleDB role = new RoleDB();
            SqlDataReader dr = role.GetRolesOfAUser(UserID);
            while (dr.Read())
            {
                string roleid = dr["RoleID"].ToString();

                int ModuleId = (int)dr["ModuleID"];

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
            SqlDataReader dr = db1.GetBrancheByUserID(UserID);
            ddlbranch.DataSource = db1.GetBranches();
            ddlbranch.DataBind();

            while (dr.Read())
            {
                ddlbranch.SelectedValue = dr["RoutingNo"].ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool ichecked = false;
            int checkerCnt = 0;
            int authCnt = 0;
            int UMRoleCnt = 0;
            int CPSRoleCnt = 0;
            int EFTNRoleCnt = 0;
            string RoleStatus = "";
            int outward = 0;
            int inward = 0;

            #region Validator
            FloraSoft.BankSettingsDB bsdb = new BankSettingsDB();
            FloraSoft.BankSettings bs = bsdb.GetBankSettings();

            for (int i = 0; i < UMRoleList.Items.Count; i++)
            {
                if (UMRoleList.Items[i].Selected)
                {
                    string lblcheck = UMRoleList.Items[i].Text;
                    if (lblcheck.IndexOf("Maker") > -1)
                    {
                        UMRoleCnt++;
                    }
                    if (lblcheck.IndexOf("Checker") > -1)
                    {
                        UMRoleCnt++;
                    }
                    ichecked = true;
                }
            }

            if (bs.Modules.Contains(",RTGS"))
            {
                for (int i = 0; i < RTGSRoleList.Items.Count; i++)
                {
                    if (RTGSRoleList.Items[i].Selected)
                    {
                        string lblcheck = RTGSRoleList.Items[i].Text;
                        if (lblcheck.IndexOf("Checker") > -1)
                        {
                            checkerCnt++;
                        }
                        if (lblcheck.IndexOf("Authorizer") > -1)
                        {
                            authCnt++;
                        }
                        ichecked = true;
                    }
                }
            }

            if (bs.Modules.Contains(",CPS"))
            {
                for (int i = 0; i < CPSRoleList.Items.Count; i++)
                {
                    if (CPSRoleList.Items[i].Selected)
                    {
                        string lblcheck = CPSRoleList.Items[i].Text;
                        if ((lblcheck.IndexOf("Maker") > -1) && (lblcheck.IndexOf("Outward") > -1)) 
                        {
                            CPSRoleCnt++;
                            outward++;
                        }
                        if ((lblcheck.IndexOf("Checker") > -1) && (lblcheck.IndexOf("Outward") > -1)) 
                        {
                            CPSRoleCnt++;
                            outward++;
                        }

                        if ((lblcheck.IndexOf("Maker") > -1) && (lblcheck.IndexOf("Inward") > -1))
                        {
                            if ((CPSRoleCnt == 1)&& (outward==1))
                            {
                                RoleStatus = "finish";
                                inward++;
                            }
                            if ((CPSRoleCnt == 2) && (outward == 2))
                            {
                                CPSRoleCnt++;
                                inward++;
                            }
                            else
                            { CPSRoleCnt = 1;
                            inward = 1;
                            }


                            //CPSRoleCnt++;
                        }
                        if ((lblcheck.IndexOf("Checker") > -1) && (lblcheck.IndexOf("Inward") > -1))
                        {
                            if (RoleStatus == "finish")
                            {
                                CPSRoleCnt++;
                                inward++;
                            }
                            if ((CPSRoleCnt == 1) && (inward == 1))
                            { CPSRoleCnt++;
                            inward++;
                            }
                            //else
                            //{
                            //    CPSRoleCnt++;
                            //}
                        }

                        ichecked = true;
                    }
                }
            }

            if (bs.Modules.Contains(",EFTN"))
            {
                for (int i = 0; i < EFTNRoleList.Items.Count; i++)
                {
                    if (EFTNRoleList.Items[i].Selected)
                    {
                        string lblcheck = EFTNRoleList.Items[i].Text;
                        if (lblcheck.IndexOf("Maker") > -1)
                        {
                            EFTNRoleCnt++;
                        }
                        if (lblcheck.IndexOf("Checker") > -1)
                        {
                            EFTNRoleCnt++;
                        }
                        ichecked = true;
                    }
                }
            }

            if (!ichecked)
            {
                lblMessage.ForeColor = System.Drawing.Color.Magenta;
                lblMessage.Text = "You must select at least one role.";
                return;
            }
            if (UMRoleCnt > 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Magenta;
                lblMessage.Text = "Both Maker and Checker role is not allowed in User Module.";
                return;
            }
            if ((CPSRoleCnt > 1)&& (outward==2))
            {
                lblMessage.ForeColor =  System.Drawing.Color.Magenta;
                lblMessage.Text = "Both Outward Maker and Outward Checker role is not allowed for Single User.";
                return;
            }

            if ((CPSRoleCnt > 1) && (inward == 2))
            {
                lblMessage.ForeColor = System.Drawing.Color.Magenta;
                lblMessage.Text = "Both Inward Maker and Inward Checker role is not allowed for Single User.";
                return;
            }



            if (EFTNRoleCnt > 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Magenta;
                lblMessage.Text = "Both Maker and Checker role is not allowed in EFTN Module.";
                return;
            }
            if (checkerCnt > 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Magenta;
                lblMessage.Text = "Only One Checker Role is allowed.";
                return;
            }
            if (authCnt > 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Magenta;
                lblMessage.Text = "Only One Authorizer Role is allowed.";
                return;
            }

            if (txtloginid.Text.Length < bs.LoginIDMinLen)
            {
                lblMessage.ForeColor = System.Drawing.Color.Magenta;
                lblMessage.Text = "Login ID must be minimum "+bs.LoginIDMinLen.ToString()+" characters";
                return;
            }            
            #endregion            
            

            InsertUserRoleOfAUser(UserID);

            InsertUserZoneOfAUser(UserID);

            string EditingUserID = Request.Cookies["UserID"].Value;
            string EditorLoginID = Response.Cookies["LoginID"].Value;

            UserDB db = new UserDB();
            db.UpdateSingleUser(UserID, 0, ddlbranch.SelectedValue, txtloginid.Text, 
                txtname.Text, Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), 
                ChkAllBranch.Checked, txtEmail.Text, txtcontact.Text,  Int32.Parse(EditingUserID), Request.UserHostAddress, ddlSubBranch.SelectedValue);
            lblMessage.ForeColor = System.Drawing.Color.Magenta;
            lblMessage.Text = "Updated Successfully";
            //Response.Redirect("Users.aspx");
           

        }


        private void InsertUserZoneOfAUser(int UserID)
        {
            ZonesDB db = new ZonesDB();

            db.DeleteZonesOfAUser(UserID);

            int n = ZoneList.Items.Count;
            //int m = RTGSRoleList.Items.Count;
            //int j = CPSRoleList.Items.Count;
            //int k = EFTNRoleList.Items.Count;

            for (int i = 0; i < n; i++)
            {
                if (ZoneList.Items[i].Selected)
                {
                    try
                    {
                        int UserZone = Int32.Parse(ZoneList.Items[i].Value);
                        db.InsertZone(UserID, UserZone, Request.UserHostAddress);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }

     

      

    
       }
        
        private void InsertUserRoleOfAUser(int UserID)
        {

            string LoginID = Request.Cookies["LoginID"].ToString();
            RoleDB db = new RoleDB();

            db.DeleteRoleOfAUser(UserID);

            int n = UMRoleList.Items.Count;
            int m = RTGSRoleList.Items.Count;
            int j = CPSRoleList.Items.Count;
            int k = EFTNRoleList.Items.Count;

            for (int i = 0; i < n; i++)
            {
                if (UMRoleList.Items[i].Selected)
                {
                    try
                    {
                        int UserRole = Int32.Parse(UMRoleList.Items[i].Value);
                        db.InsertRole(UserID, UserRole, 1,LoginID, Request.UserHostAddress);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }

            for (int i=0; i<m; i++)
            {
                if (RTGSRoleList.Items[i].Selected)
                {
                    try
                    {
                        int UserRole = Int32.Parse(RTGSRoleList.Items[i].Value);
                        db.InsertRole(UserID, UserRole, 2,LoginID, Request.UserHostAddress);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }

            for (int i = 0; i < j; i++)
            {
                if (CPSRoleList.Items[i].Selected)
                {
                    try
                    {   
                       
                        int UserRole = Int32.Parse(CPSRoleList.Items[i].Value);
                        db.InsertRole(UserID, UserRole, 3, LoginID, Request.UserHostAddress);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }

            for (int i = 0; i < k; i++)
            {
                if (EFTNRoleList.Items[i].Selected)
                {
                    try
                    {
                        int UserRole = Int32.Parse(EFTNRoleList.Items[i].Value);
                        db.InsertRole(UserID, UserRole, 4, LoginID, Request.UserHostAddress);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }
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

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = ddlbranch.SelectedValue;
            if (value != "")
            {
                SubBranchDB db = new SubBranchDB();
                var subBranches = db.GetSubBranchesByRoutingNo(value);
                ddlSubBranch.Items.Clear();
                ddlSubBranch.DataSource = subBranches;
                ddlSubBranch.DataBind();
            }
        }
    }
}