Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class STTenants
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "STTenants"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__SEQUENCE__NUMBER AS String = "Sequence Number"
	Public Const SQL_COL__APARTMENT AS String = "Apartment"
	Public Const SQL_COL__TENANT__NAME AS String = "Tenant Name"
	Public Const SQL_COL__AMOUNT AS String = "Amount"
	Public Const SQL_COL__SOCIAL__SECURITY__ONE AS String = "Social Security One"
	Public Const SQL_COL__SOCIAL__SECURITY__TWO AS String = "Social Security Two"
	Public Const SQL_COL__INTEREST AS String = "Interest"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__APARTMENT & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__SOCIAL__SECURITY__ONE & "]," & _
			"[" & SQL_COL__SOCIAL__SECURITY__TWO & "]," & _
			"[" & SQL_COL__INTEREST & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';" AND SQL_COL__TENANT__NUMBER & " = '?';" AND SQL_COL__SEQUENCE__NUMBER & " = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__APARTMENT & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__SOCIAL__SECURITY__ONE & "]," & _
			"[" & SQL_COL__SOCIAL__SECURITY__TWO & "]," & _
			"[" & SQL_COL__INTEREST & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]," & _
			"[" & SQL_COL__APARTMENT & "]," & _
			"[" & SQL_COL__TENANT__NAME & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__SOCIAL__SECURITY__ONE & "]," & _
			"[" & SQL_COL__SOCIAL__SECURITY__TWO & "]" & _
			"[" & SQL_COL__INTEREST & "]" & _
			") VALUES ('?', '?', ?, '?', '?', ?, '?', '?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__SEQUENCE__NUMBER & "]=?," & _
			"[" & SQL_COL__APARTMENT & "]='?'," & _
			"[" & SQL_COL__TENANT__NAME & "]='?'," & _
			"[" & SQL_COL__AMOUNT & "]=?," & _
			"[" & SQL_COL__SOCIAL__SECURITY__ONE & "]='?'," & _
			"[" & SQL_COL__SOCIAL__SECURITY__TWO & "]='?'" & _
			"[" & SQL_COL__INTEREST & "]='?'" & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';" AND SQL_COL__TENANT__NUMBER & " = '?';" AND SQL_COL__SEQUENCE__NUMBER & " = ?;"

	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_SequenceNumber As Int32
	Protected m_Apartment As String
	Protected m_TenantName As String
	Protected m_Amount As Decimal
	Protected m_SocialSecurityOne As String
	Protected m_SocialSecurityTwo As String
	Protected m_Interest As String

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal SequenceNumber As Int32,ByVal Apartment As String,ByVal TenantName As String,ByVal Amount As Decimal,ByVal SocialSecurityOne As String,ByVal SocialSecurityTwo As String,ByVal Interest As String)
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_SequenceNumber = SequenceNumber
		m_Apartment = Apartment
		m_TenantName = TenantName
		m_Amount = Amount
		m_SocialSecurityOne = SocialSecurityOne
		m_SocialSecurityTwo = SocialSecurityTwo
		m_Interest = Interest
	End Sub

	Protected Function fill(ByVal dr as DataRow) As STTenants

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_SequenceNumber = dr(SQL_COL__SEQUENCE__NUMBER)
			m_Apartment = dr(SQL_COL__APARTMENT)
			m_TenantName = dr(SQL_COL__TENANT__NAME)
			m_Amount = dr(SQL_COL__AMOUNT)
			m_SocialSecurityOne = dr(SQL_COL__SOCIAL__SECURITY__ONE)
			m_SocialSecurityTwo = dr(SQL_COL__SOCIAL__SECURITY__TWO)
			m_Interest = dr(SQL_COL__INTEREST)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_BuildingNumber As String, ByVal m_TenantNumber As String, ByVal m_SequenceNumber As Int32) As STTenants

		Dim obj As STTenants = New STTenants
		Dim opt() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As STTenants()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As STTenants = New STTenants

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New STTenants
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(STTenants)), STTenants())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_BuildingNumber > 0 Then
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_Apartment, m_TenantName, m_Amount, m_SocialSecurityOne, m_SocialSecurityTwo, m_Interest, m_BuildingNumber, m_TenantNumber, m_SequenceNumber}
			If Me.updateRecord(m_BuildingNumber, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_TenantNumber, m_SequenceNumber, m_Apartment, m_TenantName, m_Amount, m_SocialSecurityOne, m_SocialSecurityTwo, m_Interest}
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

	Public  Property Apartment As String
		Get
			Return m_Apartment
		End Get
		Set(ByVal value As String)
			m_Apartment = value
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

	Public  Property Amount As Decimal
		Get
			Return m_Amount
		End Get
		Set(ByVal value As Decimal)
			m_Amount = value
		End Set
	End Property

	Public  Property SocialSecurityOne As String
		Get
			Return m_SocialSecurityOne
		End Get
		Set(ByVal value As String)
			m_SocialSecurityOne = value
		End Set
	End Property

	Public  Property SocialSecurityTwo As String
		Get
			Return m_SocialSecurityTwo
		End Get
		Set(ByVal value As String)
			m_SocialSecurityTwo = value
		End Set
	End Property

	Public  Property Interest As String
		Get
			Return m_Interest
		End Get
		Set(ByVal value As String)
			m_Interest = value
		End Set
	End Property

End Class
