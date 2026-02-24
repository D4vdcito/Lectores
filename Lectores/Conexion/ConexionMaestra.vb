Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlData
Imports System.Configuration
Imports System.Data.SqlClient


Module ConexionMaestra
    Public MiConexion As New SqlConnection("Server=LAPTOP-3UIMMVJ2\SQLEXPRESS;Database=bd_crud ;User Id=adan ; Password=admin ;")

    Sub Abrir_Conexion()
        If MiConexion.State = 0 Then
            MiConexion.Open()
        End If
    End Sub

    Sub Cerrar_Conexion()
        If MiConexion.State = 1 Then
            MiConexion.Close()
        End If

    End Sub
End Module
