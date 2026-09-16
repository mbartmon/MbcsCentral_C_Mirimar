using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public class MbcsCentralService : Installer
{
    private ServiceProcessInstaller processInstaller;
    private ServiceInstaller serviceInstaller;

    public MbcsCentralService()
    {
        processInstaller = new ServiceProcessInstaller();
        serviceInstaller = new ServiceInstaller();
        processInstaller.Account = ServiceAccount.LocalSystem;
        serviceInstaller.StartType = ServiceStartMode.Automatic;
        serviceInstaller.ServiceName = "MbcsCentralService";
        serviceInstaller.Description = "1) Automated backup of database, 2)Rotates log files and deletes old ones. 3)Performs Winpm table cleanups.";
        Installers.Add(serviceInstaller);
        Installers.Add(processInstaller);
    }
}