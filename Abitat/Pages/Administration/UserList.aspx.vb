Imports Abitat.Business.Abitat.Business
Imports Abitat.Entities
Imports Abitat.Infrastructure

Namespace Pages.Administration

    Public Class UserList
        Inherits AuthenticatedPage

        Private ReadOnly _userService As New UserService()
        Private ReadOnly _generalStatusService As New GeneralStatusService()
        Private ReadOnly _profileService As New ProfileService()

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                LoadStatusDropDown()
                LoadProfileDropDown()
                BindGrid()
            End If
        End Sub

        Private Sub LoadStatusDropDown()
            ddlStatus.Items.Clear()
            ddlStatus.Items.Add(New ListItem("All", ""))
            For Each st In _generalStatusService.GetAll()
                ddlStatus.Items.Add(New ListItem(st.Name, st.Id.ToString()))
            Next
        End Sub

        Private Sub LoadProfileDropDown()
            ddlProfile.Items.Clear()
            ddlProfile.Items.Add(New ListItem("All", ""))
            For Each st In _profileService.GetAll()
                ddlProfile.Items.Add(New ListItem(st.Name, st.Id.ToString()))
            Next
        End Sub

        Private Sub BindGrid()
            Try
                Dim search As String = txtSearch.Text.Trim()
                Dim statusId As Integer? =
                    If(String.IsNullOrEmpty(ddlStatus.SelectedValue),
                       CType(Nothing, Integer?),
                       CInt(ddlStatus.SelectedValue))

                Dim profileId As Integer? =
                    If(String.IsNullOrEmpty(ddlProfile.SelectedValue),
                       CType(Nothing, Integer?),
                       CInt(ddlProfile.SelectedValue))

                gvUsers.DataSource = _userService.GetAll(search, statusId, profileId)
                gvUsers.DataBind()
            Catch ex As Exception
                ShowMessage("Error loading data: " & ex.Message, "danger")
            End Try
        End Sub
        Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
            BindGrid()
        End Sub

        Protected Sub btnNew_Click(sender As Object, e As EventArgs)
            Response.Redirect("~/Pages/Administration/UserEdit.aspx", False)
        End Sub

        Protected Sub gvUsers_RowCommand(sender As Object, e As GridViewCommandEventArgs)
            Dim id As Integer = CInt(e.CommandArgument)

            Select Case e.CommandName
                Case "EditItem"
                    Response.Redirect("~/Pages/Administration/UserEdit.aspx?id=" & id, False)

                Case "ToggleStatus"
                    Try
                        Dim user = _userService.GetById(id)
                        If user IsNot Nothing Then
                            Dim newStatus As Integer = If(user.GeneralStatusId = 1, 2, 1)
                            _userService.ChangeStatus(id, newStatus)
                            ShowMessage("Status updated successfully.", "success")
                        End If
                    Catch ex As Exception
                        ShowMessage("Error: " & ex.Message, "danger")
                    End Try
                    BindGrid()
            End Select
        End Sub

        Protected Sub gvUsers_RowDataBound(sender As Object, e As GridViewRowEventArgs)
            If e.Row.RowType <> DataControlRowType.DataRow Then Return

            Dim user As User = TryCast(e.Row.DataItem, User)
            If user Is Nothing Then Return

            Dim lblStatus As Label = TryCast(e.Row.FindControl("lblStatus"), Label)

            If lblStatus IsNot Nothing AndAlso user.GeneralStatus IsNot Nothing Then
                Dim isActive As Boolean = String.Equals(user.GeneralStatus.Name, "Active",
                                                        StringComparison.OrdinalIgnoreCase)
                lblStatus.CssClass = If(isActive, "badge bg-success", "badge bg-secondary")
            End If

            Dim btnToggle As LinkButton = TryCast(e.Row.FindControl("btnToggle"), LinkButton)
            If btnToggle IsNot Nothing AndAlso user.GeneralStatus IsNot Nothing Then
                Dim isActive As Boolean = String.Equals(user.GeneralStatus.Name, "Active",
                                                        StringComparison.OrdinalIgnoreCase)
                If isActive Then
                    btnToggle.Text = "<i class='bi bi-x-circle'></i> Deactivate"
                    btnToggle.CssClass = "btn btn-sm btn-outline-danger"
                Else
                    btnToggle.Text = "<i class='bi bi-toggle-on'></i> Activate"
                    btnToggle.CssClass = "btn btn-sm btn-outline-success"
                End If
            End If

            If user.Id = CurrentUserId AndAlso btnToggle IsNot Nothing Then
                btnToggle.Enabled = False
                btnToggle.ToolTip = "You cannot deactivate yourself"
                btnToggle.OnClientClick = "return false;"
            End If
        End Sub

        Private Sub ShowMessage(message As String, type As String)
            pnlMessage.Visible = True
            pnlMessage.CssClass = "alert alert-" & type & " alert-dismissible fade show"
            litMessage.Text = message &
                "<button type='button' class='btn-close' data-bs-dismiss='alert'></button>"
        End Sub

    End Class

End Namespace