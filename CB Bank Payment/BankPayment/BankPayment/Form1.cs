using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACCPAC.Advantage;
using System.Net;
using Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Linq;
using System.IO;
using System.Globalization;

namespace BankPayment
{
    public partial class BankPayment : Form
    {
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        private List<Tuple<string, string, double>> BankSubTotal = new List<Tuple<string, string, double>>();

        public BankPayment()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open("ADMIN", "ADMS4G3COM1", "KMKDAT", DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            csQry = mDBLinkCmpRW.OpenView("CS0120");

        }

        private void BankPayment_Load(object sender, EventArgs e)
        {
            try
            {
                Connect();

                string StringSql = @"SELECT b.BANKCODE, a.BANKNAME, b.FISCYR, b.PERIOD, b.DATE, 
                                   b.REFERENCE, b.TEXTDESC, b.BK2GLCURSR, b.BANKAMOUNT, 
                                   b.BATCHID, b.ENTRYNO, c.LEGALNAME
                                   FROM CBBANK a 
                                   INNER JOIN CBPJHD b ON a.BANKCODE = b.BANKCODE
                                   INNER JOIN CSCOM c ON b.AUDTORG = c.ORGID
                                   WHERE  b.BANKAMOUNT < 0
                                   ORDER BY b.BANKCODE, b.DATE";


                csQry.Browse(StringSql, true);
                csQry.InternalSet(256);

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/CB Bank Payment");
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/CB Bank Payment/CB Bank Payment KMKDAT " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".xlsx";
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);


                //-----------------------------SHEET 1---------------------------------------------------------------------
                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Name = "BANK PAYMENT";

                xlWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;
                xlWorkSheet.Cells[1, 1] = "'" + "BANK CODE"; //BANKCODE
                xlWorkSheet.Cells[1, 2] = "'" + "BANK NAME"; //BANKNAME
                xlWorkSheet.Cells[1, 3] = "'" + "YEAR"; //FISCYR
                xlWorkSheet.Cells[1, 4] = "'" + "PRD"; //PERIOD
                xlWorkSheet.Cells[1, 5] = "'" + "DATE"; //DATE
                xlWorkSheet.Cells[1, 6] = "'" + "REFERENCE"; //REFERENCE
                xlWorkSheet.Cells[1, 7] = "'" + "BATCH NO"; //BATCHID
                xlWorkSheet.Cells[1, 8] = "'" + "ENTRY NO"; //ENTRYNO
                xlWorkSheet.Cells[1, 9] = "'" + "DESCRIPTION";//TEXTDESC
                xlWorkSheet.Cells[1, 10] = "'" + "CURRENCY";//BK2GLCURSR
                xlWorkSheet.Cells[1, 11] = "'" + "BANK AMOUNT";//BANKAMOUNT


                int numrow = 2;
                double tempAmount = 0;
                string IDTemp = "";
                string NameTemp = "";

                while (csQry.Fetch(false))
                {
                    DateTime transactionDate = DateTime.ParseExact(csQry.Fields[4].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    double amount = double.Parse(csQry.Fields[8].Value.ToString())*-1;

                    if (csQry.Fields[0].Value.ToString() != IDTemp && IDTemp != "")
                    {
                        xlWorkSheet.Cells[numrow, 1].EntireRow.Interior.Color = XlRgbColor.rgbLightSkyBlue;
                        xlWorkSheet.Cells[numrow, 1].EntireRow.Font.Bold = true;
                        xlWorkSheet.Cells[numrow, 1] = "'" + IDTemp;
                        xlWorkSheet.Cells[numrow, 1].Font.Bold = false;
                        xlWorkSheet.Cells[numrow, 2] = "'" + NameTemp;
                        xlWorkSheet.Cells[numrow, 2].Font.Bold = false;
                        xlWorkSheet.Cells[numrow, 10] = "Sub Total";
                        xlWorkSheet.Cells[numrow, 11] = tempAmount;
                        BankSubTotal.Add(new Tuple<string, string, double>(IDTemp, NameTemp, tempAmount));
                        tempAmount = 0;
                        numrow = numrow + 2;
                    }

                    xlWorkSheet.Cells[numrow, 1] = "'" + csQry.Fields[0].Value.ToString();
                    xlWorkSheet.Cells[numrow, 2] = "'" + csQry.Fields[1].Value.ToString();
                    xlWorkSheet.Cells[numrow, 3] = "'" + csQry.Fields[2].Value.ToString();
                    xlWorkSheet.Cells[numrow, 4] = "'" + csQry.Fields[3].Value.ToString();
                    xlWorkSheet.Cells[numrow, 5] = "'" + transactionDate.ToString("MM/dd/yyyy");
                    xlWorkSheet.Cells[numrow, 6] = "'" + csQry.Fields[5].Value.ToString();
                    xlWorkSheet.Cells[numrow, 7] = "'" + csQry.Fields[9].Value.ToString();
                    xlWorkSheet.Cells[numrow, 8] = "'" + csQry.Fields[10].Value.ToString();
                    xlWorkSheet.Cells[numrow, 9] = "'" + csQry.Fields[6].Value.ToString();
                    xlWorkSheet.Cells[numrow, 10] = "'" + csQry.Fields[7].Value.ToString();
                    xlWorkSheet.Cells[numrow, 11] = amount;

                    IDTemp = csQry.Fields[0].Value.ToString();
                    NameTemp = csQry.Fields[1].Value.ToString();
                    tempAmount = tempAmount + amount;
                    numrow++;
                }
                xlWorkSheet.Cells[numrow, 1].EntireRow.Interior.Color = XlRgbColor.rgbLightSkyBlue;
                xlWorkSheet.Cells[numrow, 1].EntireRow.Font.Bold = true;
                xlWorkSheet.Cells[numrow, 1] = "'" + IDTemp;
                xlWorkSheet.Cells[numrow, 1].Font.Bold = false;
                xlWorkSheet.Cells[numrow, 2] = "'" + NameTemp;
                xlWorkSheet.Cells[numrow, 2].Font.Bold = false;
                xlWorkSheet.Cells[numrow, 10] = "Sub Total";
                xlWorkSheet.Cells[numrow, 11] = tempAmount;
                BankSubTotal.Add(new Tuple<string, string, double>(IDTemp, NameTemp, tempAmount));

                xlWorkSheet.Columns.AutoFit();

                //----------------------------------SHEET 2----------------------------------------------------------

                Worksheet xlWorkSheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
                xlWorkSheet2.Name = "SUMMARY";

                xlWorkSheet2.Cells[1, 1].EntireRow.Font.Bold = true;
                xlWorkSheet2.Cells[1, 1] = "'" + "BANK ID";
                xlWorkSheet2.Cells[1, 2] = "'" + "BANK NAME";
                xlWorkSheet2.Cells[1, 3] = "'" + "TOTAL AMOUNT";

                int numrow2 = 2;
                foreach(Tuple<string, string, double> tuple in BankSubTotal)
                {
                    xlWorkSheet2.Cells[numrow2, 1] = "'" + tuple.Item1;
                    xlWorkSheet2.Cells[numrow2, 2] = "'" + tuple.Item2;
                    xlWorkSheet2.Cells[numrow2, 3] = tuple.Item3;
                    numrow2++;
                }

                xlWorkSheet2.Columns.AutoFit();

                xlWorkBook.SaveAs(filePath);

                xlWorkBook.Close();
                xlApp.Quit();

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
    }
}
