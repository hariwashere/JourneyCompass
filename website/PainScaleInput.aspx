<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PainScaleInput.aspx.cs" Inherits="PainScaleInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#pain_slider").slider({
                range: "min",
                value: 5,
                min: 1,
                max: 9,
                slide: function (event, ui) {
                    $("#c_pain").val(ui.value);
                }
            });
        });

        $(function () {
            $("#nausea_slider").slider({
                range: "min",
                value: 5,
                min: 1,
                max: 9,
                slide: function (event, ui) {
                    $("#c_nausea").val(ui.value);
                }
            });
        });

        $(function () {
            $("#fatigue_slider").slider({
                range: "min",
                value: 5,
                min: 1,
                max: 9,
                slide: function (event, ui) {
                    $("#c_fatigue").val(ui.value);
                }
            });
        });

        $(function () {
            $("#sleep_slider").slider({
                range: "min",
                value: 5,
                min: 1,
                max: 9,
                slide: function (event, ui) {
                    $("#c_sleep").val(ui.value);
                }
            });
        });

        $(function () {
            $("#constipation_slider").slider({
                range: "min",
                value: 5,
                min: 1,
                max: 9,
                slide: function (event, ui) {
                    $("#c_constipation").val(ui.value);
                }
            });
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
        Attention: This tracker should not be used to report emergencies. In case of extreme symptoms please contact 911.
        <div>
            Pain scale: 
            <asp:TextBox ID="c_pain" runat="server" Text ="5"/>
            <br />
            <div id="pain_slider"></div>
            <br />
            Nausea scale: 
            <asp:TextBox ID="c_nausea" runat="server" Text ="5"/>
            <br />
            <div id="nausea_slider"></div>
            <br />
            Fatigue scale: 
            <asp:TextBox ID="c_fatigue" runat="server" Text ="5"/>
            <br />
            <div id="fatigue_slider"></div>
            <br />
            Constipation scale: 
            <asp:TextBox ID="c_constipation" runat="server" Text ="5"/>
            <br />
            <div id="constipation_slider"></div>
            <br />
            Sleep scale: 
            <asp:TextBox ID="c_sleep" runat="server" Text ="5"/>
            <br />
            <div id="sleep_slider"></div>
            <br />
            <asp:Button ID="c_AddPainScale" Text="Add Symptoms Entry" OnClick="addPainScale" runat="server" />
        </div>
    </form>
</body>
</html>
