Imports System.Data
Imports System.Data.SqlClient
Imports Abitat.Entities

Public Class AgreementStatusRepository

    Public Function GetAll(Optional ByVal search As String = Nothing,
                           Optional ByVal generalStatusId As Integer? = Nothing) As List(Of AgreementsStatuses)
        Dim list As New List(Of AgreementsStatuses)()

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_AgreementStatus_GetAll", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Search", SqlDbType.VarChar, 100).Value =
                    If(String.IsNullOrWhiteSpace(search), CType(DBNull.Value, Object), search)
                cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value =
                    If(generalStatusId.HasValue, CType(generalStatusId.Value, Object), DBNull.Value)

                conn.Open()
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        list.Add(MapPermission(rd))
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function

    Public Function GetById(ByVal id As Integer) As AgreementsStatuses
        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_AgreementStatus_GetById", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id

                conn.Open()
                Using rd = cmd.ExecuteReader(CommandBehavior.SingleRow)
                    If rd.Read() Then
                        Return MapPermission(rd)
                    End If
                End Using
            End Using
        End Using

        Return Nothing
    End Function

    Public Function Insert(ByVal agreementsStatuses As AgreementsStatuses) As Integer
        If agreementsStatuses Is Nothing Then
            Throw New ArgumentNullException(NameOf(agreementsStatuses))
        End If

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_AgreementStatus_Insert", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = agreementsStatuses.Name
                cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value = agreementsStatuses.GeneralStatusId

                Dim pNewId As New SqlParameter("@NewId", SqlDbType.Int) With {
                    .Direction = ParameterDirection.Output
                }
                cmd.Parameters.Add(pNewId)

                conn.Open()
                cmd.ExecuteNonQuery()

                Return CInt(pNewId.Value)
            End Using
        End Using
    End Function

    Public Sub Update(ByVal agreementsStatuses As AgreementsStatuses)
        If agreementsStatuses Is Nothing Then
            Throw New ArgumentNullException(NameOf(Permission))
        End If

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_AgreementStatus_Update", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = agreementsStatuses.Id
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = agreementsStatuses.Name
                cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value = agreementsStatuses.GeneralStatusId

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function MapPermission(ByVal rd As IDataReader) As AgreementsStatuses
        Return New AgreementsStatuses With {
            .Id = CInt(rd("Id")),
            .Name = CStr(rd("Name")),
            .GeneralStatusId = CInt(rd("GeneralStatusId")),
            .GeneralStatus = New GeneralStatus With {
                .Id = CInt(rd("GeneralStatusId")),
                .Name = CStr(rd("StatusName"))
            }
        }
    End Function

End Class
