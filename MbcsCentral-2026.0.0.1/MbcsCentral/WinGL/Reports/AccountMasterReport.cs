using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace MbcsCentral.WinGL
{
    public class AccountMasterReport
    {
        static long clientId = GlobalShared.user.Account.clientId;
        static Report rpt = null;
        static Font fnt = new Font("Courier New", 16);
        static Font headerFont = new Font("Arial", 14);
        static string[] _header = { "tab(4)", "Account Master List" };
        static string[] _header2 = { "tab(4)", DateTime.Now.ToShortDateString(), "tab(17)", "page ", Report.PAGE_TAG };
        static private int[] tabs = {4, 8, 10, 14};

        private int _target;
        public int Target
        { set { _target = value;} }
        
        public AccountMasterReport() {}

        public AccountMasterReport(int target)
        {
            _target = target;
        }
        public static void process()
        {
            MajorCategoryList major = new MajorCategoryList();
            List<MajorCategories> majorList = major.GetCategories();
            Accounts[] accts = null;

            if (majorList != null && majorList.Count > 0)
            {
                rpt = new Report("Account Master List", "Courier New", 14) { BottomMargin = 50, BottomOffset = 100, LineHeight = (Single)0.150 * 96 };
                rpt.SetFont = fnt;
                rpt.LeftMargin = (float) .25*96;
                rpt.TopMargin = (float) .25*96;
                Heading();

                foreach (Category cat in majorList)
                {
                    rpt.FontBold = true;
                    rpt.PrintPageLine(new string(' ', tabs[0]) + cat.CategoryDescription);
                    rpt.FontBold = false;
                    rpt.PrintPageLine("");

                    accts = Accounts.loadByCategory(cat.Key,true, false);
                    if (accts != null)
                        PrintAccounts(accts,0);

                    if (((MajorCategories) cat).SubCats != null)
                    {
                        foreach (var subCat in ((MajorCategories)cat).SubCats)
                        {
                            rpt.PrintPageLine(new string(' ', tabs[2] - 2), false);
                            rpt.FontUnderline = true;
                            rpt.PrintPageLine( subCat.CategoryDescription);
                            rpt.FontUnderline = false;
                            if (rpt.PrintPageLine(""))
                            {
                                rpt.NewPage();
                                Heading();
                            }
                            accts = Accounts.loadByCategory(subCat.Key, true, false);
                            if (accts != null && accts.Length > 0)
                                PrintAccounts(accts, tabs[2]);
                            rpt.PrintPageLine("");

                            if (subCat.SubSubCats != null)
                            {
                                foreach (var subSubCat in subCat.SubSubCats)
                                {
                                    rpt.PrintPageLine(new string(' ', tabs[3] - 2), false);
                                    rpt.FontUnderline = true;
                                    rpt.PrintPageLine(subSubCat.CategoryDescription);
                                    rpt.FontUnderline = false;
                                    if (rpt.PrintPageLine(""))
                                    {
                                        rpt.NewPage();
                                        Heading();
                                    }
                                    accts = Accounts.loadByCategory(subSubCat.Key, true);
                                    if (accts != null && accts.Length > 0)
                                        PrintAccounts(accts, tabs[3]);
                                    rpt.PrintPageLine("");
                                }
                            }
                        }
                    }
                }
            }
            rpt.DisplayDocument();
        }

        static private void PrintAccounts(Accounts[] accts, int tab)
        {
            foreach (var acct in accts)
            {
                if (rpt.PrintPageLine(new string(' ', tab) + acct.Description.Trim() + (string)(acct.active == true ? "" : " [inactive]")))
                {
                    rpt.NewPage();
                    Heading();
                }
            }
        }
        static void Heading()
        {
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
            rpt.FontBold = true;
            rpt.PrintPageLine(_header);
            rpt.FontBold = false;
            rpt.PrintPageLine(_header2);
            rpt.PrintPageLine("");
            rpt.PrintPageLine("");
        }
    }

}
