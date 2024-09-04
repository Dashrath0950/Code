<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RSM_TechnicalProgramHODReport.aspx.cs" Inherits="RSM_Reports_RSM_TechnicalProgramHODReport"
    MasterPageFile="~/UMM/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="100%">
        <tr>
            <td class="tableheading" colspan="6">
                <span class="mainheadingborderbottom">Download HOD Report </span>
            </td>
        </tr>
        <tr>
            <td class="vtext" id="lblyear">Year</td>
            <td class="colon">:
            </td>
            <td class="required">
                <Anthem:DropDownList ID="D_ddlyear" AutoUpdateAfterCallBack="true" runat="server" BackColor="White" CssClass="dropdownsmall"></Anthem:DropDownList>
                *
            </td>
            <td class="vtext">Department
            </td>
            <td class="colon">:
            </td>
            <td>
                <Anthem:DropDownList ID="D_ddl_department" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown"
                    AutoPostBack="true" OnSelectedIndexChanged="D_ddl_department_SelectedIndexChanged">
                </Anthem:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="vtext">Section
            </td>
            <td class="colon">:
            </td>
            <td>
                <Anthem:DropDownList ID="ddl_Section" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown"></Anthem:DropDownList></td>
            <td class="vtext" id="lblComdate">Season</td>
            <td class="colon">:
            </td>
            <td>
                <Anthem:DropDownList ID="ddlRSeason" runat="server" AutoUpdateAfterCallBack="true" CssClass="dropdown">
                </Anthem:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnGet" runat="server" AutoUpdateAfterCallBack="true" Text="Get" OnClick="btnGet_Click"
                    TextDuringCallBack="WAIT..." PreCallBackFunction="btnSave_PreCallBack" />

                <Anthem:Button ID="btnreset" runat="server" AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnreset_Click" />

                <Anthem:Label ID="lblMsg" runat="server" CssClass="lblmessage" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </td>
        </tr>
        <tr>
            <td class="tableheading" colspan="6">
                <span class="subheadingborderbottom">List of HOD Report(s) </span>
            </td>
        </tr>
        <tr>
            <td colspan="6">

                <table width="100%">
                    <tr>
                        <td colspan="6">
                            <Anthem:GridView ID="gvresearch" Width="100%" runat="server" AllowPaging="false" AutoGenerateColumns="false" AutoUpdateAfterCallBack="true"
                                EnableCallBack="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="years" HeaderText="Year">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SeasonName" HeaderText="Season">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="departmentName" HeaderText="Department Name">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SectionName" HeaderText="Section">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Download">
                                        <ItemTemplate>
                                              <Anthem:HiddenField ID="hdnfilename" runat="server" Value='<% #Eval("Pk_ID")%>' />
                                            <Anthem:LinkButton ID="btnDownload" data-ID='<%# Eval("Pk_ID") %>' runat="server" OnClick="btnDownload_Click"
                                                UpdateAfterCallBack="True" EnableCallBack="false" AutoUpdateAfterCallBack="True"
                                                Text="Download" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>

                                </Columns>
                            </Anthem:GridView>
                        </td>
                    </tr>

                </table>

            </td>
        </tr>

    </table>

</asp:Content>
