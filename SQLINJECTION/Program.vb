Imports System
Imports System.Globalization
Imports System.Net
'Imports System.Web
'Imports System.Uri
Imports System.Data.SqlClient

Module Program

    ' ASP.NET 

    'Http
    Dim Port As Integer = 4444
    Dim urls() As String = {"http://*:" + Port.ToString + "/"}

    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private row As String
    Private connectionstring As String =
             "Data Source=slimbook;Initial Catalog=BASE;User ID=sa;Password=Pa88word"


    Sub Main(args As String())
        Console.WriteLine("Hello WebServer World!")
        Console.WriteLine(String.Format("System.Net.HttpListener.IsSupported: {0}", HttpListener.IsSupported))
        If args.Count() > 0 Then
            Port = CInt(args(0))
            urls(0) = "http://*:" + Port.ToString + "/"
        End If
        Console.WriteLine("   >>> Access Denied")
        Console.WriteLine("   >>> run HttpListener in non-admin mode")
        Console.WriteLine("   >>> desde consola y SuperUsuario")
        Console.WriteLine("   >>> netsh http add urlacl url=" + urls(0) + " user=Todos")
        Console.WriteLine()
        Console.WriteLine(urls(0))

        RunServer(urls, connectionstring)
    End Sub

    Private Sub RunServer(ByVal urls() As String, ByVal database As String)

        'Creamos un proceso listener para las urls
        Dim listener As System.Net.HttpListener = New System.Net.HttpListener()
        For Each s As String In urls
            listener.Prefixes.Add(s)
        Next

        'Connection y abrimos
        myConn = New SqlConnection(database)
        myConn.Open()

        'Comando a ejecutar
        myCmd = myConn.CreateCommand

        'Primero INSERT
        Randomize()
        Dim rs = Int((96 * Rnd()) + 16).ToString
        myCmd.CommandText = "DELETE FROM ARTICULOS 
                    WHERE IDARTICULO < (SELECT MAX(IDARTICULO)-10 FROM ARTICULOS)
                    INSERT INTO ARTICULOS (NOMBRE, PRECIO) VALUES 
                        ('Boli" + rs + "', " + rs + "3.45),
                        ('Libro" + rs + "', " + rs + "1.23)
                        "
        myCmd.ExecuteNonQuery()

        Try
            ' Start
            Console.WriteLine("Start ")

            listener.Start()
            Console.WriteLine("Listening on " + urls(0) + "...")
            Console.WriteLine("prueba " + urls(0) + "api/users")
            Console.WriteLine("prueba " + urls(0) + "api/users/n")
            Console.WriteLine("prueba " + urls(0) + "api/users/12 OR 1=1")


            Do While True
                Dim response As HttpListenerResponse = Nothing
                Try

                    ' Aviso: GetContext bloquea mientras espera
                    Dim context As HttpListenerContext = listener.GetContext()
                    ' solicitud
                    Dim request = context.Request


                    ' Cabecera de Request
                    'For Each key As String In request.Headers.AllKeys
                    'Console.WriteLine(key + ": " + request.Headers(key).ToString)
                    'Next

                    ' Importantes
                    Console.WriteLine($"request.Url        {request.Url}")
                    Console.WriteLine($"request.HttpMethod {request.HttpMethod}")

                    'Dim uri As New Uri(HttpUtility.UrlDecode(request.Url.ToString))
                    Dim uri As New Uri(request.Url.ToString)

                    'Dim protocol As String = uri.Scheme     ' http
                    'Dim host As String = uri.Host           ' localhost
                    'Dim port As Integer = uri.Port          ' 4444
                    Dim path As String = uri.LocalPath       ' /lo que pongamos
                    'Dim querystring As String = uri.Query   ' ?val=1&value=normal
                    'Dim hash As String = uri.Fragment       ' despues del #

                    ' respondemos
                    Dim responseString As String
                    Dim rows As Integer = 0

                    Select Case path
                        Case "/api/users", "/api/users/"
                            myCmd.CommandText = "SELECT IDARTICULO, NOMBRE, PRECIO 
                                                FROM ARTICULOS"
                            responseString = "<HTML><BODY><h1>Muchos Users</h1><h2>"
                            responseString += myCmd.CommandText + "</h2>"

                            myReader = myCmd.ExecuteReader()
                            Do While myReader.Read()
                                rows += 1
                                'Leo las columnas.
                                row = myReader.GetInt32(0).ToString & "  -  " &
                                    myReader.GetDecimal(2).ToString & "  -  " &
                                    myReader.GetString(1)
                                'y encadeno
                                Console.WriteLine(row)
                                responseString += row & "</br>"
                            Loop
                            myReader.Close()
                            responseString += "<h2>#" & rows.ToString & " rows</h2>"
                            responseString += "</BODY></HTML>"

                        Case "/api/users/0" To "/api/users/99999"
                            Dim strArr() As String
                            strArr = path.Split("/")
                            Console.WriteLine("�ltimo Param: " + strArr(strArr.Length - 1))

                            myCmd.CommandText = "SELECT IDARTICULO, NOMBRE, PRECIO 
                                                FROM ARTICULOS WHERE IDARTICULO =" & strArr(strArr.Length - 1)

                            responseString = "<HTML><BODY><h1>1 User</h1><h2>"
                            responseString += myCmd.CommandText + "</h2>"
                            myReader = myCmd.ExecuteReader()
                            Do While myReader.Read()
                                rows += 1
                                'Leo la columnas.
                                row = myReader.GetInt32(0).ToString & "  -  " &
                                    myReader.GetDecimal(2).ToString & "  -  " &
                                    myReader.GetString(1) & "</br>"
                                'y encadeno
                                Console.WriteLine(row)
                                responseString += row
                                'Exit Do
                            Loop
                            myReader.Close()
                            responseString += IIf(rows > 0, "User encontrado", "no encontrado")
                            responseString += "</br></BODY></HTML>"
                        Case Else 'Default
                            responseString =
                                "<HTML><BODY>Hello World " &
                                DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo) &
                                "</BODY></HTML>"
                    End Select

                    response = context.Response
                    Dim buffer() As Byte =
                        System.Text.Encoding.UTF8.GetBytes(responseString)
                    response.ContentLength64 = buffer.Length
                    Dim output As System.IO.Stream = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)

                Catch ex As HttpListenerException
                    Console.WriteLine(ex.Message)
                Finally
                    If response IsNot Nothing Then
                        response.Close()
                    End If
                End Try

            Loop
        Catch ex As HttpListenerException
            Console.WriteLine(ex.Message)
        Finally
            listener.Close()
            Console.WriteLine("Fin Listening...")
        End Try

        myCmd.Dispose()
        myConn.Close()
        myConn.Dispose()

    End Sub

End Module
