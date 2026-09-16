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
using MbcsCentral;
namespace MbcsCentral.WinGL
{
	public partial class frmDepartment
	{


		private bool Add = false;
		private Department m_departmentRec = null;

		private Company m_companyRec = null;

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }       
        public frmDepartment()
        {
            InitializeComponent();
            this.Loaded += frmDepartment_Loaded;
            this.Closing += frmDepartment_Closing;
        }

        public frmDepartment(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmDepartment_Loaded;
            this.Closing += frmDepartment_Closing;
        }	
	
        public Department DepartmentRecord {
			get { return m_departmentRec; }
			set { m_departmentRec = value; }
		}

        public Company CompanyRecord {
			get { return m_companyRec; }
			set { m_companyRec = value; }
		}

		private void frmDepartment_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (m_departmentRec.IsChanged) {
				switch (Interaction.MsgBox("Save your changes?", MsgBoxStyle.YesNoCancel)) {
					case MsgBoxResult.Yes:
						if (m_departmentRec.DepartmentNumber <= 0) {
							Interaction.MsgBox("Department Number cannot be blank.");
							e.Cancel = true;
							return;
						}
						if (!m_departmentRec.save()) {
							Interaction.MsgBox("Could not save Department Record.");
							e.Cancel = true;
						}
						break;
					case MsgBoxResult.Cancel:
						e.Cancel = true;
						return;

						break;
				}
			}
			Main.saveForm(this);

		}

		private void frmDepartment_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if ((m_companyRec == null)) {
				Interaction.MsgBox("Company not set for this buiilding.  Cannot continue.");
				this.Close();
			}

			Main.getForm(this);
			if ((m_departmentRec == null)) {
				m_departmentRec = new Department();
				m_departmentRec.CompanyNumber = CompanyRecord.CompanyNumber;
				Add = true;
				txtDepartmentNumber.IsReadOnly = false;
				txtDepartmentNumber.Focus();
			} else {
				txtDepartmentNumber.IsReadOnly = true;
				txtAddress1.Focus();
			}

			txtCompanyName.DataContext = m_companyRec;
			MainGrid.DataContext = m_departmentRec;

		}

		private void cmdCancel_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}

		private void cmdSave_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return;
            } if (m_departmentRec.DepartmentNumber <= 0)
            {
				Interaction.MsgBox("Department Number cannot be blank.");
				return;
			}

			if (!m_departmentRec.save())
            {
				Interaction.MsgBox("Could not save Department Record.");
			}
            else
            {
                CheckNumbers checkNumbers = CheckNumbers.load(m_departmentRec.CompanyNumber, m_departmentRec.DepartmentNumber);
                if (checkNumbers == null)
                {
                    checkNumbers = new CheckNumbers() { Company = m_departmentRec.CompanyNumber, Department = m_departmentRec.DepartmentNumber, CheckNumber = 0, BatchCheck = 800000 };
                }
				m_departmentRec.IsChanged = false;
				this.Close();
			}

		}


		private void txtDepartmentNumber_TextChanged(System.Object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (Add) {
				Department dpt = null;
			    short dptNumber;
			    if (short.TryParse(txtDepartmentNumber.Text, out dptNumber))
				    dpt = Department.loadByDepartment(dptNumber);
			    else
			    {
			        MessageBox.Show("Invalid Department number entered.");
			    }
				if ((dpt != null)) {
					Interaction.MsgBox("A department with that Department Number already exist.  Re-enter.");
					txtDepartmentNumber.Text = "";
				}
			}

		}
	}
}
