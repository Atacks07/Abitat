Imports System.Web.UI

Public Class Global_asax
    Inherits HttpApplication
    Sub Application_Start(sender As Object, e As EventArgs)
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", New ScriptResourceDefinition With {
        .Path = "~/Scripts/jquery-3.7.1.min.js",
        .DebugPath = "~/Scripts/jquery-3.7.1.js",
        .CdnPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.7.1.min.js"
    })
    End Sub
End Class