using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MbcsCentral
{
    /// <summary>
    /// Custom message window.
    /// Can be positioned at center of calling window.
    /// Optionally accepts key board input.
    /// </summary>
    public static class MessageBoxCustom
    {
        static double MAX_HEIGHT = 200;

        /// <summary>
        /// Basic message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string message, string title = "", MessageBoxButton buttons = MessageBoxButton.OK, Window owner = null, string[] customButtonText = null)
        {
            MessageBoxCustomWindow frm = new MessageBoxCustomWindow(message,title, buttons,customButtonText);
            frm.Title = title;
            if (owner != null)
            {
                frm.Owner = owner;
                frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
                frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            frm.Topmost = true;
            frm.ShowDialog();
            return frm.Result;
        }

        /// <summary>
        /// Message box with optional background color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="Background"></param>
        /// <param name="buttons"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string message, string title, System.Windows.Media.Brush Background = null,MessageBoxButton buttons = MessageBoxButton.OK, Window owner = null, string[] customButtonText = null)
        {
            MessageBoxCustomWindow frm = new MessageBoxCustomWindow(message,title, buttons, customButtonText);
            if (owner != null)
            {
                frm.Owner = owner;
                frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
                frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            frm.Title = title;
            if (Background != null)
                frm.txtMessage.Background = Background;
            frm.Topmost = true;
            frm.ShowDialog();
            return frm.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="Background"></param>
        /// <param name="buttons"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string[] message, string title, System.Windows.Media.Brush Background = null, MessageBoxButton buttons = MessageBoxButton.OK, Window owner = null, string[] customButtonText = null )
        {
            MessageBoxCustomWindow frm = new MessageBoxCustomWindow(message, buttons, customButtonText);
            if (owner != null)
            {
                frm.Owner = owner;
                frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
                frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            frm.Title = title;
            if (Background != null)
                frm.txtMessage.Background = Background;
            frm.Topmost = true;
            frm.ShowDialog();
            return frm.Result;
        }

        /// <summary>
        /// Allows for keyboard input
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value">Returned text value. MUST be declared as OUT.</param>
        /// <param name="MaxLength">Maximum length of input</param>
        /// <param name="Background"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string title, out string value, int MaxLength = 8, System.Windows.Media.Brush Background = null, Window owner = null, string[] customButtonText = null)
        {
            value = "";
            MessageBoxCustomWindow frm = new MessageBoxCustomWindow(title, MaxLength,MessageBoxButton.OKCancel, customButtonText);
            if (owner != null)
            {
                frm.Owner = owner;
                frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
                frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            frm.Title = title;
            if (Background != null)
                frm.txtMessage.Background = Background;
            frm.Topmost = true;
            frm.ShowDialog();
            value = frm.Value;
            return frm.Result;
        }
    }
}
