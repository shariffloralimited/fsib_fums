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

        public DataTable UserReport(int BranchID, string UserStatus, int RoleID)
        {
            return data.GetdataTable("Usr_RepUserList", CommandType.StoredProcedure,
                data.CreateParameter("BranchID", BranchID),
                data.CreateParameter("UserStatus", UserStatus),
                data.CreateParameter("RoleID", RoleID));
        }
        public DataTable AuditLogReport(int BranchID, DateTime BeginDate, DateTime EndDate, string LoginID)
        {
            return data.GetdataTable("ACH_GetAuditLog", CommandType.StoredProcedure,
                data.CreateParameter("BranchID", BranchID),
                  data.CreateParameter("BeginDate", BeginDate),
                data.CreateParameter("EndDate", EndDate),
                data.CreateParameter("LoginID", LoginID));
        }
    }
}