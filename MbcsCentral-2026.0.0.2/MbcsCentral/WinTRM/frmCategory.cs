namespace WinTRM
{
	public partial class frmCategory
	{

		private bool _add = false;
		private bool _fromIssue = false;
		private string _name;

		private ListItem itm;
		public bool @add {
			get { return _add; }
			set { _add = value; }
		}

		public string category {
			get { return _name; }
		}

		public bool fromIssue {
			set { _fromIssue = value; }
		}


		private void frmCategory_Load(object sender, System.EventArgs e)
		{
			getCategories();
			if (_add) {
				editCat();
			}

		}


		private void getCategories()
		{
			lstCategories.DataSource = null;
			lstCategories.Items.Clear();
			Categories[] category = Categories.load();
			ArrayList cats = new ArrayList();
			if ((category != null)) {
				Categories ct = null;
				foreach (Categories ct_loopVariable in category) {
					ct = ct_loopVariable;
					cats.Add(new ListItem(ct.getCategory(), ct.getCategoryId()));
					ct = null;
				}
				lstCategories.DataSource = cats;
			}

		}


		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			_add = true;
			editCat();

		}


		private void cmdEdit_Click(object sender, System.EventArgs e)
		{
			if (lstCategories.SelectedIndex >= 0) {
				editCat();
				itm = lstCategories.SelectedItem;
				txtCategory.Text = itm.DisplayItem;
				_add = false;
			}

		}


		private void editCat()
		{
			cmdAdd.Enabled = false;
			cmdDelete.Enabled = false;
			cmdSave.Enabled = true;
			cmdCancel.Enabled = true;
			txtCategory.Enabled = true;
			txtCategory.Focus();

		}


		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(txtCategory.Text.Trim)) {
				Interaction.MsgBox("Category cannot be blank.");
				return;
			}

			cmdSave.Enabled = false;
			cmdCancel.Enabled = false;
			txtCategory.Enabled = false;
			cmdAdd.Enabled = true;
			cmdDelete.Enabled = true;

			Categories category = null;
			if (_add) {
				category = new Categories(txtCategory.Text);
			} else {
				category = Categories.load(itm.ValueItem);
				if ((category != null)) {
					category.setCategory(txtCategory.Text);
				} else {
					Interaction.MsgBox("Could not find category (" + itm.ValueItem + ").  Not saved.");
					return;
				}
			}
			_name = txtCategory.Text;
			category.save();
			if (_fromIssue)
				this.Close();
			txtCategory.Text = "";
			getCategories();

		}


		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			cmdSave.Enabled = false;
			cmdCancel.Enabled = false;
			cmdAdd.Enabled = true;
			cmdDelete.Enabled = true;
			txtCategory.Enabled = false;
			txtCategory.Text = "";

		}


		private void cmdDelete_Click(object sender, System.EventArgs e)
		{
			itm = lstCategories.SelectedItem;
			if ((itm != null)) {
				if (itm.ValueItem > 0) {
					Categories category = Categories.load(itm.ValueItem);
					if ((category != null)) {
						category.delete(itm.ValueItem);
					}
					getCategories();
				}
			}
		}


		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			this.Close();

		}
		public frmCategory()
		{
			Load += frmCategory_Load;
		}
	}
}
