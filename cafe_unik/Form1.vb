Imports MySql.Data.MySqlClient

Public Class Form1
    Private Id_Pesanan As Int64
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataView()
    End Sub
    Sub DataView()
        Call Koneksi()
        Dim query As String = "SELECT * FROM pesanan"
        Dim cmd As New MySqlCommand(query, Conn)
        Dim dataTable As New DataTable
        Dim adapter As New MySqlDataAdapter(cmd)

        Try
            adapter.Fill(dataTable)
            DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Error : " + ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim barisTerpilih As DataGridViewRow
        If e.RowIndex >= 0 Then
            barisTerpilih = DataGridView1.Rows(e.RowIndex)
            Id_Pesanan = barisTerpilih.Cells(0).Value
            TextName.Text = barisTerpilih.Cells(1).Value.ToString()
            ComboBoxMenu.Text = barisTerpilih.Cells(2).Value.ToString()
            NumericJumlah.Value = barisTerpilih.Cells(3).Value
        End If

    End Sub
    Sub ClearForm()
        TextName.Text = ""
        ComboBoxMenu.Text = ""
        NumericJumlah.Value = 0
    End Sub

    Private Sub ButtonInput_Click(sender As Object, e As EventArgs) Handles ButtonInput.Click
        Call Koneksi()

        Dim query As String = "INSERT INTO pesanan (nama, pesanan, jumlah) VALUES(@nama, @menu, @jumlah)"
        Dim cmd As New MySqlCommand(query, Conn)
        cmd.Parameters.AddWithValue("@nama", TextName.Text)
        cmd.Parameters.AddWithValue("@menu", ComboBoxMenu.Text)
        cmd.Parameters.AddWithValue("@jumlah", NumericJumlah.Value)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Berhasil Di Simpan")
        Catch ex As Exception
            MessageBox.Show("Error : " + ex.Message)
        Finally
            Conn.Close()
        End Try
        ClearForm()
        DataView()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ClearForm()
    End Sub

    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click
        Call Koneksi()
        Dim query As String = "UPDATE pesanan SET nama=@newNama,
                              pesanan=@newMenu, 
                              jumlah=@newJumlah WHERE id=@id"
        Dim cmd As New MySqlCommand(query, Conn)
        cmd.Parameters.AddWithValue("@id", Id_Pesanan)
        cmd.Parameters.AddWithValue("@newNama", TextName.Text)
        cmd.Parameters.AddWithValue("@newMenu", ComboBoxMenu.Text)
        cmd.Parameters.AddWithValue("@newJumlah", NumericJumlah.Value)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil di edit")
        Catch ex As Exception
            MessageBox.Show("Error : ", ex.Message)
        Finally
            Conn.Close()
        End Try

        DataView()
        ClearForm()
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Call Koneksi()
        Dim query As String = "DELETE FROM pesanan WHERE id=@id_pesanan"
        Dim cmd As New MySqlCommand(query, Conn)
        cmd.Parameters.AddWithValue("@id_pesanan", Id_Pesanan)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil di hapus")
        Catch ex As Exception
            MessageBox.Show("Error : ", ex.Message)
        Finally
            Conn.Close()
        End Try

        ClearForm()
        DataView()
    End Sub
End Class
