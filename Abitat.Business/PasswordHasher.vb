Imports BC = BCrypt.Net.BCrypt

Namespace Abitat.Business
    Public Class PasswordHasher

        Private Const WORK_FACTOR As Integer = 12

        Public Shared Function Hash(ByVal plainPassword As String) As String
            If String.IsNullOrEmpty(plainPassword) Then
                Throw New ArgumentException("The password cannot be empty.", NameOf(plainPassword))
            End If

            Return BC.HashPassword(plainPassword, WORK_FACTOR)
        End Function

        Public Shared Function Verify(ByVal plainPassword As String,
                                      ByVal storedHash As String) As Boolean
            If String.IsNullOrEmpty(plainPassword) OrElse String.IsNullOrEmpty(storedHash) Then
                Return False
            End If

            Try
                Return BC.Verify(plainPassword, storedHash)
            Catch

                Return False
            End Try
        End Function

    End Class

End Namespace