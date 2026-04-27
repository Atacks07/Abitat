Imports System.Data
Imports System.Data.SqlClient
Imports Abitat.Entities
Public Class UserRepository

    Public Function GetAll(Optional ByVal search As String = Nothing,
                           Optional ByVal generalStatusId As Integer? = Nothing,
                           Optional ByVal profileId As Integer? = Nothing) As List(Of User)
        Dim list As New List(Of User)()

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_User_GetAll", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Search", SqlDbType.VarChar, 100).Value =
                    If(String.IsNullOrWhiteSpace(search), CType(DBNull.Value, Object), search)
                cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value =
                    If(generalStatusId.HasValue, CType(generalStatusId.Value, Object), DBNull.Value)
                cmd.Parameters.Add("@ProfileId", SqlDbType.Int).Value =
                    If(profileId.HasValue, CType(profileId.Value, Object), DBNull.Value)

                conn.Open()
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        list.Add(MapUser(rd))
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function

    Public Function GetByUsername(ByVal userName As String) As User
        Using conn As SqlConnection = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_User_GetByUsername", conn)
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
                                .GeneralStatus = New GeneralStatus With {
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

    Public Function GetPermissionsByUserId(ByVal userId As Integer) As List(Of Permission)
        Dim list As New List(Of Permission)()

        Using conn As SqlConnection = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_User_GetPermissionsByUserId", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId

                conn.Open()
                Using rd As SqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        list.Add(MapPermission(rd))
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function

    Public Function GetById(ByVal id As Integer) As User
        Using conn As SqlConnection = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_User_GetById", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return New User With {
                                .Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                .UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                .ProfileId = reader.GetInt32(reader.GetOrdinal("ProfileId")),
                                .GeneralStatusId = reader.GetInt32(reader.GetOrdinal("GeneralStatusId")),
                                .Profile = New Profile With {
                                    .Id = reader.GetInt32(reader.GetOrdinal("ProfileId")),
                                    .Name = reader.GetString(reader.GetOrdinal("ProfileName"))
                                },
                                .GeneralStatus = New GeneralStatus With {
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

    Public Sub Update(ByVal user As User)
        Using conn As SqlConnection = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_User_Update", conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = user.UserName

                Dim pwdParam = cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255)
                If String.IsNullOrWhiteSpace(user.Password) Then
                    pwdParam.Value = DBNull.Value
                Else
                    pwdParam.Value = user.Password
                End If

                cmd.Parameters.Add("@ProfileId", SqlDbType.Int).Value = user.ProfileId
                cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value = user.GeneralStatusId

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function MapUser(ByVal rd As IDataReader) As User
        Return New User With {
            .Id = CInt(rd("Id")),
            .UserName = CStr(rd("UserName")),
            .GeneralStatusId = CInt(rd("GeneralStatusId")),
            .ProfileId = CInt(rd("ProfileId")),
            .GeneralStatus = New GeneralStatus With {
                .Id = CInt(rd("GeneralStatusId")),
                .Name = CStr(rd("StatusName"))
            },
            .Profile = New Profile With {
                .Id = CInt(rd("ProfileId")),
                .Name = CStr(rd("ProfileName"))
            }
        }
    End Function

    Private Shared Function MapPermission(ByVal rd As IDataReader) As Permission
        Return New Permission With {
            .Id = CInt(rd("Id")),
            .Code = CStr(rd("Code")),
            .Name = CStr(rd("Name")),
            .GeneralStatusId = CInt(rd("GeneralStatusId")),
            .GeneralStatus = New GeneralStatus With {
                .Id = CInt(rd("GeneralStatusId")),
                .Name = CStr(rd("StatusName"))
            }
        }
    End Function

End Class