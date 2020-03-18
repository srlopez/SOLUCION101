Option Explicit On

Imports System

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello VB.NET 101!")

#Region "====== DECLARACION DE VARIABLES Y Constantes ======="
        Const noUsadaInt As Integer = 30    'Constante
        Dim estaFacturado As Boolean = True 'Booleano
        'Enteros con signo
        Dim sb As SByte = 127               '8 bits -128 .. 127
        Dim sshort As Short = 32.767        '16 bits -32.768 .. 32.767
        Dim i, j, k As Integer              'Enteros con signo 32-bit 
        Dim facturaId As Long = 0           'Entero con signo 64-bit
        'Enteros sin signo
        Dim usbyte As Byte = 255            '8 bits 255
        Dim usshort As UShort = 65535       '16 bits
        Dim usint As UInteger = 4294967295  '32 bits
        Dim uslong As ULong = 1             '64 bits
        'Decimales
        Dim descuento As Single = 2.678737  '4 bytes Coma flotante precision Sencilla
        Dim total As Double = 0.82          '8 bytes Coma flotante Doble precision
        Dim coronavirus As Decimal = 0.98   '16 bytes
        'Chars y Strings
        Dim soloChar As Char                'Caracter de 16 bit Unicode 
        Dim miString As String = ""         'String sin nada
        Dim miString2 As String = String.Empty 'String        
        Dim miString3 As String = "saludos"
        'Fechas
        Dim hoy As DateTime = DateTime.Now  'Hoy ahora!!!!
        Dim fechaNac As DateTime = New DateTime(2020, 3, 16, 13, 48, 39) 'A M D h m s
        Dim fechaDos As DateTime = #03/17/2020 11:25:34# 'Usamos lo que nos intrese
        'Grupos (ver m�s adelante otros tipos de datos)
        Dim items() As Integer = {1, 2, 3}       'Array / Matriz
        Dim items2(100) As Integer
        Dim matriz(,) As Integer = {{1, 2}, {3, 4}}
        'Nullables
        Dim precio? As Single


        'Public - Variables accesibles
        'Protected - Accesible en la clase, y los herederos
        'Friend - Accesible en el 'ensamblado' DLL, Librer�as, etc...
        'Protected Friend - Ensamblado + Clases que heredan de la clase donde est� declarada
        'Private - restringida al modulo, clase o estructura donde es declarada
#End Region

#Region "====== OPERADORES Y DIRECTIVAS DE COMPILACION ===="
#Const VALOR = 1
#If VALOR = 0 Then
        Operadores Computacionales
                +   Suma
                -   Negaci�n/Subtraci�n
                *   Multiplicaci�n
                /   Divisi�n Float
                \   Divisi�n Integer
                Mod M�dulo
        Operadores L�gicos
                <   Menor que
                >   Mayor que
                <=  Menor o igual
                >=  Mayor o igual
                =   Igual
                <>  Desigual ' !=
                Not No
                And Y 
                Or  �
                AndAlso Abreviaci�n
                OrElse  Abreviaci�n
          Operadores de asignaci�n
                =   Asignamos
          Operadores de asignaci�n compuesta
                +=  Suma
                -=  Resta
                *=  Multiplicacion
                /=  Divisi�n
#End If
#If VALOR = 1 Then
        soloChar = "a"
#End If
#End Region

#Region "====== COMPARACIONES E INTERPOLACION DE STRINGS ======="
        i = 12
        k = 1
        j = 4

        'If/Else 
        If precio.HasValue Then
            Console.WriteLine(precio)
        Else
            precio = 6.7
            Console.WriteLine($"nuevo precio {precio}")
        End If

        If (i > 0 AndAlso j <> 5) OrElse k = 1 Then
            Console.WriteLine("(i > 0 AndAlso j <> 5) OrElse k = 1")
        ElseIf (i < 0) Then
            i += 1
            Console.WriteLine("ElseIf")
        ElseIf (i < -5) Then
            i += 1
            Console.WriteLine("ElseIf -5 ")
        Else
            If (i > 8) Then
                Console.WriteLine("(i>8)")
            End If
            Console.WriteLine("Else")
        End If

        'Case + Is = Switch
        Select Case i
            Case Is < 2
                Debug.WriteLine("i es Peque�o")
            Case 3, 5
                Console.WriteLine("i es 3 � 5")
            Case 1 To 4, 7 To 9, 11, 13, Is < k
                Console.WriteLine("i es un jaleo")
            Case Else 'Default
                i = i + 1
                Console.WriteLine($"i  es {i}")
                Console.WriteLine("i  es {0}{2}{1}", i, j, k)
                Console.WriteLine("i  es " + CStr(i))
        End Select

        'If ternario + Cast
        j = IIf(i > 0, 1, 0)
        miString = CStr(IIf(j > 3, 4, 0))
        Console.WriteLine("mistring " + miString)
        'CBool
        'CByte
        'CChar
        'CDate
        '......
        'format string
        Console.WriteLine(Format(descuento, "Fixed"))
        'Currency, Fixed, Percent, Standard, Scientific, E, X, 
        Console.WriteLine(Format(descuento, "00#.###"))
        'format Dia y Horas
        'G, D, d, T, s
        'dd, MM, hh, mm, ss, ....

#End Region

#Region "====== CICLOS ======="
        Console.WriteLine("===== CICLOS =====")

        'While 
        While i < 20
            i += 1
            Console.Write(".")
        End While
        Console.WriteLine("")

        'For + If
        For l As Integer = 0 To 19 Step 2
            If (l = 3) Then Continue For
            Console.Write(l)
            If (l > 6) Then Exit For 'Exit loop
        Next
        Console.WriteLine("")

        'For each + List
        Dim lst As New List(Of String) From {"abc", "def", "ghi"}
        For Each item As String In lst
            Console.Write("_")
            Console.WriteLine(item)

            Debug.WriteLine(item)
        Next
        Console.WriteLine("")

        'Do 
        Do
            Console.Write("-")
            i += 1
        Loop Until i > 9
        Console.WriteLine("")
        Console.WriteLine("===== FIN CICLOS =====")

#End Region

#Region "====== MANEJO DE ERRORES ======="
        Dim s As String = ""
        Dim x As Integer = 5
        Dim y As Integer = 0

        Try
            x = x / y '<--- OJO! Aqu� salta el Error!!!!
        Catch ex As Exception
            s = "Exception Error:" + vbCrLf _
                + "Target: " + ex.TargetSite.ToString _
                + vbCrLf + "Error: " _
                + ex.Message.ToString + vbCrLf _
                + "Trace: " + ex.StackTrace.ToString
            Console.WriteLine("[" + s + "]")

        Finally
            Console.WriteLine($"---{{{s}}}---")
            Console.WriteLine("Siempre paso por aqu�")
        End Try
#End Region

#Region "====== TIPOS DE DATOS AVANZADOS ====="
        ' Array = Vectores
        Dim aitems(10) As String
        aitems(1) = "luis"
        aitems(0) = "pedro"
        aitems(2) = "sonia"

        ' ArrayList
        Dim cuidado As New ArrayList
        cuidado.Add(5)
        cuidado.Add("5")
        Console.WriteLine(cuidado(0))

        'List
        Dim lst1 As New List(Of String) From {"abc", "def", "ghi"}
        Dim list As New List(Of Integer)({2, 3, 5, 7})
        list.Add(1)
        list.Add(4)
        list.Add(6)
        list.Add(8)
        list.RemoveAt(5)
        list.RemoveRange(0, 1)
        Console.WriteLine("list.count: {0}", list.Count)
        Console.WriteLine("ultimo:  {0}", list(list.Count - 1))
        Console.WriteLine("tercero:  {0} y sexto: {1}", list(3), list.Item(5))
        list.Clear()


        ' Colas
        Dim q As Queue(Of Integer) = New Queue(Of Integer)()
        q.Enqueue(5)
        q.Enqueue(10)
        q.Enqueue(15)
        q.Enqueue(20)
        i = q.Dequeue()
        Console.WriteLine($"Elemento desencolado: {i}")

        If q.Peek Then
            Console.WriteLine($"Cola de {q.Count} elementos")
        End If

        'Pilas
        Dim miStack As Stack = New Stack()
        miStack.Push("Hello")
        miStack.Push("World")
        miStack.Push("!")
        miStack.Push("1DAW3")
        miString = miStack.Pop()
        Console.WriteLine("miStack.Count:    {0} para {1}", miStack.Count, miString)
#End Region

#Region "===== INTRO STRINGS ===="
        Dim string1 = "HOLA MUNDO 1DAW3"
        Console.WriteLine(Right(string1, 3))
        Console.WriteLine(Left(string1, 3))
        Console.WriteLine(Mid(string1, 3, 3))
        Mid(string1, 3, 3) = "12345"
        Console.WriteLine(string1)
#End Region

#Region "====== MODULOS, CLASES Y LIBRERIAS ======="

        ' uso de un emsamblado = Librer�a
        miString2 = "holamundo informatico"
        Console.WriteLine(MisUtilidades.Cases.ToUpperImpar(miString2))
        Console.WriteLine(MisUtilidades.Cases.ToUpperPar(miString2))

        Console.WriteLine("invocando1...{0}", MisFunciones.miDato1)
        Console.WriteLine("invocando2...{0}", MisFunciones.SuperSuma(10, 11))
        Console.WriteLine("invocando3...{0}", MiModulo.miDato1)
        Console.WriteLine("invocando4...{0}", MiModulo.SuperSumaDos(10, 11))

        Console.WriteLine(miModulo2.hola("santi"))

        ' En un Archivo de Clase
        Dim e As ClaseVacia = New ClaseVacia()

        ' En un M�dulo dentro de este Archivo
        Dim b As MiClaseB = New MiClaseB(4)
        Console.WriteLine(b.Val2())
        b.Display()
        b.Saludos()
#End Region

    End Sub

End Module

#Region "Anexos"
Module MisFunciones

    Public miDato1 As Integer = 10
    Public miDato2 As Integer = 20

    Public Function SuperSuma(ByVal i1 As Integer, ByVal i2 As Integer)
        Return i2 + i1
    End Function

End Module

Class MiClaseA
    'propiedad
    Private _val As Integer

    Public Sub New(ByVal value As Integer)
        _val = value * 2
        'Me._val = value * 2
        Console.WriteLine(MyBase.ToString)
    End Sub

    Public Function Val() As Integer
        Return _val
    End Function

    Public Overloads Function Val2() As Integer
        Return _val * 2
    End Function

    Public Sub Display()
        Console.WriteLine(_val)
    End Sub

    Public Overridable Sub Hello()
        Console.WriteLine("Hello desde MiClaseA")
    End Sub

    Public Sub Saludos()
        MyClass.Hello() ' Desde la clase en que se invoca A
        Me.Hello()      ' Dede la instancia B
    End Sub

End Class

Class MiClaseB : Inherits MiClaseA
    Private _val2 As Integer

    Public Sub New(ByVal value As Integer)
        MyBase.New(value)
        _val2 = value
    End Sub

    Public Function Val() As Integer
        Return _val2
    End Function

    Public Overrides Sub Hello()
        Console.WriteLine("Hello desde MiClaseB")
    End Sub
End Class

#End Region
