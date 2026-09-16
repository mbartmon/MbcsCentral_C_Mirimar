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
using System.Collections.ObjectModel;
namespace MbcsCentral
{

	public class HistoryList : ObservableCollection<HistoryDisplay>
	{


		private ListCollectionView _View;

		public HistoryList()
		{
		}


		public HistoryList(int rowType, System.DateTime historyDate, string historyDescription, object amount, object arrears, object paid, object paidDate, decimal balance ,string notes)
		{
			base.Add(new HistoryDisplay(rowType, historyDate, historyDescription, amount, arrears, paid, paidDate, balance, notes));

		}


		public void Add(int rowType, System.DateTime historyDate, string historyDescription, object amount, object arrears, object paid, object paidDate, decimal balance, string notes)
		{
			base.Add(new HistoryDisplay(rowType, historyDate, historyDescription, amount, arrears, paid, paidDate, balance,notes));

		}

		public ListCollectionView ListView {
			get {
				if ((_View == null)) {
					_View = new ListCollectionView(this);
				}
				return _View;
			}
		}

		public HistoryDisplay GetFirst {
			get { return this.Items[0]; }
		}
	}
}
namespace MbcsCentral
{

	public class HistoryDisplay
	{

		private int m_rowType;
		private System.DateTime m_historyDate;
		private string m_historyDescription;
		private object m_amount;
		private object m_arrears;
		private object m_paid;
		private object m_paidDate;
		private decimal m_balance;
        private string m_notes;

        private string m_historyMonthYear;
		public enum RowTypes : int
		{
			Charges = 0,
			Payments = 1,
			Total = 2
		}


		public HistoryDisplay()
		{
		}

		public HistoryDisplay(int rowType, System.DateTime historyDate, string historyDescription, object amount, object arrears, object paid, object paidDate, decimal balance, string notes)
		{
			m_rowType = rowType;
			m_historyDate = historyDate;
			m_historyDescription = historyDescription;
			m_amount = ((amount == null) ? 0 : Convert.ToDecimal(amount));
			m_arrears = ((arrears == null) ? 0 : Convert.ToDecimal(arrears));
			m_paid = ((paid == null) ? 0 : Convert.ToDecimal(paid));
			m_paidDate = ((paidDate == null) ? new DateTime(1900,1,1,0,0,0) : Convert.ToDateTime(paidDate));
			m_balance = balance;
			m_notes = notes;
			m_historyMonthYear = Strings.Format(m_historyDate, "MM/yyyy");
			if (m_rowType == (int)RowTypes.Payments) {
                m_historyDate = new DateTime(1900, 1, 1, 0, 0, 0);
			}
		}

		public int RowType {
			get { return m_rowType; }
			set { m_rowType = value; }
		}
		public System.DateTime HistoryDate {
			get { return m_historyDate; }
			set {
				m_historyDate = value;
				m_historyMonthYear = Strings.Format(value, "MM/yyyy");
			}
		}
		public string HistoryMonthYear {
			get { return m_historyMonthYear; }
			set { m_historyMonthYear = value; }
		}
		public string HistoryDescription {
			get { return m_historyDescription; }
			set { m_historyDescription = value; }
		}
		public object Amount {
			get { return m_amount; }
			set { m_amount = value; }
		}
		public object Arrears {
			get { return m_arrears; }
			set { m_arrears = value; }
		}
		public object Paid {
			get { return m_paid; }
			set { m_paid = value; }
		}
		public object PaidDate {
			get { return m_paidDate; }
			set { m_paidDate = value; }
		}
		public decimal Balance {
			get { return m_balance; }
			set { m_balance = value; }
		}
        public string Notes
        {
            get { return m_notes; }
            set { m_notes = value; }
        }
    }
}
