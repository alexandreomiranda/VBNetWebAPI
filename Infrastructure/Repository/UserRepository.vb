Imports Dapper
Imports Domain

Public Class UserRepository
    Public Function Authenticate(username As String, password As String) As User
        'Dim cmd = DBConnection.GetConnection()
        'Dim sql = "SELECT name, email FROM Users where username = @usr and password = @pwd"
        'Return cmd.QueryFirstOrDefault(Of User)(sql, New With {.usr = username, .pwd = password})

        'fake user - no Database
        If username = "admin" And password = StringHelper.EncryptPassword("123456") Then
            Dim userData As New User
            userData.Name = "admin"
            userData.Email = "admin@myemail.com"
            Return userData
        Else
            Return Nothing
        End If

    End Function
End Class
