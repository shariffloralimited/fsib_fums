using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace FloraSoft.Cps.UserManager
{
    public class UserInfo
    {
        public string UserID;
        public string RoleCD;
        public string RoleID;
        public string ZoneID;
        public string BranchID;
        public string RoutingNo;
        public string UserName;
        public string RoleName;
        public string RoleCount;
        public string BranchName;
        public string EntryHash;
        public string BranchHash;
        public string DaysPassed;
        public string ChangePwdNow;
        public string ExpMsg;
        public string EditingUserID;
    }
    public class PasswordPolicy
    {
        public  string PasswordPolicyTest(string Pwd, int UserID)
        {
            FloraSoft.BankSettingsDB bsdb = new BankSettingsDB();
            FloraSoft.BankSettings bs = bsdb.GetBankSettings();

            string result = PwdHasMinDigits(Pwd, bs.PasswordMinLen);
            if (result != "OK")
            {
                return result;
            }
            result = IsNotSameAsLoginID(Pwd, UserID);
            if (result != "OK")
            {
                return result;
            }
            result = IsAlphaNumeric(Pwd);
            if (result != "OK")
            {
                return result;
            }
            //if (UserID != 0)
            //{
            //    result = IsRepeatingOldPassword(Pwd, UserID);
            //    if (result != "OK")
            //    {
            //        return result;
            //    }
            //}
            return "OK";
        }
        public string IsNotSameAsLoginID(string Pwd, int UserID)
        {
            UserDB user = new UserDB();
            string LoginID = "";
            SqlDataReader dr = user.GetSingleUser(UserID);
            while (dr.Read())
            {
                LoginID = (string)dr["LoginID"];
            }
            dr.Close();
            dr.Dispose();

            if (LoginID != Pwd)
            {
                return "OK";
            }
            else
            {
                return "Your Password can not be the same as your LoginID";
            }
        }
        public string IsExpiring(int DaysPassedSinceLastChange)
        {
            if (DaysPassedSinceLastChange > 44)
            {
                return "Your Password has expired. Please contact Administrator.";
            }
            if (DaysPassedSinceLastChange > 34)
            {
                return "Your Password will expire in " + (45 - DaysPassedSinceLastChange).ToString() + " day(s). Please change your password ASAP.";
            }
            return "OK";
        }
        private string PwdHasMinDigits(string Pwd, int PasswordMinLen)
        {
            if (Pwd.Length > PasswordMinLen-1)
            {
                return "OK";
            }
            else
            {
                return "Password must be atleast "+PasswordMinLen.ToString()+" characters.";
            }
        }
        private string IsAlphaNumeric(string Pwd)
        {
            string alphaLow = "abcdefghijklmnopqrstuvwxyz";
            string alphaUp  = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numeric  = "0123456789";
            string special  = "~!@#$%^&*";
            char[] p = Pwd.ToCharArray();

            bool AlphaLowFound = false;
            bool AlphaUpFound  = false;
            bool NumFound      = false;
            bool SpFound       = false;

            foreach (char c in p)
            {
                if (alphaLow.IndexOf(c) != -1)
                {
                    AlphaLowFound = true;
                }
                if (alphaUp.IndexOf(c) != -1)
                {
                    AlphaUpFound = true;
                } 
                if (numeric.IndexOf(c) != -1)
                {
                    NumFound = true;
                }
                if (special.IndexOf(c) != -1)
                {
                    SpFound = true;
                }
            }
            if ((AlphaLowFound) && (AlphaUpFound) && (NumFound) && (SpFound))
            {
                return "OK";
            }
            else
            {
                return "Password must contain Upper case Alphabets, Lower case Alphabets, Numbers and Special Characters.";
            }
        }
        private string IsRepeatingOldPassword(string Pwd, int UserID)
        {
            bool repeating = false;
            UserDB user   = new UserDB();
            string NewPwd = user.Encrypt(Pwd);

            SqlDataReader dr = GetLast3Passwords(UserID);
            while(dr.Read())
            {
                if (NewPwd == (string) dr["Password"])
                {
                    repeating = true;
                }
            }
            dr.Close();
            dr.Dispose();

            if (repeating)
            {
                return "Can not use the same password that you have used during the last 3 changes.";
            }
            else
            {
                return "OK";
            }
        }
        private SqlDataReader GetLast3Passwords(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetLast3Passwords", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
    }
    public class UserDB
    {
        public UserInfo Login(string LoginID, string Password, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UserLogin", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = Encrypt(Password);
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 40);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterRoleCD = new SqlParameter("@RoleCD", SqlDbType.VarChar, 4);
            parameterRoleCD.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleCD);
            
            SqlParameter parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleID);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.Int, 4);
            parameterRoutingNo.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleName);

            SqlParameter parameterRoleCount = new SqlParameter("@RoleCount", SqlDbType.Int, 4);
            parameterRoleCount.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleCount);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterDaysPassed = new SqlParameter("@DaysPassed", SqlDbType.Int, 4);
            parameterDaysPassed.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDaysPassed);

            SqlParameter parameterChangePwdNow = new SqlParameter("@ChangePwdNow", SqlDbType.Bit);
            parameterChangePwdNow.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterChangePwdNow);

            SqlParameter parameterExpMsg = new SqlParameter("@ExpMsg", SqlDbType.VarChar, 50);
            parameterExpMsg.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterExpMsg);
            
            myConnection.Open();
            myCommand.ExecuteNonQuery();

            UserInfo ui     = new UserInfo();

            ui.UserID       = parameterUserID.Value.ToString();
            ui.RoleCD       = parameterRoleCD.Value.ToString();
            ui.RoleID       = parameterRoleID.Value.ToString();
            ui.BranchID     = parameterBranchID.Value.ToString();
            ui.RoutingNo    = parameterRoutingNo.Value.ToString();

            ui.UserName     = (string) parameterUserName.Value;
            ui.RoleName     = (string) parameterRoleName.Value;
            ui.BranchName   = (string) parameterBranchName.Value;

            ui.DaysPassed   = parameterDaysPassed.Value.ToString();
            ui.ChangePwdNow = parameterChangePwdNow.Value.ToString().ToUpper();
            ui.ExpMsg       = (string)parameterExpMsg.Value;

            
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();          

            return ui;
        }
        public UserInfo ADLogin(string LoginID, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_ADLogin", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 40);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterRoleCount = new SqlParameter("@RoleCount", SqlDbType.Int, 4);
            parameterRoleCount.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleCount);

            SqlParameter parameterRoleCD = new SqlParameter("@RoleCD", SqlDbType.VarChar, 4);
            parameterRoleCD.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleCD);

            SqlParameter parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleID);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.Int, 4);
            parameterRoutingNo.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleName);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterChangePwdNow = new SqlParameter("@ChangePwdNow", SqlDbType.Bit);
            parameterChangePwdNow.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterChangePwdNow);

            SqlParameter parameterDaysPassed = new SqlParameter("@DaysPassed", SqlDbType.Int, 4);
            parameterDaysPassed.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterDaysPassed);

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            UserInfo ui = new UserInfo();

            ui.UserID = parameterUserID.Value.ToString();
            ui.RoleCD = parameterRoleCD.Value.ToString();
            ui.RoleID = parameterRoleID.Value.ToString();
            ui.BranchID = parameterBranchID.Value.ToString();
            ui.RoutingNo = parameterRoutingNo.Value.ToString();

            ui.UserName = (string)parameterUserName.Value;
            ui.RoleName = (string)parameterRoleName.Value;
            ui.BranchName = (string)parameterBranchName.Value;

            ui.DaysPassed = parameterDaysPassed.Value.ToString();

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return ui;
        }
        public void UpdateUser(int UserID, String UserName, String Department, String Email, String ContactNo, String LoginID, int EnteredBy, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UpdateUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
            parameterUserName.Value = UserName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterDepartment = new SqlParameter("@Department", SqlDbType.VarChar, 50);
            parameterDepartment.Value = Department;
            myCommand.Parameters.Add(parameterDepartment);

            SqlParameter parameterEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            parameterEmail.Value = Email;
            myCommand.Parameters.Add(parameterEmail);

            SqlParameter parameterContactNo = new SqlParameter("@ContactNo", SqlDbType.VarChar, 50);
            parameterContactNo.Value = ContactNo;
            myCommand.Parameters.Add(parameterContactNo);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.VarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy); 
            
            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

        }
        public void ChangeUserStatus(int UserID, int StatusID, int EnteredBy, string IPAddress, string EditingUser)
        {

            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_ChangeUserStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterUserStatus = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterUserStatus.Value = StatusID;
            myCommand.Parameters.Add(parameterUserStatus);


            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterEditingUserId = new SqlParameter("@EditingUserID", SqlDbType.NVarChar, 50);
            parameterEditingUserId.Value = UserID;
            myCommand.Parameters.Add(parameterEditingUserId);

            //SqlParameter parameterEditingUserId = new SqlParameter("@EditingUserID", SqlDbType.NVarChar, 50);
            //parameterEditingUserId.Value = UserID;
            //myCommand.Parameters.Add(parameterEditingUserId);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

        }

        public void ApproveUser(int UserID, int EnteredBy, string IPAddress)
        {

            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_ApproveUserChanges", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@EditedUserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterEnteredBy = new SqlParameter("@EditingUserID", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterIPAddress = new SqlParameter("@EditorsIP", SqlDbType.VarChar, 40);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }
        public void DisapproveUser(int UserID, int EnteredBy, string IPAddress, string EditorRole)
        {

            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_DisapproveUserChanges", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterEnteredBy = new SqlParameter("@EditingUserID", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterIPAddress = new SqlParameter("@EditorsIP", SqlDbType.NVarChar, 40);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterEditorsRole = new SqlParameter("@EditorsRole", SqlDbType.VarChar, 50);
            parameterEditorsRole.Value = EditorRole;
            myCommand.Parameters.Add(parameterEditorsRole);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }
        public void SuspendUser(int UserID, int BranchID, string UserStatus, DateTime SuspendDateBegin, DateTime SuspendDateEnd, int EnteredBy, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_SuspendUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.NVarChar, 50);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterUserStatus = new SqlParameter("@UserStatus", SqlDbType.NVarChar, 50);
            parameterUserStatus.Value = UserStatus;
            myCommand.Parameters.Add(parameterUserStatus);

            SqlParameter parameterSuspendDateBegin = new SqlParameter("@SuspendDateBegin", SqlDbType.SmallDateTime);
            parameterSuspendDateBegin.Value = SuspendDateBegin;
            myCommand.Parameters.Add(parameterSuspendDateBegin);

            SqlParameter parameterSuspendDateEnd = new SqlParameter("@SuspendDateEnd", SqlDbType.SmallDateTime);
            parameterSuspendDateEnd.Value = SuspendDateEnd;
            myCommand.Parameters.Add(parameterSuspendDateEnd);

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

        }
        public void LockUser(string LoginID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_LockUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();        
        }
        public void UserLogout(int UserID, int ModuleID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UserLogout", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = ModuleID;
            myCommand.Parameters.Add(parameterModuleID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        public SqlDataReader GetAllUsers(int BankID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetUsers", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
            parameterBankID.Value = BankID;
            myCommand.Parameters.Add(parameterBankID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetUserByBranchID(int ModuleID, int BranchID, int StatusID, string LoginID, bool IsPending)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetUserByBranchID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = ModuleID;
            myCommand.Parameters.Add(parameterModuleID);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Value = BranchID;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int, 4);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.VarChar, 20);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterIsPending = new SqlParameter("@IsPending", SqlDbType.Bit);
            parameterIsPending.Value = IsPending;
            myCommand.Parameters.Add(parameterIsPending);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetTempUserByBranchID(int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("Temp_GetUserByBranchID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Value = BranchID;
            myCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetUserList()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetUserList", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetSingleUser(int userID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetSingleUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = userID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader TempGetSingleUser(int userID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("Temp_GetSingleUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = userID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public int ChangePassword(int UserID, String OldPassword, String NewPassword, String IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_ChangePassword", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int,4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterOldPassword = new SqlParameter("@OldPassword", SqlDbType.NVarChar, 50);
            parameterOldPassword.Value = OldPassword;
            myCommand.Parameters.Add(parameterOldPassword);

            SqlParameter parameterNewPassword = new SqlParameter("@NewPassword", SqlDbType.NVarChar, 50);
            parameterNewPassword.Value = NewPassword;
            myCommand.Parameters.Add(parameterNewPassword);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterStatus = new SqlParameter("@Status", SqlDbType.Int, 4);
            parameterStatus.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterStatus);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterStatus.Value;
        }
        public void ResetPassword(int userID, string password, int EnteredBy, string EditorsIP)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_ResetPassword", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = userID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterPassword = new SqlParameter("@NewPassword", SqlDbType.NVarChar, 50);
            parameterPassword.Value = Encrypt(password);
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterEditorsIP = new SqlParameter("@EditorsIP", SqlDbType.VarChar, 50);
            parameterEditorsIP.Value = EditorsIP;
            myCommand.Parameters.Add(parameterEditorsIP);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        public void DeleteOfAUser(int userID, int EnteredBy, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_DeleteOfAUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = userID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        public int InsertUser(String RoutingNo, int DeptID, int StatusID, String LoginID,
            String UserName, String Email, String ContactNo, string Password, bool allBranch,
            int EnteredBy, string EditorsIP, string EditorRole, string subBranchCD, out string dbMessage)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertSingleUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.VarChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterDepartment = new SqlParameter("@DeptID", SqlDbType.Int);
            parameterDepartment.Value = DeptID;
            myCommand.Parameters.Add(parameterDepartment);

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.NVarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Value = UserName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            parameterEmail.Value = Email;
            myCommand.Parameters.Add(parameterEmail);

            SqlParameter parameterContactNo = new SqlParameter("@ContactNo", SqlDbType.NVarChar, 50);
            parameterContactNo.Value = ContactNo;
            myCommand.Parameters.Add(parameterContactNo); 
            
            SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = Encrypt(Password);
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterAllBranch = new SqlParameter("@AllBranch", SqlDbType.Bit);
            parameterAllBranch.Value = allBranch;
            myCommand.Parameters.Add(parameterAllBranch);

            //SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            //parameterEnteredBy.Value = EnteredBy;
            //myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterEnteredBy = new SqlParameter("@EditingUserID", SqlDbType.Int);
            parameterEnteredBy.Value = EnteredBy;
            myCommand.Parameters.Add(parameterEnteredBy);

            SqlParameter parameterEditorsRole = new SqlParameter("@EditorsRole", SqlDbType.VarChar, 50);
            parameterEditorsRole.Value = EditorRole;
            myCommand.Parameters.Add(parameterEditorsRole);



            SqlParameter parameterEditorsIP = new SqlParameter("@EditorsIP", SqlDbType.VarChar, 50);
            parameterEditorsIP.Value = EditorsIP;
            myCommand.Parameters.Add(parameterEditorsIP);

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterSubBranchD = new SqlParameter("@SubBranchCD", SqlDbType.VarChar, 4);
            parameterSubBranchD.Value = (object)subBranchCD ?? DBNull.Value;
            myCommand.Parameters.Add(parameterSubBranchD);

            SqlParameter parameterUserInsertMessage = new SqlParameter("@UserInsertMessage", SqlDbType.VarChar, 200);
            parameterUserInsertMessage.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserInsertMessage);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            dbMessage = parameterUserInsertMessage.Value.ToString();
            myConnection.Close();
            return (int)parameterUserID.Value;
        }
        public void UpdateSingleUser(int UserID, int ZoneID, string RoutingNo, String LoginID, String UserName, int DeptID, int StatusID, bool AllBranch, String Email,
            String ContactNo, int EditingUserID,   string IPAddress, string subBranchCD)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UpdateSingleUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterZoneID = new SqlParameter("@ZoneID", SqlDbType.Int);
            parameterZoneID.Value = ZoneID;
            myCommand.Parameters.Add(parameterZoneID);


            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.VarChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.VarChar, 50);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameterUserName.Value = UserName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterDepartment = new SqlParameter("@DeptID", SqlDbType.Int);
            parameterDepartment.Value = DeptID;
            myCommand.Parameters.Add(parameterDepartment);

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterAllBranch = new SqlParameter("@AllBranch", SqlDbType.Bit);
            parameterAllBranch.Value = AllBranch;
            myCommand.Parameters.Add(parameterAllBranch);

            SqlParameter parameterEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            parameterEmail.Value = Email;
            myCommand.Parameters.Add(parameterEmail);

            SqlParameter parameterContactNo = new SqlParameter("@ContactNo", SqlDbType.NVarChar, 50);
            parameterContactNo.Value = ContactNo;
            myCommand.Parameters.Add(parameterContactNo);

            SqlParameter parameterEditingUserID = new SqlParameter("@EditingUserID", SqlDbType.Int);
            parameterEditingUserID.Value = EditingUserID;
            myCommand.Parameters.Add(parameterEditingUserID);

         

            //SqlParameter parameterEnteredBy = new SqlParameter("@EnteredBy", SqlDbType.Int, 4);
            //parameterEnteredBy.Value = EnteredBy;
            //myCommand.Parameters.Add(parameterEnteredBy);

           

        

            //SqlParameter parameterEditingLoginID = new SqlParameter("@EditorLoginID", SqlDbType.VarChar, 50);
            //parameterEditingLoginID.Value = EditingLoginID;
            //myCommand.Parameters.Add(parameterEditingLoginID);

            SqlParameter parameterIPAddress = new SqlParameter("@EditorsIP", SqlDbType.NVarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterSubBranchD = new SqlParameter("@SubBranchCD", SqlDbType.VarChar, 4);
            parameterSubBranchD.Value = (object)subBranchCD ?? DBNull.Value;
            myCommand.Parameters.Add(parameterSubBranchD);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

        }
        public string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }
        public DataTable SearchUserHistory(int UserID, int Month, int Year)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_SearchUserHistory", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.SelectCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterMonth = new SqlParameter("@Month", SqlDbType.Int, 4);
            parameterMonth.Value = Month;
            myCommand.SelectCommand.Parameters.Add(parameterMonth);

            SqlParameter parameterYearID = new SqlParameter("@Year", SqlDbType.Int, 4);
            parameterYearID.Value = Year;
            myCommand.SelectCommand.Parameters.Add(parameterYearID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);
            return dt;
        }
        public void LogOut(int UserID, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_LogOut", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress); 
            
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }
    }
}


    

