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
namespace MbcsCentral
{
	public partial class frmOwner
	{

		private bool m_add;
		public bool Add {
			get { return m_add; }
			set { m_add = value; }
		}
		private int m_id;
		public int ID {
			set { m_id = value; }
		}

        private string m_ThisApp;
        public string ThisApp
        {
            set { m_ThisApp = value; }
        }
		private new Owners owner = null;

        public frmOwner()
        {
            InitializeComponent();
            this.Loaded += frmOwner_Loaded;
            this.Closing += frmOwner_Closing;
        }
        public frmOwner(string parm)
        {
            InitializeComponent();
            m_ThisApp = (string) parm;
            this.Loaded += frmOwner_Loaded;
            this.Closing += frmOwner_Closing;
        }

        private bool SaveOwner()
		{
            if (!GlobalShared.user.hasModifyPermission(m_ThisApp))
            {
                MessageBox.Show("You do not have have permission to perform this operation.");
                return true;
            }
			if (m_add) {
				Owners xOwner = Owners.loadByOwnerNumber(owner.OwnerNumber,GlobalShared.user.Account.clientId);
				if ((xOwner != null) && xOwner.OwnerNumber == owner.OwnerNumber) {
					Interaction.MsgBox("There is already an Owner with that Owner Number.");
					return false;
				}
			}
            owner.clientId = 1;
			return owner.save();

		}

		private void frmOwner_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ((owner != null) && owner.IsChanged) {
				switch (Interaction.MsgBox("Save changes?", MsgBoxStyle.YesNoCancel)) {
					case MsgBoxResult.Yes:
						if (!SaveOwner()) {
							Interaction.MsgBox("Owner record not saved");
							e.Cancel = true;
							return;
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

		private void frmOwner_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);
			if (m_add) {
				owner = new Owners();
				txtOwnerNumber.Focus();
			} else {
				owner = Owners.loadOne(m_id);
				txtOwnerNumber.IsReadOnly = true;
				txtOwnerName.Focus();
			}

			MainGrid.DataContext = owner;


		}


		private void cmdOk_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			if (!SaveOwner())
				Interaction.MsgBox("Owner record not saved.");
            this.Close();
		}


		private void cmdCancel_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Main.PrintWindow(this);
        }
	}
}
