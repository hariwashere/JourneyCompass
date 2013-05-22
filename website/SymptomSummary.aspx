<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SymptomSummary.aspx.cs" Inherits="SymptomSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Symptom Summary</title>

    <link rel="stylesheet" type="text/css" href="jquery/css/smoothness/jquery-ui-1.10.3.custom.min.css" />
    <script src="jquery/js/jquery-1.9.1.js"></script>
    <script src="jquery/js/jquery-ui-1.10.3.custom.min.js"></script>

    <link rel="stylesheet" type="text/css" href="includes/theme.css" />

    <script type="text/javascript" src="js/highcharts.src.js"></script>
    <script type="text/javascript" src="js/exporting.src.js"></script>

    <!-- <script type="text/javascript" src="js/draw_graph.js"></script> -->
    <asp:Literal ID="chartScript" runat="server" />
    <asp:Literal ID="collapse_chartScript" runat="server" />

    <script type="text/javascript">
        $(function () {
            $("#from_date").datepicker();
        });

        $(function () {
            $('#charts_sel').click(function () {
                if ($('#charts_sel').is(':checked')) {
                    $('#CollapseContainer').show();
                    $('#SeparateContainer').hide();
                } else {
                    $('#CollapseContainer').hide();
                    $('#SeparateContainer').show();
                }
            });
        });

    </script>
</head>
<body>
    <table width="100%" border="0" cellpadding="5">
        <tr>
            <td width="20" />
            <td>
                <img src="images/JourneyCompass_1_medium.png" border="0" /></td>
        </tr>
    </table>
    <div style="background: #005C8A; width: 1270px; height: 50px; opacity: 0.3; filter: alpha(opacity=30); padding: 2px 2px 2px 2px; overflow: auto">
        <table style="width: 1270px; border: 0; padding: 0;">
            <tr>
                <td style="text-align: center; font-size: xx-large">Symptom Summary</td>
            </tr>
        </table>
    </div>
    <br />
    <form id="form1" runat="server">
        <div style="margin-left: 70px; margin-right: 70px;">
            Name:
            <asp:Label ID="patient_name" runat="Server" />
            <br />
            Birth Year:
            <asp:Label ID="dob" runat="Server" />
            <br />
            City:
            <asp:Label ID="city" runat="Server" />
            <br />
            State:
            <asp:Label ID="state" runat="Server" />
            <br />
            Please select the date range (Default is a week):
            <asp:TextBox ID="from_date" runat="server" />
            until Today       
            <br />
            <br />
            <asp:Button ID="refresh" Text="Refresh Graph" runat="server" />
            <input type="button" id="export" value="Send to physician" />
<%--            <div style="display: none;">
                <asp:Table ID="c_PainSummaryTable" runat="Server" hidden="true" />
                <asp:Table ID="nausea_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="pain_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="fatigue_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="sleep_summary_table" runat="Server" hidden="true" />
                <asp:Table ID="constipation_summary_table" runat="Server" hidden="true" />
            </div>--%>
        </div>
    </form>
    
    <div style="margin-left: 70px; margin-right: 70px;">
        <input id="charts_sel" type="checkbox" name="chart_type_sel" /> Check this to collapse all symptom charts.
    </div>
    <br />
    <%--<div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>--%>
    <div style="background: #6AB2BA; width: 1120px; margin-left: 50px; margin-right: 50px; margin-bottom: 50px; border: 1px solid #198895;">
        <div id="CollapseContainer" style="width:1100px; display: none; margin: 7px;"></div>
        <div id="SeparateContainer">
            <table border="0" width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                        <div id="Pain_graph" style="width: 550px; height: 400px; float: none"></div>
                    </td>
                    <td>
                        <div id="Fatigue_graph" style="width: 550px; height: 400px; float: none"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="Nausea_graph" style="width: 550px; height: 400px; float: none"></div>
                    </td>
                    <td>
                        <div id="Sleep_graph" style="width: 550px; height: 400px; float: none"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="Constipation_graph" style="width: 550px; height: 400px; float: none"></div>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
