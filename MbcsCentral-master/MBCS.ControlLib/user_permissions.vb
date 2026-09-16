Public Class user_permissions
	Inherits clsDB

	Public Const SQL_COL_PERMISSIONS_ID = "permissions_id"
	Public Const SQL_COL_USER_ID = "user_id"
	Public Const SQL_COL_PERMISSION_ID = "permission_id"
    Public Const SQL_COL_PERMISSION_TYPE = "permission_type"
    Public Const SQL_COL_ACCESS = "access"
    Public Const SQL_COL_MODIFY = "modify"
    Public Const SQL_COL_REMOVE = "remove"

    Public Const SQL_COL_ID As String = SQL_COL_PERMISSIONS_ID
    Shared TABLE As String = "user_permissions"

    Shared SQL_LOAD As String = " SELECT " & _
      "[" & SQL_COL_PERMISSIONS_ID & "]," & _
      "[" & SQL_COL_USER_ID & "]," & _
      "[" & SQL_COL_PERMISSION_ID & "]," & _
      "[" & SQL_COL_PERMISSION_TYPE & "]," & _
      "[" & SQL_COL_ACCESS & "]," & _
      "[" & SQL_COL_MODIFY & "]," & _
      "[" & SQL_COL_REMOVE & "]" & _
      " FROM " & TABLE & _
      " WHERE " & SQL_COL_PERMISSIONS_ID & "= ?;"

    Shared SQL_LOAD_ALL As String = " SELECT " & _
      "[" & SQL_COL_PERMISSIONS_ID & "]," & _
      "[" & SQL_COL_USER_ID & "]," & _
      "[" & SQL_COL_PERMISSION_ID & "]," & _
      "[" & SQL_COL_PERMISSION_TYPE & "]," & _
      "[" & SQL_COL_ACCESS & "]," & _
      "[" & SQL_COL_MODIFY & "]," & _
      "[" & SQL_COL_REMOVE & "]" & _
      " FROM " & TABLE & ";"

    Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
      "[" & SQL_COL_USER_ID & "]," & _
      "[" & SQL_COL_PERMISSION_ID & "]," & _
      "[" & SQL_COL_PERMISSION_TYPE & "]," & _
      "[" & SQL_COL_ACCESS & "]," & _
      "[" & SQL_COL_MODIFY & "]," & _
      "[" & SQL_COL_REMOVE & "]" & _
      ") VALUES (?, ?, ?, ?, ?, ?);"

    Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
      "[" & SQL_COL_USER_ID & "]=?," & _
      "[" & SQL_COL_PERMISSION_ID & "]=?," & _
      "[" & SQL_COL_PERMISSION_TYPE & "]=?," & _
      "[" & SQL_COL_ACCESS & "]=?," & _
      "[" & SQL_COL_MODIFY & "]=?," & _
      "[" & SQL_COL_REMOVE & "]=?" & _
      " FROM " & TABLE & _
      " WHERE " & SQL_COL_PERMISSIONS_ID & "= ?;"

    Shared SQL_LOAD_BY_USER As String = " SELECT " & _
      "[" & SQL_COL_PERMISSIONS_ID & "]," & _
      "[" & SQL_COL_USER_ID & "]," & _
      "[" & SQL_COL_PERMISSION_ID & "]," & _
      "[" & SQL_COL_PERMISSION_TYPE & "]," & _
      "[" & SQL_COL_ACCESS & "]," & _
      "[" & SQL_COL_MODIFY & "]," & _
      "[" & SQL_COL_REMOVE & "]" & _
      " FROM " & TABLE & _
      " WHERE " & SQL_COL_USER_ID & " = ?;"

    Protected ID As Integer = 0
    Protected permissions_id As Int64
    Protected user_id As Int64
    Protected permission_id As Int64
    Protected permission_type As Int32
    Protected access As Boolean
    Protected modify As Boolean
    Protected remove As Boolean

    Public Sub New()
        MyBase.New(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
    End Sub

    Public Sub New(ByVal permissions_id As Int64, ByVal user_id As Int64, ByVal permission_id As Int64, ByVal permission_type As Int32, ByVal access As Boolean, ByVal modify As Boolean, ByVal remove As Boolean)
        MyBase.New(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
        Me.permissions_id = permissions_id
        Me.user_id = user_id
        Me.permission_id = permission_id
        Me.permission_type = permission_type
        Me.access = access
        Me.modify = modify
        Me.remove = remove
    End Sub

    Protected Function fill(ByVal dr As DataRow) As user_permissions

        Try
            ID = 1
            Me.permissions_id = dr(SQL_COL_PERMISSIONS_ID)
            Me.user_id = dr(SQL_COL_USER_ID)
            Me.permission_id = dr(SQL_COL_PERMISSION_ID)
            Me.permission_type = dr(SQL_COL_PERMISSION_TYPE)
            Me.access = dr(SQL_COL_ACCESS)
            Me.modify = dr(SQL_COL_MODIFY)
            Me.remove = dr(SQL_COL_REMOVE)
        Catch ex As Exception
        End Try
        Return Me

    End Function

    Public Overloads Shared Function load(ByVal permissions_id As Int64) As user_permissions

        Dim obj As user_permissions = New user_permissions
        Dim opt() As String = {permissions_id}
        Dim ds As DataSet = New DataSet
        ds = obj.load(SQL_LOAD, opt)
        Dim dr As DataRow = ds.Tables(0).Rows(0)

        Return obj.fill(dr)

    End Function

    Public Shared Function loadByUser(ByVal userid As Int64) As user_permissions()

        Dim opt() As String = {userid}

        Dim list As ArrayList = New ArrayList()

        Dim ds As DataSet = New DataSet
        Dim obj As New user_permissions

        ds = obj.load(SQL_LOAD_BY_USER, opt)
        obj = Nothing
        Dim dr As DataRow
        For Each dr In ds.Tables(0).Rows
            obj = New user_permissions
            obj.fill(dr)
            list.Add(obj)
            obj = Nothing
        Next
        Try
            Return DirectCast(list.ToArray(GetType(user_permissions)), user_permissions())
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Overloads Shared Function load() As user_permissions()

        Dim list As ArrayList = New ArrayList()

        Dim ds As DataSet = New DataSet
        Dim obj As user_permissions = New user_permissions

        ds = obj.loadAll()
        obj = Nothing
        Dim dr As DataRow
        For Each dr In ds.Tables(0).Rows
            obj = New user_permissions
            obj.fill(dr)
            list.Add(obj)
            obj = Nothing
        Next
        Try
            Return DirectCast(list.ToArray(GetType(user_permissions)), user_permissions())
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function save() As Boolean

        If ID > 0 Then
            Dim values() As String = {user_id, permission_id, permission_type, access, modify, remove, permissions_id}
            If Me.updateRecord(ID, values) Then
                Return True
            Else
                Return False
            End If
        Else
            Dim values() As String = {user_id, permission_id, permission_type, access, modify, remove}
            ID = Me.insertRecord(values)
            If ID >= 0 Then
                Return True
            End If
        End If
        Return False

    End Function

    ' Get/Set Routines
    Public Function getpermissions_id() As Int64
        Return Me.permissions_id
    End Function

    Public Sub setuser_id(ByVal value As Int64)
        Me.user_id = value
    End Sub
    Public Function getuser_id() As Int64
        Return Me.user_id
    End Function

    Public Sub setpermission_id(ByVal value As Int64)
        Me.permission_id = value
    End Sub
    Public Function getpermission_id() As Int64
        Return Me.permission_id
    End Function

    Public Sub setpermission_type(ByVal value As Int32)
        Me.permission_type = value
    End Sub
    Public Function getpermission_type() As Int32
        Return Me.permission_type
    End Function

	Public Sub setaccess(ByVal value As Boolean)
		Me.access = value
	End Sub
	Public Function getaccess() As Boolean
		Return Me.access
	End Function

	Public Sub setmodify(ByVal value As Boolean)
		Me.modify = value
	End Sub
	Public Function getmodify() As Boolean
		Return Me.modify
	End Function

	Public Sub setremove(ByVal value As Boolean)
		Me.remove = value
	End Sub
	Public Function getremove() As Boolean
		Return Me.remove
	End Function

End Class
