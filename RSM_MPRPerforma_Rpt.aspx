<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="RSM_MPRPerforma_Rpt.aspx.cs" Inherits="RSM_Reports_RSM_MPRPerforma_Rpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="100%">
        <tr>
            <td colspan="6" class="tableheading"><span class="mainheadingborderbottom">Monthly Progress Research Report</td>
        </tr>
        <tr>
            <td class="vtext" style="width: 15%" id="lblrtitle">Research Title</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddlrtitle" CssClass="dropdown" runat="server" AutoUpdateAfterCallBack="true"></Anthem:DropDownList>*
            </td>
            <td class="vtext">Year</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="ddlyear" runat="server" AutoUpdateAfterCallBack="true"></Anthem:DropDownList>*
            </td>
        </tr>


        <tr>
            <td class="vtext">Month</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="ddlmonth" runat="server" AutoUpdateAfterCallBack="true"></Anthem:DropDownList>*
            </td>
            <td class="vtext">Report Format</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:RadioButtonList ID="rdbrptformat" runat="server" RepeatDirection="Horizontal"
                    AutoUpdateAfterCallBack="true">
                    <asp:ListItem Value="P" Selected="True" Text="PDF"></asp:ListItem>
                    <asp:ListItem Value="E" Text="Excel"></asp:ListItem>
                </Anthem:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnprint" runat="server" TextDuringCallBack="WAIT..." EnableCallBack="false" OnClick="btnprint_Click" AutoUpdateAfterCallBack="true" Text="PRINT" />
                <Anthem:Button ID="btnreset" runat="server" TextDuringCallBack="WAIT..." AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnreset_Click" />
                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
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

