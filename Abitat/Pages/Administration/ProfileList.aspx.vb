Imports Abitat.Business.Abitat.Business
Imports Abitat.Entities
Imports Abitat.Infrastructure

Namespace Pages.Administration

    Public Class ProfileList
        Inherits AuthenticatedPage

        Private ReadOnly _profileService As New ProfileService()
        Private ReadOnly _statusService As New GeneralStatusService()

        Protected Overrides ReadOnly Property RequiredPermission As String
            Get
                Return "PROFILES_VIEW"
            End Get
        End Property
        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                LoadStatusDropDown()
                BindGrid()
            End If
        End Sub

        Private Sub LoadStatusDropDown()
            ddlStatus.Items.Clear()
            ddlStatus.Items.Add(New ListItem("All", ""))
            For Each s As GeneralStatus In _statusService.GetAll()
                ddlStatus.Items.Add(New ListItem(s.Name, s.Id.ToString()))
            Next
        End Sub

        Private Sub BindGrid()
            Dim search As String = txtSearch.Text.Trim()
            Dim statusId As Integer? = Nothing
            If Not String.IsNullOrWhiteSpace(ddlStatus.SelectedValue) Then
                statusId = CInt(ddlStatus.SelectedValue)
            End If

            gvProfiles.DataSource = _profileService.GetAll(search, statusId)
            gvProfiles.DataBind()
        End Sub
        Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
            BindGrid()
        End Sub

        Protected Sub btnNew_Click(sender As Object, e As EventArgs)
            Response.Redirect("~/Pages/Administration/ProfileEdit.aspx", False)
        End Sub

        Protected Sub gvProfiles_RowCommand(sender As Object, e As GridViewCommandEventArgs)
            Dim id As Integer
            If Not Integer.TryParse(CStr(e.CommandArgument), id) Then Return

            Select Case e.CommandName
                Case "EditItem"
                    Response.Redirect("~/Pages/Administration/ProfileEdit.aspx?id=" & id, False)

                Case "ToggleStatus"
                    Try
                        Dim current As Profile = _profileService.GetById(id)
                        If current Is Nothing Then
                            ShowMessage("Profile not found.", False)
                            Return
                        End If

                        Dim statuses = _statusService.GetAll()
                        Dim activeId As Integer =
                            statuses.First(Function(s) s.Name.Equals("Active", StringComparison.OrdinalIgnoreCase)).Id
                        Dim inactiveId As Integer =
                            statuses.First(Function(s) s.Name.Equals("Inactive", StringComparison.OrdinalIgnoreCase)).Id

                        Dim newStatusId As Integer =
                            If(current.GeneralStatusId = activeId, inactiveId, activeId)

                        _profileService.ChangeStatus(id, newStatusId)
                        ShowMessage($"Profile '{current.Name}' updated.", True)
                        BindGrid()

                    Catch ex As Exception
                        ShowMessage("Error: " & ex.Message, False)
                    End Try
                Case "ManagePermissions"
                    Response.Redirect("~/Pages/Administration/PermissionProfileAssign.aspx?profileId=" & id, False)
            End Select
        End Sub

        Protected Sub gvProfiles_RowDataBound(sender As Object, e As GridViewRowEventArgs)
            If e.Row.RowType <> DataControlRowType.DataRow Then Return

            Dim prof As Profile = CType(e.Row.DataItem, Profile)
            Dim isActive As Boolean = prof.GeneralStatus IsNot Nothing AndAlso
                                      prof.GeneralStatus.Name.Equals("Active", StringComparison.OrdinalIgnoreCase)

            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            If lblStatus IsNot Nothing Then
                lblStatus.CssClass = If(isActive,
                                        "badge bg-success-subtle text-success border border-success-subtle",
                                        "badge bg-secondary-subtle text-secondary border border-secondary-subtle")
            End If

            Dim btnToggle As LinkButton = CType(e.Row.FindControl("btnToggle"), LinkButton)
            If btnToggle IsNot Nothing Then
                If isActive Then
                    btnToggle.Text = "<i class='bi bi-x-circle'></i> Deactivate"
                    btnToggle.CssClass = "btn btn-sm btn-outline-danger"
                Else
                    btnToggle.Text = "<i class='bi bi-check-circle'></i> Activate"
                    btnToggle.CssClass = "btn btn-sm btn-outline-success"
                End If
            End If
        End Sub

        Private Sub ShowMessage(ByVal text As String, ByVal isSuccess As Boolean)
            litMessage.Text = Server.HtmlEncode(text)
            pnlMessage.CssClass = If(isSuccess, "alert alert-success mb-3", "alert alert-danger mb-3")
            pnlMessage.Visible = True
        End Sub

    End Class

End Namespace