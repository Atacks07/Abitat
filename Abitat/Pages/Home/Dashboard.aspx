<%@ Page Title="Dashboard" Language="vb" MasterPageFile="~/Site.Master"
         AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb"
         Inherits="Abitat.Pages.Home.Dashboard" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="phHead" runat="server">
</asp:Content>

<asp:Content ID="ctTitle" ContentPlaceHolderID="phPageTitle" runat="server">
    Dashboard
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="phMain" runat="server">

    <div class="row g-3 mb-4">
        <div class="col-md-4">
            <div class="card h-100">
                <div class="card-body">
                    <h6 class="text-muted mb-2"><i class="bi bi-person-circle"></i> User</h6>
                    <div class="fs-5 fw-semibold"><asp:Literal ID="litUserName" runat="server" /></div>
                    <div class="text-muted small">ID: <asp:Literal ID="litUserId" runat="server" /></div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card h-100">
                <div class="card-body">
                    <h6 class="text-muted mb-2"><i class="bi bi-shield-lock"></i> Profile</h6>
                    <div class="fs-5 fw-semibold">Profile ID: <asp:Literal ID="litProfileId" runat="server" /></div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card h-100">
                <div class="card-body">
                    <h6 class="text-muted mb-2"><i class="bi bi-key"></i> Permissions</h6>
                    <div class="fs-5 fw-semibold"><asp:Literal ID="litPermissionCount" runat="server" /></div>
                    <div class="text-muted small">granted permissions</div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header">Your permissions</div>
        <div class="card-body p-0">
            <asp:GridView ID="gvPermissions" runat="server" AutoGenerateColumns="false"
                          CssClass="table table-hover table-sm mb-0"
                          GridLines="None" EmptyDataText="No permissions assigned.">
                <HeaderStyle CssClass="table-light" />
                <Columns>
                    <asp:BoundField DataField="Id"   HeaderText="Id"   ItemStyle-Width="80px" />
                    <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>