<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Abitat.Pages.Security.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abitat</title>
    <link href="~/Content/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin" defaultfocus="txtUserName">
        <div class="card">
            <h1>Abitat</h1>
            <div class="sub">Contract Request &amp; Authorization</div>

            <label for="txtUserName">Username</label>
            <asp:TextBox ID="txtUserName" runat="server" MaxLength="50" />

            <label for="txtPassword">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="100" />

            <asp:Button ID="btnLogin" runat="server" Text="Sign In"
                        CssClass="btn" OnClick="btnLogin_Click" />

            <asp:Label ID="lblErr" runat="server" CssClass="msg-err" Visible="false" />
        </div>
    </form>
</body>
</html>