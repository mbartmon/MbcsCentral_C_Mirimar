using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmEOM.xaml
    /// </summary>
    public partial class frmEOM : Window
    {
        System.DateTime bildate;
        System.DateTime LastDate;
        System.DateTime clsdate;
        Control control;
        
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        private string m_EOMflag;
        public string EOMflag
        {
            set { m_EOMflag = value; }
        }

        private double m_progress;
        public double Progress
        {
            set { m_progress = value;
                prgProgress.Dispatcher.Invoke(new CrossAppDomainDelegate(setProgress), DispatcherPriority.Background,
                                              null);
            }
        }

        private string m_progressMsg;
        public string progressMsg
        {
            set { m_progressMsg = value;
                txtProgress.Dispatcher.Invoke(new CrossAppDomainDelegate(setProgressMsg), DispatcherPriority.Background,
                                              null);
            }
        }
      
        public frmEOM()
        {
            InitializeComponent();
        }
        public frmEOM(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
        }

        public frmEOM(string parm1, string parm2)
        {
            InitializeComponent();
            m_ThisApp = (string)parm1;
            m_EOMflag = (string)parm2;
        }
        
        public void setProgress()
        {
            prgProgress.Value = m_progress;
            prgProgress.InvalidateVisual();
        }
        private void setProgressMsg()
        {
            txtProgress.Text = m_progressMsg;
        }

        private void formEOM_Loaded(object sender, RoutedEventArgs e)
	    {
            Main.getForm(this);

            control = Control.load(GlobalShared.user.Account.clientId);

	        if (control == null)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition,"Could not load control record.", false, Microsoft.VisualBasic.MsgBoxStyle.OkOnly);
                this.Close();
            }

            mainGrid.DataContext = control;

		    switch (m_EOMflag) {
			    case "E":
				    // Reorg
				    this.Title = "End of Month Reorganization";
				    cmdProcess.Content = "REORG";
				    LastDate = (control.LastReorgDate == null ? DateTime.Now : control.LastReorgDate);
				    break;
			    case "R":
				    // Roll Rents
				    this.Title = "Roll Rents";
				    cmdProcess.Content = "ROLL";
				    LastDate = (control.LastRollDate == null ? DateTime.Now : control.LastRollDate);
				    break;
			    case "S":
				    // Statements
				    this.Title = "Print Rent Bills";
				    cmdProcess.Content = "PRINT";
				    LastDate = (control.LastReorgDate == null ? DateTime.Now : control.LastReorgDate);
				    break;
		    }
	    }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {

            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
           // REORG/ROLL/PRINT
            clsdate = (DateTime)control.ClosingDate;
            bildate = control.BillingDate;
            control.save();
            this.Cursor = Cursors.Wait;
            switch (m_EOMflag)
            {
                case "E":
                    // REORG
                    if (LastDate.Month == bildate.Month && LastDate.Year == bildate.Year)
                    {
                        if (MessageBox.Show("Month End Processing done for current billing cycle.  Are you sure you want to do it again?", "Month End Processing", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    ProcessEOM.EomReorg(bildate, clsdate, this);
                    break;
                case "R":
                    // ROLL
                    if (LastDate.Month == bildate.Month && LastDate.Year == bildate.Year)
                    {
                        if (MessageBox.Show("Rents have been rolled for current billing cycle.  Are you sure you want to do it again?", "Roll Rents", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    ProcessEOM.EomRoll(bildate, clsdate, this);
                    break;
                case "S":
                    {
                        // PRINT
                        if (LastDate.Month != bildate.Month || LastDate.Year != bildate.Year)
                        {
                            if (MessageBox.Show("Month End Processing was not done for current billing cycle.  Are you sure you want to print bills?", "Print Rent Bills", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            {
                                return;
                            }
                        }
                        frmStmtLaser frm = new frmStmtLaser();
                        frm.Show();
                        break;
                        /*
                        switch (m_EOMflag)
                        {
                            case "L":
                                // Laser
                                frmStmtLaser frm = new frmStmtLaser();
                                frm.Show();
                                switch (m_EOMflag)
                                {
                                    case "1":
                                        //WinpmStmtsLaser.Show();
                                        break;
                                    case "2":
                                        //WinpmStmtsLaser2.Show();
                                        break;
                                }
                                break;
                            case "C":
                                // continuous
                                //WinpmStmts.Show();
                                break;
                        }
                        break;
                                   */
                    }
            }
            this.Cursor = Cursors.Arrow;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
            this.Close();
        }

    }

}
