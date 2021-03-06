using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FloraSoft.Cps.UserManager.BLL;
using FloraSoft.Cps.UserManager;
using System.Configuration;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace FloraSoft.Cps.UserMgr
{
    public partial class LoginLog : System.Web.UI.Page
    {
        private System.Web.HttpResponse httpResponse;
        protected void Page_Init(object sender, EventArgs e)
        {
            string RoleID = Request.Cookies["RoleID"].Value;
            if ((RoleID != "99") &&(RoleID != "100"))
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FrDay.SelectedValue = System.DateTime.Today.Day.ToString();
                FrMonth.SelectedValue = System.DateTime.Today.Month.ToString();
                FrYear.SelectedValue = System.DateTime.Today.Year.ToString();

                DateTime tomorrow = System.DateTime.Today.AddDays(1);

                ToDay.SelectedValue = tomorrow.Day.ToString();
                ToMonth.SelectedValue = tomorrow.Month.ToString();
                ToYear.SelectedValue = tomorrow.Year.ToString();
            }
        }

        private DataTable GetData()
        {
            DataTable dt;

            bool BadAttempt = ChkBadAttempt.Checked;
            ReportBLL repBLL = new ReportBLL();
            DateTime BeginDate;
            DateTime EndDate;
            try
            {
                BeginDate = new DateTime(Int32.Parse(FrYear.SelectedValue), Int32.Parse(FrMonth.SelectedValue), Int32.Parse(FrDay.SelectedValue));
                EndDate = new DateTime(Int32.Parse(ToYear.SelectedValue), Int32.Parse(ToMonth.SelectedValue), Int32.Parse(ToDay.SelectedValue));
            }
            catch
            {
                BeginDate = System.DateTime.Today.AddDays(1);
                EndDate = System.DateTime.Today.AddDays(1);
            }

            dt = repBLL.LoginLogReport(BadAttempt, BeginDate, EndDate, txtLogInID.Text);

            return dt;
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            DateTime BeginDate;
            DateTime EndDate;

            try
            {
                BeginDate = new DateTime(Int32.Parse(FrYear.SelectedValue), Int32.Parse(FrMonth.SelectedValue), Int32.Parse(FrDay.SelectedValue));
                EndDate = new DateTime(Int32.Parse(ToYear.SelectedValue), Int32.Parse(ToMonth.SelectedValue), Int32.Parse(ToDay.SelectedValue));
            }
            catch
            {
                BeginDate = System.DateTime.Today.AddDays(1);
                EndDate = System.DateTime.Today.AddDays(1);
            }

            TimeSpan t = EndDate - BeginDate;
            double NrOfDays = t.TotalDays;
            if (NrOfDays > 3)
            {
                lblError.Text = "Date range can not be more than 3 days.";
                gvwReport.DataSource = null;
                gvwReport.DataBind();
                return;
            }

            gvwReport.DataSource = GetData();
            gvwReport.DataBind();
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();

            if (dt.Rows.Count > 0)
            {
                string xlsFileName = "Login_Log-D"+ System.DateTime.Today.ToString("yyyyMMdd") + "-T" + System.DateTime.Now.ToString("HHmmss") + ".xlsx";

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