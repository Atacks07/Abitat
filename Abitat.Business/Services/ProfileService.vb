Imports System.Text.RegularExpressions
Imports Abitat.Data
Imports Abitat.Entities

Namespace Abitat.Business

    Public Class ProfileService

        Private ReadOnly _repository As ProfileRepository

        Public Sub New()
            _repository = New ProfileRepository()
        End Sub

        Public Function GetAll(Optional ByVal search As String = Nothing,
                           Optional ByVal generalStatusId As Integer? = Nothing) As List(Of Profile)
            Return _repository.GetAll(search, generalStatusId)
        End Function

        Public Function GetById(ByVal id As Integer) As Profile
            If id <= 0 Then
                Throw New ArgumentException("Id must be a positive integer.", NameOf(id))
            End If
            Return _repository.GetById(id)
        End Function

        Public Function Create(ByVal profile As Profile) As Integer
            Validate(profile)
            Return _repository.Insert(profile)
        End Function

        Public Sub Update(ByVal profile As Profile)
            If profile Is Nothing Then Throw New ArgumentNullException(NameOf(profile))
            If profile.Id <= 0 Then Throw New ArgumentException("Id is required for update.", NameOf(profile))
            Validate(profile)
            _repository.Update(profile)
        End Sub

        Public Sub ChangeStatus(ByVal id As Integer, ByVal newStatusId As Integer)
            Dim current As Profile = _repository.GetById(id)
            If current Is Nothing Then
                Throw New InvalidOperationException("Profile not found.")
            End If
            current.GeneralStatusId = newStatusId
            _repository.Update(current)
        End Sub

        Private Shared ReadOnly CodePattern As New Regex("^[A-Z0-9_]+$", RegexOptions.Compiled)

        Private Shared Sub Validate(ByVal p As Profile)
            If p Is Nothing Then
                Throw New ArgumentNullException(NameOf(p))
            End If

            If String.IsNullOrWhiteSpace(p.Name) Then
                Throw New ArgumentException("Name is required.")
            End If
            p.Name = p.Name.Trim()
            If p.Name.Length > 100 Then
                Throw New ArgumentException("Name cannot exceed 50 characters.")
            End If

            If p.GeneralStatusId <= 0 Then
                Throw New ArgumentException("Status is required.")
            End If
        End Sub

    End Class

End Namespace
