Imports System.Data
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Legal
	Inherits clsDB
	Implements INotifyPropertyChanged

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Shared TABLE As String = "Legal"

	Public Const SQL_COL__I_D AS String = "ID"
	Public Const SQL_COL__YEAR AS String = "Year"
	Public Const SQL_COL__AN_W_G_CHILDREN AS String = "AnWGChildren"
	Public Const SQL_COL__AN_W_G_NO_CHILDREN AS String = "AnWGNoChildren"
	Public Const SQL_COL__AN_W_G_WANT AS String = "AnWGWant"
	Public Const SQL_COL__AN_W_G_INSTALLED AS String = "AnWGInstalled"
	Public Const SQL_COL__AN_W_G_NOT_INSTALLED AS String = "AnWGNotInstalled"
	Public Const SQL_COL__AN_W_G_MAINTENANCE AS String = "AnWGMaintenance"
	Public Const SQL_COL__AN_W_G_NO_MAINTENANCE AS String = "AnWGNoMaintenance"
	Public Const SQL_COL__RN_W_G_CHILDREN AS String = "RnWGChildren"
	Public Const SQL_COL__RN_W_G_NO_CHILDREN AS String = "RnWGNoChildren"
	Public Const SQL_COL__RN_W_G_WANT AS String = "RnWGWant"
	Public Const SQL_COL__AN_L_P_CHILDREN AS String = "AnLPChildren"
	Public Const SQL_COL__RN_L_P_CHILDREN AS String = "RnLPChildren"
	Public Const SQL_COL__AN_L_P_CHILDREN_NO AS String = "AnLPChildrenNo"
	Public Const SQL_COL__RN_L_P_CHILDREN_NO AS String = "RnLPChildrenNo"

	Shared SQL_LOAD As String = " SELECT " & _
			"[" & SQL_COL__I_D & "]," & _
			"[" & SQL_COL__YEAR & "]," & _
			"[" & SQL_COL__AN_W_G_CHILDREN & "]," & _
			"[" & SQL_COL__AN_W_G_NO_CHILDREN & "]," & _
			"[" & SQL_COL__AN_W_G_WANT & "]," & _
			"[" & SQL_COL__AN_W_G_INSTALLED & "]," & _
			"[" & SQL_COL__AN_W_G_NOT_INSTALLED & "]," & _
			"[" & SQL_COL__AN_W_G_MAINTENANCE & "]," & _
			"[" & SQL_COL__AN_W_G_NO_MAINTENANCE & "]," & _
			"[" & SQL_COL__RN_W_G_CHILDREN & "]," & _
			"[" & SQL_COL__RN_W_G_NO_CHILDREN & "]," & _
			"[" & SQL_COL__RN_W_G_WANT & "]," & _
			"[" & SQL_COL__AN_L_P_CHILDREN & "]," & _
			"[" & SQL_COL__RN_L_P_CHILDREN & "]," & _
			"[" & SQL_COL__AN_L_P_CHILDREN_NO & "]," & _
			"[" & SQL_COL__RN_L_P_CHILDREN_NO & "]" & _
			" FROM " & TABLE & _
			" WHERE " & 

	Shared SQL_LOAD_ALL As String = " SELECT " & _
			"[" & SQL_COL__I_D & "]," & _
			"[" & SQL_COL__YEAR & "]," & _
			"[" & SQL_COL__AN_W_G_CHILDREN & "]," & _
			"[" & SQL_COL__AN_W_G_NO_CHILDREN & "]," & _
			"[" & SQL_COL__AN_W_G_WANT & "]," & _
			"[" & SQL_COL__AN_W_G_INSTALLED & "]," & _
			"[" & SQL_COL__AN_W_G_NOT_INSTALLED & "]," & _
			"[" & SQL_COL__AN_W_G_MAINTENANCE & "]," & _
			"[" & SQL_COL__AN_W_G_NO_MAINTENANCE & "]," & _
			"[" & SQL_COL__RN_W_G_CHILDREN & "]," & _
			"[" & SQL_COL__RN_W_G_NO_CHILDREN & "]," & _
			"[" & SQL_COL__RN_W_G_WANT & "]," & _
			"[" & SQL_COL__AN_L_P_CHILDREN & "]," & _
			"[" & SQL_COL__RN_L_P_CHILDREN & "]," & _
			"[" & SQL_COL__AN_L_P_CHILDREN_NO & "]," & _
			"[" & SQL_COL__RN_L_P_CHILDREN_NO & "]" & _
			" FROM " & TABLE & ";"

	Shared SQL_INSERT As String = " INSERT INTO " & TABLE & " (" & _
			"[" & SQL_COL__I_D & "]," & _
			"[" & SQL_COL__YEAR & "]," & _
			"[" & SQL_COL__AN_W_G_CHILDREN & "]," & _
			"[" & SQL_COL__AN_W_G_NO_CHILDREN & "]," & _
			"[" & SQL_COL__AN_W_G_WANT & "]," & _
			"[" & SQL_COL__AN_W_G_INSTALLED & "]," & _
			"[" & SQL_COL__AN_W_G_NOT_INSTALLED & "]," & _
			"[" & SQL_COL__AN_W_G_MAINTENANCE & "]," & _
			"[" & SQL_COL__AN_W_G_NO_MAINTENANCE & "]," & _
			"[" & SQL_COL__RN_W_G_CHILDREN & "]," & _
			"[" & SQL_COL__RN_W_G_NO_CHILDREN & "]," & _
			"[" & SQL_COL__RN_W_G_WANT & "]," & _
			"[" & SQL_COL__AN_L_P_CHILDREN & "]," & _
			"[" & SQL_COL__RN_L_P_CHILDREN & "]," & _
			"[" & SQL_COL__AN_L_P_CHILDREN_NO & "]" & _
			"[" & SQL_COL__RN_L_P_CHILDREN_NO & "]" & _
			") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ??);"

	Shared SQL_UPDATE As String = " UPDATE " & TABLE & " SET " & _
			"[" & SQL_COL__I_D & "]=?," & _
			"[" & SQL_COL__YEAR & "]=?," & _
			"[" & SQL_COL__AN_W_G_CHILDREN & "]=?," & _
			"[" & SQL_COL__AN_W_G_NO_CHILDREN & "]=?," & _
			"[" & SQL_COL__AN_W_G_WANT & "]=?," & _
			"[" & SQL_COL__AN_W_G_INSTALLED & "]=?," & _
			"[" & SQL_COL__AN_W_G_NOT_INSTALLED & "]=?," & _
			"[" & SQL_COL__AN_W_G_MAINTENANCE & "]=?," & _
			"[" & SQL_COL__AN_W_G_NO_MAINTENANCE & "]=?," & _
			"[" & SQL_COL__RN_W_G_CHILDREN & "]=?," & _
			"[" & SQL_COL__RN_W_G_NO_CHILDREN & "]=?," & _
			"[" & SQL_COL__RN_W_G_WANT & "]=?," & _
			"[" & SQL_COL__AN_L_P_CHILDREN & "]=?," & _
			"[" & SQL_COL__RN_L_P_CHILDREN & "]=?," & _
			"[" & SQL_COL__AN_L_P_CHILDREN_NO & "]=?" & _
			"[" & SQL_COL__RN_L_P_CHILDREN_NO & "]=?" & _
			" WHERE " & 

	Protected m_ID As Int32
	Protected m_Year As Int16
	Protected m_AnWGChildren As Boolean
	Protected m_AnWGNoChildren As Boolean
	Protected m_AnWGWant As Boolean
	Protected m_AnWGInstalled As Boolean
	Protected m_AnWGNotInstalled As Boolean
	Protected m_AnWGMaintenance As Boolean
	Protected m_AnWGNoMaintenance As Boolean
	Protected m_RnWGChildren As Boolean
	Protected m_RnWGNoChildren As Boolean
	Protected m_RnWGWant As Boolean
	Protected m_AnLPChildren As Boolean
	Protected m_RnLPChildren As Boolean
	Protected m_AnLPChildrenNo As Boolean
	Protected m_RnLPChildrenNo As Boolean

	Public Sub New()
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	End Sub

	Public Sub New(ByVal ID As Int32,ByVal Year As Int16,ByVal AnWGChildren As Boolean,ByVal AnWGNoChildren As Boolean,ByVal AnWGWant As Boolean,ByVal AnWGInstalled As Boolean,ByVal AnWGNotInstalled As Boolean,ByVal AnWGMaintenance As Boolean,ByVal AnWGNoMaintenance As Boolean,ByVal RnWGChildren As Boolean,ByVal RnWGNoChildren As Boolean,ByVal RnWGWant As Boolean,ByVal AnLPChildren As Boolean,ByVal RnLPChildren As Boolean,ByVal AnLPChildrenNo As Boolean,ByVal RnLPChildrenNo As Boolean)
		MyBase.New(TABLE, , SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
		m_ID = ID
		m_Year = Year
		m_AnWGChildren = AnWGChildren
		m_AnWGNoChildren = AnWGNoChildren
		m_AnWGWant = AnWGWant
		m_AnWGInstalled = AnWGInstalled
		m_AnWGNotInstalled = AnWGNotInstalled
		m_AnWGMaintenance = AnWGMaintenance
		m_AnWGNoMaintenance = AnWGNoMaintenance
		m_RnWGChildren = RnWGChildren
		m_RnWGNoChildren = RnWGNoChildren
		m_RnWGWant = RnWGWant
		m_AnLPChildren = AnLPChildren
		m_RnLPChildren = RnLPChildren
		m_AnLPChildrenNo = AnLPChildrenNo
		m_RnLPChildrenNo = RnLPChildrenNo
	End Sub

	Protected Function fill(ByVal dr as DataRow) As Legal

		Try
			m_ID = dr(SQL_COL__I_D)
			m_Year = dr(SQL_COL__YEAR)
			m_AnWGChildren = dr(SQL_COL__AN_W_G_CHILDREN)
			m_AnWGNoChildren = dr(SQL_COL__AN_W_G_NO_CHILDREN)
			m_AnWGWant = dr(SQL_COL__AN_W_G_WANT)
			m_AnWGInstalled = dr(SQL_COL__AN_W_G_INSTALLED)
			m_AnWGNotInstalled = dr(SQL_COL__AN_W_G_NOT_INSTALLED)
			m_AnWGMaintenance = dr(SQL_COL__AN_W_G_MAINTENANCE)
			m_AnWGNoMaintenance = dr(SQL_COL__AN_W_G_NO_MAINTENANCE)
			m_RnWGChildren = dr(SQL_COL__RN_W_G_CHILDREN)
			m_RnWGNoChildren = dr(SQL_COL__RN_W_G_NO_CHILDREN)
			m_RnWGWant = dr(SQL_COL__RN_W_G_WANT)
			m_AnLPChildren = dr(SQL_COL__AN_L_P_CHILDREN)
			m_RnLPChildren = dr(SQL_COL__RN_L_P_CHILDREN)
			m_AnLPChildrenNo = dr(SQL_COL__AN_L_P_CHILDREN_NO)
			m_RnLPChildrenNo = dr(SQL_COL__RN_L_P_CHILDREN_NO)
		Catch ex As OleDbException
		End Try
		Return Me

	End Function

	Public Overloads Shared Function load() As Legal

		Dim obj As Legal = New Legal
		Dim opt() as String = {}
		Dim ds As DataSet = New DataSet
		ds = obj.load(SQL_LOAD, opt)
		Dim dr as DataRow = ds.Tables(0).Rows(0)

		Return obj.fill(dr)

	End Function

	Public Overloads Shared Function load() As Legal()

		Dim list as ArrayList = New ArrayList()

		Dim ds As DataSet = New DataSet
		Dim obj As Legal = New Legal

		ds = obj.loadAll()
		obj = Nothing
		Dim dr as DataRow
		For Each dr in ds.Tables(0).Rows
			obj = New Legal
			obj.fill(dr)
			list.Add(obj)
			obj = Nothing
		Next
		Try
			Return DirectCast(list.ToArray(GetType(Legal)), Legal())
		Catch ex As OleDbException
			Return Nothing
		End Try

	End Function

	Public Function save() As Boolean

		If  > 0 Then
			Dim values() as String = {m_ID, m_Year, m_AnWGChildren, m_AnWGNoChildren, m_AnWGWant, m_AnWGInstalled, m_AnWGNotInstalled, m_AnWGMaintenance, m_AnWGNoMaintenance, m_RnWGChildren, m_RnWGNoChildren, m_RnWGWant, m_AnLPChildren, m_RnLPChildren, m_AnLPChildrenNo, m_RnLPChildrenNo}
			If Me.updateRecord(, values) Then
				Return True
			Else
				Return False
			End If
		Else
			Dim values() as String = {m_ID, m_Year, m_AnWGChildren, m_AnWGNoChildren, m_AnWGWant, m_AnWGInstalled, m_AnWGNotInstalled, m_AnWGMaintenance, m_AnWGNoMaintenance, m_RnWGChildren, m_RnWGNoChildren, m_RnWGWant, m_AnLPChildren, m_RnLPChildren, m_AnLPChildrenNo, m_RnLPChildrenNo}
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
	Public  Property ID As Int32
		Get
			Return m_ID
		End Get
		Set(ByVal value As Int32)
			m_ID = value
		End Set
	End Property

	Public  Property Year As Int16
		Get
			Return m_Year
		End Get
		Set(ByVal value As Int16)
			m_Year = value
		End Set
	End Property

	Public  Property AnWGChildren As Boolean
		Get
			Return m_AnWGChildren
		End Get
		Set(ByVal value As Boolean)
			m_AnWGChildren = value
		End Set
	End Property

	Public  Property AnWGNoChildren As Boolean
		Get
			Return m_AnWGNoChildren
		End Get
		Set(ByVal value As Boolean)
			m_AnWGNoChildren = value
		End Set
	End Property

	Public  Property AnWGWant As Boolean
		Get
			Return m_AnWGWant
		End Get
		Set(ByVal value As Boolean)
			m_AnWGWant = value
		End Set
	End Property

	Public  Property AnWGInstalled As Boolean
		Get
			Return m_AnWGInstalled
		End Get
		Set(ByVal value As Boolean)
			m_AnWGInstalled = value
		End Set
	End Property

	Public  Property AnWGNotInstalled As Boolean
		Get
			Return m_AnWGNotInstalled
		End Get
		Set(ByVal value As Boolean)
			m_AnWGNotInstalled = value
		End Set
	End Property

	Public  Property AnWGMaintenance As Boolean
		Get
			Return m_AnWGMaintenance
		End Get
		Set(ByVal value As Boolean)
			m_AnWGMaintenance = value
		End Set
	End Property

	Public  Property AnWGNoMaintenance As Boolean
		Get
			Return m_AnWGNoMaintenance
		End Get
		Set(ByVal value As Boolean)
			m_AnWGNoMaintenance = value
		End Set
	End Property

	Public  Property RnWGChildren As Boolean
		Get
			Return m_RnWGChildren
		End Get
		Set(ByVal value As Boolean)
			m_RnWGChildren = value
		End Set
	End Property

	Public  Property RnWGNoChildren As Boolean
		Get
			Return m_RnWGNoChildren
		End Get
		Set(ByVal value As Boolean)
			m_RnWGNoChildren = value
		End Set
	End Property

	Public  Property RnWGWant As Boolean
		Get
			Return m_RnWGWant
		End Get
		Set(ByVal value As Boolean)
			m_RnWGWant = value
		End Set
	End Property

	Public  Property AnLPChildren As Boolean
		Get
			Return m_AnLPChildren
		End Get
		Set(ByVal value As Boolean)
			m_AnLPChildren = value
		End Set
	End Property

	Public  Property RnLPChildren As Boolean
		Get
			Return m_RnLPChildren
		End Get
		Set(ByVal value As Boolean)
			m_RnLPChildren = value
		End Set
	End Property

	Public  Property AnLPChildrenNo As Boolean
		Get
			Return m_AnLPChildrenNo
		End Get
		Set(ByVal value As Boolean)
			m_AnLPChildrenNo = value
		End Set
	End Property

	Public  Property RnLPChildrenNo As Boolean
		Get
			Return m_RnLPChildrenNo
		End Get
		Set(ByVal value As Boolean)
			m_RnLPChildrenNo = value
		End Set
	End Property

End Class
