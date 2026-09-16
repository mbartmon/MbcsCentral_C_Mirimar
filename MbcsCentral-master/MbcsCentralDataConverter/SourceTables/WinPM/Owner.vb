Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Owner
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Owner"

	Public Const SQL_COL__OWNER__NUMBER AS String = "Owner Number"
	Public Const SQL_COL__ADDRESS_1 AS String = "Address 1"
	Public Const SQL_COL__ADDRESS_2 AS String = "Address 2"
	Public Const SQL_COL__TELEPHONE AS String = "Telephone"
	Public Const SQL_COL__FEDERAL_I_D AS String = "FederalID"
	Public Const SQL_COL__OWNER__NAME AS String = "Owner Name"
	Public Const SQL_COL__BILLING__NAME AS String = "Billing Name"
	Public Const SQL_COL_EMAIL AS String = "email"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__OWNER__NUMBER & "]," & _
			"[" & SQL_COL__ADDRESS_1 & "]," & _
			"[" & SQL_COL__ADDRESS_2 & "]," & _
			"[" & SQL_COL__TELEPHONE & "]," & _
			"[" & SQL_COL__FEDERAL_I_D & "]," & _
			"[" & SQL_COL__OWNER__NAME & "]," & _
			"[" & SQL_COL__BILLING__NAME & "]," & _
			"[" & SQL_COL_EMAIL & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__OWNER__NUMBER & " = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__OWNER__NUMBER & "]," & _
			"[" & SQL_COL__ADDRESS_1 & "]," & _
			"[" & SQL_COL__ADDRESS_2 & "]," & _
			"[" & SQL_COL__TELEPHONE & "]," & _
			"[" & SQL_COL__FEDERAL_I_D & "]," & _
			"[" & SQL_COL__OWNER__NAME & "]," & _
			"[" & SQL_COL__BILLING__NAME & "]," & _
			"[" & SQL_COL_EMAIL & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__OWNER__NUMBER & "]," & _
			"[" & SQL_COL__ADDRESS_1 & "]," & _
			"[" & SQL_COL__ADDRESS_2 & "]," & _
			"[" & SQL_COL__TELEPHONE & "]," & _
			"[" & SQL_COL__FEDERAL_I_D & "]," & _
			"[" & SQL_COL__OWNER__NAME & "]," & _
			"[" & SQL_COL__BILLING__NAME & "]" & _
			"[" & SQL_COL_EMAIL & "]" & _
			") VALUES (?, '?', '?', '?', '?', '?', '?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__OWNER__NUMBER & "]=?," & _
			"[" & SQL_COL__ADDRESS_1 & "]='?'," & _
			"[" & SQL_COL__ADDRESS_2 & "]='?'," & _
			"[" & SQL_COL__TELEPHONE & "]='?'," & _
			"[" & SQL_COL__FEDERAL_I_D & "]='?'," & _
			"[" & SQL_COL__OWNER__NAME & "]='?'," & _
			"[" & SQL_COL__BILLING__NAME & "]='?'" & _
			"[" & SQL_COL_EMAIL & "]='?'" & _
			" WHERE " & SQL_COL__OWNER__NUMBER & " = ?;"

	Protected m_OwnerNumber As Int16
	Protected m_Address1 As String
	Protected m_Address2 As String
	Protected m_Telephone As String
	Protected m_FederalID As String
	Protected m_OwnerName As String
	Protected m_BillingName As String
	Protected m_email As String

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__OWNER__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal OwnerNumber As Int16,ByVal Address1 As String,ByVal Address2 As String,ByVal Telephone As String,ByVal FederalID As String,ByVal OwnerName As String,ByVal BillingName As String,ByVal email As String)
		MyBase.New(TABLE, SQL_COL__OWNER__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_OwnerNumber = OwnerNumber
		m_Address1 = Address1
		m_Address2 = Address2
		m_Telephone = Telephone
		m_FederalID = FederalID
		m_OwnerName = OwnerName
		m_BillingName = BillingName
		m_email = email
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Owner

		Try
			m_OwnerNumber = dr(SQL_COL__OWNER__NUMBER)
			m_Address1 = dr(SQL_COL__ADDRESS_1)
			m_Address2 = dr(SQL_COL__ADDRESS_2)
			m_Telephone = dr(SQL_COL__TELEPHONE)
			m_FederalID = dr(SQL_COL__FEDERAL_I_D)
			m_OwnerName = dr(SQL_COL__OWNER__NAME)
			m_BillingName = dr(SQL_COL__BILLING__NAME)
			m_email = dr(SQL_COL_EMAIL)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_OwnerNumber As Int16) As Owner

		Dim obj As Owner = New Owner
		Dim opt() as String = {m_OwnerNumber}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Owner()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Owner = New Owner

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Owner
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Owner)), Owner())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_OwnerNumber > 0 Then
			Dim values() as String = {m_OwnerNumber, m_Address1, m_Address2, m_Telephone, m_FederalID, m_OwnerName, m_BillingName, m_email, m_OwnerNumber}
			If Me.updateRecord(m_OwnerNumber, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_OwnerNumber, m_Address1, m_Address2, m_Telephone, m_FederalID, m_OwnerName, m_BillingName, m_email}
			m_OwnerNumber = Me.insertRecord(values)
			If m_OwnerNumber >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property OwnerNumber As Int16
		Get
			Return m_OwnerNumber
		End Get
		Set(ByVal value As Int16)
			m_OwnerNumber = value
		End Set
	End Property

	Public  Property Address1 As String
		Get
			Return m_Address1
		End Get
		Set(ByVal value As String)
			m_Address1 = value
		End Set
	End Property

	Public  Property Address2 As String
		Get
			Return m_Address2
		End Get
		Set(ByVal value As String)
			m_Address2 = value
		End Set
	End Property

	Public  Property Telephone As String
		Get
			Return m_Telephone
		End Get
		Set(ByVal value As String)
			m_Telephone = value
		End Set
	End Property

	Public  Property FederalID As String
		Get
			Return m_FederalID
		End Get
		Set(ByVal value As String)
			m_FederalID = value
		End Set
	End Property

	Public  Property OwnerName As String
		Get
			Return m_OwnerName
		End Get
		Set(ByVal value As String)
			m_OwnerName = value
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

	Public  Property email As String
		Get
			Return m_email
		End Get
		Set(ByVal value As String)
			m_email = value
		End Set
	End Property

End Class
