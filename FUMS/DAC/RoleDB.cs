using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft.Cps.UserManager
{
    public class RoleDB
    {
        public SqlDataReader GetAllRoles()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetRole", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        public SqlDataReader GetAllRolesByModuleID(int ModuleID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetRoleByModuleID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = ModuleID;
            myCommand.Parameters.Add(parameterModuleID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetRolesOfAUser(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetRoleOfAUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
        public SqlDataReader GetTempRolesOfAUser(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("Temp_GetRoleOfAUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        public SqlDataReader GetRolesForLoginProceed(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetRolesForLoginProceed", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        public void DeleteRoleOfAUser(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_DeleteUserRoleOfAUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public SqlDataReader GetTransLimits(int RoleID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetTransLimits", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = RoleID;
            myCommand.Parameters.Add(parameterRoleID);


            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        public void InsertRole(int UserID, int RoleID, int ModuleID, string LoginID, string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertUserRoleOfAUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = RoleID;
            myCommand.Parameters.Add(parameterRoleID);

            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = ModuleID;
            myCommand.Parameters.Add(parameterModuleID);

            SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.VarChar, 40);
            parameterLoginID.Value = LoginID;
            myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 40);
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

