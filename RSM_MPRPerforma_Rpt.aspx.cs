using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ResearchLayers;

public partial class RSM_Reports_RSM_MPRPerforma_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Binddropdown();
            BindMonth();
        }
    }

    protected void Binddropdown()
    {
        try
        {
            DataSet ds = SPs.RSM_BindDropDown_ALL(10).GetDataSet();
            // Research Title
            if (ds.Tables[0].Rows.Count > 0)
            {
                D_ddlrtitle.DataSource = ds.Tables[0];
                D_ddlrtitle.DataValueField = "pk_research_ID";
                D_ddlrtitle.DataTextField = "Research_title";
                D_ddlrtitle.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void BindMonth()
    {
        try
        {
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("userid");
            dt.TableName = "userid";
            DataRow dr = dt.NewRow();
            dr["userid"] = Session["UserID"].ToString();
            dt.Rows.Add(dr);
            ds1.Tables.Add(dt);

            DataSet ds = SPs.RSM_BindDropDown_ALL(ds1.GetXml(), 11).GetDataSet();
            // Research Title

            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlyear.DataSource = ds.Tables[1];
                ddlyear.DataValueField = "pk_yearID";
                ddlyear.DataTextField = "description";
                ddlyear.DataBind();
                ddlyear.Items.Insert(0, new ListItem("-- Select Year --", "0"));
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                ddlmonth.DataSource = ds.Tables[2];
                ddlmonth.DataValueField = "pk_MonthId";
                ddlmonth.DataTextField = "descriptiion";
                ddlmonth.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (D_ddlrtitle.SelectedIndex == 0)
        {
            ClientMessaging("Research title is required");
            D_ddlrtitle.Focus();
            return;
        }
        if (ddlyear.SelectedIndex == 0)
        {
            ClientMessaging("year is required");
            ddlyear.Focus();
            return;
        }
        if (ddlmonth.SelectedIndex == 0)
        {
            ClientMessaging("month is required");
            ddlmonth.Focus();
            return;
        }

        View();

    }

    protected void View()
    {
        try
        {
            {
                DataSet ds = new DataSet();
                ds.ReadXml(HttpContext.Current.Server.MapPath("~/SSRSLINK.xml"));

                recieptviewer.Reset();
                //IReportServerCredentials irsc = new CustomReportCredentials(ds.Tables[0].Rows[0]["username"].ToString(), ds.Tables[0].Rows[0]["password"].ToString()
                //    , ds.Tables[0].Rows[0]["Domain"].ToString());
                recieptviewer.ShowPrintButton = false;
                recieptviewer.ServerReport.ReportServerCredentials = new CustomReportCredentials(ds.Tables[0].Rows[0]["username"].ToString(), ds.Tables[0].Rows[0]["password"].ToString(), ds.Tables[0].Rows[0]["Domain"].ToString());

                recieptviewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                recieptviewer.ShowParameterPrompts = false;

                recieptviewer.ServerReport.ReportServerUrl = new Uri(ds.Tables[0].Rows[0]["ReportServerUrl"].ToString());

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IUMSNXG"].ConnectionString;

                string str = builder.InitialCatalog;
                if (str.ToUpper() == ds.Tables[0].Rows[0]["dbname"].ToString())
                    recieptviewer.ServerReport.ReportPath = "/" + ds.Tables[0].Rows[0]["Dirname"].ToString() + "/RPCAU_Reports" + "/RSM_MPR_Rpt";

                else
                    recieptviewer.ServerReport.ReportPath = "/" + ds.Tables[0].Rows[0]["DirnameTest"].ToString() + "/RPCAU_Reports" + "/RSM_MPR_Rpt";

                recieptviewer.ServerReport.Refresh();
                //Array size describes the number of paramaters. 
                recieptviewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

                Microsoft.Reporting.WebForms.ReportParameter[] reportParameterCollection = new Microsoft.Reporting.WebForms.ReportParameter[3];
                //Give Your Parameter Name 

                reportParameterCollection[0] = new Microsoft.Reporting.WebForms.ReportParameter("fk_yearid", ddlyear.SelectedValue);
                reportParameterCollection[1] = new Microsoft.Reporting.WebForms.ReportParameter("fk_monthid", ddlmonth.SelectedValue);
                reportParameterCollection[2] = new Microsoft.Reporting.WebForms.ReportParameter("fk_research_id", D_ddlrtitle.SelectedValue);

                //Pass Parametrs's value here. 
                recieptviewer.ServerReport.SetParameters(reportParameterCollection);
                recieptviewer.ServerReport.Refresh();
                recieptviewer.Visible = true;

                Microsoft.Reporting.WebForms.ReportParameterInfoCollection reportParameterInfoCollection = this.recieptviewer.ServerReport.GetParameters();
                //Get each parameter information.
                foreach (Microsoft.Reporting.WebForms.ReportParameterInfo rpi in reportParameterInfoCollection)
                {
                    //Get the default value of each parameter.
                    //rpi.Values.Count > 0 means the parameter has defaul value(s). If the default values can be array.
                    if (rpi.Values.Count == 0)
                    {

                        recieptviewer.Visible = false;
                        String script = String.Format("alert('{0}');", "No Record Found");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                        //lblMsg.Text = "No Record Found";
                        return;
                        /*
                        //Store the values into a array
                        //Let assume the value is string data type.
                        string[] aryValues = new string[rpi.Values.Count];
                        for (int i = 0; i < rpi.Values.Count; i++)
                        {
                            aryValues[i] = rpi.Values[i].ToString();
                        }
                         */
                    }
                }

                #region output as PDF
                //output as PDF  
                byte[] returnValue = null;
                string format = "PDF";
                if (rdbrptformat.SelectedValue == "E")
                    format = "Excel";
                string deviceinfo = "";
                string mimeType = "";
                string encoding = "";
                string extension = "pdf";
                if (rdbrptformat.SelectedValue == "E")
                    extension = "xls";
                string[] streams = null;

                Microsoft.Reporting.WebForms.Warning[] warnings = null;

                returnValue = recieptviewer.ServerReport.Render(format, deviceinfo,
                    out mimeType, out encoding, out extension, out streams, out warnings);

                Response.Buffer = true;

                Response.Clear();

                Response.ContentType = mimeType;

                if (rdbrptformat.SelectedValue == "E")
                    Response.AddHeader("content-disposition", "attachment; filename=Monthly Progress Research Report.xls");
                else
                    Response.AddHeader("content-disposition", "attachment; filename=Monthly Progress Research Report.pdf");

                Response.BinaryWrite(returnValue);

                Response.Flush();

                Response.End();
                #endregion
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }

    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        D_ddlrtitle.SelectedIndex = 0;
        ddlmonth.SelectedIndex = 0;
        ddlyear.SelectedIndex = 0;
        rdbrptformat.SelectedValue = "P";
        lblMsg.Text = "";
    }
}
