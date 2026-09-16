using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.WinPM
{
    class Class1
    {

        private void payent_alloc()
        {
            decimal[] damount = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            decimal[] ddue = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            decimal paidx = 0;
            decimal Paid = 0;
            int j = 0;
            int nc = 0;
            decimal x = 0;
            bool found;

            for (j = 0; j < nc - 1; j++)
                damount[j] = 0;

            found = false;
            for (j = 0; j < 10; j++)
            {
                if (Paid == ddue[j])
                {
                    damount[j] = Paid;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                paidx = Paid;
                for (j = 0; j < 10; j++)
                {
                    if (ddue[j] < 0)
                        damount[0] += +ddue[j];
                }
                j = 0;
                while (j <= 10)
                {
                    if (ddue[j] < paidx)
                        damount[j] += ddue[j];
                    else
                        damount[j] += paidx;
                    paidx -= damount[j];
                    j++;
                }
                if (paidx != 0)
                {
                    for (j = 10; j == 0; j++)
                    {
                        if (ddue[j] - damount[j] <= 0)
                            break;
                        x = ddue[j] - damount[j];
                        if (x >= paidx)
                        {
                            damount[j] += paidx;
                            paidx = 0;
                            break;
                        }
                        else
                        {
                            damount[j] += x;
                            paidx -= x;
                        }
                    }
                    if (paidx != 0)
                        damount[0] += paidx;
                }
            }
        }
    }
}
