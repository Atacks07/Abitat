<%@ Page Title="Manage Permissions" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master"
    CodeBehind="PermissionProfileAssign.aspx.vb"
    Inherits="Abitat.Pages.Administration.PermissionProfileAssign" %>

<asp:Content ID="cHead" ContentPlaceHolderID="phHead" runat="server">
</asp:Content>

<asp:Content ID="ctTitle" ContentPlaceHolderID="phPageTitle" runat="server">
    <asp:Literal ID="litPageTitle" runat="server" Text="Manage Permissions" />
</asp:Content>

<asp:Content ID="cMain" ContentPlaceHolderID="phMain" runat="server">

    <asp:HiddenField ID="hfProfileId" runat="server" />

    <asp:Panel ID="pnlMessage" runat="server" Visible="false"
               CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <div class="card shadow-sm">
        <div class="card-header bg-white d-flex align-items-center justify-content-between">
            <div>
                <i class="bi bi-shield-lock me-2"></i>
                Permissions for:
                <strong><asp:Literal ID="litProfileName" runat="server" /></strong>
            </div>
            <div>
                <button type="button" class="btn btn-sm btn-outline-secondary"
                        onclick="toggleAllPermissions(true); return false;">
                    Check all
                </button>
                <button type="button" class="btn btn-sm btn-outline-secondary"
                        onclick="toggleAllPermissions(false); return false;">
                    Uncheck all
                </button>
            </div>
        </div>

        <div class="card-body">

            <asp:Repeater ID="rptPermissions" runat="server">
                <HeaderTemplate>
                    <div class="list-group">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="list-group-item">
                        <asp:HiddenField ID="hfPermissionId" runat="server"
                                         Value='<%# Eval("Id") %>' />
                        <asp:CheckBox ID="chkPermission" runat="server"
                            CssClass="form-check-input me-2"
                            Checked='<%# CBool(Eval("IsAssigned")) %>'
                            Text='<%# CStr(Eval("Code")) & " — " & CStr(Eval("Name")) %>' />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

            <asp:Panel ID="pnlEmpty" runat="server" Visible="false"
                       CssClass="text-center text-muted py-4">
                <i class="bi bi-inbox fs-1"></i>
                <p class="mt-2">No active permissions available.</p>
            </asp:Panel>

        </div>

        <div class="card-footer bg-white text-end">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                CssClass="btn btn-outline-secondary me-2"
                CausesValidation="false"
                OnClick="btnCancel_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save Changes"
                CssClass="btn btn-primary"
                OnClick="btnSave_Click" />
        </div>
    </div>

</asp:Content>

<asp:Content ID="cScripts" ContentPlaceHolderID="phScripts" runat="server">
    <script>
        function toggleAllPermissions(checked) {
            var checkboxes = document.querySelectorAll('.list-group-item input[type=checkbox]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = checked;
            }
        }
    </script>
</asp:Content>