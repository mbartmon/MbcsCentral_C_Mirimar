using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MbcsCentral.WinPM
{
	/// <summary>
	/// Interaction logic for frmSecTrans.xaml
	/// </summary>
	public partial class frmSecTrans : Window
	{
		bool noTenant;
        bool usingTenantId = false;
        bool needSaving = false;
        bool firstTime = true;
        STTenants[] stens = null;
        STTenants sten = null;
		int tenantId;
        ArrayList trans = null;
        STCodes[] codes;
        private ArrayList deletion_ids = new ArrayList();

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        // Property
        public int SequenceNumber { set { tenantId = value; } }

		public frmSecTrans()
		{
			InitializeComponent();
		}

        public frmSecTrans(string parm)
        {
           InitializeComponent();
           m_ThisApp = (string) parm;
        }	

        private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            Main.getForm(this);

            codes = STCodes.load();
            System.Windows.Data.CollectionViewSource codesSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Codes")));
            codesSource.Source = codes;

            if (tenantId > 0)
            {
                usingTenantId = true;
                getTenant();
                usingTenantId = false;
                grdMain.Focus();
            }
            else
            txtBuildingNumber.Focus();
            //firstTime = false;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (needSaving)
            {
                switch (MessageBox.Show("Save your changes?","",MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Yes:
                        cmdSave_Click(sender, new RoutedEventArgs());
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
               }
            }
            Main.saveForm(this);
		}

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectionStart = 0;
            txt.SelectionLength = txt.Text.Length;
        }

        private void txtBuildingNumber_TextChanged(object sender, TextChangedEventArgs e)
		{
            if (!firstTime)
                Main.moveNextOnMaxlength(sender, e);
            else
                firstTime = false;
		}

        private void txtTenantNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                txtTenantNumber_LostFocus(sender, new RoutedEventArgs());
            }

        }
        private void txtTenantNumber_LostFocus(object sender, RoutedEventArgs e)
		{
            if (usingTenantId)
            {
                usingTenantId = false;
                return;
            }
			if (noTenant)
			{
				noTenant = false;
				return;
			}
			stens = STTenants.loadByTenant(txtBuildingNumber.Text, txtTenantNumber.Text);
			if (stens == null || stens.Length == 0)
			{
				MessageBox.Show("Invalid Tenant Number");
				txtTenantNumber.Focus();
				noTenant = true;
				txtTenantNumber.Focus();
				return;
			}
			if (stens.Length == 1)
			{
				tenantId = stens[0].SequenceNumber;
			}
			else
			{
                int highestSeq = 0;
                foreach(STTenants ten in stens)
                {
                    if (ten.SequenceNumber > highestSeq)
                        highestSeq = ten.SequenceNumber;
                }
                tenantId = highestSeq;

                /*
				try
				{
					frmChooseTenant frm = new frmChooseTenant();
					frm.SecTenants = stens;
					frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
					frm.ShowDialog();
					if (frm.SequenceNumber == -1)
					{
						return;
					}
					tenantId = frm.SequenceNumber;
				}
				catch (Exception ex)
				{
					GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Security Transactions: " + ex.Message, false);
				}
                */
			}
			getTenant();
            grdMain.Focus();
		}

		private void getTenant()
		{
            if (needSaving)
            {

            }
            deletion_ids.Clear();
            sten = STTenants.loadBySequence(tenantId);
            MainGrid.DataContext = sten;
            trans = STTrans.loadBySequenceList(tenantId);
            if (trans != null)
            {
                grdMain.ItemsSource = (STTrans[])trans.ToArray(typeof(STTrans));
                TextChangedEventArgs e =  null;
                object sender = null;
                NumberEntry_TextChanged(sender, e);
            }
            grdMain.Focus();
		}

        private void ctxDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to perform this action.");
                return;
            }
            if (grdMain.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Delete this allocation?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int i = grdMain.SelectedIndex;
                    deletion_ids.Add(((STTrans)trans[i]).ID);
                    grdMain.ItemsSource = null;
                    trans.RemoveAt(i);
                    grdMain.ItemsSource = (STTrans[])trans.ToArray(typeof(STTrans));
                }

            }
        }

        private void ctxAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have permission to perform this action.");
                return;
            }
            STTrans tran = new STTrans();
            tran.SequenceNumber = sten.SequenceNumber;
            tran.BuildingNumber = sten.BuildingNumber;
            tran.TenantNumber = sten.TenantNumber;
            tran.Date = DateTime.Now;
            tran.isChanged = true;
            if (trans == null)
                trans = new ArrayList();
            trans.Add(tran);
            grdMain.ItemsSource = null;
            //grdMain.ItemsSource = (STTrans[])trans.ToArray(typeof(STTrans));
            grdMain.ItemsSource = trans;
            grdMain.ScrollIntoView(trans[trans.Count -1]);
            needSaving = true;
        }

        private void cmdGetTenant_Click(object sender, RoutedEventArgs e)
        {
            txtTenantNumber_LostFocus(sender, e);
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            if (needSaving)
            {

            }
            txtBuildingNumber.Text = "";
            txtTenantNumber.Text = "";
            trans = null;
            grdMain.ItemsSource = null;
            MainGrid.DataContext = null;
            txtBuildingNumber.Focus();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            ctxAdd_Click(sender, e);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            STTrans tran;
            for (int i = 0; i < trans.Count; i++ )
            {
                tran = (STTrans)trans[i];
                if (tran.isChanged)
                    tran.save();
            }
            if (deletion_ids != null && deletion_ids.Count > 0)
            for (int i = 0; i < deletion_ids.Count; i++)
            {
                STTrans.delete((int)deletion_ids[i]);                
            }

            needSaving = false;
            cmdClear_Click(sender, e);
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int n = grdMain.SelectedIndex;
            if (n >= trans.Count || n < 0 || c.SelectedItem == null)
                return;
             
            ((STTrans)trans[n]).TransType = ((STCodes)c.SelectedItem).Key;
            ((STTrans)trans[n]).Description = ((STCodes)c.SelectedItem).FullDescription;
        }

        //
        // SINGLE CLICK EDITING
        //
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                DataGridCell cell = sender as DataGridCell;
                if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
                {
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }
                    DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                    if (dataGrid != null)
                    {
                        if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                        {
                            if (!cell.IsSelected)
                                cell.IsSelected = true;
                        }
                        else
                        {
                            DataGridRow row = FindVisualParent<DataGridRow>(cell);
                            if (row != null && !row.IsSelected)
                            {
                                row.IsSelected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void NumberEditor_ValueChanged1(object sender, VIBlend.WPF.Controls.ValueChangedEditorEventArgs args)
        {
            decimal total = 0;
            foreach (STTrans tran in trans)
            {
                total += tran.Amount;
            }
            txtTotal.Text = total.ToString("####0.00");
        }

        private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

            decimal total = 0;
            foreach (STTrans tran in trans)
            {
                total += tran.Amount;
            }
            txtTotal.Text = total.ToString("####0.00");
        }

        private void cmdGetList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stens = STTenants.loadByTenant(txtBuildingNumber.Text, txtTenantNumber.Text);
               
                frmChooseTenant frm = new frmChooseTenant();
                frm.SecTenants = stens;
                frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                frm.ShowDialog();
                if (frm.SequenceNumber == -1)
                {
                    return;
                }
                tenantId = frm.SequenceNumber;
                getTenant();
                grdMain.Focus();
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Security Transactions: " + ex.Message, false);
            }

        }
    }
}
