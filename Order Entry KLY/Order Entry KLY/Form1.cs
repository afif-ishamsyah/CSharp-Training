using ACCPAC.Advantage;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Order_Entry_KLY
{
    public partial class OrderEntryKLY : Form
    {

        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        private string fullFileName;
        private string rawFileName;
        private string fileExtension;
        private FileInfo fileInfo;
        private string username;
        private string password;
        private string database;
        private string fileLocation;
        private string successLocation;
        private string errorLocation;
        private string logErrorLocation;
        private TextFieldParser tfp;

        public OrderEntryKLY()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open(username, password, database, DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            csQry = mDBLinkCmpRW.OpenView("CS0120");
        }

        private void OrderEntryKLY_Load(object sender, EventArgs e)
        {
            LoadSaveFile();

            //Check if  all required folder is exist
            if (Directory.Exists(fileLocation) == false || Directory.Exists(successLocation) == false || Directory.Exists(errorLocation) == false || Directory.Exists(logErrorLocation) == false)
            {
                MessageBox.Show("Please configure Database Setup first, and make sure all required folder is exist", "Folder Not Found");
                Application.Exit();
            }

            string folder = fileLocation;

            try
            {

                foreach (string file in Directory.GetFiles(fileLocation))
                {
                    fullFileName = fileLocation + "/" + Path.GetFileName(file); //Get first file in the folder
                    rawFileName = Path.GetFileName(file);
                    fileExtension = Path.GetExtension(file);
                    fileInfo = new FileInfo(file);

                    //file must be a csv and not hidden
                    if (!fileInfo.Attributes.HasFlag(FileAttributes.Hidden) && fileExtension == ".csv")
                    {
                        SendtoSage();

                        //If Insert to Sage success, move file Completed Folder
                        File.Move(fileLocation + "/" + rawFileName, successLocation + "/" + rawFileName);
                    }
                }
            }
            catch (OleDbException) { Application.Exit(); } //Handle connection error in SendtoSage()
            catch (FileNotFoundException) { Application.Exit(); } //Handle when Folder is empty
            catch (ArgumentNullException) { Application.Exit(); }
            catch (IndexOutOfRangeException) { Application.Exit(); } //Handle when filename is already moved because of an error in Sendtosage() (look at SendtoSage exception)
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Please configure Database Setup first, and make sure all required folder is exist", "Folder Not Found");
            }
            Application.Exit();
        }

        private void SendtoSage()
        {
            try
            {
                Connect();

                tfp = new TextFieldParser(fullFileName);
                tfp.Delimiters = new string[] { "," };
                tfp.TextFieldType = FieldType.Delimited;

                ACCPAC.Advantage.View OEORD1header;
                ACCPAC.Advantage.View OEORD1detail1;
                ACCPAC.Advantage.View OEORD1detail2;
                ACCPAC.Advantage.View OEORD1detail3;
                ACCPAC.Advantage.View OEORD1detail4;
                ACCPAC.Advantage.View OEORD1detail5;
                ACCPAC.Advantage.View OEORD1detail6;
                ACCPAC.Advantage.View OEORD1detail7;
                ACCPAC.Advantage.View OEORD1detail8;
                ACCPAC.Advantage.View OEORD1detail9;
                ACCPAC.Advantage.View OEORD1detail10;
                ACCPAC.Advantage.View OEORD1detail11;
                ACCPAC.Advantage.View OEORD1detail12;


                OEORD1header = mDBLinkCmpRW.OpenView("OE0520");
                OEORD1detail1 = mDBLinkCmpRW.OpenView("OE0500");
                OEORD1detail2 = mDBLinkCmpRW.OpenView("OE0740");
                OEORD1detail3 = mDBLinkCmpRW.OpenView("OE0180");
                OEORD1detail4 = mDBLinkCmpRW.OpenView("OE0526");
                OEORD1detail5 = mDBLinkCmpRW.OpenView("OE0522");
                OEORD1detail6 = mDBLinkCmpRW.OpenView("OE0508");
                OEORD1detail7 = mDBLinkCmpRW.OpenView("OE0507");
                OEORD1detail8 = mDBLinkCmpRW.OpenView("OE0501");
                OEORD1detail9 = mDBLinkCmpRW.OpenView("OE0502");
                OEORD1detail10 = mDBLinkCmpRW.OpenView("OE0504");
                OEORD1detail11 = mDBLinkCmpRW.OpenView("OE0506");
                OEORD1detail12 = mDBLinkCmpRW.OpenView("OE0503");

                OEORD1header.Compose(new[] { OEORD1detail1, null, OEORD1detail3, OEORD1detail2, OEORD1detail4, OEORD1detail5 });
                OEORD1detail1.Compose(new[] { OEORD1header, OEORD1detail8, OEORD1detail12, OEORD1detail9, OEORD1detail6, OEORD1detail7 });
                OEORD1detail2.Compose(new[] { OEORD1header });
                OEORD1detail3.Compose(new[] { OEORD1header, OEORD1detail1 });
                OEORD1detail4.Compose(new[] { OEORD1header });
                OEORD1detail5.Compose(new[] { OEORD1header });
                OEORD1detail6.Compose(new[] { OEORD1detail1 });
                OEORD1detail7.Compose(new[] { OEORD1detail1 });
                OEORD1detail8.Compose(new[] { OEORD1detail1 });
                OEORD1detail9.Compose(new[] { OEORD1detail1, OEORD1detail10, OEORD1detail11 });
                OEORD1detail10.Compose(new[] { OEORD1detail9 });
                OEORD1detail11.Compose(new[] { OEORD1detail9 });
                OEORD1detail12.Compose(new[] { OEORD1detail1 });


                tfp.ReadLine(); //skip header
                int numrow = 1;

                while (tfp.EndOfData == false)
                {
                    string[] fields = tfp.ReadFields();

                    if (numrow == 1)
                    {
                        //string sequenceLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Kapan Lagi/Order Entry/Save/Sequence.txt";
                        string sequenceLocation = "E:/Sage/Interface/KAPAN LAGI DOT COM/Module/Order Entry/Order Entry/Sequence.txt";
                        string[] lines = File.ReadAllLines(sequenceLocation);
                        int sequenceNumber = int.Parse(lines[0]);
                        string orderNumber = "SO" + sequenceNumber.ToString("00000000");

                        sequenceNumber++;
                        File.WriteAllText(sequenceLocation, sequenceNumber.ToString());

                        OEORD1header.Init();
                        OEORD1header.Fields.FieldByName("ORDNUMBER").SetValue(orderNumber, false);
                        OEORD1header.Fields.FieldByName("ORDDATE").SetValue(DateTime.ParseExact(fields[1].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture), false);
                        OEORD1header.Fields.FieldByName("CUSTOMER").SetValue(fields[2], false);
                        OEORD1header.Fields.FieldByName("PONUMBER").SetValue(fields[0], false);
                        OEORD1header.Fields.FieldByName("DESC").SetValue(fields[3], false);
                        OEORD1header.Fields.FieldByName("TCLASS1").SetValue("3", false);
                    }

                    string customerAccountSet = "";
                    string customerID = fields[2].ToString();
                    csQry.Browse("select a.IDACCTSET from ARCUS a where a.IDCUST='" + customerID + "'", true);
                    csQry.InternalSet(256);

                    while (csQry.Fetch(false))
                    {
                        customerAccountSet = csQry.Fields[0].Value.ToString();
                    }

                   

                    double qty = 100;
                    double extendedAmount = double.Parse(fields[6].ToString())/100;
                    double unitAmount = double.Parse(fields[6].ToString())/10000;

                    if (extendedAmount < 0)
                    {
                        extendedAmount = Math.Abs(extendedAmount);
                        unitAmount = Math.Abs(unitAmount);
                        qty = -100;
                    }

                    OEORD1detail1.RecordCreate(ViewRecordCreate.NoInsert);
                    OEORD1detail1.Fields.FieldByName("ITEM").SetValue(fields[4], false);
                    OEORD1detail1.Fields.FieldByName("CATEGORY").SetValue(fields[4], false);
                    OEORD1detail1.Fields.FieldByName("LOCATION").SetValue(fields[5], false);
                    OEORD1detail1.Fields.FieldByName("PRIUNTPRC").SetValue(unitAmount, false);
                    OEORD1detail1.Fields.FieldByName("EXTINVMISC").SetValue(extendedAmount, false);
                    OEORD1detail1.Fields.FieldByName("ORDUNIT").SetValue("UOM", false);
                    OEORD1detail1.Fields.FieldByName("QTYORDERED").SetValue(qty, false);
                    OEORD1detail1.Fields.FieldByName("TCLASS1").SetValue("3", false);

                    if (customerAccountSet.Substring(0,3).Contains("REL") == true)
                    {
                        OEORD1detail8.Fields.FieldByName("OPTFIELD").SetValue("1CUSTNAME", false);
                        OEORD1detail8.Fields.FieldByName("VALIFTEXT").SetValue(GetCustomerName(fields[2]), false);
                        OEORD1detail8.Insert();
                    }                

                    OEORD1detail8.Fields.FieldByName("OPTFIELD").SetValue("2PO", false);
                    OEORD1detail8.Fields.FieldByName("VALIFTEXT").SetValue(fields[0], false);
                    OEORD1detail8.Insert();
                    OEORD1detail1.Insert();
                    numrow++;
                }
                OEORD1header.Insert();

                session.Dispose();
                tfp.Dispose();
                tfp.Close();
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                tfp.Dispose();
                tfp.Close();
                List<string> errors = new List<string>();
                FileStream files = File.Create(logErrorLocation + "/" + rawFileName + ".txt");
                files.Close();

                for (int k = 0; k <= session.Errors.Count - 1; k++)
                {
                    errors.Add(session.Errors[k].Message);
                }

                string errorMessage = string.Join(" ", errors);


                FileSystem.WriteAllText(logErrorLocation + "/" + rawFileName + ".txt", errorMessage, true);
                File.Move(fileLocation + "/" + rawFileName, errorLocation + "/" + rawFileName);
                session.Errors.Clear();
            }
        }

        private string GetCustomerName(string IDCUST)
        {
            string customerName = "";

            ACCPAC.Advantage.View csQryCust = mDBLinkCmpRW.OpenView("CS0120");

            string stringSQL = @"select NAMECUST from ARCUS where IDCUST = '" + IDCUST + "'";

            csQryCust.Browse(stringSQL, true);
            csQryCust.InternalSet(256);

            while (csQryCust.Fetch(false))
            {
                customerName = csQryCust.Fields[0].Value.ToString();
            }

            return customerName;
        }

        private void LoadSaveFile()
        {
            try
            {
                //string fileload = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Kapan Lagi/Order Entry/Save/DatabaseSetupOrderEntryKLY.txt";
                string fileload = "E:/Sage/Interface/KAPAN LAGI DOT COM/Module/Order Entry/Order Entry/DatabaseSetupOrderEntryKLY.txt";
                string[] lines;
                string[] loadedLines = File.ReadAllLines(fileload);

                int index = 0;

                int n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                username = lines[n - 1];

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                password = lines[n - 1];

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                database = lines[n - 1];

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                fileLocation = lines[n - 1];

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                successLocation = lines[n - 1];

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                errorLocation = lines[n - 1];

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                logErrorLocation = lines[n - 1];
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Please configure Database Setup first, and make sure all required folder is exist", "Folder Not Found");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not founds", "Folder Not Found");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Save file from Database Setup is corrupted", "File Error");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Save file from Database Setup is corrupted", "File Error");
            }
        }
    }
}
