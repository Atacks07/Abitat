Imports System.Data
Imports System.Data.SqlClient
Imports Abitat.Entities
Public Class UserRepository

        Public Function GetUser(ByVal userName As String) As User
            Using conn As SqlConnection = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_User_GetUser", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return New User With {
                                .Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                .UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                .Password = reader.GetString(reader.GetOrdinal("Password")),
                                .ProfileId = reader.GetInt32(reader.GetOrdinal("ProfileId")),
                                .GeneralStatusId = reader.GetInt32(reader.GetOrdinal("GeneralStatusId")),
                                .Profile = New Profile With {
                                    .Id = reader.GetInt32(reader.GetOrdinal("ProfileId")),
                                    .Name = reader.GetString(reader.GetOrdinal("ProfileName"))
                                },
                                .Status = New GeneralStatus With {
                                    .Id = reader.GetInt32(reader.GetOrdinal("GeneralStatusId")),
                                    .Name = reader.GetString(reader.GetOrdinal("StatusName"))
                                }
                            }
                    End If
                End Using
            End Using
        End Using

            Return Nothing
        End Function

        Public Function GetPermissionsByUser(ByVal userId As Integer) As List(Of Permission)
            Dim list As New List(Of Permission)()

            Using conn As SqlConnection = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_User_GetPermissionsByUser", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New Permission With {
                                .Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                .Code = If(reader.IsDBNull(reader.GetOrdinal("Code")),
                                           Nothing,
                                           reader.GetString(reader.GetOrdinal("Code"))),
                                .Name = reader.GetString(reader.GetOrdinal("Name")),
                                .GeneralStatusId = reader.GetInt32(reader.GetOrdinal("GeneralStatusId"))
                            })
                    End While
                End Using
            End Using
        End Using

            Return list
        End Function

        Public Function Insert(ByVal user As User) As Integer
            Using conn As SqlConnection = Connection.GetConnection()
                Using cmd As New SqlCommand("dbo.sp_User_Insert", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = user.UserName
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = user.Password
                    cmd.Parameters.Add("@ProfileId", SqlDbType.Int).Value = user.ProfileId
                    cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value = user.GeneralStatusId

                    Dim outId As New SqlParameter("@NewId", SqlDbType.Int) With {
                        .Direction = ParameterDirection.Output
                    }
                    cmd.Parameters.Add(outId)

                    conn.Open()
                    cmd.ExecuteNonQuery()

                    Return Convert.ToInt32(outId.Value)
                End Using
            End Using
        End Function

End Class