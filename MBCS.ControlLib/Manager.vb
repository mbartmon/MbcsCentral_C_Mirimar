Imports System.Data.SqlClient
Public Module Manager

    Public userSession As Session

    Public Function setUser(ByVal login As String) As Accounts

        Dim user As Accounts = Accounts.load(login)
        If Not IsNothing(user) Then
            userSession.setParam("User", user)
            Return user
        Else
            Return Nothing
        End If

    End Function

    Public Function getAppPerms() As user_permissions()

        Dim perms() As user_permissions = Nothing
        Dim user As Accounts = userSession.getParam("User")
        If Not IsNothing(user) Then
            perms = user_permissions.loadByUser(user.getAccountId)
        End If

        Return perms

    End Function

End Module
