using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral
{
    public class DirectPrint
    {
        private const string NEW_PAGE = "NEWPAGE";
        private int currentRow = 0;
        private float Xpos = 0;
        private float Ypos = 0;
        private List<LineParms> lines;
        private PrintDocument prDoc;

        public DirectPrint()
        {
            lines = new List<LineParms>();
            prDoc = new PrintDocument();
            prDoc.PrintPage += PrintPage;
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            if (lines != null && lines.Count > 0)
            {
                while (currentRow < lines.Count)
                {
                    if (lines[currentRow].line.Trim().Equals(NEW_PAGE))
                    {
                        Ypos = 0;
                        e.HasMorePages = true;
                        currentRow++;
                        return;
                    }
                    e.Graphics.DrawString(lines[currentRow].line, lines[currentRow].font, lines[currentRow++].brush, Xpos, Ypos);
                    Ypos += LineSpacing;
                }
                e.HasMorePages = false;
            }
            else
                e.HasMorePages = false;
        }

        public void NewPage()
        {
            PrintPageLine(null);
        }

        public void PrintPageLine(string line, bool lineFeed = true)
        {
            if (line == null)
                line = NEW_PAGE;
            if (lineFeed || lines.Count == 0)
                lines.Add(new LineParms() { line = line, brush = this.brush, font = this.font });
            else
            {
                LineParms temp = lines[lines.Count - 1];
                temp.line += line;
                lines[lines.Count - 1] = temp;
            }
        }

        public void Print(System.Printing.PrintQueue printer)
        {
            if (printer == null) return;
            GlobalShared.Log.Log((int)LogClass.logType.Info, "      Sending to printer: " + printer.Name, false);
            System.Drawing.Printing.PrinterSettings settings = new PrinterSettings();
            settings.PrinterName = printer.Name;
            prDoc.PrinterSettings = settings;
            try
            {
                prDoc.Print();
            }
            catch (InvalidPrinterException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Named printer [" + printer.Name + "]\r\n   " + ex.Message, false);
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error attempting to print to [" + printer.Name + "]\r\n   " + ex.Message, false);
            }
        }
        public System.Drawing.Brush brush { get; set; } = System.Drawing.Brushes.Black;
        public Font font { get; set; } = new Font("Courier New", 10);
        public float LineSpacing { get; set; } = 10.0F;
    }

    public class LineParms
    {
        public string line { get; set; }

        public System.Drawing.Brush brush { get; set; }

        public System.Drawing.Font font {get;set;}
    }
}
