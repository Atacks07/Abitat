Imports System.Text.RegularExpressions
Imports Abitat.Data
Imports Abitat.Entities

Namespace Abitat.Business

    Public Class CurrencyService

        Private ReadOnly _repository As CurrencyRepository

        Public Sub New()
            _repository = New CurrencyRepository()
        End Sub

        Public Function GetAll(Optional ByVal search As String = Nothing,
                           Optional ByVal generalStatusId As Integer? = Nothing) As List(Of Currency)
            Return _repository.GetAll(search, generalStatusId)
        End Function

        Public Function GetById(ByVal id As Integer) As Currency
            If id <= 0 Then
                Throw New ArgumentException("Id must be a positive integer.", NameOf(id))
            End If
            Return _repository.GetById(id)
        End Function

        Public Function Create(ByVal currency As Currency) As Integer
            Validate(currency)
            Return _repository.Insert(currency)
        End Function

        Public Sub Update(ByVal currency As Currency)
            If currency Is Nothing Then Throw New ArgumentNullException(NameOf(currency))
            If currency.Id <= 0 Then Throw New ArgumentException("Id is required for update.", NameOf(currency))
            Validate(currency)
            _repository.Update(currency)
        End Sub

        Public Sub ChangeStatus(ByVal id As Integer, ByVal newStatusId As Integer)
            Dim current As Currency = _repository.GetById(id)
            If current Is Nothing Then
                Throw New InvalidOperationException("Currency not found.")
            End If
            current.GeneralStatusId = newStatusId
            _repository.Update(current)
        End Sub

        Private Shared ReadOnly CodePattern As New Regex("^[A-Z0-9_]+$", RegexOptions.Compiled)

        Private Shared Sub Validate(ByVal currency As Currency)
            If currency Is Nothing Then
                Throw New ArgumentNullException(NameOf(currency))
            End If

            If String.IsNullOrWhiteSpace(currency.Name) Then
                Throw New ArgumentException("Name is required.")
            End If
            currency.Name = currency.Name.Trim()
            If currency.Name.Length > 100 Then
                Throw New ArgumentException("Name cannot exceed 50 characters.")
            End If

            If currency.GeneralStatusId <= 0 Then
                Throw New ArgumentException("Status is required.")
            End If
        End Sub

    End Class

End Namespace
