Imports Abitat.Business.Abitat.Business
Imports Abitat.Infrastructure

Namespace Pages.Administration

    Public Class GeneralStatusList
        Inherits AuthenticatedPage

        Private ReadOnly _generalStatusService As New GeneralStatusService()

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                BindGrid()
            End If
        End Sub

        Private Sub BindGrid()
            Try
                gvStatus.DataSource = _generalStatusService.GetAll()
                gvStatus.DataBind()
            Catch ex As Exception
                ShowMessage("Error loading data: " & ex.Message, "danger")
            End Try
        End Sub

        Private Sub ShowMessage(message As String, type As String)
            pnlMessage.Visible = True
            pnlMessage.CssClass = "alert alert-" & type & " alert-dismissible fade show"
            litMessage.Text = message &
                "<button type='button' class='btn-close' data-bs-dismiss='alert'></button>"
        End Sub

    End Class

End Namespace