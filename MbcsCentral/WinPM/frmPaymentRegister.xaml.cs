using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace MbcsCentral.WinPM
{
    /// <summary>
    /// Interaction logic for frmPaymentRegister.xaml
    /// </summary>
    public partial class frmPaymentRegister : Window
    {
        public string LockBox { get; set; } = "";

        private PaymentRegister rpt;

        public frmPaymentRegister()
        {
            InitializeComponent();
        }

        private void Display_Click( object sender, RoutedEventArgs e )
        {
            rpt = new PaymentRegister(false) { LockBox = this.LockBox };
            rpt.print_report();
        }

        private void Print_Click( object sender, RoutedEventArgs e )
        {
            rpt = new PaymentRegister(true) { LockBox = this.LockBox };
            rpt.print_report();
        }

        private void Exit_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
            //formPaymentRegister_Closed(sender, new EventArgs());
        }

        private void formPaymentRegister_Closed(object sender, EventArgs e)
        {
            if (MessageBox.Show("Post Payments and delete temporary records?","",MessageBoxButton.OKCancel) == MessageBoxResult.OK )
            {
                Post();
            }
        }

        private void Post()
        {
            Control ctrl = Control.load(GlobalShared.user.Account.clientId);
            if (ctrl != null && ctrl.UseWingl)
                if (!GLTransactions.GLproc(ctrl))
                    return;

            if (!Paytemp.delete(GlobalShared.user.Account.accountId))
                MessageBox.Show("Temporary payment records could not be deleted.");
        }

/* Payment Posting
Public Sub GLproc()
           
    Dim x As String, dt As String, f() As String, fl As Boolean
    Dim i As Integer, ST As String, BN As String, artrans As artranstype
    Dim j As Integer, ca() As Variant, acct() As Variant, ix As Integer
           
            On Error GoTo glproc_error
            fl = False
            ReDim acct(2, 0), ca(2, 0)
            x = date$
            dt = Left$(x, 2) & Mid$(x, 4, 2) & Right$(x, 2)
            artrans.crlf = vbCrLf
           
            Set tblControl = PMSdb.OpenRecordset("GLTable")
            ix = -1
            If tblControl.RecordCount > 0 Then
                tblControl.MoveFirst
                While Not tblControl.EOF
                    ix = ix + 1
                    ReDim Preserve f(2, ix)
                    f(0, ix) = tblControl("From")
                    f(1, ix) = tblControl("To")
                    tblControl.MoveNext
                Wend
            End If
            tblControl.Close
           
            Set tblCodes = PMSdb.OpenRecordset("Codes")
            tblCodes.Index = "PrimaryKey"
           
            fi = FreeFile
            Open appath & "\artrans" For Binary Access Write As fi
           
            Set tblBuilding = PMSdb.OpenRecordset("Building")
           
            xsql = "SELECT * FROM Paytemp WHERE [UserID] = '" & UserID & "'"
            Set tblPayTemp = PMSdb.OpenRecordset(xsql)
           
            While Not tblPayTemp.EOF
                If fl Then
                    If tblPayTemp("Building Number") <> bldkey Then
                        GoSub GLproc_bld
                        bldkey = tblPayTemp("Building Number")
                    End If
                Else
                    bldkey = tblPayTemp("Building Number")
                    fl = True
                End If
                For i = 1 To UBound(ca, 2)
                    If ca(0, i) = tblPayTemp("Code") Then
                        ca(1, i) = ca(1, i) + tblPayTemp("Net")
                        Exit For
                    End If
                Next
                If i > UBound(ca, 2) Then
                    ReDim Preserve ca(2, i)
                    ca(0, i) = tblPayTemp("Code")
                    ca(1, i) = tblPayTemp("Net")
                End If
                tblPayTemp.MoveNext
            Wend
            GoSub GLproc_bld
            tblBuilding.Close
            tblPayTemp.Close
            Close fi
            tblCodes.Close
            GoTo glproc_exit
           
GLproc_bld:
            tblBuilding.Index = "PrimaryKey"
            tblBuilding.Seek "=", bldkey
            If Not tblBuilding.NoMatch Then
                BN = Format$(Val(tblBuilding("Owner Number")), "00") & Format$(Val(tblBuilding("Building Number")), "000")
          
                If ix > -1 Then
                    For i = 0 To UBound(f, 2)
                        If bldkey = f(0, i) Then
                            BN = Format$(Val(tblBuilding("Owner Number")), "00") & Format$(Val(f(1, i)), "000")
                            Exit For
                        End If
                    Next
                End If
         
                For i = 1 To UBound(ca, 2)
                    If ca(1, i) <> 0 Then
                        tblCodes.Seek "=", ca(0, i)
                        If Not tblCodes.NoMatch Then
                            If Not IsNull(tblCodes("GL Credit Account")) And Not IsNull(tblCodes("GL Credit Sub-Account")) And Not IsNull(tblCodes("GL Debit Account")) And Not IsNull(tblCodes("GL Debit Sub-Account")) Then
                                artrans.ownbld = BN
                                artrans.acct = tblCodes("Gl Credit Account") & tblCodes("GL Credit Sub-Account")
                                artrans.date = dt
                                artrans.desc = tblCodes("Charge Description")
                                artrans.amount = Format$(-ca(1, i), "00000000.00;-0000000.00")
         
                                Put #fi, LOF(fi) + 1, artrans
                                For j = 1 To UBound(acct, 2)
                                    If tblCodes("GL Debit Account") & tblCodes("GL Debit Sub-Account") = acct(0, j) Then
                                        acct(1, j) = acct(1, j) + ca(1, i)
                                        Exit For
                                    End If
                                Next
                                If j > UBound(acct, 2) Then
                                    ReDim Preserve acct(2, j)
                                    acct(0, j) = tblCodes("GL Debit Account") & tblCodes("GL Debit Sub-Account")
                                    acct(1, j) = ca(1, i)
                                End If
                            End If
                        End If
                    End If
                Next
                For i = 1 To UBound(acct, 2)
                    If acct(1, i) <> 0 Then
                        artrans.ownbld = BN
                        artrans.acct = acct(0, i)
                        artrans.date = dt
                        artrans.desc = "Cash Receipts     "
                        artrans.amount = Format$(acct(1, i), "00000000.00;-0000000.00")
                        Put #fi, LOF(fi) + 1, artrans
                    End If
                Next
         
                Erase acct, ca
                ReDim acct(2, 0)
                ReDim ca(2, 0)
            End If
            Return
          
glproc_error:
            MsgBox "", vbCritical, sProcID
            Resume glproc_exit
    
glproc_exit:
           
End Sub
Public Function cformat(c As Variant, n As Integer) As String
           
    Dim x As String, f As String
           
            On Error GoTo cformat_error
            cformat = " "
            cformat = Format(n)
            f = String$(n - 4, "#")
            f = f & "0.00"
            x = Format$(c, f)
            cformat = String$(n - Len(x), " ") & x
            GoTo cformat_exit
    
cformat_error:
            Resume cformat_exit ' 06/23/99
    
cformat_exit:
           
End Function
         */
    }

    public class ChargePayment
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public ChargePayment() { }
    }

    public class artranstype
    {
        public string ownbld{ get; set;} // 5
        public string acct { get; set;}   // 7
        public string date { get; set;}   // 6
        public string desc { get; set;}   // 18
        public string amount { get; set; } // 11
        // crlf As String * 2

        public artranstype() {}
    }

}
