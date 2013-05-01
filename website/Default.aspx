<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:MultiView ID="StartupData" runat="server">
            <asp:View ID="ApplicationData" runat="server">
                <b>Welcome to HealthVault</b>  <br /><br />
                This is the account of Mr <asp:Label ID="c_UserName" runat="Server"/><br />
                <b>Basic Application Data</b>  <br />
                <asp:Label ID="AppName" runat="server" Text="Application Name: " /><br />
                Here is what you can do<br />
                <asp:Button ID="c_addSymmtoms" Text="Add Symptoms Entry" OnClick="addSymptoms" runat="server"/>
                <asp:Button ID="c_viewSymptomSummary" Text="View Symptom History" OnClick="viewSymptomSummary" runat="server"/>
            </asp:View>
            <asp:View ID="ErrorData" runat="server">
                <asp:Label ID="Error" runat="server" Text="Error was encountered when trying to retrieve application data: " />
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
