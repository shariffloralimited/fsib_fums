using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace FloraSoft.Cps.UserManager
{
	public class BranchesDB
	{
        public DataTable GetBranches()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetBranches", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }


        public DataTable GetModule()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetModule", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }


        public SqlDataReader GetBrancheByUserID(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            //SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetBranchByUserID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetBrancheByUserID", myConnection);
            //myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            //myConnection.Open();
            //DataTable dt = new DataTable();
            //myCommand.Fill(dt);
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
           
          
            return result;

            //return dt;
        }


        public DataTable GetZoneBranchesOfAUser(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetZoneBranchesOfAUser", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.SelectCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetBranchesByZoneID(int zoneID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetBranchesByZoneID", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterZoneID = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameterZoneID.Value = zoneID;
            myCommand.SelectCommand.Parameters.Add(parameterZoneID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }
        public SqlDataReader GetBranchesByBankID(int BankID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetBranchesByBankID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
            parameterBankID.Value = BankID;
            myCommand.Parameters.Add(parameterBankID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetBranchesByClearingHouseID(int ClearingHouseID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetBranchesByClearingHouseID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterClearingHouseID = new SqlParameter("@ClearingHouseID", SqlDbType.Int, 4);
            parameterClearingHouseID.Value = ClearingHouseID;
            myCommand.Parameters.Add(parameterClearingHouseID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public int InsertBranches2(int ZoneID, String BranchName, int BranchCD,int RoutingNo)
        {
            UserDB user      = new UserDB();
            string EntryHash = user.Encrypt(RoutingNo.ToString() + "AA");

            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertBranch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterZoneID = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameterZoneID.Value = ZoneID;
            myCommand.Parameters.Add(parameterZoneID);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Value = BranchName;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterBranchCD = new SqlParameter("@BranchCD", SqlDbType.SmallInt, 2);
            parameterBranchCD.Value = BranchCD;
            myCommand.Parameters.Add(parameterBranchCD); 
            
            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.Int, 4);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterEntryHash = new SqlParameter("@EntryHash", SqlDbType.NVarChar, 50);
            parameterEntryHash.Value = EntryHash;
            myCommand.Parameters.Add(parameterEntryHash); 
            
            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterBranchID.Value;
        }

        public string UpdateBranch(int BranchID, String BranchName, string RoutingNo, string BranchNumonic, string BranchCD, bool Approved, string RoleCD)
        {

            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UpdateBranch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int, 4);
            parameterBranchID.Value = BranchID;
            myCommand.Parameters.Add(parameterBranchID);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.VarChar, 50);
            parameterBranchName.Value = BranchName;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.VarChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterBranchNumonic = new SqlParameter("@BranchNumonic", SqlDbType.VarChar, 4);
            parameterBranchNumonic.Value = BranchNumonic;
            myCommand.Parameters.Add(parameterBranchNumonic);

            SqlParameter parameterBranchCD = new SqlParameter("@BranchCD", SqlDbType.VarChar, 4);
            parameterBranchCD.Value = BranchCD;
            myCommand.Parameters.Add(parameterBranchCD);

            SqlParameter parameterApproved = new SqlParameter("@Approved", SqlDbType.Bit);
            parameterApproved.Value = Approved;
            myCommand.Parameters.Add(parameterApproved);

            SqlParameter parameterRoleCD = new SqlParameter("@RoleCD", SqlDbType.VarChar, 4);
            parameterRoleCD.Value = RoleCD;
            myCommand.Parameters.Add(parameterRoleCD);
            SqlParameter parameterMessage = new SqlParameter("@Msg", SqlDbType.VarChar, 50);
            parameterMessage.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMessage);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return parameterMessage.Value.ToString();
        }
        public string InsertBranches(String BranchName, string RoutingNo, string BranchNumonic, string BranchCD, string LoginID, string IPAddress, string RoleCD)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertBranch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int, 4);
            //parameterBankID.Value = BankID;
            //myCommand.Parameters.Add(parameterBankID);

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.VarChar, 50);
            parameterBranchName.Value = BranchName;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterZoneID = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameterZoneID.Value = 0;
            myCommand.Parameters.Add(parameterZoneID);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.VarChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterBranchNumonic = new SqlParameter("@BranchNumonic", SqlDbType.VarChar, 4);
            parameterBranchNumonic.Value = BranchNumonic;
            myCommand.Parameters.Add(parameterBranchNumonic);

            SqlParameter parameterBranchCD = new SqlParameter("@BranchCD", SqlDbType.VarChar, 4);
            parameterBranchCD.Value = BranchCD;
            myCommand.Parameters.Add(parameterBranchCD);

            SqlParameter parameterEditingLoginID = new SqlParameter("@LastEditingUser", SqlDbType.VarChar, 50);
            parameterEditingLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterEditingLoginID);

            SqlParameter parameterRoleCD = new SqlParameter("@RoleCD", SqlDbType.VarChar, 4);
            parameterRoleCD.Value = RoleCD;
            myCommand.Parameters.Add(parameterRoleCD);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);


            SqlParameter parameterMsg = new SqlParameter("@Msg", SqlDbType.VarChar, 50);
            parameterMsg.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMsg);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return parameterMsg.Value.ToString();
        }
        public int InsertBranchForBulk(String BranchName, string RoutingNo)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertBranchForBulk", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchName = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
            parameterBranchName.Value = BranchName;
            myCommand.Parameters.Add(parameterBranchName);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.NChar, 9);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBranchID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (int)parameterBranchID.Value;
        }

    }
}

