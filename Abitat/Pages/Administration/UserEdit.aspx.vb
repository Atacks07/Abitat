Imports Abitat.Business.Abitat.Business
Imports Abitat.Entities
Imports Abitat.Infrastructure

Namespace Pages.Administration

    Public Class UserEdit
        Inherits AuthenticatedPage

        Private ReadOnly _userService As New UserService()
        Private ReadOnly _profileService As New ProfileService()
        Private ReadOnly _generalStatusService As New GeneralStatusService()

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                LoadDropDowns()
                InitializeForm()
            End If
        End Sub
        Private Sub LoadDropDowns()

            ddlProfile.Items.Clear()
            ddlProfile.Items.Add(New ListItem("-- Select --", ""))
            For Each p In _profileService.GetAll()
                ddlProfile.Items.Add(New ListItem(p.Name, p.Id.ToString()))
            Next

            ddlStatus.Items.Clear()
            ddlStatus.Items.Add(New ListItem("-- Select --", ""))
            For Each s In _generalStatusService.GetAll()
                ddlStatus.Items.Add(New ListItem(s.Name, s.Id.ToString()))
            Next
        End Sub

        Private Sub InitializeForm()
            Dim idParam As String = Request.QueryString("id")

            If String.IsNullOrEmpty(idParam) Then

                hfId.Value = "0"
                litPageTitle.Text = "New User"
                litCardTitle.Text = "New User"
                litPasswordLabel.Text = "Password"
                litPasswordHelp.Text = "Minimum 8 characters."
                rfvUserName.Enabled = True
                Return
            End If

            Dim id As Integer
            If Not Integer.TryParse(idParam, id) OrElse id <= 0 Then
                ShowMessage("Invalid user Id.", "danger")
                Return
            End If

            Try
                Dim user As User = _userService.GetById(id)
                If user Is Nothing Then
                    ShowMessage("User not found.", "danger")
                    Return
                End If

                hfId.Value = user.Id.ToString()
                litPageTitle.Text = "Edit User"
                litCardTitle.Text = "Edit User"
                litPasswordLabel.Text = "New Password"
                litPasswordHelp.Text = "Leave blank to keep current password. Minimum 8 characters if changed."

                txtUserName.Text = user.UserName
                txtPassword.Text = String.Empty

                If ddlProfile.Items.FindByValue(user.ProfileId.ToString()) IsNot Nothing Then
                    ddlProfile.SelectedValue = user.ProfileId.ToString()
                End If

                If ddlStatus.Items.FindByValue(user.GeneralStatusId.ToString()) IsNot Nothing Then
                    ddlStatus.SelectedValue = user.GeneralStatusId.ToString()
                End If

            Catch ex As Exception
                ShowMessage("Error loading user: " & ex.Message, "danger")
            End Try
        End Sub
        Protected Sub btnSave_Click(sender As Object, e As EventArgs)
            If Not Page.IsValid Then Return

            Dim id As Integer = CInt(hfId.Value)
            Dim isCreate As Boolean = (id = 0)

            If isCreate AndAlso String.IsNullOrWhiteSpace(txtPassword.Text) Then
                ShowMessage("Password is required for new users.", "danger")
                Return
            End If

            If Not String.IsNullOrWhiteSpace(txtPassword.Text) AndAlso
               txtPassword.Text.Length < 8 Then
                ShowMessage("Password must be at least 8 characters.", "danger")
                Return
            End If

            Dim user As New User With {
                .Id = id,
                .UserName = txtUserName.Text.Trim(),
                .Password = txtPassword.Text,
                .ProfileId = CInt(ddlProfile.SelectedValue),
                .GeneralStatusId = CInt(ddlStatus.SelectedValue)
            }

            Try
                If isCreate Then
                    _userService.Create(user)
                Else
                    _userService.Update(user)
                End If

                Response.Redirect("~/Pages/Administration/UserList.aspx", False)

            Catch ex As Exception
                ShowMessage("Error saving user: " & ex.Message, "danger")
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
            Response.Redirect("~/Pages/Administration/UserList.aspx", False)
        End Sub

        Private Sub ShowMessage(message As String, type As String)
            pnlMessage.Visible = True
            pnlMessage.CssClass = "alert alert-" & type & " alert-dismissible fade show"
            litMessage.Text = message &
                "<button type='button' class='btn-close' data-bs-dismiss='alert'></button>"
        End Sub

    End Class

End Namespace