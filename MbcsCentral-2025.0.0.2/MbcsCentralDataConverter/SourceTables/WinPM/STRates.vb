Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class STRates
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "STRates"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__DATE AS String = "Date"
	Public Const SQL_COL__RATE AS String = "Rate"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__RATE & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__RATE & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]" & _
			"[" & SQL_COL__RATE & "]" & _
			") VALUES ('?', #?#?);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__DATE & "]=#?#" & _
			"[" & SQL_COL__RATE & "]=?" & _
			" WHERE " & 

	Protected m_BuildingNumber As String
	Protected m_Date As DateTime
	Protected m_Rate As Single

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal Date As DateTime,ByVal Rate As Single)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_Date = Date
		m_Rate = Rate
	End Sub

	Protected Function fill(ByVal dr as DataRow) As STRates

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_Date = dr(SQL_COL__DATE)
			m_Rate = dr(SQL_COL__RATE)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As STRates

		Dim obj As STRates = New STRates
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As STRates()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As STRates = New STRates

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New STRates
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(STRates)), STRates())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_BuildingNumber, m_Date, m_Rate}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_Date, m_Rate}
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
	Public  Property BuildingNumber As String
		Get
			Return m_BuildingNumber
		End Get
		Set(ByVal value As String)
			m_BuildingNumber = value
		End Set
	End Property

	Public  Property Date As DateTime
		Get
			Return m_Date
		End Get
		Set(ByVal value As DateTime)
			m_Date = value
		End Set
	End Property

	Public  Property Rate As Single
		Get
			Return m_Rate
		End Get
		Set(ByVal value As Single)
			m_Rate = value
		End Set
	End Property

End Class
