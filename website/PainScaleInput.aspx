<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PainScaleInput.aspx.cs" Inherits="PainScaleInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Pain scale: 
            <asp:TextBox ID="c_pain" runat="server" />
            <br />
            Nausea scale: 
            <asp:TextBox ID="c_nausea" runat="server" />
            <br />
            Fatigue scale: 
            <asp:TextBox ID="c_fatigue" runat="server" />
            <br />
            Constipation scale: 
            <asp:TextBox ID="c_constipation" runat="server" />
            <br />
            Sleep scale: 
            <asp:TextBox ID="c_sleep" runat="server" />
            <br />
            <asp:Button ID="c_AddPainScale" Text="Add Pain Scale Entry" OnClick="addPainScale" runat="server"/>
        </div>
    </form>
</body>
</html>
