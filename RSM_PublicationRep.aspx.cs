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
using ResearchLayers;

public partial class RSM_Reports_RSM_PublicationRep : System.Web.UI.Page
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
    ReportDocument rpt = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //clear(); 
            //Session["cnt"] = 1;
            //genrate_structure();
            BindDropDown();
            BindPubName();
        }
    }
    protected void BindPubName()
    {
        try
        {
            DataSet ds = RSM_BindPubName_TRN().GetDataSet();
            // Research Sector
            if (ds.Tables[0].Rows.Count > 0)
            {
                R_txtptitle.DataSource = ds;
                R_txtptitle.DataValueField = "pk_pbid";
                R_txtptitle.DataTextField = "Pubname";
                R_txtptitle.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void BindDropDown()
    {
        try
        {
            DataSet ds = SPs.RSM_BindDropDown_ALL(13).GetDataSet();
            // Research Title
           
                if (ds.Tables[0].Rows.Count > 0)
                {
                    D_ddlMtype.DataSource = ds.Tables[0];
                    D_ddlMtype.DataValueField = "pk_medid";
                    D_ddlMtype.DataTextField = "Media_tpe";
                    D_ddlMtype.DataBind();
                }
            
        }

        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
        //try
        //{
        //    DataSet ds = SPs.RSM_BindDropDown_ALL(2).GetDataSet();
        //    // Research Sector

        //    BindDepartment();
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = ex.Message;
        //}
    }

    void BindDepartment()
    {
        DataTable dt = new DataTable();
        IDataReader idr = SPs.UM_SP_DepartmentUser_SelForGrid("H", Session["empID"].ToString().Trim()).GetReader();
        dt.Load(idr);
        idr.Dispose();
        idr.Close();
        //ddl_department.DataSource = dt;
        //ddl_department.DataTextField = "description";
        //ddl_department.DataValueField = "pk_deptid";
        //ddl_department.DataBind();
        //ddl_department.Items.Insert(0, "-- Select Department --");
        //ddl_department.SelectedIndex = 0;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void clear()
    {
        //ddl_department.SelectedIndex = 0;
        lblMsg.Text = "";
        D_ddlMtype.SelectedIndex = 0;
        R_txtptitle.SelectedIndex = 0;
        R_txtAuthor.Text = "";
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    public static StoredProcedure RSM_Getpublication_detail(string pk_deptid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_Getpublication_detail", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_deptid", pk_deptid, DbType.String);
        return sp;
    }
    public static StoredProcedure RSM_Getpublication_detail_RptData(string Author,string Publication_title,int fk_medid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_Getpublication_detail_RptData", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Author", Author, DbType.String);
        sp.Command.AddParameter("@Publication_title", Publication_title, DbType.String);
        sp.Command.AddParameter("@fk_medid", fk_medid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure RSM_BindPubName_TRN()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("RSM_BindPubName", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }
    protected void btnget_Click(object sender, EventArgs e)
    {
        //if (R_txtAuthor.Text == "")
        //{
        //    ClientMessaging("Author is required");
        //    Anthem.Manager.IncludePageScripts = true;
        //    R_txtptitle.Focus();
        //    return;
        //}
        //if (D_ddlMtype.SelectedIndex == 0)
        //{
        //    ClientMessaging("Media type is required");
        //    Anthem.Manager.IncludePageScripts = true;
        //    R_txtptitle.Focus();
        //    return;
        //}
        //if (R_txtptitle.SelectedIndex == 0)
        //{
        //    ClientMessaging("Department is required");
        //    Anthem.Manager.IncludePageScripts = true;
        //    R_txtptitle.Focus();
        //    return;
        //}
        DataSet ds = RSM_Getpublication_detail_RptData(R_txtAuthor.Text.ToString(), R_txtptitle.SelectedValue.ToString(), Convert.ToInt32(D_ddlMtype.SelectedValue)).GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.WriteXml(Server.MapPath("~/XMLReports/RSM_publication_detail.xml"));
            string filename = Server.MapPath("RSM_PublicationRep.rpt");
            rpt.Load(filename);
            rpt.SetDataSource(ds);
            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "RSM_PublicationReport");
        }

        else
        {
            ClientMessaging("No Details Found!");
        }
        //if (ddl_department.SelectedIndex == 0)
        //{
        //    ClientMessaging("Department is required");
        //    Anthem.Manager.IncludePageScripts = true;
        //    ddl_department.Focus();
        //    return;
        //}
        // DataSet ds = RSM_Getpublication_detail(ddl_department.SelectedValue.ToString()).GetDataSet();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ds.WriteXml(Server.MapPath("~/XMLReports/RSM_publication_detail.xml"));
        //    string filename = Server.MapPath("RSM_PublicationRep.rpt");
        //    rpt.Load(filename);
        //    rpt.SetDataSource(ds);
        //    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "RSM_PublicationReport");
        //}
        //else
        //{
        //    ClientMessaging("No Details Found!");
        //}
    }

}





