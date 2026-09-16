Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class StatGroups
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "StatGroups"

	Public Const SQL_COL__GROUP__NUMBER AS String = "Group Number"
	Public Const SQL_COL__FROM__GROUP AS String = "From Group"
	Public Const SQL_COL__TO__GROUP AS String = "To Group"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__GROUP__NUMBER & "]," & _
			"[" & SQL_COL__FROM__GROUP & "]," & _
			"[" & SQL_COL__TO__GROUP & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__GROUP__NUMBER & " = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__GROUP__NUMBER & "]," & _
			"[" & SQL_COL__FROM__GROUP & "]," & _
			"[" & SQL_COL__TO__GROUP & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__GROUP__NUMBER & "]," & _
			"[" & SQL_COL__FROM__GROUP & "]" & _
			"[" & SQL_COL__TO__GROUP & "]" & _
			") VALUES (?, '?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__GROUP__NUMBER & "]=?," & _
			"[" & SQL_COL__FROM__GROUP & "]='?'" & _
			"[" & SQL_COL__TO__GROUP & "]='?'" & _
			" WHERE " & SQL_COL__GROUP__NUMBER & " = ?;"

	Protected m_GroupNumber As Byte
	Protected m_FromGroup As String
	Protected m_ToGroup As String

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__GROUP__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal GroupNumber As Byte,ByVal FromGroup As String,ByVal ToGroup As String)
		MyBase.New(TABLE, SQL_COL__GROUP__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_GroupNumber = GroupNumber
		m_FromGroup = FromGroup
		m_ToGroup = ToGroup
	End Sub

	Protected Function fill(ByVal dr as DataRow) As StatGroups

		Try
			m_GroupNumber = dr(SQL_COL__GROUP__NUMBER)
			m_FromGroup = dr(SQL_COL__FROM__GROUP)
			m_ToGroup = dr(SQL_COL__TO__GROUP)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_GroupNumber As Byte) As StatGroups

		Dim obj As StatGroups = New StatGroups
		Dim opt() as String = {m_GroupNumber}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As StatGroups()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As StatGroups = New StatGroups

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New StatGroups
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(StatGroups)), StatGroups())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_GroupNumber > 0 Then
			Dim values() as String = {m_GroupNumber, m_FromGroup, m_ToGroup, m_GroupNumber}
			If Me.updateRecord(m_GroupNumber, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_GroupNumber, m_FromGroup, m_ToGroup}
			m_GroupNumber = Me.insertRecord(values)
			If m_GroupNumber >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property GroupNumber As Byte
		Get
			Return m_GroupNumber
		End Get
		Set(ByVal value As Byte)
			m_GroupNumber = value
		End Set
	End Property

	Public  Property FromGroup As String
		Get
			Return m_FromGroup
		End Get
		Set(ByVal value As String)
			m_FromGroup = value
		End Set
	End Property

	Public  Property ToGroup As String
		Get
			Return m_ToGroup
		End Get
		Set(ByVal value As String)
			m_ToGroup = value
		End Set
	End Property

End Class
