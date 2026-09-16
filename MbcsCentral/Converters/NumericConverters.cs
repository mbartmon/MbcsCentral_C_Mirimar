using System;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.VisualBasic;
namespace MbcsCentral
{
    public class CurrencyPaddingConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            bool useCommas = false;
            decimal amount = System.Convert.ToDecimal(value);
            string s = (string)parameter;
            if (s.ToLower().Contains("c"))
            {
                useCommas = true;
                s = s.Replace("C", "").Replace("c", "");
            }
            int l = System.Convert.ToInt32(s);

            if (l > 0)
            {
                string d;
                d = (string)(Strings.StrDup(l - 4, "#") + "0.00");
                if (useCommas)
                    d = d.Insert(d.Length - 5, ",");
                return Strings.Format(amount, d).PadLeft(l);
            }
            else
            {
                return "";
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            decimal amount = 0;
            try
            {
                amount = System.Convert.ToDecimal(value);
            }
            catch
            {
            }
            return amount;

        }
    }
    public class AmountConverter : IValueConverter
    {


        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            //Dim Hist As HistoryDisplay = CType(parameter, HistoryDisplay)
            if (value != null)
            {
                decimal amount = System.Convert.ToDecimal(value);

                switch (0)
                {
                    case 0: // Charges
                        return string.Format("{0,10}", Strings.Format(amount, "##,##0.00"));
                    default:
                        return "";
                }
            }
            else
            {
                return "";
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;

        }
    }

    public class PaidConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            //Dim Hist As HistoryDisplay = CType(parameter, HistoryDisplay)
            if (value != null)
            {
                decimal amount = System.Convert.ToDecimal(value);

                switch (1)
                {
                    case 1: // Paid
                        return string.Format("{0,10}", Strings.Format(amount, "##,##0.00"));
                    default:
                        return "";
                }
            }
            else
            {
                return "";
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;

        }
    }

    public class TotalConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            //Dim Hist As HistoryDisplay = CType(parameter, HistoryDisplay)
            if (value != null)
            {
                decimal amount = System.Convert.ToDecimal(value);

                switch (2)
                {
                    case 2: // Total
                        return string.Format("{0,10}", Strings.Format(amount, "##,##0.00"));
                    default:
                        return "";
                }
            }
            else
            {
                return "";
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;

        }
    }

    public class NegativeColorRed : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value.ToString().Trim().Equals(""))
                return new SolidColorBrush(Colors.Black);

            decimal amount = System.Convert.ToDecimal(value);
            if (amount < 0)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;

        }
    }

    public class NotNumericReturnSpaces : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString().ToLower().Contains("ret"))
                return " ";
            return value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}