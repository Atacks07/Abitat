Imports System.Web
Imports Abitat.Entities

Namespace Services.Security

    Public NotInheritable Class SessionHelper

        Private Const KEY_USER_ID As String = "UserId"
        Private Const KEY_USER_NAME As String = "UserName"
        Private Const KEY_PROFILE_ID As String = "ProfileId"
        Private Const KEY_PERMISSIONS As String = "Permissions"

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property IsAuthenticated As Boolean
            Get
                Return HttpContext.Current IsNot Nothing AndAlso
                       HttpContext.Current.Session IsNot Nothing AndAlso
                       HttpContext.Current.Session(KEY_USER_ID) IsNot Nothing
            End Get
        End Property

        Public Shared ReadOnly Property UserId As Integer
            Get
                If Not IsAuthenticated Then Return 0
                Return CInt(HttpContext.Current.Session(KEY_USER_ID))
            End Get
        End Property

        Public Shared ReadOnly Property UserName As String
            Get
                If Not IsAuthenticated Then Return String.Empty
                Return CStr(HttpContext.Current.Session(KEY_USER_NAME))
            End Get
        End Property

        Public Shared ReadOnly Property ProfileId As Integer
            Get
                If Not IsAuthenticated Then Return 0
                Return CInt(HttpContext.Current.Session(KEY_PROFILE_ID))
            End Get
        End Property

        Public Shared ReadOnly Property Permissions As List(Of Permission)
            Get
                If Not IsAuthenticated Then Return New List(Of Permission)()
                Dim obj As Object = HttpContext.Current.Session(KEY_PERMISSIONS)
                If obj Is Nothing Then Return New List(Of Permission)()
                Return CType(obj, List(Of Permission))
            End Get
        End Property

        Public Shared Function HasPermission(ByVal code As String) As Boolean
            If String.IsNullOrWhiteSpace(code) Then Return False
            Return Permissions.Any(Function(p) String.Equals(p.Code, code, StringComparison.OrdinalIgnoreCase))
        End Function

        Public Shared Sub SignOut()
            If HttpContext.Current Is Nothing OrElse HttpContext.Current.Session Is Nothing Then Return
            HttpContext.Current.Session.Clear()
            HttpContext.Current.Session.Abandon()
        End Sub

    End Class

End Namespace