using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ACCPAC.Advantage;
using FileHelpers;
using System.Linq;

namespace Payment_Vendor
{
    public partial class PaymentVendorForm : Form
    {
        FileHelperEngine<Data> engine = new FileHelperEngine<Data>();
        List<Data> data = new List<Data>();
        private Dictionary<String, String> vendorlist = new Dictionary<String, String>();
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;



        public PaymentVendorForm()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open("ADMIN", "ADMS4G3COM1", "CMWIDT", DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            csQry = mDBLinkCmpRW.OpenView("CS0120");
          
        }

        private void PaymentVendorForm_Load(object sender, EventArgs e)
        {
            vendorlist.Add("All Vendor", "All Vendor");
            VendorComboBox.Items.Add("All Vendor");
            VendorComboBox.SelectedIndex = 0;
            CsvButton.Enabled = false;
            
            try
            {
                Connect();

                csQry.Browse(@"select a.VENDORID, a.VENDNAME from APVEN a order by a.VENDORID asc", true);
                csQry.InternalSet(256);

                while (csQry.Fetch(false))
                {
                    vendorlist.Add(csQry.Fields[0].Value.ToString(), csQry.Fields[1].Value.ToString());
                    VendorComboBox.Items.Add(csQry.Fields[0].Value.ToString());
                }

                session.Dispose();
            }
            catch (SessionException)
            {
                MessageBox.Show("Please connect to Sage Server first","Connection Error");
                Application.Exit();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
                Application.Exit();
            }

        }

        private void LoadList(String VendorID)
        {
            try
            {
                DisableButton();
                data.Clear();
                Connect();

                String stringSQL = @"select a.BATCHID, b.MISCCODE, b.TEXTDESC, b.DATE, c.VALUE, d.VALUE, e.VALUE, b.BANKAMOUNT, f.VALUE
                            from CBBCTL a
                            join CBBTHD b ON a.BATCHID=b.BATCHID
                            join APVENO c ON b.MISCCODE=c.VENDORID
                            join APVENO d ON b.MISCCODE=d.VENDORID
                            join APVENO e ON b.MISCCODE=e.VENDORID
                            join APVENO f ON b.MISCCODE=f.VENDORID
                            where c.OPTFIELD='BKNAME' and d.OPTFIELD='BKBENE' and e.OPTFIELD='BKACCT' and f.OPTFIELD='BKCODE'";

                if(VendorID!="All Vendor")
                {
                    stringSQL = stringSQL + " and b.MISCCODE = '" + VendorID + "'";
                }

                if (!String.IsNullOrEmpty(FromTextBox.Text))
                {
                    DateTime FromDate = DateTime.ParseExact(FromTextBox.Text, "dd/MM/yyyy", null);
                    stringSQL = stringSQL + " and b.DATE >= '" + FromDate.ToString("yyyyMMdd") + "'";
                }

                if (!String.IsNullOrEmpty(ToTextBox.Text))
                {
                    DateTime ToDate = DateTime.ParseExact(ToTextBox.Text, "dd/MM/yyyy", null);
                    stringSQL = stringSQL + " and b.DATE <= '" + ToDate.ToString("yyyyMMdd") + "'";
                }

                csQry.Browse(stringSQL, true);
                csQry.InternalSet(256);

                GenerateListView(csQry);

                session.Dispose();
                EnableButton();
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

        private void CsvButton_Click(object sender, EventArgs e)
        {
            DisableButton();
            data.Clear();
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Payment Vendor");
            String csvLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Payment Vendor/Payment " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".csv";

            foreach (ListViewItem item in BatchView.Items)
            {
                if(item.Checked == true)
                {
                    data.Add(new Data()
                    {
                        BKNAME = item.SubItems[4].Text,
                        BANKADDRESS = "",
                        BANKCITY = "",
                        BANKCODE = item.SubItems[10].Text,
                        BKBENE = item.SubItems[5].Text,
                        BKACCT = item.SubItems[6].Text,
                        CURRENCY = "IDR",
                        AMOUNT = item.SubItems[7].Text.ToString().Replace('-', ' '),
                        DESCRIPTION = item.SubItems[2].Text.ToString().Replace(',',' '),
                        DESCRIPTION2 = "",
                        EMAIL = "",
                        TRANSACTIONTYPE = GetTransactionType(item.SubItems[4].Text),
                        RESIDENTSTATUS = "0",
                        CITIZENSTATUS = "0"
                    });

                }
            }

            engine.WriteFile(csvLocation, data);
            MessageBox.Show("CSV Generated", "Success");
            EnableButton();
        }

        private void GenerateListView(ACCPAC.Advantage.View csQry)
        {
            BatchView.Clear();
            BatchView.View = System.Windows.Forms.View.Details;
            BatchView.CheckBoxes = true;
            BatchView.Columns.Add("BATCH ID", 75, HorizontalAlignment.Center);
            BatchView.Columns.Add("VENDOR ID", 75, HorizontalAlignment.Center);
            BatchView.Columns.Add("DESCRIPTION", 250, HorizontalAlignment.Center);
            BatchView.Columns.Add("DATE", 100, HorizontalAlignment.Center);         
            BatchView.Columns.Add("BANK NAME", 125, HorizontalAlignment.Center);
            BatchView.Columns.Add("BENEFICIARY NAME", 150, HorizontalAlignment.Center);
            BatchView.Columns.Add("ACCOUNT NUMBER", 110, HorizontalAlignment.Center);
            BatchView.Columns.Add("AMOUNT", 100, HorizontalAlignment.Center);
            BatchView.Columns.Add("CURRENCY", 75, HorizontalAlignment.Center);
            BatchView.Columns.Add("TYPE", 75, HorizontalAlignment.Center);
            BatchView.Columns.Add("BANK CODE", 75, HorizontalAlignment.Center);


            while (csQry.Fetch(false))
            {
                var row = new ListViewItem(new string[] {
                    csQry.Fields[0].Value.ToString(),
                    csQry.Fields[1].Value.ToString(),
                    csQry.Fields[2].Value.ToString(),
                    DateTime.ParseExact(csQry.Fields[3].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                    csQry.Fields[4].Value.ToString(),
                    csQry.Fields[5].Value.ToString(),
                    csQry.Fields[6].Value.ToString(),
                    csQry.Fields[7].Value.ToString().Trim(new Char[] {'-'}),
                    "IDR",
                    GetTransactionType(csQry.Fields[4].Value.ToString()),
                    csQry.Fields[8].Value.ToString()});
                BatchView.Items.Add(row);
            }
        }

        private void VendorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VendorNameTextBox.Text = vendorlist[VendorComboBox.Text];
        }

        private void FromCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            FromTextBox.Text = FromCalendar.SelectionStart.ToShortDateString();
        }

        private void ToCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            ToTextBox.Text = ToCalendar.SelectionStart.ToShortDateString();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            LoadList(VendorComboBox.SelectedItem.ToString());
        }

        private void DisableButton()
        {
            FromCalendar.Enabled = false;
            ToCalendar.Enabled = false;
            SearchButton.Enabled = false;
            CsvButton.Enabled = false;
        }

        private void EnableButton()
        {
            FromCalendar.Enabled = true;
            ToCalendar.Enabled = true;
            SearchButton.Enabled = true;
            CsvButton.Enabled = true;
        }

        private void FromClearButton_Click(object sender, EventArgs e)
        {
            FromTextBox.Clear();
        }

        private void ToClearButton_Click(object sender, EventArgs e)
        {
            ToTextBox.Clear();
        }

        private String GetTransactionType(string bank)
        {
            if (bank.IndexOf("permata", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "OVB";
            }
            else
            {
                return "LLG";
            }
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in BatchView.Items)
            {
                item.Checked = true;
            }
        }

        private void UnselectAllButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in BatchView.Items)
            {
                item.Checked = false;
            }
        }
    }
}
