using System;
using System.Windows.Data;
namespace MbcsCentral
{
    public class DateConverter : IValueConverter
    {


        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            //Dim Hist As HistoryList = CType(parameter, HistoryList)
            if (value != null)
            {
                DateTime xDate = System.Convert.ToDateTime(value);

                // display if not 1/1/1900
                if (xDate > DateTime.Parse("1/1/1900"))
                {
                    return Microsoft.VisualBasic.Strings.Format(xDate, "MM/dd/yyyy");
                }
                else
                {
                    return "";
                }
            }
            return "";

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime dateValue;
                if (DateTime.TryParse(value.ToString(), out dateValue ))
                {
                    return dateValue;
                }
               
            }

            return "";

        }
    }

    public class DateNotNullBool : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime xDate = System.Convert.ToDateTime(value);

                if (xDate > DateTime.Parse("1/1/1900"))
                    return true;
                else
                    return false;
            }
            return false;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return "";

        }
    }
}
