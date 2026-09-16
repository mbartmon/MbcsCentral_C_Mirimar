Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class HistChg
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "HistChg"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__SEQUENCE__NUMBER AS String = "Sequence Number"
	Public Const SQL_COL__CHARGE__NUMBER AS String = "Charge Number"
	Public Const SQL_COL__DATE AS String = "Date"
	Public Const SQL_COL__CODE AS String = "Code"
	Public Const SQL_COL__AMOUNT AS String = "Amount"
	Public Const SQL_COL__ARREARS AS String = "Arrears"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__CHARGE__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__ARREARS & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__CHARGE__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__ARREARS & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__CHARGE__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__AMOUNT & "]" & _
			"[" & SQL_COL__ARREARS & "]" & _
			") VALUES ('?', '?', ?, ?, #?#, '?', ??);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]=?," & _
			"[" & SQL_COL__CHARGE__NUMBER & "]=?," & _
			"[" & SQL_COL__DATE & "]=#?#," & _
			"[" & SQL_COL__CODE & "]='?'," & _
			"[" & SQL_COL__AMOUNT & "]=?" & _
			"[" & SQL_COL__ARREARS & "]=?" & _
			" WHERE " & 

	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_SequenceNumber As Int32
	Protected m_ChargeNumber As Int32
	Protected m_Date As DateTime
	Protected m_Code As String
	Protected m_Amount As Decimal
	Protected m_Arrears As Decimal

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal SequenceNumber As Int32,ByVal ChargeNumber As Int32,ByVal Date As DateTime,ByVal Code As String,ByVal Amount As Decimal,ByVal Arrears As Decimal)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_SequenceNumber = SequenceNumber
		m_ChargeNumber = ChargeNumber
		m_Date = Date
		m_Code = Code
		m_Amount = Amount
		m_Arrears = Arrears
	End Sub

	Protected Function fill(ByVal dr as DataRow) As HistChg

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_SequenceNumber = dr(SQL_COL__SEQUENCE__NUMBER)
			m_ChargeNumber = dr(SQL_COL__CHARGE__NUMBER)
			m_Date = dr(SQL_COL__DATE)
			m_Code = dr(SQL_COL__CODE)
			m_Amount = dr(SQL_COL__AMOUNT)
			m_Arrears = dr(SQL_COL__ARREARS)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As HistChg

		Dim obj As HistChg = New HistChg
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As HistChg()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As HistChg = New HistChg

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New HistChg
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(HistChg)), HistChg())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_ChargeNumber, m_Date, m_Code, m_Amount, m_Arrears}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_ChargeNumber, m_Date, m_Code, m_Amount, m_Arrears}
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

	Public  Property SequenceNumber As Int32
		Get
			Return m_SequenceNumber
		End Get
		Set(ByVal value As Int32)
			m_SequenceNumber = value
		End Set
	End Property

	Public  Property ChargeNumber As Int32
		Get
			Return m_ChargeNumber
		End Get
		Set(ByVal value As Int32)
			m_ChargeNumber = value
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

	Public  Property Code As String
		Get
			Return m_Code
		End Get
		Set(ByVal value As String)
			m_Code = value
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

	Public  Property Arrears As Decimal
		Get
			Return m_Arrears
		End Get
		Set(ByVal value As Decimal)
			m_Arrears = value
		End Set
	End Property

End Class
