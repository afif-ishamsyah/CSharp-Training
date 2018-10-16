using ACCPAC.Advantage;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Customer_Import
{
    public partial class CustomerImport : Form
    {
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        string FileExcel;
        List<string> IDCustomerList = new List<string>();
        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Workbook xlWorkBook;
        Worksheet xlWorkSheet;

        public CustomerImport()
        {
            InitializeComponent();
            DatabaseBox.SelectedIndex = 0;
            FileNameTextbox.ReadOnly = true;
            UploadButton.Enabled = false;
            LoadingLabel.Visible = false;
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open("ADMIN", "ADMS4G3COM1", DatabaseBox.SelectedItem.ToString(), DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (SearchDialog.ShowDialog() == DialogResult.OK)
            {
                FileExcel = SearchDialog.FileName;
                FileNameTextbox.Text = Path.GetFileName(FileExcel);

                if (FileNameTextbox.Text != "")
                {
                    UploadButton.Enabled = true;
                }
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(FileExcel);

            this.Enabled = false;


            if(File.Exists(FileExcel))
            {
                if (extension == ".xlsx" || extension == ".xls")
                {
                    LoadingLabel.Visible = true;
                    xlWorkBook = xlApp.Workbooks.Open(FileExcel);
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    if (xlWorkSheet.Name.ToLower() == "customer creation form v2")
                    {
                        SendtoSage();
                    }
                    else
                    {
                        MessageBox.Show("File yang dipilih salah", "Error");

                        xlWorkBook.Close(0);
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
                string IDCUST = xlWorkSheet.Cells[23, 3].value;

                //CUSTOMER------------------------------------------------------------------
                ACCPAC.Advantage.View ARCUSTOMER1header;
                ACCPAC.Advantage.View ARCUSTOMER1detail;
                ACCPAC.Advantage.View ARCUSTSTAT2;
                ACCPAC.Advantage.View ARCUSTCMT3;

                ARCUSTOMER1header = mDBLinkCmpRW.OpenView("AR0024");
                ARCUSTOMER1detail = mDBLinkCmpRW.OpenView("AR0400");
                ARCUSTSTAT2 = mDBLinkCmpRW.OpenView("AR0022");
                ARCUSTCMT3 = mDBLinkCmpRW.OpenView("AR0021");

                ARCUSTOMER1header.Compose(new[] { ARCUSTOMER1detail });
                ARCUSTOMER1detail.Compose(new[] { ARCUSTOMER1header });

                //Check if customer already exist
                string searchFilter = "IDCUST LIKE %" + IDCUST + "%";

                ARCUSTOMER1header.FilterSelect(searchFilter, true, 0, ViewFilterOrigin.FromStart);
                while (ARCUSTOMER1header.FilterFetch(false))
                {
                    IDCustomerList.Add(ARCUSTOMER1header.Fields.FieldByName("IDCUST").Value.ToString());
                }

                if (IDCustomerList.Contains(IDCUST))
                {
                    MessageBox.Show("Customer sudah ada sebelumnya", "Error");

                    this.Enabled = true;
                    LoadingLabel.Visible = false;

                    xlWorkBook.Close(0);
                    xlApp.Quit();
                }
                else
                {
                    //If data not exist, insert customer data to Sage
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    string tempNameCust = (string)xlWorkSheet.Cells[27, 3].value;
                    string nameCust = textInfo.ToTitleCase(tempNameCust.ToLower());
                    string tempNameCust2 = (string)xlWorkSheet.Cells[29, 3].value;
                    string nameCust2 = (tempNameCust2 == null) ? "" : textInfo.ToTitleCase(tempNameCust2.ToLower());




                    ARCUSTOMER1header.Init();
                    ARCUSTOMER1header.Fields.FieldByName("IDGRP").SetValue(xlWorkSheet.Cells[23, 8].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("IDCUST").SetValue(IDCUST, false);
                    ARCUSTOMER1header.Fields.FieldByName("NAMECUST").SetValue(nameCust, false);
                    ARCUSTOMER1header.Fields.FieldByName("TEXTSTRE1").SetValue(xlWorkSheet.Cells[31, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("TEXTSTRE2").SetValue(xlWorkSheet.Cells[33, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("TEXTSTRE3").SetValue(xlWorkSheet.Cells[35, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("TEXTSTRE4").SetValue(xlWorkSheet.Cells[37, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("CODEPSTL").SetValue(xlWorkSheet.Cells[45, 9].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("CODECTRY").SetValue(xlWorkSheet.Cells[45, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("NAMECITY").SetValue(xlWorkSheet.Cells[43, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("CODESTTE").SetValue(xlWorkSheet.Cells[43, 9].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("TEXTPHON1").SetValue(xlWorkSheet.Cells[39, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("TEXTPHON2").SetValue(xlWorkSheet.Cells[39, 9].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("EMAIL2").SetValue(xlWorkSheet.Cells[41, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("CTACPHONE").SetValue(xlWorkSheet.Cells[77, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("EMAIL1").SetValue(xlWorkSheet.Cells[79, 9].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("NAMECTAC").SetValue(xlWorkSheet.Cells[73, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("IDTAXREGI1").SetValue(xlWorkSheet.Cells[56, 3].value, false);
                    ARCUSTOMER1header.Fields.FieldByName("IDACCTSET").SetValue(xlWorkSheet.Cells[50, 11].value, false);

                    ARCUSTOMER1detail.Fields.FieldByName("OPTFIELD").SetValue("CUSTNAME2", false);
                    ARCUSTOMER1detail.Fields.FieldByName("VALIFTEXT").SetValue(nameCust2, false);
                    ARCUSTOMER1detail.Insert();

                    ARCUSTOMER1header.Insert();

                    //SHIPTOLOACTION------------------------------------------------------------------

                    ACCPAC.Advantage.View ARCUSTSHIP2header;
                    ACCPAC.Advantage.View ARCUSTSHIP2detailFields;

                    ARCUSTSHIP2header = mDBLinkCmpRW.OpenView("AR0023");
                    ARCUSTSHIP2detailFields = mDBLinkCmpRW.OpenView("AR0412");

                    ARCUSTSHIP2header.Compose(new[] { ARCUSTSHIP2detailFields });
                    ARCUSTSHIP2detailFields.Compose(new[] { ARCUSTSHIP2header });

                    ARCUSTSHIP2header.Init();
                    ARCUSTSHIP2header.Fields.FieldByName("IDCUST").SetValue(IDCUST, false);
                    ARCUSTSHIP2header.Fields.FieldByName("IDCUSTSHPT").SetValue("NPWP", false);
                    ARCUSTSHIP2header.Fields.FieldByName("NAMELOCN").SetValue(nameCust, false);
                    ARCUSTSHIP2header.Fields.FieldByName("TEXTSTRE1").SetValue(xlWorkSheet.Cells[58, 3].value, false);
                    ARCUSTSHIP2header.Fields.FieldByName("TEXTSTRE2").SetValue(xlWorkSheet.Cells[60, 3].value, false);
                    ARCUSTSHIP2header.Fields.FieldByName("TEXTSTRE3").SetValue(xlWorkSheet.Cells[62, 3].value, false);
                    ARCUSTSHIP2header.Fields.FieldByName("TEXTSTRE4").SetValue(xlWorkSheet.Cells[64, 3].value, false);
                    ARCUSTSHIP2header.Fields.FieldByName("CODECTRY").SetValue(xlWorkSheet.Cells[45, 3].value, false);
                    ARCUSTSHIP2header.Fields.FieldByName("CODESTTE").SetValue(xlWorkSheet.Cells[66, 9].value, false);
                    ARCUSTSHIP2header.Fields.FieldByName("NAMECITY").SetValue(xlWorkSheet.Cells[66, 3].value, false);
                    ARCUSTSHIP2header.Fields.FieldByName("CODETERR").SetValue("0" + xlWorkSheet.Cells[68, 3].value, false);

                    ARCUSTSHIP2detailFields.Fields.FieldByName("OPTFIELD").SetValue("CUSTNAME2", false);
                    ARCUSTSHIP2detailFields.Fields.FieldByName("VALIFTEXT").SetValue(nameCust2, false);
                    ARCUSTSHIP2detailFields.Insert();

                    ARCUSTSHIP2header.Insert();

                    MessageBox.Show("Customer berhasil ditambah", "Completed");

                    this.Enabled = true;
                    LoadingLabel.Visible = false;

                    xlWorkBook.Close(0);
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

        private void CancelsButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
