<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SymptomSummary.aspx.cs" Inherits="SymptomSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Symptom Summary</title>

    <link rel="stylesheet" type="text/css" href="jquery/css/smoothness/jquery-ui-1.10.3.custom.min.css" />
    <script src="jquery/js/jquery-1.9.1.js"></script>
    <script src="jquery/js/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="jquery/js/jquery.form.min.js"></script>

    <!--<script src="jquery/js/jQuery.XDomainRequest.js"></script>-->

    <link rel="stylesheet" type="text/css" href="includes/theme.css" />

    <script type="text/javascript" src="js/highcharts.src.js"></script>
    <script type="text/javascript" src="js/exporting.src.js"></script>

    <!-- <script type="text/javascript" src="js/draw_graph.js"></script> -->
    <asp:Literal ID="chartScript" runat="server" />
    <asp:Literal ID="collapse_chartScript" runat="server" />

    <script type="text/javascript">
        String.prototype.escapeSpecialChars = function (valueToEscape) {
            if (valueToEscape != null && valueToEscape != "") {
                return valueToEscape.replace(/\n/g, "\\n");
            } else {
                return valueToEscape;
            }
        };
    </script>

    <script type="text/javascript">
        function PostSendEmailReq() {
            $("#dialog-sendreport").dialog("close");
            $("#dialog-aftersend").html("<p>Please wait until your mail is sent out<br/>This may take a while..</p>");
            $("#dialog-aftersend").dialog({
                width: "auto",
                height: "auto",
                modal: true,
                dialogClass: 'dialog_general',
                show: "clip"
            });

            p_info = $("input[name='patient_info']").val();

            p_note = $("textarea[name='note']").val();
            // take care of html tags, ' and ".
            p_note = p_note.replace(/</g, '&lt;').replace(/>/g, '&gt;');
            //p_note = p_note.replace(/'/g, "''");
            p_note = p_note.replace(/"/g, '\\"');
            p_note = p_note.escapeSpecialChars(p_note);

            p_files = $("input[name='filenames']").val();
            p_to = $("select[name='to']").val();
            p_subject = "Symptom Report for <%=user_name%>";
            p_patient = "<%=user_name%>";
            p_jsondata = '{ "to":"' + p_to + '", "subject":"' + p_subject + '", "patient_info": "' + p_info + '", "note": "' + p_note + '", "charts": "' + p_files + '", "patient": "' + p_patient + '" }';
            
            // Make sure JSON data does not have any special characters in it.
            // p_jsondata = p_jsondata.escapeSpecialChars(p_jsondata);

            $.ajax({
                url: '/Service.svc/SendEmail',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: p_jsondata,
                success: function (data) {
                    $("#dialog-aftersend").html("<p>" + data.d + "</p>" + "You can close this box now.");
                },
                error: function (xhr, textStatus, errorThrown) {
                    $("#dialog-aftersend").dialog("close");
                    alert("in SendMail service error: " + textStatus + " : " + errorThrown);
                }
            });            
        }

    </script>
    <script type='text/javascript'>
        var formConfirm = null;
        $(function () {
            $.support.cors = true;
            $("#exportForm").ajaxForm({
                dataType: "json",
                beforeSubmit: function () {
                    if (formConfirm == null) {
                        $("#dialog-report").dialog({
                            resizable: false,
                            height: "auto",
                            modal: true,
                            show: "clip",
                            dialogClass: 'dialog_general',
                            buttons: {
                                "Yes, Continue...": function () {
                                    $(this).dialog("close");
                                    formConfirm = true;
                                    $("#exportForm").submit();
                                },
                                Cancel: function () {
                                    $(this).dialog("close");
                                    return false;
                                }
                            }
                        });
                        return false;
                    } else {
                        if (formConfirm != null)
                            formConfirm = null;
                        $("#dialog-preparereport").dialog({
                            width: "auto",
                            height: "auto",
                            modal: true,
                            dialogClass: 'dialog_general',
                            show: "blind"
                        });
                        return true;
                    }
                },
                success: function (data) {
                    $("#dialog-preparereport").dialog("close");
                    $("input[name='filenames']").val(data.filenames);
                    $("#dialog-sendreport").dialog({
                        width: "auto",
                        height: "auto",
                        modal: true,
                        dialogClass: 'dialog_general',
                        show: "clip"
                    });
                },
                error: function (xhr, textStatus, errorThrown) {
                    $("#dialog-preparereport").dialog("close");
                    alert("Chart Exposrt Server error: " + textStatus + " : "+errorThrown);
                }
            });
        });

    </script>

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

    <asp:Literal ID="initChartDisplay" runat="server" />
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
    <table style="border:none;margin:0px 0px 0px 0px;">
        <tr>
            <td>
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
                </div>
            </td>
        </tr>
    </table>
    <table style="border:none;">
        <tr>
            <td>
                <form id="form1" runat="server">
                    <div style="margin-left: 70px; margin-right: 0px;">
                        Please select the date range (Default is 2 weeks):
                        <asp:TextBox ID="from_date" runat="server" />
                        until Today       
                        <br /><br />
                        <asp:Checkbox id="charts_sel" runat="server" Text="Check this to collapse all symptom charts." /> 
                        <br /><br />
                        <asp:Button ID="refresh" Text="Refresh Graph" runat="server" style="width:500px;height:55px;float:right;"/>
                    </div>
                </form>
            </td>
            <td style="vertical-align:bottom;">
                <asp:Literal ID="exportForm" runat="server" />
            </td>
        </tr>
    </table>
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
    <div id="dialog-report" title="Symptom Report Request" style="display: none;" >
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>This will prepare a report for your physician. Do you want to start now?</p>
    </div>
    
    <div id="dialog-preparereport" title="Prepare Report" style="display: none;" >
        <p>Processing ... Please wait until your report is generated..</p>
    </div>

    <div id="dialog-sendreport" title="Send Report" style="display:none;" >
        <table border="0">
            <tr>
                <td style="float:right"><b>To:</b></td>
                <td>
                    <select name="to">
                        <option value="harbinclinic@gadirect.net">Harbin Clinic</option>
                        <option value="sheryl.testgahie@gadirect.net">Sheryl TestGaHIE (Test Acct)</option>
                        <option value="myungchoi@hisp.i3l.gatech.edu">I3L (Test via I3L Direct)</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="float:right"><b>Subject:</b></td><td>Patient Symportion Report for <%=user_name%></td>
            </tr>
            <tr>
                <td align="top" style="float:right;"><b>Note:</b></td>
                <td><textarea name="note" rows="5" cols="50"></textarea></td>
            </tr>
        </table>
        <input type="hidden" name="patient_info" value="<%=user_info%>"/>
        <input type="hidden" name="filenames" value=""/><br/><br/>
        <button onclick="return PostSendEmailReq();" style="width:100%;">Submit to Send Email</button>
    </div>

    <div id="dialog-aftersend" title="Send Report" style="display:none;" ></div>

</body>
</html>
