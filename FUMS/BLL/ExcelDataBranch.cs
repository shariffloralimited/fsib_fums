using System.Data;
using FloraSoft.Cps.UserManager;

namespace FloraSoft.Cps.UserMgr.BLL
{
    public class ExcelDataBranch
    {
        private string excelFilePath;
        public string ExcelFilePath
        {
            get { return excelFilePath; }
        }

        public ExcelDataBranch(string ExcelFilePath)
        {
            this.excelFilePath = ExcelFilePath;
        }

        public DataTable EntryData()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = InsertBranch();
            }
            catch
            {

            }
            finally
            {
                System.IO.File.Delete(this.excelFilePath);
            }
            return dt;
        }

        private DataTable InsertBranch()
        {
            FloraSoft.Cps.UserMgr.component.ExcelDB excelDB = new FloraSoft.Cps.UserMgr.component.ExcelDB();
            DataTable data = excelDB.GetData(this.excelFilePath);

            BranchesDB branchesDB = new BranchesDB();
            foreach (DataRow row in data.Rows)
            {
                string routingNumber = row["ROUTINGNO"].ToString();
                string branchName = row["BRANCH NAME"].ToString();

                branchesDB.InsertBranchForBulk(branchName, routingNumber);
            }
            return data;
        }
    }
}
