Imports System
' Ejemplo de multiplicacion de matrices
' https://es.wikipedia.org/wiki/Multiplicaci%C3%B3n_de_matrices
#If COMMENT Then
Un ejemplo de multiplicación
Se han de intrioducir los números( enteros, positivos o negativos) separados por un espacio
De la primera introducción de datos se obtiene la dimensión de las matrices
y luego se valida que se introducen los datos correctamente

Es decir funciona para cualquier dimension de matriz

== m1 ==
row#1:  1 4 7
(3 int) row#2: 2 5 8
(3 int) row#3: 3 6 9
== m1 ==
  1  4  7
  2  5  8
  3  6  9
== m2 ==
(3 int) row#1: 1 -1 2
(3 int) row#2: 2 -1 2
(3 int) row#3: 3 -3 0
== m2 ==
  1 -1  2
  2 -1  2
  3 -3  0
== p ==
 30-26 10
 36-31 14
 42-36 18
#End If
Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World! multiplica matrices")

        ' Matrices cuadradas
        Dim m1 = PromptMatrix("m1")
        Dim items = m1.Count

        ShowMatrix("m1", m1)
        Dim m2 = PromptMatrix("m2", items)
        ShowMatrix("m2", m2)

        Dim p = New List(Of List(Of Integer)) From {}


        For i As Integer = 0 To items - 1
            p.Add(New List(Of Integer) From {})
            For j As Integer = 0 To items - 1
                p(i).Add(0)
                For n As Integer = 0 To items - 1
                    p(i)(j) += m1(i)(n) * m2(n)(j)
                Next
            Next
        Next


        ShowMatrix("p", p)

    End Sub

    Function PromptRow(prompt As String, n As Integer) As List(Of Integer)
        Dim ns = New List(Of Integer) From {}
        While True
            If n > 0 Then Console.Write($"({n} int) ")
            Console.Write($"{prompt}: ")

            Try
                Dim input As String = Console.ReadLine()
                Dim ls = input.Split(" ")
                ' verificamos que tenemos el mismo número de elementos pedidos
                If n <> 0 And ls.Length <> n Then Throw New System.Exception("Número de elementos en la fila incorrecto")
                For Each s In ls
                    ns.Add(CInt(s))
                Next
                Return ns
            Catch ex As Exception
                Console.WriteLine($"{ex.Message}")
                Console.WriteLine("Inténtalo de nuevo")
            End Try

        End While
        Return ns
    End Function

    Function PromptMatrix(name As String, Optional n As Integer = 0) As List(Of List(Of Integer))
        Console.WriteLine($"== {name} ==")

        Dim m = New List(Of List(Of Integer)) From {}
        Dim f = 1
        Dim row = PromptRow($"row#{f}", n)
        Dim items = row.Count
        m.Add(row)
        While f < items
            f += 1
            row = PromptRow($"row#{f}", items)
            m.Add(row)
        End While
        Return m
    End Function

    Sub ShowMatrix(name As String, m As List(Of List(Of Integer)))
        Console.WriteLine($"== {name} ==")
        For i As Integer = 0 To m.Count - 1
            For j As Integer = 0 To m(i).Count - 1
                Console.Write($"{m(i)(j),3}")
            Next
            Console.WriteLine()
        Next
    End Sub

End Module
