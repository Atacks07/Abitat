<%@ Page Title="General Status" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master"
    CodeBehind="GeneralStatusList.aspx.vb"
    Inherits="Abitat.Pages.Administration.GeneralStatusList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="phHead" runat="server">
</asp:Content>

<asp:Content ID="ctTitle" ContentPlaceHolderID="phPageTitle" runat="server">
    General Statuses
</asp:Content>

<asp:Content ID="cMain" ContentPlaceHolderID="phMain" runat="server">

    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
        <asp:Literal ID="litMessage" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header d-flex align-items-center">
            <i class="bi bi-list-check"></i> General Statuses
        </div>
       <div class="table-responsive">
            <asp:GridView ID="gvStatus" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                CssClass="table table-hover table-striped align-middle mb-0"
                GridLines="None"
                EmptyDataText="No records found."
                EmptyDataRowStyle-CssClass="text-center text-muted py-4">

                <HeaderStyle CssClass="table-light" />

                <Columns>
                    <asp:BoundField DataField="Id"   HeaderText="Id"   ItemStyle-Width="80px" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                </Columns>

            </asp:GridView>

        </div>
    </div>

</asp:Content>

<asp:Content ID="cScripts" ContentPlaceHolderID="phScripts" runat="server">
</asp:Content>