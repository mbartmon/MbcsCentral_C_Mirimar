using System;
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

namespace MbcsCentral.WinGL
{
    /// <summary>
    /// Interaction logic for frmLaserChk.xaml
    /// </summary>
    public partial class frmLaserChk : Window
    {
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public int CheckNumber { get; set; }
        public DateTime CheckDate { get; set; }

        public frmLaserChk()
        {
            InitializeComponent();
        }

        private void formLaserChk_Loaded(object sender, RoutedEventArgs e)
        {
            Main.getForm(this);
            txtCompany.Text = CompanyName;
            txtDepartment.Text = DepartmentName;
            txtCheckNumber.Text = CheckNumber.ToString("#########0");
            dtCheckDate.SelectedDate = CheckDate;
        }

        private void formLaserChk_Closed(object sender, EventArgs e)
        {
            int cn;
            int.TryParse(txtCheckNumber.Text, out cn);
            CheckNumber = cn;
            CheckDate = dtCheckDate.SelectedDate.Value;
            Main.saveForm(this);
        }

        private void cmdContinue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
