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

        public void UpdateTransactionLimit(int LevelID, string LowerLimit, string UpperLimit)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UpdateTransactionLimit", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterLevelID = new SqlParameter("@LevelID", SqlDbType.Int, 4);
            parameterLevelID.Value = LevelID;
            myCommand.Parameters.Add(parameterLevelID);

            SqlParameter parameterLowerLimit = new SqlParameter("@LowerLimit", SqlDbType.VarChar, 50);
            parameterLowerLimit.Value = LowerLimit;
            myCommand.Parameters.Add(parameterLowerLimit);

            SqlParameter parameterUpperLimit = new SqlParameter("@UpperLimit", SqlDbType.VarChar, 50);
            parameterUpperLimit.Value = UpperLimit;
            myCommand.Parameters.Add(parameterUpperLimit);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        public void InsertTransactionLimit(string LevelName, double TransLimit, string LimitWord)
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

        internal void UpdateTransactionLimit(int LevelID, string LevelName, decimal TransLimit, string LimitWord)
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

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }
    }
}

