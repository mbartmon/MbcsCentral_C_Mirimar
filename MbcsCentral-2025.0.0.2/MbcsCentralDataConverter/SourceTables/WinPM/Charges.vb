Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Charges
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Charges"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__SEQUENCE__NUMBER AS String = "Sequence Number"
	Public Const SQL_COL__CHARGE__CODE AS String = "Charge Code"
	Public Const SQL_COL__TYPE AS String = "Type"
	Public Const SQL_COL__AMOUNT AS String = "Amount"
	Public Const SQL_COL__ARREARS AS String = "Arrears"
	Public Const SQL_COL__PAID AS String = "Paid"
	Public Const SQL_COL__STARTING__DATE AS String = "Starting Date"
	Public Const SQL_COL__ENDING__DATE AS String = "Ending Date"
	Public Const SQL_COL__FUTURE__AMOUNT AS String = "Future Amount"
	Public Const SQL_COL__FUTURE__DATE AS String = "Future Date"
	Public Const SQL_COL__CHARGE__NUMBER AS String = "Charge Number"
	Public Const SQL_COL__HISTORY__DATE AS String = "History Date"
	Public Const SQL_COL__COMMENT AS String = "Comment"
	Public Const SQL_COL__ON_BILL AS String = "OnBill"
	Public Const SQL_COL__CHARGE_TO_TENANT AS String = "ChargeToTenant"
	Public Const SQL_COL__LEGAL_RENT AS String = "LegalRent"
	Public Const SQL_COL__SHOW_ON_BILL AS String = "ShowOnBill"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__CHARGE__CODE & "]," & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__ARREARS & "]," & _
			"[" & SQL_COL__PAID & "]," & _
			"[" & SQL_COL__STARTING__DATE & "]," & _
			"[" & SQL_COL__ENDING__DATE & "]," & _
			"[" & SQL_COL__FUTURE__AMOUNT & "]," & _
			"[" & SQL_COL__FUTURE__DATE & "]," & _
			"[" & SQL_COL__CHARGE__NUMBER & "]," & _
			"[" & SQL_COL__HISTORY__DATE & "]," & _
			"[" & SQL_COL__COMMENT & "]," & _
			"[" & SQL_COL__ON_BILL & "]," & _
			"[" & SQL_COL__CHARGE_TO_TENANT & "]," & _
			"[" & SQL_COL__LEGAL_RENT & "]," & _
			"[" & SQL_COL__SHOW_ON_BILL & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__CHARGE__CODE & "]," & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__ARREARS & "]," & _
			"[" & SQL_COL__PAID & "]," & _
			"[" & SQL_COL__STARTING__DATE & "]," & _
			"[" & SQL_COL__ENDING__DATE & "]," & _
			"[" & SQL_COL__FUTURE__AMOUNT & "]," & _
			"[" & SQL_COL__FUTURE__DATE & "]," & _
			"[" & SQL_COL__CHARGE__NUMBER & "]," & _
			"[" & SQL_COL__HISTORY__DATE & "]," & _
			"[" & SQL_COL__COMMENT & "]," & _
			"[" & SQL_COL__ON_BILL & "]," & _
			"[" & SQL_COL__CHARGE_TO_TENANT & "]," & _
			"[" & SQL_COL__LEGAL_RENT & "]," & _
			"[" & SQL_COL__SHOW_ON_BILL & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__CHARGE__CODE & "]," & _
			"[" & SQL_COL__TYPE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__ARREARS & "]," & _
			"[" & SQL_COL__PAID & "]," & _
			"[" & SQL_COL__STARTING__DATE & "]," & _
			"[" & SQL_COL__ENDING__DATE & "]," & _
			"[" & SQL_COL__FUTURE__AMOUNT & "]," & _
			"[" & SQL_COL__FUTURE__DATE & "]," & _
			"[" & SQL_COL__HISTORY__DATE & "]," & _
			"[" & SQL_COL__COMMENT & "]," & _
			"[" & SQL_COL__ON_BILL & "]," & _
			"[" & SQL_COL__CHARGE_TO_TENANT & "]," & _
			"[" & SQL_COL__LEGAL_RENT & "]," & _
			"[" & SQL_COL__SHOW_ON_BILL & "]" & _
			") VALUES ('?', '?', ?, '?', '?', ?, ?, ?, #?#, #?#, ?, #?#, #?#, '?', ?, ?, ?, ?);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]=?," & _
			"[" & SQL_COL__CHARGE__CODE & "]='?'," & _
			"[" & SQL_COL__TYPE & "]='?'," & _
			"[" & SQL_COL__AMOUNT & "]=?," & _
			"[" & SQL_COL__ARREARS & "]=?," & _
			"[" & SQL_COL__PAID & "]=?," & _
			"[" & SQL_COL__STARTING__DATE & "]=#?#," & _
			"[" & SQL_COL__ENDING__DATE & "]=#?#," & _
			"[" & SQL_COL__FUTURE__AMOUNT & "]=?," & _
			"[" & SQL_COL__FUTURE__DATE & "]=#?#," & _
			"[" & SQL_COL__HISTORY__DATE & "]=#?#," & _
			"[" & SQL_COL__COMMENT & "]='?'," & _
			"[" & SQL_COL__ON_BILL & "]=?," & _
			"[" & SQL_COL__CHARGE_TO_TENANT & "]=?," & _
			"[" & SQL_COL__LEGAL_RENT & "]=?," & _
			"[" & SQL_COL__SHOW_ON_BILL & "]=?" & _
			" WHERE " & 

	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_SequenceNumber As Int32
	Protected m_ChargeCode As String
	Protected m_Type As String
	Protected m_Amount As Decimal
	Protected m_Arrears As Decimal
	Protected m_Paid As Decimal
	Protected m_StartingDate As DateTime
	Protected m_EndingDate As DateTime
	Protected m_FutureAmount As Decimal
	Protected m_FutureDate As DateTime
	Protected m_ChargeNumber As Int32
	Protected m_HistoryDate As DateTime
	Protected m_Comment As String
	Protected m_OnBill As Boolean
	Protected m_ChargeToTenant As Boolean
	Protected m_LegalRent As Boolean
	Protected m_ShowOnBill As Boolean

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal SequenceNumber As Int32,ByVal ChargeCode As String,ByVal Type As String,ByVal Amount As Decimal,ByVal Arrears As Decimal,ByVal Paid As Decimal,ByVal StartingDate As DateTime,ByVal EndingDate As DateTime,ByVal FutureAmount As Decimal,ByVal FutureDate As DateTime,ByVal ChargeNumber As Int32,ByVal HistoryDate As DateTime,ByVal Comment As String,ByVal OnBill As Boolean,ByVal ChargeToTenant As Boolean,ByVal LegalRent As Boolean,ByVal ShowOnBill As Boolean)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_SequenceNumber = SequenceNumber
		m_ChargeCode = ChargeCode
		m_Type = Type
		m_Amount = Amount
		m_Arrears = Arrears
		m_Paid = Paid
		m_StartingDate = StartingDate
		m_EndingDate = EndingDate
		m_FutureAmount = FutureAmount
		m_FutureDate = FutureDate
		m_ChargeNumber = ChargeNumber
		m_HistoryDate = HistoryDate
		m_Comment = Comment
		m_OnBill = OnBill
		m_ChargeToTenant = ChargeToTenant
		m_LegalRent = LegalRent
		m_ShowOnBill = ShowOnBill
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Charges

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_SequenceNumber = dr(SQL_COL__SEQUENCE__NUMBER)
			m_ChargeCode = dr(SQL_COL__CHARGE__CODE)
			m_Type = dr(SQL_COL__TYPE)
			m_Amount = dr(SQL_COL__AMOUNT)
			m_Arrears = dr(SQL_COL__ARREARS)
			m_Paid = dr(SQL_COL__PAID)
			m_StartingDate = dr(SQL_COL__STARTING__DATE)
			m_EndingDate = dr(SQL_COL__ENDING__DATE)
			m_FutureAmount = dr(SQL_COL__FUTURE__AMOUNT)
			m_FutureDate = dr(SQL_COL__FUTURE__DATE)
			m_ChargeNumber = dr(SQL_COL__CHARGE__NUMBER)
			m_HistoryDate = dr(SQL_COL__HISTORY__DATE)
			m_Comment = dr(SQL_COL__COMMENT)
			m_OnBill = dr(SQL_COL__ON_BILL)
			m_ChargeToTenant = dr(SQL_COL__CHARGE_TO_TENANT)
			m_LegalRent = dr(SQL_COL__LEGAL_RENT)
			m_ShowOnBill = dr(SQL_COL__SHOW_ON_BILL)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As Charges

		Dim obj As Charges = New Charges
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Charges()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Charges = New Charges

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Charges
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Charges)), Charges())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_ChargeCode, m_Type, m_Amount, m_Arrears, m_Paid, m_StartingDate, m_EndingDate, m_FutureAmount, m_FutureDate, m_HistoryDate, m_Comment, m_OnBill, m_ChargeToTenant, m_LegalRent, m_ShowOnBill}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_ChargeCode, m_Type, m_Amount, m_Arrears, m_Paid, m_StartingDate, m_EndingDate, m_FutureAmount, m_FutureDate, m_HistoryDate, m_Comment, m_OnBill, m_ChargeToTenant, m_LegalRent, m_ShowOnBill}
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

	Public  Property ChargeCode As String
		Get
			Return m_ChargeCode
		End Get
		Set(ByVal value As String)
			m_ChargeCode = value
		End Set
	End Property

	Public  Property Type As String
		Get
			Return m_Type
		End Get
		Set(ByVal value As String)
			m_Type = value
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

	Public  Property Paid As Decimal
		Get
			Return m_Paid
		End Get
		Set(ByVal value As Decimal)
			m_Paid = value
		End Set
	End Property

	Public  Property StartingDate As DateTime
		Get
			Return m_StartingDate
		End Get
		Set(ByVal value As DateTime)
			m_StartingDate = value
		End Set
	End Property

	Public  Property EndingDate As DateTime
		Get
			Return m_EndingDate
		End Get
		Set(ByVal value As DateTime)
			m_EndingDate = value
		End Set
	End Property

	Public  Property FutureAmount As Decimal
		Get
			Return m_FutureAmount
		End Get
		Set(ByVal value As Decimal)
			m_FutureAmount = value
		End Set
	End Property

	Public  Property FutureDate As DateTime
		Get
			Return m_FutureDate
		End Get
		Set(ByVal value As DateTime)
			m_FutureDate = value
		End Set
	End Property

	Public ReadOnly Property ChargeNumber As Int32
		Get
			Return m_ChargeNumber
		End Get
	End Property

	Public  Property HistoryDate As DateTime
		Get
			Return m_HistoryDate
		End Get
		Set(ByVal value As DateTime)
			m_HistoryDate = value
		End Set
	End Property

	Public  Property Comment As String
		Get
			Return m_Comment
		End Get
		Set(ByVal value As String)
			m_Comment = value
		End Set
	End Property

	Public  Property OnBill As Boolean
		Get
			Return m_OnBill
		End Get
		Set(ByVal value As Boolean)
			m_OnBill = value
		End Set
	End Property

	Public  Property ChargeToTenant As Boolean
		Get
			Return m_ChargeToTenant
		End Get
		Set(ByVal value As Boolean)
			m_ChargeToTenant = value
		End Set
	End Property

	Public  Property LegalRent As Boolean
		Get
			Return m_LegalRent
		End Get
		Set(ByVal value As Boolean)
			m_LegalRent = value
		End Set
	End Property

	Public  Property ShowOnBill As Boolean
		Get
			Return m_ShowOnBill
		End Get
		Set(ByVal value As Boolean)
			m_ShowOnBill = value
		End Set
	End Property

End Class
