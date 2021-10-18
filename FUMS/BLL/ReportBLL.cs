using FloraSoft.Cps.UserManager.BLL.Objects;
using FloraSoft.Cps.UserManager.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace FloraSoft.Cps.UserManager.BLL
{
    public class ReportBLL
    {
        DataAccess data = new DataAccess();

        public DataTable UserReport(int BranchID, int UserStatus, int RoleID)
        {
            return data.GetdataTable("Usr_RepUserList", CommandType.StoredProcedure,
                data.CreateParameter("BranchID", BranchID),
                data.CreateParameter("StatusID", UserStatus),
                data.CreateParameter("RoleID", RoleID));
        }
        public DataTable UserReportDept(int BranchID, int UserStatus, int RoleID)
        {
            return data.GetdataTable("Usr_RepUserListDept", CommandType.StoredProcedure,
                data.CreateParameter("DeptID", BranchID),
                data.CreateParameter("StatusID", UserStatus),
                data.CreateParameter("RoleID", RoleID));
        }
        public DataTable AuditLogReport(string RoutingNo, DateTime BeginDate, DateTime EndDate, string LoginID)
        {
            return data.GetdataTable("ACH_GetAuditLog", CommandType.StoredProcedure,
                data.CreateParameter("RoutingNo", RoutingNo),
                  data.CreateParameter("BeginDate", BeginDate),
                data.CreateParameter("EndDate", EndDate),
                data.CreateParameter("LoginID", LoginID));
        }
        public DataTable LoginLogReport(bool BadAttempt, DateTime BeginDate, DateTime EndDate, string LoginID)
        {
            return data.GetdataTable("ACH_GetLoginLog", CommandType.StoredProcedure,
                data.CreateParameter("BadAttempt", BadAttempt),
                  data.CreateParameter("BeginDate", BeginDate),
                data.CreateParameter("EndDate", EndDate),
                data.CreateParameter("LoginID", LoginID));
        }
        public DataTable AuditLogReportDept(int DeptID, DateTime BeginDate, DateTime EndDate, string LoginID)
        {
            return data.GetdataTable("ACH_GetAuditLogDept", CommandType.StoredProcedure,
                data.CreateParameter("DeptID", DeptID),
                  data.CreateParameter("BeginDate", BeginDate),
                data.CreateParameter("EndDate", EndDate),
                data.CreateParameter("LoginID", LoginID));
        }
    }
}