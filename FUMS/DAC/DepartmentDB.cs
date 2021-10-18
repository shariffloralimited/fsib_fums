using FloraSoft.Cps.UserManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FloraSoft.Cps.UserMgr.DAC
{
    public class DepartmentDB
    {
        public void InsertDepartment(string DeptName)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_InsertDepartment", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDeptName = new SqlParameter("@DeptName", SqlDbType.VarChar, 50);
            parameterDeptName.Value = DeptName;
            myCommand.Parameters.Add(parameterDeptName);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }
        public void UpdateDepartment(int DeptID, string DeptName)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_UpdateDepartment", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDeptID = new SqlParameter("@DeptID", SqlDbType.Int, 4);
            parameterDeptID.Value = DeptID;
            myCommand.Parameters.Add(parameterDeptID);

            SqlParameter parameterDeptName = new SqlParameter("@DeptName", SqlDbType.VarChar, 50);
            parameterDeptName.Value = DeptName;
            myCommand.Parameters.Add(parameterDeptName);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myConnection.Dispose();
            myCommand.Dispose();
        }

        internal SqlDataReader GetDepartment()
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetDepartment", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        internal SqlDataReader GetDepartmentByBranchID(int BranchID)
        {
            SqlConnection myConnection = new SqlConnection(AppVariables.ConStr);
            SqlCommand myCommand = new SqlCommand("ACH_GetDepartmentByBranchID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;

            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }
    }
}