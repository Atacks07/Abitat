Imports Abitat.Business.Services.Security

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If SessionHelper.IsAuthenticated Then
            litUserName.Text = Server.HtmlEncode(SessionHelper.UserName)
        End If

        ' Prevent caching of authenticated pages globally
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
    End Sub

    Protected Sub btnSignOut_Click(sender As Object, e As EventArgs)
        SessionHelper.SignOut()
        Response.Redirect("~/Pages/Security/Login.aspx", True)
    End Sub

End Class