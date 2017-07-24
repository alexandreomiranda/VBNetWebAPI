Imports System.Web.Http
Imports ApplicationService
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.OAuth
Imports Owin

Public Class Startup
    Dim authenticateUser As New UserAppService()
    Public Sub Configuration(app As IAppBuilder)
        Dim config As New HttpConfiguration()

        With config
            .MapHttpAttributeRoutes()
            .Routes.MapHttpRoute(
                name:="DefaultApi",
                routeTemplate:="api/{controller}/{id}",
                defaults:=New With {.id = RouteParameter.Optional}
            )
        End With

        Dim formatters = config.Formatters
        formatters.Remove(formatters.XmlFormatter)

        ConfigureOAuth(app, authenticateUser)
        app.UseCors(Cors.CorsOptions.AllowAll)
        app.UseWebApi(config)
    End Sub

    Public Sub ConfigureOAuth(app As IAppBuilder, authenticateUser As UserAppService)

        Dim OAuthServerOptions As New OAuthAuthorizationServerOptions()
        With OAuthServerOptions
            .AllowInsecureHttp = True
            .TokenEndpointPath = New PathString("/api/security/token")
            .AccessTokenExpireTimeSpan = TimeSpan.FromDays(3)
            .Provider = New MyAuthorizationServerProvider(authenticateUser)
        End With

        app.UseOAuthAuthorizationServer(OAuthServerOptions)
        app.UseOAuthBearerAuthentication(New OAuthBearerAuthenticationOptions())

    End Sub
End Class
