Imports System.Data
Imports System.Data.SqlClient
Imports Abitat.Entities

Public Class CurrencyRepository

    Public Function GetAll(Optional ByVal search As String = Nothing,
                           Optional ByVal generalStatusId As Integer? = Nothing) As List(Of Currency)
        Dim list As New List(Of Currency)()

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_Currency_GetAll", conn)
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

    Public Function GetById(ByVal id As Integer) As Currency
        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_Currency_GetById", conn)
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

    Public Function Insert(ByVal currency As Currency) As Integer
        If currency Is Nothing Then
            Throw New ArgumentNullException(NameOf(currency))
        End If

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_Currency_Insert", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = currency.Name
                cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value = currency.GeneralStatusId

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

    Public Sub Update(ByVal currency As Currency)
        If currency Is Nothing Then
            Throw New ArgumentNullException(NameOf(Permission))
        End If

        Using conn = Connection.GetConnection()
            Using cmd As New SqlCommand("dbo.sp_Currency_Update", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = currency.Id
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = currency.Name
                cmd.Parameters.Add("@GeneralStatusId", SqlDbType.Int).Value = currency.GeneralStatusId

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function MapPermission(ByVal rd As IDataReader) As Currency
        Return New Currency With {
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
