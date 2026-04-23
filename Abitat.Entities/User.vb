Namespace Abitat.Entities

    Public Class User
        Public Property Id As Integer
        Public Property UserName As String
        Public Property Password As String
        Public Property ProfileId As Integer
        Public Property GeneralStatusId As Integer
        Public Property Profile As Profile
        Public Property Status As GeneralStatus
        Public Property Permissions As List(Of Permission)
    End Class

End Namespace