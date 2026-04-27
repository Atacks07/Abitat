Imports System.Text.RegularExpressions
Imports Abitat.Data
Imports Abitat.Entities

Namespace Abitat.Business

    Public Class PermissionService

        Private ReadOnly _repository As PermissionRepository

        Public Sub New()
            _repository = New PermissionRepository()
        End Sub

        Public Function GetAll(Optional ByVal search As String = Nothing,
                           Optional ByVal generalStatusId As Integer? = Nothing) As List(Of Permission)
            Return _repository.GetAll(search, generalStatusId)
        End Function

        Public Function GetById(ByVal id As Integer) As Permission
            If id <= 0 Then
                Throw New ArgumentException("Id must be a positive integer.", NameOf(id))
            End If
            Return _repository.GetById(id)
        End Function

        Public Function Create(ByVal permission As Permission) As Integer
            Validate(permission)
            Return _repository.Insert(permission)
        End Function

        Public Sub Update(ByVal permission As Permission)
            If permission Is Nothing Then Throw New ArgumentNullException(NameOf(permission))
            If permission.Id <= 0 Then Throw New ArgumentException("Id is required for update.", NameOf(permission))
            Validate(permission)
            _repository.Update(permission)
        End Sub

        Public Sub ChangeStatus(ByVal id As Integer, ByVal newStatusId As Integer)
            Dim current As Permission = _repository.GetById(id)
            If current Is Nothing Then
                Throw New InvalidOperationException("Permission not found.")
            End If
            current.GeneralStatusId = newStatusId
            _repository.Update(current)
        End Sub

        Private Shared ReadOnly CodePattern As New Regex("^[A-Z0-9_]+$", RegexOptions.Compiled)

        Private Shared Sub Validate(ByVal p As Permission)
            If p Is Nothing Then
                Throw New ArgumentNullException(NameOf(p))
            End If

            If String.IsNullOrWhiteSpace(p.Code) Then
                Throw New ArgumentException("Code is required.")
            End If
            p.Code = p.Code.Trim().ToUpperInvariant()
            If p.Code.Length > 30 Then
                Throw New ArgumentException("Code cannot exceed 30 characters.")
            End If
            If Not CodePattern.IsMatch(p.Code) Then
                Throw New ArgumentException("Code can only contain uppercase letters, numbers, and underscores.")
            End If

            If String.IsNullOrWhiteSpace(p.Name) Then
                Throw New ArgumentException("Name is required.")
            End If
            p.Name = p.Name.Trim()
            If p.Name.Length > 100 Then
                Throw New ArgumentException("Name cannot exceed 100 characters.")
            End If

            If p.GeneralStatusId <= 0 Then
                Throw New ArgumentException("Status is required.")
            End If
        End Sub

    End Class

End Namespace
