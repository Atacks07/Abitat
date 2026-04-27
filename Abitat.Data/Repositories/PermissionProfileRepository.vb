Imports System.Data
Imports System.Data.SqlClient
Imports Abitat.Entities

Public Class PermissionProfileRepository

    Public Function GetByProfileId(ByVal profileId As Integer) As List(Of Permission)
        Dim list As New List(Of Permission)()

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_PermissionProfile_GetByProfileId", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@ProfileId", SqlDbType.Int).Value = profileId

                conn.Open()
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        list.Add(New Permission With {
                            .Id = CInt(rd("Id")),
                            .Code = CStr(rd("Code")),
                            .Name = CStr(rd("Name")),
                            .IsAssigned = CBool(rd("IsAssigned"))
                        })
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function

    Public Sub Insert(ByVal profileId As Integer, ByVal permissionIds As List(Of Integer))
        Dim csv As String = String.Empty
        If permissionIds IsNot Nothing AndAlso permissionIds.Count > 0 Then
            csv = String.Join(",", permissionIds)
        End If

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_PermissionProfile_Insert", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@ProfileId", SqlDbType.Int).Value = profileId
                cmd.Parameters.Add("@PermissionIds", SqlDbType.VarChar).Value =
                    If(String.IsNullOrEmpty(csv), CType(DBNull.Value, Object), csv)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class