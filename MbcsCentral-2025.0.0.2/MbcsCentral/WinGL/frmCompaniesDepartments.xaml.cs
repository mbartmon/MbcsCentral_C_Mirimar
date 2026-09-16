using System;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MbcsCentral.WinGL
{
    public partial class frmCompaniesDepartments
    {

        Company[] companyRecs;

        Department[] dptRecs;
        Company companyRec;

        Department departmentRec;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }

        public frmCompaniesDepartments()
        {
            InitializeComponent();
            this.Loaded += frmCompaniesDepartments_Loaded;
            this.Closing += frmCompaniesDepartments_Closing;
        }

        public frmCompaniesDepartments(string parm)
        {
            InitializeComponent();
            this.Loaded += frmCompaniesDepartments_Loaded;
            this.Closing += frmCompaniesDepartments_Closing;
            m_ThisApp = (string)parm;
        }

        private void frmCompaniesDepartments_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.saveForm(this);

        }


        private void frmCompaniesDepartments_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.getForm(this);
            companyRecs = Company.load(false, true);
            if ((companyRecs != null))
            {
                lstCompanies.ItemsSource = companyRecs;
                lstCompanies.DisplayMemberPath = "Name";
            }

        }


        private void lstCompanies_SelectionChanged(System.Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstCompanies.SelectedIndex >= 0)
            {
                getDepartments();
            }

        }

        private void getDepartments()
        {
            lstDepartments.ItemsSource = null;
            companyRec = (Company)lstCompanies.SelectedItem;
            if ((companyRec != null))
            {
                dptRecs = Department.loadByCompany(companyRec.CompanyNumber, false, true);
                if ((dptRecs != null))
                {
                    lstDepartments.ItemsSource = dptRecs;
                    lstDepartments.DisplayMemberPath = "Name";
                }
            }

        }

        private void cmdExit_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();

        }

        private void cmdRemoveCompany_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            companyRec = (Company)lstCompanies.SelectedItem;
            if ((companyRec == null))
            {
                Interaction.MsgBox("You must select an company to remove.");
                return;
            }
            Department[] blds = Department.loadByCompany(companyRec.CompanyNumber);
            if ((blds != null) && blds.Length > 0)
            {
                Interaction.MsgBox("You must Move or Remove all Departments for this Company before you can Remove the company record.");
                return;
            }
            if (Interaction.MsgBox("Do you really want to remove <" + companyRec.Name + ">?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
            {
                companyRec.delete(companyRec.Id.ToString());
                lstCompanies.ItemsSource = null;
                companyRecs = Company.load(false, true);
                lstCompanies.ItemsSource = companyRecs;
            }
        }

        private void cmdRemoveDepartment_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            Department department = (Department)lstDepartments.SelectedItem;
            if ((department == null))
            {
                Interaction.MsgBox("You must select a Department to remove.");
                return;
            }
            if (MessageBoxCustom.Show("Do you really want to remove Department #" + department.DepartmentNumber + "  <" + department.Name + ">?", "",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (MessageBoxCustom.Show("Doing this will delete all G/L Transactions for this Company/Department.\n\r\n\r THIS CANNOT BE REVERSED.\n\r\n\r  Do you still want to remove this department?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                deleteDepartment(department);
                lstDepartments.ItemsSource = null;
                dptRecs = Department.loadByCompany(department.CompanyNumber, false, true);
            }
        }

        private void deleteDepartment(Department dept)
        {
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBoxCustom.Show("You do not have permission to perform that operation.");
                return;
            }

            int DepartmentCount = -1;
            int MemoCount = 0;
            int BatchReportCount = 0;
            int TransactionsCount = 0;
            if (GlobalShared.CN.State == System.Data.ConnectionState.Open)
                GlobalShared.CN.Close();
            GlobalShared.CN.ConnectionString = GlobalShared.SqlConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("DeleteGlDepartment", MbcsCentral.GlobalShared.CN);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@CompanyNumber", System.Data.SqlDbType.NVarChar).Value = dept.CompanyNumber;
            cmd.Parameters.Add("@DepartmentNumber", System.Data.SqlDbType.NVarChar).Value = dept.DepartmentNumber;
            cmd.Parameters.Add("@NumberOfTransactions", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@NumberOfMemos", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@NumberOfBatchReports", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@NumberOfDepartments", System.Data.SqlDbType.Int);
            cmd.Parameters["@NumberOfTransactions"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters["@NumberOfMemos"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters["@NumberOfBatchReports"].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters["@NumberOfDepartments"].Direction = System.Data.ParameterDirection.Output;

            try
            {
                MbcsCentral.GlobalShared.CN.Open();
                cmd.ExecuteNonQuery();
                TransactionsCount = (int)cmd.Parameters["@NumberOfTransactions"].Value;
                MemoCount = (int)cmd.Parameters["@NumberOfMemos"].Value;
                //BatchReportCount = (int)cmd.Parameters["@NumberOfBatchReports"].Value;
                DepartmentCount = (int)cmd.Parameters["@NumberOfDepartments"].Value;
                CheckNumbers checkNumbers = CheckNumbers.load(dept.CompanyNumber, dept.DepartmentNumber);
                if (checkNumbers != null)
                    checkNumbers.delete(checkNumbers.Id.ToString());
            }
            catch (SqlException ex)
            {
                Interaction.MsgBox("Could not remove building: " + ex.Message);
                MessageBoxCustom.Show("Error Count::" + ex.Errors.Count);
                string s = "";
                foreach (SqlError myError in ex.Errors)
                {
                    s += myError.Number.ToString("#####  ") + myError.Message + "\n\r\n\r";
                }
                MessageBoxCustom.Show(s, "", null);
                DepartmentCount = -1;
            }
            catch (Exception ex)
            {
                DepartmentCount = -1;
            }

            if (DepartmentCount > 0)
            {
                MessageBoxCustom.Show("Department removed along with: " + Constants.vbCrLf + Strings.Format(TransactionsCount, "##,##0").PadLeft(7) + " transactions\n\r" + Strings.Format(MemoCount, "##,##0").PadLeft(7) + " memos\n\r" + Strings.Format(BatchReportCount, "##,##0").PadLeft(7) + " Batch Reports");
            }
            else
                Interaction.MsgBox("Department not removed due to error condition.");
        }


        private void cmdEditCompany_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (lstCompanies.SelectedIndex >= 0)
            {
				companyRec = (Company)lstCompanies.SelectedItem;
				frmCompany frm = new frmCompany(m_ThisApp);
				frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
				frm.ID = companyRec.CompanyNumber;
				frm.ShowDialog();
				lstCompanies.ItemsSource = null;
				companyRecs = Company.load(false,true);
				lstCompanies.ItemsSource = companyRecs;
			}

		}

		private void cmdAddCompany_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } 
            frmCompany frm = new frmCompany(m_ThisApp);
			frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
			frm.Add = true;
			frm.ShowDialog();
			lstCompanies.ItemsSource = null;
			companyRecs = Company.load(false,true);
			lstCompanies.ItemsSource = companyRecs;

		}

		private void cmdEditDepartment_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if ((companyRec == null))
            {
				Interaction.MsgBox("Company not selected.");
				return;
			}
			if (lstDepartments.SelectedIndex >= 0) {
				departmentRec = (Department)lstDepartments.SelectedItem;
				frmDepartment frm = new frmDepartment(m_ThisApp);
				frm.DepartmentRecord = departmentRec;
				frm.CompanyRecord = companyRec;
				frm.ShowDialog();
				getDepartments();
			}

		}

		private void lstDepartments_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdEditDepartment_Click(sender, new System.Windows.RoutedEventArgs());

		}

		private void lstCompanies_MouseDoubleClick(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			cmdEditCompany_Click(sender, new System.Windows.RoutedEventArgs());

		}

		private void cmdMoveDepartment_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (lstDepartments.SelectedIndex >= 0)
            {
				Department bld = (Department)lstDepartments.SelectedItem;
				frmMoveDepartment frm = new frmMoveDepartment();
				frm.DepartmentRecord = bld;
				frm.Companies = companyRecs;
				frm.ShowDialog();
				getDepartments();
			}

		}

		private void cmdAddDepartment_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasRemovePermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            }
            if ((companyRec != null))
            {
				frmDepartment frm = new frmDepartment(m_ThisApp);
				frm.DepartmentRecord = null;
				frm.CompanyRecord = companyRec;
				frm.ShowDialog();
				getDepartments();
			}

		}

	}
}
