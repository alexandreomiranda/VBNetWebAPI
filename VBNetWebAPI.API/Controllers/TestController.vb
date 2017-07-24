Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports ApplicationService

Namespace Controllers
    Public Class TestController
        Inherits ApiController
        Private _userAppService As UserAppService

        <HttpGet>
        <Route("api/test/")>
        Public Function GetTest() As HttpResponseMessage
            Return Request.CreateResponse(HttpStatusCode.OK, "API is running")
        End Function

    End Class
End Namespace