using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Return left part of string
        /// </summary>
        /// <param name="str">string to parse</param>
        /// <param name="number">Maximum number of characters</param>
        /// <returns></returns>
        public static String Left(this String str, int number)
        {
            if (str == null)
                return "";
            return str.Substring(0, str.Length < number ? str.Length : number);
        }
       
        /// <summary>
        /// Return left part of string with right padding
        /// </summary>
        /// <param name="str">string to parse</param>
        /// <param name="number">Maximum number characters</param>
        /// <param name="padding">Pad on right with n characters</param>
        /// <returns></returns>
        public static String Left(this String str, int number, int padding)
        {
            if (str == null)
                return "";
            return str.Substring(0, str.Length < number ? str.Length : number).PadRight(padding);

        }

        public static String Right(this String str, int number)
        {
            return str.Substring(str.Length - number);
        }

        /// <summary>
        /// Return left part of string with Left padding
        /// </summary>
        /// <param name="str">string to parse</param>
        /// <param name="number">Maximum number characters</param>
        /// <param name="padding">Pad on Left with n characters</param>
        /// <returns></returns>
        public static String LeftPad(this String str, int number, int padding)
        {
            if (str == null)
                return "";
            return str.Substring(0, str.Length < number ? str.Length : number).PadLeft(padding);

        }

        private static Action EmptyDelegate = delegate () { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

        public static string[] Wrap(this string line, int lineLength)
        {
            string[] words;
            string[] lines = { "" };
            if (line == null || line.Length == 0)
            {
                line = "";
                words = line.Split(' ');
            }
            else
            {
                int l = 1;
                words = line.Split(' ');
                if (words.Length <= 1)
                    return words;
                foreach (string word in words)
                {
                    if (lines[l- 1].Length + word.Length > lineLength)
                    {
                        if (word.Length > lineLength)
                        {
                            string newWord = word + " ";
                            while (newWord.Length > 0)
                            {
                                string temp = newWord.Substring(0, Math.Min(lineLength, newWord.Length));

                                Array.Resize<string>(ref lines, ++l);
                                lines[l - 1] += temp + " ";
                                newWord = newWord.Left(lineLength);
                                if (newWord == null || newWord.Length == 0)
                                    break;
                            }
                        }
                        else
                        {
                            Array.Resize<string>(ref lines, ++l);
                            lines[l - 1] += word + " ";

                        }

                    }
                    else
                    {
                       // Array.Resize<string>(ref lines, ++l);
                        lines[l - 1] += word + " ";
                    }
                }

            }
            return lines;
        }
    }


}
