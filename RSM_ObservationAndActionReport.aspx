<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="RSM_ObservationAndActionReport.aspx.cs" Inherits="RSM_Reports_RSM_ObservationAndActionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" width="100%" class="table">
        <tr>
            <td class="tableheading" colspan="6">
                <span class="mainheadingborderbottom">Technical Programmer Meeting
                </span>
            </td>
        </tr>
        <tr>


            <td class="vtext" style="width: 20%;">Committee</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddlCommitte" runat="server" AutoCallBack="true" AutoUpdateAfterCallBack="true" SkinID="dropdown" OnSelectedIndexChanged="D_ddlCommitte_SelectedIndexChanged"></Anthem:DropDownList>
            </td>
            <td class="vtext">Department/Section name</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddldname" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown" AutoCallBack="true" OnSelectedIndexChanged="D_ddldname_SelectedIndexChanged"></Anthem:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="vtext">Research Name</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddlResid" runat="server" AutoCallBack="true" AutoUpdateAfterCallBack="true" SkinID="dropdown"></Anthem:DropDownList>
            </td>
            <td class="vtext">Year</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddlyear" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown"></Anthem:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <Anthem:Button ID="btnGet" runat="server" TextDuringCallBack="WAIT..." CommandName="GET" AutoUpdateAfterCallBack="true" Text="GET" OnClick="btnGet_Click" EnableCallBack="false" />
                <Anthem:Button ID="btnReset" runat="server" TextDuringCallBack="WAIT..." CommandName="RESET" AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnReset_Click" EnableCallBack="false" />
            </td>
        </tr>
    </table>
</asp:Content>
