
using System;
using System.Security;
using System.ComponentModel;
using System.Runtime.InteropServices;
namespace WinTRM
{

	public sealed class NetworkInformation
	{

		public enum JoinStatus
		{
			Unknown = 0,
			UnJoined = 1,
			Workgroup = 2,
			Domain = 3
		}

		[DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int NetGetJoinInformation(string computerName, ref IntPtr buffer, ref JoinStatus status);

		[SuppressUnmanagedCodeSecurityAttribute()]
		[DllImport("netapi32.dll", SetLastError = true)]
		public static extern int NetApiBufferFree(IntPtr buffer);

		// <summary>
		// Netapi32.dll : The NetServerEnum function lists all servers
		// of the specified type that are
		// visible in a domain. For example, an 
		// application can call NetServerEnum
		// to list all domain controllers only
		// or all SQL servers only.
		// You can combine bit masks to list
		// several types. For example, a value 
		// of 0x00000003  combines the bit
		// masks for SV_TYPE_WORKSTATION 
		// (0x00000001) and SV_TYPE_SERVER (0x00000002)
		// </summary>
		[SuppressUnmanagedCodeSecurityAttribute()]
		[DllImport("netapi32.dll", SetLastError = true)]
		public static extern int NetServerEnum(string ServerNane, int dwLevel, ref IntPtr pBuf, int dwPrefMaxLen, ref int dwEntriesRead, ref int dwTotalEntries, int dwServerType, string domain, ref int dwResumeHandle);


		// <summary>
		// Netapi32.dll : The NetApiBufferFree function frees 
		// the memory that the NetApiBufferAllocate function allocates. 
		// Call NetApiBufferFree to free
		// the memory that other network 
		// management functions return.
		// </summary>
		//declare the Netapi32 : NetApiBufferFree method import
		//<DllImport("Netapi32", SetLastError = True), SuppressUnmanagedCodeSecurityAttribute()> _
		//Public Shared Function NetApiBufferFree(pBuf As IntPtr) As Integer

		private static NetworkInformation _local = new NetworkInformation();
		private string _computerName;
		private string _domainName;

		private JoinStatus _status = JoinStatus.Unknown;
		public NetworkInformation(string computerName)
		{
			if (computerName == null || 0 == computerName.Length) {
				throw new ArgumentNullException("computerName");
			}

			_computerName = computerName;
			LoadInformation();
		}

		private NetworkInformation()
		{
			LoadInformation();
		}

		public static NetworkInformation LocalComputer {
			get { return _local; }
		}

		public string ComputerName {
			get {
				if (_computerName == null)
					return "(local)";
				return _computerName;
			}
		}

		public string DomainName {
			get { return _domainName; }
		}

		public JoinStatus Status {
			get { return _status; }
		}

		private void LoadInformation()
		{
			IntPtr pBuffer = IntPtr.Zero;
			JoinStatus status = default(JoinStatus);

			try {
				int result = NetGetJoinInformation(_computerName, ref pBuffer, ref status);
				if (0 != result)
					throw new Win32Exception();

				_status = status;
				_domainName = Marshal.PtrToStringUni(pBuffer);

			} finally {
				if (!IntPtr.Zero.Equals(pBuffer)) {
					NetApiBufferFree(pBuffer);
				}
			}
		}

		public override string ToString()
		{
			switch (_status) {
				case JoinStatus.Domain:
					return ComputerName + " is a member of the domain " + DomainName;
				case JoinStatus.Workgroup:
					return ComputerName + " is a member of the workgroup " + DomainName;
				case JoinStatus.UnJoined:
					return ComputerName + " is a standalone computer";
				default:
					return "Unable to determine the network status of " + ComputerName;
			}
		}

		public struct _SERVER_INFO_100
		{

			public int sv100_platform_id;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string sv100_name;
		}

		// Uses the DllImport : NetServerEnum
		// with all its required parameters
		// (see http:'msdn.microsoft.com/library/default.asp?
		//      url=/library/en-us/netmgmt/netmgmt/netserverenum.asp
		// for full details or method signature) to
		// retrieve a list of domain SV_TYPE_WORKSTATION
		// and SV_TYPE_SERVER PC's
		// </summary>
		// <returns>Arraylist that represents
		// all the SV_TYPE_WORKSTATION and SV_TYPE_SERVER
		// PC's in the Domain</returns>
		public ArrayList getNetworkComputers()
		{

			//local fields
			ArrayList networkComputers = new ArrayList();
			const int MAX_PREFERRED_LENGTH = -1;
			int SV_TYPE_WORKSTATION = 1;
			int SV_TYPE_SERVER = 2;
			IntPtr buffer = IntPtr.Zero;
			IntPtr tmpBuffer = IntPtr.Zero;
			int entriesRead = 0;
			int totalEntries = 0;
			int resHandle = 0;
			int sizeofINFO = Marshal.SizeOf(typeof(_SERVER_INFO_100));



			try {
				//call the DllImport : NetServerEnum 
				//with all its required parameters
				//see http:'msdn.microsoft.com/library/
				//default.asp?url=/library/en-us/netmgmt/netmgmt/netserverenum.asp
				//for full details of method signature
				int ret = NetServerEnum(null, 100, ref buffer, MAX_PREFERRED_LENGTH, ref entriesRead, ref totalEntries, SV_TYPE_WORKSTATION | SV_TYPE_SERVER, null, ref resHandle);
				//if the returned with a NERR_Success 
				//(C++ term), =0 for C#

				if (ret == 0) {
					//loop through all SV_TYPE_WORKSTATION 
					//and SV_TYPE_SERVER PC's

					for (int i = 0; i <= totalEntries - 1; i++) {
						//get pointer to, Pointer to the 
						//buffer that received the data from
						//the call to NetServerEnum. 
						//Must ensure to use correct size of 
						//STRUCTURE to ensure correct 
						//location in memory is pointed to
						tmpBuffer = new IntPtr(Convert.ToInt32(buffer) + (i * sizeofINFO));
						//Have now got a pointer to the list 
						//of SV_TYPE_WORKSTATION and 
						//SV_TYPE_SERVER PC's, which is unmanaged memory
						//Needs to Marshal data from an 
						//unmanaged block of memory to a 
						//managed object, again using 
						//STRUCTURE to ensure the correct data
						//is marshalled 
						_SERVER_INFO_100 svrInfo = (_SERVER_INFO_100)Marshal.PtrToStructure(tmpBuffer, typeof(_SERVER_INFO_100));

						//add the PC names to the ArrayList
						networkComputers.Add(svrInfo.sv100_name);
					}
				}


			} catch (Exception ex) {
				MessageBox.Show("Problem with acessing " + "network computers in NetworkBrowser " + "\\r\\n\\r\\n\\r\\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;


			} finally {
				//The NetApiBufferFree function frees 
				//the memory that the 
				//NetApiBufferAllocate function allocates
				NetApiBufferFree(buffer);
			}
			//return entries found
			return networkComputers;

		}
	}
}
