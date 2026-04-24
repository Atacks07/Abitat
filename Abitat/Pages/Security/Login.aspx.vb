Imports Abitat.Business
Imports Abitat.Data
Imports Abitat.Entities

Namespace Pages.Security

    Public Class Login
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                If Session("UserId") IsNot Nothing Then
                    Response.Redirect("~/Pages/Home/Dashboard.aspx", False)
                End If
            End If
        End Sub

        Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
            lblErr.Visible = False

            Dim userName As String = txtUserName.Text.Trim()
            Dim password As String = txtPassword.Text

            If String.IsNullOrWhiteSpace(userName) OrElse String.IsNullOrWhiteSpace(password) Then
                ShowError("Please enter username and password.")
                Return
            End If

            Try
                Dim svc As New UserService()
                Dim result As LoginResult = svc.ValidateLogin(userName, password)

                If Not result.Success Then
                    ShowError("Invalid username or password.")
                    Return
                End If

                Session("UserId") = result.AuthenticatedUser.Id
                Session("UserName") = result.AuthenticatedUser.UserName
                Session("ProfileId") = result.AuthenticatedUser.ProfileId
                Session("Permissions") = result.AuthenticatedUser.Permissions

                Response.Redirect("~/Pages/Home/Dashboard.aspx", False)

            Catch ex As Exception
                ShowError("An unexpected error occurred. Please try again.")
            End Try
        End Sub

        Private Sub ShowError(msg As String)
            lblErr.Text = msg
            lblErr.Visible = True
            txtPassword.Text = String.Empty
        End Sub

    End Class

End Namespace