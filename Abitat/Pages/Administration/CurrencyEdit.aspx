<%@ Page Title="Currency" Language="vb" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" CodeBehind="CurrencyEdit.aspx.vb"
    Inherits="Abitat.Pages.Administration.CurrencyEdit" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="phHead" runat="server">
</asp:Content>

<asp:Content ID="ctTitle" ContentPlaceHolderID="phPageTitle" runat="server">
    <asp:Literal ID="litPageTitle" runat="server" Text="New Currency" />
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="phMain" runat="server">

    <asp:HiddenField ID="hfId" runat="server" />

    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
        <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header d-flex align-items-center">
            <i class="bi bi-key me-2"></i>
            <asp:Literal ID="litCardTitle" runat="server" Text="New Currency" />
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-8">

                    <div class="mb-3">
                        <label for="<%= txtName.ClientID %>" class="form-label">
                            Name <span class="text-danger">*</span>
                        </label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                            MaxLength="100" placeholder="Short descriptive name" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="txtName"
                            ErrorMessage="Name is required."
                            ValidationGroup="CurrencyForm"
                            CssClass="text-danger small"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="<%= ddlStatus.ClientID %>" class="form-label">
                            Status <span class="text-danger">*</span>
                        </label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" />
                    </div>

                </div>
            </div>
        </div>
        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-secondary"
                OnClick="btnCancel_Click" CausesValidation="false">
                <i class="bi bi-x-lg"></i> Cancel
            </asp:LinkButton>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                OnClick="btnSave_Click" ValidationGroup="CurrencyForm" />
        </div>
    </div>

</asp:Content>
