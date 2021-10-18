using System;
using System.Web.UI.WebControls;
using FloraSoft.Cps.UserManager.BLL;
using System.Configuration;
using ClosedXML.Excel;
using System.IO;
using System.Data;

namespace FloraSoft.Cps.UserManager
{
    public partial class RolewiseUsers : System.Web.UI.Page
    {
        private System.Web.HttpResponse httpResponse;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               BindLists();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            ReportBLL repBLL = new ReportBLL();
            gvwReport.DataSource = repBLL.UserReport(Int32.Parse(BranchList.SelectedValue), 0, Int32.Parse(ddlRoles.Text));

            gvwReport.DataBind();
        }
        private void BindLists()
        {
            BranchesDB db = new BranchesDB();
            BranchList.DataSource = db.GetBranches();
            BranchList.DataBind();
            BranchList.Items.Add(new ListItem("All", "0"));
            BranchList.SelectedValue = "0";

            RoleDB roledb = new RoleDB();
            ddlRoles.DataSource = roledb.GetAllRoles();
            ddlRoles.DataBind();
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ReportBLL repBLL = new ReportBLL();
            DataTable dt = repBLL.UserReport(Int32.Parse(BranchList.SelectedValue), 0, Int32.Parse(ddlRoles.Text));

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "RoleWiseReport-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".xlsx";

                XLWorkbook workbook = new XLWorkbook();
                workbook.Worksheets.Add(dt, "Sheet1");

                // Prepare the response
                httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string attachment = "attachment; filename=" + xlsFileName;
                httpResponse.AddHeader("content-disposition", attachment);

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                dt.Dispose();
                httpResponse.End();

            }
        } 
    }
}

