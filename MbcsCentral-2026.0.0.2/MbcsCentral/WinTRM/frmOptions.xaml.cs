using Microsoft.Win32;
namespace WinTRM
{

	public class frmOptions
	{

		Options opt = null;

		RegistryKey regkey;
		private void frmOptions_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			opt = Options.load;
			if ((opt != null)) {
				txtCompanyName.Text = opt.companyName;
				txtSignature.Text = opt.emailSignature;
			}
			regkey = Registry.CurrentUser.OpenSubKey(GlobalShared.optionsKeyLoc + "DB Locations", true);
			if ((regkey != null)) {
				txtPmDB.Text = regkey.GetValue("pmPath", "");
				txtTrmDB.Text = regkey.GetValue("dbPath", "");
			}
			regkey = Registry.CurrentUser.OpenSubKey(GlobalShared.optionsKeyLoc + "General", true);
			if ((regkey != null)) {
				GlobalShared.autoEmail = (regkey.GetValue("autoEmail", false) ? true : false);
			}
			chkAutoEmail.IsChecked = GlobalShared.autoEmail;

		}


		private void cmdSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if ((opt == null)) {
				opt = new Options();
			}
			opt.companyName = txtCompanyName.Text;
			opt.emailSignature = txtSignature.Text;
			opt.save();
			//regkey = Registry.CurrentUser.OpenSubKey(optionsKeyLoc & "DB Locations", True)
			//regkey.SetValue("pmPath", txtPmDB.Text)
			regkey = Registry.CurrentUser.OpenSubKey(GlobalShared.optionsKeyLoc + "General", true);
			regkey.SetValue("autoEmail", chkAutoEmail.IsChecked);
			GlobalShared.autoEmail = chkAutoEmail.IsChecked;
			this.Close();

		}


		private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}


		private void cmdPmDB_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.DefaultExt = ".mdb";
			dlg.FileName = GlobalShared.pmPath;
			dlg.InitialDirectory = GlobalShared.pmPath.Substring(0, GlobalShared.pmPath.LastIndexOf("\\"));
			dlg.ShowDialog();
			if (!string.IsNullOrEmpty(dlg.FileName.Trim)) {
				GlobalShared.pmPath = dlg.FileName;
				regkey.SetValue("pmPath", GlobalShared.pmPath);
				txtPmDB.Text = GlobalShared.pmPath;
				Main.setPM_DB();
			}
		}


		private void cmdTrmDB_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.DefaultExt = ".mdb";
			dlg.FileName = GlobalShared.pmPath;
			dlg.InitialDirectory = GlobalShared.dbPath.Substring(0, GlobalShared.pmPath.LastIndexOf("\\"));
			dlg.ShowDialog();
			if (!string.IsNullOrEmpty(dlg.FileName.Trim)) {
				GlobalShared.dbPath = dlg.FileName;
				regkey.SetValue("dbPath", GlobalShared.dbPath);
				txtPmDB.Text = GlobalShared.pmPath;
				Main.setPM_DB();
			}

		}
		public frmOptions()
		{
			Loaded += frmOptions_Loaded;
		}
	}
}
