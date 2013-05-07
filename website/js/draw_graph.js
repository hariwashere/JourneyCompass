$(function () {
    $(document).ready(function () {
        Highcharts.visualize = function (table, options) {
            options.xAxis.categories = [];
            $('tbody th', table).each(function (i) {
                if (i < 2)
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

            //var chart = new Highcharts.Chart(options);
        }

        var table = document.getElementById('c_PainSummaryTable'),
        options = {
            chart: {renderTo: 'container',type: 'line'},
            title: {text: 'Symptoms Summary'},
            xAxis: {},
            yAxis: {title: {text: 'Units'}},
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                        this.y + ' ' + this.x.toLowerCase();
                }
            }
        };

        nausea_options = {
            chart: { renderTo: 'container', type: 'line' },
            title: { text: 'Symptoms Summary' },
            xAxis: {},
            yAxis: { title: { text: 'Units' } },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                        this.y + ' ' + this.x.toLowerCase();
                }
            }
        };
        var nausea_table = document.getElementById('nausea_summary_table');

        Highcharts.visualize(table, options);
        Highcharts.visualize(nausea_table, nausea_options);
        var options_pain = {
            chart: { renderTo: 'pain_graph', type: 'line' },
            title: { text: 'Pain Summary' },
            xAxis: { categories: options.xAxis.categories },
            yAxis: { title: { text: 'Units' } },
            series: [options.series[0]]
        }
        var options_fatigue = {
            chart: { renderTo: 'fatigue_graph', type: 'line' },
            title: { text: 'Fatigue Summary' },
            xAxis: { categories: options.xAxis.categories },
            yAxis: { title: { text: 'Units' } },
            series: [options.series[1]]
        }
        var options_sleep = {
            chart: { renderTo: 'sleep_graph', type: 'line' },
            title: { text: 'Sleep Summary' },
            xAxis: { categories: options.xAxis.categories },
            yAxis: { title: { text: 'Units' } },
            series: [options.series[2]]
        }
        var options_nausea = {
            chart: { renderTo: 'nausea_graph', type: 'line' },
            title: { text: 'Nausea Summary' },
            xAxis: { categories: nausea_options.xAxis.categories },
            yAxis: { title: { text: 'Units' } },
            series: [nausea_options.series[0]]
        }
        var options_constipation = {
            chart: { renderTo: 'constipation_graph', type: 'line' },
            title: { text: 'Constipation Summary' },
            xAxis: { categories: options.xAxis.categories },
            yAxis: { title: { text: 'Units' } },
            series: [options.series[3]]
        }
        var pain_chart = new Highcharts.Chart(options_pain);
        var fatigue_chart = new Highcharts.Chart(options_fatigue);
        var sleep_chart = new Highcharts.Chart(options_sleep);
        var nausea_chart = new Highcharts.Chart(options_nausea);
        var constipation_chart = new Highcharts.Chart(options_constipation);

        document.getElementById("export").onclick = function () {
            $.ajax({
                url: "http://localhost:8080/export/sendToPhysician",
                type: "POST",
                data: { options_pain: JSON.stringify(options_pain), options_fatigue: JSON.stringify(options_fatigue), options_sleep: JSON.stringify(options_sleep), options_nausea: JSON.stringify(options_nausea), options_constipation: JSON.stringify(options_constipation), type: "image/png", constr: "Chart", scale: "4", name: $('#patient_name').text(), dob: $('#dob').text(), city: $('#city').text(), state: $('#state').text() },
                cache: false,
                success: function (response) {
                    alert("The report was sent successfully");
                }
            });
        };
    });
});