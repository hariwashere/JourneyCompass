<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PainScaleInput.aspx.cs" Inherits="PainScaleInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            You should enter the pain scale here
            <br />
            <asp:TextBox ID="c_Pain" runat="server" />
            <asp:Button ID="c_AddPainScale" Text="Add Pain Scale Entry" OnClick="addPainScale" runat="server"/>
        </div>
    </form>
</body>
</html>
