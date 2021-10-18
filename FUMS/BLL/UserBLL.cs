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
    public class UserBLL
    {
        DataAccess data = new DataAccess();
        public List<Role> GetRoleOfAUser(int UserID)
        {
            return data.ReadList<Role>("ACH_GetRoleOfAUser", CommandType.StoredProcedure,
                data.CreateParameter("@UserID", UserID)
            );
        }
        public User UserLogin(string LoginID, int nTries)
        {
            return data.ReadSingle<User>("ACH_UserLogin1", CommandType.StoredProcedure,
                data.CreateParameter("@nTries", nTries),
                data.CreateParameter("@LoginID", LoginID)
            );
        }
        public int LockUser(string LoginID)
        {
            return data.ExecuteNonQuery("ACH_LockUser", CommandType.StoredProcedure,
                data.CreateParameter("@LoginID", LoginID)
            );
        }
        public List<Role> GetAllRoles()
        {

            return data.ReadList<Role>("ACH_GetRole", CommandType.StoredProcedure
            );
        }
        public List<Role> GetRoles(int ModuleID, int UserID)
        {

            return data.ReadList<Role>("ACH_GetUserRole", CommandType.StoredProcedure,
                data.CreateParameter("UserID", UserID),
                data.CreateParameter("ModuleID", ModuleID)
            );
        }

        public List<Role> GetTempRoles(int ModuleID, int UserID)
        {

            return data.ReadList<Role>("Temp_GetUserRole", CommandType.StoredProcedure,
                data.CreateParameter("UserID", UserID),
                data.CreateParameter("ModuleID", ModuleID)
            );
        }

        public int ApproveUserChanges(int UserID)
        {
            return data.ExecuteNonQuery("ACH_ApproveUserChanges", CommandType.StoredProcedure,
                data.CreateParameter("UserID", UserID)
            );
        }
    }
}