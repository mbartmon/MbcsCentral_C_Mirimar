using System;
using System.Windows.Data;
namespace MbcsCentral
{
    public class StringPad : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string s = System.Convert.ToString(value);
            int l = System.Convert.ToInt32(parameter); // negative = rightpad

            if (s != null && s.Length > 0 && l != 0)
            {
                if (l > 0) // left pad
                {
                    return s.Trim().PadLeft(l);
                }
                else // right pad
                {
                    return s.Trim().PadRight(l * -1);
                }
            }
            return "";

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;

        }
    }

    public class StringTruncate : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string s = System.Convert.ToString(value);
            int l = System.Convert.ToInt32(parameter);
            if (s != null && s.Trim() != "")
            {
                if (l < s.Length)
                {
                    return s.Substring(0, l);
                }
                else
                {
                    return s;
                }
            }
            else
            {
                return "";
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = System.Convert.ToString(value);
            int l = System.Convert.ToInt32(parameter);
            if (s != null && s.Trim() != "")
            {
                if (l < s.Length)
                {
                    return s.Substring(0, l);
                }
                else
                {
                    return s;
                }
            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// Returns passed string with Trim() applied
    /// </summary>
    public class StringTrim : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string t = (string)value;
                return t.Trim();
            }
            catch { return ""; }
            return "";

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string t = (string)value;
                return t.Trim();
            }
            catch { return ""; }
            return "";
        }
    }
        

    public class UpperCase : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string t = (string)value;
                return t.ToUpper();
            }
            catch { return ""; }
        }
         public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string t = (string)value;
            return t.ToUpper();
        }
   }


    public class ChargeTypeConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string t = System.Convert.ToString(value);

            switch (t.Trim())
            {
                case "1": // one time
                    return "O";
                case "2": // re-curring
                    return "R";
            }
            return "";

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;

        }
    }

    public class BooleanYesNoConverter : IValueConverter
    {
        public object Convert(object value, System.Type tartgetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool f = System.Convert.ToBoolean( value );
            
            if (f)
                return "yes";
            else
                if (parameter != null && parameter.ToString().Trim() != "")
                    return "no";
                else
                    return "";
        }

        public object ConvertBack( object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {

            return null;

        }
    }

    public class BooleanOnOffConverter : IValueConverter
    {
        public object Convert(object value, System.Type tartgetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool f = System.Convert.ToBoolean(value);

            if (f)
                return "on";
            else
                if (parameter != null && parameter.ToString().Trim() != "")
                    return "off";
                else
                    return "";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;

        }
    }
    public class SetColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(System.Windows.Media.Brush))
                return null;
            bool flag = (bool)value;
            if (flag)
                return System.Windows.Media.Brushes.Red;
            else
                return System.Windows.Media.Brushes.Black;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            throw new NotSupportedException("Cannot convert back");
        }
    }

    public class TwoValueReturnNonNull : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[1] == null)
                return (string)values[0];
            else
                return (string)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {

            throw new NotSupportedException("Cannot convert back");

        }
    }

    public class IdToName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string table = (string)parameter;
            string name = "";

            switch (table.ToLower())
            {
                
                case "company":
                    Int16 id = Int16.Parse(value.ToString());
                    MbcsCentral.WinGL.Company c = MbcsCentral.WinGL.Company.loadByCompany(id);
                    if (c != null)
                        name = c.Name;
                    break;
                case "department":
                    Int16 id1 = Int16.Parse(value.ToString());
                    MbcsCentral.WinGL.Department d = MbcsCentral.WinGL.Department.load(id1);
                    if (d != null)
                        name = d.Name;
                    break;
                case "accounts":
                    int id2 = Int32.Parse(value.ToString());
                    MbcsCentral.WinGL.Accounts a = MbcsCentral.WinGL.Accounts.load(id2);
                    if (a != null)
                        name = a.Description;
                    break;
            }
            return name;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {

            throw new NotSupportedException("Cannot convert back");

        }
    }

}