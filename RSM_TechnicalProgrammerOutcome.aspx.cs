using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RSM_Reports_RSM_TechnicalProgrammerOutcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RSMData"] == null)
        {
            Response.Redirect("RSM_TechnicalProgramRep.aspx");
        }

        if (Session["RSMData"] != null)
        {
            DataSet ds = Session["RSMData"] as DataSet;
            CreateReport(ds);
        }

    }


    void CreateReport(DataSet ds)
    {
        StringBuilder str = new StringBuilder();
        string compname = "DR. RAJENDRA PRASAD CENTRAL AGRICULTURAL UNIVERSITY PUSA, SAMASTIPUR - 848125, BIHAR, INDIA";
        //string filepath = Page.ResolveUrl("~/Images/logo.png");
        string filepath = Page.ResolveUrl("~/Images/logo.jpg");
        string researchcolumns = Convert.ToString(Session["reseachcolumns"]);
        string Reporttitile = "TECHNICAL PROGRAM WORK DETAILS REPORT";

        str.Append("<table border='0' cellpadding='5' cellspacing='5' style='width:100%'>");
        str.Append("<tr><td class='vtexthrpt'  width='100' rowspan='2'><img height='60' width='62' src='" + filepath + "' alt='' /></td>");
        str.Append("<td class='vtexthrpt' valign='top' colspan='6' style><center><U>" + compname + "</U></center></td></tr>");
        str.Append("<td class='vtexthrpt' valign='top' colspan='6'><center><U>" + Reporttitile + "</U></center></td></tr>");
        try
        {
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["description"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Name of the Department </td>");
                        str.Append("<td style='padding-right:5px;height: 30px; font-weight:bold;'>" + Convert.ToString(ds.Tables[1].Rows[0]["description"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["StaffPosition"])))
                    {
                        if (researchcolumns.Contains("r1"))
                        {
                            str.Append("<td></td>");
                            str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Staff position (Provide scheme wise and post wise details) </td>");
                            str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["StaffPosition"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<tr>");
                        }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["achievements"])))
                    {
                        if (researchcolumns.Contains("r2"))
                        {
                            str.Append("<td></td>");
                            str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Achievements </td>");
                            str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["achievements"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<tr>");
                        }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["Emerging"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Emerging / thrust areas </td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["Emerging"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["ActionTaken"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "ACTION TAKEN REPORT </td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["ActionTaken"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["ResearchProjects"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Research projects  </td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["ResearchProjects"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["ResearchProjectsCompleted"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "RESEARCH PROJECTS COMPLETED  </td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["ResearchProjectsCompleted"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["ResearchProjectsSubmitted"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "RESEARCH PROJECTS SUBMITTED / SANCTIONED </td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["ResearchProjectsSubmitted"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["SchemeSummary"])))
                    {
                        if (researchcolumns.Contains("r6"))
                        {
                            str.Append("<td></td>");
                            str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Scheme-wise summary of experiments </td>");
                            str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["SchemeSummary"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<tr>");
                        }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["ListEquipment"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "List of equipment’s (last three years) </td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["ListEquipment"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["Recommendation"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Recommendation generated for field application </td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["Recommendation"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["Protection"])))
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Protection of IPR instruments</td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(ds.Tables[1].Rows[0]["Protection"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        try
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                str.Append("<td></td>");
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "DETAILS OF EACH RESEARCH SCHEME(S) FOR REVIEW:</td>");
                str.Append("</tr>");
                str.Append("<tr>");

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    if (dr["schemename"].ToString() != "")
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-right:5px;height: 30px;font-weight:bold;'>" + Convert.ToString(dr["schemename"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");

                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "No. and title of the Research Scheme</td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["schemename"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (dr["yearid"].ToString() != "")
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Year of Start</td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["yearid"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (dr["fk_locationid"].ToString() != "")
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Location</td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["fk_locationid"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (dr["objectives"].ToString() != "")
                    {
                        if (researchcolumns.Contains("r3"))
                        {
                            str.Append("<td></td>");
                            str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Objectives</td>");
                            str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["objectives"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<tr>");
                        }
                    }

                    if (dr["modifyobjectives"].ToString() != "")
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Any need to modify the objectives as per need of the state</td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["modifyobjectives"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");
                    }

                    if (dr["budget"].ToString() != "")
                    {
                        if (researchcolumns.Contains("r4"))
                        {
                            str.Append("<td></td>");
                            str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Budget for the year with head wise sanctioned and expenditure details</td>");
                            str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["budget"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<tr>");
                        }

                    }
                    if (dr["researchfindings"].ToString() != "")
                    {
                        if (researchcolumns.Contains("r5"))
                        {
                            str.Append("<td></td>");
                            str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Salient research findings of the scheme</td>");
                            str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["researchfindings"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<tr>");
                        }

                    }
                    if (dr["justification"].ToString() != "")
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Justification for continuation of the scheme</td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["justification"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");

                    }
                    if (dr["constraints"].ToString() != "")
                    {
                        str.Append("<td></td>");
                        str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Constraints, if any</td>");
                        str.Append("<td style='padding-right:5px;height: 30px;'>" + Convert.ToString(dr["constraints"]) + "</td>");
                        str.Append("</tr>");
                        str.Append("<tr>");

                    }
                }
            }

        }
        catch
        {

        }
        str.Append("<td></td>");
        str.Append("<td style='padding-right:5px;height: 30px;font-weight:bold;'>" + "Technical Programme</td>");
        str.Append("</tr>");
        str.Append("<tr>");

        int srNo = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            srNo++;
            str.Append("<tr>");
            str.Append("<td style='text-align:right;padding-right:5px;height: 30px;text-align:center;font-weight:bold;width:1%' >" + srNo + "</td>");
            string ResearchTitle = "";
            ResearchTitle = Convert.ToString(ds.Tables[0].Rows[i]["Scheme_name"]);
            if (ds.Tables[0].Rows[i]["Research_title"].ToString() != "")
            {
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold; width:29%'>" + " Title of the Experiment </td>");
                str.Append("<td style='padding-right:5px;height: 30px;'>" + ds.Tables[0].Rows[i]["Research_title"].ToString() + "</td>");
                str.Append("</tr>");
                str.Append("<tr>");
            }

            if (ds.Tables[0].Rows[i]["objective"].ToString() != "")
            {
                str.Append("<td></td>");
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Objectives of the Experiment </td>");
                str.Append("<td style='padding-right:5px;height: 30px;'>" + ds.Tables[0].Rows[i]["objective"].ToString() + "</td>");
                str.Append("</tr>");
                str.Append("<tr>");
            }

            if (ds.Tables[0].Rows[i]["PInvest"].ToString() != "")
            {
                str.Append("<td></td>");
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Name of the investigator(s) with Activity Profile </ td>");
                str.Append("<td style='padding-right:5px;height: 30px;'>" + ds.Tables[0].Rows[i]["PInvest"].ToString() + "</td>");
                str.Append("</tr>");
                str.Append("<tr>");
            }

            if (ds.Tables[0].Rows[i]["col"].ToString() != "")
            {
                str.Append("<td></td>");
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Name of the Collaborator(s) with Activity Profile </ td>");
                str.Append("<td style='padding-right:5px;height: 30px;'>" + ds.Tables[0].Rows[i]["col"].ToString() + "</td>");
                str.Append("</tr>");
                str.Append("<tr>");
            }

            if (ds.Tables[0].Rows[i]["Research_Start_Date"].ToString() != "")
            {
                str.Append("<tr><td></td>");
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Year of Start </td>");
                str.Append("<td style='padding-right:5px;height: 30px;'>" + ds.Tables[0].Rows[i]["Research_Start_Date"].ToString() + "</td>");
                str.Append("</tr>");
                str.Append("<tr>");
            }

            if (ds.Tables[0].Rows[i]["T_Details"].ToString() != "")
            {
                str.Append("<td></td>");
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Treatment Details </td>");
                str.Append("<td style='padding-right:5px;height: 30px;'>" + ds.Tables[0].Rows[i]["T_Details"].ToString() + "</td>");
                str.Append("</tr>");
                str.Append("<tr>");
            }

            if (ds.Tables[0].Rows[i]["POW"].ToString() != "")
            {
                str.Append("<td></td>");
                str.Append("<td style='padding-left:5px;height: 30px;font-weight:bold;'>" + "Results Year wise</td>");
                str.Append("<td style='padding-right:5px;height: 30px;'>" + ds.Tables[0].Rows[i]["POW"].ToString() + "</td>");
                str.Append("</tr>");
                str.Append("<tr>");
            }
            str.Append("<td style='border:1px solid black' colspan='6'></td></tr>");
        }

        // str.Append("</tr>");
        str.Append("</table>");

        lblRpt.Text = str.ToString();
        Session["RSMData"] = null;
    }

}