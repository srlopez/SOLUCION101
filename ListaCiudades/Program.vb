Imports System

Module Program
    Sub Main(args As String())

        Dim input As New List(Of String) From {"Tokyo", "London", "Rome", "Donlon", "Kyoto", "Paris"}
        Dim output As New List(Of List(Of String)) From {}
#Region "INPUT"
        Console.Write("[ ")
        For i = 0 To input.Count - 1
            Console.Write(input(i) + ", ")
        Next
        Console.WriteLine(vbBack + vbBack + " ]")
#End Region

        For i As Integer = 0 To input.Count - 1
            Dim found = False
            Dim j As Integer
            For j = 0 To output.Count - 1
                If test(input(i), output(j)(0)) Then
                    found = True
                    Exit For
                End If
            Next
            If Not found Then
                output.Add(New List(Of String) From {input(i)})
            Else
                output(j).Add(input(i))
            End If
        Next

#Region "OUTPUT"
        Console.WriteLine("[ ")
        For i = 0 To output.Count - 1
            Console.Write("   [ ")
            For j = 0 To output(i).Count - 1
                Console.Write(output(i)(j) + ", ")
            Next
            Console.WriteLine(vbBack + vbBack + " ]")
        Next
        Console.WriteLine("]")
#End Region
    End Sub

    Function test(a As String, b As String) As Boolean
        ' tokio            kioto
        Dim ax2 = a.ToLower + a.ToLower
        ' tokiotokio
        Return (ax2.IndexOf(b.ToLower) >= 0)
    End Function
End Module

