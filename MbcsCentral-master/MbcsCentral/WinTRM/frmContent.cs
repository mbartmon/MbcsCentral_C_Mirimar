using WPDynamic;
using WinTRM.contents;
using WinTRM.Accounts;
namespace WinTRM
{
	public partial class frmContent
	{

		private long contentId = 0;
		private contents content = null;

		private bool allowEdit = true;

		public frmContent()
		{
			Load += frmContent_Load;
			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			txtRtf.EditorStart(GlobalShared.LICENSE_NAME, GlobalShared.LICENSE_KEY);
			txtRtf.SetEditorMode(EditorMode.wpmodSingleEditor, EditorXMode.wpmodexToolbarLG | EditorXMode.wpmodexTables, EditorGUI.wpguiVertScrollBar | EditorGUI.wpguiHorzScrollBar | EditorGUI.wpguiPanelH1, EditorGUI.wpguiDontSet);
			txtRtf.SetLayout(System.Windows.Forms.Application.StartupPath + "\\buttons.pcc", "");
			txtRtf.ShowHints = true;

		}

		public void setContentId(long id)
		{
			contentId = id;
		}

		public void setAllowEdit(bool allow)
		{
			allowEdit = allow;
		}


		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			if (!allowEdit)
				return;
			if (string.IsNullOrEmpty(txtDescription.Text.Trim)) {
				Interaction.MsgBox("You MUST enter a description.");
				return;
			}
			content.setaccount_id(Accounts.user.getAccountId());
			content.setcontent_type((optPlain.Checked ? contents.PLAIN_TEXT_FORMAT : contents.RICH_TEXT_FORMAT));
			content.setcontent((optPlain.Checked ? txtPlain.Text : txtRtf.GetText(0, "", false)));
			content.setDescription(txtDescription.Text);
			content.save();
			Close();


		}


		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			if ((optPlain.Checked ? txtPlain.Modified : txtRtf.Memo.Modified) & allowEdit) {
				switch (Interaction.MsgBox("Save changes?", MsgBoxStyle.YesNoCancel)) {
					case MsgBoxResult.Yes:
						cmdSave_Click(sender, e);
						break;
					case MsgBoxResult.Cancel:
						return;

						break;
					default:
						Close();
						break;
				}
			}
			Close();

		}

		private void optRTF_CheckedChanged(object sender, System.EventArgs e)
		{
			if (optRTF.Checked) {
				txtRtf.Text = txtPlain.Text;
				txtPlain.Visible = false;
			} else {
				txtPlain.Text = txtRtf.Memo.SaveToString(false, "ANSI");
				txtPlain.Visible = true;
			}
		}


		private void frmContent_Load(object sender, System.EventArgs e)
		{
			if (!allowEdit) {
				txtDescription.Enabled = false;
				txtPlain.Enabled = false;
				txtRtf.Enabled = false;
				cmdSave.Visible = false;
			}
			if (contentId > 0) {
				content = contents.load(contentId);
				if ((content == null)) {
					Interaction.MsgBox("Invalid content id: " + contentId);
					return;
				}
				txtDescription.Text = content.getDescription();
				switch (content.getcontent_type()) {
					case contents.PLAIN_TEXT_FORMAT:
						optPlain.Checked = true;
						txtPlain.Text = content.getcontent();
						txtPlain.Visible = true;
						break;
					case contents.RICH_TEXT_FORMAT:
						optRTF.Checked = true;
						txtRtf.Text = content.getcontent();
						txtPlain.Visible = false;
						break;
					default:
						Interaction.MsgBox("Invalid content type found: " + content.getcontent_type());
						break;
				}
			} else {
				content = new contents();
			}

		}
	}
}
