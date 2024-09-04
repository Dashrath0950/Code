<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RSM_TechnicalProgramHODwise.aspx.cs" Inherits="RSM_Reports_RSM_TechnicalProgramHODwise" MasterPageFile="~/UMM/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        //function Checking(obj) {
        //    alert(obj);
        //}



        function CheckAllDataGridCheckBoxes() {
            debugger;
            var intRowCount = document.getElementById("ctl00_ContentPlaceHolder1_gvRsc").rows.length;
            //document.getElementById('ctl00_ContentPlaceHolder1_hdCount').value;
            var chkAll = document.getElementById('ctl00_ContentPlaceHolder1_gvRsc_ctl01_chkAll')
            var iStartItemIndex = 2;
            if (chkAll.checked == true) {

                for (i = iStartItemIndex; i < parseInt(intRowCount) + parseInt(iStartItemIndex) ; i++) {
                    if (i < 10) {
                        document.getElementById("ctl00_ContentPlaceHolder1_gvRsc_ctl0" + i + "_" + "chk").checked = true;
                    }
                    else
                        document.getElementById("ctl00_ContentPlaceHolder1_gvRsc_ctl" + i + "_" + "chk").checked = true;
                }
            }
            else {

                for (i = iStartItemIndex; i < parseInt(intRowCount) + parseInt(iStartItemIndex) ; i++) {
                    if (i < 10) {

                        document.getElementById("ctl00_ContentPlaceHolder1_gvRsc_ctl0" + i + "_" + "chk").checked = false;
                    }
                    else
                        document.getElementById("ctl00_ContentPlaceHolder1_gvRsc_ctl" + i + "_" + "chk").checked = false;
                }
            }
        }

        function onchange(chk) {
            debugger;
            var chk = document.getElementById(chk).checked;
            if (chk) {
                document.getElementById("ctl00_ContentPlaceHolder1_chkList_1").checked = true;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_chkList_1").checked = false;
            }
        }

    </script>

    <table width="100%">
        <tr>
            <td colspan="6" class="tableheading"><span class="mainheadingborderbottom">Technical  Program Work
            </td>
        </tr>
        <tr>
            <td class="vtext" id="lblRtitle" style="width: 20%;">Department</td>
            <td class="colon">:</td>
            <td>
                <Anthem:DropDownList ID="ddl_department" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" AutoCallBack="true"></Anthem:DropDownList>
            </td>
            <td class="vtext" id="lblRname">Scheme</td>
            <td class="colon">:</td>
            <td>
                <Anthem:DropDownList ID="ddl_scheme" runat="server" AutoUpdateAfterCallBack="true" SkinID="dropdown"></Anthem:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="vtext" id="lblsector" runat="server">Section</td>
            <td class="colon">:
            </td>
            <td>
                <Anthem:DropDownList ID="D_ddlsector" AutoUpdateAfterCallBack="true" runat="server"
                    CssClass="dropdownlong">
                    <asp:ListItem Value="0">--Select Section--</asp:ListItem>
                </Anthem:DropDownList>
            </td>
            <td class="vtext">Year
            </td>
            <td class="colon">:
            </td>
            <td>
                <Anthem:DropDownList ID="ddlyear" AutoUpdateAfterCallBack="true" runat="server"
                    BackColor="White" CssClass="dropdownsmall">
                </Anthem:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td>
                <Anthem:RadioButtonList ID="rblPrintSelection" AutoUpdateAfterCallBack="true" runat="server" Style="color: #b92b27; display: inline-flex !important;"
                    RepeatDirection="Horizontal" CellSpacing="10" AutoCallBack="true" EnableCallBack="false">
                    <asp:ListItem Value="P" Selected="True">PDF</asp:ListItem>
                    <asp:ListItem Value="W">Word</asp:ListItem>
                    <asp:ListItem Value="E">Excel</asp:ListItem>
                </Anthem:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td>
                <%--OnSelectedIndexChanged="chkList_SelectedIndexChanged"--%>
                <Anthem:CheckBoxList ID="chkList" AutoUpdateAfterCallBack="true" runat="server" Style="color: #b92b27; display: inline-flex !important;"
                    RepeatDirection="Vertical" CellSpacing="10" AutoCallBack="true" onclientclick="return onchange(this);">
                    <asp:ListItem Value="Title" Selected="True" Enabled="false">Title of the experiment</asp:ListItem>
                    <asp:ListItem Value="objective">Objective of the experiment</asp:ListItem>
                    <asp:ListItem Value="POW">Results Year wise</asp:ListItem>
                    <asp:ListItem Value="r1">Staff position scheme wise and Post wise</asp:ListItem>
                    <asp:ListItem Value="r2">Research Achievement</asp:ListItem>
                    <asp:ListItem Value="r3">Scheme Objective</asp:ListItem>
                    <asp:ListItem Value="r4">Scheme wise budget details</asp:ListItem>
                    <asp:ListItem Value="r5">Salient Research finding of the scheme</asp:ListItem>
                    <asp:ListItem Value="r6">Scheme wise summery of experiment </asp:ListItem>

                    <asp:ListItem Value="col">All</asp:ListItem>
                </Anthem:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="tdgap"></td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnsave" runat="server" TextDuringCallBack="WAIT..." Text="GET" AutoCallBack="true" OnClientClick="target ='_self';"
                    OnClick="btnget_Click" AutoUpdateAfterCallBack="true" EnableCallBack="false" />

                <Anthem:Button ID="btnreset" OnClientClick="target ='_self';" runat="server" TextDuringCallBack="WAIT..." AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnreset_Click" />
                <Anthem:Label ID="lblMsg" runat="server" CssClass="lblmessage" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <table width="100%">
                    <tr>
                        <td align="left" class="tablesubheading"><span class="subheadingborderbottom">List of Research Observations Year Wise
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <Anthem:GridView ID="gvRsc" runat="server" AutoGenerateColumns="False" AutoUpdateAfterCallBack="true" PageSize="10"
                                Width="100%" AllowPaging="False" OnRowCommand="gvRsc_RowCommand" OnPageIndexChanging="gvRsc_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField HeaderText="SNo.">
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="All">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderTemplate>
                                            <Anthem:CheckBox ID="chkAll" onclick="CheckAllDataGridCheckBoxes();" runat="server"
                                                AutoUpdateAfterCallBack="true" AutoCallBack="true" Checked="true" Text="ALL"></Anthem:CheckBox>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <Anthem:CheckBox ID="chk" OnCheckedChanged="chk_CheckedChanged" runat="server"
                                                AutoUpdateAfterCallBack="true" AutoCallBack="true" Checked="true"></Anthem:CheckBox>
                                            <asp:HiddenField ID="hf_reserchid" Value='<%# Eval("pk_research_ID") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Research_title" HeaderText="Experiment Title">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Scheme_name" HeaderText="Scheme">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="committeename" HeaderText="Committe Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="P_Sr_No" HeaderText="Experiment No.">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Research_Start_Date" HeaderText="Year Of Start">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <%--<asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemTemplate>
                                            <Anthem:LinkButton ID="editproposal" runat="server" CommandArgument='<%# Eval("pk_Twid") %>'
                                                CommandName="SELECT" CausesValidation="False" TextDuringCallBack="Loading..."
                                                Text="Edit" AutoUpdateAfterCallBack="true" EnableCallBack="false" Autocallback="false"><img src="../../Images/Edit.gif" alt="" border="0"/>
                                            </Anthem:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemTemplate>
                                            <Anthem:LinkButton ID="deleteproposal" runat="server"  CommandArgument='<%# Eval("pk_Twid") %>'
                                                CommandName="DELETEREC"  OnClientClick="return confirm('Are you sure you want to Delete selected Record Permanently?');"
                                                CssClass="DataGriditemLink" Text="Delete"> <img src="../../Images/Delete.gif" alt="" border="0"  />
                                            </Anthem:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="View" Visible="false">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <Anthem:LinkButton ID="lnkreport1" runat="server" CssClass="GridLink"
                                                Text="View" CommandArgument='<%# Eval("pk_research_ID") %>' CommandName="PRINTREC" AutoUpdateAfterCallBack="true" EnableCallBack="false"></Anthem:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </Anthem:GridView>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="4">
                <Anthem:Button ID="btnPrnt1" runat="server" TextDuringCallBack="WAIT..." Text="Print 1" AutoCallBack="true" OnClick="btnprnt_Click1" OnClientClick="target ='_self';"
                    AutoUpdateAfterCallBack="true" EnableCallBack="false" />

                <Anthem:Button ID="btnprnt" runat="server" TextDuringCallBack="WAIT..." Text="Print 2" AutoCallBack="true" OnClientClick="target ='_blank';"
                    OnClick="btnprnt_Click" AutoUpdateAfterCallBack="true" EnableCallBack="false" />

            </td>
        </tr>
    </table>

</asp:Content>
