<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="Rdl.Web.ReportView" %>
<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    
    <%: Styles.Render("~/Content/css") %>

    <title></title>
</head>
<body style="background: white !important">
    <form id="form1" runat="server">
    <div>
        <table style="border: 0; width: 100%; height: 52px; position: fixed; top: 0; left: 0; background: lightgray; z-index: 100">
            <tr>
                <td style="text-align: left; padding: 10px; width: 33%">
                    <asp:Button ID="PrevBtn" runat="server" CssClass="btn btn-primary" OnClick="PrevBtn_Click" Text="Prev Page" />
                </td>
                <td style="text-align: center; padding: 10px; width: 33%">
                    <a class="btn btn-primary" href="<%: Request.Url.AbsoluteUri + "&render=PDF" %>" target="_blank">View PDF</a>
                </td>
                <td style="text-align: right; padding: 10px; width: 33%">
                    <asp:Button ID="NextBtn" runat="server" CssClass="btn btn-primary" OnClick="NextBtn_Click" Text="Next Page" />
                </td>
            </tr>
        </table>
        <div style="width: 100%; height: 52px;"></div>
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="Report" runat="server" Width="100%" AsyncRendering="True" ProcessingMode="Local" ShowBackButton="False" ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowParameterPrompts="False" ShowPageNavigationControls="True" ShowFindControls="False" ShowExportControls="False" ShowToolBar="False" ShowWaitControlCancelLink="False" ShowZoomControl="False" SizeToReportContent="True">
        </rsweb:ReportViewer>
    </div>
        
    </form>
</body>
</html>
