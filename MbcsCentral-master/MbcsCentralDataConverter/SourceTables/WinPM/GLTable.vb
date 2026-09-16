Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class GLTable
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "GLTable"

	Public Const SQL_COL__FROM AS String = "From"
	Public Const SQL_COL__TO AS String = "To"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__FROM & "]," & _
			"[" & SQL_COL__TO & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__FROM & "]," & _
			"[" & SQL_COL__TO & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__FROM & "]" & _
			"[" & SQL_COL__TO & "]" & _
			") VALUES ('?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__FROM & "]='?'" & _
			"[" & SQL_COL__TO & "]='?'" & _
			" WHERE " & 

	Protected m_From As String
	Protected m_To As String

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal From As String,ByVal To As String)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_From = From
		m_To = To
	End Sub

	Protected Function fill(ByVal dr as DataRow) As GLTable

		Try
			m_From = dr(SQL_COL__FROM)
			m_To = dr(SQL_COL__TO)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As GLTable

		Dim obj As GLTable = New GLTable
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As GLTable()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As GLTable = New GLTable

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New GLTable
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(GLTable)), GLTable())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_From, m_To}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_From, m_To}
			 = Me.insertRecord(values)
			If  >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property From As String
		Get
			Return m_From
		End Get
		Set(ByVal value As String)
			m_From = value
		End Set
	End Property

	Public  Property To As String
		Get
			Return m_To
		End Get
		Set(ByVal value As String)
			m_To = value
		End Set
	End Property

End Class
