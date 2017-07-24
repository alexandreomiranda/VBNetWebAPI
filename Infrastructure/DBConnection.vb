Imports System.Configuration
Imports System.Data.SqlClient

Public Class DBConnection

    Public Shared Function GetConnection() As SqlConnection

        Dim connString = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Dim connBuilder As New SqlConnectionStringBuilder() With {
            .ConnectionString = connString
        }
        Return New SqlConnection(connBuilder.ConnectionString)

    End Function
End Class
