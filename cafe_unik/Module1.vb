Imports MySql.Data.MySqlClient
Module Module1
    Public Conn As New MySqlConnection
    Private myConnectionString As String

    Sub Koneksi()
        myConnectionString = "server=127.0.0.1;" _
            & "uid=root;" _
            & "pwd=;" _
            & "database=db_cafe"
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        Conn.ConnectionString = myConnectionString

        Try
            Conn.Open()
        Catch ex As MySqlException
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

End Module
