Imports System.Drawing
Imports System.Windows
Imports System.Windows.Controls
Public Class FormattedTextBox
    Inherits System.Windows.Controls.TextBox

    Protected _length As Integer
    Protected _decimals As Integer = 0
    Protected _negativeColor As Boolean = False
    Protected _leadingZero As Boolean = False
    Protected wasLoaded As Boolean = False

    Public Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub OnTextChanged(e As System.Windows.Controls.TextChangedEventArgs)
        Try
            If Not wasLoaded Or Not Me.IsEnabled Then
                formatText()
                e.Handled = True
                wasLoaded = True
            Else
                formatText()
            End If
            MyBase.OnTextChanged(e)
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Protected Overrides Sub OnGotFocus(e As System.Windows.RoutedEventArgs)

        Try
            Dim txt As FormattedTextBox = DirectCast(e.Source, FormattedTextBox)
            txt.Text = txt.Text.Trim
            txt.Select(0, _length - 1)
            e.Handled = True
            MyBase.OnGotFocus(e)
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Protected Overrides Sub OnKeyDown(e As System.Windows.Input.KeyEventArgs)

        If (e.Key >= Windows.Input.Key.D0 And e.Key <= Windows.Input.Key.D9) Or (e.Key >= Windows.Input.Key.NumPad0 And e.Key <= Input.Key.NumPad9) Then
            e.Handled = False
        ElseIf e.Key <> Windows.Input.Key.Back And e.Key <> Windows.Input.Key.Tab And e.Key <> Windows.Input.Key.Decimal And e.Key <> Input.Key.OemPeriod And e.Key <> Windows.Input.Key.OemMinus And e.Key <> Windows.Input.Key.OemPlus Then
            e.Handled = True
        End If
        MyBase.OnKeyDown(e)

    End Sub

    Protected Overrides Sub OnLostFocus(e As System.Windows.RoutedEventArgs)

        Try
            formatText()
            MyBase.OnLostFocus(e)
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Protected Sub formatText()

        Try
            If Not IsNumeric(Me.Text) Or Me.Text.Trim = "" Then Exit Sub
            Dim s As String = ""

            If _decimals > 0 Then
                s = StrDup(_length - _decimals - 1, IIf(_leadingZero, "0", "#"))
                s &= "."
                s &= StrDup(_decimals, "0")
            Else
                s = StrDup(_length, IIf(_leadingZero, "0", "#"))
            End If
            Me.Text = Format(Val(Me.Text), s).PadLeft(_length)
            If _negativeColor And Val(Me.Text) < 0 Then
                Me.Foreground = System.Windows.Media.Brushes.Red
            Else
                Me.Foreground = System.Windows.Media.Brushes.Black
            End If
        Catch ex As StackOverflowException

        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Property length As Integer
        Get
            Return _length
        End Get
        Set(value As Integer)
            _length = value
            Me.MaxLength = _length
        End Set
    End Property
    Public Property decimals As Integer
        Get
            Return _decimals
        End Get
        Set(value As Integer)
            Try
                _decimals = value
            Catch ex As System.Exception
                MsgBox(ex.Message)
            End Try
        End Set
    End Property
    Public Property NegativeColorRed As Boolean
        Get
            Return _negativeColor
        End Get
        Set(value As Boolean)
            _negativeColor = value
        End Set
    End Property
    Public Property LeadingZero As Boolean
        Get
            Return _leadingZero
        End Get
        Set(value As Boolean)
            _leadingZero = value
        End Set
    End Property

    Private Sub FormattedTextBox_Initialized(sender As Object, e As System.EventArgs) Handles Me.Initialized

    End Sub
End Class
