Public Class permission
	Inherits clsDB

	Public Const SQL_COL_PERMISSION_ID = "permission_id"
	Public Const SQL_COL_PERMISSION_TYPE = "permission_type"
	Public Const SQL_COL_PERMISSION_NAME = "permission_name"
	Public Const SQL_COL_PERMISSION_DESCRIPTION = "permission_description"

	Public Const SQL_COL_ID as String = SQL_COL_PERMISSION_ID
	Shared TABLE As String = "permission"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL_PERMISSION_ID & "]," & _
			"[" & SQL_COL_PERMISSION_TYPE & "]," & _
			"[" & SQL_COL_PERMISSION_NAME & "]," & _
			"[" & SQL_COL_PERMISSION_DESCRIPTION & "]" & _
			" FROM " & TABLE & _
			" WHERE SQL_COL_PERMISSION_ID = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL_PERMISSION_ID & "]," & _
			"[" & SQL_COL_PERMISSION_TYPE & "]," & _
			"[" & SQL_COL_PERMISSION_NAME & "]," & _
			"[" & SQL_COL_PERMISSION_DESCRIPTION & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL_PERMISSION_TYPE & "]," & _
			"[" & SQL_COL_PERMISSION_NAME & "]," & _
			"[" & SQL_COL_PERMISSION_DESCRIPTION & "]" & _
			") VALUES (?, '?', '?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL_PERMISSION_TYPE & "]=?," & _
			"[" & SQL_COL_PERMISSION_NAME & "]='?'," & _
			"[" & SQL_COL_PERMISSION_DESCRIPTION & "]='?'" & _
			" FROM " & TABLE & _
			" WHERE SQL_COL_PERMISSION_ID = ?;"

	Protected ID as Integer = 0
	Protected permission_id As Int64
	Protected permission_type As Int32
	Protected permission_name As String
	Protected permission_description As String

	Public Sub New()
        MyBase.New(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal permission_id As Int64,ByVal permission_type As Int32,ByVal permission_name As String,ByVal permission_description As String)
        MyBase.New(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		Me.permission_id = permission_id
		Me.permission_type = permission_type
		Me.permission_name = permission_name
		Me.permission_description = permission_description
	End Sub

	Protected Function fill(ByVal dr as DataRow) As permission

		Try
			ID = 1
			Me.permission_id = dr(SQL_COL_PERMISSION_ID)
			Me.permission_type = dr(SQL_COL_PERMISSION_TYPE)
			Me.permission_name = dr(SQL_COL_PERMISSION_NAME)
			Me.permission_description = dr(SQL_COL_PERMISSION_DESCRIPTION)
		Catch ex As Exception
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal permission_id As Int64) As permission

		Dim obj As permission = New permission
		Dim opt() as String = {permission_id}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As permission()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As permission = New permission

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New permission
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(permission)), permission())
		Catch ex As Exception
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If ID > 0 Then
			Dim values() as String = {permission_type, permission_name, permission_description, permission_id}
			If Me.updateRecord(ID, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {permission_type, permission_name, permission_description}
			ID = Me.insertRecord(values)
			If ID >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	' Get/Set Routines
	Public Function getpermission_id() As Int64
		Return Me.permission_id
	End Function

	Public Sub setpermission_type(ByVal value As Int32)
		Me.permission_type = value
	End Sub
	Public Function getpermission_type() As Int32
		Return Me.permission_type
	End Function

	Public Sub setpermission_name(ByVal value As String)
		Me.permission_name = value
	End Sub
	Public Function getpermission_name() As String
		Return Me.permission_name
	End Function

	Public Sub setpermission_description(ByVal value As String)
		Me.permission_description = value
	End Sub
	Public Function getpermission_description() As String
		Return Me.permission_description
	End Function

End Class
