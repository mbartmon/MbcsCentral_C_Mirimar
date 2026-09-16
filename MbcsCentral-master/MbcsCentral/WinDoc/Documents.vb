Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Documents
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Documents"

	Public Const SQL_COL_DOCUMENT_ID AS String = "document_id"
	Public Const SQL_COL_PAGE_NUMBER AS String = "page_number"
	Public Const SQL_COL_FILENAME AS String = "filename"
	Public Const SQL_COL_DESCRIPTION AS String = "description"
	Public Const SQL_COL_GROUP_ID_1 AS String = "group_id_1"
	Public Const SQL_COL_GROUP_ID_2 AS String = "group_id_2"
	Public Const SQL_COL_GROUP_ID_3 AS String = "group_id_3"
	Public Const SQL_COL_BUILDING AS String = "building"
	Public Const SQL_COL_TENANT AS String = "tenant"
	Public Const SQL_COL_TENANT_SEQ AS String = "tenant_seq"
	Public Const SQL_COL_VENDOR AS String = "vendor"
	Public Const SQL_COL_INVOICE AS String = "invoice"
	Public Const SQL_COL_DATE AS String = "date"
	Public Const SQL_COL_DATE_2 AS String = "date_2"
	Public Const SQL_COL_CREATED AS String = "created"
	Public Const SQL_COL_ORIG_DOC_ID AS String = "origDocId"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL_DOCUMENT_ID & "]," & _
			"[" & SQL_COL_PAGE_NUMBER & "]," & _
			"[" & SQL_COL_FILENAME & "]," & _
			"[" & SQL_COL_DESCRIPTION & "]," & _
			"[" & SQL_COL_GROUP_ID_1 & "]," & _
			"[" & SQL_COL_GROUP_ID_2 & "]," & _
			"[" & SQL_COL_GROUP_ID_3 & "]," & _
			"[" & SQL_COL_BUILDING & "]," & _
			"[" & SQL_COL_TENANT & "]," & _
			"[" & SQL_COL_TENANT_SEQ & "]," & _
			"[" & SQL_COL_VENDOR & "]," & _
			"[" & SQL_COL_INVOICE & "]," & _
			"[" & SQL_COL_DATE & "]," & _
			"[" & SQL_COL_DATE_2 & "]," & _
			"[" & SQL_COL_CREATED & "]," & _
			"[" & SQL_COL_ORIG_DOC_ID & "]" & _
			" FROM " & TABLE & _
			" WHERE " & "[" & SQL_COL_DOCUMENT_ID & "] = ?;" AND "[" & SQL_COL_PAGE_NUMBER & "] = ?;"

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL_DOCUMENT_ID & "]," & _
			"[" & SQL_COL_PAGE_NUMBER & "]," & _
			"[" & SQL_COL_FILENAME & "]," & _
			"[" & SQL_COL_DESCRIPTION & "]," & _
			"[" & SQL_COL_GROUP_ID_1 & "]," & _
			"[" & SQL_COL_GROUP_ID_2 & "]," & _
			"[" & SQL_COL_GROUP_ID_3 & "]," & _
			"[" & SQL_COL_BUILDING & "]," & _
			"[" & SQL_COL_TENANT & "]," & _
			"[" & SQL_COL_TENANT_SEQ & "]," & _
			"[" & SQL_COL_VENDOR & "]," & _
			"[" & SQL_COL_INVOICE & "]," & _
			"[" & SQL_COL_DATE & "]," & _
			"[" & SQL_COL_DATE_2 & "]," & _
			"[" & SQL_COL_CREATED & "]," & _
			"[" & SQL_COL_ORIG_DOC_ID & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL_DOCUMENT_ID & "]," & _
			"[" & SQL_COL_PAGE_NUMBER & "]," & _
			"[" & SQL_COL_FILENAME & "]," & _
			"[" & SQL_COL_DESCRIPTION & "]," & _
			"[" & SQL_COL_GROUP_ID_1 & "]," & _
			"[" & SQL_COL_GROUP_ID_2 & "]," & _
			"[" & SQL_COL_GROUP_ID_3 & "]," & _
			"[" & SQL_COL_BUILDING & "]," & _
			"[" & SQL_COL_TENANT & "]," & _
			"[" & SQL_COL_TENANT_SEQ & "]," & _
			"[" & SQL_COL_VENDOR & "]," & _
			"[" & SQL_COL_INVOICE & "]," & _
			"[" & SQL_COL_DATE & "]," & _
			"[" & SQL_COL_DATE_2 & "]," & _
			"[" & SQL_COL_CREATED & "]" & _
			"[" & SQL_COL_ORIG_DOC_ID & "]" & _
			") VALUES (?, ?, '?', '?', ?, ?, ?, '?', '?', ?, '?', '?', #?#, #?#, #?#?);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL_DOCUMENT_ID & "]=?," & _
			"[" & SQL_COL_PAGE_NUMBER & "]=?," & _
			"[" & SQL_COL_FILENAME & "]='?'," & _
			"[" & SQL_COL_DESCRIPTION & "]='?'," & _
			"[" & SQL_COL_GROUP_ID_1 & "]=?," & _
			"[" & SQL_COL_GROUP_ID_2 & "]=?," & _
			"[" & SQL_COL_GROUP_ID_3 & "]=?," & _
			"[" & SQL_COL_BUILDING & "]='?'," & _
			"[" & SQL_COL_TENANT & "]='?'," & _
			"[" & SQL_COL_TENANT_SEQ & "]=?," & _
			"[" & SQL_COL_VENDOR & "]='?'," & _
			"[" & SQL_COL_INVOICE & "]='?'," & _
			"[" & SQL_COL_DATE & "]=#?#," & _
			"[" & SQL_COL_DATE_2 & "]=#?#," & _
			"[" & SQL_COL_CREATED & "]=#?#" & _
			"[" & SQL_COL_ORIG_DOC_ID & "]=?" & _
			" WHERE " & "[" & SQL_COL_DOCUMENT_ID & "] = ?;" AND "[" & SQL_COL_PAGE_NUMBER & "] = ?;"

	Protected m_document_id As Int32
	Protected m_page_number As Int32
	Protected m_filename As String
	Protected m_description As String
	Protected m_group_id_1 As Int32
	Protected m_group_id_2 As Int32
	Protected m_group_id_3 As Int32
	Protected m_building As String
	Protected m_tenant As String
	Protected m_tenant_seq As Int32
	Protected m_vendor As String
	Protected m_invoice As String
	Protected m_date As DateTime
	Protected m_date_2 As DateTime
	Protected m_created As DateTime
	Protected m_origDocId As Int32

	Public Sub New()
		MyBase.New(TABLE, SQL_COL_DOCUMENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal document_id As Int32,ByVal page_number As Int32,ByVal filename As String,ByVal description As String,ByVal group_id_1 As Int32,ByVal group_id_2 As Int32,ByVal group_id_3 As Int32,ByVal building As String,ByVal tenant As String,ByVal tenant_seq As Int32,ByVal vendor As String,ByVal invoice As String,ByVal date As DateTime,ByVal date_2 As DateTime,ByVal created As DateTime,ByVal origDocId As Int32)
		MyBase.New(TABLE, SQL_COL_DOCUMENT_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_document_id = document_id
		m_page_number = page_number
		m_filename = filename
		m_description = description
		m_group_id_1 = group_id_1
		m_group_id_2 = group_id_2
		m_group_id_3 = group_id_3
		m_building = building
		m_tenant = tenant
		m_tenant_seq = tenant_seq
		m_vendor = vendor
		m_invoice = invoice
		m_date = date
		m_date_2 = date_2
		m_created = created
		m_origDocId = origDocId
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Documents

		Try
			m_document_id = dr(SQL_COL_DOCUMENT_ID)
			m_page_number = dr(SQL_COL_PAGE_NUMBER)
			m_filename = dr(SQL_COL_FILENAME)
			m_description = dr(SQL_COL_DESCRIPTION)
			m_group_id_1 = dr(SQL_COL_GROUP_ID_1)
			m_group_id_2 = dr(SQL_COL_GROUP_ID_2)
			m_group_id_3 = dr(SQL_COL_GROUP_ID_3)
			m_building = dr(SQL_COL_BUILDING)
			m_tenant = dr(SQL_COL_TENANT)
			m_tenant_seq = dr(SQL_COL_TENANT_SEQ)
			m_vendor = dr(SQL_COL_VENDOR)
			m_invoice = dr(SQL_COL_INVOICE)
			m_date = dr(SQL_COL_DATE)
			m_date_2 = dr(SQL_COL_DATE_2)
			m_created = dr(SQL_COL_CREATED)
			m_origDocId = dr(SQL_COL_ORIG_DOC_ID)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load(ByVal m_document_id As Int32, ByVal m_page_number As Int32) As Documents

		Dim obj As Documents = New Documents
		Dim opt() as String = {m_document_id, m_page_number}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Documents()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Documents = New Documents

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Documents
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Documents)), Documents())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If m_document_id > 0 Then
			Dim values() as String = {m_document_id, m_page_number, m_filename, m_description, m_group_id_1, m_group_id_2, m_group_id_3, m_building, m_tenant, m_tenant_seq, m_vendor, m_invoice, m_date, m_date_2, m_created, m_origDocId, m_document_id, m_page_number}
			If Me.updateRecord(m_document_id, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_document_id, m_page_number, m_filename, m_description, m_group_id_1, m_group_id_2, m_group_id_3, m_building, m_tenant, m_tenant_seq, m_vendor, m_invoice, m_date, m_date_2, m_created, m_origDocId}
			m_document_id = Me.insertRecord(values)
			If m_document_id >= 0 Then
				Return True
			End If
		End If
		Return False

	End Function

	Private Sub OnPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

	' Property Routines
	Public  Property document_id As Int32
		Get
			Return m_document_id
		End Get
		Set(ByVal value As Int32)
			m_document_id = value
		End Set
	End Property

	Public  Property page_number As Int32
		Get
			Return m_page_number
		End Get
		Set(ByVal value As Int32)
			m_page_number = value
		End Set
	End Property

	Public  Property filename As String
		Get
			Return m_filename
		End Get
		Set(ByVal value As String)
			m_filename = value
		End Set
	End Property

	Public  Property description As String
		Get
			Return m_description
		End Get
		Set(ByVal value As String)
			m_description = value
		End Set
	End Property

	Public  Property group_id_1 As Int32
		Get
			Return m_group_id_1
		End Get
		Set(ByVal value As Int32)
			m_group_id_1 = value
		End Set
	End Property

	Public  Property group_id_2 As Int32
		Get
			Return m_group_id_2
		End Get
		Set(ByVal value As Int32)
			m_group_id_2 = value
		End Set
	End Property

	Public  Property group_id_3 As Int32
		Get
			Return m_group_id_3
		End Get
		Set(ByVal value As Int32)
			m_group_id_3 = value
		End Set
	End Property

	Public  Property building As String
		Get
			Return m_building
		End Get
		Set(ByVal value As String)
			m_building = value
		End Set
	End Property

	Public  Property tenant As String
		Get
			Return m_tenant
		End Get
		Set(ByVal value As String)
			m_tenant = value
		End Set
	End Property

	Public  Property tenant_seq As Int32
		Get
			Return m_tenant_seq
		End Get
		Set(ByVal value As Int32)
			m_tenant_seq = value
		End Set
	End Property

	Public  Property vendor As String
		Get
			Return m_vendor
		End Get
		Set(ByVal value As String)
			m_vendor = value
		End Set
	End Property

	Public  Property invoice As String
		Get
			Return m_invoice
		End Get
		Set(ByVal value As String)
			m_invoice = value
		End Set
	End Property

	Public  Property date As DateTime
		Get
			Return m_date
		End Get
		Set(ByVal value As DateTime)
			m_date = value
		End Set
	End Property

	Public  Property date_2 As DateTime
		Get
			Return m_date_2
		End Get
		Set(ByVal value As DateTime)
			m_date_2 = value
		End Set
	End Property

	Public  Property created As DateTime
		Get
			Return m_created
		End Get
		Set(ByVal value As DateTime)
			m_created = value
		End Set
	End Property

	Public  Property origDocId As Int32
		Get
			Return m_origDocId
		End Get
		Set(ByVal value As Int32)
			m_origDocId = value
		End Set
	End Property

End Class
