using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACCPAC.Advantage;
using Microsoft.Office.Interop.Excel;

namespace Vendor_Export_and_Import
{
    public partial class VendorForm : Form
    {
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        string FileExcel;
        string VendorIDCheck;
        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Workbook xlWorkBook;
        Worksheet xlWorkSheet;

        public VendorForm()
        {
            InitializeComponent();
            DatabaseBox.SelectedIndex = 0;
        }

        private void VendorForm_Load(object sender, EventArgs e)
        {
            FileNameTextBox.ReadOnly = true;
            UploadButton.Enabled = false;
            CheckExistButton.Enabled = false;
            FirstCharacterTextBox.CharacterCasing = CharacterCasing.Upper;
            SearchNameTextBox.CharacterCasing = CharacterCasing.Upper;
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open("ADM", "ADM123456", DatabaseBox.SelectedItem.ToString(), DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            csQry = mDBLinkCmpRW.OpenView("CS0120");
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (SearchDialog.ShowDialog() == DialogResult.OK)
            {
                FileExcel = SearchDialog.FileName;
                FileNameTextBox.Text = Path.GetFileName(FileExcel);

                if (FileNameTextBox.Text != "")
                {
                    UploadButton.Enabled = true;
                    CheckExistButton.Enabled = true;
                }

            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(FileExcel);
            this.Enabled = false;

            if (File.Exists(FileExcel))
            {
                if (extension == ".xlsx" || extension == ".xls")
                {
                    xlWorkBook = xlApp.Workbooks.Open(FileExcel);
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    if (xlWorkSheet.Name.ToLower() == "vendor details")
                    {
                        SendtoSage();
                        this.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("File yang dipilih salah", "Error");

                        xlWorkBook.Close();
                        xlApp.Quit();
                        this.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("File harus bereskstensi Microsoft Excel (.xlsx or .xls)", "Wrong File Type");
                    this.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("File tidak ditemukan. Silahkan lakukan kembali pencarian File", "Error");
                this.Enabled = true;
            }
        }

        private void CheckExistButton_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(FileExcel);
            this.Enabled = false;

            if (File.Exists(FileExcel))
            {
                if (extension == ".xlsx" || extension == ".xls")
                {
                    xlWorkBook = xlApp.Workbooks.Open(FileExcel);
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    if (xlWorkSheet.Name.ToLower() == "vendor details")
                    {
                        CheckIDExist();
                        this.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("File yang dipilih salah", "Error");

                        xlWorkBook.Close();
                        xlApp.Quit();
                        this.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("File harus bereskstensi Microsoft Excel (.xlsx or .xls)", "Wrong File Type");
                    this.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("File tidak ditemukan. Silahkan lakukan kembali pencarian File", "Error");
                this.Enabled = true;
            }
        }


        private void SendtoSage()
        {
            try
            {
                Connect();

                string VENDORID = xlWorkSheet.Cells[1, 2].value;
                string TaxClass;
                string TaxStatus;

                if (xlWorkSheet.Cells[28, 2].value == "PKP")
                {
                    TaxClass = "2";
                    TaxStatus = "1";
                }
                else
                {
                    TaxClass = "1";
                    TaxStatus = "0";
                }

                ACCPAC.Advantage.View APVENDOR1header;
                ACCPAC.Advantage.View APVENDOR1detail;
                ACCPAC.Advantage.View APVENDSTAT2;
                ACCPAC.Advantage.View APVENDCMNT3;

                APVENDOR1header = mDBLinkCmpRW.OpenView("AP0015");
                APVENDOR1detail = mDBLinkCmpRW.OpenView("AP0407");
                APVENDSTAT2 = mDBLinkCmpRW.OpenView("AP0019");
                APVENDCMNT3 = mDBLinkCmpRW.OpenView("AP0014");

                APVENDOR1header.Compose(new[] { APVENDOR1detail });
                APVENDOR1detail.Compose(new[] { APVENDOR1header });

                // Check if customer already exist
                csQry.Browse("select a.VENDORID from APVEN a where a.VENDORID='" + VENDORID + "'", true);
                csQry.InternalSet(256);

                while (csQry.Fetch(false))
                {
                    VendorIDCheck = csQry.Fields[0].Value.ToString();
                }

                if (string.Compare(VENDORID, VendorIDCheck) == 0)
                {
                    MessageBox.Show("Vendor sudah ada sebelumnya", "Error");

                    xlWorkBook.Close();
                    xlApp.Quit();
                }
                else
                {
                    // If data not exist, insert customer data to Sage

                    APVENDOR1header.Init();
                    APVENDOR1header.Fields.FieldByName("IDGRP").SetValue(xlWorkSheet.Cells[23, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("VENDORID").SetValue(VENDORID, false);
                    APVENDOR1header.Fields.FieldByName("VENDNAME").SetValue(xlWorkSheet.Cells[2, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("LEGALNAME").SetValue(xlWorkSheet.Cells[2, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TEXTSTRE1").SetValue(xlWorkSheet.Cells[3, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TEXTSTRE2").SetValue(xlWorkSheet.Cells[4, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TEXTSTRE3").SetValue(xlWorkSheet.Cells[5, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TEXTSTRE4").SetValue(xlWorkSheet.Cells[6, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("CODEPSTL").SetValue(xlWorkSheet.Cells[10, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("CODECTRY").SetValue(xlWorkSheet.Cells[9, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("NAMECITY").SetValue(xlWorkSheet.Cells[7, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("CODESTTE").SetValue(xlWorkSheet.Cells[8, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TEXTPHON1").SetValue(xlWorkSheet.Cells[11, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TEXTPHON2").SetValue(xlWorkSheet.Cells[12, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("EMAIL2").SetValue(xlWorkSheet.Cells[13, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("CTACPHONE").SetValue(xlWorkSheet.Cells[16, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("EMAIL1").SetValue(xlWorkSheet.Cells[15, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("NAMECTAC").SetValue(xlWorkSheet.Cells[14, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("IDTAXREGI1").SetValue(xlWorkSheet.Cells[19, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("IDACCTSET").SetValue(xlWorkSheet.Cells[25, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TERMSCODE").SetValue(xlWorkSheet.Cells[24, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("SHORTNAME").SetValue(xlWorkSheet.Cells[26, 2].value, false);
                    APVENDOR1header.Fields.FieldByName("TAXCLASS1").SetValue(TaxClass, false);

                    APVENDOR1detail.Fields.FieldByName("OPTFIELD").SetValue("BKACCT", false);
                    APVENDOR1detail.Fields.FieldByName("VALIFTEXT").SetValue(xlWorkSheet.Cells[21, 2].value, false);
                    APVENDOR1detail.Insert();
                    APVENDOR1detail.Fields.FieldByName("OPTFIELD").SetValue("BKBENE", false);
                    APVENDOR1detail.Fields.FieldByName("VALIFTEXT").SetValue(xlWorkSheet.Cells[22, 2].value, false);
                    APVENDOR1detail.Insert();
                    APVENDOR1detail.Fields.FieldByName("OPTFIELD").SetValue("BKNAME", false);
                    APVENDOR1detail.Fields.FieldByName("VALIFTEXT").SetValue(xlWorkSheet.Cells[20, 2].value, false);
                    APVENDOR1detail.Insert();
                    APVENDOR1detail.Fields.FieldByName("OPTFIELD").SetValue("BKCODE", false);
                    APVENDOR1detail.Fields.FieldByName("VALIFTEXT").SetValue(xlWorkSheet.Cells[30, 2].value, false);
                    APVENDOR1detail.Insert();
                    if (DatabaseBox.SelectedItem.ToString().Contains("CMWIDT"))
                    {
                        APVENDOR1detail.Fields.FieldByName("OPTFIELD").SetValue("FINANCENAME", false);
                        APVENDOR1detail.Fields.FieldByName("VALIFTEXT").SetValue(xlWorkSheet.Cells[27, 2].value, false);
                        APVENDOR1detail.Insert();
                        APVENDOR1detail.Fields.FieldByName("OPTFIELD").SetValue("FINANCEPHONE", false);
                        APVENDOR1detail.Fields.FieldByName("VALIFTEXT").SetValue(xlWorkSheet.Cells[29, 2].value, false);
                        APVENDOR1detail.Insert();
                        APVENDOR1detail.Fields.FieldByName("OPTFIELD").SetValue("PKP", false);
                        APVENDOR1detail.Fields.FieldByName("VALIFBOOL").SetValue(TaxStatus, false);
                        APVENDOR1detail.Insert();
                    }

                    APVENDOR1header.Insert();

                    MessageBox.Show("Vendor berhasil ditambah", "Completed");

                    session.Dispose();
                    xlWorkBook.Close();
                    xlApp.Quit();
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
                    errors = errors + "File yang dipilih salah";

                MessageBox.Show(errors, "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }

            catch (Exception e)
            {
                MessageBox.Show("Error" + e.ToString(), "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }
        }

        private void CheckIDExist()
        {
            try
            {
                Connect();

                string VENDORID = xlWorkSheet.Cells[1, 2].value;

                // Check if customer already exist
                csQry.Browse("select a.VENDORID from APVEN a where a.VENDORID='" + VENDORID + "'", true);
                csQry.InternalSet(256);

                while (csQry.Fetch(false))
                {
                    VendorIDCheck = csQry.Fields[0].Value.ToString();
                }

                if (string.Compare(VENDORID, VendorIDCheck) == 0)
                {
                    MessageBox.Show("ID Vendor ini sudah digunakan di database. Mohon cek dan ganti ID", "Error");

                    xlWorkBook.Close();
                    xlApp.Quit();
                }
                else
                {
                    MessageBox.Show("ID Vendor belum ada di database. Aman untuk digunakan", "Completed");

                    session.Dispose();
                    xlWorkBook.Close();
                    xlApp.Quit();
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
                    errors = errors + "File yang dipilih salah";

                MessageBox.Show(errors, "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }

            catch (Exception e)
            {
                MessageBox.Show("Error" + e.ToString(), "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }
        }

        private void SearchNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SearchNameTextBox.Text != "")
                {
                    this.Enabled = false;
                    VendorNameListView.Clear();
                    VendorNameListView.View = System.Windows.Forms.View.Details;
                    VendorNameListView.Columns.Add("VendorName", 200, HorizontalAlignment.Center);

                    Connect();

                    csQry.Browse("select a.VENDNAME from APVEN a where upper(a.VENDNAME) like '%" + SearchNameTextBox.Text + "%'", true);
                    csQry.InternalSet(256);

                    while (csQry.Fetch(false))
                    {
                        ListViewItem row = new ListViewItem(new string[] { csQry.Fields[0].Value.ToString() });
                        VendorNameListView.Items.Add(row);
                    }

                    session.Dispose();
                    this.Enabled = true;
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
                    errors = errors + "File yang dipilih salah";

                MessageBox.Show(errors, "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString(), "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }
        }

        private void SearchIDButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstCharacterTextBox.Text != "")
                {
                    this.Enabled = false;
                    ResultComboBox.Items.Clear();

                    Connect();

                    csQry.Browse("select a.VENDORID from APVEN a where a.VENDORID like '" + FirstCharacterTextBox.Text + "%' order by a.VENDORID asc", true);
                    csQry.InternalSet(256);

                    while (csQry.Fetch(false))
                    {
                        ResultComboBox.Items.Add(csQry.Fields[0].Value.ToString());
                    }

                    ResultComboBox.SelectedIndex = 0;

                    session.Dispose();
                    this.Enabled = true;
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
                    errors = errors + "File yang dipilih salah";

                MessageBox.Show(errors, "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString(), "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                this.Enabled = true;
            }
        }

        private void CancelButtons_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                Connect();

                string StringSql = @"SELECT a.VENDORID, a.VENDNAME, 
                                   a.TEXTSTRE1, a.TEXTSTRE2, a.TEXTSTRE3, a.TEXTSTRE4,
                                   a.CODEPSTL, a.NAMECITY, a.CODESTTE, a.CODECTRY, a.TEXTPHON1, a.TEXTPHON2, a.EMAIL2,
                                   a.NAMECTAC, a.CTACPHONE, a.EMAIL1, a.IDTAXREGI1, b.VALUE, c.VALUE, d.VALUE, a.CURNCODE, a.TERMSCODE
                                   FROM APVEN a
                                   JOIN APVENO b ON a.VENDORID=b.VENDORID
                                   JOIN APVENO c ON a.VENDORID=c.VENDORID
                                   JOIN APVENO d ON a.VENDORID=d.VENDORID
                                   WHERE b.OPTFIELD='BKNAME' AND c.OPTFIELD='BKBENE' AND d.OPTFIELD='BKACCT'
                                   ORDER BY a.VENDORID ASC";


                csQry.Browse(StringSql, true);
                csQry.InternalSet(256);

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Sage Data Export/Vendor");
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Sage Data Export/Vendor/Data Vendor " + DatabaseBox.SelectedItem.ToString() + " " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".xlsx";
                Microsoft.Office.Interop.Excel.Application ExportApp = new Microsoft.Office.Interop.Excel.Application();

                object misValue = System.Reflection.Missing.Value;
                Workbook ExportWorkBook = ExportApp.Workbooks.Add(misValue);

                //-----------------------------SHEET 1---------------------------------------------------------------------
                Worksheet ExportWorkSheet = (Worksheet)ExportWorkBook.Worksheets.get_Item(1);
                ExportWorkSheet.Name = "VENDOR";

                ExportWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;
                ExportWorkSheet.Cells[1, 1] = "'" + "ID VENDOR";
                ExportWorkSheet.Cells[1, 2] = "'" + "VENDOR NAME";
                ExportWorkSheet.Cells[1, 3] = "'" + "ADDRESS";
                ExportWorkSheet.Cells[1, 4] = "'" + "POSTAL CODE";
                ExportWorkSheet.Cells[1, 5] = "'" + "CITY";
                ExportWorkSheet.Cells[1, 6] = "'" + "STATE";
                ExportWorkSheet.Cells[1, 7] = "'" + "COUNTRY";
                ExportWorkSheet.Cells[1, 8] = "'" + "PHONE 1";
                ExportWorkSheet.Cells[1, 9] = "'" + "PHONE 2";
                ExportWorkSheet.Cells[1, 10] = "'" + "EMAIL";
                ExportWorkSheet.Cells[1, 11] = "'" + "CONTACT NAME";
                ExportWorkSheet.Cells[1, 12] = "'" + "CONTACT PHONE";
                ExportWorkSheet.Cells[1, 13] = "'" + "CONTACT EMAIL";
                ExportWorkSheet.Cells[1, 14] = "'" + "NPWP";
                ExportWorkSheet.Cells[1, 15] = "'" + "BANK NAME";
                ExportWorkSheet.Cells[1, 16] = "'" + "BENEFICIARY NAME";
                ExportWorkSheet.Cells[1, 17] = "'" + "ACCOUNT NUMBER";
                ExportWorkSheet.Cells[1, 18] = "'" + "CURRENCY";
                ExportWorkSheet.Cells[1, 19] = "'" + "TERMS CODE";

                int numrow = 2;
                while (csQry.Fetch(false))
                {

                    ExportWorkSheet.Cells[numrow, 1] = "'" + csQry.Fields[0].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 2] = "'" + csQry.Fields[1].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 3] = "'" + (csQry.Fields[2].Value.ToString() + " " +
                                                         csQry.Fields[3].Value.ToString() + " " +
                                                         csQry.Fields[4].Value.ToString() + " " +
                                                         csQry.Fields[5].Value.ToString()).Trim();
                    ExportWorkSheet.Cells[numrow, 4] = "'" + csQry.Fields[6].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 5] = "'" + csQry.Fields[7].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 6] = "'" + csQry.Fields[8].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 7] = "'" + csQry.Fields[9].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 8] = "'" + csQry.Fields[10].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 9] = "'" + csQry.Fields[11].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 10] = "'" + csQry.Fields[12].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 11] = "'" + csQry.Fields[13].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 12] = "'" + csQry.Fields[14].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 13] = "'" + csQry.Fields[15].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 14] = "'" + csQry.Fields[16].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 15] = "'" + csQry.Fields[17].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 16] = "'" + csQry.Fields[18].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 17] = "'" + csQry.Fields[19].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 18] = "'" + csQry.Fields[20].Value.ToString();
                    ExportWorkSheet.Cells[numrow, 19] = "'" + csQry.Fields[21].Value.ToString();
                    numrow++;
                }

                ExportWorkSheet.Columns.AutoFit();
                ExportWorkBook.SaveAs(filePath);

                ExportWorkBook.Close();
                ExportApp.Quit();

                session.Dispose();
                MessageBox.Show("Data Exported", "Success");
                this.Enabled = true;
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
