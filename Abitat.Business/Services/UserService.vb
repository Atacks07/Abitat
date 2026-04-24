Imports Abitat.Data
Imports Abitat.Entities

Public Class LoginResult
    Public Property Success As Boolean
    Public Property Message As String
    Public Property AuthenticatedUser As User
End Class

Public Class UserService

    Private ReadOnly _repository As UserRepository

    Public Sub New()
        _repository = New UserRepository()
    End Sub

    Public Function ValidateLogin(ByVal userName As String, ByVal password As String) As LoginResult
        Dim result As New LoginResult()

        If String.IsNullOrWhiteSpace(userName) OrElse String.IsNullOrWhiteSpace(password) Then
            result.Success = False
            result.Message = "Username and password are required."
            Return result
        End If

        Dim repo As New UserRepository()
        Dim user As User = repo.GetUser(userName)

        If user Is Nothing Then
            result.Success = False
            result.Message = "Invalid credentials."
            Return result
        End If

        If Not PasswordHasher.Verify(password, user.Password) Then
            result.Success = False
            result.Message = "Invalid credentials."
            Return result
        End If

        user.Permissions = repo.GetPermissionsByUser(user.Id)

        result.Success = True
        result.AuthenticatedUser = user
        Return result
    End Function

    Public Function CreateUser(ByVal userName As String,
                               ByVal plainPassword As String,
                               ByVal profileId As Integer,
                               ByVal generalStatusId As Integer) As Integer

        If String.IsNullOrWhiteSpace(userName) Then
            Throw New ArgumentException("El nombre de usuario es obligatorio.")
        End If
        If String.IsNullOrWhiteSpace(plainPassword) OrElse plainPassword.Length < 8 Then
            Throw New ArgumentException("La contraseña debe tener al menos 8 caracteres.")
        End If

        Dim newUser As New User With {
            .UserName = userName.Trim(),
            .Password = PasswordHasher.Hash(plainPassword),
            .ProfileId = profileId,
            .GeneralStatusId = generalStatusId
        }

        Return _repository.Insert(newUser)
    End Function

End Class
