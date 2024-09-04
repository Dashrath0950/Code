<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RSM_TechnicalProgrammerOutcome.aspx.cs" Inherits="RSM_Reports_RSM_TechnicalProgrammerOutcome" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>RPCAU</title>
</head>
<body>
    <form runat="server">

        <style>

          @media print {
                        header, footer {
                            display: none; 
                        } 
          }

        </style>

        <script language="javascript" type="text/javascript">
        
            function printDiv(divName) {
                var printContents = document.getElementById(divName).innerHTML;
                var originalContents = document.body.innerHTML;
                document.body.innerHTML = printContents;
                window.print();
                document.body.innerHTML = originalContents;
            }
           
        </script>

    <div>
        <%-- <a href="javascript:print()"  CssClass="button" AutoUpdateAfterCallBack="True">Print</a>--%>
        <Anthem:Button ID="btnReport" runat="server" CssClass="button"
                    Text="Download Report" AutoUpdateAfterCallBack="True"
                    TabIndex="3" OnClientClick="printDiv('bb')" Style="margin-left:500px"/>
           <%-- OnClientClick="printDiv('bb')"--%>

    </div>

    <div id="bb" class="divprint">
    <%--<asp:Literal runat="server" Text="" ID="lbl" ></asp:Literal>--%>
        <anthem:Label runat="server" ID="lblRpt" AutoUpdateAfterCallBack="true"> </anthem:Label>
    </div>

    </form> 
</body>
</html>
     
<%--</asp:Content>--%>
