using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Globalization;

namespace MbcsCentral
{
    class NumberEntry :  TextBox
    {
        private int dec = 2;
        private string formatString = "";
        
        public NumberEntry() : base()
        {
            //this.PreviewKeyDown += new KeyEventHandler( OnKeyDown);
            this.GotFocus += new RoutedEventHandler(OnGotFocus);
            this.LostFocus += new RoutedEventHandler(OnLostFocus);
            this.TextAlignment = TextAlignment.Right;
            this.PreviewTextInput += TextBox_PreviewTextInput;
            this.Loaded += new RoutedEventHandler(OnLoad);
            this.MouseDoubleClick += new MouseButtonEventHandler(OnMouseDoubleClick);
            this.PreviewKeyDown += new KeyEventHandler(OnKeyDown);
            //decimal num;
            //Decimal.TryParse(this.Text, out num);
            //this.Text = num.ToString(format());
        }

        private  void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string text;

            try
            {
                bool isValid = IsSymbolValid(e.Text);
                e.Handled = !isValid;
                if (isValid)
                {
                    int caret = this.CaretIndex;

                    /*
                    if (e.Text.Equals(".") && this.SelectionLength >= this.SelectionStart)
                    {
                        string str = this.Text.Substring(0, this.SelectionStart);
                        this.Text = str.Normalize();
                        if (str.EndsWith(".") && !this.Text.EndsWith("."))
                            this.Text += String.Format("{0}1}", ".");
                        caret = this.Text.Length;
                    }
                    */
                    //if (this.Text.Trim().Equals("0"))
                    //    this.Text = "";
                    if (this.Text.Trim().Equals(NumberFormatInfo.CurrentInfo.NegativeSign) && e.Text.Equals(NumberFormatInfo.CurrentInfo.NegativeSign))
                    {
                        this.Text = "";
                        e.Handled = true;
                        return;
                    }
                    else if (this.Text.Trim().Equals(NumberFormatInfo.CurrentInfo.NegativeSign) && e.Text.Equals(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                    {
                        this.Text = "-.";
                        this.CaretIndex = 3;
                        e.Handled = true;
                        return;
                    }
                    else if (this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) >= 0 && e.Text.Equals(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                    {
                        caret = this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 1;
                        this.CaretIndex = caret;
                        this.SelectionStart = caret;
                        this.SelectionLength = 2;
                        e.Handled = true;
                        return;
                    }
                    //if (this.SelectionLength > 0 && caret > 0)
                    //    this.Text = this.Text.Substring(0, caret);
                    //if (this.Text.Trim().Equals(NumberFormatInfo.CurrentInfo.NegativeSign))
                    //    this.Text = "";
                    text = (this.Text.Trim().Equals("") ? "0" : this.Text);
                    bool textInserted = false;
                    int selectionLength = 0;

                    if (this.SelectionLength > 0 && !e.Text.Equals(NumberFormatInfo.CurrentInfo.NegativeSign))
                    {
                        text = text.Substring(0, this.SelectionStart) +
                                text.Substring(this.SelectionStart + this.SelectionLength);
                        caret = this.SelectionStart;
                    }

                    if (e.Text == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                    {
                        while (true)
                        {
                            int ind = text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                            if (ind == -1)
                                break;

                            text = text.Substring(0, ind) + text.Substring(ind + 1);
                            if (caret > ind)
                                caret--;
                        }

                        if (caret == 0)
                        {
                            text = "0" + text;
                            caret++;
                        }
                        else
                        {
                            if (caret == 1 && string.Empty + text[0] == NumberFormatInfo.CurrentInfo.NegativeSign)
                            {
                                text = NumberFormatInfo.CurrentInfo.NegativeSign + "0" + text.Substring(1);
                                caret++;
                            }
                        }

                        if (caret == text.Length)
                        {
                            selectionLength = 1;
                            textInserted = true;
                            text = text + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "0";
                            caret++;
                        }
                    }
                    else if (e.Text == NumberFormatInfo.CurrentInfo.NegativeSign)
                    {
                        textInserted = true;
                        if (this.Text.Contains(NumberFormatInfo.CurrentInfo.NegativeSign))
                        {
                            text = text.Replace(NumberFormatInfo.CurrentInfo.NegativeSign, string.Empty);
                            if (caret != 0)
                                caret++;
                        }
                        else
                        {
                            if (this.SelectionLength == this.Text.Length)
                                this.Text = "";
                            else
                            {
                                decimal num;
                                decimal.TryParse(this.Text, out num);
                                if (num == 0)
                                    this.Text = " ";
                            }
                            text = NumberFormatInfo.CurrentInfo.NegativeSign + this.Text.Trim();
                            if (caret == 0)
                            caret++;
                        }
                    }

                    if (!textInserted)
                    {
                        bool minusDecimal = text.Equals("-.");
                        text = text.Substring(0, caret) + e.Text +
                            ((caret < this.Text.Length) ? text.Substring(caret) : string.Empty);

                        if (minusDecimal)
                            caret++;
                        caret++;
                    }

                    try
                    {
                        //decimal val = Convert.ToDecimal(text);
                        decimal val = 0;
                        Decimal.TryParse(text, out val);
                        decimal newVal = ValidateLimits(this.Minimum, this.Maximum, val);
                        if (val != 0 && val != newVal)
                        {
                            text = newVal.ToString();
                        }
                        else if (val == 0)
                        {
                            if (!text.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) && !text.Contains(NumberFormatInfo.CurrentInfo.NegativeSign))
                                text = "0";
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!text.Trim().Equals(NumberFormatInfo.CurrentInfo.NegativeSign) && !text.Trim().Equals(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                            text = "0";
                    }

                    while (text.Length > 1 && text[0] == '0' && string.Empty + text[1] != NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                    {
                        text = text.Substring(1);
                        if (caret > 0)
                            caret--;
                    }

                    while (text.Length > 2 && string.Empty + text[0] == NumberFormatInfo.CurrentInfo.NegativeSign && text[1] == '0' && string.Empty + text[2] != NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                    {
                        text = NumberFormatInfo.CurrentInfo.NegativeSign + text.Substring(2);
                        if (caret > 1)
                            caret--;
                    }

                    //if (caret > text.Length)
                    //    caret = text.Length;

                    this.Text = (text.Trim().Equals("") ? "0" : text);
                    this.CaretIndex = caret;
                    this.SelectionStart = caret;
                    if (e.Text.Equals(NumberFormatInfo.CurrentInfo.NegativeSign))
                       this.SelectionLength = selectionLength;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private  decimal ValidateLimits(decimal min, decimal max, decimal value)
        {
            if (!min.Equals(double.NaN))
            {
                if (value < min)
                    return min;
            }

            if (!max.Equals(double.NaN))
            {
                if (value > max)
                    return max;
            }

            return value;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
           NumberEntry textbox = (NumberEntry)sender;
            UIElement focusedElement = Keyboard.FocusedElement as UIElement;
            try {
                switch (e.Key)
                {
                    case Key.OemPeriod:
                    case Key.Decimal:
                        string st = textbox.Text;
                        int position = 0;
                        int idx = 0;
                        //if (!st.Contains("."))
                        //{
                        position = st.IndexOf(".");
                        if (position < 0)
                            st = st + "." ;
                        else
                            st = st.Substring(0, position).Replace(".", "") + "." + st.Substring(position).Replace(".", "");
                        idx = st.Length;
                        textbox.Text = st;
                        textbox.SelectionStart = st.IndexOf(".") + 1;
                        textbox.SelectionLength = st.Length - textbox.SelectionStart;
                        //}
                        e.Handled = true;
                        return;
                        break;
                    case Key.Subtract:
                    case Key.OemMinus:
                        decimal number = 0;

                        if (!decimal.TryParse(textbox.Text, out number))
                            number = 0;
                        if (textbox.Text.Trim().Equals("-"))
                            textbox.Text = "";
                        else if (textbox.Text.Trim().Equals("") || number == 0)
                        {
                            textbox.Text = "-";
                        }
                        else
                        {
                            number *= -1;
                            textbox.Text = number.ToString(format());
                        }
                        textbox.SelectionStart = (number < 0 || textbox.Text.Trim().Equals("-") ? 1 : 0);
                        textbox.SelectionLength = textbox.Text.Length;
                        e.Handled = true;
                        return;
                        break;
                    case Key.Delete:
                    case Key.Back:
                    case Key.Enter:
                    case Key.Home:
                    case Key.Left:
                    case Key.Right:
                    case Key.Tab:
                        return;
                        break;
                    case Key.Up:
                        if (focusedElement != null)
                        {
                            focusedElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                        }
                        e.Handled = true;
                        break;
                    case Key.Down:
                        if (focusedElement != null)
                        {
                            focusedElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                        }
                        e.Handled = true;
                        break;
                }
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                {
                    int i = 0;
                    if (textbox.Text.Trim().Equals("-"))
                    {
                        i = 2;
                        textbox.Text = "-";
                        switch (e.Key)
                        {
                            case Key.D0:
                            case Key.NumPad0:
                                textbox.Text += "0";
                                break;
                            case Key.D1:
                            case Key.NumPad1:
                                textbox.Text += "1";
                                break;
                            case Key.D2:
                            case Key.NumPad2:
                                textbox.Text += "2";
                                break;
                            case Key.D3:
                            case Key.NumPad3:
                                textbox.Text += "3";
                                break;
                            case Key.D4:
                            case Key.NumPad4:
                                textbox.Text += "4";
                                break;
                            case Key.D5:
                            case Key.NumPad5:
                                textbox.Text += "5";
                                break;
                            case Key.D6:
                            case Key.NumPad6:
                                textbox.Text += "6";
                                break;
                            case Key.D7:
                            case Key.NumPad7:
                                textbox.Text += "7";
                                break;
                            case Key.D8:
                            case Key.NumPad8:
                                textbox.Text += "8";
                                break;
                            case Key.D9:
                            case Key.NumPad9:
                                textbox.Text += "9";
                                break;
                            case Key.Decimal:
                            case Key.OemPeriod:
                                textbox.Text += ".0";
                                i = 2;
                                break;
                        }
                        textbox.SelectionStart = i;
                        e.Handled = true;
                    }
                    else
                        e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                    return;
                }
            string[] s = textbox.Text.Split('.');
            if (s.Length > 2)
                e.Handled = true;
        }
        catch (Exception ex)
        {
            GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "NumberEntry: " + ex.Message, false);
        }
    }

    public void OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.SelectAll();
            this.SelectionStart = 0;
            this.SelectionLength = this.Text.Length;
        }
        public void OnLostFocus(object sender, RoutedEventArgs e)
        {
            NumberEntry textbox = (NumberEntry)sender;
            if (textbox.Text.Trim() == "")
                textbox.Text = "0";

            decimal number = 0;
            if (!decimal.TryParse(textbox.Text, out number))
                number = 0;
            textbox.Text = number.ToString(format());
        }

        public void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectAll();
            this.SelectionStart = 0;
            this.SelectionLength = this.Text.Length;

        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            NumberEntry textbox = this;
            if (textbox.Text.Trim() == "")
                textbox.Text = "0";

            decimal number = 0;
            if (!decimal.TryParse(textbox.Text, out number))
                number = 0;
            textbox.Text = number.ToString(format());
        }
        static bool IsSymbolValid(string str)
        {
            if (str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator ||
                str == NumberFormatInfo.CurrentInfo.NegativeSign)
                return true;

            foreach (char ch in str)
            {
                if (!Char.IsDigit(ch))
                    return false;
            }

            return true;


        }
        private string format()
        {
            string format = "";
            int length = (this.MaxLength == 0 ? 20 : this.MaxLength);

            if (dec > 0)
            {
                format = new string('#', length - dec - 1) + "." + new string('0', dec);
            }
            else
                format = new string('#', length - 1) + "0";
            return format;
        }

        public int Decimals { get { return dec; } set { dec = value; } }

        public decimal Value
        {
            get
            { if (this.Text.Trim() == "")
                    this.Text = "0";
                return decimal.Parse(this.Text);
            }
            set
            {
                this.Text = value.ToString(format());
            }
        }

        public decimal Minimum
        { get; set; } = Decimal.MinValue;
        public decimal Maximum
        { get; set; } = Decimal.MaxValue;
                    
        
        /*
        public static DependencyProperty ValueProperty =
             DependencyProperty.Register("Value", typeof(decimal),
             typeof(NumberEntry), new FrameworkPropertyMetadata((decimal)0));
        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value);
                this.Text = value.ToString();
            }
        }
        */
    }
}
