using System;
using System.Windows;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.Configuration;
 
namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
 
    public partial class App : System.Windows.Application
    {
        //private FirstFloor.XamlSpy.XamlSpyService service;

        protected override void OnStartup(StartupEventArgs e)
        {
          base.OnStartup(e);
            /*
            PresentationTraceSources.Refresh();
            PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning | SourceLevels.Error;
            base.OnStartup(e);         
            */
            /*
          this.service = new FirstFloor.XamlSpy.XamlSpyService
          {
              RemoteAddress = "any",
              RemotePort = 4530,
              Password = "01345"
          };
           */
        }
    }

    public class DebugTraceListener : TraceListener
    {
        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
            Debugger.Break();
        }
    }
}
