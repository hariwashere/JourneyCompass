<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PainScaleSummary.aspx.cs" Inherits="PainScaleSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Pain Scale Summary</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" type="text/css" href="js/examples/css/smoothness/jquery-ui-1.8.17.custom.css" />
    <link rel="stylesheet" type="text/css" href="js/examples/css/main.css" />

    <script type="text/javascript" src="js/examples/js/jquery/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/examples/js/jquery/jquery-ui-1.8.17.custom.min.js"></script>
    <script type="text/javascript" src="js/jspdf.js"></script>
    <script type="text/javascript" src="js/libs/Deflate/adler32cs.js"></script>
    <script type="text/javascript" src="js/libs/FileSaver.js/FileSaver.js"></script>
    <script type="text/javascript" src="js/libs/Blob.js/BlobBuilder.js"></script>

    <script type="text/javascript" src="js/jspdf.plugin.addimage.js"></script>

    <script type="text/javascript" src="js/jspdf.plugin.standard_fonts_metrics.js"></script>
    <script type="text/javascript" src="js/jspdf.plugin.split_text_to_size.js"></script>
    <script type="text/javascript" src="js/jspdf.plugin.from_html.js"></script>
    <script type="text/javascript" src="js/examples/js/basic.js"></script>

    <script type="text/javascript">
        var doc = new jsPDF();

        // We'll make our own renderer to skip this editor
        var specialElementHandlers = {
            '#editor': function (element, renderer) {
                return true;
            }
        };

        // All units are in the set measurement for the document
        // This can be changed to "pt" (points), "mm" (Default), "cm", "in"
        doc.fromHTML($('body').get(0), 15, 15, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                Highcharts.visualize = function (table, options) {
                    // the categories
                    options.xAxis.categories = [];
                    $('tbody th', table).each(function (i) {
                        if (i < 6)
                            return;
                        options.xAxis.categories.push(this.innerHTML);
                    });

                    // the data series
                    options.series = [];
                    $('tr', table).each(function (i) {
                        var tr = this;
                        $('th, td', tr).each(function (j) {
                            if (j > 0) { // skip first column
                                if (i == 0) { // get the name and init the series
                                    options.series[j - 1] = {
                                        name: this.innerHTML,
                                        data: []
                                    };
                                } else { // add values
                                    options.series[j - 1].data.push(parseFloat(this.innerHTML));
                                }
                            }
                        });
                    });

                    var chart = new Highcharts.Chart(options);
                }

                var table = document.getElementById('c_PainSummaryTable'),
                options = {
                    chart: {
                        renderTo: 'container',
                        type: 'line'
                    },
                    title: {
                        text: 'Symptoms Summary'
                    },
                    xAxis: {
                    },
                    yAxis: {
                        title: {
                            text: 'Units'
                        }
                    },
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.series.name + '</b><br/>' +
                                this.y + ' ' + this.x.toLowerCase();
                        }
                    }
                };

                Highcharts.visualize(table, options);
                $.ajax({
                    url: "http://localhost:8080/export/",
                    type: "POST",
                    data: { options: JSON.stringify(options), type: "image/png", constr: "Chart", scale: "4", name: $('#patient_name').text(), dob: $('#dob').text(), city: $('#city').text(), state: $('#state').text()},
                    cache: false
                    //success: function (response) {
                    //    alert(response);
                    //}
                });
            });
        });
    </script>
    <script>
        $(function () {
            $("#from_date").datepicker();
        });
        $(function () {
            $("#to_date").datepicker();
        });
    </script>
</head>
<body>
    <script src="http://code.highcharts.com/highcharts.js"></script>
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
            To Date:
            <asp:TextBox ID="to_date" runat="server" />
            <br />
            <br />
            <asp:Button ID="refresh" Text="Refresh Graph" runat="server" />
            <br />
            <asp:Table ID="c_PainSummaryTable" runat="Server" hidden="true" />
        </div>
    </form>
    <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
</body>
</html>
