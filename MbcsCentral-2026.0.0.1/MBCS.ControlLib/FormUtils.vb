Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Reflection

Public Class FormUtils

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm">System.Windows.Forms.Form to save</param>
    ''' <remarks></remarks>
    Public Shared Sub saveForm(ByVal frm As System.Windows.Forms.Form)

        Dim asm() As String = [Assembly].GetCallingAssembly.FullName.Split(",")
        formKeyLoc = formKeyLoc.Replace("<assm>", asm(0))
        Dim regkey As Microsoft.Win32.RegistryKey
        regkey = Registry.CurrentUser.CreateSubKey(formKeyLoc & frm.Name, RegistryKeyPermissionCheck.ReadWriteSubTree)
        regkey.SetValue("Top", frm.Top, RegistryValueKind.DWord)
        regkey.SetValue("Left", frm.Left, RegistryValueKind.DWord)
        regkey.SetValue("Width", frm.Width, RegistryValueKind.DWord)
        regkey.SetValue("Height", frm.Height, RegistryValueKind.DWord)
        regkey.SetValue("State", frm.WindowState, RegistryValueKind.DWord)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm">System.Windows.Window to save</param>
    ''' <remarks></remarks>
    Public Shared Sub saveForm(ByVal frm As System.Windows.Window)

        Dim asm() As String = [Assembly].GetCallingAssembly.FullName.Split(",")
        formKeyLoc = formKeyLoc.Replace("<assm>", asm(0))
        Dim regkey As Microsoft.Win32.RegistryKey
        regkey = Registry.CurrentUser.CreateSubKey(formKeyLoc & frm.Name, RegistryKeyPermissionCheck.ReadWriteSubTree)
        regkey.SetValue("Top", frm.Top, RegistryValueKind.DWord)
        regkey.SetValue("Left", frm.Left, RegistryValueKind.DWord)
        regkey.SetValue("Width", frm.Width, RegistryValueKind.DWord)
        regkey.SetValue("Height", frm.Height, RegistryValueKind.DWord)
        regkey.SetValue("State", frm.WindowState, RegistryValueKind.DWord)

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm">System.Windows.Forms.Form for which to retrieve Rectangle</param>
    ''' <returns>System.Drawing.Rectangle</returns>
    ''' <remarks></remarks>
    Public Shared Function getForm(ByRef frm As Form) As System.Drawing.Rectangle

        Dim asm() As String = [Assembly].GetCallingAssembly.FullName.Split(",")
        formKeyLoc = formKeyLoc.Replace("<assm>", asm(0))
        Dim rect As System.Drawing.Rectangle
        Dim state As System.Windows.WindowState = WindowState.Normal
        Dim regKey As Microsoft.Win32.RegistryKey

        regKey = Registry.CurrentUser.OpenSubKey(formKeyLoc & frm.Name, False)
        If (Not IsNothing(regKey)) Then
            rect = New System.Drawing.Rectangle(Val(regKey.GetValue("Left", frm.Left)), Val(regKey.GetValue("Top", frm.Top)), Val(regKey.GetValue("Width", frm.Width)), Val(regKey.GetValue("Height", frm.Height)))
            state = regKey.GetValue("State", frm.WindowState)
        Else
            rect = New System.Drawing.Rectangle(frm.Left, frm.Top, frm.Width, frm.Height)
        End If

        frm.WindowState = state
        frm.Top = rect.Top
        frm.Left = rect.Left
        frm.Height = rect.Height
        frm.Width = rect.Width

        Return rect

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm">System.Windows.Window for which to retrieve Rectangle</param>
    ''' <returns>System.Drawing.Rectangle</returns>
    ''' <remarks></remarks>
    Public Shared Function getForm(ByRef frm As Window) As System.Drawing.Rectangle

        Dim asm() As String = [Assembly].GetCallingAssembly.FullName.Split(",")
        formKeyLoc = formKeyLoc.Replace("<assm>", asm(0))
        Dim rect As System.Drawing.Rectangle
        Dim state As System.Windows.WindowState = WindowState.Normal
        Dim regKey As Microsoft.Win32.RegistryKey

        regKey = Registry.CurrentUser.OpenSubKey(formKeyLoc & frm.Name, False)
        If (Not IsNothing(regKey)) Then
            rect = New System.Drawing.Rectangle(Val(regKey.GetValue("Left", frm.Left)), Val(regKey.GetValue("Top", frm.Top)), Val(regKey.GetValue("Width", frm.Width)), Val(regKey.GetValue("Height", frm.Height)))
            state = regKey.GetValue("State", frm.WindowState)
        Else
            rect = New System.Drawing.Rectangle(frm.Left, frm.Top, frm.Width, frm.Height)
        End If

        frm.WindowState = state
        frm.Top = rect.Top
        frm.Left = rect.Left
        frm.Height = rect.Height
        frm.Width = rect.Width
        
        Return rect

    End Function

End Class
