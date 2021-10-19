using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FloraSoft.Cps.UserManager
{
    public class TransLimitDB
	{
        public DataTable GetTransactionLimit()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetTransactionLimit", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetPendingTransactionLimits()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetPendingTransactionLimits", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public void InsertTransactionLimit(string LevelName, double TransLimit, string LimitWord, string EditingLoginID, int UserID, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertTransLimit", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLevelName = new SqlParameter("@LevelName", SqlDbType.VarChar, 50);
            parameterLevelName.Value = LevelName;
            myCommand.Parameters.Add(parameterLevelName);

            SqlParameter parameterTransLimit = new SqlParameter("@TransLimit", SqlDbType.Money, 8);
            parameterTransLimit.Value = TransLimit;
            myCommand.Parameters.Add(parameterTransLimit);

            SqlParameter parameterLimitWord = new SqlParameter("@LimitWord", SqlDbType.VarChar, 50);
            parameterLimitWord.Value = LimitWord;
            myCommand.Parameters.Add(parameterLimitWord);

            SqlParameter parameterEditingLoginID = new SqlParameter("@EditingLoginID", SqlDbType.VarChar, 50);
            parameterEditingLoginID.Value = EditingLoginID;
            myCommand.Parameters.Add(parameterEditingLoginID);

            SqlParameter parameterUserID = new SqlParameter("@LoginID", SqlDbType.VarChar, 50);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterMsg = new SqlParameter("@Msg", SqlDbType.VarChar, 100);
            parameterMsg.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMsg);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public void ApproveTransactionLimit(int LevelID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_ApproveTransactionLimit", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLevelID = new SqlParameter("@LevelID", SqlDbType.Int, 4);
            parameterLevelID.Value = LevelID;
            myCommand.Parameters.Add(parameterLevelID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public void UpdateTransactionLimit(int LevelID, string LevelName, decimal TransLimit, string LimitWord, bool Approved, string RoleCD, string EditingLoginID, int UserID, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UpdateTransLimit", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@LevelID", SqlDbType.Int).Value = LevelID;

            SqlParameter parameterLevelName = new SqlParameter("@LevelName", SqlDbType.VarChar, 50);
            parameterLevelName.Value = LevelName;
            myCommand.Parameters.Add(parameterLevelName);

            SqlParameter parameterTransLimit = new SqlParameter("@TransLimit", SqlDbType.Money, 8);
            parameterTransLimit.Value = TransLimit;
            myCommand.Parameters.Add(parameterTransLimit);

            SqlParameter parameterLimitWord = new SqlParameter("@LimitWord", SqlDbType.VarChar, 50);
            parameterLimitWord.Value = LimitWord;
            myCommand.Parameters.Add(parameterLimitWord);

            SqlParameter parameterApproved = new SqlParameter("@Approved", SqlDbType.Bit);
            parameterApproved.Value = Approved;
            myCommand.Parameters.Add(parameterApproved);

            SqlParameter parameterRoleCD = new SqlParameter("@RoleCD", SqlDbType.VarChar, 4);
            parameterRoleCD.Value = RoleCD;
            myCommand.Parameters.Add(parameterRoleCD);

            SqlParameter parameterEditingLoginID = new SqlParameter("@EditingLoginID", SqlDbType.VarChar, 50);
            parameterEditingLoginID.Value = EditingLoginID;
            myCommand.Parameters.Add(parameterEditingLoginID);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.Int);
            parameterLoginID.Value = UserID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            SqlParameter parameterMsg = new SqlParameter("@Msg", SqlDbType.VarChar, 100);
            parameterMsg.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterMsg);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }
    }
}

