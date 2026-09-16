Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Reflection
Imports System.Threading
Imports System.Windows
Imports Microsoft.Win32
Imports System.Text.RegularExpressions
Imports System.Management
Imports System.DirectoryServices
Imports WPDynamic

Module Main
    Public Const PIPE_TIMEOUT As Long = 1000 ' time in milliseconds
    Dim oldString As String = "dbSource"
    Dim debug As Boolean = False
    Dim AppDom As AppDomain = AppDomain.CurrentDomain

    Public Sub main(ByVal args() As String)

        AddHandler AppDom.UnhandledException, New UnhandledExceptionEventHandler(AddressOf GLOBALERRORHANDLER)

        'AddHandler Application.ThreadException, New ThreadExceptionEventHandler(AddressOf THREADERRORHANDLER)


        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey(optionsKeyLoc & "General", True)
        If IsNothing(regKey) Then
            regKey = Registry.CurrentUser.CreateSubKey(optionsKeyLoc & "General", RegistryKeyPermissionCheck.ReadWriteSubTree)
        Else
            autoEmail = IIf(regKey.GetValue("autoEmail", False), True, False)
        End If
        ' Setup connectionstrings and paths
        Dim connectionString As String = My.Settings.Item(ConnectionType)
        Dim newString As String = "WinTRM.accdb"
        WPDLLInt.SetDLLName(System.Windows.Forms.Application.StartupPath & "\WPTextDLL01.DLL")
        appPath = IIf(InStr(LCase(System.Windows.Forms.Application.StartupPath), "workfiles"), "c:\Program Files (x86)\MBCS\WinTRM\", System.Windows.Forms.Application.StartupPath)
        If Not appPath.EndsWith("\") Then
            appPath += "\"
        End If
        Log = New LogClass(appPath & logFileName, debug)
        Log.Log(LogClass.logType.Config, "Program Starting")
        Dim configs As New Dictionary(Of String, String)
        DictUtils.getConfigs(configs, appPath & MAIN_CONFIGURATION_FILE)
        DictUtils.setItem(configs, smtpConfigs(smtpItem.WinTrmPath), appPath)
        DictUtils.getItem(configs, CONFIG_JOB_SERVER, jobServer)
        regKey = Registry.CurrentUser.OpenSubKey(optionsKeyLoc & "DB Locations", True)
        If IsNothing(regKey) Then
            regKey = Registry.CurrentUser.CreateSubKey(optionsKeyLoc & "DB Locations", RegistryKeyPermissionCheck.ReadWriteSubTree)
        End If
        regKey.SetValue("WinTrmPath", appPath)
        pmPath = regKey.GetValue("pmPath", GetSetting(AppName:="Winpm", Section:="Options", Key:="DefaultDataBase", Default:="c:\pmsys\winpm.mdb"))
        dbPath = regKey.GetValue("dbPath", appPath)
        DictUtils.setItem(configs, CONFIG_PM_DATABASE, pmPath)
        DictUtils.setItem(configs, CONFIG_MAIN_DATABASE, dbPath)
        Log.Log(LogClass.logType.Config, "WinPM DB path=" & pmPath)
        Try
            regKey.SetValue("pmPath", pmPath)
            regKey.SetValue("dbPath", dbPath)
        Catch ex As System.Exception
            Log.Log(LogClass.logType.ErrorCondition, ex.Message)
        End Try

        connectionString = connectionString.Replace(oldString, dbPath & newString)
        CN = New System.Data.OleDb.OleDbConnection(connectionString)
        Log.Log(LogClass.logType.Config, "Main DB connector created on: " & connectionString)

        ' Setup LISTS directory, if necessary
        If Not My.Computer.FileSystem.DirectoryExists(appPath & listPath) Then
            Try
                My.Computer.FileSystem.CreateDirectory(appPath & listPath)
            Catch ex As System.Exception
                Log.Log(LogClass.logType.ErrorCondition, "Unable to create LISTS directory. Check with your system administrator.", False, MsgBoxStyle.OkOnly)
            End Try
        End If

        ' TODO: Create individual MAIL directories by user login
        ' Setup MAIL directories, if necessary
        Log.Log(LogClass.logType.Config, "Attempting to setup Mail directories")
        mailDir = appPath & mailDir
        mailInDir = mailDir & mailInDir
        mailOutDir = mailDir & mailOutDir
        Dim dirErr As Boolean = False
        If Not My.Computer.FileSystem.DirectoryExists(mailDir) Then
            Try
                My.Computer.FileSystem.CreateDirectory(mailDir)
            Catch ex As System.Exception
                Log.Log(LogClass.logType.ErrorCondition, "Unable to create MAIL directory.  Check with your system administrator.", False, MsgBoxStyle.OkOnly)
                dirErr = True
            End Try
        End If
        If Not dirErr And Not My.Computer.FileSystem.DirectoryExists(mailInDir) Then
            Try
                My.Computer.FileSystem.CreateDirectory(mailInDir)
            Catch ex As System.Exception
                Log.Log(LogClass.logType.ErrorCondition, "Unable to create MAIL IN directory.  Check with your administrator.", False, MsgBoxStyle.OkOnly)
            End Try
        End If
        If Not dirErr And Not My.Computer.FileSystem.DirectoryExists(mailOutDir) Then
            Try
                My.Computer.FileSystem.CreateDirectory(mailOutDir)
            Catch ex As System.Exception
                Log.Log(LogClass.logType.ErrorCondition, "Unable to create MAIL OUT directory.  Check with your administrator.", False, MsgBoxStyle.OkOnly)
            End Try
        End If
        Log.Log(LogClass.logType.Config, "Finished seting up Mail directories")

        ' * Register Named Pipes
        Log.Log(LogClass.logType.Config, "Starting registration of POP3 named pipe")
        ' * * Pop3 Listener
        regKey = Registry.CurrentUser.OpenSubKey(popKeyLoc & "User", False)
        If (Not IsNothing(regKey)) Then
            pipeName = regKey.GetValue("Listener Pipe", "")
        End If
        If pipeName.Trim = "" Then
            regKey = Registry.CurrentUser.CreateSubKey(popKeyLoc & "User", RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey.SetValue("Listener Pipe", LISTENER_PIPE_NAME, RegistryValueKind.String)
            pipeName = LISTENER_PIPE_NAME
        End If

        ' get the tenant database
        setPM_DB()

        ' update new db fields
        Log.Log(LogClass.logType.Config, "Updating new DB fields")
        Call updateDB()
        Log.Log(LogClass.logType.Config, "Finished updating new DB fields")

        ' Determine if Login screen should be displayed and get user object
        user = Nothing
        If args.Length > 0 Then
            user = Accounts.load("mike") ' Called from another program - try to get user account
        End If
        If InStr(LCase(System.Windows.Forms.Application.StartupPath), "workfiles") = 0 AndAlso IsNothing(user) Then
            Dim frmLogin As New Login
            frmLogin.ShowDialog()
            frmLogin = Nothing
        Else ' Developer account
            user = Accounts.load("mike")
            If IsNothing(user) Then
                user = Accounts.load("chadf")
            End If
        End If
        Log.Log(LogClass.logType.Config, "User <" & IIf(IsNothing(user), "", user.getName.ToString & "> logged in"))
        DictUtils.setConfigs(configs, appPath & MAIN_CONFIGURATION_FILE)

        If args.Length > 0 Then ' We've been called by another app
            Log.Log(LogClass.logType.Config, "Called by another application")
            Dim action As String = args(0).ToLower
            Dim noteid As Long = Val(args(1))
            If args.Length > 2 Then
                If args(2).ToLower.Equals("b") Then
                    Dim frmN As New frmIssueBuilding
                    frmN.issueId = noteid
                    frmN.WindowState = Forms.FormWindowState.Maximized
                    frmN.Focus()
                    frmN.ShowDialog()
                Else
                    Dim frmN As New frmIssue
                    frmN.issueId = noteid
                    frmN.WindowState = Forms.FormWindowState.Maximized
                    frmN.Focus()
                    frmN.ShowDialog()
                End If
            End If
        Else ' Normal startup
            Log.Log(LogClass.logType.Config, "Normal start-up completed. Loading main form")
            Dim frm As New frmMainWindow
            frm.ShowDialog()
        End If
    End Sub

    Public Sub setPM_DB()

        Dim connectionString As String

        connectionString = My.Settings.Item(pmConnectionType)
        connectionString = connectionString.Replace(oldString, pmPath)

        Try
            pmCN = Nothing
        Catch ex As System.Exception

        End Try
        pmCN = New System.Data.OleDb.OleDbConnection(connectionString)
        Log.Log(LogClass.logType.Config, "Winpm DB connector changed on: " & connectionString)

    End Sub

    Public Sub setTRM_DB()

        Dim connectionString As String = My.Settings.Item(ConnectionType)
        Dim newString As String = "WinTRM.accdb"
        connectionString = connectionString.Replace(oldString, pmPath)

        Try
            CN = Nothing
        Catch ex As System.Exception

        End Try
        CN = New System.Data.OleDb.OleDbConnection(connectionString)
        Log.Log(LogClass.logType.Config, "WinTRM DB connector changed on: " & connectionString)

    End Sub
    Private Sub updateDB()

        Try
            ' 01/02/2013
            dbFieldAppend(CN, schedules.TABLE, schedules.SQL_COL_INCLUDE_WEEKENDS, "YESNO", 0, False)
            dbFieldAppend(CN, schedules.TABLE, schedules.SQL_COL_INCLUDE_HOLIDAYS, "YESNO", 0, False)
            dbFieldAppend(CN, MessageSchedule.TABLE, MessageSchedule.SQL_COL_INCLUDE_WEEKENDS, "YESNO", 0, False)
            dbFieldAppend(CN, MessageSchedule.TABLE, MessageSchedule.SQL_COL_INCLUDE_HOLIDAYS, "YESNO", 0, False)

            ' 12/02/2012
            dbFieldAppend(CN, RecipientMaster.TABLE, RecipientMaster.SQL_COL_OWNER_FILTER, "YESNO", 0, False)
            dbFieldAppend(CN, RecipientMaster.TABLE, RecipientMaster.SQL_COL_BUILDING_FILTER, "YESNO", 0, False)
            dbFieldAppend(CN, RecipientMaster.TABLE, RecipientMaster.SQL_COL_TYPE_FILTER, "YESNO", 0, False)
            dbFieldAppend(CN, RecipientMaster.TABLE, RecipientMaster.SQL_COL_ARREARS_FILTER, "YESNO", 0, False)
            dbFieldAppend(CN, RecipientMaster.TABLE, RecipientMaster.SQL_COL_ARREARS_AMOUNT, "Currency", 0, 0)

            ' 11/23/2012
            dbFieldAppend(CN, MessageSchedule.TABLE, MessageSchedule.SQL_COL_RETURN_RECEIPT, "YESNO", 0, False)

            dbFieldAppend(CN, JobMaster.TABLE, JobMaster.SQL_COL_SMTP_ACCOUNT_ID, "Long Integer", 0, 0)
            dbFieldAppend(CN, JobMaster.TABLE, JobMaster.SQL_COL_MSG_SCHED_ID, "Long Integer", 0, 0)
            dbFieldAppend(CN, JobMaster.TABLE, JobMaster.SQL_COL_LAST_ERROR, "String", 255, "")

            ' 03/06/2012
            'dbFieldAppend(CN,Issues.TABLE,Issues)

            dbFieldAppend(CN, Accounts.TABLE, Accounts.SQL_COL_USE_OUTLOOK, "YESNO", 0, False)
            dbFieldAppend(CN, Accounts.TABLE, Accounts.SQL_COL_OUTLOOK_FOLDERS, "String", 254, "")
            dbFieldAppend(CN, Messages.TABLE, Messages.SQL_COL_READ, "YESNO", 0, False)

            ' 09/30/2011
            dbFieldAppend(CN, schedules.TABLE, schedules.SQL_COL_NO_END_DATE, "YESNO", 0, False)

            ' 12/31/2011
            dbFieldAppend(CN, IssueTenants.TABLE, IssueTenants.SQL_COL_BUILDING_NUMBER, "String", 3, Nothing)
            dbFieldAppend(CN, IssueTenants.TABLE, IssueTenants.SQL_COL_TENANT_NUMBER, "String", 4, Nothing)
            If dbFieldAppend(CN, IssueTenants.TABLE, IssueTenants.SQL_COL_APARTMENT, "String", 4, Nothing) Then
                Dim tens() As IssueTenants = IssueTenants.load()
                Dim tenrec As Tenants
                For Each ten As IssueTenants In tens
                    tenrec = Tenants.load(ten.getSequenceNumber)
                    If Not IsNothing(tenrec) Then
                        ten.buildingNumber = tenrec.getBuildingNumber
                        ten.tenantNumber = tenrec.getTenantNumber
                        ten.apartment = tenrec.getApartment
                        ten.Save()
                    End If
                Next
            End If
        Catch ex As System.Data.OleDb.OleDbException
            Log.debug = True
            Log.Log(LogClass.logType.ErrorCondition, "updateDB: " & ex.Message)
        Catch ex As System.Exception
            Log.debug = True
            Log.Log(LogClass.logType.ErrorCondition, "updateDB: " & ex.Message)
        End Try
        Log.debug = False

    End Sub

    Function GetNames() As List(Of String)

        Dim l As New List(Of String)
        Dim de As New DirectoryEntry()
        de.Path = "WinNT://" & getDomainOrGroup()

        If Not IsNothing(de) Then
            Try
                For Each d As DirectoryEntry In de.Children

                    If d.SchemaClassName = "Computer" Then l.Add(d.Name)

                Next
            Catch ex As System.Exception
                Log.Log(LogClass.logType.ErrorCondition, "Retrieving network computer names: " & ex.Message)
                Return Nothing
            End Try
        End If
        If Not l.Contains(jobServer) Then
            l.Add(jobServer)
        End If
        Return l

    End Function

    Function getDomainOrGroup() As String

        Dim info As NetworkInformation = NetworkInformation.LocalComputer

        If info.Status = NetworkInformation.JoinStatus.Workgroup Then
            'Console.WriteLine("You are part of a workgroup")
            'Console.WriteLine(info.DomainName)
            Return info.DomainName
        ElseIf info.Status = NetworkInformation.JoinStatus.Domain Then
            'Console.WriteLine("You are part of a domain")
            'Console.WriteLine(info.DomainName)
            Return info.DomainName
        Else
            'Console.WriteLine("You are not in a domain or workgroup")
            Return Nothing
        End If

    End Function

    Public Function cronString(start_date As DateTime, howOften As Integer, day_To_Process_2 As Integer) As String

        Dim lead As Double = 15
        Dim second As String = start_date.Second.ToString
        Dim minute As String = start_date.Minute.ToString
        Dim hour As String = start_date.Hour.ToString
        Dim dayOfMonth As String = start_date.Day
        Dim month As String = start_date.Month
        ' NOTE: Framework DayOfWeek is 0 - 6, Quartz cronexpression Dayofweek is 1 - 7
        Dim dayOfWeek As String = start_date.DayOfWeek + 1
        Dim year As String = start_date.Year
        Dim dayOfMonth2 As String = day_To_Process_2

        Dim caseString As String = second + " " + minute + " " + hour + " "

        Select Case howOften

            Case schedules.HowOften.Once
                caseString += "* * ? *"

            Case schedules.HowOften.Daily
                caseString += "* * ? *"

            Case schedules.HowOften.Weekly
                caseString += "? * " + dayOfWeek + " *"

            Case schedules.HowOften.BiWeekly
                caseString += "? * " + dayOfWeek + " *" 'Rework this to fire every 14 days

            Case schedules.HowOften.Monthly
                caseString += dayOfMonth + " * ? *"

            Case schedules.HowOften.SemiMonthly
                caseString += dayOfMonth + "/" + dayOfMonth2 + " * ? *"

            Case schedules.HowOften.TwoMonths
                caseString += dayOfMonth + " */2 ? *"

            Case schedules.HowOften.Quarterly
                caseString += dayOfMonth + " */3 ? *"

        End Select

        Return caseString

    End Function

    Private Sub GLOBALERRORHANDLER(sender As Object, args As UnhandledExceptionEventArgs)

        Dim ex As System.Exception = CType(args.ExceptionObject, System.Exception)
        'Dim frmErr As New FrmError
        'frmErr.p_Message = ex.Message.ToString() + " *****" + ex.Source.ToString() + " *****" + ex.StackTrace.ToString() + " *****" + ex.TargetSite.ToString()
        'frmErr.MdiParent = messageDeliveryLogName
        'frmErr.StartPosition = FormStartPosition.CenterScreen
        'frmErr.Show()
        MsgBox(ex.Message.ToString() + " *****" + ex.Source.ToString() + " *****" + ex.StackTrace.ToString() + " *****" + ex.TargetSite.ToString())

    End Sub
    Private Sub THREADERRORHANDLER(sender As Object, args As ThreadExceptionEventArgs)

        Dim ex As System.Exception = CType(args.Exception, System.Exception)
        'Dim frmErr As New FrmError()
        'frmErr.p_Message = ex.Message.ToString() + " *****" + ex.Source.ToString() + " *****" + ex.StackTrace.ToString() + " *****" + ex.TargetSite.ToString()
        'frmErr.MdiParent = messageDeliveryLogName
        'frmErr.StartPosition = FormStartPosition.CenterScreen
        'frmErr.Show()
        MsgBox(ex.Message.ToString() + " *****" + ex.Source.ToString() + " *****" + ex.StackTrace.ToString() + " *****" + ex.TargetSite.ToString())

    End Sub

End Module
