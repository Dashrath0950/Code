using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ResearchLayers;

public partial class RSM_Reports_RSM_ObservationAndActionReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindcommittee();
            BindDepartment();
            FillSession();
        }
    }

    protected void D_ddlCommitte_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (D_ddlCommitte.SelectedIndex != 0)
        {
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("userid");
            dt.Columns.Add("locid");
            dt.Columns.Add("pk_acid");
            dt.Columns.Add("deptid");
            dt.TableName = "userid";
            DataRow dr = dt.NewRow();
            dr["userid"] = Session["UserID"].ToString();
            dr["locid"] = Session["LocationID"].ToString();
            dr["pk_acid"] = Convert.ToInt32(D_ddlCommitte.SelectedValue.ToString());
            dr["deptid"] = D_ddldname.SelectedIndex == 0 ? "" : D_ddldname.SelectedValue.ToString();
            dt.Rows.Add(dr);
            ds1.Tables.Add(dt);
            D_ddlResid.Items.Clear();

            DataSet ds = SPs.RSM_BindDropDown_ALL(ds1.GetXml(), 15).GetDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                D_ddlResid.DataSource = ds;
                D_ddlResid.DataTextField = "Research_title";
                D_ddlResid.DataValueField = "pk_research_ID";
                D_ddlResid.DataBind();
                D_ddlResid.Items.Insert(0, "-- Select Research Title  --");
                D_ddlResid.SelectedIndex = 0;
            }
            else
            {
                D_ddlResid.DataSource = null;
                D_ddlResid.DataBind();
                D_ddlResid.Items.Insert(0, "-- Select Research Title  --");
                D_ddlResid.SelectedIndex = 0;
            }
        }
    }

    protected void D_ddldname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (D_ddlCommitte.SelectedIndex != 0)
        {
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("userid");
            dt.Columns.Add("locid");
            dt.Columns.Add("pk_acid");
            dt.TableName = "userid";
            dt.Columns.Add("deptid");
            DataRow dr = dt.NewRow();
            dr["userid"] = Session["UserID"].ToString();
            dr["locid"] = Session["LocationID"].ToString();
            dr["pk_acid"] = Convert.ToInt32(D_ddlCommitte.SelectedValue.ToString());
            dr["deptid"] = D_ddldname.SelectedIndex == 0 ? "" : D_ddldname.SelectedValue.ToString();
            dt.Rows.Add(dr);
            ds1.Tables.Add(dt);
            D_ddlResid.Items.Clear();

            DataSet ds = SPs.RSM_BindDropDown_ALL(ds1.GetXml(), 15).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                D_ddlResid.DataSource = ds;
                D_ddlResid.DataTextField = "Research_title";
                D_ddlResid.DataValueField = "pk_research_ID";
                D_ddlResid.DataBind();
                D_ddlResid.Items.Insert(0, "-- Select Research Title  --");
                D_ddlResid.SelectedIndex = 0;
            }
            else
            {
                D_ddlResid.DataSource = null;
                D_ddlResid.DataBind();
                D_ddlResid.Items.Insert(0, "-- Select Research Title  --");
                D_ddlResid.SelectedIndex = 0;
            }

        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        ReportDocument ObjrptDoc = new ReportDocument();
        try
        {
            DataSet dsmst = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "RSM_Status";
            dt.Columns.Add("pk_roid");
            dt.Columns.Add("fk_Cid");
            dt.Columns.Add("year");
            dt.Columns.Add("deptId");
           
            DataRow dr = dt.NewRow();
            dr["pk_roid"] = D_ddlResid.SelectedIndex == 0 ? "" : D_ddlResid.SelectedValue;
            dr["fk_Cid"] = D_ddlCommitte.SelectedIndex == 0 ? "" : D_ddlCommitte.SelectedValue;
            dr["year"] = D_ddlyear.SelectedIndex == 0 ? "" : D_ddlyear.SelectedValue;
            dr["deptId"] = D_ddldname.SelectedIndex == 0 ? "" : D_ddldname.SelectedValue;
            
            dt.Rows.Add(dr);
            dsmst.Merge(dt);
            string xml = dsmst.GetXml();

            DataSet ds = RSM_ObservationActiontaken_dataReport(xml).GetDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml(Server.MapPath("~/XMLReports/RSM_ObservationActiontaken_dataReport.xml"));
                string filename = Server.MapPath("~/RSM/Reports/RSM_ObservationActiontaken_dataReport.rpt");
                ObjrptDoc.Load(filename);

                ObjrptDoc.SetDataSource(ds);
                ObjrptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Observation Action");
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

            ClientMessaging(ex.Message);
        }
        finally
        {
            ObjrptDoc.Close();
            ObjrptDoc.Dispose();
        }
    }

    protected void ClientMessaging(string msg)
    {
        //Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        // Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    public static StoredProcedure RSM_ObservationActiontaken_dataReport(string Doc)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_ObservationActiontaken_dataReport", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Doc", Doc, DbType.String);
        return sp;
    }

    private void BindDepartment()
    {
        DataSet ds1 = SPs.RSM_BindDropDown_ALL(2).GetDataSet();
        if (ds1.Tables[8].Rows.Count > 0)
        {
            D_ddldname.DataSource = ds1.Tables[8];
            D_ddldname.DataValueField = "pk_rdid";
            D_ddldname.DataTextField = "RDname";
            D_ddldname.DataBind();
            D_ddldname.Items.Insert(0, "-- Select Department/Section Name--");
        }
    }


    public void FillSession()
    {
        DataSet ds = IUMSNXG.SP.SMS_SP_AcaSession_Mst_SelAll().GetDataSet();
        D_ddlyear.DataSource = ds;
        D_ddlyear.DataTextField = "sessionname";
        D_ddlyear.DataValueField = "pk_sessionid";
        D_ddlyear.DataBind();
        D_ddlyear.Items.Insert(0, new ListItem("-- Select Year --", "0"));

    }

    void Bindcommittee()
    {
        D_ddlResid.Items.Clear();
        D_ddlResid.Items.Insert(0, "-- Select Research Title  --");
        D_ddlResid.SelectedIndex = 0;
        DataTable dt = new DataTable();

        IDataReader idr = SPs.UM_SP_DepartmentUser_SelForGrid("C", Session["empID"].ToString().Trim()).GetReader();
        dt.Load(idr);
        idr.Dispose();
        idr.Close();

        D_ddlCommitte.DataSource = dt;
        D_ddlCommitte.DataTextField = "committee";
        D_ddlCommitte.DataValueField = "pk_committeeid";
        D_ddlCommitte.DataBind();
        D_ddlCommitte.Items.Insert(0, "-- Select Committee --");
        D_ddlCommitte.SelectedIndex = 0;

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Bindcommittee();
        BindDepartment();
        FillSession();
    }

}