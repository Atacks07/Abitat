<%@ Page Title="Users" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master"
    CodeBehind="UserList.aspx.vb"
    Inherits="Abitat.Pages.Administration.UserList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="phHead" runat="server">
</asp:Content>

<asp:Content ID="ctTitle" ContentPlaceHolderID="phPageTitle" runat="server">
    Users
</asp:Content>

<asp:Content ID="cMain" ContentPlaceHolderID="phMain" runat="server">

    <asp:Panel ID="pnlMessage" runat="server" Visible="false"
        CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <div class="card mb-3 shadow-sm">
        <div class="card-body">
            <div class="row g-2 align-items-end">
                <div class="col-md-3">
                    <label for="<%= txtSearch.ClientID %>" class="form-label small text-muted mb-1">Search</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                        placeholder="Search..." />
                </div>
                <div class="col-md-3">
                    <label for="<%= ddlStatus.ClientID %>" class="form-label small text-muted mb-1">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-3">
                    <label for="<%= ddlProfile.ClientID %>" class="form-label small text-muted mb-1">Profile</label>
                    <asp:DropDownList ID="ddlProfile" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-3 text-end">
                    <asp:Button ID="btnSearch" runat="server" Text="Search"
                        CssClass="btn btn-primary" OnClick="btnSearch_Click"
                        CausesValidation="false" />
                    <asp:Button ID="btnNew" runat="server" Text="+ New User"
                        CssClass="btn btn-success" OnClick="btnNew_Click"
                        CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="card shadow-sm">
        <div class="card-header bg-white d-flex align-items-center">
            <i class="bi bi-people me-2"></i>Users
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvUsers" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                CssClass="table table-hover table-striped align-middle mb-0"
                GridLines="None"
                OnRowCommand="gvUsers_RowCommand"
                OnRowDataBound="gvUsers_RowDataBound"
                EmptyDataText="No records found."
                EmptyDataRowStyle-CssClass="text-center text-muted py-4">

                <HeaderStyle CssClass="table-light" />

                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Username" />

                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"
                                Text='<%# Eval("GeneralStatus.Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Profile" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="lblProfile" runat="server"
                                Text='<%# Eval("Profile.Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="230px"
                        ItemStyle-CssClass="text-end pe-3" HeaderStyle-CssClass="text-end pe-3">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server"
                                CommandName="EditItem"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-sm btn-outline-primary"
                                CausesValidation="false">
                                <i class="bi bi-pencil"></i> Edit
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnToggle" runat="server"
                                CommandName="ToggleStatus"
                                CommandArgument='<%# Eval("Id") %>'
                                CausesValidation="false"
                                OnClientClick="return confirm('Change the status of this user?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
    </div>

</asp:Content>

<asp:Content ID="cScripts" ContentPlaceHolderID="phScripts" runat="server">
</asp:Content>
