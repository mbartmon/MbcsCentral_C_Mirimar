using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Printing;
using System.Windows.Documents;
using System.Xml.Serialization;
using ExtensionMethods;

namespace MbcsCentral.WinGL
{
    public class JournalReport
    {
        public short Company { get; set; } = -1;
        public short Department { get; set; } = 1;
        public int StartCheck { get; set; }
        public int EndCheck { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string CompanyName { get; set; } = "";
        public string DepartmentName { get; set; } = "";
        public bool Consolidate { get; set; }
        public bool PrintSingleDepartment { get; set; }
        public string JournalName { get; set; }
        public string Layout { get; set; }
        public bool SourceSearch { get; set; }
        public bool PrintTotals { get; set; }
        public string ReportTitle { get; set; }
        public int Target { get; set; }
        public string Printer { get; set; }

        Report rpt = null;
        PrintQueue printer;
        string fontname = "Courier New";
        float fontsize = 8;

        private object[,] lo;
        private short pg;
        private short ln;
        private bool[] cs;
        private short Columns;
        private short mr;
        private decimal[] total = null;
        private JournalReportsTable[] Trans;
        private enum LayoutFields
        {
            lofield = 0,
            lorange = 1,
            lotype = 2,
            loheading = 3,
            loprintflag = 4,
            losize = 5,
            loused = 6
        }
        private enum FieldNames
        {
            fndate = 0,
            fnaccount = 1,
            fnamount = 2,
            fndescription = 3,
            fncheck = 4,
            fnsource = 5,
            fndepartment = 6
        }

        public FixedDocument process(int target)
        {
            Target = target;
            return process(null);
        }

        public FixedDocument process(PrintQueue printer = null)
        {
            this.printer = printer;
            rpt = new Report("Journal Report", fontname, fontsize) { BottomMargin = 75, LeftMargin = 10, TopMargin = 10, BottomOffset = 100 };
			int s;
			int c;
			string x = "";
			bool Consolidated = false;

			string xsql = null;
			total = new decimal[10];
			bool first = false;
			AccountRanges[] tblrange = null;
			bool a = false;
			System.DateTime xdate = default(System.DateTime);
			string xsrc = null;
			int xdpt = 0;
			bool jPrint = false;
			bool jAnyRange = false;
			bool jRange = false;

            if (!Consolidated && Department == 0)
            {
                Department[] depts = WinGL.Department.loadByCompany(Company);
                if (depts != null && depts.Length > 0)
                {
                    Department = depts[0].DepartmentNumber;
                    DepartmentName = depts[0].Name;
                }
            }
			setup_layout();
			//System.Array.Clear(total, 0, total.Length);
			first = true;

            Trans = JournalReportsTable.load(JournalName, Company, Department, StartDate, EndDate, Consolidate, SourceSearch);
			if (Trans == null || Trans.Length == 0)
				return null;
			pg = 0;
			heading(Trans[0]);
			foreach (JournalReportsTable tblTrans in Trans)
            {
				x = new string(' ', 4);

				jPrint = false;
				jAnyRange = false;
                for (c = 0; c <= Columns - 1; c++)
                {
                    if ((bool)lo[c, (int)LayoutFields.loused])
                    { 
                        // Determine if this transaction falls within any defined ranges
                        if ((bool)lo[c, (int)LayoutFields.lorange] && !jAnyRange)
                        {
                            tblrange = AccountRanges.load("Journals:" + Layout.Trim(), c);
                            if (tblrange.Length > 0)
                            {
                                foreach (AccountRanges range in tblrange)
                                {
                                    if (range.AccountNumber == tblTrans.AccountNumber)
                                    {
                                        jAnyRange = true;
                                        break;
                                    }
                                }
                                tblrange = null;
                            }
                        }
                        // Process Debit/Credit type amount fields to determine if should be printed
                        if (!(bool)lo[c, (int)LayoutFields.loprintflag] && !jPrint)
                            jPrint = true;
                        if ((mr == 0 || jAnyRange) && !jPrint)
                        {
                            if ((short)lo[c, (int)LayoutFields.lofield] == (short)FieldNames.fnamount)
                            {
                                switch ((short)lo[c, (int)LayoutFields.lotype])
                                {
                                    case 0: // no type
                                        jPrint = true;
                                        break;
                                    case 1: //Credit
                                        if (tblTrans.Amount < 0)
                                            jPrint = true;
                                        break;
                                    case 2: // Debit
                                        if (tblTrans.Amount >= 0)
                                            jPrint = true;
                                        break;
                                }
                            }
                        }
                    }


                }
                if (!jPrint)
                    continue;

                for (c = 0; c <= Columns - 1; c++) {
                    if ((bool)lo[c, (int)LayoutFields.loused])
                    {
                        // Determine if within range
                        jRange = true;
                        if ((bool)lo[c, (int)LayoutFields.lorange])
                        {
                            tblrange = AccountRanges.load("Journals:" + Layout.Trim(), c);
                            if (tblrange.Length > 0) {
                                jRange = false;
                                foreach (AccountRanges range in tblrange)
                                {
                                    if (range.AccountNumber == tblTrans.AccountNumber)
                                    {
                                        jRange = true;
                                        break; // TODO: might not be correct. Was : Exit Do
                                    }
                                }
                                tblrange = null;
                            }
                        }
                    }
                    if (!jRange)
                    {
                        x += new string(' ', (short)lo[c, (int)LayoutFields.losize] + 1);
                        continue;
                    }

                    if (!(bool)lo[c, (int)LayoutFields.loprintflag] && jAnyRange)
                    {
                        x += new string(' ', (short)lo[c, (int)LayoutFields.losize] + 1);
                        continue;
                    }
                    else
                    {

                        switch ((short)lo[c, (int)LayoutFields.lofield])
                        {
                            case (int)FieldNames.fndate:  //DATE
                                x += " " + tblTrans.Date.ToString("MM/dd/yy").PadRight((short)lo[c, (int)LayoutFields.losize]);
                                break;
                            case (int)FieldNames.fnamount: //AMOUNT
                                s = (short)lo[c, (int)LayoutFields.losize];
                                if ((short)lo[c, (int)LayoutFields.lotype] == 1 && (decimal)(tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]]) < 0)
                                {
                                    x += " " + (System.Math.Abs((decimal)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]]).ToString(new string('#', s - 3) + ".00").PadLeft((short)lo[c, (int)LayoutFields.losize]));
                                    total[c] += System.Math.Abs((decimal)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]]);
                                }

                                else if ((short)lo[c, (int)LayoutFields.lotype] == 2 && (decimal)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]] >= 0)
                                {
                                    x += " " + ((decimal)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]]).ToString(new string('#', s - 3) + ".00").PadLeft((short)(lo[c, (int)LayoutFields.losize]));
                                    total[c] += (decimal)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]];
                                }
                                else if ((short)lo[c, (int)LayoutFields.lotype] == 0)
                                {
                                    x += " " + ((decimal)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]]).ToString(new string('#', s - 3) + ".00").PadLeft((short)lo[c, (int)LayoutFields.losize]);
                                    total[c] += (decimal)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]];
                                }
                                else
                                    x += new string(' ', (short)lo[c, (int)LayoutFields.losize] + 1);
                                break;
                            default:
                                x += " " + ((string)tblTrans[Journals.Columns[(short)lo[c, (int)LayoutFields.lofield]]]).Left((short)lo[c, (int)LayoutFields.losize]).PadRight((short)(lo[c, (int)LayoutFields.losize]));
                                break;
                        }
                    }
                }
				if (!string.IsNullOrEmpty(x.Trim()))
                {
					if (first) {
						xdate = (DateTime)tblTrans[Journals.Columns[(int)FieldNames.fndate]];
						xsrc = (string)tblTrans[Journals.Columns[(int)FieldNames.fnsource]];
						xdpt = (int)tblTrans.DepartmentNumber;
						first = false;
					}
                    if (!Consolidated && xdpt != (int)tblTrans.DepartmentNumber)
                    {
                        print_totals(ref total);
                        if (PrintTotals)
                        {
                            rpt.NewPage();
                            xdate = (DateTime)tblTrans[Journals.Columns[(int)FieldNames.fndate]];
                            xsrc = (string)tblTrans[Journals.Columns[(int)FieldNames.fnsource]];
                            xdpt = (int)tblTrans.DepartmentNumber;
                            heading(tblTrans);
                        }
                    }
					if (xdate.Date != ((DateTime)tblTrans[Journals.Columns[(int)FieldNames.fndate]]).Date)
                    {
						rpt.PrintPageLine("");
						ln++;
                        xdate = (DateTime)tblTrans[Journals.Columns[(int)FieldNames.fndate]];
                        xsrc = (string)tblTrans[Journals.Columns[(int)FieldNames.fnsource]];
                    }
                    if (SourceSearch && xsrc != (string)tblTrans[Journals.Columns[(int)FieldNames.fnsource]])
                    {
						rpt.PrintPageLine("");
						ln++;
                        xdate = (DateTime)tblTrans[Journals.Columns[(int)FieldNames.fndate]];
                        xsrc = (string)tblTrans[Journals.Columns[(int)FieldNames.fnsource]];
                    }
                    if (ln > rpt.MaxPageHeight - 8) {
						rpt.NewPage();
						heading(tblTrans);
					}
					if (rpt.PrintPageLine(x))
                    {
                        rpt.NewPage();
                        heading(tblTrans);
                    }
                    ln++;
				}
			}
            if (PrintTotals)
                print_totals(ref total);
            if (Target == 1)
                rpt.PrintDocument(printer);
            else if (Target == 3)
            {
                rpt.NewPage(true);
                return rpt.Document;
            }
            else
                rpt.DisplayDocument();

            return null;
        }

        private void print_totals(ref decimal[] total)
        {
            int s;

			string x = new string(' ' , 4);
			for (int c = 0; c <= Columns - 1; c++) {
				if (cs[c]) {
                    x += " " + new string('=', (short)lo[c, (int)LayoutFields.losize]);
				} else {
                    x += new string(' ', (short)lo[c, (int)LayoutFields.losize] + 1);
				}
			}
			rpt.PrintPageLine(x);
			x = new string(' ', 4);
            for (int c = 0; c <= Columns - 1; c++)
            {
                if ((bool)lo[c, (short)LayoutFields.loused] && (short)lo[c, (short)LayoutFields.lofield] == 2)
                {
                    s = (short)lo[c, (int)LayoutFields.losize];
                    x += " " + total[c].ToString( new string('#', s - 3) + ".00").PadLeft((short)(lo[c, (int)LayoutFields.losize]));
                }
                else
				    x += " " + new string(' ', (short)lo[c, (int)LayoutFields.losize]);

            }
            rpt.PrintPageLine(x);
			System.Array.Clear(total, 0, total.Length);
			return;

        }

        private void heading(JournalReportsTable tblTrans)
        {
            short i = 0;
            short l = 0;
            string x = null;
            pg++;
            ln = WinglGlobal.tmargin;
            rpt.PrintPageLine(new string(' ', 5) + CompanyName);
            rpt.PrintPageLine(new string(' ', 5) + ReportTitle);
            if (!Consolidate && Department > 0)
            {
                rpt.PrintPageLine(new string(' ', 5) + tblTrans[Journals.Columns[(int)FieldNames.fndepartment]]);
                ln++;
            }
            rpt.PrintPageLine(new string(' ', 5) + StartDate.ToShortDateString() + " to " + EndDate.ToShortDateString() + "            page " + pg);
            rpt.PrintPageLine("");
            x = new string(' ', 5);
            l = 0;
            for (i = 0; i <= Columns - 1; i++)
            {
                if ((bool)lo[i, (int)LayoutFields.loused] && (short)lo[i, (int)LayoutFields.lofield] == 2)
                {
                        x += ((string)lo[i, (int)LayoutFields.loheading]).Left((short)lo[i, (int)LayoutFields.losize]).PadLeft((short)(lo[i, (int)LayoutFields.losize])) + " ";
                        cs[i] = true;
                }
                else if ((bool)lo[i, (int)LayoutFields.loused])
                {
                    x += ((string)lo[i, (int)LayoutFields.loheading]).Left( (short)lo[i, (int)LayoutFields.losize]).PadRight((short)lo[i, (int)LayoutFields.losize] + 1);
                    cs[i] = false;
                }
                l += ((short)lo[i, (int)LayoutFields.losize]);
                l++;
            }
            rpt.PrintPageLine(x);
            rpt.PrintPageLine(new String(' ', 5) + new string('=', l));
            ln += 6;

        }

        public void setDates(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
        }

        private void setup_layout()
        {
            int i;

            Journals[] tbl = null;

            lo = null;
            mr = 0;
            tbl = Journals.load(Layout);
            if (tbl != null && tbl.Length > 0)
            {
                Columns = (short)tbl.Length;
                lo = new object[Columns, tbl[0].ColumnCount];
                cs = new bool[Columns];
                foreach (Journals jrn in tbl)
                {
                    for (i = 2; i <= tbl[0].ColumnCount - 2; i++)
                    {
                        if (i == 4)
                            lo[(int)jrn.Column, i - 2] = Convert.IsDBNull(jrn[Journals.LayoutColumns[i]]) ? 0 : jrn[Journals.LayoutColumns[i]];
                        else
                            lo[(int)jrn.Column, i - 2] = Convert.IsDBNull(jrn[Journals.LayoutColumns[i]]) ? "" : jrn[Journals.LayoutColumns[i]];
                    }
                    lo[(int)jrn.Column, (int)LayoutFields.loused] = true;
                    if ((bool)lo[(int)jrn.Column, (int)LayoutFields.lorange])
                        mr++;
                }
                tbl = null;
            }
            else
            {
                MessageBoxCustom.Show("Layout not found.", this.ReportTitle, System.Windows.MessageBoxButton.OK);
            }

        }

        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static JournalReport DeSerialize(string inputString)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(JournalReport));
            StringReader rdr = new StringReader(inputString);
            JournalReport parms = (JournalReport)serializer.Deserialize(rdr);
            return parms;

        }
    }

}