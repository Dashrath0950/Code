<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="RSM_ProjectCompletion_Rpt.aspx.cs" Inherits="RSM_Reports_RSM_ProjectCompletion_Rpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="100%">
        <tr>
            <td colspan="6" class="tableheading"><span class="mainheadingborderbottom">Project Completion Report</td>
        </tr>

        <tr>

            <td class="vtext" style="width: 20%;">Research Complete Date(From)
            </td>
            <td class="colon">:
            </td>
            <td class="required">
                <Anthem:TextBox ID="txtStartingdate" runat="server" AutoUpdateAfterCallBack="True"
                    MaxLength="10" onkeydown="return false;"
                    onpaste="return false;" ondrop="return false;" SkinID="gtextboxdate"
                    Style="position: static"></Anthem:TextBox><a
                        href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_txtStartingdate);return false;"><img
                            align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                            width="34" /></a>*
            </td>
            <td class="vtext" id="lblComdate">Research Complete Date(To)</td>
            <td class="colon">:
            </td>
            <td class="required">
                <Anthem:TextBox ID="txtComdate" runat="server" AutoUpdateAfterCallBack="True" MaxLength="10"
                    onkeydown="return false;"
                    onpaste="return false;" ondrop="return false;" SkinID="gtextboxdate" Style="position: static"></Anthem:TextBox><a
                        href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_txtComdate);return false;"><img
                            align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                            width="34" /></a>*
            </td>
        </tr>
        <tr>
            <td class="vtext" style="width: 15%" id="lblrtitle">Research Title</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddlrtitle" CssClass="dropdown" runat="server" AutoUpdateAfterCallBack="true"></Anthem:DropDownList>
            </td>
            <td class="vtext">Researcher Name</td>
            <td class="colon">:</td>
            <td>
                <Anthem:TextBox ID="txtrname" runat="server" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnprint" runat="server" TextDuringCallBack="WAIT..." EnableCallBack="false" AutoUpdateAfterCallBack="true" Text="PRINT" OnClick="btnprint_Click" />
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
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>
