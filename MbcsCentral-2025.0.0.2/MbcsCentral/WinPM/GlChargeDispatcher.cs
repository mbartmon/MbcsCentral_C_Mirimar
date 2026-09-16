using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinPM
{
    public class GlChargeDispatcher
    {

        public string ThisApp { get; set; }

        GlChargeDispatcher(string thisApp)
        {
            ThisApp = thisApp;
        }

        public static void Dispatch(string thisApp)
        {
            WinPM.Control[] pmControl = WinPM.Control.load();
            if (pmControl == null || pmControl.Length == 0)
            {
                MessageBoxCustom.Show("Could not retrieve WinPM control file.");
                return;
            }
            if (!pmControl[0].UseWingl)
            {
                MessageBoxCustom.Show("Use of WinGL has not been Configured.");
                return;
            }

            switch (pmControl[0].UseExternalWinGL)
            {
                case true:
                        frmChargeCodes frm = new frmChargeCodes(thisApp);
                        frm.Show();
                        break;
                case false:
                        frmChargeCodesInternal frmInternal = new frmChargeCodesInternal(thisApp);
                        frmInternal.Show();
                        break;
            }
        }
    }
}
