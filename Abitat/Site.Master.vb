Imports Abitat.Business.Services.Security

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If SessionHelper.IsAuthenticated Then
            litUserName.Text = Server.HtmlEncode(SessionHelper.UserName)
        End If

        HighlightActiveMenu()
        ApplyPermissions()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
    End Sub

    Protected Sub btnSignOut_Click(sender As Object, e As EventArgs)
        SessionHelper.SignOut()
        Response.Redirect("~/Pages/Security/Login.aspx", True)
    End Sub

    Private Sub HighlightActiveMenu()
        Dim currentPath As String = Request.AppRelativeCurrentExecutionFilePath.ToLowerInvariant()

        If currentPath.Contains("/pages/administration/userlist.aspx") OrElse
           currentPath.Contains("/pages/administration/useredit.aspx") Then
            lnkUsers.Attributes("class") = "nav-link active"
        End If

        If currentPath.Contains("/pages/administration/profilelist.aspx") OrElse
           currentPath.Contains("/pages/administration/profileedit.aspx") OrElse
           currentPath.Contains("/pages/administration/permissionprofileassign.aspx") Then
            lnkProfiles.Attributes("class") = "nav-link active"
        End If

        If currentPath.Contains("/pages/administration/permissionlist.aspx") OrElse
           currentPath.Contains("/pages/administration/permissionedit.aspx") Then
            lnkPermissions.Attributes("class") = "nav-link active"
        End If
    End Sub

    Private Sub ApplyPermissions()

        If Not SessionHelper.IsAuthenticated Then
            phUsersLink.Visible = False
            phProfilesLink.Visible = False
            phPermissionsLink.Visible = False
            Return
        End If

        phUsersLink.Visible = SessionHelper.HasPermission("USERS_VIEW")
        phProfilesLink.Visible = SessionHelper.HasPermission("PROFILES_VIEW")
        phPermissionsLink.Visible = SessionHelper.HasPermission("PERMISSIONS_VIEW")

        phAdminSection.Visible = phUsersLink.Visible OrElse
                         phProfilesLink.Visible OrElse
                         phPermissionsLink.Visible


    End Sub

End Class