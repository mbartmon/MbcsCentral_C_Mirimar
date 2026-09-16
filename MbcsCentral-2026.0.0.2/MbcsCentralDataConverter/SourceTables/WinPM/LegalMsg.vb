Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class LegalMsg
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "LegalMsg"

	Public Const SQL_COL__CODE AS String = "Code"
	Public Const SQL_COL__MESSAGE AS String = "Message"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__MESSAGE & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__CODE & " = '?';"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__MESSAGE & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__CODE & "]" & _
			"[" & SQL_COL__MESSAGE & "]" & _
			") VALUES ('?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__CODE & "]='?'" & _
			"[" & SQL_COL__MESSAGE & "]='?'" & _
			" WHERE " & SQL_COL__CODE & " = '?';"

	Protected m_Code As String
	Protected m_Message As String

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal Code As String,ByVal Message As String)
		MyBase.New(TABLE, SQL_COL__CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_Code = Code
		m_Message = Message
	End Sub

	Protected Function fill(ByVal dr as DataRow) As LegalMsg

		Try
			m_Code = dr(SQL_COL__CODE)
			m_Message = dr(SQL_COL__MESSAGE)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_Code As String) As LegalMsg

		Dim obj As LegalMsg = New LegalMsg
		Dim opt() as String = {m_Code}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As LegalMsg()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As LegalMsg = New LegalMsg

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New LegalMsg
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(LegalMsg)), LegalMsg())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_Code > 0 Then
			Dim values() as String = {m_Code, m_Message, m_Code}
			If Me.updateRecord(m_Code, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_Code, m_Message}
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

	Public  Property Message As String
		Get
			Return m_Message
		End Get
		Set(ByVal value As String)
			m_Message = value
		End Set
	End Property

End Class
