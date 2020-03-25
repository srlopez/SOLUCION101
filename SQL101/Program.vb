Imports System.Data.SqlClient

Module Program
    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private row As String
    Private connectionstring() As String = {
    "Data Source=.\SQLEXPRESS;Initial Catalog=BASE;Persist Security Info=True;User ID=sa;Password=1234",
    "Data Source=localhost;Initial Catalog=BASE;User ID=sa;Password=Pa88word",
        "Data Source=192.168.122.1;Initial Catalog=BASE;User ID=sa;Password=Pa88word"
    }
    Private activeconn As Int16 = 2

    ' docker exec mssql /opt/mssql-tools/bin/sqlcmd  -U SA -P Pa88word -d BASE -Q "SELECT * FROM ARTICULOS" -S localhost

    Sub Main(args As String())
        Console.OutputEncoding = Text.Encoding.UTF8 'Para permitir el €
        Console.WriteLine("Hello SQL-World!!!")
        Console.WriteLine(connectionstring(activeconn))


        'Connection y abrimos
        myConn = New SqlConnection(connectionstring(activeconn))
        myConn.Open()

        'Comando a ejecutar
        myCmd = myConn.CreateCommand

        'Create TABLE
        myCmd.CommandText = "
        DROP TABLE IF EXISTS ARTICULOS
        CREATE TABLE ARTICULOS(
            IDARTICULO INT IDENTITY,
            NOMBRE NVARCHAR(30),
            PRECIO NUMERIC(5,2)
        )"
        myCmd.ExecuteNonQuery()


        'Primero INSERT
        myCmd.CommandText = "INSERT INTO ARTICULOS (NOMBRE, PRECIO) VALUES (@Nombre, @Precio)"
        myCmd.Parameters.AddWithValue("@Nombre", "DESDE VB.NET")
        myCmd.Parameters.AddWithValue("@Precio", 111.99)
        myCmd.ExecuteNonQuery()

        'Segundo UPDATE
        myCmd.CommandText = "UPDATE ARTICULOS SET PRECIO = @UPrecio WHERE NOMBRE LIKE @Casi"
        myCmd.Parameters.AddWithValue("@Casi", "DESDE%")
        myCmd.Parameters.AddWithValue("@UPrecio", 99.11)
        myCmd.ExecuteNonQuery()

        'Tercero SELECT
        myCmd.CommandText = "SELECT IDARTICULO, NOMBRE, PRECIO FROM ARTICULOS"
        myReader = myCmd.ExecuteReader()
        Do While myReader.Read()
            'Encadeno las columnas.
            row = myReader.GetInt32(0) & vbTab &
                myReader.GetDecimal(2) & vbTab &
                myReader.GetString(1)
            'Muestro en Consola
            Console.WriteLine(row)
        Loop
        myReader.Close()

        'Cuarto DELETE
        myCmd.CommandText = "DELETE ARTICULOS WHERE NOMBRE LIKE @Casi"
        myCmd.ExecuteNonQuery()

        myConn.Close()

        '''''''''''''''''''
        ' Con Using y With
        '''''''''''''''''''

        Using conn = New SqlConnection(connectionstring(activeconn))
            conn.Open()
            Using cmd = conn.CreateCommand
                With cmd
                    .CommandText = "INSERT INTO ARTICULOS (NOMBRE, PRECIO) VALUES (@Nombre, @Precio)"
                    .Parameters.AddWithValue("@Nombre", "USING VB.NET")
                    .Parameters.AddWithValue("@Precio", 222.99)
                    Try
                        .ExecuteNonQuery()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message.ToString(), "Error INSERT")
                    End Try


                    .CommandText = "UPDATE ARTICULOS SET PRECIO = @UPrecio WHERE NOMBRE LIKE @Casi"
                    .Parameters.AddWithValue("@Casi", "USING%")
                    .Parameters.AddWithValue("@UPrecio", 22.99)
                    Try
                        .ExecuteNonQuery()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message.ToString(), "Error UPDATE")
                    End Try

                    .CommandText = "SELECT IDARTICULO, NOMBRE, PRECIO FROM ARTICULOS"
                    Using reader = cmd.ExecuteReader()
                        Console.WriteLine("IDART NOMBRE                            PRECIO")
                        Console.WriteLine("===== ============================== =========")
                        With reader
                            Do While .Read()
                                'New CultureInfo("es-ES"), 
                                row = String.Format("{0,5} {1,-30} {2,9:C2}",
                          .GetInt32(0), .GetString(1), .GetDecimal(2))
                                Console.WriteLine(row)
                            Loop
                            '.Close()
                        End With
                    End Using

                    .CommandText = "DELETE ARTICULOS WHERE NOMBRE LIKE @Casi"
                    Try
                        .ExecuteNonQuery()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message.ToString(), "Error DELETE")
                    End Try

                End With
            End Using
        End Using

    End Sub
End Module
