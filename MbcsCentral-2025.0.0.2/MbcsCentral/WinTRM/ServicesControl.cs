using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Text;
namespace MbcsCentral.WinTrm
{

	public static class ServicesControl
	{

		static int exceptioncaught;
		[DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		#region "Values for Services"

		private static extern int WritePrivateProfileString(string lpSectionName, string lpKeyName, string lpKeyValue, string lpFileName);
		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern int GetPrivateProfileString(string lpSectionName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static IntPtr OpenSCManager(string sMachineName, string sDbName, int iAccess)
		{
		}
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static IntPtr CreateService(IntPtr hSCM, string sName, string sDisplay, int iAccess, int iServiceType, int iStartType, int iError, string sPath, string sGroup, int iTag,
		string sDepends, string sUser, string sPass)
		{
		}
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static int CloseServiceHandle(IntPtr iHandle)
		{
		}
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static IntPtr OpenService(IntPtr hSCM, string sServiceName, int iDesiredAccess)
		{
		}
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static bool DeleteService(IntPtr hService)
		{
		}

		private enum ServiceControlManagerEnum
		{
			Connect = 0x1,
			CreateService = 0x2,
			EnumerateService = 0x4,
			Lock = 0x8,
			QueryLockStatus = 0x10,
			ModifyBootConfig = 0x20,
			AllAccess = (StandardRightsRequired | Connect | ServiceControlManagerEnum.CreateService | EnumerateService | ServiceControlManagerEnum.Lock | QueryLockStatus | ModifyBootConfig),
			StandardRightsRequired = 0xf0000
		}
		private enum ServiceAccessTypeEnum
		{
			QueryConfig = 0x1,
			ChangeConfig = 0x2,
			QueryStatus = 0x4,
			EnumerateDependents = 0x8,
			Start = 0x10,
			Stop = 0x20,
			PauseContinue = 0x40,
			Interrogate = 0x80,
			UserDefinedControl = 0x100,
			AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig | QueryStatus | EnumerateDependents | Start | Stop | PauseContinue | Interrogate | UserDefinedControl),
			StandardRightsRequired = 0xf0000
		}
		private enum ServiceTypeEnum
		{
			Win32OwnProcess = 0x10,
			AutoStart = 0x2,
			ErrorNormal = 0x1
		}
		private enum ServiceControlTypeEnum
		{
			OwnProcess = 0x10,
			ShareProcess = 0x20,
			KernelDriver = 0x1,
			FileSystemDriver = 0x2,
			InteractiveProcess = 0x100
		}
		private const string c_sBefore = "<IDEAConfiguration><Group><Name>Global</Name><BATList uselist=\"No\"/>";

		private const string c_sAfter = "</Group></IDEAConfiguration>";
		#endregion

		public static void InstallService(string sPath, string sServiceName, string sUserID)
		{
			try {
				Console.WriteLine("Running Install Service...");

				//Dim sUserID As String = sDomainName & "\" & sUserID
				string sPassword = sPassword;

				if (IsServiceInstalled(sServiceName))
					UninstallService(sServiceName);

				// INSTALL
				IntPtr hSCM = OpenSCManager(null, null, ServiceControlManagerEnum.AllAccess);
				if (hSCM.ToInt32 == 0) {
					throw new System.Exception("Could not install service. [1]");
				} else {
					int iServiceType = ServiceTypeEnum.Win32OwnProcess;

					IntPtr hService = CreateService(hSCM, sServiceName, sServiceName, ServiceControlManagerEnum.AllAccess, iServiceType, ServiceTypeEnum.AutoStart, ServiceTypeEnum.ErrorNormal, sPath + sServiceName + ".exe", null, null,
					null, sUserID, sPassword);
					if (hService.ToInt32 == 0) {
						throw new System.Exception("Could not install service. [2]");
					} else {
						CloseServiceHandle(hService);
					}

					CloseServiceHandle(hSCM);
					ControlServices(sServiceName, "Start");
				}
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, GlobalShared.appPath + sServiceName + " could not be installed: " + ex.Message);
				//Console.WriteLine(ex.Message)
				//Console.ReadLine()
			}
		}

		public static bool IsServiceInstalled(string sServiceName)
		{
			bool bResult = false;

			ServiceProcess.ServiceController[] oServiceArray = null;
			oServiceArray = ServiceProcess.ServiceController.GetServices;

			foreach (ServiceProcess.ServiceController oServiceController in oServiceArray) {
				if (oServiceController.ServiceName.Trim.ToUpper == sServiceName.Trim.ToUpper) {
					//Dim i As New ServiceControllerPermissionAttribute(Security.Permissions.SecurityAction.Demand)
					//Dim d As New ServiceControllerPermission
					//Try
					//If d.Any Then
					//d.ToString()
					//End If
					//Catch
					//End Try
					bResult = true;
					break; // TODO: might not be correct. Was : Exit For
				}
			}

			return bResult;
		}

		public static void UninstallService(string sServiceName)
		{
			Console.WriteLine("Running Uninstall Service...");

			if (IsServiceInstalled(sServiceName)) {
				// STOP SERVICE
				ControlServices(sServiceName, "Stop");
			} else {
				return;
			}

			IntPtr hSCM = OpenSCManager(null, null, ServiceControlManagerEnum.AllAccess);
			if (hSCM.ToInt32 == 0) {
				throw new System.Exception("Could not delete service. [1]");
			} else {
				IntPtr hService = OpenService(hSCM, sServiceName, ServiceAccessTypeEnum.AllAccess);
				if (hService.ToInt32 == 0) {
					// TODO: FAILED
				} else {
					if (!DeleteService(hService)) {
						throw new System.Exception("Could not delete service. [2]");
					}

					CloseServiceHandle(hService);
				}

				CloseServiceHandle(hSCM);
			}
		}

		public static bool ControlServices(string sServiceName, string sTask)
		{
			bool functionReturnValue = false;
			string Status = null;
			ServiceController objWinServ = new ServiceController();
			objWinServ.ServiceName = sServiceName;

			try {
				switch (sTask) {

					case "Reset":
						if (objWinServ.Status == ServiceControllerStatus.Running) {
							//Service is currently running, so you will have to stop it before starting it
							//First stop all it's child services, then stop the service itself
							CheckForChildServices(sServiceName, "Stop");
						}

						//This is satisfied if the service was running and then was stopped by code or if it was already stopped
						if (objWinServ.Status == ServiceControllerStatus.Stopped) {
							//service is already stopped so just start it...so first start all it's parent services
							CheckForParentServices(sServiceName, "Start");
							StartService(objWinServ.DisplayName);
						}

						break;
					case "Start":
						//This is satisfied if the service was running and then was stopped by code or if it was already stopped
						if (objWinServ.Status == ServiceControllerStatus.Stopped) {
							CheckForParentServices(sServiceName, "Start");
							if (exceptioncaught == 0) {
								Status = "Successfully Started";
							} else {
								Status = "Could not Start " + objWinServ.DisplayName;
								exceptioncaught = 0;
							}
						}

						break;
					case "Stop":
						if (objWinServ.Status != ServiceControllerStatus.Stopped) {
							CheckForChildServices(sServiceName, "Stop");
							if (exceptioncaught == 0) {
								Status = "Successfully Stopped";
							} else {
								exceptioncaught = 0;
								Status = "Could not Stop " + objWinServ.DisplayName;
							}
						}
						break;
				}
			} catch (System.InvalidOperationException ex) {
				Console.WriteLine("Service does not exist");
				return functionReturnValue;
			} catch (System.Exception e) {
				Console.WriteLine(e.Message);
				return functionReturnValue;
			}
			Console.WriteLine(Status);
			return functionReturnValue;
		}

		private static void CheckForParentServices(string sServiceName, string NextService)
		{
			ServiceController objService = new ServiceController();
			objService.ServiceName = sServiceName;
			ServiceController objParentService = default(ServiceController);

			foreach ( objParentService in objService.ServicesDependedOn) {
				CheckForParentServices(objParentService.DisplayName, NextService);
			}
			if (NextService == "Start") {
				StartService(sServiceName);
			}

		}

		private static void StartService(string sServiceName)
		{
			ServiceController objService = new ServiceController();
			objService.ServiceName = sServiceName;

			if (objService.Status == ServiceControllerStatus.Stopped) {
				try {
					objService.Start();
					objService.WaitForStatus(ServiceControllerStatus.Running, System.TimeSpan.FromSeconds(20));
				} catch (TimeoutException ex) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Could not Start " + sServiceName + " - TimeOut expired");
					exceptioncaught = 1;
				} catch (System.Exception e) {
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Could not Start " + sServiceName + " - " + e.Message);
					exceptioncaught = 1;
				}
			}

		}

		private static void CheckForChildServices(string sServiceName, string NextService)
		{
			ServiceController objService = new ServiceController();
			objService.ServiceName = sServiceName;
			ServiceController objChildService = default(ServiceController);

			foreach ( objChildService in objService.DependentServices) {
				CheckForChildServices(objChildService.DisplayName, NextService);
			}

			if (NextService == "Stop") {
				StopService(sServiceName);
			} else {
				ContinueService(sServiceName);
			}

		}

		private static void StopService(string sServiceName)
		{
			ServiceController objService = new ServiceController();
			objService.ServiceName = sServiceName;

			if (objService.Status == ServiceControllerStatus.Running) {
				try {
					objService.Stop();
					objService.WaitForStatus(ServiceControllerStatus.Stopped, System.TimeSpan.FromSeconds(20));
				} catch (TimeoutException ex) {
					exceptioncaught = 1;
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Could not Stop " + sServiceName + " - TimeOut expired");
				} catch (System.Exception e) {
					exceptioncaught = 1;
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Could not Stop " + sServiceName + " - " + e.Message);
				}
			}

		}

		private static void ContinueService(string sServiceName)
		{
			ServiceController objService = new ServiceController();
			objService.ServiceName = sServiceName;
			if (objService.Status == ServiceControllerStatus.Paused) {
				try {
					objService.Continue();
					objService.WaitForStatus(ServiceControllerStatus.Running, System.TimeSpan.FromSeconds(20));
				} catch (TimeoutException ex) {
					exceptioncaught = 1;
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Could not change status from Paused To Continue for " + sServiceName + " - TimeOut expired");
				} catch (System.Exception e) {
					exceptioncaught = 1;
					GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Could not change status from Paused To Continue for " + sServiceName + " - " + e.Message);
				}
			}

			if (objService.Status == ServiceControllerStatus.Stopped) {
				StopService(sServiceName);
			}

		}

		public static bool CheckStatusOfService(string ServiceName, string MachineName)
		{
			bool Status = false;
			ServiceController objWinServ = new ServiceController();
			objWinServ.ServiceName = ServiceName;
			objWinServ.MachineName = MachineName;

			//If the service is stopped then the button to start it needs to be enabled
			if (objWinServ.Status == ServiceControllerStatus.Stopped) {
				Status = true;
			}

			return Status;
		}
	}
}
