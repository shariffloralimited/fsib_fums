using FloraSoft.Cps.UserManager.BLL.Objects;
using FloraSoft.Cps.UserManager.DAL;
using System.Collections.Generic;
using System.Data;

namespace FloraSoft.Cps.UserManager.BLL
{
    public class BankBLL
    {
        DataAccess data = new DataAccess();

        public List<Branch> GetBranches()
        {
            return data.ReadList<Branch>("ACH_GetBranches", CommandType.StoredProcedure);
        }
    }
}