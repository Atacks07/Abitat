<%@ Page Title="Currencys" Language="vb" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" CodeBehind="AgreementStatusList.aspx.vb"
    Inherits="Abitat.Pages.Administration.AgreementStatusList" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="phHead" runat="server">
</asp:Content>

<asp:Content ID="ctTitle" ContentPlaceHolderID="phPageTitle" runat="server">
    Agreements Statuses
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="phMain" runat="server">

    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
        <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <div class="card mb-3">
        <div class="card-body">
            <div class="row g-2 align-items-end">
                <div class="col-md-5">
                    <label for="<%= txtSearch.ClientID %>" class="form-label small text-muted mb-1">Search</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                        placeholder="Name..." />
                </div>
                <div class="col-md-3">
                    <label for="<%= ddlStatus.ClientID %>" class="form-label small text-muted mb-1">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-4 text-end">
                    <asp:Button ID="btnSearch" runat="server" Text="Search"
                        CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnNew" runat="server" Text="+ New Agreement Status"
                        CssClass="btn btn-success" OnClick="btnNew_Click"
                        CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header d-flex align-items-center">
            <i class="bi bi-key me-2"></i>Agreements Statuses
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvAgreementsStatuses" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                CssClass="table table-hover table-sm mb-0 align-middle"
                GridLines="None"
                OnRowCommand="gvAgreementsStatuses_RowCommand"
                OnRowDataBound="gvAgreementsStatuses_RowDataBound"
                EmptyDataText="No AgreementsStatuses found."
                EmptyDataRowStyle-CssClass="text-center text-muted p-4">
                <HeaderStyle CssClass="table-light" />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("GeneralStatus.Name") %>' />
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
                                OnClientClick="return confirm('Change the status of this agreements statuses?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
