using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace FloraSoft.Cps.UserManager
{
    public class ClearingHouseDB
	{
        public DataTable GetClearingHousesByBankCode(int BankCode)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);

            SqlDataAdapter myCommand = new SqlDataAdapter("ACH_GetClearingHousesByBankCode", myConnection);
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
    }
}

