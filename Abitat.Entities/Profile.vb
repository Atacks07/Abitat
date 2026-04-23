Public Class Profile
    Public Property Id As Integer
    Public Property Name As String
    Public Property GeneralStatusId As Integer
    Public Property Status As GeneralStatus
    Public Property Permissions As List(Of Permission)
End Class

