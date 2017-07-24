Imports System.Security.Claims
Imports System.Threading.Tasks
Imports ApplicationService
Imports Microsoft.Owin.Security.OAuth

Public Class MyAuthorizationServerProvider
    Inherits OAuthAuthorizationServerProvider

    Private _userService As New UserAppService()

    Public Sub New(userService As UserAppService)
        Me._userService = userService
    End Sub

    Public Overrides Async Function ValidateClientAuthentication(context As OAuthValidateClientAuthenticationContext) As Task
        context.Validated()
        Await Task.FromResult(Of Object)(Nothing)
    End Function

    Public Overrides Async Function GrantResourceOwnerCredentials(context As OAuthGrantResourceOwnerCredentialsContext) As Task
        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", New String() {"*"})

        Dim userLogin = _userService.Authenticate(context.UserName, context.Password)
        If userLogin Is Nothing Then
            context.SetError("invalid_user", "Invalid password or username")
            Exit Function
        End If

        Dim identity = New ClaimsIdentity(context.Options.AuthenticationType)

        context.Validated(identity)
        Await Task.FromResult(Of Object)(Nothing)
    End Function

End Class
