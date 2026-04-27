Imports System.Text.RegularExpressions
Imports Abitat.Business.Results
Imports Abitat.Data
Imports Abitat.Entities

Namespace Abitat.Business
    Public Class UserService

        Private ReadOnly _repository As New UserRepository()

        Private Shared ReadOnly UserNamePattern As New Regex("^[a-zA-Z0-9._-]{3,50}$", RegexOptions.Compiled)

        Private Const BCryptWorkFactor As Integer = 12

        Public Function GetAll(Optional search As String = Nothing,
                               Optional generalStatusId As Integer? = Nothing,
                               Optional profileId As Integer? = Nothing) As List(Of User)
            Return _repository.GetAll(search, generalStatusId, profileId)
        End Function

        Public Function GetById(id As Integer) As User
            If id <= 0 Then
                Throw New ArgumentException("Invalid user Id.")
            End If
            Return _repository.GetById(id)
        End Function

        Public Function GetPermissionsByUserId(id As Integer) As User
            If id <= 0 Then
                Throw New ArgumentException("Invalid user Id.")
            End If
            Return _repository.GetById(id)
        End Function

        Public Function Create(user As User) As Integer
            Validate(user, isCreate:=True)

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCryptWorkFactor)

            Return _repository.Insert(user)
        End Function

        Public Sub Update(user As User)
            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If
            If user.Id <= 0 Then
                Throw New ArgumentException("Invalid user Id.")
            End If

            Validate(user, isCreate:=False)

            If Not String.IsNullOrWhiteSpace(user.Password) Then
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCryptWorkFactor)
            End If

            _repository.Update(user)
        End Sub

        Public Sub ChangeStatus(ByVal id As Integer, ByVal newStatusId As Integer)
            Dim current As User = _repository.GetById(id)
            If current Is Nothing Then
                Throw New InvalidOperationException("Permission not found.")
            End If
            current.GeneralStatusId = newStatusId
            _repository.Update(current)
        End Sub

        Private Shared Sub Validate(user As User, isCreate As Boolean)
            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If String.IsNullOrWhiteSpace(user.UserName) Then
                Throw New ArgumentException("Username is required.")
            End If
            user.UserName = user.UserName.Trim().ToLowerInvariant()
            If Not UserNamePattern.IsMatch(user.UserName) Then
                Throw New ArgumentException(
                    "Username must be 3-50 characters (letters, numbers, dot, dash, underscore).")
            End If

            If isCreate AndAlso String.IsNullOrWhiteSpace(user.Password) Then
                Throw New ArgumentException("Password is required.")
            End If

            If Not String.IsNullOrWhiteSpace(user.Password) Then
                If user.Password.Length < 8 Then
                    Throw New ArgumentException("Password must be at least 8 characters.")
                End If
            End If

            If user.ProfileId <= 0 Then
                Throw New ArgumentException("Profile is required.")
            End If

            If user.GeneralStatusId <= 0 Then
                Throw New ArgumentException("Status is required.")
            End If
        End Sub

        Public Function ValidateLogin(ByVal userName As String,
                                      ByVal plainPassword As String) As LoginResult

            If String.IsNullOrWhiteSpace(userName) OrElse
               String.IsNullOrWhiteSpace(plainPassword) Then
                Return New LoginResult With {
                    .Success = False,
                    .Message = "Usuario y contraseña son obligatorios."
                }
            End If

            Dim user As User = _repository.GetByUsername(userName.Trim())

            If user Is Nothing Then
                Return New LoginResult With {
                    .Success = False,
                    .Message = "Usuario o contraseña incorrectos."
                }
            End If

            If Not PasswordHasher.Verify(plainPassword, user.Password) Then
                Return New LoginResult With {
                    .Success = False,
                    .Message = "Usuario o contraseña incorrectos."
                }
            End If

            user.Permissions = _repository.GetPermissionsByUserId(user.Id)

            user.Password = Nothing

            Return New LoginResult With {
                .Success = True,
                .Message = "Login correcto.",
                .AuthenticatedUser = user
            }
        End Function

    End Class

End Namespace