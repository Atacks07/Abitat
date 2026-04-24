Imports System.Configuration
Imports System.Data.SqlClient

Public Class Connection

    Private Const CONNECTION_NAME As String = "AbitatDB"
    Public Shared Function GetConnection() As SqlConnection
        Dim cs As ConnectionStringSettings =
            ConfigurationManager.ConnectionStrings(CONNECTION_NAME)

        If cs Is Nothing OrElse String.IsNullOrWhiteSpace(cs.ConnectionString) Then
            Throw New ConfigurationErrorsException(
                $"The connection string was not found '{CONNECTION_NAME}' in Web.config.")
        End If

        Return New SqlConnection(cs.ConnectionString)
    End Function

End Class