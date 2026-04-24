Imports System.Security
Imports Abitat.Business.Services.Security

Namespace Pages.Home

    Public Class Dashboard
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            ' Guard: must be authenticated
            If Not SessionHelper.IsAuthenticated Then
                Response.Redirect("~/Pages/Security/Login.aspx", True)
                Return
            End If

            ' Prevent caching of authenticated pages
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()

            If Not IsPostBack Then
                BindSessionInfo()
            End If
        End Sub

        Private Sub BindSessionInfo()
            litUserName.Text = Server.HtmlEncode(SessionHelper.UserName)
            litUserName2.Text = Server.HtmlEncode(SessionHelper.UserName)
            litUserId.Text = SessionHelper.UserId.ToString()
            litProfileId.Text = SessionHelper.ProfileId.ToString()

            gvPermissions.DataSource = SessionHelper.Permissions
            gvPermissions.DataBind()
        End Sub

        Protected Sub btnSignOut_Click(sender As Object, e As EventArgs)
            SessionHelper.SignOut()
            Response.Redirect("~/Pages/Security/Login.aspx", True)
        End Sub

    End Class

End Namespace