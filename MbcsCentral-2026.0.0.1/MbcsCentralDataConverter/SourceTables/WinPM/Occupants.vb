Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Occupants
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Occupants"

	Public Const SQL_COL__I_D AS String = "ID"
	Public Const SQL_COL__NAME AS String = "Name"
	Public Const SQL_COL__RELATIONSHIP AS String = "Relationship"
	Public Const SQL_COL__D_O_B AS String = "DOB"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__I_D & "]," & _
			"[" & SQL_COL__NAME & "]," & _
			"[" & SQL_COL__RELATIONSHIP & "]," & _
			"[" & SQL_COL__D_O_B & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__I_D & "]," & _
			"[" & SQL_COL__NAME & "]," & _
			"[" & SQL_COL__RELATIONSHIP & "]," & _
			"[" & SQL_COL__D_O_B & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__I_D & "]," & _
			"[" & SQL_COL__NAME & "]," & _
			"[" & SQL_COL__RELATIONSHIP & "]" & _
			"[" & SQL_COL__D_O_B & "]" & _
			") VALUES (?, '?', '?'#?#);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__I_D & "]=?," & _
			"[" & SQL_COL__NAME & "]='?'," & _
			"[" & SQL_COL__RELATIONSHIP & "]='?'" & _
			"[" & SQL_COL__D_O_B & "]=#?#" & _
			" WHERE " & 

	Protected m_ID As Int32
	Protected m_Name As String
	Protected m_Relationship As String
	Protected m_DOB As DateTime

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal ID As Int32,ByVal Name As String,ByVal Relationship As String,ByVal DOB As DateTime)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_ID = ID
		m_Name = Name
		m_Relationship = Relationship
		m_DOB = DOB
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Occupants

		Try
			m_ID = dr(SQL_COL__I_D)
			m_Name = dr(SQL_COL__NAME)
			m_Relationship = dr(SQL_COL__RELATIONSHIP)
			m_DOB = dr(SQL_COL__D_O_B)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As Occupants

		Dim obj As Occupants = New Occupants
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Occupants()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Occupants = New Occupants

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Occupants
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Occupants)), Occupants())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_ID, m_Name, m_Relationship, m_DOB}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_ID, m_Name, m_Relationship, m_DOB}
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
	Public  Property ID As Int32
		Get
			Return m_ID
		End Get
		Set(ByVal value As Int32)
			m_ID = value
		End Set
	End Property

	Public  Property Name As String
		Get
			Return m_Name
		End Get
		Set(ByVal value As String)
			m_Name = value
		End Set
	End Property

	Public  Property Relationship As String
		Get
			Return m_Relationship
		End Get
		Set(ByVal value As String)
			m_Relationship = value
		End Set
	End Property

	Public  Property DOB As DateTime
		Get
			Return m_DOB
		End Get
		Set(ByVal value As DateTime)
			m_DOB = value
		End Set
	End Property

End Class
