Imports System

Module Program
    Dim i As UInteger = 0
    Sub Main(args As String())
        While True
            Try
                Console.Write("Introduce un número positivo (^C para acabar): ")
                i = CUInt(Console.ReadLine())
                'Console.WriteLine($"{i} esPrimo? {Valor.esPrimo(i)}")
            Catch
                Console.WriteLine("No hagas cosas equivocadas")
                Continue While
            Finally
                ' A afectos formativos
                ' No hago nada
            End Try

#Const ASYNC = 0
#If ASYNC = 0 Then
            Dim t0 = Now
            Console.WriteLine($"{i} #Primos {contaPrimos(i)}")
            Dim duracion As TimeSpan = Now - t0
            Console.WriteLine($"{i} milliseconds: {duracion.Milliseconds}")
#ElseIf ASYNC = 1 Then
            Console.WriteLine($"Running Async {i}....")
            contaPrimosAsync(i)
#End If
        End While
    End Sub

    ' Metodo secuencial
    Function contaPrimos(n As UInteger) As UInteger
        Dim count As UInteger = 0
        For j As UInteger = 0 To i
            If Valor.esPrimo(j) Then count += 1
        Next j
        Return count 'contaPrimos = count
    End Function

    '  Sub/Funcion que invoca la tarea y espera a que acabe
    '  Debe ser Async, y para evitar poner Async en main la he sacado
    Async Sub contaPrimosAsync(n As Integer)
        Dim t0 = Now
        Dim count = Await contaPrimosTask(n)
        Console.WriteLine()
        Console.WriteLine($"{n} #PrimosAsync {count}")
        Dim duracion As TimeSpan = Now - t0
        Console.WriteLine($"{n} milliseconds {duracion.Milliseconds} Async")
    End Sub

    ' Public Async - Tarea
    Public Async Function contaPrimosTask(nParam As UInteger) As Task(Of UInteger)
        Dim count As UInteger = Await Task.Run(Of UInteger)(
            Function() 'Lambda
                Return contaPrimos(nParam)
            End Function
        )
        Return count
    End Function

End Module

Class Valor
    ' Shared permite invocar sin crear Objeto  = Static

    Public Shared Function esPar(n As UInteger) As Boolean
        esPar = (n Mod 2 = 0)
    End Function

    Public Shared Function esImpar(n As UInteger) As Boolean
        esImpar = True
    End Function

    Public Shared Function esRacional(n As Double) As Boolean
        'esRacional = True
        Return True
    End Function
    Public Shared Function esPrimo(n As UInteger) As Boolean
        esPrimo = False
        If n < 2 Then Exit Function
        If n = 2 Then Return True

        If esPar(n) Then Exit Function
        Dim i As Integer = 3
        While i * i <= n
            If n Mod i = 0 Then Exit Function
            i += 2
        End While
        Debug.Print(n)
        esPrimo = True
    End Function
End Class
