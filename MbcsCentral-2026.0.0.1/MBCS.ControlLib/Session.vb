Imports System.Collections
Imports System.Collections.Generic
Public Class Session

    Private params As Hashtable

    Public Sub New()
        params = New Hashtable
    End Sub

    Public Sub New(ByVal params As List(Of sessionItem))
        Me.params = New Hashtable
        Dim parm As sessionItem
        For Each parm In params
            Me.params.Add(parm.getKey, parm.getValue)
        Next
    End Sub

    Public Function getParam(ByVal key As String) As Object
        If params.ContainsKey(key) Then
            Return params.Item(key)
        Else
            Return Nothing
        End If
    End Function

    Public Sub setParam(ByVal key As String, ByVal value As Object)
        If Not IsNothing(key) And Not IsNothing(value) Then
            If Me.params.ContainsKey(key) Then
                Me.params.Item(key) = value
            Else
                Me.params.Add(key, value)
            End If
        End If
    End Sub
End Class

Public Class sessionItem
    Private key As String
    Private value As Object

    Public Sub New(ByVal key As String, ByVal value As Object)
        Me.key = key
        Me.value = value
    End Sub

    Public Function getKey() As String
        Return Me.key
    End Function
    Public Function getValue() As Object
        Return value
    End Function
End Class
