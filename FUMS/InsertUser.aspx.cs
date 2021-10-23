using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserMgr.DAC;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FloraSoft.Cps.UserManager
{
    public partial class InsertUser : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["RoleCD"].Value != "UMMK")
            {
                Response.Redirect("~/AccessDenied.aspx");
            }        
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FloraSoft.BankSettingsDB bsdb = new BankSettingsDB();
                FloraSoft.BankSettings bs = bsdb.GetBankSettings();

                BindBranchList();
                BindDepartment();
                BindStatusList();
                BindSubBranch();

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
            }
        }
        private void BindDepartment()
        {
            DepartmentDB db = new DepartmentDB();
            ddlDepartment.DataSource = db.GetDepartment();
            ddlDepartment.DataBind();
        }

        private void BindStatusList()
        {
            StatusDB db = new StatusDB();
            ddlStatus.DataSource = db.GetMakerStatus();
            ddlStatus.DataBind();
        }
        private void BindBranchList()
        {
            BranchesDB db = new BranchesDB();
            ddlbranch.DataSource = db.GetBranches();
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("--SELECT--", ""));
        }
        private void BindUMRoleList()
        {
            UMRoleList.DataSource = new RoleDB().GetAllRolesByModuleID(1);
            UMRoleList.DataBind();
        }
        private void BindRTGSRoleList()
        {
            RTGSRoleList.DataSource = new RoleDB().GetAllRolesByModuleID(2);
            RTGSRoleList.DataBind();
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
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            bool ichecked = false;
            int checkerCnt = 0;
            int authCnt = 0;
            int UMRoleCnt = 0;
            int CPSRoleCnt = 0;
            int EFTNRoleCnt = 0;
            string RoleStatus = "";
            int outward = 0;
            int inward = 0;

            FloraSoft.BankSettingsDB bsdb = new BankSettingsDB();
            FloraSoft.BankSettings bs = bsdb.GetBankSettings();

            if (txtloginid.Text.Length < bs.LoginIDMinLen)
            {
                lblMessage.Text = "Login ID must be minimum " + bs.LoginIDMinLen.ToString() + " characters";
                return;
            }  

            PasswordPolicy pol = new PasswordPolicy();
            string result = pol.PasswordPolicyTest(txtpass.Text,0);
            if (result != "OK")
            {
                lblMessage.Text = result;
                return;
            }

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
                            if ((CPSRoleCnt == 1) && (outward == 1))
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
                            {
                                CPSRoleCnt = 1;
                                inward = 1;
                            }
                        }
                        if ((lblcheck.IndexOf("Checker") > -1) && (lblcheck.IndexOf("Inward") > -1))
                        {
                            if (RoleStatus == "finish")
                            {
                                CPSRoleCnt++;
                                inward++;
                            }
                            if ((CPSRoleCnt == 1) && (inward == 1))
                            {
                                CPSRoleCnt++;
                                inward++;
                            }
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
                lblMessage.Text = "You must select at least one role.";
                return;
            }
            if (UMRoleCnt > 1)
            {
                lblMessage.Text = "User can not have both Maker and Checker Role in User Module.";
                return;
            }
            if (CPSRoleCnt > 1)
            {
                lblMessage.Text = "Both Maker and Checker role is not allowed in CPS Module.";
                return;
            }
            if (EFTNRoleCnt > 1)
            {
                lblMessage.Text = "Both Maker and Checker role is not allowed in EFTN Module.";
                return;
            }
            if (checkerCnt > 1)
            {
                lblMessage.Text = "Only One Checker Role is allowed.";
                return;
            }
            if (authCnt > 1)
            {
                lblMessage.Text = "Only One Authorizer Role is allowed.";
                return;
            }

            UserDB db = new UserDB();
            try
            {
                string EditingUser = Request.Cookies["UserName"].Value;
                string EditorLoginID = Response.Cookies["LoginID"].Value;
                string EditorRole = Request.Cookies["RoleName"].Value;
                string SubBranchCD = "";
                SubBranchCD = ddlSubBranch.SelectedValue == "" ? null : ddlSubBranch.SelectedValue;
                string dbMessage = "";
                int UID = db.InsertUser(ddlbranch.SelectedValue, Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), txtloginid.Text, txtname.Text, txtEmail.Text, txtcontact.Text, txtpass.Text, chkAllBranch.Checked, Int32.Parse(Context.User.Identity.Name), Request.UserHostAddress, EditorRole, SubBranchCD, out dbMessage);

                InsertUserRoleOfAUser(UID);
                lblMessage.Text = dbMessage;
                if (UID > 0)
                {
                    ResetForm();
                    BindUMRoleList();
                    BindRTGSRoleList();
                    BindCPSRoleList();
                    BindEFTNRoleList();
                    lblMessage.CssClass = "alert-success";
                }
                else
                {
                    lblMessage.CssClass = "alert-danger";
                }
            }
            catch(SqlException ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void ResetForm()
        {
            txtloginid.Text = "";
            txtpass.Text = "";
            txtname.Text = "";
            txtcontact.Text = "";
            txtEmail.Text = "";
            ddlbranch.SelectedIndex = -1;
            ddlSubBranch.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
            //ddlStatus.DataSource = null;
            //UMRoleList.DataSource = null;
            //CPSRoleList.DataSource = null;
            chkAllBranch.Checked = false;
        }

        private void InsertUserRoleOfAUser(int UserID)
        {
            FloraSoft.BankSettingsDB bsdb = new BankSettingsDB();
            FloraSoft.BankSettings bs = bsdb.GetBankSettings();
            string LoginID = Request.Cookies["LoginID"].ToString();
            RoleDB db = new RoleDB();

            for (int i = 0; i < UMRoleList.Items.Count; i++)
            {
                if (UMRoleList.Items[i].Selected)
                {
                    try
                    {
                        int UserRole = Int32.Parse(UMRoleList.Items[i].Value);
                        db.InsertRole(UserID, UserRole, 1, LoginID, Request.UserHostAddress);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }

            if (bs.Modules.Contains(",RTGS"))
            {
                for (int i = 0; i < RTGSRoleList.Items.Count; i++)
                {
                    if (RTGSRoleList.Items[i].Selected)
                    {
                        try
                        {
                            int UserRole = Int32.Parse(RTGSRoleList.Items[i].Value);
                            db.InsertRole(UserID, UserRole, 2, LoginID, Request.UserHostAddress);
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                        }
                    }
                }
            }

            if (bs.Modules.Contains(",CPS"))
            {
                for (int i = 0; i < CPSRoleList.Items.Count; i++)
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
            }

            if (bs.Modules.Contains(",EFTN"))
            {
                for (int i = 0; i < EFTNRoleList.Items.Count; i++)
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


        }
        private void BindSubBranch()
        {
            ddlSubBranch.Items.Clear();
            SubBranchDB db = new SubBranchDB();
            ddlSubBranch.DataSource = db.GetSubBranchesByRoutingNo(ddlbranch.SelectedValue.ToString());
            ddlSubBranch.DataBind();
            ddlSubBranch.Items.Insert(0, new ListItem("--SELECT--", ""));
        }

        protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubBranch();
        }

    }
}