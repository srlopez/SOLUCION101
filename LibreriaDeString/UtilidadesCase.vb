Imports System.Runtime.CompilerServices

Namespace MisUtilidades
    Public Module Cases
        <Extension>
        Public Function ToUpperImpar(str As String) As String
            ToUpperImpar = "no implementado"

            Dim resp As String = LCase(str)
            For i = 1 To resp.Length Step 2
                Mid(resp, i, 1) = UCase(GetChar(resp, i))
            Next
            ToUpperImpar = resp
        End Function

        Public Function ToUpperPar(str As String) As String
            Dim resp As String = LCase(str)
            Dim i = 2
            While i < resp.Length
                Mid(resp, i, 1) = UCase(GetChar(resp, i))
                i += 2
            End While
            ToUpperPar = resp
        End Function
    End Module

    Public Module Numbers
        Public Function ToUpperPar(i As Integer) As String
            ToUpperPar = "TODO: no implementada"
        End Function
    End Module
End Namespace
