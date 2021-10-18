using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FloraSoft
{
    public class BankSettings
    {
        public string   BankName            = "";
	    public int      LoginIDMinLen		= 0;
	    public int      PasswordMinLen		= 0;
	    public int      NumOfOldPassword	= 0;
	    public bool     AllActionToChecker	= false;
	    public bool     ResetPasswordToChecker	= false;
        public bool     EnableLoginLog = false;
        public string   Modules = "";
    }
    public class BankSettingsDB
    {
        public BankSettings GetBankSettings()
        {
            SqlConnection myConnection = new SqlConnection(FloraSoft.Cps.UserManager.AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetBankSetting", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
     
            SqlParameter parameterBankName = new SqlParameter("@BankName", SqlDbType.VarChar, 40);
            parameterBankName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterBankName);

            SqlParameter parameterLoginIDMinLen = new SqlParameter("@LoginIDMinLen", SqlDbType.Int, 4);
            parameterLoginIDMinLen.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterLoginIDMinLen);

            SqlParameter parameterPasswordMinLen = new SqlParameter("@PasswordMinLen", SqlDbType.Int, 4);
            parameterPasswordMinLen.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterPasswordMinLen);

            SqlParameter parameterNumOfOldPassword = new SqlParameter("@NumOfOldPassword", SqlDbType.Int, 4);
            parameterNumOfOldPassword.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterNumOfOldPassword);

            SqlParameter parameterAllActionToChecker = new SqlParameter("@AllActionToChecker", SqlDbType.Bit);
            parameterAllActionToChecker.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterAllActionToChecker);

            SqlParameter parameterResetPasswordToChecker = new SqlParameter("@ResetPasswordToChecker", SqlDbType.Bit);
            parameterResetPasswordToChecker.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterResetPasswordToChecker);

            SqlParameter parameterEnableLoginLog = new SqlParameter("@EnableLoginLog", SqlDbType.Bit);
            parameterEnableLoginLog.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterEnableLoginLog);

            SqlParameter parameterModules = new SqlParameter("@Modules", SqlDbType.VarChar, 100);
            parameterModules.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterModules);
           
            myConnection.Open();
            myCommand.ExecuteNonQuery();

            BankSettings bs = new BankSettings();

            bs.BankName      = (string) parameterBankName.Value;
            bs.LoginIDMinLen = (int)parameterLoginIDMinLen.Value;
            bs.PasswordMinLen = (int)parameterPasswordMinLen.Value;
            bs.NumOfOldPassword = (int)parameterNumOfOldPassword.Value;

            bs.AllActionToChecker = (bool)parameterAllActionToChecker.Value;
            bs.ResetPasswordToChecker = (bool)parameterResetPasswordToChecker.Value;
            bs.EnableLoginLog = (bool)parameterEnableLoginLog.Value;
            bs.Modules = (string)parameterModules.Value;

            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();          

            return bs;
        }
    }
}