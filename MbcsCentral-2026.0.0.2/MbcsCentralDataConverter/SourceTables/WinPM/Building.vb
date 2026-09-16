Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Building
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Building"

	Public Const SQL_COL__BUILDING__NUMBER AS String = "Building Number"
	Public Const SQL_COL__OWNER__NUMBER AS String = "Owner Number"
	Public Const SQL_COL__ADDRESS_1 AS String = "Address 1"
	Public Const SQL_COL__ADDRESS_2 AS String = "Address 2"
	Public Const SQL_COL__AGENT__NAME AS String = "Agent Name"
	Public Const SQL_COL__BLOCK__NUMBER AS String = "Block Number"
	Public Const SQL_COL__LOT__NUMBER AS String = "Lot Number"
	Public Const SQL_COL__SECTION__NUMBER AS String = "Section Number"
	Public Const SQL_COL__MULTIPLE__DWELLING__REGISTRATION__NUMBER AS String = "Multiple Dwelling Registration Number"
	Public Const SQL_COL__R_S_A AS String = "RSA"
	Public Const SQL_COL__SENIOR__CITIZEN__NUMBER AS String = "Senior Citizen Number"
	Public Const SQL_COL__SUPERINTENDANT__NAME AS String = "Superintendant Name"
	Public Const SQL_COL__SUPERINTENDANT__PHONE AS String = "Superintendant Phone"
	Public Const SQL_COL__REAL__ESTATE__TAXES AS String = "Real Estate Taxes"
	Public Const SQL_COL__MORTGAGE__INTEREST AS String = "Mortgage Interest"
	Public Const SQL_COL__TOTAL__SHARES AS String = "Total Shares"
	Public Const SQL_COL__MASTER_ACCOUNT AS String = "MasterAccount"
	Public Const SQL_COL__AUTH_REP AS String = "AuthRep"
	Public Const SQL_COL__FAX_NUMBER AS String = "FaxNumber"
	Public Const SQL_COL__AUTH_CODE AS String = "AuthCode"
	Public Const SQL_COL_EMAIL AS String = "email"
	Public Const SQL_COL__RENT_CHANGE_M_C_I AS String = "RentChangeMCI"
	Public Const SQL_COL__RENT_CHANGE_HARDSHIP AS String = "RentChangeHardship"
	Public Const SQL_COL__AGENT_IS AS String = "AgentIs"
	Public Const SQL_COL__D_H_C_R_LOW AS String = "DHCRLow"
	Public Const SQL_COL__D_H_C_R_HIGH AS String = "DHCRHigh"
	Public Const SQL_COL__D_H_C_R_STREET_NAME AS String = "DHCRStreetName"
	Public Const SQL_COL__D_H_C_R_STREET_SUFFIX AS String = "DHCRStreetSuffix"
	Public Const SQL_COL__D_H_C_R_CITY AS String = "DHCRCity"
	Public Const SQL_COL__D_H_C_R_ZIP AS String = "DHCRZip"
	Public Const SQL_COL__D_H_C_R_COUNTY AS String = "DHCRCounty"
	Public Const SQL_COL__D_H_C_R_MUNICIPALITY AS String = "DHCRMunicipality"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__OWNER__NUMBER & "]," & _
			"[" & SQL_COL__ADDRESS_1 & "]," & _
			"[" & SQL_COL__ADDRESS_2 & "]," & _
			"[" & SQL_COL__AGENT__NAME & "]," & _
			"[" & SQL_COL__BLOCK__NUMBER & "]," & _
			"[" & SQL_COL__LOT__NUMBER & "]," & _
			"[" & SQL_COL__SECTION__NUMBER & "]," & _
			"[" & SQL_COL__MULTIPLE__DWELLING__REGISTRATION__NUMBER & "]," & _
			"[" & SQL_COL__R_S_A & "]," & _
			"[" & SQL_COL__SENIOR__CITIZEN__NUMBER & "]," & _
			"[" & SQL_COL__SUPERINTENDANT__NAME & "]," & _
			"[" & SQL_COL__SUPERINTENDANT__PHONE & "]," & _
			"[" & SQL_COL__REAL__ESTATE__TAXES & "]," & _
			"[" & SQL_COL__MORTGAGE__INTEREST & "]," & _
			"[" & SQL_COL__TOTAL__SHARES & "]," & _
			"[" & SQL_COL__MASTER_ACCOUNT & "]," & _
			"[" & SQL_COL__AUTH_REP & "]," & _
			"[" & SQL_COL__FAX_NUMBER & "]," & _
			"[" & SQL_COL__AUTH_CODE & "]," & _
			"[" & SQL_COL_EMAIL & "]," & _
			"[" & SQL_COL__RENT_CHANGE_M_C_I & "]," & _
			"[" & SQL_COL__RENT_CHANGE_HARDSHIP & "]," & _
			"[" & SQL_COL__AGENT_IS & "]," & _
			"[" & SQL_COL__D_H_C_R_LOW & "]," & _
			"[" & SQL_COL__D_H_C_R_HIGH & "]," & _
			"[" & SQL_COL__D_H_C_R_STREET_NAME & "]," & _
			"[" & SQL_COL__D_H_C_R_STREET_SUFFIX & "]," & _
			"[" & SQL_COL__D_H_C_R_CITY & "]," & _
			"[" & SQL_COL__D_H_C_R_ZIP & "]," & _
			"[" & SQL_COL__D_H_C_R_COUNTY & "]," & _
			"[" & SQL_COL__D_H_C_R_MUNICIPALITY & "]" & _
			" FROM " & TABLE & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__OWNER__NUMBER & "]," & _
			"[" & SQL_COL__ADDRESS_1 & "]," & _
			"[" & SQL_COL__ADDRESS_2 & "]," & _
			"[" & SQL_COL__AGENT__NAME & "]," & _
			"[" & SQL_COL__BLOCK__NUMBER & "]," & _
			"[" & SQL_COL__LOT__NUMBER & "]," & _
			"[" & SQL_COL__SECTION__NUMBER & "]," & _
			"[" & SQL_COL__MULTIPLE__DWELLING__REGISTRATION__NUMBER & "]," & _
			"[" & SQL_COL__R_S_A & "]," & _
			"[" & SQL_COL__SENIOR__CITIZEN__NUMBER & "]," & _
			"[" & SQL_COL__SUPERINTENDANT__NAME & "]," & _
			"[" & SQL_COL__SUPERINTENDANT__PHONE & "]," & _
			"[" & SQL_COL__REAL__ESTATE__TAXES & "]," & _
			"[" & SQL_COL__MORTGAGE__INTEREST & "]," & _
			"[" & SQL_COL__TOTAL__SHARES & "]," & _
			"[" & SQL_COL__MASTER_ACCOUNT & "]," & _
			"[" & SQL_COL__AUTH_REP & "]," & _
			"[" & SQL_COL__FAX_NUMBER & "]," & _
			"[" & SQL_COL__AUTH_CODE & "]," & _
			"[" & SQL_COL_EMAIL & "]," & _
			"[" & SQL_COL__RENT_CHANGE_M_C_I & "]," & _
			"[" & SQL_COL__RENT_CHANGE_HARDSHIP & "]," & _
			"[" & SQL_COL__AGENT_IS & "]," & _
			"[" & SQL_COL__D_H_C_R_LOW & "]," & _
			"[" & SQL_COL__D_H_C_R_HIGH & "]," & _
			"[" & SQL_COL__D_H_C_R_STREET_NAME & "]," & _
			"[" & SQL_COL__D_H_C_R_STREET_SUFFIX & "]," & _
			"[" & SQL_COL__D_H_C_R_CITY & "]," & _
			"[" & SQL_COL__D_H_C_R_ZIP & "]," & _
			"[" & SQL_COL__D_H_C_R_COUNTY & "]," & _
			"[" & SQL_COL__D_H_C_R_MUNICIPALITY & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__BUILDING__NUMBER & "]," & _
			"[" & SQL_COL__OWNER__NUMBER & "]," & _
			"[" & SQL_COL__ADDRESS_1 & "]," & _
			"[" & SQL_COL__ADDRESS_2 & "]," & _
			"[" & SQL_COL__AGENT__NAME & "]," & _
			"[" & SQL_COL__BLOCK__NUMBER & "]," & _
			"[" & SQL_COL__LOT__NUMBER & "]," & _
			"[" & SQL_COL__SECTION__NUMBER & "]," & _
			"[" & SQL_COL__MULTIPLE__DWELLING__REGISTRATION__NUMBER & "]," & _
			"[" & SQL_COL__R_S_A & "]," & _
			"[" & SQL_COL__SENIOR__CITIZEN__NUMBER & "]," & _
			"[" & SQL_COL__SUPERINTENDANT__NAME & "]," & _
			"[" & SQL_COL__SUPERINTENDANT__PHONE & "]," & _
			"[" & SQL_COL__REAL__ESTATE__TAXES & "]," & _
			"[" & SQL_COL__MORTGAGE__INTEREST & "]," & _
			"[" & SQL_COL__TOTAL__SHARES & "]," & _
			"[" & SQL_COL__MASTER_ACCOUNT & "]," & _
			"[" & SQL_COL__AUTH_REP & "]," & _
			"[" & SQL_COL__FAX_NUMBER & "]," & _
			"[" & SQL_COL__AUTH_CODE & "]," & _
			"[" & SQL_COL_EMAIL & "]," & _
			"[" & SQL_COL__RENT_CHANGE_M_C_I & "]," & _
			"[" & SQL_COL__RENT_CHANGE_HARDSHIP & "]," & _
			"[" & SQL_COL__AGENT_IS & "]," & _
			"[" & SQL_COL__D_H_C_R_LOW & "]," & _
			"[" & SQL_COL__D_H_C_R_HIGH & "]," & _
			"[" & SQL_COL__D_H_C_R_STREET_NAME & "]," & _
			"[" & SQL_COL__D_H_C_R_STREET_SUFFIX & "]," & _
			"[" & SQL_COL__D_H_C_R_CITY & "]," & _
			"[" & SQL_COL__D_H_C_R_ZIP & "]," & _
			"[" & SQL_COL__D_H_C_R_COUNTY & "]" & _
			"[" & SQL_COL__D_H_C_R_MUNICIPALITY & "]" & _
			") VALUES ('?', ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, ?, ?, '?', '?', '?', '?', '?', ?, ?, ?, '?', '?', '?', '?', '?', '?', ??);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__BUILDING__NUMBER & "]='?'," & _
			"[" & SQL_COL__OWNER__NUMBER & "]=?," & _
			"[" & SQL_COL__ADDRESS_1 & "]='?'," & _
			"[" & SQL_COL__ADDRESS_2 & "]='?'," & _
			"[" & SQL_COL__AGENT__NAME & "]='?'," & _
			"[" & SQL_COL__BLOCK__NUMBER & "]='?'," & _
			"[" & SQL_COL__LOT__NUMBER & "]='?'," & _
			"[" & SQL_COL__SECTION__NUMBER & "]='?'," & _
			"[" & SQL_COL__MULTIPLE__DWELLING__REGISTRATION__NUMBER & "]='?'," & _
			"[" & SQL_COL__R_S_A & "]='?'," & _
			"[" & SQL_COL__SENIOR__CITIZEN__NUMBER & "]='?'," & _
			"[" & SQL_COL__SUPERINTENDANT__NAME & "]='?'," & _
			"[" & SQL_COL__SUPERINTENDANT__PHONE & "]='?'," & _
			"[" & SQL_COL__REAL__ESTATE__TAXES & "]=?," & _
			"[" & SQL_COL__MORTGAGE__INTEREST & "]=?," & _
			"[" & SQL_COL__TOTAL__SHARES & "]=?," & _
			"[" & SQL_COL__MASTER_ACCOUNT & "]='?'," & _
			"[" & SQL_COL__AUTH_REP & "]='?'," & _
			"[" & SQL_COL__FAX_NUMBER & "]='?'," & _
			"[" & SQL_COL__AUTH_CODE & "]='?'," & _
			"[" & SQL_COL_EMAIL & "]='?'," & _
			"[" & SQL_COL__RENT_CHANGE_M_C_I & "]=?," & _
			"[" & SQL_COL__RENT_CHANGE_HARDSHIP & "]=?," & _
			"[" & SQL_COL__AGENT_IS & "]=?," & _
			"[" & SQL_COL__D_H_C_R_LOW & "]='?'," & _
			"[" & SQL_COL__D_H_C_R_HIGH & "]='?'," & _
			"[" & SQL_COL__D_H_C_R_STREET_NAME & "]='?'," & _
			"[" & SQL_COL__D_H_C_R_STREET_SUFFIX & "]='?'," & _
			"[" & SQL_COL__D_H_C_R_CITY & "]='?'," & _
			"[" & SQL_COL__D_H_C_R_ZIP & "]='?'," & _
			"[" & SQL_COL__D_H_C_R_COUNTY & "]=?" & _
			"[" & SQL_COL__D_H_C_R_MUNICIPALITY & "]=?" & _
			" WHERE " & SQL_COL__BUILDING__NUMBER & " = '?';"

	Protected m_BuildingNumber As String
	Protected m_OwnerNumber As Int16
	Protected m_Address1 As String
	Protected m_Address2 As String
	Protected m_AgentName As String
	Protected m_BlockNumber As String
	Protected m_LotNumber As String
	Protected m_SectionNumber As String
	Protected m_MultipleDwellingRegistrationNumber As String
	Protected m_RSA As String
	Protected m_SeniorCitizenNumber As String
	Protected m_SuperintendantName As String
	Protected m_SuperintendantPhone As String
	Protected m_RealEstateTaxes As Double
	Protected m_MortgageInterest As Double
	Protected m_TotalShares As Int32
	Protected m_MasterAccount As String
	Protected m_AuthRep As String
	Protected m_FaxNumber As String
	Protected m_AuthCode As String
	Protected m_email As String
	Protected m_RentChangeMCI As Boolean
	Protected m_RentChangeHardship As Boolean
	Protected m_AgentIs As Int16
	Protected m_DHCRLow As String
	Protected m_DHCRHigh As String
	Protected m_DHCRStreetName As String
	Protected m_DHCRStreetSuffix As String
	Protected m_DHCRCity As String
	Protected m_DHCRZip As String
	Protected m_DHCRCounty As Int16
	Protected m_DHCRMunicipality As Int16

	Public Sub New()
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal BuildingNumber As String,ByVal OwnerNumber As Int16,ByVal Address1 As String,ByVal Address2 As String,ByVal AgentName As String,ByVal BlockNumber As String,ByVal LotNumber As String,ByVal SectionNumber As String,ByVal MultipleDwellingRegistrationNumber As String,ByVal RSA As String,ByVal SeniorCitizenNumber As String,ByVal SuperintendantName As String,ByVal SuperintendantPhone As String,ByVal RealEstateTaxes As Double,ByVal MortgageInterest As Double,ByVal TotalShares As Int32,ByVal MasterAccount As String,ByVal AuthRep As String,ByVal FaxNumber As String,ByVal AuthCode As String,ByVal email As String,ByVal RentChangeMCI As Boolean,ByVal RentChangeHardship As Boolean,ByVal AgentIs As Int16,ByVal DHCRLow As String,ByVal DHCRHigh As String,ByVal DHCRStreetName As String,ByVal DHCRStreetSuffix As String,ByVal DHCRCity As String,ByVal DHCRZip As String,ByVal DHCRCounty As Int16,ByVal DHCRMunicipality As Int16)
		MyBase.New(TABLE, SQL_COL__BUILDING__NUMBER, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_BuildingNumber = BuildingNumber
		m_OwnerNumber = OwnerNumber
		m_Address1 = Address1
		m_Address2 = Address2
		m_AgentName = AgentName
		m_BlockNumber = BlockNumber
		m_LotNumber = LotNumber
		m_SectionNumber = SectionNumber
		m_MultipleDwellingRegistrationNumber = MultipleDwellingRegistrationNumber
		m_RSA = RSA
		m_SeniorCitizenNumber = SeniorCitizenNumber
		m_SuperintendantName = SuperintendantName
		m_SuperintendantPhone = SuperintendantPhone
		m_RealEstateTaxes = RealEstateTaxes
		m_MortgageInterest = MortgageInterest
		m_TotalShares = TotalShares
		m_MasterAccount = MasterAccount
		m_AuthRep = AuthRep
		m_FaxNumber = FaxNumber
		m_AuthCode = AuthCode
		m_email = email
		m_RentChangeMCI = RentChangeMCI
		m_RentChangeHardship = RentChangeHardship
		m_AgentIs = AgentIs
		m_DHCRLow = DHCRLow
		m_DHCRHigh = DHCRHigh
		m_DHCRStreetName = DHCRStreetName
		m_DHCRStreetSuffix = DHCRStreetSuffix
		m_DHCRCity = DHCRCity
		m_DHCRZip = DHCRZip
		m_DHCRCounty = DHCRCounty
		m_DHCRMunicipality = DHCRMunicipality
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Building

		Try
			m_BuildingNumber = dr(SQL_COL__BUILDING__NUMBER)
			m_OwnerNumber = dr(SQL_COL__OWNER__NUMBER)
			m_Address1 = dr(SQL_COL__ADDRESS_1)
			m_Address2 = dr(SQL_COL__ADDRESS_2)
			m_AgentName = dr(SQL_COL__AGENT__NAME)
			m_BlockNumber = dr(SQL_COL__BLOCK__NUMBER)
			m_LotNumber = dr(SQL_COL__LOT__NUMBER)
			m_SectionNumber = dr(SQL_COL__SECTION__NUMBER)
			m_MultipleDwellingRegistrationNumber = dr(SQL_COL__MULTIPLE__DWELLING__REGISTRATION__NUMBER)
			m_RSA = dr(SQL_COL__R_S_A)
			m_SeniorCitizenNumber = dr(SQL_COL__SENIOR__CITIZEN__NUMBER)
			m_SuperintendantName = dr(SQL_COL__SUPERINTENDANT__NAME)
			m_SuperintendantPhone = dr(SQL_COL__SUPERINTENDANT__PHONE)
			m_RealEstateTaxes = dr(SQL_COL__REAL__ESTATE__TAXES)
			m_MortgageInterest = dr(SQL_COL__MORTGAGE__INTEREST)
			m_TotalShares = dr(SQL_COL__TOTAL__SHARES)
			m_MasterAccount = dr(SQL_COL__MASTER_ACCOUNT)
			m_AuthRep = dr(SQL_COL__AUTH_REP)
			m_FaxNumber = dr(SQL_COL__FAX_NUMBER)
			m_AuthCode = dr(SQL_COL__AUTH_CODE)
			m_email = dr(SQL_COL_EMAIL)
			m_RentChangeMCI = dr(SQL_COL__RENT_CHANGE_M_C_I)
			m_RentChangeHardship = dr(SQL_COL__RENT_CHANGE_HARDSHIP)
			m_AgentIs = dr(SQL_COL__AGENT_IS)
			m_DHCRLow = dr(SQL_COL__D_H_C_R_LOW)
			m_DHCRHigh = dr(SQL_COL__D_H_C_R_HIGH)
			m_DHCRStreetName = dr(SQL_COL__D_H_C_R_STREET_NAME)
			m_DHCRStreetSuffix = dr(SQL_COL__D_H_C_R_STREET_SUFFIX)
			m_DHCRCity = dr(SQL_COL__D_H_C_R_CITY)
			m_DHCRZip = dr(SQL_COL__D_H_C_R_ZIP)
			m_DHCRCounty = dr(SQL_COL__D_H_C_R_COUNTY)
			m_DHCRMunicipality = dr(SQL_COL__D_H_C_R_MUNICIPALITY)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_BuildingNumber As String) As Building

		Dim obj As Building = New Building
		Dim opt() as String = {m_BuildingNumber}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Building()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Building = New Building

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Building
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Building)), Building())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_BuildingNumber > 0 Then
			Dim values() as String = {m_BuildingNumber, m_OwnerNumber, m_Address1, m_Address2, m_AgentName, m_BlockNumber, m_LotNumber, m_SectionNumber, m_MultipleDwellingRegistrationNumber, m_RSA, m_SeniorCitizenNumber, m_SuperintendantName, m_SuperintendantPhone, m_RealEstateTaxes, m_MortgageInterest, m_TotalShares, m_MasterAccount, m_AuthRep, m_FaxNumber, m_AuthCode, m_email, m_RentChangeMCI, m_RentChangeHardship, m_AgentIs, m_DHCRLow, m_DHCRHigh, m_DHCRStreetName, m_DHCRStreetSuffix, m_DHCRCity, m_DHCRZip, m_DHCRCounty, m_DHCRMunicipality, m_BuildingNumber}
			If Me.updateRecord(m_BuildingNumber, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_BuildingNumber, m_OwnerNumber, m_Address1, m_Address2, m_AgentName, m_BlockNumber, m_LotNumber, m_SectionNumber, m_MultipleDwellingRegistrationNumber, m_RSA, m_SeniorCitizenNumber, m_SuperintendantName, m_SuperintendantPhone, m_RealEstateTaxes, m_MortgageInterest, m_TotalShares, m_MasterAccount, m_AuthRep, m_FaxNumber, m_AuthCode, m_email, m_RentChangeMCI, m_RentChangeHardship, m_AgentIs, m_DHCRLow, m_DHCRHigh, m_DHCRStreetName, m_DHCRStreetSuffix, m_DHCRCity, m_DHCRZip, m_DHCRCounty, m_DHCRMunicipality}
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

	Public  Property AgentName As String
		Get
			Return m_AgentName
		End Get
		Set(ByVal value As String)
			m_AgentName = value
		End Set
	End Property

	Public  Property BlockNumber As String
		Get
			Return m_BlockNumber
		End Get
		Set(ByVal value As String)
			m_BlockNumber = value
		End Set
	End Property

	Public  Property LotNumber As String
		Get
			Return m_LotNumber
		End Get
		Set(ByVal value As String)
			m_LotNumber = value
		End Set
	End Property

	Public  Property SectionNumber As String
		Get
			Return m_SectionNumber
		End Get
		Set(ByVal value As String)
			m_SectionNumber = value
		End Set
	End Property

	Public  Property MultipleDwellingRegistrationNumber As String
		Get
			Return m_MultipleDwellingRegistrationNumber
		End Get
		Set(ByVal value As String)
			m_MultipleDwellingRegistrationNumber = value
		End Set
	End Property

	Public  Property RSA As String
		Get
			Return m_RSA
		End Get
		Set(ByVal value As String)
			m_RSA = value
		End Set
	End Property

	Public  Property SeniorCitizenNumber As String
		Get
			Return m_SeniorCitizenNumber
		End Get
		Set(ByVal value As String)
			m_SeniorCitizenNumber = value
		End Set
	End Property

	Public  Property SuperintendantName As String
		Get
			Return m_SuperintendantName
		End Get
		Set(ByVal value As String)
			m_SuperintendantName = value
		End Set
	End Property

	Public  Property SuperintendantPhone As String
		Get
			Return m_SuperintendantPhone
		End Get
		Set(ByVal value As String)
			m_SuperintendantPhone = value
		End Set
	End Property

	Public  Property RealEstateTaxes As Double
		Get
			Return m_RealEstateTaxes
		End Get
		Set(ByVal value As Double)
			m_RealEstateTaxes = value
		End Set
	End Property

	Public  Property MortgageInterest As Double
		Get
			Return m_MortgageInterest
		End Get
		Set(ByVal value As Double)
			m_MortgageInterest = value
		End Set
	End Property

	Public  Property TotalShares As Int32
		Get
			Return m_TotalShares
		End Get
		Set(ByVal value As Int32)
			m_TotalShares = value
		End Set
	End Property

	Public  Property MasterAccount As String
		Get
			Return m_MasterAccount
		End Get
		Set(ByVal value As String)
			m_MasterAccount = value
		End Set
	End Property

	Public  Property AuthRep As String
		Get
			Return m_AuthRep
		End Get
		Set(ByVal value As String)
			m_AuthRep = value
		End Set
	End Property

	Public  Property FaxNumber As String
		Get
			Return m_FaxNumber
		End Get
		Set(ByVal value As String)
			m_FaxNumber = value
		End Set
	End Property

	Public  Property AuthCode As String
		Get
			Return m_AuthCode
		End Get
		Set(ByVal value As String)
			m_AuthCode = value
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

	Public  Property RentChangeMCI As Boolean
		Get
			Return m_RentChangeMCI
		End Get
		Set(ByVal value As Boolean)
			m_RentChangeMCI = value
		End Set
	End Property

	Public  Property RentChangeHardship As Boolean
		Get
			Return m_RentChangeHardship
		End Get
		Set(ByVal value As Boolean)
			m_RentChangeHardship = value
		End Set
	End Property

	Public  Property AgentIs As Int16
		Get
			Return m_AgentIs
		End Get
		Set(ByVal value As Int16)
			m_AgentIs = value
		End Set
	End Property

	Public  Property DHCRLow As String
		Get
			Return m_DHCRLow
		End Get
		Set(ByVal value As String)
			m_DHCRLow = value
		End Set
	End Property

	Public  Property DHCRHigh As String
		Get
			Return m_DHCRHigh
		End Get
		Set(ByVal value As String)
			m_DHCRHigh = value
		End Set
	End Property

	Public  Property DHCRStreetName As String
		Get
			Return m_DHCRStreetName
		End Get
		Set(ByVal value As String)
			m_DHCRStreetName = value
		End Set
	End Property

	Public  Property DHCRStreetSuffix As String
		Get
			Return m_DHCRStreetSuffix
		End Get
		Set(ByVal value As String)
			m_DHCRStreetSuffix = value
		End Set
	End Property

	Public  Property DHCRCity As String
		Get
			Return m_DHCRCity
		End Get
		Set(ByVal value As String)
			m_DHCRCity = value
		End Set
	End Property

	Public  Property DHCRZip As String
		Get
			Return m_DHCRZip
		End Get
		Set(ByVal value As String)
			m_DHCRZip = value
		End Set
	End Property

	Public  Property DHCRCounty As Int16
		Get
			Return m_DHCRCounty
		End Get
		Set(ByVal value As Int16)
			m_DHCRCounty = value
		End Set
	End Property

	Public  Property DHCRMunicipality As Int16
		Get
			Return m_DHCRMunicipality
		End Get
		Set(ByVal value As Int16)
			m_DHCRMunicipality = value
		End Set
	End Property

End Class
