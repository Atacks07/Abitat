Imports Abitat.Business.Abitat.Business
Imports Abitat.Entities
Imports Abitat.Infrastructure

Namespace Pages.Administration

    Public Class ProfileEdit
        Inherits AuthenticatedPage

        Private ReadOnly _profileService As New ProfileService()
        Private ReadOnly _statusService As New GeneralStatusService()

        Private Const LIST_URL As String = "~/Pages/Administration/ProfileList.aspx"

        Protected Overrides ReadOnly Property RequiredPermission As String
            Get
                Return "PROFILES_MANAGE"
            End Get
        End Property

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                LoadStatusDropDown()
                InitializeForm()
            End If
        End Sub

        Private Sub LoadStatusDropDown()
            ddlStatus.Items.Clear()
            For Each s As GeneralStatus In _statusService.GetAll()
                ddlStatus.Items.Add(New ListItem(s.Name, s.Id.ToString()))
            Next
        End Sub

        Private Sub InitializeForm()
            Dim idParam As String = Request.QueryString("id")
            Dim id As Integer

            If String.IsNullOrWhiteSpace(idParam) OrElse
               Not Integer.TryParse(idParam, id) OrElse id <= 0 Then
                SetPageTitle("New Profile")
                SelectDefaultStatusByName("Active")
                Return
            End If

            Dim prof As Profile = _profileService.GetById(id)
            If prof Is Nothing Then
                Response.Redirect(LIST_URL, False)
                Return
            End If

            SetPageTitle("Edit Profile")
            hfId.Value = prof.Id.ToString()
            txtName.Text = prof.Name
            SelectStatusById(prof.GeneralStatusId)
        End Sub

        Private Sub SetPageTitle(ByVal title As String)
            litPageTitle.Text = title
            litCardTitle.Text = title
        End Sub

        Private Sub SelectDefaultStatusByName(ByVal name As String)
            Dim item As ListItem = ddlStatus.Items.FindByText(name)
            If item IsNot Nothing Then
                ddlStatus.ClearSelection()
                item.Selected = True
            End If
        End Sub

        Private Sub SelectStatusById(ByVal statusId As Integer)
            Dim item As ListItem = ddlStatus.Items.FindByValue(statusId.ToString())
            If item IsNot Nothing Then
                ddlStatus.ClearSelection()
                item.Selected = True
            End If
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)
            If Not Page.IsValid Then Return

            Try
                Dim prof As New Profile With {
                    .Name = txtName.Text,
                    .GeneralStatusId = CInt(ddlStatus.SelectedValue)
                }

                Dim isEdit As Boolean =
                    Not String.IsNullOrWhiteSpace(hfId.Value) AndAlso CInt(hfId.Value) > 0

                If isEdit Then
                    prof.Id = CInt(hfId.Value)
                    _profileService.Update(prof)
                Else
                    _profileService.Create(prof)
                End If

                Response.Redirect(LIST_URL, False)

            Catch ex As Exception
                ShowMessage(ex.Message, False)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
            Response.Redirect(LIST_URL, False)
        End Sub

        Private Sub ShowMessage(ByVal text As String, ByVal isSuccess As Boolean)
            litMessage.Text = Server.HtmlEncode(text)
            pnlMessage.CssClass = If(isSuccess, "alert alert-success mb-3", "alert alert-danger mb-3")
            pnlMessage.Visible = True
        End Sub

    End Class

End Namespace