Imports System.Windows.Data

Public Class StringToDateConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert

        Dim format As String = CType(parameter, String)
        If Not IsNothing(format) AndAlso Not format.Equals("1011900") Then
            Return String.Format("{0:#0/00/0000}", value)
        Else
            Return ""
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack

        Return Nothing

    End Function
End Class

