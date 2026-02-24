Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames

Public Class Form1

    'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Abrir_Conexion()
    '    MsgBox("Conexion iniciada con exito :", vbOKOnly, vbInformation)


    'End Sub 
    'PARA COMPROBAR LA CONEXION 

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()

    End Sub


    Dim ex, ey As Integer
    Dim arrastre As Boolean
    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        ex = e.X
        ey = e.Y
        arrastre = True
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If arrastre Then Me.Location = Me.PointToScreen(New Point(Form1.MousePosition.X - Me.Location.X - ex, Form1.MousePosition.Y - Me.Location.Y - ey))
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnguardar_Click(sender As Object, e As EventArgs)
        Dim cmd As SqlCommand
        If txtnombre.Text <> "" And txtIdentidad.Text <> "" Then
            Try
                Abrir_Conexion()
                cmd = New SqlCommand("Insertar_Lector", MiConexion)
                cmd.CommandType = 4

                cmd.Parameters.AddWithValue("@IdLector", txtIdentidad.Text.ToString)
                cmd.Parameters.AddWithValue("@Nombre", txtnombre.Text.ToString)
                cmd.Parameters.AddWithValue("@Telefono", txttelefono.Text)
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.ToString)
                cmd.Parameters.AddWithValue("@Observaciones", txtdescripcion.Text.ToString)
                cmd.ExecuteNonQuery()
                Cerrar_Conexion()
                paneldatos.Visible = False
                Limpiar()
                mostrar()


            Catch ex As Exception

            End Try
        Else
            MsgBox("Los campos numero de identida, nombre, son obligatorios!", vbInformation, vbOKOnly)
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        paneldatos.Visible = True
        btnguardar.Enabled = True
        btnmodificar.Enabled = False
        txtIdentidad.Focus()
    End Sub

    Private Sub btncancelar_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Limpiar()
        txtdescripcion.Clear()
        txtDireccion.Clear()
        txtIdentidad.Clear()
        txtnombre.Clear()
        txttelefono.Clear()
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        arrastre = False
    End Sub


    Sub mostrar()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter

        Try
            Abrir_Conexion()
            da = New SqlDataAdapter("Mostrar_Lector", MiConexion)
            da.Fill(dt)
            Datalectores.DataSource = dt
            Cerrar_Conexion()

            'tamano de las celdas 

            Datalectores.Columns(0).Width = 35
            Datalectores.Columns(1).Width = 120
            Datalectores.Columns(2).Width = 250
            Datalectores.Columns(3).Width = 100
            Datalectores.Columns(4).Width = 300
            Datalectores.Columns(5).Width = 300


            'apariencia de encabezados 
            Datalectores.EnableHeadersVisualStyles = False
            Dim estilo As DataGridViewCellStyle = New DataGridViewCellStyle()
            estilo.BackColor = Color.White
            estilo.ForeColor = Color.Black
            estilo.Font = New Font("Segoe UI", 10, FontStyle.Regular Or FontStyle.Bold)
            Datalectores.ColumnHeadersDefaultCellStyle = estilo

        Catch ex As Exception

        End Try



    End Sub

    Private Sub btncancelar_Click_1(sender As Object, e As EventArgs) Handles btncancelar.Click
        Limpiar()
        paneldatos.Visible = False
        btnguardar.Enabled = True
        btnmodificar.Enabled = True
        paneldatos.Visible = False
    End Sub

    Sub buscar()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter

        Try
            Abrir_Conexion()
            da = New SqlDataAdapter("Buscar_Lector", MiConexion)
            da.SelectCommand.CommandType = 4
            da.SelectCommand.Parameters.AddWithValue("@Busqueda", txtBuscar.Text)

            da.Fill(dt)
            Datalectores.DataSource = dt
            Cerrar_Conexion()

            'tamano de las celdas 

            Datalectores.Columns(0).Width = 35
            Datalectores.Columns(1).Width = 120
            Datalectores.Columns(2).Width = 250
            Datalectores.Columns(3).Width = 100
            Datalectores.Columns(4).Width = 300
            Datalectores.Columns(5).Width = 300


            'apariencia de encabezados 
            Datalectores.EnableHeadersVisualStyles = False
            Dim estilo As DataGridViewCellStyle = New DataGridViewCellStyle()
            estilo.BackColor = Color.White
            estilo.ForeColor = Color.Black
            estilo.Font = New Font("Segoe UI", 10, FontStyle.Regular Or FontStyle.Bold)
            Datalectores.ColumnHeadersDefaultCellStyle = estilo



        Catch ex As Exception

        End Try


    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        buscar()
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        mostrar()
    End Sub

    Private Sub Datalectores_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Datalectores.CellContentDoubleClick
        paneldatos.Visible = True
        Try
            txtIdentidad.Text = Datalectores.SelectedCells.Item(1).Value
            txtnombre.Text = Datalectores.SelectedCells.Item(2).Value
            txttelefono.Text = Datalectores.SelectedCells.Item(3).Value
            txtDireccion.Text = Datalectores.SelectedCells.Item(4).Value
            txtdescripcion.Text = Datalectores.SelectedCells.Item(5).Value

            btnguardar.Enabled = False
            btnmodificar.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub paneldatos_Paint(sender As Object, e As PaintEventArgs) Handles paneldatos.Paint

    End Sub

    Private Sub btnguardar_Click_1(sender As Object, e As EventArgs) Handles btnguardar.Click
        Dim cmd As SqlCommand
        If txtnombre.Text <> "" And txtIdentidad.Text <> "" Then
            Try
                Abrir_Conexion()
                cmd = New SqlCommand("Insertar_Lector", MiConexion)
                cmd.CommandType = 4

                cmd.Parameters.AddWithValue("@IdLector", txtIdentidad.Text.ToString)
                cmd.Parameters.AddWithValue("@Nombre", txtnombre.Text.ToString)
                cmd.Parameters.AddWithValue("@Telefono", txttelefono.Text)
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.ToString)
                cmd.Parameters.AddWithValue("@Observaciones", txtdescripcion.Text.ToString)
                cmd.ExecuteNonQuery()
                Cerrar_Conexion()
                paneldatos.Visible = False
                Limpiar()
                mostrar()


            Catch ex As Exception

            End Try
        Else
            MsgBox("Los campos numero de identida, nombre, son obligatorios!", vbInformation, vbOKOnly)
        End If
    End Sub

    Private Sub Datalectores_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Datalectores.CellContentClick
        'verificar el click en la columna eliminar
        If e.ColumnIndex = Datalectores.Columns.Item("eliminar").Index Then
            Dim result As DialogResult
            result = MsgBox("¿Está seguro que quiere eliminar este registro de forma permanente?",
                MsgBoxStyle.Question Or MsgBoxStyle.OkCancel,
                "Confirmación")
            If result = DialogResult.OK Then
                Dim cmd As SqlCommand
                Try
                    Abrir_Conexion()
                    cmd = New SqlCommand("Eliminar_Lector", MiConexion)
                    cmd.CommandType = 4
                    cmd.Parameters.AddWithValue("@idLector", Datalectores.SelectedCells.Item(1).Value)
                    cmd.ExecuteNonQuery()
                    Cerrar_Conexion()
                    mostrar()

                Catch ex As Exception

                End Try

            Else
                MsgBox("Eliminacion cancelada,el registro no se eliminará", vbInformation + vbOKOnly, "Sistema Lectores")
            End If

        End If
    End Sub
End Class
