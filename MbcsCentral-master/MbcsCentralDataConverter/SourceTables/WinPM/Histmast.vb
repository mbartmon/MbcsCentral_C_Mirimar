Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Histmast
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Histmast"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__SEQUENCE__NUMBER AS String = "Sequence Number"
	Public Const SQL_COL__TENANT__NAME AS String = "Tenant Name"
	Public Const SQL_COL__APARTMENT AS String = "Apartment"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__APARTMENT & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';" AND SQL_COL__TENANT__NUMBER & " = '?';" AND SQL_COL__SEQUENCE__NUMBER & " = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__APARTMENT & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]" & _
			"[" & SQL_COL__APARTMENT & "]" & _
			") VALUES ('?', '?', ?, '?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]=?," & _
			"[" & SQL_COL__TENANT__NAME & "]='?'" & _
			"[" & SQL_COL__APARTMENT & "]='?'" & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';" AND SQL_COL__TENANT__NUMBER & " = '?';" AND SQL_COL__SEQUENCE__NUMBER & " = ?;"

	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_SequenceNumber As Int32
	Protected m_TenantName As String
	Protected m_Apartment As String

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal SequenceNumber As Int32,ByVal TenantName As String,ByVal Apartment As String)
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_SequenceNumber = SequenceNumber
		m_TenantName = TenantName
		m_Apartment = Apartment
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Histmast

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_SequenceNumber = dr(SQL_COL__SEQUENCE__NUMBER)
			m_TenantName = dr(SQL_COL__TENANT__NAME)
			m_Apartment = dr(SQL_COL__APARTMENT)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_BuildingNumber As String, ByVal m_TenantNumber As String, ByVal m_SequenceNumber As Int32) As Histmast

		Dim obj As Histmast = New Histmast
		Dim opt() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Histmast()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Histmast = New Histmast

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Histmast
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Histmast)), Histmast())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_BuildingNumber > 0 Then
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_TenantName, m_Apartment, m_BuildingNumber, m_TenantNumber, m_SequenceNumber}
			If Me.updateRecord(m_BuildingNumber, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_TenantName, m_Apartment}
			m_BuildingNumber = Me.insertRecord(values)
			If m_BuildingNumber >= 0 Then
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

	Public  Property TenantNumber As String
		Get
			Return m_TenantNumber
		End Get
		Set(ByVal value As String)
			m_TenantNumber = value
		End Set
	End Property

	Public  Property SequenceNumber As Int32
		Get
			Return m_SequenceNumber
		End Get
		Set(ByVal value As Int32)
			m_SequenceNumber = value
		End Set
	End Property

	Public  Property TenantName As String
		Get
			Return m_TenantName
		End Get
		Set(ByVal value As String)
			m_TenantName = value
		End Set
	End Property

	Public  Property Apartment As String
		Get
			Return m_Apartment
		End Get
		Set(ByVal value As String)
			m_Apartment = value
		End Set
	End Property

End Class
