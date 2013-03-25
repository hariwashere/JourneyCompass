<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PainScaleSummary.aspx.cs" Inherits="PainScaleSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Pain Scale Summary</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
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
                        text: 'Data extracted from a HTML table in the page'
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
            });

        });
		</script>
</head>
<body>
    <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
    <form id="form1" runat="server">
        <div>
            You can view the pain scale summary here
            <br />
            <asp:Table ID="c_PainSummaryTable" runat="Server" />
        </div>
    </form>
    <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
</body>
</html>
