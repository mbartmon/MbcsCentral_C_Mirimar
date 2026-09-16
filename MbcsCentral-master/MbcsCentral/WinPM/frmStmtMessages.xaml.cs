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

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmStmtMessages.xaml
    /// </summary>
    public partial class frmStmtMessages : Window
    {

        long clientId;
        Control control;
        LegalMsg[] legals;
        TypeMsg[] types;
        BuildingMsg[] buildings;

        LegalMsg LegalGridRow;
        TypeMsg TypeGridRow;
        BuildingMsg BuildingGridRow;

        CollectionViewSource TypesSource;
        CollectionViewSource LegalSource;
        CollectionViewSource BuildingSource;

        const string PROGRAM_RESGISTRY_KEY = "\\StmtMessages\\";
        const string PRIOR_PAYMENTS_KEY = "PriorPayments";

        public struct colorChart
        {
            public System.Windows.Media.Color Black;
            public System.Windows.Media.Color Red;
            public System.Windows.Media.Color Green;
            public System.Windows.Media.Color Yellow;
            public System.Windows.Media.Color Blue;
            public System.Windows.Media.Color Cyan;

            public colorChart(bool x)
            {

              Black = System.Windows.Media.Colors.Black;
              Red = System.Windows.Media.Colors.Red;
              Green = System.Windows.Media.Colors.Green;
              Yellow = System.Windows.Media.Colors.Yellow;
              Blue = System.Windows.Media.Colors.Blue;
              Cyan = System.Windows.Media.Colors.Cyan;
          }
        }
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
        public frmStmtMessages()
        {
            InitializeComponent();
            clientId = GlobalShared.user.Account.clientId;
            control = Control.load(clientId);
        }
        public frmStmtMessages(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            clientId = GlobalShared.user.Account.clientId;
            control = Control.load(clientId);
        }
        private void formStmtMessages_Loaded(object sender, RoutedEventArgs e)
        {
            if (control == null)
            {
                MessageBox.Show("Error retrieving Control information. This window will now close.");
                this.Close();
            }
            Main.getForm(this, false);
            setData();
        }

        private void setData()
        {
            string obj = (string)Main.GetRegistryEntry(GlobalShared.APPNAME + PROGRAM_RESGISTRY_KEY, PRIOR_PAYMENTS_KEY, "0", true);
            chkPrior.IsChecked = (bool)(obj == "1" ? true : false);

            legals = LegalMsg.loadAll(clientId);
            types = TypeMsg.loadAll(clientId);
            buildings = BuildingMsg.load();


            ChargeCodes[] legalCodes = ChargeCodes.loadLegal(clientId);

            LegalSource = ((CollectionViewSource)(this.FindResource("LegalSource")));
            LegalSource.Source = legalCodes;

            Types[] t = Types.loadAll();
            TypesSource = ((CollectionViewSource)(this.FindResource("TypesSource")));
            TypesSource.Source = t;

            Building[] builds = Building.load();
            BuildingSource = ((CollectionViewSource)(this.FindResource("BuildingSource")));
            BuildingSource.Source = builds;

            pnlGenMessage.DataContext = control;
            pnlGenLocation.DataContext = control;
            pnlLglLocation.DataContext = control;
            pnlTypeLocation.DataContext = control;
            pnlBuildingLocation.DataContext = control;
            grdLegalMessages.DataContext = legals;
            grdTypeMessages.DataContext = types;
            grdBuildingMessages.DataContext = buildings;

            System.Windows.Media.Color clr;

            colorChart chart = new colorChart(true);
            clr = FromWin32(control.GeneralColor);
            if (clr == chart.Black)
                GenColorBlack.IsChecked = true;
            else if (clr == chart.Red)
                GenColorRed.IsChecked = true;
            else if (clr == chart.Green)
                GenColorGreen.IsChecked = true;
            else if (clr == chart.Yellow)
                GenColorYellow.IsChecked = true;
            else if (clr == chart.Blue)
                GenColorBlue.IsChecked = true;
            else if (clr == chart.Cyan)
                GenColorCyan.IsChecked = true;

            clr = FromWin32(control.LegalColor);
            if (clr == chart.Black)
                LglColorBlack.IsChecked = true;
            else if (clr == chart.Red)
                LglColorRed.IsChecked = true;
            else if (clr == chart.Green)
                LglColorGreen.IsChecked = true;
            else if (clr == chart.Yellow)
                LglColorYellow.IsChecked = true;
            else if (clr == chart.Blue)
                LglColorBlue.IsChecked = true;
            else if (clr == chart.Cyan)
                LglColorCyan.IsChecked = true;

            clr = FromWin32(control.TypeColor);
            if (clr == chart.Black)
                TypeColorBlack.IsChecked = true;
            else if (clr == chart.Red)
                TypeColorRed.IsChecked = true;
            else if (clr == chart.Green)
                TypeColorGreen.IsChecked = true;
            else if (clr == chart.Yellow)
                TypeColorYellow.IsChecked = true;
            else if (clr == chart.Blue)
                TypeColorBlue.IsChecked = true;
            else if (clr == chart.Cyan)
                TypeColorCyan.IsChecked = true;

            clr = FromWin32(control.BuildingMsgColor);
            if (clr == chart.Black)
                BuildingColorBlack.IsChecked = true;
            else if (clr == chart.Red)
                BuildingColorRed.IsChecked = true;
            else if (clr == chart.Green)
                BuildingColorGreen.IsChecked = true;
            else if (clr == chart.Yellow)
                BuildingColorYellow.IsChecked = true;
            else if (clr == chart.Blue)
                BuildingColorBlue.IsChecked = true;
            else if (clr == chart.Cyan)
                BuildingColorCyan.IsChecked = true;

        }

        private System.Windows.Media.Color FromWin32(int msgColor)
        {
            System.Drawing.Color clr;

			clr = new System.Drawing.Color();
            clr = System.Drawing.ColorTranslator.FromWin32(msgColor);
            return System.Windows.Media.Color.FromArgb(clr.A, clr.R, clr.G, clr.B);
        }

        private int ToWin32(System.Windows.Media.Color clr)
        {
            return System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.FromArgb(clr.A, clr.R, clr.G, clr.B));
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Main.SetRegistryEntry(GlobalShared.APPNAME + PROGRAM_RESGISTRY_KEY, PRIOR_PAYMENTS_KEY, (chkPrior.IsChecked.Value ? "1" : "0"), (int)Registry.DataTypes.String, true);

            colorChart chart = new colorChart(true);
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if ((bool)GenColorBlack.IsChecked)
                control.GeneralColor = ToWin32(chart.Black);
            else if ((bool)GenColorRed.IsChecked)
                control.GeneralColor = ToWin32(chart.Red);
            else if ((bool)GenColorGreen.IsChecked)
                control.GeneralColor = ToWin32(chart.Green);
            else if ((bool)GenColorYellow.IsChecked)
                control.GeneralColor = ToWin32(chart.Yellow);
            else if ((bool)GenColorBlue.IsChecked)
                control.GeneralColor = ToWin32(chart.Blue);
            else if ((bool)GenColorCyan.IsChecked)
                control.GeneralColor = ToWin32(chart.Cyan);

            if ((bool)LglColorBlack.IsChecked)
                control.LegalColor = ToWin32(chart.Black);
            else if ((bool)LglColorRed.IsChecked)
                control.LegalColor = ToWin32(chart.Red);
            else if ((bool)LglColorGreen.IsChecked)
                control.LegalColor = ToWin32(chart.Green);
            else if ((bool)LglColorYellow.IsChecked)
                control.LegalColor = ToWin32(chart.Yellow);
            else if ((bool)LglColorBlue.IsChecked)
                control.LegalColor = ToWin32(chart.Blue);
            else if ((bool)LglColorCyan.IsChecked)
                control.LegalColor = ToWin32(chart.Cyan);

            if ((bool)TypeColorBlack.IsChecked)
                control.TypeColor = ToWin32(chart.Black);
            else if ((bool)TypeColorRed.IsChecked)
                control.TypeColor = ToWin32(chart.Red);
            else if ((bool)TypeColorGreen.IsChecked)
                control.TypeColor = ToWin32(chart.Green);
            else if ((bool)TypeColorYellow.IsChecked)
                control.TypeColor = ToWin32(chart.Yellow);
            else if ((bool)TypeColorBlue.IsChecked)
                control.TypeColor = ToWin32(chart.Blue);
            else if ((bool)TypeColorCyan.IsChecked)
                control.TypeColor = ToWin32(chart.Cyan);

            if ((bool)BuildingColorBlack.IsChecked)
                control.BuildingMsgColor = ToWin32(chart.Black);
            else if ((bool)BuildingColorRed.IsChecked)
                control.BuildingMsgColor = ToWin32(chart.Red);
            else if ((bool)BuildingColorGreen.IsChecked)
                control.BuildingMsgColor = ToWin32(chart.Green);
            else if ((bool)BuildingColorYellow.IsChecked)
                control.BuildingMsgColor = ToWin32(chart.Yellow);
            else if ((bool)BuildingColorBlue.IsChecked)
                control.BuildingMsgColor = ToWin32(chart.Blue);
            else if ((bool)BuildingColorCyan.IsChecked)
                control.BuildingMsgColor = ToWin32(chart.Cyan);

            control.save();
            if (legals != null && legals.Length > 0)
                foreach (LegalMsg lgl in legals)
                {
                    if (lgl.Code == null || lgl.Code.Trim().Equals(""))
                    {
                        if (MessageBoxCustom.Show("Legal Message <" + lgl.Message + "> has a BLANK CODE. Ignore?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                            return;
                        continue;
                    }
                    if (!lgl.save())
                    {
                        if (lgl.IsDupe)
                        {
                            if (MessageBoxCustom.Show("Legal Message <" + lgl.Code + ": " + lgl.Message + "> has a DUPLICATE CODE. Ignore?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                                return;
                            continue;
                        }
                    }
                }
            if (types != null && types.Length > 0)
                foreach (TypeMsg type in types)
                {
                    type.save();

                    if (type.Type == null || type.Type.Trim().Equals(""))
                    {
                        if (MessageBoxCustom.Show("Type Message <" + type.Message + "> has a BLANK CODE. Ignore?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                            return;
                        continue;
                    }
                    if (!type.save())
                    {
                        if (type.IsDupe)
                        {
                            if (MessageBoxCustom.Show("Type Message <" + type.Type + ": " + type.Message + "> has a DUPLICATE CODE. Ignore?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                                return;
                            continue;
                        }
                    }

                }
            if (buildings != null && buildings.Length > 0)
                foreach (BuildingMsg building in buildings)
                {
                    if (building.BuildingNumber == null || building.BuildingNumber.Trim().Equals(""))
                    {
                        if (MessageBoxCustom.Show("Building Message <" + building.Message + "> has a BLANK Building Number. Ignore?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                            return;
                        continue;
                    }
                    if (!building.save())
                    {
                        if (building.IsDupe)
                        {
                            if (MessageBoxCustom.Show("Building Message <" + building.BuildingNumber + ": " + building.Message + "> has a DUPLICATE Building Number. Ignore?", "", MessageBoxButton.YesNo, this) == MessageBoxResult.No)
                                return;
                            continue;
                        }

                    }
                }

            this.Close();
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            Main.saveForm(this);
            this.Close();
        }

        private void ctxLegalDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LegalGridRow != null)
            {
                LegalGridRow.delete(LegalGridRow.ID.ToString());
                setData();
            }
        }

        private void ctxLegalInsert_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            Array.Resize<LegalMsg>(ref legals, legals.Length + 1);
            legals[legals.Length - 1] = new LegalMsg(clientId, " ", " ");
            //legal.save();
            //setData();
        }

        private void ctxTypeDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (TypeGridRow != null)
            {
                TypeGridRow.delete(TypeGridRow.ID.ToString());
                setData();
            }
        }

        private void ctxTypeInsert_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            Array.Resize<TypeMsg>(ref types, types.Length + 1);
            types[types.Length - 1] =  new TypeMsg(clientId, " ", " ");
            grdTypeMessages.DataContext = types;
            //type.save();
            //setData();
        }

        private void ctxBuildingDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (BuildingGridRow != null)
            {
                BuildingGridRow.delete(BuildingGridRow.ID.ToString());
                setData();
            }

        }

        private void ctxBuildingInsert_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            Array.Resize<BuildingMsg>(ref buildings, buildings.Length + 1);
            buildings[buildings.Length - 1] = new BuildingMsg((int)clientId,0, " ", " ");
            grdBuildingMessages.DataContext = buildings;

            //building.save();
            //setData();

        }
        private void grdLegalMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grd = (DataGrid)sender;
            if (grd.SelectedItem != null)
                LegalGridRow = (LegalMsg)grd.SelectedItem;
        }

        private void grdTypeMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grd = (DataGrid)sender;
            if (grd.SelectedItem != null)
                TypeGridRow = (TypeMsg)grd.SelectedItem;
        }

        private void grdBuildingMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grd = (DataGrid)sender;
            if (grd.SelectedItem != null)
                BuildingGridRow = (BuildingMsg)grd.SelectedItem;
        }

        private void Location_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int result = 0;

            if (!int.TryParse(txt.Text, out result))
            {
                MessageBox.Show("Location must be numeric.");
                txt.Text = "1";
                txt.Focus();
            }
            else if (txt.Name.Trim().ToLower().Equals("genlocation") &&  (result < 1 || result > 3))
            {
                MessageBox.Show("Location must be 1, 2, or 3");
                txt.Text = "1";
                txt.Focus();
            }
            else if (result < 1 || result > 4)
            {
                MessageBox.Show("Location must be 1, 2, 3 or 4");
                txt.Text = "1";
                txt.Focus();

            }

        }

    }
}
