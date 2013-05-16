<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JourneyCompass</title>
    <link rel="stylesheet" type="text/css" href="includes/theme.css" />
</head>
<body>
    <table width="100%" border="0" cellpadding="5">
        <tr>
            <td width="20" />
            <td>
                <img alt="title" src="images/JourneyCompass_1_medium.png" border="0" />

            </td>
        </tr>
    </table>
    <div style="background: #005C8A; height: 27px; opacity: 0.3; filter: alpha(opacity=30);">
        <table width="100%" border="0" cellpadding="2">
            <tr>
                <td style="width: 30px" />
                <td style="text-align: left"><a href="SymptomSummary.aspx" target="_blank">REPORT</a> | HELP</td>
            </tr>
        </table>
    </div>
    <div style="background: #6AB2BA; margin: 50px; border: 1px solid #198895;">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left; height: 35px;">
                    <asp:MultiView ID="StartupData" runat="server">
                        <asp:View ID="ApplicationData" runat="server">
                            <table width="100%" border="0" cellpadding="5" cellspacing="5">
                                <tr>
                                    <td>Welcome <b>
                                        <asp:Label ID="c_UserName" runat="Server" /></b>,<br />
                                        <br />
                                        Please use the following slide bar to indicate stress level of your symptoms. 
                                        Note that <b>0 means the least stressful and 10 means the most stressful</b>. After choosing your level
                                        of symptoms, press Add symptoms button to submit.<br />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ErrorData" runat="server">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="AppName" runat="server" Text="Application Name: " /><br />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Error" runat="server" Text="Error was encountered when trying to retrieve application data: " />
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="line-separator" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="12" cellspacing="5">
                        <tr>
                            <td style="height:500px; vertical-align:top;">
                                <iframe style="border:none; width:100%; height:500px;" src="SymptomInput.aspx" scrolling="auto" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
