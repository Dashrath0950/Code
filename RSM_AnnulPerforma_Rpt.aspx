<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="RSM_AnnulPerforma_Rpt.aspx.cs" Inherits="RSM_Reports_RSM_AnnulPerforma_Rpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="100%">
        <tr>
            <td colspan="6" class="tableheading"><span class="mainheadingborderbottom">Annual Performa Report</td>
        </tr>
        <tr>
            <td class="vtext">Year</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="ddlyear" runat="server" AutoUpdateAfterCallBack="true"></Anthem:DropDownList>*
            </td>
        </tr>


        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnprint" runat="server" EnableCallBack="false" TextDuringCallBack="WAIT..." AutoUpdateAfterCallBack="true" Text="PRINT" OnClick="btnprint_Click" />
                <Anthem:Button ID="btnreset" runat="server" TextDuringCallBack="WAIT..." AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnreset_Click" />
                <Anthem:Label ID="lblMsg" CssClass="lblmessage" runat="server" AutoUpdateAfterCallBack="true" />
            </td>
        </tr>
    </table>
    <div id="lightbookmaster" class="grayout" style="display: none; top: 10%; height: auto;">
        <div class="popupcenter">
            <div class="dvHelp">
                <div onclick="document.getElementById('lightbookmaster').style.display='none';" class="closebtn">
                    X
                </div>
                <div id="dvHelp" runat="server">
                    <table>
                        <tr>
                            <td>
                                <rsweb:ReportViewer ID="recieptviewer" runat="server" ProcessingMode="Remote"
                                    Width="100%">
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                    </table>
                    <%--<asp:Literal ID="ltrlHelp" runat="server"></asp:Literal>
                       Under Construction...--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
