<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Abitat.Pages.Security.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abitat</title>
    <style>
        body { font-family: Segoe UI, Arial, sans-serif; background:#f4f6f8; margin:0; }
        .card {
            max-width: 380px; margin: 100px auto; padding: 30px 35px; background:#fff;
            border-radius: 8px; box-shadow: 0 2px 15px rgba(0,0,0,.08);
        }
        h1 { margin:0 0 5px 0; color:#2c3e50; font-size:26px; }
        .sub { color:#7f8c8d; font-size:13px; margin-bottom:25px; }
        label { display:block; margin-top:14px; font-weight:600; color:#34495e; font-size:13px; }
        input[type=text], input[type=password] {
            width:100%; padding:10px 12px; margin-top:5px; border:1px solid #ccc;
            border-radius:4px; box-sizing:border-box; font-size:14px;
        }
        input[type=text]:focus, input[type=password]:focus {
            outline:none; border-color:#2c3e50; box-shadow:0 0 0 3px rgba(44,62,80,.1);
        }
        .btn {
            margin-top:25px; width:100%; padding:12px; background:#2c3e50; color:#fff;
            border:0; border-radius:4px; font-weight:600; cursor:pointer; font-size:14px;
        }
        .btn:hover { background:#34495e; }
        .msg-err {
            color:#c0392b; margin-top:15px; font-size:13px; font-weight:600;
            padding:10px; background:#fadbd8; border-radius:4px; display:block;
        }
    </style>
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