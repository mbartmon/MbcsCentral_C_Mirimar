// TODO Implement PRINTING - see Testing/WinPM/maintenance.xps
using Microsoft.Data.ConnectionUI;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
namespace MbcsCentral.WinPM
{
    public partial class frmMaintenance
    {

        Improvements[] imp = null;
        Maintenance[] maint = null;

        Tenants ten;
        private string m_BuildingNumber;
        private string m_TenantNumber;

        private int seq;
        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmMaintenance()
        {
            InitializeComponent();
            this.Loaded += frmMaintenance_Loaded;
            this.Closing += frmMaintenance_Closing;
        }
        public frmMaintenance(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string)parm;
            this.Loaded += frmMaintenance_Loaded;
            this.Closing += frmMaintenance_Closing;
        }
        public string BuildingNumber
        {
            set { m_BuildingNumber = value; }
        }
        public string TenantNumber
        {
            set { m_TenantNumber = value; }
        }
        public int TenantId
        {
            set { seq = value; }
        }

        private void frmMaintenance_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.saveForm(this);

        }

        private void frmMaintenance_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.getForm(this);
            txtBuildingNumber.Text = m_BuildingNumber;
            txtTenantNumber.Text = m_TenantNumber;
            ten = Tenants.load(seq);
            if ((ten != null))
            {
                txtApartment.Text = ten.Apartment;
                txtTenantName.Text = ten.TenantName;
                txtHomePhone.Text = ten.HomePhone1;
                txtBusinessPhone.Text = ten.BusPhone1;
            }
            else
            {
                txtTenantName.Text = "* NO CURRENT TENANT *";
            }

            getImprovementRecords();
            getMaintenanceRecords();
            if (imp != null)
            grdImprovement.ScrollIntoView(grdImprovement.Items[imp.Length - 1]);

        }


        private void getImprovementRecords()
        {
            grdImprovement.ItemsSource = null;
            try
            {
                imp = Improvements.load(m_BuildingNumber, m_TenantNumber, GlobalShared.user.Account.clientId);
            }
            catch (System.Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
            if ((imp != null))
            {
                grdImprovement.ItemsSource = imp;
                grdImprovement.ScrollIntoView(grdImprovement.Items[grdImprovement.Items.Count - 1]);
            }

        }

        private void getMaintenanceRecords()
        {
            grdMaintenance.ItemsSource = null;
            try
            {
                maint = Maintenance.load(m_BuildingNumber, m_TenantNumber, GlobalShared.user.Account.clientId);
            }
            catch (System.Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
            if ((maint != null))
            {
                grdMaintenance.ItemsSource = maint;
                grdMaintenance.ScrollIntoView(grdMaintenance.Items[grdMaintenance.Items.Count - 1]);
            }
        }


        private void grdImprovement_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (grdImprovement.SelectedIndex >= 0)
            {
                Improvements impSel = (Improvements)grdImprovement.SelectedItem;
                if ((impSel != null))
                {
                    frmImprovementEdit frm = new frmImprovementEdit(m_ThisApp);
                    frm.ImprovmentRecord = impSel;
                    frm.ShowDialog();
                    getImprovementRecords();
                }
            }

        }


        private void cmdImpAdd_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            frmImprovementEdit frm = new frmImprovementEdit(m_ThisApp);
            Improvements impSel = new Improvements();
            frm.ImprovmentRecord = impSel;
            frm.Tenant = ten;
            frm.Add = true;
            frm.ShowDialog();
            /*
            if (frm.Add & (frm.ImprovmentRecord != null))
            {
                Array.Resize(ref imp, imp.Length + 1);
                imp[imp.Length - 1] = frm.ImprovmentRecord;
            }
            */
            getImprovementRecords();

        }


        private void cmdExit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();

        }


        private void grdMaintenance_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (grdMaintenance.SelectedIndex >= 0)
            {
                Maintenance mntSel = (Maintenance)grdMaintenance.SelectedItem;
                if ((mntSel != null))
                {
                    frmMaintenanceEdit frm = new frmMaintenanceEdit(m_ThisApp);
                    frm.MaintenantRecord = mntSel;
                    frm.ShowDialog();
                    getMaintenanceRecords();
                }
            }

        }


        private void cmdMntAdd_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            frmMaintenanceEdit frm = new frmMaintenanceEdit(m_ThisApp);
            Maintenance mntSel = new Maintenance();
            frm.MaintenantRecord = mntSel;
            frm.Tenant = ten;
            frm.Add = true;
            frm.ShowDialog();

            /*
            if (frm.Add & (frm.MaintenantRecord != null))
            {
                Array.Resize(ref maint, maint.Length + 1);
                maint[maint.Length - 1] = frm.MaintenantRecord;
            }
            */
            getMaintenanceRecords();

        }


        private void cmdImpDelete_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (grdImprovement.SelectedIndex >= 0)
            {
                Improvements impSel = (Improvements)grdImprovement.SelectedItem;
                if ((impSel != null))
                {
                    if (Interaction.MsgBox("Delete Improvement Record?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
                    {
                        impSel.delete(impSel.LineNumber.ToString());
                        grdImprovement.ItemsSource = null;
                        getImprovementRecords();
                        grdImprovement.ItemsSource = imp;
                    }
                }
            }

        }


        private void cmdMntDelete_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (grdMaintenance.SelectedIndex >= 0)
            {
                Maintenance mntSel = (Maintenance)grdMaintenance.SelectedItem;
                if ((mntSel != null))
                {
                    if (Interaction.MsgBox("Delete Maintenance Record?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
                    {
                        mntSel.delete(mntSel.LineNumber.ToString());
                        grdMaintenance.ItemsSource = null;
                        getMaintenanceRecords();
                        grdMaintenance.ItemsSource = maint;
                    }
                }
            }

        }


        private void cmdImpEdit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (grdImprovement.SelectedIndex >= 0)
            {
                Improvements impSel = (Improvements)grdImprovement.SelectedItem;
                if ((impSel != null))
                {
                    frmImprovementEdit frm = new frmImprovementEdit(m_ThisApp);
                    frm.ImprovmentRecord = impSel;
                    frm.Show();
                }
            }

        }


        private void cmdMntEdit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if (grdMaintenance.SelectedIndex >= 0)
            {
                Maintenance mntSel = (Maintenance)grdMaintenance.SelectedItem;
                if ((mntSel != null))
                {
                    frmMaintenanceEdit frm = new frmMaintenanceEdit(m_ThisApp);
                    frm.MaintenantRecord = mntSel;
                    frm.Show();
                }
            }

        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceReport rpt = new MaintenanceReport() { BuildingNumber = ten.BuildingNumber, TenantNumber = ten.TenantNumber, Apartment = ten.Apartment, TenantName = ten.TenantName };
            rpt.print_report();
        }
    }
}
