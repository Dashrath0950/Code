using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SubSonic;
using CuteWebUI;
using System.IO;
using ResearchLayers;

public partial class RSM_Reports_RSM_TechnicalProgramHODReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Clear();
            Binddropdown();
        }
    }

    private void BindGrid()
    {
        try
        {
            gvresearch.DataSource = null;
            gvresearch.DataBind();
            ViewState["dt"] = null;
            DataSet dsDetails = RSM_Submission_ALL(Convert.ToString(D_ddlyear.SelectedValue)).GetDataSet();
            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                gvresearch.Visible = true;
                gvresearch.DataSource = dsDetails.Tables[0];
                gvresearch.DataBind();
                ViewState["dt"] = dsDetails.Tables[0];
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = Convert.ToString(ex.Message);
            ClientMessaging(Convert.ToString(ex.Message));
        }
    }

    void BindDepartment()
    {
        DataTable dt = new DataTable();
        IDataReader idr = SPs.UM_SP_DepartmentUser_SelForGrid("H", Session["empID"].ToString().Trim()).GetReader();
        dt.Load(idr);
        idr.Dispose();
        idr.Close();
        D_ddl_department.DataSource = dt;
        ViewState["dtDepartment"] = dt;
        D_ddl_department.DataTextField = "description";
        D_ddl_department.DataValueField = "pk_deptid";
        D_ddl_department.DataBind();
        D_ddl_department.Items.Insert(0, "-- Select Department --");
        D_ddl_department.SelectedIndex = 0;
    }

    protected void Binddropdown()
    {
        try

        {
            DataSet ds = SPs.RSM_BindDropDown_ALL(11).GetDataSet();
            DataTable dtseson = new DataTable();
            dtseson.TableName = "Season";
            dtseson.Columns.Add("season");
            dtseson.Columns.Add("pk_sessionid");

            for (int i = 1; i < 8; i++)
            {
                DataRow drseason = dtseson.NewRow();
                if (i == 1)
                {
                    drseason["season"] = "Rabi";
                }
                else if (i == 2)
                {
                    drseason["season"] = "Summer";
                }
                else if (i == 3)
                {
                    drseason["season"] = "Winter";
                }
                else if (i == 4)
                {
                    drseason["season"] = "Spring";
                }
                else if (i == 5)
                {
                    drseason["season"] = "Kharif";
                }
                else if (i == 6)
                {
                    drseason["season"] = "Kharif + Rabi";
                }
                else if (i == 7)
                {
                    drseason["season"] = "Other";
                }
                drseason["pk_sessionid"] = i;
                dtseson.Rows.Add(drseason);
            }
            ddlRSeason.DataSource = null;
            ddlRSeason.DataBind();
            ddlRSeason.DataSource = dtseson;

            ddlRSeason.DataTextField = "season";
            ddlRSeason.DataValueField = "pk_sessionid";
            ddlRSeason.DataBind();
            ddlRSeason.Items.Insert(0, "-- Select Season --");

            D_ddlyear.DataSource = ds.Tables[6];
            D_ddlyear.DataTextField = "description";
            D_ddlyear.DataValueField = "pk_yearID";
            D_ddlyear.DataBind();
            D_ddlyear.Items.Insert(0, new ListItem("-- Select Year --", "0"));
            D_ddlyear.SelectedIndex = 0;
            ds.Dispose();
            BindDepartment();

            ddl_Section.Items.Clear();
            ddl_Section.DataSource = null;
            ddl_Section.DataBind();
            ddl_Section.Items.Insert(0, new ListItem("-- Select Section --", "0"));
            ddl_Section.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void Clear()
    {
        ViewState["dt"] = null;
        gvresearch.SelectedIndex = -1;
        D_ddlyear.SelectedIndex = 0;
        lblMsg.Text = "";
        ddlRSeason.SelectedIndex = 0;
        D_ddl_department.SelectedIndex = 0;
        if (ddl_Section.Items.Count > 0)
        {
            ddl_Section.Items.Clear();
        }
        ddl_Section.Items.Insert(0, new ListItem("-- Select Section --", "0"));
        gvresearch.DataSource = null;
        gvresearch.DataBind();
    }

    #region Search

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    #endregion

    protected void D_ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dts = new DataTable();
            IDataReader idr = SPs.RSM_ResearchSector_ALL("", 0, 5, D_ddl_department.SelectedValue.ToString()).GetReader();
            dts.Load(idr);
            idr.Dispose();
            idr.Close();
            if (dts.Rows.Count > 0)
            {
                ddl_Section.Items.Clear();
                ddl_Section.DataSource = dts;
                ddl_Section.DataValueField = "pk_rsid";
                ddl_Section.DataTextField = "RSname";
                ddl_Section.DataBind();
                ddl_Section.Items.Insert(0, new ListItem("--Select Section--", "0"));
            }
            else
            {
                ddl_Section.Items.Clear();
                ddl_Section.DataSource = null;
                ddl_Section.DataBind();
                ddl_Section.Items.Insert(0, new ListItem("--Select Section--", "0"));
                ddl_Section.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public static StoredProcedure RSM_Submission_ALL(string year)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SP_RSM_Research_HODReportByDeptAndYear", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@HOD_Year", year, DbType.String);
        return sp;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton button = (LinkButton)sender;
            var selectedID = (string)button.Attributes["data-ID"];

            DataTable mainDT = ViewState["dt"] as DataTable;
            DataRow dtrow = mainDT.AsEnumerable().Where(w => Convert.ToInt64(w["Pk_ID"]) == Convert.ToInt64(selectedID)).Select(s => s).AsEnumerable().FirstOrDefault();
            DataSet ds = new DataSet();
            ds.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            string host = HttpContext.Current.Request.Url.Host;
            string filepath = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    filepath = dr["Physical_Path"].ToString().Trim();
                    filepath = filepath + "/RSM/HOD/" + "";
                    break;
                }
            }

            filepath = filepath + Convert.ToString(dtrow["uploadfileName"]);
            if (File.Exists(filepath))
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + Convert.ToString(dtrow["uploadfile"]));
                Response.TransmitFile(filepath);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                ClientMessaging(Convert.ToString("File not found!"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            ClientMessaging(Convert.ToString(ex.Message));
        }


        


    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            if (D_ddlyear.SelectedIndex == 0)
            {
                ClientMessaging("Please select year!");
                D_ddlyear.Focus();
            }
            else
            {
                BindGrid();
                DataTable dt = new DataTable();
                dt = ViewState["dt"] as DataTable;
                if (dt != null)
                {
                    DataView dv = new DataView();
                    if (D_ddl_department.SelectedIndex > 0)
                    {
                        var data = dt.AsEnumerable().Where(w => Convert.ToString(w["fk_deptid"]) == Convert.ToString(D_ddl_department.SelectedValue)).Select(s => s);
                        try
                        {
                            dv = data.AsDataView();
                            dt = dv.ToTable();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (ddl_Section.SelectedIndex > 0)
                    {
                        var data = dt.AsEnumerable().Where(w => Convert.ToString(w["fk_rsid"]) == Convert.ToString(ddl_Section.SelectedValue)).Select(s => s);
                        try
                        {
                            dv = data.AsDataView();
                            dt = dv.ToTable();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (ddlRSeason.SelectedIndex > 0)
                    {
                        var data = dt.AsEnumerable().Where(w => Convert.ToString(w["Season"]) == Convert.ToString(ddlRSeason.SelectedValue)).Select(s => s);
                        try
                        {
                            dv = data.AsDataView();
                            dt = dv.ToTable();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        gvresearch.Visible = true;
                        gvresearch.DataSource = dt;
                        gvresearch.DataBind();
                    }
                    else
                    {
                        ClientMessaging("No Record Found!");
                        gvresearch.Visible = false;
                        gvresearch.DataSource = null;
                        gvresearch.DataBind();
                    }
                }
                else
                {
                    ClientMessaging("No Record Found!");
                    gvresearch.Visible = false;
                    gvresearch.DataSource = null;
                    gvresearch.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            ClientMessaging(Convert.ToString(ex.Message));
        }
    }
}