Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Codes
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Codes"

	Public Const SQL_COL__CHARGE__CODE AS String = "Charge Code"
	Public Const SQL_COL__CHARGE__DESCRIPTION AS String = "Charge Description"
	Public Const SQL_COL__G_L__CREDIT__ACCOUNT AS String = "GL Credit Account"
	Public Const SQL_COL__G_L__CREDIT__SUB__ACCOUNT AS String = "GL Credit Sub-Account"
	Public Const SQL_COL__G_L__DEBIT__ACCOUNT AS String = "GL Debit Account"
	Public Const SQL_COL__G_L__DEBIT__SUB__ACCOUNT AS String = "GL Debit Sub-Account"
	Public Const SQL_COL__G_L__CASH__ACCOUNT AS String = "GL Cash Account"
	Public Const SQL_COL__GL__CASH__SUB__ACCOUNT AS String = "Gl Cash Sub-Account"
	Public Const SQL_COL__LEGAL__CODE AS String = "Legal Code"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__CHARGE__CODE & "]," & _
			"[" & SQL_COL__CHARGE__DESCRIPTION & "]," & _
			"[" & SQL_COL__G_L__CREDIT__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__CREDIT__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__DEBIT__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__DEBIT__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__CASH__ACCOUNT & "]," & _
			"[" & SQL_COL__GL__CASH__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__LEGAL__CODE & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__CHARGE__CODE & " = '?';"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__CHARGE__CODE & "]," & _
			"[" & SQL_COL__CHARGE__DESCRIPTION & "]," & _
			"[" & SQL_COL__G_L__CREDIT__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__CREDIT__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__DEBIT__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__DEBIT__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__CASH__ACCOUNT & "]," & _
			"[" & SQL_COL__GL__CASH__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__LEGAL__CODE & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__CHARGE__CODE & "]," & _
			"[" & SQL_COL__CHARGE__DESCRIPTION & "]," & _
			"[" & SQL_COL__G_L__CREDIT__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__CREDIT__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__DEBIT__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__DEBIT__SUB__ACCOUNT & "]," & _
			"[" & SQL_COL__G_L__CASH__ACCOUNT & "]," & _
			"[" & SQL_COL__GL__CASH__SUB__ACCOUNT & "]" & _
			"[" & SQL_COL__LEGAL__CODE & "]" & _
			") VALUES ('?', '?', '?', '?', '?', '?', '?', '?'?);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__CHARGE__CODE & "]='?'," & _
			"[" & SQL_COL__CHARGE__DESCRIPTION & "]='?'," & _
			"[" & SQL_COL__G_L__CREDIT__ACCOUNT & "]='?'," & _
			"[" & SQL_COL__G_L__CREDIT__SUB__ACCOUNT & "]='?'," & _
			"[" & SQL_COL__G_L__DEBIT__ACCOUNT & "]='?'," & _
			"[" & SQL_COL__G_L__DEBIT__SUB__ACCOUNT & "]='?'," & _
			"[" & SQL_COL__G_L__CASH__ACCOUNT & "]='?'," & _
			"[" & SQL_COL__GL__CASH__SUB__ACCOUNT & "]='?'" & _
			"[" & SQL_COL__LEGAL__CODE & "]=?" & _
			" WHERE " & SQL_COL__CHARGE__CODE & " = '?';"

	Protected m_ChargeCode As String
	Protected m_ChargeDescription As String
	Protected m_GLCreditAccount As String
	Protected m_GLCreditSubAccount As String
	Protected m_GLDebitAccount As String
	Protected m_GLDebitSubAccount As String
	Protected m_GLCashAccount As String
	Protected m_GlCashSubAccount As String
	Protected m_LegalCode As Boolean

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__CHARGE__CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal ChargeCode As String,ByVal ChargeDescription As String,ByVal GLCreditAccount As String,ByVal GLCreditSubAccount As String,ByVal GLDebitAccount As String,ByVal GLDebitSubAccount As String,ByVal GLCashAccount As String,ByVal GlCashSubAccount As String,ByVal LegalCode As Boolean)
		MyBase.New(TABLE, SQL_COL__CHARGE__CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_ChargeCode = ChargeCode
		m_ChargeDescription = ChargeDescription
		m_GLCreditAccount = GLCreditAccount
		m_GLCreditSubAccount = GLCreditSubAccount
		m_GLDebitAccount = GLDebitAccount
		m_GLDebitSubAccount = GLDebitSubAccount
		m_GLCashAccount = GLCashAccount
		m_GlCashSubAccount = GlCashSubAccount
		m_LegalCode = LegalCode
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Codes

		Try
			m_ChargeCode = dr(SQL_COL__CHARGE__CODE)
			m_ChargeDescription = dr(SQL_COL__CHARGE__DESCRIPTION)
			m_GLCreditAccount = dr(SQL_COL__G_L__CREDIT__ACCOUNT)
			m_GLCreditSubAccount = dr(SQL_COL__G_L__CREDIT__SUB__ACCOUNT)
			m_GLDebitAccount = dr(SQL_COL__G_L__DEBIT__ACCOUNT)
			m_GLDebitSubAccount = dr(SQL_COL__G_L__DEBIT__SUB__ACCOUNT)
			m_GLCashAccount = dr(SQL_COL__G_L__CASH__ACCOUNT)
			m_GlCashSubAccount = dr(SQL_COL__GL__CASH__SUB__ACCOUNT)
			m_LegalCode = dr(SQL_COL__LEGAL__CODE)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_ChargeCode As String) As Codes

		Dim obj As Codes = New Codes
		Dim opt() as String = {m_ChargeCode}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Codes()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Codes = New Codes

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Codes
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Codes)), Codes())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_ChargeCode > 0 Then
			Dim values() as String = {m_ChargeCode, m_ChargeDescription, m_GLCreditAccount, m_GLCreditSubAccount, m_GLDebitAccount, m_GLDebitSubAccount, m_GLCashAccount, m_GlCashSubAccount, m_LegalCode, m_ChargeCode}
			If Me.updateRecord(m_ChargeCode, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_ChargeCode, m_ChargeDescription, m_GLCreditAccount, m_GLCreditSubAccount, m_GLDebitAccount, m_GLDebitSubAccount, m_GLCashAccount, m_GlCashSubAccount, m_LegalCode}
			m_ChargeCode = Me.insertRecord(values)
			If m_ChargeCode >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property ChargeCode As String
		Get
			Return m_ChargeCode
		End Get
		Set(ByVal value As String)
			m_ChargeCode = value
		End Set
	End Property

	Public  Property ChargeDescription As String
		Get
			Return m_ChargeDescription
		End Get
		Set(ByVal value As String)
			m_ChargeDescription = value
		End Set
	End Property

	Public  Property GLCreditAccount As String
		Get
			Return m_GLCreditAccount
		End Get
		Set(ByVal value As String)
			m_GLCreditAccount = value
		End Set
	End Property

	Public  Property GLCreditSubAccount As String
		Get
			Return m_GLCreditSubAccount
		End Get
		Set(ByVal value As String)
			m_GLCreditSubAccount = value
		End Set
	End Property

	Public  Property GLDebitAccount As String
		Get
			Return m_GLDebitAccount
		End Get
		Set(ByVal value As String)
			m_GLDebitAccount = value
		End Set
	End Property

	Public  Property GLDebitSubAccount As String
		Get
			Return m_GLDebitSubAccount
		End Get
		Set(ByVal value As String)
			m_GLDebitSubAccount = value
		End Set
	End Property

	Public  Property GLCashAccount As String
		Get
			Return m_GLCashAccount
		End Get
		Set(ByVal value As String)
			m_GLCashAccount = value
		End Set
	End Property

	Public  Property GlCashSubAccount As String
		Get
			Return m_GlCashSubAccount
		End Get
		Set(ByVal value As String)
			m_GlCashSubAccount = value
		End Set
	End Property

	Public  Property LegalCode As Boolean
		Get
			Return m_LegalCode
		End Get
		Set(ByVal value As Boolean)
			m_LegalCode = value
		End Set
	End Property

End Class
