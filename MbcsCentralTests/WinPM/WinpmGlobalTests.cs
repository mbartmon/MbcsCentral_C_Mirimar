using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbcsCentral.WinPM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinPM.Tests
{
    [TestClass()]
    public class WinpmGlobalTests
    {
        [TestMethod()]
        public void LockBoxCheckDigitTest()
        {
            string[] accounts = { "0000000200640", "0000000200660", "0000000200680", "0000000200700", "0000000200720" };
            int[] checkDigits = { 1, 9, 7, 3, 1 };
            int r = 0;

            for (int i = 0; i < accounts.Length; i++)
            {
                r = WinpmGlobal.LockBoxCheckDigit(accounts[i]);

                if (r != checkDigits[i])
                    Assert.Fail(accounts[i] + " returned " + r.ToString() + " instead of " + checkDigits[i].ToString());
            }
        }
    }
}