Imports System
' Programa inspirado en:
' https://hackernoon.com/how-to-lose-an-it-job-in-10-minutes-3d63213c8370
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
#Const ORDERED = 2
#If ORDERED = 1 Then
        ' tokio            kioto
        Dim ax2 = a.ToLower + a.ToLower
        ' tokiotokio
        Return (ax2.IndexOf(b.ToLower) >= 0)
#Else
        ' Como Lambda
        Dim charsOrdered = Function(s As String) As String
                               ' Obtengo los Bytes de un Array
                               Dim sBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(s)
                               ' Ordeno el array
                               Array.Sort(Of Byte)(sBytes)
                               ' Convierto el Array a String
                               Return System.Text.ASCIIEncoding.ASCII.GetString(sBytes)
                           End Function
        ' London = dlnnoo
        ' Donlon = dlnnoo
        Return charsOrdered(a.ToLower) = charsOrdered(b.ToLower)
#End If
    End Function


End Module

