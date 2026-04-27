Imports Abitat.Business.Abitat.Business
Imports Abitat.Entities
Imports Abitat.Infrastructure

Namespace Pages.Administration

    Public Class PermissionProfileAssign
        Inherits AuthenticatedPage

        Private ReadOnly _permissionProfileService As New PermissionProfileService()
        Private ReadOnly _profileService As New ProfileService()

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                InitializePage()
            End If
        End Sub

        Private Sub InitializePage()
            Dim profileIdParam As String = Request.QueryString("profileId")

            If String.IsNullOrEmpty(profileIdParam) Then
                Response.Redirect("~/Pages/Administration/ProfileList.aspx", False)
                Return
            End If

            Dim profileId As Integer
            If Not Integer.TryParse(profileIdParam, profileId) OrElse profileId <= 0 Then
                ShowMessage("Invalid profile Id.", "danger")
                Return
            End If

            Try
                Dim profile As Profile = _profileService.GetById(profileId)
                If profile Is Nothing Then
                    ShowMessage("Profile not found.", "danger")
                    Return
                End If

                hfProfileId.Value = profile.Id.ToString()
                litProfileName.Text = profile.Name
                litPageTitle.Text = "Permissions for: " & profile.Name

                BindPermissions()

            Catch ex As Exception
                ShowMessage("Error loading profile: " & ex.Message &
                    "<br/><pre>" & ex.StackTrace & "</pre>", "danger")
            End Try
        End Sub

        Private Sub BindPermissions()
            Dim profileId As Integer = CInt(hfProfileId.Value)
            Dim permissions As List(Of Permission) =
                _permissionProfileService.GetByProfileId(profileId)

            If permissions Is Nothing OrElse permissions.Count = 0 Then
                pnlEmpty.Visible = True
                rptPermissions.Visible = False
                Return
            End If

            pnlEmpty.Visible = False
            rptPermissions.Visible = True
            rptPermissions.DataSource = permissions
            rptPermissions.DataBind()
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)
            Dim profileId As Integer
            If Not Integer.TryParse(hfProfileId.Value, profileId) OrElse profileId <= 0 Then
                ShowMessage("Invalid profile Id.", "danger")
                Return
            End If

            Try
                Dim selectedIds As New List(Of Integer)()

                For Each item As RepeaterItem In rptPermissions.Items
                    If item.ItemType <> ListItemType.Item AndAlso
                       item.ItemType <> ListItemType.AlternatingItem Then
                        Continue For
                    End If

                    Dim chk As CheckBox = TryCast(item.FindControl("chkPermission"), CheckBox)
                    Dim hf As HiddenField = TryCast(item.FindControl("hfPermissionId"), HiddenField)

                    If chk IsNot Nothing AndAlso hf IsNot Nothing AndAlso chk.Checked Then
                        selectedIds.Add(CInt(hf.Value))
                    End If
                Next

                _permissionProfileService.Save(profileId, selectedIds)

                ShowMessage("Permissions updated successfully.", "success")
                BindPermissions()

            Catch ex As Exception
                ShowMessage("Error saving permissions: " & ex.Message, "danger")
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
            Response.Redirect("~/Pages/Administration/ProfileList.aspx", False)
        End Sub

        Private Sub ShowMessage(message As String, type As String)
            pnlMessage.Visible = True
            pnlMessage.CssClass = "alert alert-" & type & " alert-dismissible fade show"
            litMessage.Text = message &
                "<button type='button' class='btn-close' data-bs-dismiss='alert'></button>"
        End Sub

    End Class

End Namespace