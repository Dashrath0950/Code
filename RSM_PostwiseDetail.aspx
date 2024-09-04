<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="RSM_PostwiseDetail.aspx.cs" Inherits="RSM_Reports_RSM_PostwiseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td colspan="6" class="tableheading"><span class="mainheadingborderbottom">Scheme-wise Staff Details</span>
            </td>
        </tr>
        <tr>
            <td class="vtext" id="lblReportType" style="width: 20%;">Report Type</td>
            <td class="colon">:</td>
            <td colspan="4" class="required">
                <Anthem:DropDownList ID="D_ddlReportType" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown" OnSelectedIndexChanged="D_ddlReportType_SelectedIndexChanged" AutoCallBack="true">
                    <asp:ListItem Value="0">Select Report Type</asp:ListItem>
                    <asp:ListItem Value="staffdtl">Scheme-wise Staff Details</asp:ListItem>
                    <asp:ListItem Value="SanctionBudget">Sanctioned Budget</asp:ListItem>
                </Anthem:DropDownList>*
            </td>
        </tr>
        <tr>
            <td class="vtext" style="width: 20%;">Department</td>
            <td class="colon">:</td>
            <td colspan="4">
                <Anthem:DropDownList ID="D_ddldepartment" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" AutoCallBack="true"></Anthem:DropDownList>
                <Anthem:Label runat="server" AutoUpdateAfterCallBack="true" Visible="false" ID="lblrequireddept" ForeColor="red">*</Anthem:Label>
            </td>
        </tr>
        <tr>
            <td class="vtext" id="lblRname">Scheme</td>
            <td class="colon">:</td>
            <td colspan="4">
                <Anthem:DropDownList ID="ddl_scheme" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown"></Anthem:DropDownList>
                <Anthem:Label runat="server" AutoUpdateAfterCallBack="true" Visible="false" ID="lblrequiredScheme" ForeColor="red">*</Anthem:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td>
                <Anthem:RadioButtonList ID="rblPrintSelection" AutoUpdateAfterCallBack="true" runat="server" Style="color: #b92b27; display: inline-flex !important;"
                    RepeatDirection="Horizontal" CellSpacing="10" AutoCallBack="true" EnableCallBack="false">
                    <asp:ListItem Value="E" Selected="True">Excel</asp:ListItem>
                    <asp:ListItem Value="P">PDF</asp:ListItem>
                    <asp:ListItem Value="W">Word</asp:ListItem>

                </Anthem:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="tdgap"></td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnGet" runat="server" TextDuringCallBack="WAIT..." Text="GET" AutoCallBack="true" OnClick="btnGet_Click" AutoUpdateAfterCallBack="true" EnableCallBack="false" />

                <Anthem:Button ID="btnreset" runat="server" TextDuringCallBack="WAIT..." AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnreset_Click" />
                <Anthem:Label ID="lblMsg" runat="server" CssClass="lblmessage" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </td>
        </tr>
    </table>
</asp:Content>
