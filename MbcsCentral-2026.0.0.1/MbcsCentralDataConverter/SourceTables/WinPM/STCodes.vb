Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class STCodes
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "STCodes"

	Public Const SQL_COL__CODE AS String = "Code"
	Public Const SQL_COL__KEY AS String = "Key"
	Public Const SQL_COL__DESCRIPTION AS String = "Description"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__KEY & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__CODE & " = '?';"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__KEY & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]" & _
			") VALUES ('?', '?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__CODE & "]='?'," & _
			"[" & SQL_COL__DESCRIPTION & "]='?'" & _
			" WHERE " & SQL_COL__CODE & " = '?';"

	Protected m_Code As String
	Protected m_Key As Int32
	Protected m_Description As String

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal Code As String,ByVal Key As Int32,ByVal Description As String)
		MyBase.New(TABLE, SQL_COL__CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_Code = Code
		m_Key = Key
		m_Description = Description
	End Sub

	Protected Function fill(ByVal dr as DataRow) As STCodes

		Try
			m_Code = dr(SQL_COL__CODE)
			m_Key = dr(SQL_COL__KEY)
			m_Description = dr(SQL_COL__DESCRIPTION)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_Code As String) As STCodes

		Dim obj As STCodes = New STCodes
		Dim opt() as String = {m_Code}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As STCodes()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As STCodes = New STCodes

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New STCodes
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(STCodes)), STCodes())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_Code > 0 Then
			Dim values() as String = {m_Code, m_Description, m_Code}
			If Me.updateRecord(m_Code, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_Code, m_Description}
			m_Code = Me.insertRecord(values)
			If m_Code >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property Code As String
		Get
			Return m_Code
		End Get
		Set(ByVal value As String)
			m_Code = value
		End Set
	End Property

	Public ReadOnly Property Key As Int32
		Get
			Return m_Key
		End Get
	End Property

	Public  Property Description As String
		Get
			Return m_Description
		End Get
		Set(ByVal value As String)
			m_Description = value
		End Set
	End Property

End Class
