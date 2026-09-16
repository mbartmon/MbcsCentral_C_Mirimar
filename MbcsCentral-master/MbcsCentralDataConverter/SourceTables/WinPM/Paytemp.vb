Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Paytemp
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Paytemp"

	Public Const SQL_COL__USER_I_D AS String = "UserID"
	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__TENANT__NUMBER AS String = "Tenant Number"
	Public Const SQL_COL__DATE AS String = "Date"
	Public Const SQL_COL__AMOUNT AS String = "Amount"
	Public Const SQL_COL__CODE AS String = "Code"
	Public Const SQL_COL__NAME AS String = "Name"
	Public Const SQL_COL__APARTMENT AS String = "Apartment"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__USER_I_D & "]," & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__NAME & "]," & _
			"[" & SQL_COL__APARTMENT & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__USER_I_D & "]," & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__NAME & "]," & _
			"[" & SQL_COL__APARTMENT & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__USER_I_D & "]," & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__TENANT__NUMBER & "]," & _
			"[" & SQL_COL__DATE & "]," & _
			"[" & SQL_COL__AMOUNT & "]," & _
			"[" & SQL_COL__CODE & "]," & _
			"[" & SQL_COL__NAME & "]" & _
			"[" & SQL_COL__APARTMENT & "]" & _
			") VALUES ('?', '?', '?', #?#, ?, '?', '?''?');"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__USER_I_D & "]='?'," & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__TENANT__NUMBER & "]='?'," & _
			"[" & SQL_COL__DATE & "]=#?#," & _
			"[" & SQL_COL__AMOUNT & "]=?," & _
			"[" & SQL_COL__CODE & "]='?'," & _
			"[" & SQL_COL__NAME & "]='?'" & _
			"[" & SQL_COL__APARTMENT & "]='?'" & _
			" WHERE " & 

	Protected m_UserID As String
	Protected m_BuildingNumber As String
	Protected m_TenantNumber As String
	Protected m_Date As DateTime
	Protected m_Amount As Decimal
	Protected m_Code As String
	Protected m_Name As String
	Protected m_Apartment As String

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal UserID As String,ByVal BuildingNumber As String,ByVal TenantNumber As String,ByVal Date As DateTime,ByVal Amount As Decimal,ByVal Code As String,ByVal Name As String,ByVal Apartment As String)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_UserID = UserID
		m_BuildingNumber = BuildingNumber
		m_TenantNumber = TenantNumber
		m_Date = Date
		m_Amount = Amount
		m_Code = Code
		m_Name = Name
		m_Apartment = Apartment
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Paytemp

		Try
			m_UserID = dr(SQL_COL__USER_I_D)
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_TenantNumber = dr(SQL_COL__TENANT__NUMBER)
			m_Date = dr(SQL_COL__DATE)
			m_Amount = dr(SQL_COL__AMOUNT)
			m_Code = dr(SQL_COL__CODE)
			m_Name = dr(SQL_COL__NAME)
			m_Apartment = dr(SQL_COL__APARTMENT)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As Paytemp

		Dim obj As Paytemp = New Paytemp
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Paytemp()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Paytemp = New Paytemp

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Paytemp
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Paytemp)), Paytemp())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_UserID, m_BuildingNumber, m_TenantNumber, m_Date, m_Amount, m_Code, m_Name, m_Apartment}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_UserID, m_BuildingNumber, m_TenantNumber, m_Date, m_Amount, m_Code, m_Name, m_Apartment}
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
	Public  Property UserID As String
		Get
			Return m_UserID
		End Get
		Set(ByVal value As String)
			m_UserID = value
		End Set
	End Property

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

	Public  Property Amount As Decimal
		Get
			Return m_Amount
		End Get
		Set(ByVal value As Decimal)
			m_Amount = value
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

	Public  Property Name As String
		Get
			Return m_Name
		End Get
		Set(ByVal value As String)
			m_Name = value
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
