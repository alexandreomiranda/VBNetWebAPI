Imports Domain
Imports Infrastructure

Public Class UserAppService
    Private _userRepository As New UserRepository()

    Public Function Authenticate(username As String, password As String) As User
        password = StringHelper.EncryptPassword(password)
        Return _userRepository.Authenticate(username, password)
    End Function
End Class
