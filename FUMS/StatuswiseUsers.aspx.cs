using System;
using System.Web.UI.WebControls;
using FloraSoft.Cps.UserManager.BLL;
using System.Configuration;
using ClosedXML.Excel;
using System.IO;
using System.Data;

namespace FloraSoft.Cps.UserManager
{
    public partial class StatuswiseUsers : System.Web.UI.Page
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
            if (Request.QueryString["StatusID"] != null)
            {
                int StatusID = Int32.Parse(Request.QueryString["StatusID"]);
                BindData(StatusID);
            }
        }
        private void BindData(int StatusID)
        {
            ReportBLL repBLL = new ReportBLL();

            gvwReport.DataSource = repBLL.UserReport(Int32.Parse(BranchList.SelectedValue), StatusID, 0);
            gvwReport.DataBind();

        }
        private void BindLists()
        {
                BranchesDB db = new BranchesDB();
                BranchList.DataSource = db.GetBranches();
                BranchList.DataBind();
                BranchList.Items.Add(new ListItem("All", "0"));

                if (Request.QueryString["BranchID"] != null)
                {
                    BranchList.SelectedValue = Request.QueryString["BranchID"];
                }
                else
                {
                    BranchList.SelectedValue = "0";
                }
               
        }

        protected void btnActive_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatuswiseUsers.aspx?StatusID=1&BranchID="+BranchList.SelectedValue);
        }

        protected void btnInactive_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatuswiseUsers.aspx?StatusID=2&BranchID=" + BranchList.SelectedValue);
        }

        protected void btnSuspended_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatuswiseUsers.aspx?StatusID=3&BranchID=" + BranchList.SelectedValue);
        }

        protected void btnPending_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatuswiseUsers.aspx?StatusID=5&BranchID=" + BranchList.SelectedValue);
        }
        
        protected void btnDisapproved_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatuswiseUsers.aspx?StatusID=20&BranchID=" + BranchList.SelectedValue);
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["StatusID"] == null)
                return;
            int StatusID = Int32.Parse(Request.QueryString["StatusID"]);
            
            ReportBLL repBLL = new ReportBLL();
            DataTable dt = repBLL.UserReport(Int32.Parse(BranchList.SelectedValue), StatusID, 0);

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "StatuswiseReport-D" + System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".xlsx";

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

