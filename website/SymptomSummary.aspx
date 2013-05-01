<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SymptomSummary.aspx.cs" Inherits="SymptomSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Symptom Summary</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.min.js"></script>
    <script src="js/draw_graph.js"></script>

    <script type="text/javascript">
       
    </script>
    <script>
        $(function () {
            $("#from_date").datepicker();
        });
    </script>
</head>
<body>
    <script src="http://code.highcharts.com/highcharts.src.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
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
            <br />
            From Date:
            <asp:TextBox ID="from_date" runat="server" />
            <br />
            <br />
            To Date: Today
            <br />
            <br />
            <asp:Button ID="refresh" Text="Refresh Graph" runat="server" />
            <input type="button" id="export" value="Send to physician" />
            <br />
            <asp:Table ID="c_PainSummaryTable" runat="Server" hidden="true" />
        </div>
    </form>
    <%--<div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>--%>
    <div id="pain_graph" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
    <div id="fatigue_graph" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
    <div id="nausea_graph" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
    <div id="sleep_graph" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
    <div id="constipation_graph" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
</body>
</html>
