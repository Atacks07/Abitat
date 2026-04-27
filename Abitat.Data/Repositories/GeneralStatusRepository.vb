Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports Abitat.Entities

Public Class GeneralStatusRepository

    Public Function GetAll() As List(Of GeneralStatus)
        Dim list As New List(Of GeneralStatus)()
        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_GeneralStatus_GetAll", conn)
                cmd.CommandType = CommandType.StoredProcedure
                conn.Open()
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        list.Add(New GeneralStatus With {
                                 .Id = CInt(rd("Id")),
                                 .Name = CStr(rd("Name"))
                                 })
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function
End Class
