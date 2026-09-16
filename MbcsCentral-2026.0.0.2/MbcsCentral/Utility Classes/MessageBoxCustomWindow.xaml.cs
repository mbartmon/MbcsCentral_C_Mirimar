using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MbcsCentral
{
    /// <summary>
    /// Interaction logic for MessageBoxCustomWindow.xaml
    /// </summary>
    public partial class MessageBoxCustomWindow : Window
    {
        /// <summary>
        /// On return, contains MessageBoxButton that user clicked on
        /// </summary>
        public MessageBoxResult Result { get; set; }
        public string Heading;
        /// <summary>
        /// Text value returned by Input method
        /// </summary>
        public string Value { get; set; }
        private bool Input = false;

        /// <summary>
        /// Basic message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        public MessageBoxCustomWindow(string message, string title = "", MessageBoxButton buttons = MessageBoxButton.OK, string[] customButtonText = null)
        {
            InitializeComponent();
            txtMessage.Text = message;
            SetControls(buttons, customButtonText);
            this.txtTitle.Text = title;
        }

        /// <summary>
        /// Accepts string array.
        /// 
        /// Each array element is printed on separate line.
        /// </summary>
        /// <param name="message">String array</param>
        /// <param name="buttons"></param>
        public MessageBoxCustomWindow(string[] message, MessageBoxButton buttons = MessageBoxButton.OK, string[] customButtonText = null)
        {
            InitializeComponent();
            foreach(string s in message)
            {
                txtMessage.Text += s + "\r\n";
            }           
            SetControls(buttons, customButtonText);
        }

        public MessageBoxCustomWindow(ArrayList messages, MessageBoxButton buttons = MessageBoxButton.OK, string[] customButtonText = null)
        {
            InitializeComponent();
            foreach(string s in messages)
            {
                txtMessage.Text += s + "\r\n";
            }
            SetControls(buttons, customButtonText);
        }

        /// <summary>
        /// For keyboard entry.
        /// 
        /// Retrieve entered text value from Value public property after call
        /// </summary>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        public MessageBoxCustomWindow(string title, int MaxLength, MessageBoxButton buttons = MessageBoxButton.OKCancel, string[] customButtonText = null)
        {
            InitializeComponent();
            txtMessage.IsReadOnly = false;
            txtMessage.MaxLength = MaxLength;
            txtTitle.Text = title;
            SetControls(buttons, customButtonText);
            Input = true;
            txtMessage.Focus();
        }

        /// <summary>
        /// Internal method to set the requested MessageBoxButtons
        /// </summary>
        /// <param name="buttons">MessageBoxButton enum</param>
        private void SetControls(MessageBoxButton buttons, string[] customButtonText = null)
        {
            if (buttons == MessageBoxButton.OK || buttons == MessageBoxButton.OKCancel)
            {
                Button cmdOK = new Button();
                if (customButtonText != null) 
                    cmdOK.Content = customButtonText[0];
                else
                    cmdOK.Content = "OK";
                cmdOK.Margin = new Thickness(5, 0, 0, 0);
                cmdOK.Click += cmdOK_Click;
                pnlButtons.Children.Add(cmdOK);
            }
            if (buttons == MessageBoxButton.YesNo || buttons == MessageBoxButton.YesNoCancel)
            {
                Button cmdYes = new Button();
                if (customButtonText != null && customButtonText.Length >= 1)
                    cmdYes.Content = customButtonText[0]; 
                else
                    cmdYes.Content = "YES";
                cmdYes.Margin = new Thickness(5, 0, 0, 0);
                cmdYes.Click += cmdYes_Click;
                pnlButtons.Children.Add(cmdYes);

            }
            if (buttons == MessageBoxButton.YesNo || buttons == MessageBoxButton.YesNoCancel)
            {
                Button cmdNo = new Button();
                if (customButtonText != null && customButtonText.Length >= 2)
                    cmdNo.Content = customButtonText[1];
                else
                    cmdNo.Content = "NO";
                cmdNo.Margin = new Thickness(5, 0, 0, 0);
                cmdNo.Click += cmdNo_Click;
                pnlButtons.Children.Add(cmdNo);
            }
            if (buttons == MessageBoxButton.OKCancel || buttons == MessageBoxButton.YesNoCancel)
            {
                Button cmdCancel = new Button();
                if (customButtonText != null && customButtonText.Length == 3)
                    cmdCancel.Content = customButtonText[2];
                else
                    cmdCancel.Content = "CANCEL";
                cmdCancel.Margin = new Thickness(5, 0, 0, 0);
                cmdCancel.Click += cmdCancel_Click;
                pnlButtons.Children.Add(cmdCancel);
            }

        }
        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            if (Input)
                Value = txtMessage.Text;
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Value = "";
            this.Close();
        }

        private void cmdYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.Close();
        }

        private void cmdNo_Click(object sender,RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Close();
        }
        private void MessageBoxCustomWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MessageBoxCustomWindow_Closed(object sender, EventArgs e)
        {

        }

        private void FormMessageBoxCustom_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            txtMessage.Height = this.Height - 110;

        }

        private TextBlock ParseText(string text)
        {
            ArrayList list = new ArrayList();
            int posClosing1 = -1;
            int posOpen2 = -1;
            int posClosing2 = -1;
            int position = (text.IndexOf('{'));
            if (position >= 0)
            {
                posClosing1 = text.IndexOf('}');
                if (posClosing1 >= 0)
                {
                    posOpen2 = text.IndexOf('{', posClosing1);
                    if (posOpen2 >= 0)
                        posClosing2 = text.IndexOf('}', posOpen2);
                }
            }
            if (position < 0 || posClosing1 < 0 || posOpen2 < 0 || posClosing2 < 0)
            {
                return new TextBlock() { Text = text };
            }

            string s = text.Substring(0, position);

            TextBlock txt = new TextBlock();

            return null;

        }
        private System.Drawing.Color TextForeground(string colorName)
        {
            return System.Drawing.Color.FromName(colorName);
        }

        private System.Windows.FontWeight TextFontWeight(string fontWeight)
        {
            FontWeight fw;
            try
            {
                System.Windows.FontWeightConverter fwc = new FontWeightConverter();
                fw = (FontWeight)fwc.ConvertFromString(fontWeight);
            }
            catch (Exception ex)
            {
                return FontWeights.Normal;
            }
            return fw;
        }
    }
}
