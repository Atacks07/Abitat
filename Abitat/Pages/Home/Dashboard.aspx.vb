Imports Abitat.Infrastructure
Imports Abitat.Business.Services.Security

Namespace Pages.Home

    Public Class Dashboard
        Inherits AuthenticatedPage

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                BindDashboard()
            End If
        End Sub

        Private Sub BindDashboard()
            litUserName.Text = Server.HtmlEncode(CurrentUserName)
            litUserId.Text = CurrentUserId.ToString()
            litProfileId.Text = CurrentProfileId.ToString()

            Dim perms = SessionHelper.Permissions
            litPermissionCount.Text = perms.Count.ToString()

            gvPermissions.DataSource = perms
            gvPermissions.DataBind()
        End Sub

    End Class

End Namespace