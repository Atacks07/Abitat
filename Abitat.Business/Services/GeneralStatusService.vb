Imports Abitat.Data
Imports Abitat.Entities

Namespace Abitat.Business
    Public Class GeneralStatusService

        Private ReadOnly _repository As GeneralStatusRepository

        Public Sub New()
            _repository = New GeneralStatusRepository()
        End Sub

        Public Function GetAll() As List(Of GeneralStatus)
            Return _repository.GetAll()
        End Function

    End Class

End Namespace
