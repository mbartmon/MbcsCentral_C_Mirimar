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

	[ProvideProperty("Index", typeof(AxLpLib.AxfpCombo))]
	public class AxfpComboArray : Microsoft.VisualBasic.Compatibility.VB6.BaseOcxArray, IExtenderProvider
	{

		public AxfpComboArray() : base()
		{
		}

		public AxfpComboArray(IContainer Container) : base(Container)
		{
		}

		public new event ClickEventEventHandler ClickEvent;
		public new delegate void ClickEventEventHandler(System.Object sender, System.EventArgs e);
		public new event CloseUpEventHandler CloseUp;
		public new delegate void CloseUpEventHandler(System.Object sender, System.EventArgs e);
		public new event DropDownEventHandler DropDown;
		public new delegate void DropDownEventHandler(System.Object sender, System.EventArgs e);
		public new event KeyDownEventEventHandler KeyDownEvent;
		public new delegate void KeyDownEventEventHandler(System.Object sender, AxLpLib._DfpComboEvents_KeyDownEvent e);
		public new event KeyPressEventEventHandler KeyPressEvent;
		public new delegate void KeyPressEventEventHandler(System.Object sender, AxLpLib._DfpComboEvents_KeyPressEvent e);
		public new event SelChangeEventHandler SelChange;
		public new delegate void SelChangeEventHandler(System.Object sender, AxLpLib._DfpComboEvents_SelChangeEvent e);
		public new event ColWidthChangeEventHandler ColWidthChange;
		public new delegate void ColWidthChangeEventHandler(System.Object sender, AxLpLib._DfpComboEvents_ColWidthChangeEvent e);
		public new event DragDropColEventHandler DragDropCol;
		public new delegate void DragDropColEventHandler(System.Object sender, AxLpLib._DfpComboEvents_DragDropColEvent e);
		public new event TopChangeEventHandler TopChange;
		public new delegate void TopChangeEventHandler(System.Object sender, AxLpLib._DfpComboEvents_TopChangeEvent e);
		public new event DataLoadedEventHandler DataLoaded;
		public new delegate void DataLoadedEventHandler(System.Object sender, System.EventArgs e);
		public new event ChangeEventHandler Change;
		public new delegate void ChangeEventHandler(System.Object sender, System.EventArgs e);
		public new event VirtualRequestEventHandler VirtualRequest;
		public new delegate void VirtualRequestEventHandler(System.Object sender, AxLpLib._DfpComboEvents_VirtualRequestEvent e);
		public new event VirtualSearchEventHandler VirtualSearch;
		public new delegate void VirtualSearchEventHandler(System.Object sender, AxLpLib._DfpComboEvents_VirtualSearchEvent e);
		public new event DragDropGrpEventHandler DragDropGrp;
		public new delegate void DragDropGrpEventHandler(System.Object sender, AxLpLib._DfpComboEvents_DragDropGrpEvent e);
		public new event GrpWidthChangeEventHandler GrpWidthChange;
		public new delegate void GrpWidthChangeEventHandler(System.Object sender, AxLpLib._DfpComboEvents_GrpWidthChangeEvent e);
		public new event DataRowChangedEventHandler DataRowChanged;
		public new delegate void DataRowChangedEventHandler(System.Object sender, AxLpLib._DfpComboEvents_DataRowChangedEvent e);
		public new event DblClickEventHandler DblClick;
		public new delegate void DblClickEventHandler(System.Object sender, System.EventArgs e);
		public new event KeyUpEventEventHandler KeyUpEvent;
		public new delegate void KeyUpEventEventHandler(System.Object sender, AxLpLib._DfpComboEvents_KeyUpEvent e);
		public new event MouseDownEventEventHandler MouseDownEvent;
		public new delegate void MouseDownEventEventHandler(System.Object sender, AxLpLib._DfpComboEvents_MouseDownEvent e);
		public new event MouseMoveEventEventHandler MouseMoveEvent;
		public new delegate void MouseMoveEventEventHandler(System.Object sender, AxLpLib._DfpComboEvents_MouseMoveEvent e);
		public new event MouseUpEventEventHandler MouseUpEvent;
		public new delegate void MouseUpEventEventHandler(System.Object sender, AxLpLib._DfpComboEvents_MouseUpEvent e);

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public bool CanExtend(object target)
		{
			if (target is AxLpLib.AxfpCombo) {
				return BaseCanExtend(target);
			}
		}

		public short GetIndex(AxLpLib.AxfpCombo o)
		{
			return BaseGetIndex(o);
		}

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public void SetIndex(AxLpLib.AxfpCombo o, short Index)
		{
			BaseSetIndex(o, Index);
		}

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public bool ShouldSerializeIndex(AxLpLib.AxfpCombo o)
		{
			return BaseShouldSerializeIndex(o);
		}

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
		public void ResetIndex(AxLpLib.AxfpCombo o)
		{
			BaseResetIndex(o);
		}

		public AxLpLib.AxfpCombo this[short Index] {
			get { return (AxLpLib.AxfpCombo)BaseGetItem(Index); }
		}

		protected override System.Type GetControlInstanceType()
		{
			return typeof(AxLpLib.AxfpCombo);
		}

		protected override void HookUpControlEvents(object o)
		{
			AxLpLib.AxfpCombo ctl = (AxLpLib.AxfpCombo)o;
			base.HookUpControlEvents(o);
			if ((ClickEventEvent != null)) {
				ctl.ClickEvent += new System.EventHandler(HandleClickEvent);
			}
			if ((CloseUpEvent != null)) {
				ctl.CloseUp += new System.EventHandler(HandleCloseUp);
			}
			if ((DropDownEvent != null)) {
				ctl.DropDown += new System.EventHandler(HandleDropDown);
			}
			if ((KeyDownEventEvent != null)) {
				ctl.KeyDownEvent += new AxLpLib._DfpComboEvents_KeyDownEventHandler(HandleKeyDownEvent);
			}
			if ((KeyPressEventEvent != null)) {
				ctl.KeyPressEvent += new AxLpLib._DfpComboEvents_KeyPressEventHandler(HandleKeyPressEvent);
			}
			if ((SelChangeEvent != null)) {
				ctl.SelChange += new AxLpLib._DfpComboEvents_SelChangeEventHandler(HandleSelChange);
			}
			if ((ColWidthChangeEvent != null)) {
				ctl.ColWidthChange += new AxLpLib._DfpComboEvents_ColWidthChangeEventHandler(HandleColWidthChange);
			}
			if ((DragDropColEvent != null)) {
				ctl.DragDropCol += new AxLpLib._DfpComboEvents_DragDropColEventHandler(HandleDragDropCol);
			}
			if ((TopChangeEvent != null)) {
				ctl.TopChange += new AxLpLib._DfpComboEvents_TopChangeEventHandler(HandleTopChange);
			}
			if ((DataLoadedEvent != null)) {
				ctl.DataLoaded += new System.EventHandler(HandleDataLoaded);
			}
			if ((ChangeEvent != null)) {
				ctl.Change += new System.EventHandler(HandleChange);
			}
			if ((VirtualRequestEvent != null)) {
				ctl.VirtualRequest += new AxLpLib._DfpComboEvents_VirtualRequestEventHandler(HandleVirtualRequest);
			}
			if ((VirtualSearchEvent != null)) {
				ctl.VirtualSearch += new AxLpLib._DfpComboEvents_VirtualSearchEventHandler(HandleVirtualSearch);
			}
			if ((DragDropGrpEvent != null)) {
				ctl.DragDropGrp += new AxLpLib._DfpComboEvents_DragDropGrpEventHandler(HandleDragDropGrp);
			}
			if ((GrpWidthChangeEvent != null)) {
				ctl.GrpWidthChange += new AxLpLib._DfpComboEvents_GrpWidthChangeEventHandler(HandleGrpWidthChange);
			}
			if ((DataRowChangedEvent != null)) {
				ctl.DataRowChanged += new AxLpLib._DfpComboEvents_DataRowChangedEventHandler(HandleDataRowChanged);
			}
			if ((DblClickEvent != null)) {
				ctl.DblClick += new System.EventHandler(HandleDblClick);
			}
			if ((KeyUpEventEvent != null)) {
				ctl.KeyUpEvent += new AxLpLib._DfpComboEvents_KeyUpEventHandler(HandleKeyUpEvent);
			}
			if ((MouseDownEventEvent != null)) {
				ctl.MouseDownEvent += new AxLpLib._DfpComboEvents_MouseDownEventHandler(HandleMouseDownEvent);
			}
			if ((MouseMoveEventEvent != null)) {
				ctl.MouseMoveEvent += new AxLpLib._DfpComboEvents_MouseMoveEventHandler(HandleMouseMoveEvent);
			}
			if ((MouseUpEventEvent != null)) {
				ctl.MouseUpEvent += new AxLpLib._DfpComboEvents_MouseUpEventHandler(HandleMouseUpEvent);
			}
		}

		private void HandleClickEvent(System.Object sender, System.EventArgs e)
		{
			if (ClickEvent != null) {
				ClickEvent(sender, e);
			}
		}

		private void HandleCloseUp(System.Object sender, System.EventArgs e)
		{
			if (CloseUp != null) {
				CloseUp(sender, e);
			}
		}

		private void HandleDropDown(System.Object sender, System.EventArgs e)
		{
			if (DropDown != null) {
				DropDown(sender, e);
			}
		}

		private void HandleKeyDownEvent(System.Object sender, AxLpLib._DfpComboEvents_KeyDownEvent e)
		{
			if (KeyDownEvent != null) {
				KeyDownEvent(sender, e);
			}
		}

		private void HandleKeyPressEvent(System.Object sender, AxLpLib._DfpComboEvents_KeyPressEvent e)
		{
			if (KeyPressEvent != null) {
				KeyPressEvent(sender, e);
			}
		}

		private void HandleSelChange(System.Object sender, AxLpLib._DfpComboEvents_SelChangeEvent e)
		{
			if (SelChange != null) {
				SelChange(sender, e);
			}
		}

		private void HandleColWidthChange(System.Object sender, AxLpLib._DfpComboEvents_ColWidthChangeEvent e)
		{
			if (ColWidthChange != null) {
				ColWidthChange(sender, e);
			}
		}

		private void HandleDragDropCol(System.Object sender, AxLpLib._DfpComboEvents_DragDropColEvent e)
		{
			if (DragDropCol != null) {
				DragDropCol(sender, e);
			}
		}

		private void HandleTopChange(System.Object sender, AxLpLib._DfpComboEvents_TopChangeEvent e)
		{
			if (TopChange != null) {
				TopChange(sender, e);
			}
		}

		private void HandleDataLoaded(System.Object sender, System.EventArgs e)
		{
			if (DataLoaded != null) {
				DataLoaded(sender, e);
			}
		}

		private void HandleChange(System.Object sender, System.EventArgs e)
		{
			if (Change != null) {
				Change(sender, e);
			}
		}

		private void HandleVirtualRequest(System.Object sender, AxLpLib._DfpComboEvents_VirtualRequestEvent e)
		{
			if (VirtualRequest != null) {
				VirtualRequest(sender, e);
			}
		}

		private void HandleVirtualSearch(System.Object sender, AxLpLib._DfpComboEvents_VirtualSearchEvent e)
		{
			if (VirtualSearch != null) {
				VirtualSearch(sender, e);
			}
		}

		private void HandleDragDropGrp(System.Object sender, AxLpLib._DfpComboEvents_DragDropGrpEvent e)
		{
			if (DragDropGrp != null) {
				DragDropGrp(sender, e);
			}
		}

		private void HandleGrpWidthChange(System.Object sender, AxLpLib._DfpComboEvents_GrpWidthChangeEvent e)
		{
			if (GrpWidthChange != null) {
				GrpWidthChange(sender, e);
			}
		}

		private void HandleDataRowChanged(System.Object sender, AxLpLib._DfpComboEvents_DataRowChangedEvent e)
		{
			if (DataRowChanged != null) {
				DataRowChanged(sender, e);
			}
		}

		private void HandleDblClick(System.Object sender, System.EventArgs e)
		{
			if (DblClick != null) {
				DblClick(sender, e);
			}
		}

		private void HandleKeyUpEvent(System.Object sender, AxLpLib._DfpComboEvents_KeyUpEvent e)
		{
			if (KeyUpEvent != null) {
				KeyUpEvent(sender, e);
			}
		}

		private void HandleMouseDownEvent(System.Object sender, AxLpLib._DfpComboEvents_MouseDownEvent e)
		{
			if (MouseDownEvent != null) {
				MouseDownEvent(sender, e);
			}
		}

		private void HandleMouseMoveEvent(System.Object sender, AxLpLib._DfpComboEvents_MouseMoveEvent e)
		{
			if (MouseMoveEvent != null) {
				MouseMoveEvent(sender, e);
			}
		}

		private void HandleMouseUpEvent(System.Object sender, AxLpLib._DfpComboEvents_MouseUpEvent e)
		{
			if (MouseUpEvent != null) {
				MouseUpEvent(sender, e);
			}
		}

	}
}

