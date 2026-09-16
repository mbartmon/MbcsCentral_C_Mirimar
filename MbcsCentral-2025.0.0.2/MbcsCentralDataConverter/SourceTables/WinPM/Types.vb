Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Types
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Types"

	Public Const SQL_COL__TYPE AS String = "Type"
	Public Const SQL_COL__DESCRIPTION AS String = "Description"
	Public Const SQL_COL__BILLING__NAME AS String = "Billing Name"
	Public Const SQL_COL__PRINT__BILL AS String = "Print Bill"
	Public Const SQL_COL__LEASE AS String = "Lease"
	Public Const SQL_COL__PRINT_APT AS String = "PrintApt"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]," & _
			"[" & SQL_COL__BILLING__NAME & "]," & _
			"[" & SQL_COL__PRINT__BILL & "]," & _
			"[" & SQL_COL__LEASE & "]," & _
			"[" & SQL_COL__PRINT_APT & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__TYPE & " = '?';"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]," & _
			"[" & SQL_COL__BILLING__NAME & "]," & _
			"[" & SQL_COL__PRINT__BILL & "]," & _
			"[" & SQL_COL__LEASE & "]," & _
			"[" & SQL_COL__PRINT_APT & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__DESCRIPTION & "]," & _
			"[" & SQL_COL__BILLING__NAME & "]," & _
			"[" & SQL_COL__PRINT__BILL & "]," & _
			"[" & SQL_COL__LEASE & "]" & _
			"[" & SQL_COL__PRINT_APT & "]" & _
			") VALUES ('?', '?', '?', ?, ??);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__TYPE & "]='?'," & _
			"[" & SQL_COL__DESCRIPTION & "]='?'," & _
			"[" & SQL_COL__BILLING__NAME & "]='?'," & _
			"[" & SQL_COL__PRINT__BILL & "]=?," & _
			"[" & SQL_COL__LEASE & "]=?" & _
			"[" & SQL_COL__PRINT_APT & "]=?" & _
			" WHERE " & SQL_COL__TYPE & " = '?';"

	Protected m_Type As String
	Protected m_Description As String
	Protected m_BillingName As String
	Protected m_PrintBill As Boolean
	Protected m_Lease As Boolean
	Protected m_PrintApt As Boolean

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__TYPE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal Type As String,ByVal Description As String,ByVal BillingName As String,ByVal PrintBill As Boolean,ByVal Lease As Boolean,ByVal PrintApt As Boolean)
		MyBase.New(TABLE, SQL_COL__TYPE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_Type = Type
		m_Description = Description
		m_BillingName = BillingName
		m_PrintBill = PrintBill
		m_Lease = Lease
		m_PrintApt = PrintApt
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Types

		Try
			m_Type = dr(SQL_COL__TYPE)
			m_Description = dr(SQL_COL__DESCRIPTION)
			m_BillingName = dr(SQL_COL__BILLING__NAME)
			m_PrintBill = dr(SQL_COL__PRINT__BILL)
			m_Lease = dr(SQL_COL__LEASE)
			m_PrintApt = dr(SQL_COL__PRINT_APT)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_Type As String) As Types

		Dim obj As Types = New Types
		Dim opt() as String = {m_Type}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Types()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Types = New Types

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Types
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Types)), Types())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_Type > 0 Then
			Dim values() as String = {m_Type, m_Description, m_BillingName, m_PrintBill, m_Lease, m_PrintApt, m_Type}
			If Me.updateRecord(m_Type, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_Type, m_Description, m_BillingName, m_PrintBill, m_Lease, m_PrintApt}
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

	Public  Property Description As String
		Get
			Return m_Description
		End Get
		Set(ByVal value As String)
			m_Description = value
		End Set
	End Property

	Public  Property BillingName As String
		Get
			Return m_BillingName
		End Get
		Set(ByVal value As String)
			m_BillingName = value
		End Set
	End Property

	Public  Property PrintBill As Boolean
		Get
			Return m_PrintBill
		End Get
		Set(ByVal value As Boolean)
			m_PrintBill = value
		End Set
	End Property

	Public  Property Lease As Boolean
		Get
			Return m_Lease
		End Get
		Set(ByVal value As Boolean)
			m_Lease = value
		End Set
	End Property

	Public  Property PrintApt As Boolean
		Get
			Return m_PrintApt
		End Get
		Set(ByVal value As Boolean)
			m_PrintApt = value
		End Set
	End Property

End Class
