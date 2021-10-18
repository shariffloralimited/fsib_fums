using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft.Cps.UserManager
{
    public class ZonesDB
    {
       public DataTable GetZonesByBankCode(int BankCode)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("[ACH_GetZonesByBankCode]", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterBankCode = new SqlParameter("@BankCode", SqlDbType.Int, 4);
            parameterBankCode.Value = BankCode;
            myCommand.SelectCommand.Parameters.Add(parameterBankCode);

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }

       public SqlDataReader GetZonesByUserID(int UserID)
       {
           //SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

           //SqlDataAdapter myCommand = new SqlDataAdapter("[ACH_GetZonesOfAUser]", myConnection);
           //myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

           SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
           SqlCommand myCommand = new SqlCommand("ACH_GetZonesOfAUser", myConnection);
           myCommand.CommandType = CommandType.StoredProcedure;

           SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
           parameterUserID.Value = UserID;
           myCommand.Parameters.Add(parameterUserID);

           //myConnection.Open();
           //DataTable dt = new DataTable();
           //myCommand.Fill(dt);

           //myConnection.Close();
           //myCommand.Dispose();
           //myConnection.Dispose();

           //return dt;
           myConnection.Open();
           SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
           return result;
       }
       public SqlDataReader GetZones()
       {
           SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
           SqlCommand myCommand = new SqlCommand("ACH_GetZones", myConnection);
           myCommand.CommandType = CommandType.StoredProcedure;

           myConnection.Open();
           SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
           return result;
       }
        public int GetZoneIDByRoutingNo(int RoutingNo)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetZoneIDByRoutingNo", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterRoutingNo = new SqlParameter("@RoutingNo", SqlDbType.Int, 4);
            parameterRoutingNo.Value = RoutingNo;
            myCommand.Parameters.Add(parameterRoutingNo);

            SqlParameter parameterZoneID = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameterZoneID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterZoneID);
            myConnection.Open();
            myCommand.ExecuteNonQuery();

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();

            return (int) parameterZoneID.Value;
        }

        public void InsertZone(int UserID, int ZoneID,  string IPAddress)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertUserZoneOfAUser", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = UserID;
            myCommand.Parameters.Add(parameterUserID);

            SqlParameter parameterZoneID = new SqlParameter("@ZoneID", SqlDbType.Int, 4);
            parameterZoneID.Value = ZoneID;
            myCommand.Parameters.Add(parameterZoneID);

            //SqlParameter parameterLoginID = new SqlParameter("@LoginID", SqlDbType.Int, 4);
            //parameterLoginID.Value = LoginID;
            //myCommand.Parameters.Add(parameterLoginID);

            SqlParameter parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar, 20);
            parameterIPAddress.Value = IPAddress;
            myCommand.Parameters.Add(parameterIPAddress);

            //SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            //parameterModuleID.Value = ModuleID;
            //myCommand.Parameters.Add(parameterModuleID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }
        public void DeleteZonesOfAUser(int UserID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_DeleteUserZoneOfAUser", myConnection);
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
    }
}
