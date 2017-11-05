Imports System.Security.Claims
Imports System.Threading.Tasks
Imports ApplicationService
Imports Microsoft.Owin.Security
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
        identity.AddClaim(New Claim(ClaimTypes.Name, userLogin.Name))

        Dim props = New AuthenticationProperties(New Dictionary(Of String, String)() From {
            {"nome", userLogin.Name},
            {"email", userLogin.Email}
        })

        Dim ticket = New AuthenticationTicket(identity, props)

        context.Validated(ticket)

        Await Task.FromResult(Of Object)(Nothing)
    End Function
    Public Overrides Async Function TokenEndpoint(context As OAuthTokenEndpointContext) As Task
        For Each [dadosUsuario] As KeyValuePair(Of String, String) In context.Properties.Dictionary
            context.AdditionalResponseParameters.Add([dadosUsuario].Key, [dadosUsuario].Value)
        Next
        Await Task.FromResult(Of Object)(Nothing)
    End Function
End Class
