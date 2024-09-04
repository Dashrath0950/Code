<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UMM/MasterPage.master" CodeFile="RSM_PublicationRep.aspx.cs" Inherits="RSM_Reports_RSM_PublicationRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td colspan="6" class="tableheading"><span class="mainheadingborderbottom">Publication Report
            </td>
        </tr>
        <%--<tr>
            <td class="vtext" id="lblRtitle" style="width: 20%;">Department</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="ddl_department" runat="server" AutoCallBack="true" AutoUpdateAfterCallBack="true" SkinID="dropdown"></Anthem:DropDownList>*
            </td>
        </tr>--%>

        <tr>
            <td class="vtext" id="lblAuthor">Author
            </td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:TextBox ID="R_txtAuthor" MaxLength="150" runat="server" AutoUpdateAfterCallBack="true"></Anthem:TextBox></td>

             <td class="vtext" id="lblptitle">Publication Title
            </td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="R_txtptitle" runat="server" AutoUpdateAfterCallBack="true"></Anthem:DropDownList></td>
            
        </tr>
        <tr>
            <td class="vtext" id="lblMtype">Media Type
            </td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddlMtype" runat="server" AutoUpdateAfterCallBack="true"></Anthem:DropDownList></td>
        </tr>
        <tr>
            <td class="tdgap"></td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnsave" runat="server" TextDuringCallBack="WAIT..." Text="GET" AutoCallBack="true" OnClick="btnget_Click" AutoUpdateAfterCallBack="true" EnableCallBack="false" />

                <Anthem:Button ID="btnreset" runat="server" TextDuringCallBack="WAIT..." AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnreset_Click" />
                <Anthem:Label ID="lblMsg" runat="server" CssClass="lblmessage" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </td>
        </tr>
    </table>
</asp:Content>
