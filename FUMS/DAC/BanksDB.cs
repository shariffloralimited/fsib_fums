using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft.Cps.UserManager
{
    public class BanksDB
    {
        public DataTable GetBureauBanks()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetBureauBanks", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }
        public DataTable GetBanks()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetBanks", myConnection);
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();

            return dt;
        }
        public DataTable GetBankCodeByBankID(int BankID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("ACH_GetBankCodeByBankID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBankID = new SqlParameter("@BankID", SqlDbType.Int);
            parameterBankID.Value = BankID;
            myAdapter.SelectCommand.Parameters.Add(parameterBankID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

    }
}
