<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccessDenied.aspx.vb" Inherits="AccessDenied" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso Denegado</title>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Content/access-denied.css") %>" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="card">

                <h1>:(</h1>
                <h2>Access Denied</h2>
                <p>
                    You do not have permission to view this page.<br />
                    Please check your credentials and try again.
                </p>

                <asp:Button
                    ID="btnBack"
                    runat="server"
                    Text="Return"
                    CssClass="btn"
                    PostBackUrl="~/Pages/Home/Dashboard.aspx" />
            </div>
        </div>
    </form>
</body>
</html>
