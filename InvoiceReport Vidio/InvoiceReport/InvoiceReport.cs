using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ACCPAC.Advantage;
using System.Net;
using Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace InvoiceReport
{

    public partial class InvoiceReport : Form
    {
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        private string passphrase = "sage300accpac";
        //private string passfile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Invoice Report/Pass.txt";
        private string passfile = "E:/Sage/Auto Generate Reports/VIDIO DOT COM PT/AP Aging/Config/Pass.txt";
        //private string recipientfile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Invoice Report/Recipient.txt";
        private string recipientfile = "E:/Sage/Auto Generate Reports/VIDIO DOT COM PT/AP Aging/Config/Recipient.txt";
        //private string senderfile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Invoice Report/Sender.txt";
        private string senderfile = "E:/Sage/Auto Generate Reports/VIDIO DOT COM PT/AP Aging/Config/Sender.txt";

        public struct Summary
        {
            public string currency;
            public double total_current;
            public double total_1_30;
            public double total_31_60;
            public double total_61_90;
            public double total_91_180;
            public double total_over_180;
            public double total_overdue;
            public double total_balance_source;
            public int status;
        }

        public InvoiceReport()
        {
            InitializeComponent();
        }

        private void InvoiceReport_Load(object sender, EventArgs e)
        {
            try
            {
                Connect();

                string StringSql = @"SELECT a.IDVEND, b.VENDNAME, a.IDINVC, a.DESCINVC, a.DATEINVC, a.DATEINVCDU, a.CODECURN, a.AMTINVCTC, a.AMTINVCHC, a.AUDTORG, d.SUM1, d.SUM2, DATEDIFF(d, CONVERT(varchar, a.DATEINVCDU,  103), GETDATE())
                                   FROM APOBL a
                                   LEFT OUTER JOIN APVEN b ON a.IDVEND=b.VENDORID
                                   LEFT OUTER JOIN (SELECT IDVEND, IDINVC, SUM(AMTPAYMTC) SUM1, SUM(AMTPAYMHC) SUM2 FROM APOBP GROUP BY IDVEND, IDINVC) d ON a.IDVEND=d.IDVEND AND a.IDINVC=d.IDINVC
                                   ORDER BY a.IDVEND, a.IDINVC";


                csQry.Browse(StringSql, true);
                csQry.InternalSet(256);

                //Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Invoice Report");
                //string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Invoice Report/AP Aging VIDDAT " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".xlsx";
                string filePath = "E:/Sage/Auto Generate Reports/VIDIO DOT COM PT/AP Aging/Reports/AP Aging VIDDAT " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".xlsx";
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);

                Summary[] Summary_Invoice = new Summary[9];
                Summary_Invoice[0].currency = "IDR";
                Summary_Invoice[1].currency = "USD";
                Summary_Invoice[2].currency = "SGD";
                Summary_Invoice[3].currency = "EUR";
                Summary_Invoice[4].currency = "CAD";
                Summary_Invoice[5].currency = "CNY";
                Summary_Invoice[6].currency = "AUD";
                Summary_Invoice[7].currency = "NZD";
                Summary_Invoice[8].currency = "ZAR";

                int i;
                for (i = 0; i < 9; i++)
                {
                    Summary_Invoice[i].total_current = 0;
                    Summary_Invoice[i].total_1_30 = 0;
                    Summary_Invoice[i].total_31_60 = 0;
                    Summary_Invoice[i].total_61_90 = 0;
                    Summary_Invoice[i].total_91_180 = 0;
                    Summary_Invoice[i].total_over_180 = 0;
                    Summary_Invoice[i].total_overdue = 0;
                    Summary_Invoice[i].total_balance_source = 0;
                    Summary_Invoice[i].status = 0;
                }


                //-----------------------------SHEET 1---------------------------------------------------------------------
                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Name = "DETAIL";

                xlWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;
                xlWorkSheet.Cells[1, 1] = "'" + "ID VENDOR"; //IDVEND
                xlWorkSheet.Cells[1, 2] = "'" + "VENDOR NAME"; //VENDNAME
                xlWorkSheet.Cells[1, 3] = "'" + "DOCUMENT NO"; //IDINVC
                xlWorkSheet.Cells[1, 4] = "'" + "DESCRIPTION"; //DESCINVC
                xlWorkSheet.Cells[1, 5] = "'" + "DATE DOC"; //DATEINVC
                xlWorkSheet.Cells[1, 6] = "'" + "DATE DUE"; //DATEINVCDU
                xlWorkSheet.Cells[1, 7] = "'" + "CURRENCY"; //CODECURN
                xlWorkSheet.Cells[1, 8] = "'" + "ORIGINAL INVOICE"; //AMTINVCTC
                xlWorkSheet.Cells[1, 9] = "'" + "DAYS OVER";
                xlWorkSheet.Cells[1, 10] = "'" + "CURRENT";
                xlWorkSheet.Cells[1, 11] = "'" + "1-30";
                xlWorkSheet.Cells[1, 12] = "'" + "31-60";
                xlWorkSheet.Cells[1, 13] = "'" + "61-90";
                xlWorkSheet.Cells[1, 14] = "'" + "91-180";
                xlWorkSheet.Cells[1, 15] = "'" + "OVER 180";
                xlWorkSheet.Cells[1, 16] = "'" + "TOTAL OVERDUE";
                xlWorkSheet.Cells[1, 17] = "'" + "BALANCE SOURCE";
                xlWorkSheet.Cells[1, 18] = "'" + "BALANCE FUNCTION";


                int numrow = 2;
                string IDTemp = "";
                string NameTemp = "";
                double vendorTotalCurrent = 0;
                double vendorTotal_1_30 = 0;
                double vendorTotal_31_60 = 0;
                double vendorTotal_61_90 = 0;
                double vendorTotal_91_180 = 0;
                double vendorTotal_Over_180 = 0;
                double vendorTotalOverdue = 0;
                double vendorTotalBalanceSource = 0;
                double vendorTotalBalanceFunction = 0;

                while (csQry.Fetch(false))
                {
                   
                    double balanceSource = GetBalance(double.Parse(csQry.Fields[7].Value.ToString()), double.Parse(csQry.Fields[10].Value.ToString())); ;
                    double balanceFunction = GetBalance(double.Parse(csQry.Fields[8].Value.ToString()), double.Parse(csQry.Fields[11].Value.ToString()));

                    DateTime DATEINVC = DateTime.ParseExact(csQry.Fields[4].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    DateTime DATEINVCDU = DateTime.ParseExact(csQry.Fields[5].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    double daysOver = double.Parse(csQry.Fields[12].Value.ToString());

                    if (balanceSource != 0 || balanceFunction != 0)
                    {
                        if (csQry.Fields[0].Value.ToString() != IDTemp && IDTemp != "")
                        {
                            xlWorkSheet.Cells[numrow, 1].EntireRow.Interior.Color = XlRgbColor.rgbLightSkyBlue;
                            xlWorkSheet.Cells[numrow, 1].EntireRow.Font.Bold = true;
                            xlWorkSheet.Cells[numrow, 1] = "'" + IDTemp;
                            xlWorkSheet.Cells[numrow, 1].Font.Bold = false;
                            xlWorkSheet.Cells[numrow, 2] = "'" + NameTemp;
                            xlWorkSheet.Cells[numrow, 2].Font.Bold = false;
                            xlWorkSheet.Cells[numrow, 9] = "Sub Total";
                            xlWorkSheet.Cells[numrow, 10] = vendorTotalCurrent;
                            xlWorkSheet.Cells[numrow, 11] = vendorTotal_1_30;
                            xlWorkSheet.Cells[numrow, 12] = vendorTotal_31_60;
                            xlWorkSheet.Cells[numrow, 13] = vendorTotal_61_90;
                            xlWorkSheet.Cells[numrow, 14] = vendorTotal_91_180;
                            xlWorkSheet.Cells[numrow, 15] = vendorTotal_Over_180;
                            xlWorkSheet.Cells[numrow, 16] = vendorTotalOverdue;
                            xlWorkSheet.Cells[numrow, 17] = vendorTotalBalanceSource;
                            xlWorkSheet.Cells[numrow, 18] = vendorTotalBalanceFunction;
                            vendorTotalCurrent = 0;
                            vendorTotal_1_30 = 0;
                            vendorTotal_31_60 = 0;
                            vendorTotal_61_90 = 0;
                            vendorTotal_91_180 = 0;
                            vendorTotal_Over_180 = 0;
                            vendorTotalOverdue = 0;
                            vendorTotalBalanceSource = 0;
                            vendorTotalBalanceFunction = 0;
                            numrow = numrow + 2;
                        }

                        xlWorkSheet.Cells[numrow, 1] = "'" + csQry.Fields[0].Value.ToString();
                        xlWorkSheet.Cells[numrow, 2] = "'" + csQry.Fields[1].Value.ToString();
                        xlWorkSheet.Cells[numrow, 3] = "'" + csQry.Fields[2].Value.ToString();
                        xlWorkSheet.Cells[numrow, 4] = "'" + csQry.Fields[3].Value.ToString();
                        xlWorkSheet.Cells[numrow, 5] = DATEINVC.ToString("dd/MM/yyyy");
                        xlWorkSheet.Cells[numrow, 6] = DATEINVCDU.ToString("dd/MM/yyyy");
                        xlWorkSheet.Cells[numrow, 7] = "'" + csQry.Fields[6].Value.ToString();
                        xlWorkSheet.Cells[numrow, 8] = csQry.Fields[7].Value.ToString();
                        xlWorkSheet.Cells[numrow, 9] = daysOver;
                        xlWorkSheet.Cells[numrow, 10] = GetCurrent(daysOver, balanceSource);
                        xlWorkSheet.Cells[numrow, 11] = Get_1_30(daysOver, balanceSource);
                        xlWorkSheet.Cells[numrow, 12] = Get_31_60(daysOver, balanceSource);
                        xlWorkSheet.Cells[numrow, 13] = Get_61_90(daysOver, balanceSource);
                        xlWorkSheet.Cells[numrow, 14] = Get_91_180(daysOver, balanceSource);
                        xlWorkSheet.Cells[numrow, 15] = Get_Over_180(daysOver, balanceSource);
                        xlWorkSheet.Cells[numrow, 16] = GetTotalOverdue(daysOver, balanceSource);
                        xlWorkSheet.Cells[numrow, 17] = balanceSource;
                        xlWorkSheet.Cells[numrow, 18] = balanceFunction;

                        IDTemp = csQry.Fields[0].Value.ToString();
                        NameTemp = csQry.Fields[1].Value.ToString();
                        vendorTotalCurrent = vendorTotalCurrent + GetCurrent(daysOver, balanceSource);
                        vendorTotal_1_30 = vendorTotal_1_30 + Get_1_30(daysOver, balanceSource);
                        vendorTotal_31_60 = vendorTotal_31_60 + Get_31_60(daysOver, balanceSource);
                        vendorTotal_61_90 = vendorTotal_61_90 + Get_61_90(daysOver, balanceSource);
                        vendorTotal_91_180 = vendorTotal_91_180 + Get_91_180(daysOver, balanceSource);
                        vendorTotal_Over_180 = vendorTotal_Over_180 + Get_Over_180(daysOver, balanceSource);
                        vendorTotalOverdue = vendorTotalOverdue + GetTotalOverdue(daysOver, balanceSource);
                        vendorTotalBalanceSource = vendorTotalBalanceSource + balanceSource;
                        vendorTotalBalanceFunction = vendorTotalBalanceFunction + balanceFunction;

                        SetAllSummary(csQry.Fields[6].Value.ToString(), 
                                        GetCurrent(daysOver, balanceSource),
                                        Get_1_30(daysOver, balanceSource),
                                        Get_31_60(daysOver, balanceSource),
                                        Get_61_90(daysOver, balanceSource),
                                        Get_91_180(daysOver, balanceSource),
                                        Get_Over_180(daysOver, balanceSource),
                                        GetTotalOverdue(daysOver, balanceSource),
                                        balanceSource,
                                        ref Summary_Invoice);
                        numrow++;
                    }
                }
                xlWorkSheet.Cells[numrow, 1].EntireRow.Interior.Color = XlRgbColor.rgbLightSkyBlue;
                xlWorkSheet.Cells[numrow, 1].EntireRow.Font.Bold = true;
                xlWorkSheet.Cells[numrow, 1] = "'" + IDTemp;
                xlWorkSheet.Cells[numrow, 1].Font.Bold = false;
                xlWorkSheet.Cells[numrow, 2] = "'" + NameTemp;
                xlWorkSheet.Cells[numrow, 2].Font.Bold = false;
                xlWorkSheet.Cells[numrow, 9] = "Sub Total";
                xlWorkSheet.Cells[numrow, 10] = vendorTotalCurrent;
                xlWorkSheet.Cells[numrow, 11] = vendorTotal_1_30;
                xlWorkSheet.Cells[numrow, 12] = vendorTotal_31_60;
                xlWorkSheet.Cells[numrow, 13] = vendorTotal_61_90;
                xlWorkSheet.Cells[numrow, 14] = vendorTotal_91_180;
                xlWorkSheet.Cells[numrow, 15] = vendorTotal_Over_180;
                xlWorkSheet.Cells[numrow, 16] = vendorTotalOverdue;
                xlWorkSheet.Cells[numrow, 17] = vendorTotalBalanceSource;
                xlWorkSheet.Cells[numrow, 18] = vendorTotalBalanceFunction;

                /*dynamic allDataRange = xlWorkSheet.UsedRange;
                allDataRange.Sort(allDataRange.Columns[7], XlSortOrder.xlAscending, // the first sort key Column 1 for Range
                allDataRange.Columns[9], Type.Missing, XlSortOrder.xlDescending,// second sort key Column 6 of the range
                Type.Missing, XlSortOrder.xlAscending,  // third sort key nothing, but it wants one
                XlYesNoGuess.xlGuess, Type.Missing, Type.Missing,
                XlSortOrientation.xlSortColumns, XlSortMethod.xlPinYin,
                XlSortDataOption.xlSortNormal,
                XlSortDataOption.xlSortNormal,
                XlSortDataOption.xlSortNormal);*/

                xlWorkSheet.Columns.AutoFit();

                //--------------------------------------SHEET 2------------------------------------------------------

                Worksheet xlWorkSheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
                xlWorkSheet2.Name = "SUMMARY";

                xlWorkSheet2.Cells[1, 1].EntireRow.Font.Bold = true;
                xlWorkSheet2.Cells[1, 1] = "'" + "CURRENCY"; //CODECURN
                xlWorkSheet2.Cells[1, 2] = "'" + "CURRENT";
                xlWorkSheet2.Cells[1, 3] = "'" + "1-30";
                xlWorkSheet2.Cells[1, 4] = "'" + "31-60";
                xlWorkSheet2.Cells[1, 5] = "'" + "61-90";
                xlWorkSheet2.Cells[1, 6] = "'" + "91-180";
                xlWorkSheet2.Cells[1, 7] = "'" + "OVER 180";
                xlWorkSheet2.Cells[1, 8].EntireColumn.Interior.Color = XlRgbColor.rgbYellow;
                xlWorkSheet2.Cells[1, 8] = "'" + "TOTAL OVERDUE";
                xlWorkSheet2.Cells[1, 9] = "'" + "BALANCE SOURCE";

                int numrow2 = 2;
                for (i = 0; i < 9; i++)
                {
                    if (Summary_Invoice[i].status == 1)
                    {
                        xlWorkSheet2.Cells[numrow2, 1] = "'" + Summary_Invoice[i].currency;
                        xlWorkSheet2.Cells[numrow2, 2] = Summary_Invoice[i].total_current;
                        xlWorkSheet2.Cells[numrow2, 3] = Summary_Invoice[i].total_1_30;
                        xlWorkSheet2.Cells[numrow2, 4] = Summary_Invoice[i].total_31_60;
                        xlWorkSheet2.Cells[numrow2, 5] = Summary_Invoice[i].total_61_90;
                        xlWorkSheet2.Cells[numrow2, 6] = Summary_Invoice[i].total_91_180;
                        xlWorkSheet2.Cells[numrow2, 7] = Summary_Invoice[i].total_over_180;
                        xlWorkSheet2.Cells[numrow2, 8] = Summary_Invoice[i].total_overdue;
                        xlWorkSheet2.Cells[numrow2, 9] = Summary_Invoice[i].total_balance_source;
                        numrow2++;
                    }
                }

                xlWorkSheet2.Columns.AutoFit();

                xlWorkBook.Saved = true;
                xlWorkBook.SaveCopyAs(filePath);

                xlWorkBook.Close();
                xlApp.Quit();

                SendEmail(filePath);

                session.Dispose();
                System.Windows.Forms.Application.Exit();
            }
            catch (SessionException)
            {
                MessageBox.Show("Please connect to Sage Server first", "Connection Error");
                System.Windows.Forms.Application.Exit();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
                System.Windows.Forms.Application.Exit();
            }
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open("ADM", "ADM123456", "VIDDAT", DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            csQry = mDBLinkCmpRW.OpenView("CS0120");

        }

        private double GetBalance(double amount, double sum)
        {
            if (sum != 0.000)
            {
                return amount + sum;
            }
            else
            {
                return amount;
            }
        }

        private double GetCurrent(double daysOver, double balance)
        {
            if(daysOver <= 0)
            {
                return balance;
            }
            else
            {
                return 0;
            }
        }

        private double Get_1_30(double daysOver, double balance)
        {
            if (daysOver >= 1 && daysOver <=30)
            {
                return balance;
            }
            else
            {
                return 0;
            }
        }

        private double Get_31_60(double daysOver, double balance)
        {
            if (daysOver >= 31 && daysOver <= 60)
            {
                return balance;
            }
            else
            {
                return 0;
            }
        }

        private double Get_61_90(double daysOver, double balance)
        {
            if (daysOver >= 61 && daysOver <= 90)
            {
                return balance;
            }
            else
            {
                return 0;
            }
        }

        private double Get_91_180(double daysOver, double balance)
        {
            if (daysOver >= 91 && daysOver <= 180)
            {
                return balance;
            }
            else
            {
                return 0;
            }
        }

        private double Get_Over_180(double daysOver, double balance)
        {
            if (daysOver>180)
            {
                return balance;
            }
            else
            {
                return 0;
            }
        }

        private double GetTotalOverdue(double daysOver, double balance)
        {
            if (daysOver>1)
            {
                return balance;
            }
            else
            {
                return 0;
            }
        }

        private void SetAllSummary(string currency, double amount_current, 
                                                    double amount_1_30,
                                                    double amount_31_60,
                                                    double amount_61_90,
                                                    double amount_91_180,
                                                    double amount_over_180,
                                                    double amount_total_overdue,
                                                    double amount_total_balance_source,
                                                    ref Summary[] sum)
        {
            if (currency == "IDR")
            {
                sum[0].status = 1;
                sum[0].total_current = sum[0].total_current + amount_current;
                sum[0].total_1_30 = sum[0].total_1_30 + amount_1_30;
                sum[0].total_31_60 = sum[0].total_31_60 + amount_31_60;
                sum[0].total_61_90 = sum[0].total_61_90 + amount_61_90;
                sum[0].total_91_180 = sum[0].total_91_180 + amount_91_180;
                sum[0].total_over_180 = sum[0].total_over_180 + amount_over_180;
                sum[0].total_overdue = sum[0].total_overdue + amount_total_overdue;
                sum[0].total_balance_source = sum[0].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "USD")
            {
                sum[1].status = 1;
                sum[1].total_current = sum[1].total_current + amount_current;
                sum[1].total_1_30 = sum[1].total_1_30 + amount_1_30;
                sum[1].total_31_60 = sum[1].total_31_60 + amount_31_60;
                sum[1].total_61_90 = sum[1].total_61_90 + amount_61_90;
                sum[1].total_91_180 = sum[1].total_91_180 + amount_91_180;
                sum[1].total_over_180 = sum[1].total_over_180 + amount_over_180;
                sum[1].total_overdue = sum[1].total_overdue + amount_total_overdue;
                sum[1].total_balance_source = sum[1].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "SGD")
            {
                sum[2].status = 1;
                sum[2].total_current = sum[2].total_current + amount_current;
                sum[2].total_1_30 = sum[2].total_1_30 + amount_1_30;
                sum[2].total_31_60 = sum[2].total_31_60 + amount_31_60;
                sum[2].total_61_90 = sum[2].total_61_90 + amount_61_90;
                sum[2].total_91_180 = sum[2].total_91_180 + amount_91_180;
                sum[2].total_over_180 = sum[2].total_over_180 + amount_over_180;
                sum[2].total_overdue = sum[2].total_overdue + amount_total_overdue;
                sum[2].total_balance_source = sum[2].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "EUR")
            {
                sum[3].status = 1;
                sum[3].total_current = sum[3].total_current + amount_current;
                sum[3].total_1_30 = sum[3].total_1_30 + amount_1_30;
                sum[3].total_31_60 = sum[3].total_31_60 + amount_31_60;
                sum[3].total_61_90 = sum[3].total_61_90 + amount_61_90;
                sum[3].total_91_180 = sum[3].total_91_180 + amount_91_180;
                sum[3].total_over_180 = sum[3].total_over_180 + amount_over_180;
                sum[3].total_overdue = sum[3].total_overdue + amount_total_overdue;
                sum[3].total_balance_source = sum[3].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "CAD")
            {
                sum[4].status = 1;
                sum[4].total_current = sum[4].total_current + amount_current;
                sum[4].total_1_30 = sum[4].total_1_30 + amount_1_30;
                sum[4].total_31_60 = sum[4].total_31_60 + amount_31_60;
                sum[4].total_61_90 = sum[4].total_61_90 + amount_61_90;
                sum[4].total_91_180 = sum[4].total_91_180 + amount_91_180;
                sum[4].total_over_180 = sum[4].total_over_180 + amount_over_180;
                sum[4].total_overdue = sum[4].total_overdue + amount_total_overdue;
                sum[4].total_balance_source = sum[4].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "CNY")
            {
                sum[5].status = 1;
                sum[5].total_current = sum[5].total_current + amount_current;
                sum[5].total_1_30 = sum[5].total_1_30 + amount_1_30;
                sum[5].total_31_60 = sum[5].total_31_60 + amount_31_60;
                sum[5].total_61_90 = sum[5].total_61_90 + amount_61_90;
                sum[5].total_91_180 = sum[5].total_91_180 + amount_91_180;
                sum[5].total_over_180 = sum[5].total_over_180 + amount_over_180;
                sum[5].total_overdue = sum[5].total_overdue + amount_total_overdue;
                sum[5].total_balance_source = sum[5].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "AUD")
            {
                sum[6].status = 1;
                sum[6].total_current = sum[6].total_current + amount_current;
                sum[6].total_1_30 = sum[6].total_1_30 + amount_1_30;
                sum[6].total_31_60 = sum[6].total_31_60 + amount_31_60;
                sum[6].total_61_90 = sum[6].total_61_90 + amount_61_90;
                sum[6].total_91_180 = sum[6].total_91_180 + amount_91_180;
                sum[6].total_over_180 = sum[6].total_over_180 + amount_over_180;
                sum[6].total_overdue = sum[6].total_overdue + amount_total_overdue;
                sum[6].total_balance_source = sum[6].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "NZD")
            {
                sum[7].status = 1;
                sum[7].total_current = sum[7].total_current + amount_current;
                sum[7].total_1_30 = sum[7].total_1_30 + amount_1_30;
                sum[7].total_31_60 = sum[7].total_31_60 + amount_31_60;
                sum[7].total_61_90 = sum[7].total_61_90 + amount_61_90;
                sum[7].total_91_180 = sum[7].total_91_180 + amount_91_180;
                sum[7].total_over_180 = sum[7].total_over_180 + amount_over_180;
                sum[7].total_overdue = sum[7].total_overdue + amount_total_overdue;
                sum[7].total_balance_source = sum[7].total_balance_source + amount_total_balance_source;
            }
            else if (currency == "ZAR")
            {
                sum[8].status = 1;
                sum[8].total_current = sum[8].total_current + amount_current;
                sum[8].total_1_30 = sum[8].total_1_30 + amount_1_30;
                sum[8].total_31_60 = sum[8].total_31_60 + amount_31_60;
                sum[8].total_61_90 = sum[8].total_61_90 + amount_61_90;
                sum[8].total_91_180 = sum[8].total_91_180 + amount_91_180;
                sum[8].total_over_180 = sum[8].total_over_180 + amount_over_180;
                sum[8].total_overdue = sum[8].total_overdue + amount_total_overdue;
                sum[8].total_balance_source = sum[8].total_balance_source + amount_total_balance_source;
            }
        }

        private void SendEmail(string fileLocation)
        {
            string line;
            string sender = File.ReadLines(senderfile).First();
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(sender);

            StreamReader file = new StreamReader(recipientfile);
            while ((line = file.ReadLine()) != null)
            {
                mail.To.Add(line);

            }
            file.Close();

            mail.Subject = "AP Aging - VIDDAT - " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss");
            mail.Body = "";

            Attachment attachment;
            attachment = new Attachment(fileLocation);
            attachment.ContentDisposition.FileName = "AP Aging - VIDDAT - " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".xlsx";
            mail.Attachments.Add(attachment);

            string pass = File.ReadLines(passfile).First();

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(sender, Decrypt(pass,passphrase));
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

        private static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        private static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
