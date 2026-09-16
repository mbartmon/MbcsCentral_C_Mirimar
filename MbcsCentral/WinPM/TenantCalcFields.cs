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
using System.ComponentModel;
namespace MbcsCentral.WinPM
{
	public sealed class TenantCalcFields : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		private decimal _totalRent;
		private decimal _currentOneCharges;
		private decimal _arrearsOneCharges;
		private decimal _totalOneCharges;
		private decimal _currentRecCharges;
		private decimal _arrearsRecCharges;
		private decimal _totalRecCharges;
		private decimal _currentTotal;
		private decimal _arrearsTotal;
		private decimal _totalTotal;

		private decimal _balance;

		public TenantCalcFields()
		{
		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public decimal TotalRent {
			get { return _totalRent; }
			set {
				_totalRent = value;
				OnPropertyChanged("TotalRent");
			}
		}
		public decimal CurrentOneCharges {
			get { return _currentOneCharges; }
			set {
				_currentOneCharges = value;
				OnPropertyChanged("CurrentOneCharges");
			}
		}
		public decimal ArrearsOneCharges {
			get { return _arrearsOneCharges; }
			set {
				_arrearsOneCharges = value;
				OnPropertyChanged("ArrearsOneCharges");
			}
		}
		public decimal TotalOneCharges {
			get { return _totalOneCharges; }
			set {
				_totalOneCharges = value;
				OnPropertyChanged("TotalOneCharges");
			}
		}
		public decimal CurrentRecCharges {
			get { return _currentRecCharges; }
			set {
				_currentRecCharges = value;
				OnPropertyChanged("CurrentRecCharges");
			}
		}
		public decimal ArrearsRecCharges {
			get { return _arrearsRecCharges; }
			set {
				_arrearsRecCharges = value;
				OnPropertyChanged("ArrearsRecCharges");
			}
		}
		public decimal TotalRecCharges {
			get { return _totalRecCharges; }
			set {
				_totalRecCharges = value;
				OnPropertyChanged("TotalRecCharges");
			}
		}
		public decimal CurrentTotal {
			get { return _currentTotal; }
			set {
				_currentTotal = value;
				OnPropertyChanged("CurrentTotal");
			}
		}
		public decimal ArrearsTotal {
			get { return _arrearsTotal; }
			set {
				_arrearsTotal = value;
				OnPropertyChanged("ArrearsTotal");
			}
		}
		public decimal TotalTotal {
			get { return _totalTotal; }
			set {
				_totalTotal = value;
				OnPropertyChanged("TotalTotal");
			}
		}
		public decimal Balance {
			get { return _balance; }
			set {
				_balance = value;
				OnPropertyChanged("Balance");
			}
		}
	}
}
