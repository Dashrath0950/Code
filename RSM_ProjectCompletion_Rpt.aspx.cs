using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ResearchLayers;

public partial class RSM_Reports_RSM_ProjectCompletion_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Binddropdown();
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

                txtStartingdate.Text = "01" + DateTime.Now.ToString("/MM/yyyy").Replace("-", "/");
                txtComdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                txtrname.Text = "";
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
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        //String script = String.Format("alert('{0}');", msg);
        //Anthem.Manager.IncludePageScripts = true;
        ////Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        //Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        //if (D_ddlrtitle.SelectedIndex == 0)
        //{
        //    ClientMessaging("Research title is required");
        //    D_ddlrtitle.Focus();
        //    return;
        //}
        if (txtStartingdate.Text.Trim() == "")
        {
            ClientMessaging("Research End Date(From) is required");
            txtStartingdate.Focus();
            return;
        }
        if (txtComdate.Text.Trim() == "")
        {
            ClientMessaging("Research End Date(From) is required");
            txtComdate.Focus();
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
                    recieptviewer.ServerReport.ReportPath = "/" + ds.Tables[0].Rows[0]["Dirname"].ToString() + "/RPCAU_Reports" + "/RSM_FinalResearch_Rpt";
                else
                    recieptviewer.ServerReport.ReportPath = "/" + ds.Tables[0].Rows[0]["DirnameTest"].ToString() + "/RPCAU_Reports" + "/RSM_FinalResearch_Rpt";

                recieptviewer.ServerReport.Refresh();
                //Array size describes the number of paramaters. 
                recieptviewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                Microsoft.Reporting.WebForms.ReportParameter[] reportParameterCollection = new Microsoft.Reporting.WebForms.ReportParameter[4];
                //Give Your Parameter Name 

                reportParameterCollection[0] = new Microsoft.Reporting.WebForms.ReportParameter("fdate", CommonCode.DateFormats.Date_FrontToDB_R(txtStartingdate.Text));
                reportParameterCollection[1] = new Microsoft.Reporting.WebForms.ReportParameter("tdate", CommonCode.DateFormats.Date_FrontToDB_R(txtComdate.Text));
                reportParameterCollection[2] = new Microsoft.Reporting.WebForms.ReportParameter("fk_research_id", D_ddlrtitle.SelectedValue);
                reportParameterCollection[3] = new Microsoft.Reporting.WebForms.ReportParameter("rname", (txtrname.Text.Trim() == "") ? " " : txtrname.Text.Trim());

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
                string deviceinfo = "";
                string mimeType = "";
                string encoding = "";
                string extension = "pdf";
                string[] streams = null;

                Microsoft.Reporting.WebForms.Warning[] warnings = null;

                returnValue = recieptviewer.ServerReport.Render(format, deviceinfo,
                    out mimeType, out encoding, out extension, out streams, out warnings);

                Response.Buffer = true;

                Response.Clear();

                Response.ContentType = mimeType;

                Response.AddHeader("content-disposition", "attachment; filename=Prophylactic Measures.pdf");

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
        Binddropdown();
        lblMsg.Text = "";
    }

}