Imports System

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello VB.NET 101!")

#Region "====== DECLARACION DE VARIABLES Y Constantes ======="
        Const noUsadaInt As Integer = 30    'Constante
        Dim estaFacturado As Boolean = True 'Booleano
        'Enteros con signo
        Dim sb As SByte = 127               '8bits -128 .. 127
        Dim sshort As Short = 32.767        '16 bits -32.768 .. 32.767
        Dim i, j, k As Integer              'Enteros con signo 32-bit 
        Dim facturaId As Long = 0           'Entero con signo 64-bit
        'Enteros sin signo
        Dim usbyte As Byte = 255            '8 bits 255
        Dim usshort As UShort = 65535       '16 bits
        Dim usint As UInteger = 4294967295  '32 bits
        Dim uslong As ULong = 1            '64 bits
        'Decimales
        Dim descuento As Single = 0.6787    '4 bytes Coma flotante precision Sencilla
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
        Dim fechaDos As DateTime = #03/17/2020 11:25# 'Usamos lo que nos intrese
        'Grupos (ver m�s adelante otros tipos de datos)
        Dim items(100) As Integer           'Array (See ArrayList)
        'Nullables
        Dim precio? As single
#End Region

#Region "==== OPERADORES Y DIRECTIVAS DE COMPILACION ===="
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
                <>  Desigual
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
        If (precio.HasValue) Then
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
        Else
            If (i > 8) Then
                Console.WriteLine("(i>8)")
            End If
            Console.WriteLine("Else")
        End If

        'Case + Is
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
                Console.WriteLine("i  es {0}", i)
                Console.WriteLine("i  es " + CStr(i))
        End Select

        'If ternario + Cast
        j = CInt(IIf(i > 0, 1, 0))
#End Region


    End Sub
End Module
