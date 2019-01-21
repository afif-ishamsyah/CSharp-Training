using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ACCPAC.Advantage;
using FileHelpers;
using System.Diagnostics;
using System.Threading;
using BrightIdeasSoftware;

namespace Payment_Vendor
{
    public partial class PaymentVendorForm : Form
    {
        FileHelperEngine<Data> engine = new FileHelperEngine<Data>();
        List<Data> data = new List<Data>();
        private Dictionary<string, string> vendorlist = new Dictionary<string, string>();
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;

        public PaymentVendorForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            BatchView.CellEditActivation = ObjectListView.CellEditActivateMode.SingleClick;
            BatchView.UseFiltering = true;
            BatchView.SelectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.None;
            BatchView.ShowFilterMenuOnRightClick = false;
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
                    vendorlist.Add(csQry.Fields[0].Value.ToString() + " - " + csQry.Fields[1].Value.ToString(), csQry.Fields[0].Value.ToString());
                    VendorComboBox.Items.Add(csQry.Fields[0].Value.ToString() + " - " + csQry.Fields[1].Value.ToString());
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

        private void LoadList(string VendorID)
        {
            try
            {
                DisableButton();
                data.Clear();
                Connect();

                string stringSQL = @"SELECT a.BATCHID, b.MISCCODE, b.TEXTDESC, b.DATE, c.VALUE, d.VALUE, e.VALUE, b.BANKAMOUNT, f.VALUE, g.VENDNAME
                            FROM CBBCTL a
                            JOIN CBBTHD b ON a.BATCHID=b.BATCHID
                            LEFT OUTER JOIN APVENO c ON b.MISCCODE=c.VENDORID
                            LEFT OUTER JOIN APVENO d ON b.MISCCODE=d.VENDORID
                            LEFT OUTER JOIN APVENO e ON b.MISCCODE=e.VENDORID
                            LEFT OUTER JOIN APVENO f ON b.MISCCODE=f.VENDORID
                            JOIN APVEN g ON b.MISCCODE=g.VENDORID
                            WHERE a.STATUS='0' AND c.OPTFIELD='BKNAME' AND d.OPTFIELD='BKBENE' AND e.OPTFIELD='BKACCT' AND f.OPTFIELD='BKCODE'";

                
                if (VendorID != "All Vendor")
                {
                    stringSQL = stringSQL + " AND b.MISCCODE = '" + VendorID + "'";
                }

                if (!string.IsNullOrEmpty(FromTextBox.Text))
                {
                    DateTime FromDate = DateTime.ParseExact(FromTextBox.Text, "dd/MM/yyyy", null);
                    stringSQL = stringSQL + " AND b.DATE >= '" + FromDate.ToString("yyyyMMdd") + "'";
                }

                if (!string.IsNullOrEmpty(ToTextBox.Text))
                {
                    DateTime ToDate = DateTime.ParseExact(ToTextBox.Text, "dd/MM/yyyy", null);
                    stringSQL = stringSQL + " AND b.DATE <= '" + ToDate.ToString("yyyyMMdd") + "'";
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
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Payment Vendor CSV Files");
            string csvLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Payment Vendor CSV Files/Payment " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".csv";

            if (BatchView.CheckedObjects.Count == 0)
            {
                MessageBox.Show("Please check at least one item", "Alert");
                EnableButton();
            }
            else
            {
                foreach (PaymentColumn item in BatchView.CheckedObjects)
                {
                    string desc = item.DESCRIPTION.ToString().Replace(',', ' ').Replace('.', ' ');
                    if(desc.Length>35)
                    {
                        desc = desc.Substring(0,35);
                    }

                    data.Add(new Data()
                    { 
                        BKNAME = item.BANKNAME,
                        BANKADDRESS = "",
                        BANKCITY = "",
                        BANKCODE = item.BANKCODE,
                        BKBENE = item.BENEFICIARYNAME,
                        BKACCT = item.ACCOUNTNUMBER,
                        CURRENCY = "IDR",
                        AMOUNT = item.AMOUNT.ToString().Replace('-', ' '),
                        DESCRIPTION = desc,
                        DESCRIPTION2 = "",
                        EMAIL = "",
                        TRANSACTIONTYPE = GetTransactionType(item.BANKNAME),
                        RESIDENTSTATUS = "0",
                        CITIZENSTATUS = "0"
                    });

                }

                engine.WriteFile(csvLocation, data);
                MessageBox.Show("CSV Generated", "Success");
                EnableButton();
                string path = csvLocation.Replace("/", "\\");
                FileInfo fileInfo = new FileInfo(path);

                //check if directory exists so that 'fileInfo.Directory' doesn't throw directory not found

                ProcessStartInfo pi = new ProcessStartInfo("explorer.exe")
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = true,
                    FileName = fileInfo.Directory.FullName,
                    Verb = "open"
                };

                Process.Start(pi);
                Thread.Sleep(500);
                SendKeys.SendWait(fileInfo.Name);
            }
        }

        private void GenerateListView(ACCPAC.Advantage.View csQry)
        {
            BatchView.ClearObjects();
            int rowNumber = 1;
            while (csQry.Fetch(false))
            {
                string description = csQry.Fields[2].Value.ToString().Substring(csQry.Fields[2].Value.ToString().LastIndexOf(';') + 1);
                PaymentColumn rowObject = new PaymentColumn(
                    rowNumber.ToString(),
                    csQry.Fields[0].Value.ToString(),
                    csQry.Fields[1].Value.ToString(),
                    csQry.Fields[9].Value.ToString(),
                    description,
                    DateTime.ParseExact(csQry.Fields[3].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                    csQry.Fields[4].Value.ToString(),
                    csQry.Fields[5].Value.ToString(),
                    csQry.Fields[6].Value.ToString(),
                    csQry.Fields[7].Value.ToString().Trim(new Char[] {'-'}),
                    "IDR",
                    GetTransactionType(csQry.Fields[4].Value.ToString()),
                    csQry.Fields[8].Value.ToString());
                BatchView.AddObject(rowObject);
                rowNumber++;
            }
            BatchView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            BatchView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void HandleCellEditFinished(object sender, CellEditEventArgs e)
        { 
            Enabled = false;
            if(e.Column == BANKNAME)
            {
                foreach(PaymentColumn row in BatchView.Objects)
                {
                    row.TYPE = GetTransactionType(row.BANKNAME);
                }
            }
            Enabled = true;
        }

        private void VendorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VendorIDTextBox.Text = vendorlist[VendorComboBox.Text];
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
            LoadList(VendorIDTextBox.Text);
        }

        private void DisableButton()
        {
            this.Enabled = false;
        }

        private void EnableButton()
        {
            this.Enabled = true;
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

        private string GetTransactionType(string bank)
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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CheckAllButton_Click(object sender, EventArgs e)
        {
            BatchView.CheckAll();
        }

        private void UncheckAllButton_Click(object sender, EventArgs e)
        {
            BatchView.UncheckAll();
        }

        private void CheckFilteredButton_Click(object sender, EventArgs e)
        {
            BatchView.CheckObjects(BatchView.FilteredObjects);
        }

        private void UncheckFIlteredButton_Click(object sender, EventArgs e)
        {
            BatchView.UncheckObjects(BatchView.FilteredObjects);
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            BatchView.ModelFilter = TextMatchFilter.Contains(BatchView, SearchTextBox.Text);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            SearchTextBox.Clear();
        }

        private void BatchView_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(BatchView.FocusedItem.Index.ToString());
            //int i = BatchView.FocusedItem.Index;
            //MessageBox.Show(BatchView.FocusedItem.SubItems[7].Text);
        }
    }
}
