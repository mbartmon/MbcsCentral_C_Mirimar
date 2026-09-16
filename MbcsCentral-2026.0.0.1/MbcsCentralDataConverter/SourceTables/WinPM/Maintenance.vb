Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Maintenance
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Maintenance"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__DATE AS String = "Date"
	Public Const SQL_COL__LINE__NUMBER AS String = "Line Number"
	Public Const SQL_COL__DESCRIPTION AS String = "Description"
	Public Const SQL_COL__VENDOR AS String = "Vendor"
	Public Const SQL_COL__COST AS String = "Cost"
	Public Const SQL_COL__INSPECTED AS String = "Inspected"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__LINE__NUMBER & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]," & _
			"[" & SQL_COL__VENDOR & "]," & _
			"[" & SQL_COL__COST & "]," & _
			"[" & SQL_COL__INSPECTED & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';" AND SQL_COL__TENANT__NUMBER & " = '?';" AND SQL_COL__DATE & " = ?;" AND SQL_COL__LINE__NUMBER & " = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__LINE__NUMBER & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]," & _
			"[" & SQL_COL__VENDOR & "]," & _
			"[" & SQL_COL__COST & "]," & _
			"[" & SQL_COL__INSPECTED & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]," & _
			"[" & SQL_COL__VENDOR & "]," & _
			"[" & SQL_COL__COST & "]," & _
			"[" & SQL_COL__INSPECTED & "]" & _
			") VALUES ('?', '?', #?#, '?', '?', ?, ?);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__DATE & "]=#?#," & _
			"[" & SQL_COL__DESCRIPTION & "]='?'," & _
			"[" & SQL_COL__VENDOR & "]='?'," & _
			"[" & SQL_COL__COST & "]=?," & _
			"[" & SQL_COL__INSPECTED & "]=?" & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';" AND SQL_COL__TENANT__NUMBER & " = '?';" AND SQL_COL__DATE & " = ?;" AND SQL_COL__LINE__NUMBER & " = ?;"

	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_Date As DateTime
	Protected m_LineNumber As Int32
	Protected m_Description As String
	Protected m_Vendor As String
	Protected m_Cost As Decimal
	Protected m_Inspected As Boolean

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal Date As DateTime,ByVal LineNumber As Int32,ByVal Description As String,ByVal Vendor As String,ByVal Cost As Decimal,ByVal Inspected As Boolean)
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_Date = Date
		m_LineNumber = LineNumber
		m_Description = Description
		m_Vendor = Vendor
		m_Cost = Cost
		m_Inspected = Inspected
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Maintenance

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_Date = dr(SQL_COL__DATE)
			m_LineNumber = dr(SQL_COL__LINE__NUMBER)
			m_Description = dr(SQL_COL__DESCRIPTION)
			m_Vendor = dr(SQL_COL__VENDOR)
			m_Cost = dr(SQL_COL__COST)
			m_Inspected = dr(SQL_COL__INSPECTED)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_BuildingNumber As String, ByVal m_TenantNumber As String, ByVal m_Date As DateTime, ByVal m_LineNumber As Int32) As Maintenance

		Dim obj As Maintenance = New Maintenance
		Dim opt() as String = {m_BuildingNumber, m_TenantNumber, m_Date, m_LineNumber}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Maintenance()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Maintenance = New Maintenance

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Maintenance
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Maintenance)), Maintenance())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_BuildingNumber > 0 Then
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_Date, m_Description, m_Vendor, m_Cost, m_Inspected, m_BuildingNumber, m_TenantNumber, m_Date, m_LineNumber}
			If Me.updateRecord(m_BuildingNumber, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_Date, m_Description, m_Vendor, m_Cost, m_Inspected}
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

	Public  Property Date As DateTime
		Get
			Return m_Date
		End Get
		Set(ByVal value As DateTime)
			m_Date = value
		End Set
	End Property

	Public ReadOnly Property LineNumber As Int32
		Get
			Return m_LineNumber
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

	Public  Property Vendor As String
		Get
			Return m_Vendor
		End Get
		Set(ByVal value As String)
			m_Vendor = value
		End Set
	End Property

	Public  Property Cost As Decimal
		Get
			Return m_Cost
		End Get
		Set(ByVal value As Decimal)
			m_Cost = value
		End Set
	End Property

	Public  Property Inspected As Boolean
		Get
			Return m_Inspected
		End Get
		Set(ByVal value As Boolean)
			m_Inspected = value
		End Set
	End Property

End Class
