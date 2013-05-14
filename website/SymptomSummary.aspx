<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SymptomSummary.aspx.cs" Inherits="SymptomSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Symptom Summary</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/highcharts.src.js"></script>
    <script type="text/javascript" src="js/exporting.src.js"></script>

    <script type="text/javascript" src="js/draw_graph.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#from_date").datepicker();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Symptoms Summary</h3>
            <br />
            Name:
           
            <asp:Label ID="patient_name" runat="Server" />
            <br />
            Date of Birth:
           
            <asp:Label ID="dob" runat="Server" />
            <br />
            City:
           
            <asp:Label ID="city" runat="Server" />
            <br />
            State:
           
            <asp:Label ID="state" runat="Server" />
            <br />
            Please select the date range: (Default is a week)
            <br />
            From Date:
            <asp:TextBox ID="from_date" runat="server" />
            - 
            To Date: Today
           
            <br />
            <br />
            <asp:Button ID="refresh" Text="Refresh Graph" runat="server" />
            <input type="button" id="export" value="Send to physician" />
            <br />
            <div style="display:none;">
                <asp:Table ID="c_PainSummaryTable" runat="Server" hidden="true" />
                <asp:Table ID="nausea_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="pain_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="fatigue_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="sleep_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="constipation_summary_table" runat="Server" hidden="true" />
            </div>
        </div>
    </form>
    <%--<div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>--%>
    <table border="0" width="100%" cellpadding="3" cellspacing="3">
        <tr>
            <td>
                <div id="pain_graph" style="width: 550px; height: 400px; float: none"></div>
            </td>
            <td>
                <div id="fatigue_graph" style="width: 550px; height: 400px; float: none"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="nausea_graph" style="width: 550px; height: 400px; float: none"></div>
            </td>
            <td>
                <div id="sleep_graph" style="width: 550px; height: 400px; float: none"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="constipation_graph" style="width: 550px; height: 400px; float: none"></div>
            </td>
            <td></td>
        </tr>
    </table>
</body>
</html>
