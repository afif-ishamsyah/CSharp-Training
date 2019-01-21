using ACCPAC.Advantage;
using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace API_Payment_Plan
{
    public partial class PaymentForm : Form
    {
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        private Dictionary<String, String> bankIDList = new Dictionary<String, String>();
        private Dictionary<String, String> bankAccountList = new Dictionary<String, String>();
        private Dictionary<String, String> databaseList = new Dictionary<String, String>();

        public PaymentForm()
        {
            InitializeComponent();
            //SendCashBookButton.Enabled = false;
            this.WindowState = FormWindowState.Maximized;
            objectListView.UseFiltering = true;
            objectListView.SelectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.None;
            objectListView.ShowFilterMenuOnRightClick = false;
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            //session.Open("ADMIN", "ADMS4G3COM1", DatabaseComboBox.Text, DateTime.Today, 0);
            session.Open("ADMIN", "ADMS4G3COM1", "CMWTRN", DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            csQry = mDBLinkCmpRW.OpenView("CS0120");

        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            databaseList.Add("CMWIDT - APLIKASI PESAN INDONESIA, PT.", "CMWIDT");
            databaseList.Add("CMWCAD - CMW SOCIAL NETWORKING, LTD.", "CMWCAD");
            databaseList.Add("CMWDAT - CREATIVE MEDIA WORKS PTE, LTD.", "CMWDAT");
            databaseList.Add("ESNAED - ELANG SOCIAL NETWORKING FZ-LLC-AED", "ESNAED");
            DatabaseComboBox.Items.Add("CMWIDT - APLIKASI PESAN INDONESIA, PT.");
            DatabaseComboBox.Items.Add("CMWCAD - CMW SOCIAL NETWORKING, LTD.");
            DatabaseComboBox.Items.Add("CMWDAT - CREATIVE MEDIA WORKS PTE, LTD.");
            DatabaseComboBox.Items.Add("ESNAED - ELANG SOCIAL NETWORKING FZ-LLC-AED");
            DatabaseComboBox.SelectedIndex = 0;

        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            DatabaseGroupBox.Enabled = false;
            ManageGroupBox.Enabled = false;
            CashbookGroupBox.Enabled = false;
            SearchTextBox.Clear();
            bankIDList.Clear();
            BankComboBox.Items.Clear();
            objectListView.ClearObjects();
            Get_Bank();
            InvoiceReport_Load();
            BankComboBox.SelectedIndex = 0;
            GenerateListView(csQry);
            DatabaseGroupBox.Enabled = true;
            ManageGroupBox.Enabled = true;
            CashbookGroupBox.Enabled = true;
        }

        private void Get_Bank()
        {
            try
            {
                Connect();

                string StringSql = @"SELECT BANKCODE, BANKNAME, BANKACCTNO
                                   FROM CBBANK";

                csQry.Browse(StringSql, true);
                csQry.InternalSet(256);

                while (csQry.Fetch(false))
                {
                    bankIDList.Add(csQry.Fields[0].Value.ToString() + " - " + csQry.Fields[1].Value.ToString(), csQry.Fields[0].Value.ToString());
                    bankAccountList.Add(csQry.Fields[0].Value.ToString() + " - " + csQry.Fields[1].Value.ToString(), csQry.Fields[2].Value.ToString());
                    BankComboBox.Items.Add(csQry.Fields[0].Value.ToString() + " - " + csQry.Fields[1].Value.ToString());
                }
            }
            catch (SessionException)
            {
                MessageBox.Show("Please connect to Sage Server first", "Connection Error");
                Application.Exit();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
                Application.Exit();
            }
        }

        private void InvoiceReport_Load()
        {
            try
            {
                Connect();

                string StringSql = @"SELECT a.IDVEND, b.VENDNAME, a.IDINVC, a.DESCINVC, a.DATEINVC, a.DATEINVCDU, a.CODECURN, a.AMTINVCTC, a.AMTINVCHC, a.AUDTORG, d.SUM1, d.SUM2, DATEDIFF(d, CONVERT(varchar, a.DATEINVCDU,  103), GETDATE()) AS DAYSOVER
                                   FROM APOBL a
                                   LEFT OUTER JOIN APVEN b ON a.IDVEND=b.VENDORID
                                   LEFT OUTER JOIN (SELECT IDVEND, IDINVC, SUM(AMTPAYMTC) SUM1, SUM(AMTPAYMHC) SUM2 FROM APOBP GROUP BY IDVEND, IDINVC) d ON a.IDVEND=d.IDVEND AND a.IDINVC=d.IDINVC
                                   ORDER BY a.IDVEND, a.IDINVC, DAYSOVER DESC";


                csQry.Browse(StringSql, true);
                csQry.InternalSet(256);
            }
            catch (SessionException)
            {
                MessageBox.Show("Please connect to Sage Server first", "Connection Error");
                Application.Exit();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
                Application.Exit();
            }
        }
        private void GenerateListView(ACCPAC.Advantage.View csQry)
        {
            while (csQry.Fetch(false))
            {
                double balanceSource = GetBalance(double.Parse(csQry.Fields[7].Value.ToString()), double.Parse(csQry.Fields[10].Value.ToString())); ;
                double balanceFunction = GetBalance(double.Parse(csQry.Fields[8].Value.ToString()), double.Parse(csQry.Fields[11].Value.ToString()));

                DateTime DATEINVC = DateTime.ParseExact(csQry.Fields[4].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                DateTime DATEINVCDU = DateTime.ParseExact(csQry.Fields[5].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                double daysOver = double.Parse(csQry.Fields[12].Value.ToString());

                if (balanceSource != 0 || balanceFunction != 0)
                {

                    InvoiceColumn rowObject = new InvoiceColumn(
                        csQry.Fields[0].Value.ToString(),
                        csQry.Fields[1].Value.ToString(),
                        csQry.Fields[2].Value.ToString(),
                        csQry.Fields[3].Value.ToString(),
                        DATEINVC.ToString("dd/MM/yyyy"),
                        DATEINVCDU.ToString("dd/MM/yyyy"),
                        csQry.Fields[6].Value.ToString(),
                        csQry.Fields[7].Value.ToString(),
                        daysOver.ToString(),
                        balanceSource.ToString(),
                        balanceFunction.ToString());
                    objectListView.AddObject(rowObject);
                }
            }
            objectListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            objectListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            objectListView.ModelFilter = TextMatchFilter.Contains(objectListView, SearchTextBox.Text);
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

        private void PostButton_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();
                ACCPAC.Advantage.View CBBTCH2batch;
                ACCPAC.Advantage.View CBBTCH2header;
                ACCPAC.Advantage.View CBBTCH2detail1;
                ACCPAC.Advantage.View CBBTCH2detail2;
                ACCPAC.Advantage.View CBBTCH2detail3;
                ACCPAC.Advantage.View CBBTCH2detail4;
                ACCPAC.Advantage.View CBBTCH2detail5;
                ACCPAC.Advantage.View CBBTCH2detail6;
                ACCPAC.Advantage.View CBBTCH2detail7;
                ACCPAC.Advantage.View CBBTCH2detail8;

                CBBTCH2batch = mDBLinkCmpRW.OpenView("CB0009");
                CBBTCH2header = mDBLinkCmpRW.OpenView("CB0010");
                CBBTCH2detail1 = mDBLinkCmpRW.OpenView("CB0011");
                CBBTCH2detail2 = mDBLinkCmpRW.OpenView("CB0012");
                CBBTCH2detail3 = mDBLinkCmpRW.OpenView("CB0013");
                CBBTCH2detail4 = mDBLinkCmpRW.OpenView("CB0014");
                CBBTCH2detail5 = mDBLinkCmpRW.OpenView("CB0015");
                CBBTCH2detail6 = mDBLinkCmpRW.OpenView("CB0016");
                CBBTCH2detail7 = mDBLinkCmpRW.OpenView("CB0403");
                CBBTCH2detail8 = mDBLinkCmpRW.OpenView("CB0404");

                CBBTCH2batch.Compose(new[] { CBBTCH2header });
                CBBTCH2header.Compose(new[] { CBBTCH2batch, CBBTCH2detail1, CBBTCH2detail4, CBBTCH2detail8});
                CBBTCH2detail1.Compose(new[] { CBBTCH2header, CBBTCH2detail2, CBBTCH2detail5, CBBTCH2detail7});
                CBBTCH2detail2.Compose(new[] { CBBTCH2detail1, CBBTCH2detail3, CBBTCH2detail6 });
                CBBTCH2detail3.Compose(new[] { CBBTCH2detail2 });
                CBBTCH2detail4.Compose(new[] { CBBTCH2header });
                CBBTCH2detail5.Compose(new[] { CBBTCH2detail1 });
                CBBTCH2detail6.Compose(new[] { CBBTCH2detail2 });
                CBBTCH2detail7.Compose(new[] { CBBTCH2detail1 });
                CBBTCH2detail8.Compose(new[] { CBBTCH2header });

               
                foreach (InvoiceColumn item in objectListView.CheckedObjects)
                {                                                       
                   
                    CBBTCH2batch.RecordCreate(ViewRecordCreate.Insert);

                    CBBTCH2batch.Fields.FieldByName("BANKCODE").SetValue(BankCodeTextBox.Text, false);
                    CBBTCH2batch.Update();

                    //CBBTCH2header.RecordCreate(ViewRecordCreate.DelayKey);
                    CBBTCH2header.Init();
                    CBBTCH2header.Fields.FieldByName("DATE").SetValue(DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"), false);
                    CBBTCH2header.Fields.FieldByName("REFERENCE").SetValue(Strings.Right(bankAccountList[BankComboBox.Text], 2) + DateTime.Today.ToString("yyMM") + "001", false);
                    CBBTCH2header.Fields.FieldByName("MISCCODE").SetValue(item.IDVendor, false);
                    CBBTCH2header.Fields.FieldByName("ENTRYTYPE").SetValue("1", false);

                    
                    CBBTCH2detail1.RecordCreate(ViewRecordCreate.NoInsert);
                    CBBTCH2detail1.Fields.FieldByName("TEXTDESC").SetValue(item.Description, false);
                    CBBTCH2detail1.Fields.FieldByName("SRCECODE").SetValue("CB", false);
                    CBBTCH2detail1.Fields.FieldByName("DTLAMOUNT").SetValue(double.Parse(item.OriginalInvoice), false);
                    CBBTCH2detail1.Insert();

                    CBBTCH2header.Insert();
                    session.Dispose();
                }
                
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                string errors = "";

                if (session.Errors != null)
                {
                    for (int k = 0; k <= session.Errors.Count - 1; k++)
                        errors = errors + session.Errors[k].Message;
                }
                else
                    errors = errors + "Data Error";

                MessageBox.Show(errors, "Error");
                this.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString(), "Error");
                this.Enabled = true;
            }
        }
    

             

        private void CheckAllButton_Click(object sender, EventArgs e)
        {
            objectListView.CheckAll();
        }

        private void UncheckAllButton_Click(object sender, EventArgs e)
        {
            objectListView.UncheckAll();
        }

        private void CheckFilteredButton_Click(object sender, EventArgs e)
        {
            objectListView.CheckObjects(objectListView.FilteredObjects);
        }

        private void UncheckFIlteredButton_Click(object sender, EventArgs e)
        {
            objectListView.UncheckObjects(objectListView.FilteredObjects);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            SearchTextBox.Clear();
        }

        private void BankComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BankCodeTextBox.Text = bankIDList[BankComboBox.Text];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseTextBox.Text = databaseList[DatabaseComboBox.Text];
        }
    }
}
