using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloraSoft.Cps.UserManager.BLL.Objects
{
    public class User
    {
        public int UserID { get; set; }
        public int RoleCount { get; set; }
        public int RoleID { get; set; }
        public int ZoneID { get; set; }
        public int BranchID { get; set; }
        public int RoutingNo { get; set; }
        public int BankCode { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string BranchName { get; set; }
        public string BankName { get; set; }
        public string EntryHash { get; set; }
        public string BranchHash { get; set; }
        public int DaysPassed { get; set; }
    }
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int LevelID { get; set; }
    }

}