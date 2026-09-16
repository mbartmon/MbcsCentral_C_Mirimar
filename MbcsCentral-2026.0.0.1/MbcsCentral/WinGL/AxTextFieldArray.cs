using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
//UPGRADE_WARNING: The entire project must be compiled once before a form with an ActiveX Control Array can be displayed

using System.ComponentModel;
namespace WinGL
{

	[ProvideProperty("Index", typeof(AxMBText.AxTextField))]
	public class AxTextFieldArray : Microsoft.VisualBasic.Compatibility.VB6.BaseOcxArray, IExtenderProvider
	{

		public AxTextFieldArray() : base()
		{
		}

		public AxTextFieldArray(IContainer Container) : base(Container)
		{
		}

		public new event KeyPressEventEventHandler KeyPressEvent;
		public new delegate void KeyPressEventEventHandler(System.Object sender, AxMBText.__TextField_KeyPressEvent e);
		public new event KeyDownEventEventHandler KeyDownEvent;
		public new delegate void KeyDownEventEventHandler(System.Object sender, AxMBText.__TextField_KeyDownEvent e);
		public new event KeyUpEventEventHandler KeyUpEvent;
		public new delegate void KeyUpEventEventHandler(System.Object sender, AxMBText.__TextField_KeyUpEvent e);

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public bool CanExtend(object target)
		{
			if (target is AxMBText.AxTextField) {
				return BaseCanExtend(target);
			}
		}

		public short GetIndex(AxMBText.AxTextField o)
		{
			return BaseGetIndex(o);
		}

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public void SetIndex(AxMBText.AxTextField o, short Index)
		{
			BaseSetIndex(o, Index);
		}

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public bool ShouldSerializeIndex(AxMBText.AxTextField o)
		{
			return BaseShouldSerializeIndex(o);
		}

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public void ResetIndex(AxMBText.AxTextField o)
		{
			BaseResetIndex(o);
		}

		public AxMBText.AxTextField this[short Index] {
			get { return (AxMBText.AxTextField)BaseGetItem(Index); }
		}

		protected override System.Type GetControlInstanceType()
		{
			return typeof(AxMBText.AxTextField);
		}

		protected override void HookUpControlEvents(object o)
		{
			AxMBText.AxTextField ctl = (AxMBText.AxTextField)o;
			base.HookUpControlEvents(o);
			if ((KeyPressEventEvent != null)) {
				ctl.KeyPressEvent += new AxMBText.__TextField_KeyPressEventHandler(HandleKeyPressEvent);
			}
			if ((KeyDownEventEvent != null)) {
				ctl.KeyDownEvent += new AxMBText.__TextField_KeyDownEventHandler(HandleKeyDownEvent);
			}
			if ((KeyUpEventEvent != null)) {
				ctl.KeyUpEvent += new AxMBText.__TextField_KeyUpEventHandler(HandleKeyUpEvent);
			}
		}

		private void HandleKeyPressEvent(System.Object sender, AxMBText.__TextField_KeyPressEvent e)
		{
			if (KeyPressEvent != null) {
				KeyPressEvent(sender, e);
			}
		}

		private void HandleKeyDownEvent(System.Object sender, AxMBText.__TextField_KeyDownEvent e)
		{
			if (KeyDownEvent != null) {
				KeyDownEvent(sender, e);
			}
		}

		private void HandleKeyUpEvent(System.Object sender, AxMBText.__TextField_KeyUpEvent e)
		{
			if (KeyUpEvent != null) {
				KeyUpEvent(sender, e);
			}
		}

	}
}

