using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SubSonic;
//using IUMSNXG;
using System.Threading.Tasks;
using DataAccessLayer;
using System.IO;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;
using ResearchLayers;

public partial class RSM_Reports_RSM_TechnicalProgramRep : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    string filePath = String.Empty;
    string filename1 = String.Empty;
    string filename = String.Empty;
    string ext = String.Empty;
    string type = String.Empty;
    Byte[] bytes;
    DataSet Ds2;
    CommonFunction cmnobj = new CommonFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["cnt"] = 1;
            //genrate_structure();
            BindDropDown();
            clear();
           // fillgrid();//"0"
        }

    }


    protected void BindDropDown()
    {
        try
        {
            //DataSet ds  = SPs.RSM_BindDropDown_ALL(2).GetDataSet();
            // Research Sector

            BindDepartment();
            Bindscheme();
            DataSet ds = SPs.RSM_BindDropDown_ALL(11).GetDataSet();
            ddlyear.DataSource = ds.Tables[6];
            ddlyear.DataTextField = "description";
            ddlyear.DataValueField = "pk_yearID";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("-- Select Year --", "0"));
            ds.Dispose();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }

    }

    void Bindscheme()
    {
        try
        {
            ddl_scheme.DataSource = null;
            ddl_scheme.DataBind();
            try
            {
                if (ddl_scheme.Items.Count > 0)
                {
                    ddl_scheme.Items.Clear();
                }
            }
            catch (Exception)
            {

            }
            ddl_scheme.Items.Insert(0, "-- Select Scheme --");
            ddl_scheme.SelectedIndex = 0;
        }
        catch (Exception)
        {

        }
    }

    void Bindscheme(string deptname)
    {
        DataTable dt = new DataTable();

        IDataReader idr = SP_DepartmentUser(deptname).GetReader();
        dt.Load(idr);
        idr.Dispose();
        idr.Close();
        ddl_scheme.DataSource = dt;
        ddl_scheme.DataTextField = "HeadDescription";
        ddl_scheme.DataValueField = "pk_anc";
        ddl_scheme.DataBind();
        ddl_scheme.Items.Insert(0, "-- Select Scheme --");
        ddl_scheme.SelectedIndex = 0;
    }

    public static StoredProcedure SP_DepartmentUser(string deptname)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_BindDropDown_SchemeFromDept", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@dept_name", deptname, DbType.String);
        return sp;
    }

    void BindDepartment()
    {
        DataTable dt = new DataTable();

        IDataReader idr = SPs.UM_SP_DepartmentUser_SelForGrid("H", Session["empID"].ToString().Trim()).GetReader();
        dt.Load(idr);
        idr.Dispose();
        idr.Close();
        ddl_department.DataSource = dt;
        ddl_department.DataTextField = "description";
        ddl_department.DataValueField = "pk_deptid";
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, "-- Select Department --");
        ddl_department.SelectedIndex = 0;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        clear();
        gvRsc.SelectedIndex = -1;
        gvRsc.PageIndex = 0;
        if (ddl_department.SelectedIndex == 0 && ddl_scheme.SelectedIndex == 0)
        {
            gvRsc.DataSource = null;
            gvRsc.DataBind();
            fillgrid();//"0"

        }
        else
        {
            fillgrid();//"1"
        }

    }

    protected void clear()
    {
        Bindscheme();
        gvRsc.SelectedIndex = -1;
        ddl_department.SelectedIndex = -1;
        ddl_scheme.SelectedIndex = -1;
        D_ddlsector.SelectedIndex = -1;
        lblMsg.Text = "";
        ddlyear.SelectedIndex = -1;
    }

    protected void btnget_Click(object sender, EventArgs e)
    {

        fillgrid();//"1"
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    protected void gvRsc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRsc.SelectedIndex = -1;
        gvRsc.PageIndex = e.NewPageIndex;

        //if (ddl_department.SelectedIndex == 0 && ddl_scheme.SelectedIndex == 0)
        //    fillgrid("0");
        //else      
        clear();
        fillgrid();//"1"
    }

    void fillgrid()//string status
    {
        //if (status != "0")
        //{

        int schemeid = 0;
        string department = "";
        string Section = "0";
        if (ddl_scheme.SelectedIndex == 0 || ddl_scheme.SelectedIndex == -1)
        {
            schemeid = 0;
        }
        else
        {
            schemeid = Convert.ToInt32(ddl_scheme.SelectedValue.ToString());

        }
        if (ddl_department.SelectedIndex == 0)
        {
            department = "";
        }
        else
        {
            department = ddl_department.SelectedValue.ToString();

        }

        if (D_ddlsector.SelectedIndex == 0)
        {
            Section = "0";
        }
        else
        {
            Section = Convert.ToString(D_ddlsector.SelectedValue.ToString());

        }

        DataSet dsmst = new DataSet();
        DataTable dstrn = new DataTable();
        DataRow dr = dstrn.NewRow();
        dstrn.Columns.Add("fk_insUserID");
        dstrn.Columns.Add("Fk_Locid");
        dstrn.Columns.Add("Sectionid");
        dr["fk_insUserID"] = Session["USerid"].ToString();
        dr["Fk_Locid"] = Session["LocationID"].ToString();
        dr["Sectionid"] = Section;
        dstrn.Rows.Add(dr);
        dsmst.Merge(dstrn);
        string xml = dsmst.GetXml();

        DataSet ds = RSM_Technical_Program_Report("P", department, schemeid, 0, Session["LocationID"].ToString(), xml).GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvRsc.DataSource = ds;
            gvRsc.DataBind();
        }
        else
        {
            gvRsc.DataSource = null;
            gvRsc.DataBind();
        }
        // }
        //else
        //{
        //    gvRsc.DataSource = null;
        //    gvRsc.DataBind();
        //    DataSet ds = RSM_Technical_Program_Report("S", "", 0, 0, Session["LocationID"].ToString()).GetDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvRsc.DataSource = ds;
        //        gvRsc.DataBind();
        //        //gvRsc.PageIndex = 0;
        //    }
        //}

    }

    public DataSet getdata()
    {
        DataSet dsMain = new DataSet();
        dsMain.Tables.Add("Table");
        dsMain.Tables[0].Columns.Add("fk_reserchid");
        CheckBox chkbx = new CheckBox();
        DataRow dr = dsMain.Tables[0].NewRow();
        foreach (GridViewRow gr in gvRsc.Rows)
        {
            chkbx = (CheckBox)gr.FindControl("chk");
            HiddenField hf = (HiddenField)gr.FindControl("hf_reserchid");
            if (chkbx.Checked == true)
            {
                dr = dsMain.Tables[0].NewRow();
                dr["fk_reserchid"] = hf.Value;
                dsMain.Tables[0].Rows.Add(dr);
            }
        }

        return dsMain;

    }

    protected void gvRsc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().Trim().ToUpper() == "SELECT")
        {


        }
        else if (e.CommandName.ToString().Trim().ToUpper() == "DELETEREC")
        {


        }
        else if (e.CommandName.ToString().Trim().ToUpper() == "PRINTREC")
        {
            ReportDocument ObjrptDoc = new ReportDocument();
            string pk_research_ID = e.CommandArgument.ToString();
            DataSet ds = new DataSet();
            DataSet dsmst = getdata();
            string xml = dsmst.GetXml();
            ds = RSM_Technical_Program_Reportupdated("P", ddl_department.SelectedValue, D_ddlsector.SelectedValue,
                Convert.ToInt32(pk_research_ID), Session["LocationID"].ToString(), xml, "", ddlyear.SelectedValue).GetDataSet();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                ClientMessaging("No details found!!");
                return;
            }
            ds.Tables[0].TableName = "RSM_Technical_work_Details";

            ds.WriteXml(Server.MapPath("~/XMLReports/RSM_Technical_work_Details.xml"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                string filename = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    filename = Server.MapPath("~/RSM/Reports/RSM_Technical_work_Details.rpt");

                }

                ObjrptDoc.Load(filename);
                ObjrptDoc.SetDataSource(ds);
                ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Technical Programme");
                ObjrptDoc.Close();
                ObjrptDoc.Dispose();

            }
            else
            {
                ClientMessaging("No Details Found!");
            }

        }

    }

    public static StoredProcedure RSM_Technical_Program_Report(string status, string pk_deptid, int pk_Scid, int pk_resid, string Fk_Locid, string xml)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_Technical_Program_Report", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@status", status, DbType.String);
        sp.Command.AddParameter("@pk_deptid", pk_deptid, DbType.String);
        sp.Command.AddParameter("@fk_Schemeid", pk_Scid, DbType.Int32);
        sp.Command.AddParameter("@pk_research_ID", pk_resid, DbType.Int32);
        sp.Command.AddParameter("@Fk_Locid", Fk_Locid, DbType.String);
        sp.Command.AddParameter("@doc", xml, DbType.String);
        return sp;
    }

    public static StoredProcedure RSM_Technical_Program_Reportupdated(string status, string pk_deptid, string pk_Scid, int pk_resid, string Fk_Locid, string xml, string columnName,
        string hodyear)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_Technical_Program_Reportupdated", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@status", status, DbType.String);
        sp.Command.AddParameter("@pk_deptid", pk_deptid, DbType.String);
        sp.Command.AddParameter("@fk_Schemeid", pk_Scid, DbType.String);
        sp.Command.AddParameter("@pk_research_ID", pk_resid, DbType.Int32);
        sp.Command.AddParameter("@Fk_Locid", Fk_Locid, DbType.String);
        sp.Command.AddParameter("@Doc", xml, DbType.String);
        sp.Command.AddParameter("@columnNames", columnName, DbType.String);
        sp.Command.AddParameter("@HOD_Year", hodyear, DbType.String);
        return sp;
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        Anthem.CheckBox chkAll = (Anthem.CheckBox)gvRsc.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow gr in gvRsc.Rows)
        {
            Anthem.CheckBox chk = (Anthem.CheckBox)gr.FindControl("chk");
            if (!chk.Checked)
                chkAll.Checked = false;
        }
    }

    protected void btnprnt_Click1(object sender, EventArgs e)
    {
        ReportDocument ObjrptDoc = new ReportDocument();
        try
        {
            string columnNames = "";
            foreach (System.Web.UI.WebControls.ListItem item in chkList.Items)
            {
                if (item.Selected == true)
                {
                    if (columnNames == "")
                        columnNames = columnNames + item.Value;
                    else
                    {
                        if (item.Text == "All")
                        {
                            columnNames = columnNames + ", " + item.Value + ",PInvest,Research_Start_Date,T_Details,objective";
                        }
                        else if (item.Text == "Objective Results Year wise")
                        {
                            columnNames = columnNames + ", " + item.Value + "";
                        }
                        else
                            columnNames = columnNames + ", " + item.Value;
                    }
                }
            }

            DataSet ds = new DataSet();
            DataSet dsmst = getdata();
            string xml = dsmst.GetXml();

            int res = 0;

            for (int i = 0; i < gvRsc.Rows.Count; i++)
            {
                Anthem.CheckBox chkDtl = gvRsc.Rows[i].FindControl("chk") as Anthem.CheckBox;
                if (chkDtl.Checked)
                {
                    res = 1;
                    break;
                }
            }

            if (res == 0)
            {
                ClientMessaging("Please Select Atleast One Research Title!");
                return;
            }
            ds = RSM_Technical_Program_Reportupdated("P", ddl_department.SelectedValue, D_ddlsector.SelectedValue, 0,
                Session["LocationID"].ToString(), xml, columnNames, ddlyear.SelectedValue).GetDataSet();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                ClientMessaging("No details found!!");
                return;
            }

            ds.Tables[0].TableName = "RSM_Technical_work_Details";
            ds.WriteXml(Server.MapPath("~/XMLReports/RSM_Technical_work_Details.xml"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                string filename = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    filename = Server.MapPath("~/RSM/Reports/RSM_Technical_work_Details.rpt");

                }

                ObjrptDoc.Load(filename);
                ObjrptDoc.SetDataSource(ds);

                if (rblPrintSelection.SelectedValue == "W")
                {

                    ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Technical Programme");
                }
                else if (rblPrintSelection.SelectedValue == "P")
                {

                    ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Technical Programme");
                }
                else
                {
                    ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook, Response, true, "Technical Programme");
                }

                ObjrptDoc.Close();
                ObjrptDoc.Dispose();
            }
            else
            {
                ClientMessaging("No Details Found!");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
        finally
        {
            ObjrptDoc.Close();
            ObjrptDoc.Dispose();
        }
    }

    protected void chkList_SelectedIndexChanged(object sender, EventArgs e)
    {

        foreach (System.Web.UI.WebControls.ListItem item1 in chkList.Items)
        {
            bool chk = false;
            if (item1.Value == "col")
            {
                chk = item1.Selected;

                foreach (System.Web.UI.WebControls.ListItem item in chkList.Items)
                {
                    if (item.Value == "objective" || item.Value == "POW")
                    {
                        item.Selected = chk;
                    }
                }
            }
        }

    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dts = new DataTable();
        IDataReader idr = SPs.RSM_ResearchSector_ALL("", 0, 5, ddl_department.SelectedValue.ToString()).GetReader();
        dts.Load(idr);
        idr.Dispose();
        idr.Close();
        if (dts.Rows.Count > 0)
        {
            D_ddlsector.Items.Clear();
            D_ddlsector.DataSource = dts;
            D_ddlsector.DataValueField = "pk_rsid";
            D_ddlsector.DataTextField = "RSname";
            D_ddlsector.DataBind();
            D_ddlsector.Items.Insert(0, new ListItem("--Select Section--", "0"));

        }
        else
        {
            D_ddlsector.Items.Clear();
            D_ddlsector.DataSource = null;
            D_ddlsector.DataBind();
            D_ddlsector.Items.Insert(0, new ListItem("--Select Section--", "0"));
            D_ddlsector.SelectedIndex = 0;

        }
        Bindscheme(Convert.ToString(ddl_department.SelectedValue));

    }

    //protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataTable dts = new DataTable();
    //    IDataReader idr = Get_Hod_Sectiondepartment_report(Convert.ToString(Session["UserID"]).Trim(), ddl_department.SelectedValue.ToString()).GetReader();
    //    dts.Load(idr);
    //    idr.Dispose();
    //    idr.Close();
    //    if (dts.Rows.Count > 0)
    //    {
    //        D_ddlsector.Items.Clear();
    //        D_ddlsector.DataSource = dts;
    //        D_ddlsector.DataValueField = "pk_sectionid";
    //        D_ddlsector.DataTextField = "description";
    //        D_ddlsector.DataBind();
    //        D_ddlsector.Items.Insert(0, new ListItem("--Select Section--", "0"));
    //    }
    //    else
    //    {
    //        D_ddlsector.Items.Clear();
    //        D_ddlsector.DataSource = null;
    //        D_ddlsector.DataBind();
    //        D_ddlsector.Items.Insert(0, new ListItem("--Select Section--", "0"));
    //        D_ddlsector.SelectedIndex = 0;
    //    }
    //    Bindscheme(Convert.ToString(ddl_department.SelectedValue));
    //}

    private StoredProcedure Get_Hod_Sectiondepartment_report(string userid, string departmentid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Get_Hod_Sectiondepartment_report", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@userid", userid, DbType.String);
        sp.Command.AddParameter("@deptid", departmentid, DbType.String);
        return sp;
    }

    protected void btnprnt_Click(object sender, EventArgs e)
    {
        ReportDocument ObjrptDoc = new ReportDocument();
        string columnNames = "";
        foreach (System.Web.UI.WebControls.ListItem item in chkList.Items)
        {
            if (item.Selected == true)
            {
                if (columnNames == "")
                    columnNames = columnNames + item.Value;
                else
                {
                    if (item.Text == "All")
                    {
                        columnNames = columnNames + ", " + item.Value + ",PInvest,Research_Start_Date,T_Details,objective";
                    }
                    else if (item.Text == "Objective Results Year wise")
                    {
                        columnNames = columnNames + ", " + item.Value + "";
                    }
                    else
                        columnNames = columnNames + ", " + item.Value;
                }
            }
        }

        DataSet ds = new DataSet();
        DataSet dsmst = getdata();
        string xml = dsmst.GetXml();
        int res = 0;

        for (int i = 0; i < gvRsc.Rows.Count; i++)
        {
            Anthem.CheckBox chkDtl = gvRsc.Rows[i].FindControl("chk") as Anthem.CheckBox;
            if (chkDtl.Checked)
            {
                res = 1;
                break;
            }
        }

        if (res == 0)
        {
            ClientMessaging("Please Select Atleast One Research Title!");
            return;
        }

        ds = RSM_Technical_Program_Reportupdated("P", ddl_department.SelectedValue, D_ddlsector.SelectedValue, 0, Session["LocationID"].ToString(), xml, columnNames,
            ddlyear.SelectedValue).GetDataSet();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            ClientMessaging("No details found!!");
            return;
        }
        ds.Tables[0].TableName = "RSM_Technical_work_Details";

        // Open the Document for writing
        if (rblPrintSelection.SelectedValue == "P")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string reseachcolumns = "";
                foreach (System.Web.UI.WebControls.ListItem item in chkList.Items)
                {
                    if (item.Selected == true)
                    {
                        if (item.Text == "All")
                        {
                            reseachcolumns = "r1,r2,r3,r4,r5,r6";
                            continue;
                        }
                        else
                        {
                            reseachcolumns = reseachcolumns + ", " + item.Value;
                        }
                    }
                }
                Session["RSMData"] = ds;
                Session["reseachcolumns"] = reseachcolumns;
                Response.Redirect("RSM_TechnicalProgrammerOutcome.aspx");
            }
        }
        else
        {
            ClientMessaging("Only PDF formet allowed to print this Report");
        }
    }
}
