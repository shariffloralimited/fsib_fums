using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace FloraSoft.Cps.UserManager
{
    public class SubBranchDB
    {
        public void UpdateSubBranch(int subBranchID, string subBranchName, string subBranchCD, string routingNo, string roleCD, out string message)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UpdateSubBranch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSubBranchID = new SqlParameter("@SubBranchID", SqlDbType.Int);
            parameterSubBranchID.Value = subBranchID;
            myCommand.Parameters.Add(parameterSubBranchID);

            SqlParameter parameterSubBranchName = new SqlParameter("@SubBranchName", SqlDbType.VarChar, 50);
            parameterSubBranchName.Value = subBranchName;
            myCommand.Parameters.Add(parameterSubBranchName);

            SqlParameter parameterSubBranchCD = new SqlParameter("@SubBranchCD", SqlDbType.VarChar, 4);
            parameterSubBranchCD.Value = subBranchCD;
            myCommand.Parameters.Add(parameterSubBranchCD);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.Int, 9);
            parameterRoutingNo.Value = routingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterRoleCD = new SqlParameter("@RoleCD", SqlDbType.VarChar, 4);
            parameterRoleCD.Value = roleCD;
            myCommand.Parameters.Add(parameterRoleCD);

            SqlParameter parameterUpdateMessage = new SqlParameter("@UpdateMessage", SqlDbType.VarChar, 200);
            parameterUpdateMessage.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUpdateMessage);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            message = parameterUpdateMessage.Value.ToString();
        }

        public DataTable GetBranchesActive()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            // SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetBranchesActive", myConnection);
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
        public int InsertSubBranch(string subBranchName, string subBranchCD, string routingNo, string roleCD, out string insertMessage)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertSubBranch", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterSubBranchName = new SqlParameter("@SubBranchName", SqlDbType.VarChar, 50);
            parameterSubBranchName.Value = subBranchName;
            myCommand.Parameters.Add(parameterSubBranchName);

            SqlParameter parameterSubBranchCD = new SqlParameter("@SubBranchCD", SqlDbType.VarChar, 9);
            parameterSubBranchCD.Value = subBranchCD;
            myCommand.Parameters.Add(parameterSubBranchCD);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.Int, 4);
            parameterRoutingNo.Value = routingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterRoleCD = new SqlParameter("@RoleCD", SqlDbType.VarChar, 4);
            parameterRoleCD.Value = roleCD;
            myCommand.Parameters.Add(parameterRoleCD);

            SqlParameter parameterSubBranchID = new SqlParameter("@SubBranchID", SqlDbType.Int, 4);
            parameterSubBranchID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterSubBranchID);

            SqlParameter parameterInsertMessage = new SqlParameter("@InsertMessage", SqlDbType.VarChar, 200);
            parameterInsertMessage.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterInsertMessage);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            insertMessage = parameterInsertMessage.Value.ToString();
            return (int)parameterSubBranchID.Value;
        }

        public string GetRoutingNoByBranchNumonic(String BranchNumonic)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetRoutingNoByBranchNumonic", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBranchNumonic = new SqlParameter("@BranchNumonic", SqlDbType.VarChar, 4);
            parameterBranchNumonic.Value = BranchNumonic;
            myCommand.Parameters.Add(parameterBranchNumonic);

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.VarChar, 9);
            parameterRoutingNo.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoutingNo);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            return (string)parameterRoutingNo.Value;
        }

        public DataTable GetAllSubBranches()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetSubBranches", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

        public DataTable GetSubBranchesByRoutingNo(string routingNo)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetSubBranchesByRoutingNo", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterZoneID = new SqlParameter("@RoutingNo", SqlDbType.VarChar, 9);
            parameterZoneID.Value = routingNo;
            myCommand.SelectCommand.Parameters.Add(parameterZoneID);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }
    }
}