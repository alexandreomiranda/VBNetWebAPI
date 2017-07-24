Imports Dapper
Imports Domain

Public Class UserRepository
    Public Function Authenticate(username As String, password As String) As User
        Dim cmd = DBConnection.GetConnection()
        Dim sql = "SELECT name, email FROM Users where username = @usr and password = @pwd"
        Return cmd.QueryFirstOrDefault(Of User)(sql, New With {.usr = username, .pwd = password})
    End Function
End Class
