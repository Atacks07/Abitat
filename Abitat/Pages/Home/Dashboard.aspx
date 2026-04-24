<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="Abitat.Pages.Home.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abitat</title>
    <style>
        * { box-sizing: border-box; }
        body { font-family: Segoe UI, Arial, sans-serif; background:#f4f6f8; margin:0; }
        header {
            background:#2c3e50; color:#fff; padding:15px 30px;
            display:flex; align-items:center; justify-content:space-between;
        }
        header h1 { margin:0; font-size:20px; }
        .user-box { display:flex; align-items:center; gap:15px; font-size:14px; }
        .user-box .name { font-weight:600; }
        .btn-signout {
            background:transparent; color:#fff; border:1px solid #fff;
            padding:6px 14px; border-radius:4px; cursor:pointer; font-size:13px;
        }
        .btn-signout:hover { background:#fff; color:#2c3e50; }
        main { max-width:1100px; margin:30px auto; padding:0 20px; }
        .card {
            background:#fff; border-radius:8px; box-shadow:0 2px 8px rgba(0,0,0,.06);
            padding:25px 30px; margin-bottom:20px;
        }
        .card h2 { margin-top:0; color:#2c3e50; font-size:18px; }
        .kv { display:grid; grid-template-columns:180px 1fr; gap:10px 20px; font-size:14px; }
        .kv dt { color:#7f8c8d; font-weight:600; }
        .kv dd { margin:0; color:#2c3e50; }
        table { width:100%; border-collapse:collapse; font-size:13px; }
        table th, table td { text-align:left; padding:8px 12px; border-bottom:1px solid #ecf0f1; }
        table th { background:#f8f9fa; color:#7f8c8d; font-weight:600; text-transform:uppercase; font-size:11px; }
        .empty { color:#95a5a6; font-style:italic; padding:15px 0; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>Abitat &mdash; Contract Authorization</h1>
            <div class="user-box">
                <span>Welcome, <span class="name"><asp:Literal ID="litUserName" runat="server" /></span></span>
                <asp:Button ID="btnSignOut" runat="server" Text="Sign out"
                            CssClass="btn-signout" OnClick="btnSignOut_Click" CausesValidation="false" />
            </div>
        </header>

        <main>
            <div class="card">
                <h2>Session</h2>
                <dl class="kv">
                    <dt>User Id</dt><dd><asp:Literal ID="litUserId" runat="server" /></dd>
                    <dt>Username</dt><dd><asp:Literal ID="litUserName2" runat="server" /></dd>
                    <dt>Profile Id</dt><dd><asp:Literal ID="litProfileId" runat="server" /></dd>
                </dl>
            </div>

            <div class="card">
                <h2>Permissions</h2>
                <asp:GridView ID="gvPermissions" runat="server" AutoGenerateColumns="false"
                              GridLines="None" CssClass="" EmptyDataText="No permissions assigned.">
                    <Columns>
                        <asp:BoundField DataField="Id"   HeaderText="Id" />
                        <asp:BoundField DataField="Code" HeaderText="Code" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                    </Columns>
                </asp:GridView>
            </div>
        </main>
    </form>
</body>
</html>