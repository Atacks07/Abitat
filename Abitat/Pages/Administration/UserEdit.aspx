<%@ Page Title="User" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master"
    CodeBehind="UserEdit.aspx.vb"
    Inherits="Abitat.Pages.Administration.UserEdit" %>

<asp:Content ID="cHead" ContentPlaceHolderID="phHead" runat="server">
</asp:Content>

<asp:Content ID="ctTitle" ContentPlaceHolderID="phPageTitle" runat="server">
    <asp:Literal ID="litPageTitle" runat="server" Text="New User" />
</asp:Content>

<asp:Content ID="cMain" ContentPlaceHolderID="phMain" runat="server">

    <asp:HiddenField ID="hfId" runat="server" />

    <asp:Panel ID="pnlMessage" runat="server" Visible="false"
               CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <div class="card shadow-sm">
        <div class="card-header bg-white d-flex align-items-center">
            <i class="bi bi-person-gear me-2"></i>
            <asp:Literal ID="litCardTitle" runat="server" Text="New User" />
        </div>
        <div class="card-body">

            <asp:ValidationSummary ID="vsForm" runat="server"
                ValidationGroup="UserForm"
                CssClass="alert alert-danger"
                HeaderText="Please fix the following errors:" />

            <div class="row g-3">

                <div class="col-md-6">
                    <label for="<%= txtUserName.ClientID %>" class="form-label">
                        Username <span class="text-danger">*</span>
                    </label>
                    <asp:TextBox ID="txtUserName" runat="server"
                        CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                        ControlToValidate="txtUserName"
                        ErrorMessage="Username is required."
                        Display="Dynamic"
                        CssClass="text-danger small"
                        ValidationGroup="UserForm" />
                    <asp:RegularExpressionValidator ID="revUserName" runat="server"
                        ControlToValidate="txtUserName"
                        ValidationExpression="^[a-zA-Z0-9._-]{3,50}$"
                        ErrorMessage="Username must be 3-50 characters (letters, numbers, dot, dash, underscore)."
                        Display="Dynamic"
                        CssClass="text-danger small"
                        ValidationGroup="UserForm" />
                </div>

                <div class="col-md-6">
                    <label for="<%= txtPassword.ClientID %>" class="form-label">
                        <asp:Literal ID="litPasswordLabel" runat="server" Text="Password" />
                        <span class="text-danger">*</span>
                    </label>
                    <asp:TextBox ID="txtPassword" runat="server"
                        CssClass="form-control" TextMode="Password" MaxLength="100" />
                    <small class="form-text text-muted">
                        <asp:Literal ID="litPasswordHelp" runat="server"
                            Text="Minimum 8 characters." />
                    </small>
                </div>

                <div class="col-md-6">
                    <label for="<%= ddlProfile.ClientID %>" class="form-label">
                        Profile <span class="text-danger">*</span>
                    </label>
                    <asp:DropDownList ID="ddlProfile" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator ID="rfvProfile" runat="server"
                        ControlToValidate="ddlProfile"
                        InitialValue=""
                        ErrorMessage="Profile is required."
                        Display="Dynamic"
                        CssClass="text-danger small"
                        ValidationGroup="UserForm" />
                </div>

                <div class="col-md-6">
                    <label for="<%= ddlStatus.ClientID %>" class="form-label">
                        Status <span class="text-danger">*</span>
                    </label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server"
                        ControlToValidate="ddlStatus"
                        InitialValue=""
                        ErrorMessage="Status is required."
                        Display="Dynamic"
                        CssClass="text-danger small"
                        ValidationGroup="UserForm" />
                </div>

            </div>

        </div>

        <div class="card-footer bg-white text-end">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                CssClass="btn btn-outline-secondary me-2"
                CausesValidation="false"
                OnClick="btnCancel_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save"
                CssClass="btn btn-primary"
                ValidationGroup="UserForm"
                OnClick="btnSave_Click" />
        </div>
    </div>

</asp:Content>

<asp:Content ID="cScripts" ContentPlaceHolderID="phScripts" runat="server">
</asp:Content>