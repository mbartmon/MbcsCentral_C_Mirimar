using MbcsCentral;
using MbcsCentral.WinPM;
public class BackgroundTasks
{


	public class retrieveTenantNames
	{
        public retrieveTenantNames()
        {
        }
        public void CheckStatus(object stateinfo)
        {
            MbcsCentral.GlobalShared.tenantNamesTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            MbcsCentral.GlobalShared.tenantNameInDelegate = true;

            MbcsCentral.GlobalShared.tenantNames = Tenants.loadForNames();

            MbcsCentral.GlobalShared.tenantNamesTimer.Change(MbcsCentral.GlobalShared.TENANT_NAMES_INTERVAL, MbcsCentral.GlobalShared.TENANT_NAMES_INTERVAL);
            MbcsCentral.GlobalShared.tenantNameInDelegate = false;
        }
   }


}
