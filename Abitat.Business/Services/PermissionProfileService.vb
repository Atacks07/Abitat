Imports System.Text.RegularExpressions
Imports Abitat.Data
Imports Abitat.Entities

Namespace Abitat.Business

    Public Class PermissionProfileService

        Private ReadOnly _repository As New PermissionProfileRepository()

        Public Function GetByProfileId(ByVal id As Integer) As List(Of Permission)
            If id <= 0 Then
                Throw New ArgumentException("Id must be a positive integer.", NameOf(id))
            End If
            Return _repository.GetByProfileId(id)
        End Function

        Public Sub Save(ByVal profileId As Integer,
                        ByVal permissionIds As List(Of Integer))

            If profileId <= 0 Then
                Throw New ArgumentException("Invalid profile Id.")
            End If

            Dim cleanIds As List(Of Integer) = Nothing
            If permissionIds IsNot Nothing Then
                cleanIds = permissionIds _
                    .Where(Function(id) id > 0) _
                    .Distinct() _
                    .ToList()
            End If

            _repository.Insert(profileId, cleanIds)
        End Sub

    End Class

End Namespace
