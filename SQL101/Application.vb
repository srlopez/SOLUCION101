Imports System.Data.SqlClient
Imports System.Data.OleDb

#Const SQLSERVER = 0
Public Class Application

    Public Shared Sub Main(args As String())


#If SQLSERVER = 1 Then
        Dim wildChar = "%"
        Dim connectionString As String =
            "Data Source=slimbook;Initial Catalog=BASE;User ID=sa;Password=Pa88word"
        Dim myConn As SqlConnection
        Dim myCmd As SqlCommand
        myConn = New SqlConnection(connectionString)
#Else
        Dim wildChar = "%" '"*"
        Dim connectionString As String =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\..\..\BASE.accdb;Persist Security Info=False"
        Dim myConn As OleDbConnection
        Dim myCmd As OleDbCommand
        myConn = New OleDbConnection(connectionString)
#End If

        Console.OutputEncoding = Text.Encoding.UTF8 'Para permitir el €
        Console.WriteLine("Hello SQL-World!!!")
        Console.WriteLine(connectionString)

        '''''''''''''''''''
        ' Con Using y With
        '''''''''''''''''''

        Using myConn ' hace un close/dispose del objeto = nos ayuda a evitar errores
            myConn.Open()
            myCmd = myConn.CreateCommand
            Using myCmd
                With myCmd ' nos ayuda a teclear menos

                    ' CREATE
                    Try
#If SQLSERVER = 1 Then
                    .CommandText = "
                        DROP TABLE IF EXISTS ARTICULOS
                        CREATE TABLE ARTICULOS(
                            IDARTICULO INT IDENTITY,
                            NOMBRE NVARCHAR(30),
                            PRECIO NUMERIC(5,2)
                        )"
#Else
                        .CommandText = "
                        CREATE TABLE ARTICULOS(
                            IDARTICULO AUTOINCREMENT PRIMARY KEY,
                            NOMBRE CHAR(30),
                            PRECIO DECIMAL(5,2)
                        )"
#End If

                        .ExecuteNonQuery()
                    Catch ex As Exception
                        ' Posible error la TABLA ya EXISTE (ACCESS)
                        Console.WriteLine($"Error: {ex.Message}")
                        .CommandText = "DELETE FROM ARTICULOS"
                        .ExecuteNonQuery()
                    End Try

                    ' SCALAR
                    .CommandText = "SELECT COUNT(*) FROM ARTICULOS"
                    Console.WriteLine($"CREATE COUNT(*) { .ExecuteScalar()}")

                    ' INSERT
                    ' @Nombre, @Precio
                    Try
                        .CommandText = "INSERT INTO ARTICULOS 
                                    (NOMBRE, PRECIO) VALUES 
                                    (@Nombre, @Precio)"
                        .Parameters.AddWithValue("@Nombre", "USING VB.NET")
                        .Parameters.AddWithValue("@Precio", 222.99)
                        .ExecuteNonQuery()

                        .Parameters.Clear()
                        .Parameters.AddWithValue("@Nombre", "DATO DOS")
                        .Parameters.AddWithValue("@Precio", 111.11)
                        .ExecuteNonQuery()
                    Catch ex As Exception
                        Console.WriteLine("Error INSERT ", ex.Message.ToString())
                    End Try

                    ' SCALAR
                    .CommandText = "SELECT COUNT(*) FROM ARTICULOS"
                    Console.WriteLine($"INSERT COUNT(*) { .ExecuteScalar()}")

                    '----

                    ' UPDATE
                    ' @UPrecio @Casi
                    Dim slike = "US" + wildChar
                    Dim sql As String = "UPDATE ARTICULOS" &
                                        "  SET PRECIO = @UPrecio" &
                                        "  WHERE NOMBRE LIKE @like"

                    Try
                        Console.WriteLine("SQL: " + sql)
                        .CommandText = sql
                        .Parameters.Clear()
                        'OleDb necesita los parámetros en orden
                        .Parameters.AddWithValue("@UPrecio", 16.8)
                        .Parameters.AddWithValue("@like", slike)
                        .ExecuteNonQuery()
                    Catch ex As Exception
                        Console.WriteLine("Error UPDATE ", ex.Message.ToString())
                    End Try

                    ' BUCLE SELECT
                    .CommandText = "SELECT IDARTICULO, NOMBRE, PRECIO 
                                    FROM ARTICULOS"
                    Using reader = .ExecuteReader()
                        Console.WriteLine("IDART NOMBRE                            PRECIO")
                        Console.WriteLine("===== ============================== =========")
                        With reader
                            Dim row As String
                            Do While .Read()
                                'New CultureInfo("es-ES"), 
                                row = String.Format("{0,5} {1,-30} {2,9:C2}",
                                reader(0), reader(1), reader(2))

                                '.GetInt32(0), .GetString(1), .GetDecimal(2))
                                Console.WriteLine(row)
                            Loop
                        End With
                    End Using

                    ' DELETE
                    Try
                        .CommandText = "DELETE FROM ARTICULOS 
                                    WHERE NOMBRE LIKE @like"
                        .Parameters.Clear()
                        .Parameters.AddWithValue("@like", slike)
                        .ExecuteNonQuery()
                    Catch ex As Exception
                        Console.WriteLine("Error DELETE ", ex.Message.ToString())
                    End Try

                    ' SCALAR
                    .CommandText = "SELECT COUNT(*) FROM ARTICULOS"
                    Console.WriteLine($"DELETE COUNT(*) { .ExecuteScalar()}")

                End With
            End Using
        End Using

    End Sub

End Class





