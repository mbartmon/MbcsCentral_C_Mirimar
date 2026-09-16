Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class TypeMsg
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "TypeMsg"

	Public Const SQL_COL__TYPE AS String = "Type"
	Public Const SQL_COL__MESSAGE AS String = "Message"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__MESSAGE & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__TYPE & " = '?';"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__MESSAGE & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__TYPE & "]" & _
			"[" & SQL_COL__MESSAGE & "]" & _
			") VALUES ('?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__TYPE & "]='?'" & _
			"[" & SQL_COL__MESSAGE & "]='?'" & _
			" WHERE " & SQL_COL__TYPE & " = '?';"

	Protected m_Type As String
	Protected m_Message As String

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__TYPE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal Type As String,ByVal Message As String)
		MyBase.New(TABLE, SQL_COL__TYPE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_Type = Type
		m_Message = Message
	End Sub

	Protected Function fill(ByVal dr as DataRow) As TypeMsg

		Try
			m_Type = dr(SQL_COL__TYPE)
			m_Message = dr(SQL_COL__MESSAGE)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_Type As String) As TypeMsg

		Dim obj As TypeMsg = New TypeMsg
		Dim opt() as String = {m_Type}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As TypeMsg()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As TypeMsg = New TypeMsg

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New TypeMsg
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(TypeMsg)), TypeMsg())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_Type > 0 Then
			Dim values() as String = {m_Type, m_Message, m_Type}
			If Me.updateRecord(m_Type, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_Type, m_Message}
			m_Type = Me.insertRecord(values)
			If m_Type >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property Type As String
		Get
			Return m_Type
		End Get
		Set(ByVal value As String)
			m_Type = value
		End Set
	End Property

	Public  Property Message As String
		Get
			Return m_Message
		End Get
		Set(ByVal value As String)
			m_Message = value
		End Set
	End Property

End Class
