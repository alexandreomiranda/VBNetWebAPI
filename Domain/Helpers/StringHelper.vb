Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Text

Public Class StringHelper

    Public Shared Function EncryptPassword(value As String) As String
        If String.IsNullOrEmpty(value) Then
            Return ""
        End If

        value += "|aba4253a-5bc0-4471-af89-50eb55190779"
        Dim md5 As MD5 = MD5.Create()
        Dim data As Byte() = md5.ComputeHash(Encoding.Default.GetBytes(value))
        Dim sbString As New StringBuilder()
        For i As Integer = 0 To data.Length - 1
            sbString.Append(data(i).ToString("x2"))
        Next
        Return sbString.ToString()
    End Function

    Public Shared Function ToTitleCase(text As String) As String
        Return ToTitleCase(text, False)
    End Function

    Public Shared Function ToTitleCase(text As String, keepWhatBeCapital As Boolean) As String
        text = text.Trim()
        If Not keepWhatBeCapital Then
            text = text.ToLower()
        End If

        Dim textInfo = New CultureInfo("pt-BR", False).TextInfo
        Return textInfo.ToTitleCase(text)
    End Function
End Class
