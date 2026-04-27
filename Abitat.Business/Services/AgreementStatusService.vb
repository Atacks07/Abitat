Imports System.Text.RegularExpressions
Imports Abitat.Data
Imports Abitat.Entities

Namespace Abitat.Business

    Public Class AgreementStatusService

        Private ReadOnly _repository As AgreementStatusRepository

        Public Sub New()
            _repository = New AgreementStatusRepository()
        End Sub

        Public Function GetAll(Optional ByVal search As String = Nothing,
                           Optional ByVal generalStatusId As Integer? = Nothing) As List(Of AgreementsStatuses)
            Return _repository.GetAll(search, generalStatusId)
        End Function

        Public Function GetById(ByVal id As Integer) As AgreementsStatuses
            If id <= 0 Then
                Throw New ArgumentException("Id must be a positive integer.", NameOf(id))
            End If
            Return _repository.GetById(id)
        End Function

        Public Function Insert(ByVal agreementsStatuses As AgreementsStatuses) As Integer
            Validate(agreementsStatuses)
            Return _repository.Insert(agreementsStatuses)
        End Function

        Public Sub Update(ByVal agreementsStatuses As AgreementsStatuses)
            If agreementsStatuses Is Nothing Then Throw New ArgumentNullException(NameOf(agreementsStatuses))
            If agreementsStatuses.Id <= 0 Then Throw New ArgumentException("Id is required for update.", NameOf(agreementsStatuses))
            Validate(agreementsStatuses)
            _repository.Update(agreementsStatuses)
        End Sub

        Public Sub ChangeStatus(ByVal id As Integer, ByVal newStatusId As Integer)
            Dim agreementsStatuses As AgreementsStatuses = _repository.GetById(id)
            If agreementsStatuses Is Nothing Then
                Throw New InvalidOperationException("Currency not found.")
            End If
            agreementsStatuses.GeneralStatusId = newStatusId
            _repository.Update(agreementsStatuses)
        End Sub

        Private Shared ReadOnly CodePattern As New Regex("^[A-Z0-9_]+$", RegexOptions.Compiled)

        Private Shared Sub Validate(ByVal agreementsStatuses As AgreementsStatuses)
            If agreementsStatuses Is Nothing Then
                Throw New ArgumentNullException(NameOf(agreementsStatuses))
            End If

            If String.IsNullOrWhiteSpace(agreementsStatuses.Name) Then
                Throw New ArgumentException("Name is required.")
            End If
            agreementsStatuses.Name = agreementsStatuses.Name.Trim()
            If agreementsStatuses.Name.Length > 100 Then
                Throw New ArgumentException("Name cannot exceed 50 characters.")
            End If

            If agreementsStatuses.GeneralStatusId <= 0 Then
                Throw New ArgumentException("Status is required.")
            End If
        End Sub

    End Class

End Namespace
