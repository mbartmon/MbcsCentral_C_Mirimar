Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Payments
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Payments"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__DATE_OF__ENTRY AS String = "Date of Entry"
	Public Const SQL_COL__AMOUNT AS String = "Amount"
	Public Const SQL_COL__SECURITY__CODE AS String = "Security Code"
	Public Const SQL_COL__SEQUENCE__NUMBER AS String = "Sequence Number"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE_OF__ENTRY & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__SECURITY__CODE & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE_OF__ENTRY & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__SECURITY__CODE & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE_OF__ENTRY & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__SECURITY__CODE & "]" & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]" & _
			") VALUES ('?', '?', #?#, ?, '?'?);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__DATE_OF__ENTRY & "]=#?#," & _
			"[" & SQL_COL__AMOUNT & "]=?," & _
			"[" & SQL_COL__SECURITY__CODE & "]='?'" & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]=?" & _
			" WHERE " & 

	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_DateofEntry As DateTime
	Protected m_Amount As Decimal
	Protected m_SecurityCode As String
	Protected m_SequenceNumber As Int32

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal DateofEntry As DateTime,ByVal Amount As Decimal,ByVal SecurityCode As String,ByVal SequenceNumber As Int32)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_DateofEntry = DateofEntry
		m_Amount = Amount
		m_SecurityCode = SecurityCode
		m_SequenceNumber = SequenceNumber
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Payments

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_DateofEntry = dr(SQL_COL__DATE_OF__ENTRY)
			m_Amount = dr(SQL_COL__AMOUNT)
			m_SecurityCode = dr(SQL_COL__SECURITY__CODE)
			m_SequenceNumber = dr(SQL_COL__SEQUENCE__NUMBER)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As Payments

		Dim obj As Payments = New Payments
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Payments()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Payments = New Payments

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Payments
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Payments)), Payments())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_DateofEntry, m_Amount, m_SecurityCode, m_SequenceNumber}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_DateofEntry, m_Amount, m_SecurityCode, m_SequenceNumber}
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

	Public  Property TenantNumber As String
		Get
			Return m_TenantNumber
		End Get
		Set(ByVal value As String)
			m_TenantNumber = value
		End Set
	End Property

	Public  Property DateofEntry As DateTime
		Get
			Return m_DateofEntry
		End Get
		Set(ByVal value As DateTime)
			m_DateofEntry = value
		End Set
	End Property

	Public  Property Amount As Decimal
		Get
			Return m_Amount
		End Get
		Set(ByVal value As Decimal)
			m_Amount = value
		End Set
	End Property

	Public  Property SecurityCode As String
		Get
			Return m_SecurityCode
		End Get
		Set(ByVal value As String)
			m_SecurityCode = value
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

End Class
