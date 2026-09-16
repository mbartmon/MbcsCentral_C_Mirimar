using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace MbcsCentral.WinGL
{
    public class Invoices : clsDB, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static string TABLE = "gl_Invoices";

        public static string BASE_TABLE = "gl_Invoices";

        public const string SQL_COL_DATE = "Date";
        public const string SQL_COL_PAY_DUE_DATE = "PayDueDate";
        public const string SQL_COL_DATE_LAST_PAID = "DateLastPaid";
        public const string SQL_COL_CHECK_DATE = "CheckDate";
        public const string SQL_COL_CHECK_GROUP = "CheckGroup";
        public const string SQL_COL_PAY_FLAG = "PayFlag";
        public const string SQL_COL_VENDOR_NUMBER = "VendorNumber";
        public const string SQL_COL_BATCH = "Batch";
        public const string SQL_COL_CHECK_NUMBER = "CheckNumber";
        public const string SQL_COL_COMPANY = "Company";
        public const string SQL_COL_DEPARTMENT = "Department";
        public const string SQL_COL_INVOICE_KEY = "InvoiceKey";
        public const string SQL_COL_AMOUNT = "Amount";
        public const string SQL_COL_ALLOWANCE = "Allowance";
        public const string SQL_COL_DISCOUNT = "Discount";
        public const string SQL_COL_DISCOUNT_AMOUNT = "DiscountAmount";
        public const string SQL_COL_AMOUNT_PAID = "AmountPaid";
        public const string SQL_COL_AMOUNT_TO_PAY = "AmountToPay";
        public const string SQL_COL_ALLOCATION_FLAG = "AllocationFlag";
        public const string SQL_COL_INVOICE_NUMBER = "InvoiceNumber";
        public const string SQL_COL_DESCRIPTION = "Description";
        public const string SQL_COL_MEMO = "Memo";
        public const string SQL_COL_MEMO_ID = "MemoId";
        public const string SQL_COL_PREFIX = "Prefix";
        public const string SQL_COL_SUFFIX = "Suffix";
        public const string SQL_COL_EFT = "EFT";
        public const string SQL_COL_DISCOUNT_DOLLAR = "DiscDollar";
        public const string SQL_COL_ACH = "ACH";
        public const string SQL_COL_ACH_TYPE = "AchType";

        static string SQL_LOAD = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +  
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_INVOICE_KEY + "] = ?;";

        public static string SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                TABLE + ".[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                TABLE + ".[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +
                 "[" + SQL_COL_DISCOUNT_DOLLAR + "]," +
                "[" + Vendors.TABLE + "].[" + Vendors.SQL_COL_VENDOR_NAME + "]," +
                "[" + WinGL.Department.TABLE + "].[" + WinGL.Department.SQL_COL_NAME + "]" +
                " FROM " + TABLE + ", " + Vendors.TABLE + ", " + WinGL.Department.TABLE +
                " WHERE " + Vendors.TABLE + ".[" + Vendors.SQL_COL_VENDOR_NUMBER + "] = " + TABLE + ".[" + SQL_COL_VENDOR_NUMBER +
                "] AND " + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + TABLE + "." + SQL_COL_DEPARTMENT + 
                " ORDER BY [" + SQL_COL_INVOICE_KEY + "]";

        public static string SQL_LOAD_FOR_SELECTION = "SELECT " +
        "[" + SQL_COL_DATE + "]," +
        "[" + SQL_COL_PAY_DUE_DATE + "]," +
        "[" + SQL_COL_DATE_LAST_PAID + "]," +
        "[" + SQL_COL_CHECK_DATE + "]," +
        "[" + SQL_COL_CHECK_GROUP + "]," +
        "[" + SQL_COL_PAY_FLAG + "]," +
        TABLE + ".[" + SQL_COL_VENDOR_NUMBER + "]," +
        "[" + SQL_COL_BATCH + "]," +
        "[" + SQL_COL_CHECK_NUMBER + "]," +
        "[" + SQL_COL_COMPANY + "]," +
        "[" + SQL_COL_DEPARTMENT + "]," +
        "[" + SQL_COL_INVOICE_KEY + "]," +
        "[" + SQL_COL_AMOUNT + "]," +
        "[" + SQL_COL_ALLOWANCE + "]," +
        "[" + SQL_COL_DISCOUNT + "]," +
        "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
        "[" + SQL_COL_AMOUNT_PAID + "]," +
        "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
        "[" + SQL_COL_ALLOCATION_FLAG + "]," +
        "[" + SQL_COL_INVOICE_NUMBER + "]," +
        "[" + SQL_COL_DESCRIPTION + "]," +
        "[" + SQL_COL_MEMO + "]," +
        "[" + SQL_COL_MEMO_ID + "]," +
        "[" + SQL_COL_PREFIX + "]," +
        "[" + SQL_COL_SUFFIX + "]," +
        "[" + SQL_COL_EFT + "]," +
        TABLE + ".[" + SQL_COL_ACH + "]," +
        "[" + SQL_COL_ACH_TYPE + "]," +
       "[" + SQL_COL_DISCOUNT_DOLLAR + "]," +
        "[" + Vendors.TABLE + "].[" + Vendors.SQL_COL_VENDOR_NAME + "]," +
        "[" + Vendors.TABLE + "].[" + Vendors.SQL_COL_STATE_I_D + "]," +
        "[" + WinGL.Department.TABLE + "].[" + WinGL.Department.SQL_COL_NAME + "]" +
        " FROM " + TABLE + ", " + Vendors.TABLE + ", " + WinGL.Department.TABLE +
        " WHERE " + Vendors.TABLE + ".[" + Vendors.SQL_COL_VENDOR_NUMBER + "] = " + TABLE + ".[" + SQL_COL_VENDOR_NUMBER +
        "] AND " + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + TABLE + "." + SQL_COL_DEPARTMENT + " ";


        private static string SQL_LOAD_BY_INVOICE = "SELECT " +
                                                    "[" + SQL_COL_DATE + "]," +
                                                    "[" + SQL_COL_PAY_DUE_DATE + "]," +
                                                    "[" + SQL_COL_DATE_LAST_PAID + "]," +
                                                    "[" + SQL_COL_CHECK_DATE + "]," +
                                                    "[" + SQL_COL_CHECK_GROUP + "]," +
                                                    "[" + SQL_COL_PAY_FLAG + "]," +
                                                    "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                                    "[" + SQL_COL_BATCH + "]," +
                                                    "[" + SQL_COL_CHECK_NUMBER + "]," +
                                                    "[" + SQL_COL_COMPANY + "]," +
                                                    "[" + SQL_COL_DEPARTMENT + "]," +
                                                    "[" + SQL_COL_INVOICE_KEY + "]," +
                                                    "[" + SQL_COL_AMOUNT + "]," +
                                                    "[" + SQL_COL_ALLOWANCE + "]," +
                                                    "[" + SQL_COL_DISCOUNT + "]," +
                                                    "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                                                    "[" + SQL_COL_AMOUNT_PAID + "]," +
                                                    "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                                                    "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                                                    "[" + SQL_COL_INVOICE_NUMBER + "]," +
                                                    "[" + SQL_COL_DESCRIPTION + "]," +
                                                    "[" + SQL_COL_MEMO + "]," +
                                                    "[" + SQL_COL_MEMO_ID + "]," +
                                                    "[" + SQL_COL_PREFIX + "]," +
                                                    "[" + SQL_COL_SUFFIX + "]," +
                                                    "[" + SQL_COL_EFT + "]," +
                                                    "[" + SQL_COL_ACH + "]," +
                                                    "[" + SQL_COL_ACH_TYPE + "]," +
                                                    "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                                                    " FROM " + TABLE +
                                                    " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                                                    " AND " + "[" + SQL_COL_INVOICE_NUMBER + "] = '?'";

        private static string SQL_LOAD_BY_VENDOR = "SELECT " +
                                                   "[" + SQL_COL_DATE + "]," +
                                                   "[" + SQL_COL_PAY_DUE_DATE + "]," +
                                                   "[" + SQL_COL_DATE_LAST_PAID + "]," +
                                                   "[" + SQL_COL_CHECK_DATE + "]," +
                                                   "[" + SQL_COL_CHECK_GROUP + "]," +
                                                   "[" + SQL_COL_PAY_FLAG + "]," +
                                                   "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                                   "[" + SQL_COL_BATCH + "]," +
                                                   "[" + SQL_COL_CHECK_NUMBER + "]," +
                                                   "[" + SQL_COL_COMPANY + "]," +
                                                   "[" + SQL_COL_DEPARTMENT + "]," +
                                                   "[" + SQL_COL_INVOICE_KEY + "]," +
                                                   "[" + SQL_COL_AMOUNT + "]," +
                                                   "[" + SQL_COL_ALLOWANCE + "]," +
                                                   "[" + SQL_COL_DISCOUNT + "]," +
                                                   "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                                                   "[" + SQL_COL_AMOUNT_PAID + "]," +
                                                   "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                                                   "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                                                   "[" + SQL_COL_INVOICE_NUMBER + "]," +
                                                   "[" + SQL_COL_DESCRIPTION + "]," +
                                                   "[" + SQL_COL_MEMO + "]," +
                                                   "[" + SQL_COL_MEMO_ID + "]," +
                                                   "[" + SQL_COL_PREFIX + "]," +
                                                   "[" + SQL_COL_SUFFIX + "]," +
                                                   "[" + SQL_COL_EFT + "]," +
                                                   "[" + SQL_COL_ACH + "]," +
                                                   "[" + SQL_COL_ACH_TYPE + "]," +
                                                   "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                                                   " FROM " + TABLE +
                                                   " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?";

        private static string SQL_LOAD_BY_BATCH = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_BATCH + "] = ?";


        private static string SQL_LOAD_FOR_PAYMENTS = "SELECT " +
                                                       "[" + SQL_COL_DATE + "]," +
                                                       "[" + SQL_COL_PAY_DUE_DATE + "]," +
                                                       "[" + SQL_COL_DATE_LAST_PAID + "]," +
                                                       "[" + SQL_COL_CHECK_DATE + "]," +
                                                       "[" + SQL_COL_CHECK_GROUP + "]," +
                                                       "[" + SQL_COL_PAY_FLAG + "]," +
                                                       "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                                       "[" + SQL_COL_BATCH + "]," +
                                                       "[" + SQL_COL_CHECK_NUMBER + "]," +
                                                       "[" + SQL_COL_COMPANY + "]," +
                                                       "[" + SQL_COL_DEPARTMENT + "]," +
                                                       "[" + SQL_COL_INVOICE_KEY + "]," +
                                                       "[" + SQL_COL_AMOUNT + "]," +
                                                       "[" + SQL_COL_ALLOWANCE + "]," +
                                                       "[" + SQL_COL_DISCOUNT + "]," +
                                                       "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                                                       "[" + SQL_COL_AMOUNT_PAID + "]," +
                                                       "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                                                       "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                                                       "[" + SQL_COL_INVOICE_NUMBER + "]," +
                                                       "[" + SQL_COL_DESCRIPTION + "]," +
                                                       "[" + SQL_COL_MEMO + "]," +
                                                       "[" + SQL_COL_MEMO_ID + "]," +
                                                       "[" + SQL_COL_PREFIX + "]," +
                                                       "[" + SQL_COL_SUFFIX + "]," +
                                                       "[" + SQL_COL_EFT + "]," +
                                                       "[" + SQL_COL_ACH + "]," +
                                                       "[" + SQL_COL_ACH_TYPE + "]," +
                                                       "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                                                       " FROM " + TABLE +
                                                       " WHERE " + "[" + SQL_COL_CHECK_NUMBER + "] = 0" +
                                                       " AND " + "[" + SQL_COL_ALLOCATION_FLAG + "] = 1" +
                                                       " AND " + "[" + SQL_COL_ACH + "] = 0" +
                                                       " AND [" + SQL_COL_PAY_DUE_DATE + "] <= '?'";

        private static string SQL_LOAD_FOR_CHECKS = "SELECT " +
                                                    "[" + SQL_COL_DATE + "]," +
                                                    "[" + SQL_COL_PAY_DUE_DATE + "]," +
                                                    "[" + SQL_COL_DATE_LAST_PAID + "]," +
                                                    "[" + SQL_COL_CHECK_DATE + "]," +
                                                    "[" + SQL_COL_CHECK_GROUP + "]," +
                                                    "[" + SQL_COL_PAY_FLAG + "]," +
                                                    "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                                    "[" + SQL_COL_BATCH + "]," +
                                                    "[" + SQL_COL_CHECK_NUMBER + "]," +
                                                    "[" + SQL_COL_COMPANY + "]," +
                                                    "[" + SQL_COL_DEPARTMENT + "]," +
                                                    "[" + SQL_COL_INVOICE_KEY + "]," +
                                                    "[" + SQL_COL_AMOUNT + "]," +
                                                    "[" + SQL_COL_ALLOWANCE + "]," +
                                                    "[" + SQL_COL_DISCOUNT + "]," +
                                                    "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                                                    "[" + SQL_COL_AMOUNT_PAID + "]," +
                                                    "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                                                    "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                                                    "[" + SQL_COL_INVOICE_NUMBER + "]," +
                                                    "[" + SQL_COL_DESCRIPTION + "]," +
                                                    "[" + SQL_COL_MEMO + "]," +
                                                    "[" + SQL_COL_MEMO_ID + "]," +
                                                    "[" + SQL_COL_PREFIX + "]," +
                                                    "[" + SQL_COL_SUFFIX + "]," +
                                                    "[" + SQL_COL_EFT + "]," +
                                                    "[" + SQL_COL_ACH + "]," +
                                                    "[" + SQL_COL_ACH_TYPE + "]," +
                                                    "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                                                    " FROM " + TABLE +
                                                    " WHERE " + "[" + SQL_COL_CHECK_NUMBER + "] = 0" +
                                                    " AND (" + "[" + SQL_COL_PAY_FLAG + "] = 1" +
                                                    " OR " + "[" + SQL_COL_PAY_FLAG + "] = 2)" +
                                                    " ORDER BY [" + SQL_COL_COMPANY + "]," +
                                                    "[" + SQL_COL_DEPARTMENT + "]," +
                                                    "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                                    "[" + SQL_COL_CHECK_GROUP + "]," +
                                                    "[" + SQL_COL_INVOICE_KEY + "]";

        private static string SQL_LOAD_FOR_ACH = "SELECT " +
                                            "[" + SQL_COL_DATE + "]," +
                                            "[" + SQL_COL_PAY_DUE_DATE + "]," +
                                            "[" + SQL_COL_DATE_LAST_PAID + "]," +
                                            "[" + SQL_COL_CHECK_DATE + "]," +
                                            "[" + SQL_COL_CHECK_GROUP + "]," +
                                            "[" + SQL_COL_PAY_FLAG + "]," +
                                            "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                            "[" + SQL_COL_BATCH + "]," +
                                            "[" + SQL_COL_CHECK_NUMBER + "]," +
                                            "[" + SQL_COL_COMPANY + "]," +
                                            "[" + SQL_COL_DEPARTMENT + "]," +
                                            "[" + SQL_COL_INVOICE_KEY + "]," +
                                            "[" + SQL_COL_AMOUNT + "]," +
                                            "[" + SQL_COL_ALLOWANCE + "]," +
                                            "[" + SQL_COL_DISCOUNT + "]," +
                                            "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                                            "[" + SQL_COL_AMOUNT_PAID + "]," +
                                            "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                                            "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                                            "[" + SQL_COL_INVOICE_NUMBER + "]," +
                                            "[" + SQL_COL_DESCRIPTION + "]," +
                                            "[" + SQL_COL_MEMO + "]," +
                                            "[" + SQL_COL_MEMO_ID + "]," +
                                            "[" + SQL_COL_PREFIX + "]," +
                                            "[" + SQL_COL_SUFFIX + "]," +
                                            "[" + SQL_COL_EFT + "]," +
                                            "[" + SQL_COL_ACH + "]," +
                                            "[" + SQL_COL_ACH_TYPE + "]," +
                                            "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                                            " FROM " + TABLE +
                                            " WHERE " + "[" + SQL_COL_ACH + "] = 1" +                                           
                                            " ORDER BY " + 
                                            " DATEPART(year," + SQL_COL_CHECK_DATE  + "), DATEPART(month," + SQL_COL_CHECK_DATE  + "), DATEPART(day," + SQL_COL_CHECK_DATE  + ")," +
                                            "[" + SQL_COL_ACH_TYPE + "]," +
                                            "[" + SQL_COL_COMPANY + "]," + 
                                            "[" + SQL_COL_DEPARTMENT + "]," +
                                            "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                            "[" + SQL_COL_CHECK_GROUP + "]," +
                                            "[" + SQL_COL_INVOICE_KEY + "]";
        /*
                                                     " WHERE " + "[" + SQL_COL_CHECK_NUMBER + "] > 0" +
                                            " AND " + "[" + SQL_COL_ACH + "] = 1" +
                                            " AND (" + "[" + SQL_COL_PAY_FLAG + "] = 1" +
                                            " OR " + "[" + SQL_COL_PAY_FLAG + "] = 2)" +
                                            " ORDER BY " + "[" + SQL_COL_CHECK_DATE + "]" +
                                            "[" + SQL_COL_COMPANY + "]," + 
                                            "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                            "[" + SQL_COL_DEPARTMENT + "]," +
                                            "[" + SQL_COL_VENDOR_NUMBER + "]," +
                                            "[" + SQL_COL_CHECK_GROUP + "]," +
                                            "[" + SQL_COL_INVOICE_KEY + "]";

         */


        static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
            "[" + SQL_COL_DATE + "]," +
            "[" + SQL_COL_PAY_DUE_DATE + "]," +
            "[" + SQL_COL_DATE_LAST_PAID + "]," +
            "[" + SQL_COL_CHECK_DATE + "]," +
            "[" + SQL_COL_CHECK_GROUP + "]," +
            "[" + SQL_COL_PAY_FLAG + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]," +
            "[" + SQL_COL_BATCH + "]," +
            "[" + SQL_COL_CHECK_NUMBER + "]," +
            "[" + SQL_COL_COMPANY + "]," +
            "[" + SQL_COL_DEPARTMENT + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_ALLOWANCE + "]," +
            "[" + SQL_COL_DISCOUNT + "]," +
            "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
            "[" + SQL_COL_AMOUNT_PAID + "]," +
            "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
            "[" + SQL_COL_ALLOCATION_FLAG + "]," +
            "[" + SQL_COL_INVOICE_NUMBER + "]," +
            "[" + SQL_COL_DESCRIPTION + "]," +
            "[" + SQL_COL_MEMO + "]," +
            "[" + SQL_COL_MEMO_ID + "]," +
            "[" + SQL_COL_PREFIX + "]," +
            "[" + SQL_COL_SUFFIX + "]," +
            "[" + SQL_COL_EFT + "]," +
            "[" + SQL_COL_ACH + "]," +
            "[" + SQL_COL_ACH_TYPE + "]," +
            "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
            ") VALUES ('?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', ?, '?', '?', ?, ?, '?', ?);";

        static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_DATE + "]='?'," +
                "[" + SQL_COL_PAY_DUE_DATE + "]='?'," +
                "[" + SQL_COL_DATE_LAST_PAID + "]='?'," +
                "[" + SQL_COL_CHECK_DATE + "]='?'," +
                "[" + SQL_COL_CHECK_GROUP + "]=?," +
                "[" + SQL_COL_PAY_FLAG + "]=?," +
                "[" + SQL_COL_VENDOR_NUMBER + "]=?," +
                "[" + SQL_COL_BATCH + "]=?," +
                "[" + SQL_COL_CHECK_NUMBER + "]=?," +
                "[" + SQL_COL_COMPANY + "]=?," +
                "[" + SQL_COL_DEPARTMENT + "]=?," +
                "[" + SQL_COL_AMOUNT + "]=?," +
                "[" + SQL_COL_ALLOWANCE + "]=?," +
                "[" + SQL_COL_DISCOUNT + "]=?," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]=?," +
                "[" + SQL_COL_AMOUNT_PAID + "]=?," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]=?," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]=?," +
                "[" + SQL_COL_INVOICE_NUMBER + "]='?'," +
                "[" + SQL_COL_DESCRIPTION + "]='?'," +
                "[" + SQL_COL_MEMO + "]='?'," +
                "[" + SQL_COL_MEMO_ID + "]=? ," +
                "[" + SQL_COL_PREFIX + "]='?'," +
                "[" + SQL_COL_SUFFIX + "]='?'," +
                "[" + SQL_COL_EFT + "]=?," +
                "[" + SQL_COL_ACH + "]=?," +
                "[" + SQL_COL_ACH_TYPE + "]='?'," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]=?" +
                " WHERE " + "[" + SQL_COL_INVOICE_KEY + "] = ?;";

        protected DateTime m_Date;
        protected DateTime m_PayDueDate;
        protected DateTime m_DateLastPaid;
        protected DateTime m_CheckDate;
        protected short m_CheckGroup;
        protected short m_PayFlag;
        protected int m_VendorNumber;
        protected int m_Batch;
        protected int m_CheckNumber;
        protected int m_Company;
        protected int m_Department;
        protected int m_InvoiceKey;
        protected decimal m_Amount;
        protected decimal m_Allowance;
        protected decimal m_Discount;
        protected decimal m_DiscountAmount;
        protected decimal m_AmountPaid;
        protected decimal m_AmountToPay;
        protected bool m_AllocationFlag;
        protected string m_InvoiceNumber;
        protected string m_Description;
        protected string m_Memo;
        protected int m_MemoId;
        protected string m_Prefix;
        protected string m_Suffix;
        protected bool m_DiscountDollar;
        protected bool m_PrePaid = false;
        protected bool m_EFT;
        protected bool m_ACH;
        protected string m_ACH_TYPE;
        protected bool m_isChanged;

        // InvoiceList
        protected string m_VendorName;
        protected string m_StateId;
        protected string m_DepartmentName;
        protected Vendors m_Vendor;

        public enum PayFlags
        {
            DoNotPay,
            Pay,
            PayForceDiscount,
            NotSelected,
            Paid,
            EFT
        }

        public static char CompanyDelimiter = '^';
        public static char TenantDelimiter = '~';

        public Invoices(string table = "")
        {
            if (!table.Trim().Equals(""))
                TABLE = table;
            base.localTable = TABLE;
            base.priKeyName = SQL_COL_INVOICE_KEY;
            base.connectionString = GlobalShared.CN.ConnectionString;
            BuildSqlStatements();
        }
        //public Invoices() : base(TABLE, SQL_COL_INVOICE_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        //{
        //}

        public Invoices(DateTime Date, DateTime PayDueDate, DateTime DateLastPaid, DateTime CheckDate, short CheckGroup, short PayFlag, int VendorNumber, int Batch, int CheckNumber, int Company, int Department, int InvoiceKey, decimal Amount, decimal Allowance, decimal Discount, decimal DiscountAmount, decimal AmountPaid, decimal AmountToPay, bool AllocationFlag, string InvoiceNumber, string Description, string Memo) :
                base(TABLE, SQL_COL_INVOICE_KEY, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            m_Date = Date;
            m_PayDueDate = PayDueDate;
            m_DateLastPaid = DateLastPaid;
            m_CheckDate = CheckDate;
            m_CheckGroup = CheckGroup;
            m_PayFlag = PayFlag;
            m_VendorNumber = VendorNumber;
            m_Batch = Batch;
            m_CheckNumber = CheckNumber;
            m_Company = Company;
            m_Department = Department;
            m_InvoiceKey = InvoiceKey;
            m_Amount = Amount;
            m_Allowance = Allowance;
            m_Discount = Discount;
            m_DiscountAmount = DiscountAmount;
            m_AmountPaid = AmountPaid;
            m_AmountToPay = AmountToPay;
            m_AllocationFlag = AllocationFlag;
            m_InvoiceNumber = InvoiceNumber;
            m_Description = Description;
            m_Memo = Memo;
        }

        private void BuildSqlStatements()
        {
            SQL_LOAD = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_INVOICE_KEY + "] = ?;";
            base.LoadStmt = SQL_LOAD;

            SQL_LOAD_ALL = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                TABLE + ".[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                TABLE + ".[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]," +
                "[" + Vendors.TABLE + "].[" + Vendors.SQL_COL_VENDOR_NAME + "]," +
                "[" + Vendors.TABLE + "].[" + Vendors.SQL_COL_STATE_I_D + "]," +
                "[" + WinGL.Department.TABLE + "].[" + WinGL.Department.SQL_COL_NAME + "]" +
                " FROM " + TABLE + ", " + Vendors.TABLE + ", " + WinGL.Department.TABLE +
                " WHERE " + Vendors.TABLE + ".[" + Vendors.SQL_COL_VENDOR_NUMBER + "] = " + TABLE + ".[" + SQL_COL_VENDOR_NUMBER +
                "] AND " + WinGL.Department.TABLE + ".[" + WinGL.Department.SQL_COL_DEPARTMENT_NUMBER + "] = " + TABLE + "." + SQL_COL_DEPARTMENT;
            base.loadAllStmt = SQL_LOAD_ALL;

            SQL_DELETE = "DELETE FROM " + TABLE + " WHERE " + priKeyName + " = ?;";

            SQL_LOAD_BY_INVOICE = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?" +
                " AND " + "[" + SQL_COL_INVOICE_NUMBER + "] = '?'";

            SQL_LOAD_BY_VENDOR = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_VENDOR_NUMBER + "] = ?";

            SQL_LOAD_BY_BATCH = "SELECT " +
                "[" + SQL_COL_DATE + "]," +
                "[" + SQL_COL_PAY_DUE_DATE + "]," +
                "[" + SQL_COL_DATE_LAST_PAID + "]," +
                "[" + SQL_COL_CHECK_DATE + "]," +
                "[" + SQL_COL_CHECK_GROUP + "]," +
                "[" + SQL_COL_PAY_FLAG + "]," +
                "[" + SQL_COL_VENDOR_NUMBER + "]," +
                "[" + SQL_COL_BATCH + "]," +
                "[" + SQL_COL_CHECK_NUMBER + "]," +
                "[" + SQL_COL_COMPANY + "]," +
                "[" + SQL_COL_DEPARTMENT + "]," +
                "[" + SQL_COL_INVOICE_KEY + "]," +
                "[" + SQL_COL_AMOUNT + "]," +
                "[" + SQL_COL_ALLOWANCE + "]," +
                "[" + SQL_COL_DISCOUNT + "]," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
                "[" + SQL_COL_AMOUNT_PAID + "]," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]," +
                "[" + SQL_COL_INVOICE_NUMBER + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_MEMO + "]," +
                "[" + SQL_COL_MEMO_ID + "]," +
                "[" + SQL_COL_PREFIX + "]," +
                "[" + SQL_COL_SUFFIX + "]," +
                "[" + SQL_COL_EFT + "]," +
                "[" + SQL_COL_ACH + "]," +
                "[" + SQL_COL_ACH_TYPE + "]," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_BATCH + "] = ?";

            SQL_INSERT = "INSERT INTO " + TABLE + " (" +
            "[" + SQL_COL_DATE + "]," +
            "[" + SQL_COL_PAY_DUE_DATE + "]," +
            "[" + SQL_COL_DATE_LAST_PAID + "]," +
            "[" + SQL_COL_CHECK_DATE + "]," +
            "[" + SQL_COL_CHECK_GROUP + "]," +
            "[" + SQL_COL_PAY_FLAG + "]," +
            "[" + SQL_COL_VENDOR_NUMBER + "]," +
            "[" + SQL_COL_BATCH + "]," +
            "[" + SQL_COL_CHECK_NUMBER + "]," +
            "[" + SQL_COL_COMPANY + "]," +
            "[" + SQL_COL_DEPARTMENT + "]," +
            "[" + SQL_COL_AMOUNT + "]," +
            "[" + SQL_COL_ALLOWANCE + "]," +
            "[" + SQL_COL_DISCOUNT + "]," +
            "[" + SQL_COL_DISCOUNT_AMOUNT + "]," +
            "[" + SQL_COL_AMOUNT_PAID + "]," +
            "[" + SQL_COL_AMOUNT_TO_PAY + "]," +
            "[" + SQL_COL_ALLOCATION_FLAG + "]," +
            "[" + SQL_COL_INVOICE_NUMBER + "]," +
            "[" + SQL_COL_DESCRIPTION + "]," +
            "[" + SQL_COL_MEMO + "]," +
            "[" + SQL_COL_MEMO_ID + "]," +
            "[" + SQL_COL_PREFIX + "]," +
            "[" + SQL_COL_SUFFIX + "]," +
            "[" + SQL_COL_EFT + "]," +
            "[" + SQL_COL_ACH + "]," +
            "[" + SQL_COL_ACH_TYPE + "]," +
            "[" + SQL_COL_DISCOUNT_DOLLAR + "]" +
            ") VALUES ('?', '?', '?', '?', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', ?, '?', '?', ?, ?, '?', ?);";
            base.insertStmt = SQL_INSERT;

            SQL_UPDATE = "UPDATE " + TABLE + " SET " +
                "[" + SQL_COL_DATE + "]='?'," +
                "[" + SQL_COL_PAY_DUE_DATE + "]='?'," +
                "[" + SQL_COL_DATE_LAST_PAID + "]='?'," +
                "[" + SQL_COL_CHECK_DATE + "]='?'," +
                "[" + SQL_COL_CHECK_GROUP + "]=?," +
                "[" + SQL_COL_PAY_FLAG + "]=?," +
                "[" + SQL_COL_VENDOR_NUMBER + "]=?," +
                "[" + SQL_COL_BATCH + "]=?," +
                "[" + SQL_COL_CHECK_NUMBER + "]=?," +
                "[" + SQL_COL_COMPANY + "]=?," +
                "[" + SQL_COL_DEPARTMENT + "]=?," +
                "[" + SQL_COL_AMOUNT + "]=?," +
                "[" + SQL_COL_ALLOWANCE + "]=?," +
                "[" + SQL_COL_DISCOUNT + "]=?," +
                "[" + SQL_COL_DISCOUNT_AMOUNT + "]=?," +
                "[" + SQL_COL_AMOUNT_PAID + "]=?," +
                "[" + SQL_COL_AMOUNT_TO_PAY + "]=?," +
                "[" + SQL_COL_ALLOCATION_FLAG + "]=?," +
                "[" + SQL_COL_INVOICE_NUMBER + "]='?'," +
                "[" + SQL_COL_DESCRIPTION + "]='?'," +
                "[" + SQL_COL_MEMO + "]='?'," +
                "[" + SQL_COL_MEMO_ID + "]=?," +
                "[" + SQL_COL_PREFIX + "]='?'," +
                "[" + SQL_COL_SUFFIX + "]='?'," +
                "[" + SQL_COL_EFT + "]=?," +
                "[" + SQL_COL_ACH + "]=?," +
                "[" + SQL_COL_ACH_TYPE + "]='?'," +
                "[" + SQL_COL_DISCOUNT_DOLLAR + "]=?" +
                " WHERE " + "[" + SQL_COL_INVOICE_KEY + "] = ?;";
            base.updateStmt = SQL_UPDATE;

        }

        protected Invoices fill(DataRow dr, bool IsList = false)
        {
            try {
                m_Date = (DateTime)(Convert.IsDBNull(dr[SQL_COL_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_DATE]);
                m_PayDueDate = (DateTime)(Convert.IsDBNull(dr[SQL_COL_PAY_DUE_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_PAY_DUE_DATE]);
                m_DateLastPaid = (DateTime)(Convert.IsDBNull(dr[SQL_COL_DATE_LAST_PAID]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_DATE_LAST_PAID]);
                m_CheckDate = (DateTime)(Convert.IsDBNull(dr[SQL_COL_CHECK_DATE]) ? new DateTime(1900, 1, 1, 0, 0, 0) : dr[SQL_COL_CHECK_DATE]);
                m_CheckGroup = (short)dr[SQL_COL_CHECK_GROUP];
                m_PayFlag = (short)dr[SQL_COL_PAY_FLAG];
                m_VendorNumber = (int)dr[SQL_COL_VENDOR_NUMBER];
                m_Batch = (int)dr[SQL_COL_BATCH];
                m_CheckNumber = (int)dr[SQL_COL_CHECK_NUMBER];
                m_Company = (int)dr[SQL_COL_COMPANY];
                m_Department = (int)dr[SQL_COL_DEPARTMENT];
                m_InvoiceKey = (int)dr[SQL_COL_INVOICE_KEY];
                m_Amount = (decimal)dr[SQL_COL_AMOUNT];
                m_Allowance = (decimal)dr[SQL_COL_ALLOWANCE];
                m_Discount = (decimal)dr[SQL_COL_DISCOUNT];
                m_DiscountAmount = (decimal)dr[SQL_COL_DISCOUNT_AMOUNT];
                m_AmountPaid = (decimal)dr[SQL_COL_AMOUNT_PAID];
                m_AmountToPay = (decimal)dr[SQL_COL_AMOUNT_TO_PAY];
                m_AllocationFlag = (bool)dr[SQL_COL_ALLOCATION_FLAG];
                m_InvoiceNumber = (string)(Convert.IsDBNull(dr[SQL_COL_INVOICE_NUMBER]) ? "" : dr[SQL_COL_INVOICE_NUMBER]);
                m_Description = (string)(Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
                m_Memo = (string)(Convert.IsDBNull(dr[SQL_COL_MEMO]) ? "" : dr[SQL_COL_MEMO]);
                m_MemoId = (int)dr[SQL_COL_MEMO_ID];
                m_Prefix = (string)dr[SQL_COL_PREFIX];
                m_Suffix = (string)dr[SQL_COL_SUFFIX];
                m_EFT = (dr[SQL_COL_EFT] is DBNull ? false : (bool)dr[SQL_COL_EFT]);
                m_ACH = (dr[SQL_COL_ACH] is DBNull ? false : (bool)dr[SQL_COL_ACH]);
                m_ACH_TYPE = (string)(Convert.IsDBNull(dr[SQL_COL_ACH_TYPE]) ? "" : dr[SQL_COL_ACH_TYPE]);
                m_DiscountDollar = (dr[SQL_COL_DISCOUNT_DOLLAR] is DBNull ? false : (bool)dr[SQL_COL_DISCOUNT_DOLLAR]);
                if (IsList)
                {
                    m_VendorName = (string)(Convert.IsDBNull(dr[Vendors.SQL_COL_VENDOR_NAME]) ? "" : dr[Vendors.SQL_COL_VENDOR_NAME]);
                    m_StateId = (string)(Convert.IsDBNull(dr[Vendors.SQL_COL_STATE_I_D]) ? "" : dr[Vendors.SQL_COL_STATE_I_D]);
                    m_DepartmentName = (string)(Convert.IsDBNull(dr[WinGL.Department.SQL_COL_NAME]) ? "" : dr[WinGL.Department.SQL_COL_NAME]);
                    m_Vendor = Vendors.load(m_VendorNumber);
                }
            } catch (SqlException ex) {
            }

            return this;

        }

        public static Invoices load(int m_InvoiceKey, string instanceTable = "")
        {
            Invoices obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));
            string[] opt = { m_InvoiceKey.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0) {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            } else
                return null;

        }

        public static Invoices[] load(string instanceTable = "")
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Invoices obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));

            string xsql = SQL_LOAD_ALL + " ORDER BY " + Vendors.SQL_COL_VENDOR_NAME;

            ds = obj.load(xsql, null);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows) {
                obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));
                obj.fill(dr, true);
                list.Add(obj);
                obj = null;
            }
            try {
                return (Invoices[])list.ToArray(typeof(Invoices));
            } catch (SqlException ex) {
                return null;
            }

        }

        public static Invoices[] load(bool EFT, bool ACH, string instanceTable = "")
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Invoices obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));

            ds = obj.loadAll();
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));
                obj.fill(dr, true);
                if ((EFT && !obj.EFT) || (!EFT && obj.EFT))
                    continue;
                if ((ACH && !obj.ACH) || (!ACH && obj.ACH))
                    continue;
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Invoices[])list.ToArray(typeof(Invoices));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static Invoices LoadByInvoice(int vendorNumber, string invoiceNumber, string instanceTable = "")
        {
            Invoices obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));
            string[] opt = { vendorNumber.ToString(), invoiceNumber };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;


        }

        public static Invoices[] loadByVendor(int vendor, string instanceTable = "")
        {
            string[] opt = { vendor.ToString() };

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Invoices obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));

            ds = obj.load(SQL_LOAD_BY_VENDOR, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Invoices[])list.ToArray(typeof(Invoices));
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public static Invoices[] loadForBatch(int batch, string instanceTable = "")
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Invoices obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));

            string[] opt = { batch.ToString() };
            //TODO: Add ORDER BY BuildingNumber, VendorName
            string xsql = SQL_LOAD_ALL + " AND " + SQL_COL_BATCH + "=?";
            xsql += " ORDER BY " + Vendors.SQL_COL_VENDOR_NAME;
            ds = obj.load(xsql, opt);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = (instanceTable.Trim() == "" ? new Invoices() : new Invoices(instanceTable));
                obj.fill(dr, true);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Invoices[])list.ToArray(typeof(Invoices));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static Invoices[] LoadForPayment(DateTime payDate, bool add = false)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Invoices obj = new Invoices();

            string[] opt = { payDate.ToString() };
            //if (add)
            //   SQL_LOAD_FOR_PAYMENTS += " AND [" + SQL_COL_PAY_FLAG + "] <= 0";
            ds = obj.load(SQL_LOAD_FOR_PAYMENTS, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Invoices();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Invoices[])list.ToArray(typeof(Invoices));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public static Invoices[] LoadForChecks(bool ACH = false, DateTime ?PayDueDate = null)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Invoices obj = new Invoices();

            string[] opt = null;
            if (ACH)
            {
                Array.Resize(ref opt, 1);
                opt[0] = PayDueDate.ToString();
                int idx = SQL_LOAD_FOR_ACH.IndexOf("WHERE");
                string xsql = SQL_LOAD_FOR_ACH.Substring(0, idx + 5) + " " + SQL_COL_PAY_DUE_DATE + " = '?' AND " + SQL_LOAD_FOR_ACH.Substring(idx + 5);
                ds = obj.load(xsql, opt);
            }
            else
                ds = obj.load(SQL_LOAD_FOR_CHECKS, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Invoices();
                obj.fill(dr);
                if (!ACH || obj.Amount - obj.AmountPaid != 0)
                    list.Add(obj);
                obj = null;
            }
            try
            {
                return (Invoices[])list.ToArray(typeof(Invoices));
            }
            catch (SqlException ex)
            {
                return null;
            }

        }
        public static Invoices[] LoadQuery(string query, bool add = false)
        {
            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Invoices obj = new Invoices();
            string[] opt = null;

            ds = obj.load(query, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new Invoices();
                obj.fill(dr, add);
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (Invoices[])list.ToArray(typeof(Invoices));
            }
            catch (SqlException ex)
            {
                return null;
            }


        }

        public bool save(bool report = false)
        {
            if (m_InvoiceKey > 0) {
                if (report)
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "Updating Check# " + m_CheckNumber + " Invoice# " + m_InvoiceNumber + " Vendor: " + m_VendorNumber + " Check# " + m_CheckNumber, false);
                string[] values = {m_Date.ToString(), m_PayDueDate.ToString(), m_DateLastPaid.ToString(), m_CheckDate.ToString(), m_CheckGroup.ToString(),
                                   m_PayFlag.ToString(), m_VendorNumber.ToString(), m_Batch.ToString(), m_CheckNumber.ToString(), m_Company.ToString(),
                                   m_Department.ToString(), m_Amount.ToString(), m_Allowance.ToString(), m_Discount.ToString(), m_DiscountAmount.ToString(),
                                   m_AmountPaid.ToString(), m_AmountToPay.ToString(), m_AllocationFlag.ToString(), m_InvoiceNumber, m_Description, m_Memo,
                                   m_MemoId.ToString(), m_Prefix, m_Suffix, m_EFT.ToString(), m_ACH.ToString(), m_ACH_TYPE, m_DiscountDollar.ToString(), m_InvoiceKey.ToString()};
                if (this.updateRecord(m_InvoiceKey.ToString(), values, (string)(report ? TABLE : ""))) {
                    if (report)
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "UPDATE OK", false);
                    return true;
                } else {
                    if (report)
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "Unable to update Check# " + m_CheckNumber + " Invoice# " + m_InvoiceNumber + " Vendor: " + m_VendorNumber + " Check# " + m_CheckNumber, false);

                    return false;
                }
            } else {
                string[] values = {m_Date.ToString(), m_PayDueDate.ToString(), m_DateLastPaid.ToString(), m_CheckDate.ToString(), m_CheckGroup.ToString(),
                                   m_PayFlag.ToString(), m_VendorNumber.ToString(), m_Batch.ToString(), m_CheckNumber.ToString(), m_Company.ToString(),
                                   m_Department.ToString(), m_Amount.ToString(), m_Allowance.ToString(), m_Discount.ToString(), m_DiscountAmount.ToString(),
                                   m_AmountPaid.ToString(), m_AmountToPay.ToString(), m_AllocationFlag.ToString(), m_InvoiceNumber, m_Description, m_Memo,
                                   m_MemoId.ToString(), m_Prefix, m_Suffix, m_EFT.ToString(), m_ACH.ToString(), m_ACH_TYPE, m_DiscountDollar.ToString()};
                m_InvoiceKey = this.insertRecord(values);
                if (m_InvoiceKey >= 0) {
                    return true;
                }

            }
            return false;

        }
        public void SetTable(string table)
        {
            TABLE = table;
            BuildSqlStatements();
        }

        private void OnPropertyChanged(string info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
                m_isChanged = true;
            }
        }

        // Properties

        public DateTime Date {
            set { m_Date = value;
                OnPropertyChanged("Date"); }
            get { return m_Date; }
        }

        public DateTime PayDueDate {
            set { m_PayDueDate = value;
                OnPropertyChanged("PayDueDate"); }
            get { return m_PayDueDate; }
        }

        public DateTime DateLastPaid {
            set { m_DateLastPaid = value;
                OnPropertyChanged("DateLastPaid"); }
            get { return m_DateLastPaid; }
        }

        public DateTime CheckDate {
            set { m_CheckDate = value;
                OnPropertyChanged("CheckDate"); }
            get { return m_CheckDate; }
        }

        public short CheckGroup {
            set { m_CheckGroup = value;
                OnPropertyChanged("CheckGroup"); }
            get { return m_CheckGroup; }
        }

        public short PayFlag {
            set { m_PayFlag = value;
                OnPropertyChanged("PayFlag"); }
            get { return m_PayFlag; }
        }

        public int VendorNumber {
            set { m_VendorNumber = value;
                OnPropertyChanged("VendorNumber"); }
            get { return m_VendorNumber; }
        }

        public int Batch {
            set { m_Batch = value;
                OnPropertyChanged("Batch"); }
            get { return m_Batch; }
        }

        public int CheckNumber {
            set { m_CheckNumber = value;
                OnPropertyChanged("CheckNumber"); }
            get { return m_CheckNumber; }
        }

        public int Company {
            set { m_Company = value;
                OnPropertyChanged("Company"); }
            get { return m_Company; }
        }

        public int Department {
            set { m_Department = value;
                OnPropertyChanged("Department"); }
            get { return m_Department; }
        }

        public int InvoiceKey {
            get { return m_InvoiceKey; }
        }

        public decimal Amount {
            set { m_Amount = value;
                OnPropertyChanged("Amount"); }
            get { return m_Amount; }
        }

        public decimal Allowance {
            set { m_Allowance = value;
                OnPropertyChanged("Allowance"); }
            get { return m_Allowance; }
        }

        public decimal Discount {
            set { m_Discount = value;
                OnPropertyChanged("Discount"); }
            get { return m_Discount; }
        }

        public decimal DiscountAmount {
            set { m_DiscountAmount = value;
                OnPropertyChanged("DiscountAmount"); }
            get { return m_DiscountAmount; }
        }

        public bool DiscountDollar
        {
            set
            {
                m_DiscountDollar = value;
                OnPropertyChanged("DiscountDollar");
            }
            get { return m_DiscountDollar; }
        }


        public decimal AmountPaid {
            set { m_AmountPaid = value;
                OnPropertyChanged("AmountPaid"); }
            get { return m_AmountPaid; }
        }

        public decimal AmountToPay {
            set { m_AmountToPay = value;
                OnPropertyChanged("AmountToPay"); }
            get { return m_AmountToPay; }
        }

        public bool AllocationFlag {
            set { m_AllocationFlag = value;
                OnPropertyChanged("AllocationFlag"); }
            get { return m_AllocationFlag; }
        }

        public string InvoiceNumber {
            set { m_InvoiceNumber = value;
                OnPropertyChanged("InvoiceNumber"); }
            get { return m_InvoiceNumber; }
        }

        public string Description {
            set { m_Description = value;
                OnPropertyChanged("Description"); }
            get { return m_Description; }
        }

        public string Memo {
            set { m_Memo = value;
                OnPropertyChanged("Memo"); }
            get { return m_Memo; }
        }

        public int MemoId
        {
            set
            {
                m_MemoId = value;
                OnPropertyChanged("MemoId");
            }
            get { return m_MemoId; }
        }

        public string Prefix
        {
            set { m_Prefix = value;
                OnPropertyChanged("Prefix");
            }
            get { return m_Prefix; }
        }

        public string Suffix
        {
            set { m_Suffix = value;
                OnPropertyChanged("Suffix");
            }
            get { return m_Suffix; }
        }

        public bool EFT
        {
            set { m_EFT = value;
                OnPropertyChanged("EFT");
            }
            get { return m_EFT; }
        }

        public bool ACH
        {
            set
            {
                m_ACH = value;
                OnPropertyChanged("ACH");
            }
            get { return m_ACH; }   
        }


        public string ACH_TYPE
        {
            get { return m_ACH_TYPE; }
            set 
            { 
                m_ACH_TYPE = value;
                OnPropertyChanged("ACH_TYPE");
            }
        }

        public string VendorName
        {
         //   get { return m_VendorName; }
          get { return m_VendorName.Trim() + (StateID.Replace("\0", "").Trim() != "" ? " {" + StateID.Replace("\0","").Trim() + "}" : ""); }
        }
        public string StateID
        {
            get { return m_StateId; }
        }

        public string DepartmentName
        {
            get { return m_DepartmentName; }
        }

        public Vendors Vendor
        {
            get { return m_Vendor; }
        }

        public bool PrePaid
        {
            get { return m_PrePaid; }
            set
            {
                m_PrePaid = value;
                OnPropertyChanged("PrePaid");
            }
        }

        public string FullInvoiceNumber { get { return (m_Prefix.Trim() + " " + m_InvoiceNumber.Trim() + " " + m_Suffix).Trim();} }

        public decimal AmountWithDiscount
        {
            get
            {
                decimal amount = this.Amount;
                decimal allowance = this.Allowance;
                decimal discount = this.Discount;
                bool dollar = this.DiscountDollar;

                if (this.DiscountDollar)
                    discount = this.Discount;
                else
                    discount = System.Math.Round((amount - allowance) * (this.Discount * (decimal)0.01), 2);

                return amount - discount;
            }
        }
        public Brush RowForegroundBrush { get; set; } = Brushes.Black;
    }
}
