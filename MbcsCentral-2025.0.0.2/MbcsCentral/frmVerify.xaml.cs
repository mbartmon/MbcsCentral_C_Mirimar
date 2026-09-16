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
using CSharp;

namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for frmVerify.xaml
    /// </summary>
    public partial class frmVerify : Window
    {
        public bool Valid { get; set; }

        public frmVerify()
        {
            InitializeComponent();
        }

        private void cmdValidate_Click(object sender, RoutedEventArgs e)
        {
            string s = txtSet1.Text + txtSet2.Text + txtSet3.Text + txtSet4.Text + txtSet5.Text;
            int rc = RegistrationValidator.Validate(GlobalShared.ValidateConnectionString, s, "FullVersion", true);
            if (rc < 0)
            {
                if (MessageBox.Show("Invalid Key Code entered.  Try again?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    Valid = false;
                    this.Close();
                }
                return;
            }
            //RegistrationValidator.SetRegistry(s, GlobalShared.validationKeyLoc);
            Main.SetRegistryEntry(GlobalShared.validationKeyLoc, "Setting", s, (int)Registry.DataTypes.String);
            Valid = true;
            this.Close();
       }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            Valid = false;
            this.Close();
        }

        private void moveNextOnMaxLength(object sender, TextChangedEventArgs e)
        {
            Main.moveNextOnMaxlength(sender, e);

        }
    }
}
