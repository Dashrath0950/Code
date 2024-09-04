using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ResearchLayers;

public partial class RSM_Reports_RSM_PostwiseDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
           // D_ddldepartment.Items.Insert(0, "-- Select Department --");
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    public bool ValidateRecord()
    {
        if (D_ddlReportType.SelectedIndex == 0)
        {
            D_ddlReportType.Focus();
            ClientMessaging("Please Select Report Type");
            return false;
        }

        return true;
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        ReportDocument ObjrptDoc = new ReportDocument();
        //try
        //{           
        if (ValidateRecord())
        {
            if (D_ddlReportType.SelectedValue == "staffdtl")
            {
                DataSet ds = new DataSet();
                ds = RSM_PostDetail_Rpt("", "", D_ddldepartment.SelectedIndex == 0 ? "0" : D_ddldepartment.SelectedValue, "", "", "", "", ddl_scheme.SelectedIndex == 0 ? "0" : ddl_scheme.SelectedValue, "").GetDataSet();
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    ClientMessaging("No details found!!");
                    return;
                }

                ds.Tables[0].TableName = "RSM_PostDetail";
                ds.WriteXml(Server.MapPath("~/XMLReports/RSM_PostDetail.xml"));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string filename = "";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        filename = Server.MapPath("~/RSM/Reports/RSM_PostDetail.rpt");

                    }

                    ObjrptDoc.Load(filename);
                    ObjrptDoc.SetDataSource(ds);

                    if (rblPrintSelection.SelectedValue == "E")
                    {
                        ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, "Post Detail");
                    }
                    else if (rblPrintSelection.SelectedValue == "P")
                    {
                        ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Post Detail");
                    }
                    else
                    {
                        ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Post Detail");
                    }

                    ObjrptDoc.Close();
                    ObjrptDoc.Dispose();

                }
                else
                {
                    ClientMessaging("No Details Found!");
                }
            }
            else
            {
                DataSet ds = new DataSet();
                DataSet dsmst = getdata();
                string xml = dsmst.GetXml();
                ds = RSM_GetResearchProjectDtl(dsmst.GetXml()).GetDataSet();
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    ClientMessaging("No details found!!");
                    return;
                }

                ds.Tables[0].TableName = "RSM_GetResearchProjectDtl";
                ds.WriteXml(Server.MapPath("~/XMLReports/RSM_GetResearchProjectDtl.xml"));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string filename = "";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        filename = Server.MapPath("~/RSM/Reports/RSM_ResearchProjectDetail.rpt");
                    }

                    ObjrptDoc.Load(filename);
                    ObjrptDoc.SetDataSource(ds);

                    if (rblPrintSelection.SelectedValue == "E")
                    {
                        ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "Sanctioned Budget");
                    }
                    else if (rblPrintSelection.SelectedValue == "P")
                    {
                        ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Sanctioned Budget");
                    }
                    else
                    {
                        ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Sanctioned Budget");
                    }

                    ObjrptDoc.Close();
                    ObjrptDoc.Dispose();

                }
                else
                {
                    ClientMessaging("No Details Found!");
                }
            }
        }
        //}
        //catch (Exception ex)
        //{
        //    ClientMessaging(ex.Message);
        //}
        //finally {
        //    ObjrptDoc.Close();
        //    ObjrptDoc.Dispose();
        //}
    }

    public void clear()
    {
        ddl_scheme.SelectedIndex = 0;
        D_ddldepartment.SelectedIndex = 0;
        D_ddlReportType.SelectedIndex = 0;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        clear();
    }

    public static StoredProcedure RSM_GetResearchProjectDtl(string xml)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_GetResearchProjectDtl", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@doc", xml, DbType.String);
        return sp;
    }

    public DataSet getdata()
    {
        DataSet dsMain = new DataSet();
        dsMain.Tables.Add("RSM_Status");
        dsMain.Tables[0].Columns.Add("deptid");
        dsMain.Tables[0].Columns.Add("Schemeid");
        dsMain.Tables[0].Columns.Add("Fk_Locid");

        DataRow dr = dsMain.Tables[0].NewRow();
        dr["deptid"] = D_ddldepartment.SelectedValue;
        dr["Schemeid"] = ddl_scheme.SelectedIndex == 0 ? "0" : ddl_scheme.SelectedValue;
        dr["Fk_Locid"] = Session["LocationID"].ToString();
        dsMain.Tables[0].Rows.Add(dr);

        return dsMain;

    }

    protected void BindDropDown()
    {
        try
        {
            DataSet ds = SPs.RSM_BindDropDown_ALL(2).GetDataSet();
            // Research Sector

            BindDepartment();
            Bindscheme();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }

    }

    void Bindscheme()
    {
        if (D_ddldepartment.SelectedIndex == 0)
        {
            ddl_scheme.Items.Clear();
            ddl_scheme.Items.Insert(0, "-- Select Scheme --");
        }
        else
        {
            DataTable dt = new DataTable();
            IDataReader idr = RSM_SP_SchemeByDepartmentId("GetByDEPT", 0, D_ddldepartment.SelectedIndex == 0 ? "0" : D_ddldepartment.SelectedValue, Session["empID"].ToString().Trim(), Session["LocationID"].ToString()).GetReader();
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
    }

    void BindDepartment()
    {
        //DataTable dt = new DataTable();

        //IDataReader idr  = SPs.UM_SP_DepartmentUser_SelForGrid("H", Session["empID"].ToString().Trim()).GetReader();
        //dt.Load(idr);
        //idr.Dispose();
        //idr.Close();
        //D_ddldepartment.DataSource = dt;
        //D_ddldepartment.DataTextField = "description";
        //D_ddldepartment.DataValueField = "pk_deptid";
        //D_ddldepartment.DataBind();
        //D_ddldepartment.Items.Insert(0, "-- Select Department --");
        //D_ddldepartment.SelectedIndex = 0;

        DataTable dt = new DataTable();
        IDataReader idr = RSM_SP_SchemeByDepartmentId("BindDept", 0, D_ddldepartment.SelectedIndex == 0 ? "0" : D_ddldepartment.SelectedValue, Session["UserID"].ToString().Trim(), Session["LocationID"].ToString()).GetReader();
        dt.Load(idr);
        idr.Dispose();
        idr.Close();
        D_ddldepartment.DataSource = dt;
        D_ddldepartment.DataTextField = "RDname";
        D_ddldepartment.DataValueField = "pk_rdid";
        D_ddldepartment.DataBind();
        //D_ddldepartment.SelectedIndex = 0;
        D_ddldepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
    }

    //Get Scheme by DepartmentId
    public static StoredProcedure RSM_SP_SchemeByDepartmentId(string mode, int pk_anc, string deptid, string empid, string locid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_SP_SchemeByDepartmentId", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@mode", mode, DbType.String);
        sp.Command.AddParameter("@pk_anc", pk_anc, DbType.Int32);
        sp.Command.AddParameter("@deptId", deptid, DbType.String);
        sp.Command.AddParameter("@empid", empid, DbType.String);
        sp.Command.AddParameter("@fk_locid", locid, DbType.String);
        return sp;
    }

    public static StoredProcedure RSM_PostDetail_Rpt(string fk_Controlling, string fk_ddoid, string fk_deptid, string fk_Section, string fk_desigid, string fk_gradeid, string fk_fundtypeid, string fk_anc, string empType)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_PostDetail_Rpt", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@fk_Controlling", fk_Controlling, DbType.String);
        sp.Command.AddParameter("@fk_ddoid", fk_ddoid, DbType.String);
        sp.Command.AddParameter("@fk_deptid", fk_deptid, DbType.String);
        sp.Command.AddParameter("@fk_Section", fk_Section, DbType.String);
        sp.Command.AddParameter("@fk_desigid", fk_desigid, DbType.String);
        sp.Command.AddParameter("@fk_gradeid", fk_gradeid, DbType.String);
        sp.Command.AddParameter("@fk_fundtypeid", fk_fundtypeid, DbType.String);
        sp.Command.AddParameter("@fk_anc", fk_anc, DbType.String);
        sp.Command.AddParameter("@EmpType", empType, DbType.String);
        return sp;
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindscheme();
    }

    //private void excel(DataSet ds)
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //    Response.Charset = "";
    //    Response.AddHeader("content-disposition", "attachment;filename=Student Report.xls");
    //    StringWriter sWriter = new StringWriter();
    //    HtmlTextWriter hWriter = new HtmlTextWriter(sWriter);

    //    hWriter.Write("<table border='0' width=99% cellpadding='0' cellspacing='0' class='reportMainTable'>");

    //    hWriter.Write("<tr><td colspan='7'> &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp   &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp   &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp <b> CHAUDHARY CHARAN SINGH HARYANA AGRICULTURAL UNIVERSITY </b></td></tr>");
    //    hWriter.Write("<tr><td colspan='7'> &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp   &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp <b> CCSHAU, HISAR</b></td></tr>");


    //        designation specialization  Remarks grade   fundtype Headcode    posts filledposts Vacentpost pk_mapid    TempPost empname HeadDescription Discipline  fk_locid fk_anc  isteaching


    //    ds.Tables[0].Columns["CID"].ColumnName = "S.No.";
    //    ds.Tables[0].Columns["ddo"].ColumnName = "Admission No.";
    //    ds.Tables[0].Columns["location"].ColumnName = "Registration Number";
    //    ds.Tables[0].Columns["section"].ColumnName = "DOB";
    //    ds.Tables[0].Columns["gender"].ColumnName = "Gender";
    //    ds.Tables[0].Columns["seatype"].ColumnName = "Seat Type";
    //    ds.Tables[0].Columns["Name"].ColumnName = "Student Name";
    //    ds.Tables[0].Columns["fname"].ColumnName = "Father_Name";
    //    ds.Tables[0].Columns["mname"].ColumnName = "Mother_Name";
    //    ds.Tables[0].Columns["Branchname"].ColumnName = "Department";
    //    ds.Tables[0].Columns["category"].ColumnName = "Category";


    //    GridView GridView1 = new GridView();
    //    GridView1.DataSource = ds.Tables[0];
    //    GridView1.DataBind();
    //    string style = @"<style> TD { mso-number-format:\@; } </style>";
    //    Response.Write(style);
    //    GridView1.RenderControl(hWriter);
    //    Response.Output.Write(sWriter.ToString());
    //    Response.Flush();
    //    Response.End();

    //}

    protected void D_ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (D_ddlReportType.SelectedValue == "staffdtl")
        {
            lblrequireddept.Visible = true;
            lblrequiredScheme.Visible = true;
        }
        else
        {
            lblrequireddept.Visible = false;
            lblrequiredScheme.Visible = false;
        }

    }

}