using System.Drawing.Printing;
namespace WinTRM
{

	public class frmReportConfig
	{

		private Dictionary<string, string> configs = new Dictionary<string, string>();
		private int _reportId;
		private object _reportClass;
		public int reportId {
			get { return _reportId; }
			set { _reportId = value; }
		}
		private string _printerName;
		public string printerName {
			get { return _printerName; }
			set { _printerName = value; }
		}
		private int _printMode;
		public int printMode {
			get { return _printMode; }
		}
		private long _recordKey;
		public long recordKey {
			get { return _recordKey; }
			set { _recordKey = value; }
		}


		private void frmReportConfig_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{

			System.Drawing.Rectangle rect = Utils.getForm(this);
			this.Top = rect.Top;
			this.Left = rect.Left;
			this.Width = rect.Left;
			this.Height = rect.Height;

			DictUtils.getConfigs(ref configs, GlobalShared.appPath + GlobalShared.MAIN_CONFIGURATION_FILE);
			DictUtils.getItem(configs, GlobalShared.reports(_reportId) + ".Printer", ref _printerName);

			string pkInstalledPrinters = null;

			for (int i = 0; i <= PrinterSettings.InstalledPrinters.Count - 1; i++) {
				pkInstalledPrinters = PrinterSettings.InstalledPrinters.Item(i);

				lstPrinters.Items.Add(pkInstalledPrinters);
				if (pkInstalledPrinters == _printerName) {
					lstPrinters.SelectedIndex = i;
				}
			}

			lblReportName.Content = GlobalShared.reports(_reportId);

		}


		private void lstPrinters_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (lstPrinters.SelectedIndex >= 0) {
				_printerName = lstPrinters.SelectedItem;
			}

		}


		private void cmdDisplay_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_printMode = 0;
			GlobalShared.args = {
				_printMode,
				_printerName,
				_recordKey
			};
			try {
				_reportClass = Activator.CreateInstance("WinTrm", "WinTRM." + GlobalShared.reportClass(_reportId));
			} catch (TypeLoadException ex) {
				Interaction.MsgBox("Type Load Exception");
			} catch (System.Exception ex) {
				Interaction.MsgBox("Error: " + ex.Message);
			}

		}


		private void cmdPrint_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_printMode = 1;
			GlobalShared.args = {
				_printMode,
				_printerName,
				_recordKey
			};
			try {
				_reportClass = Activator.CreateInstance("WinTRM", "WinTRM." + GlobalShared.reportClass(_reportId));
			} catch (TypeLoadException ex) {
				Interaction.MsgBox("Type Load Exception");
			} catch (System.Exception ex) {
				Interaction.MsgBox("Error: " + ex.Message);
			}

		}


		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			DictUtils.setItem(configs, GlobalShared.reports(_reportId) + ".Printer", _printerName);
			DictUtils.setConfigs(ref configs, GlobalShared.appPath + GlobalShared.MAIN_CONFIGURATION_FILE);
			Utils.saveForm(this);
			this.Close();

		}
		public frmReportConfig()
		{
			Loaded += frmReportConfig_Loaded;
		}
	}
}
