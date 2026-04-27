Imports Abitat.Business.Services.Security

Namespace Infrastructure

    Public Class AuthenticatedPage
        Inherits System.Web.UI.Page

        Protected Overridable ReadOnly Property RequiredPermission As String
            Get
                Return Nothing
            End Get
        End Property

        Protected Overrides Sub OnPreInit(e As EventArgs)
            MyBase.OnPreInit(e)

            If Not SessionHelper.IsAuthenticated Then
                Response.Redirect("~/Pages/Security/Login.aspx", True)
                Return
            End If

            If Not String.IsNullOrEmpty(RequiredPermission) AndAlso
               Not SessionHelper.HasPermission(RequiredPermission) Then
                Response.Redirect("~/Pages/Security/AccessDenied.aspx", True)
                Return
            End If
        End Sub

        Protected ReadOnly Property CurrentUserId As Integer
            Get
                Return SessionHelper.UserId
            End Get
        End Property

        Protected ReadOnly Property CurrentUserName As String
            Get
                Return SessionHelper.UserName
            End Get
        End Property

        Protected ReadOnly Property CurrentProfileId As Integer
            Get
                Return SessionHelper.ProfileId
            End Get
        End Property

        Protected Function HasPermission(ByVal code As String) As Boolean
            Return SessionHelper.HasPermission(code)
        End Function

    End Class

End Namespace