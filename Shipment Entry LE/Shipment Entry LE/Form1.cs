using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using ACCPAC.Advantage;
using System.Data.OleDb;
using System.Globalization;

namespace Shipment_Entry_LE
{
    public partial class Form1 : Form
    {
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        private string fullfilename;
        private string rawfilename;
        private string username;
        private string password;
        private string database;
        private string fileLocation;
        private string successLocation;
        private string errorLocation;
        private string logErrorLocation;
        private TextFieldParser tfp;

        public Form1()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open(username, password, database, DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);
        }

        private void Form1_Load(object sender, EventArgs e)
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
                    fullfilename = fileLocation + "/" + Path.GetFileName(file); //Get first file in the folder
                    rawfilename = Path.GetFileName(file);

                    SendtoSage();

                    //If Insert to Sage success, move file Completed Folder
                    File.Move(fileLocation + "/" + rawfilename, successLocation + "/" + rawfilename);
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

                tfp = new TextFieldParser(fullfilename);
                tfp.Delimiters = new string[] { "," };
                tfp.TextFieldType = FieldType.Delimited;

                ACCPAC.Advantage.View OESHI1header;
                ACCPAC.Advantage.View OESHI1detail1;
                ACCPAC.Advantage.View OESHI1detail2;
                ACCPAC.Advantage.View OESHI1detail3;
                ACCPAC.Advantage.View OESHI1detail4;
                ACCPAC.Advantage.View OESHI1detail5;
                ACCPAC.Advantage.View OESHI1detail6;
                ACCPAC.Advantage.View OESHI1detail7;
                ACCPAC.Advantage.View OESHI1detail8;
                ACCPAC.Advantage.View OESHI1detail9;
                ACCPAC.Advantage.View OESHI1detail10;
                ACCPAC.Advantage.View OESHI1detail11;
                ACCPAC.Advantage.View OESHI1detail12;

                OESHI1header = mDBLinkCmpRW.OpenView("OE0692");
                OESHI1detail1 = mDBLinkCmpRW.OpenView("OE0691");
                OESHI1detail2 = mDBLinkCmpRW.OpenView("OE0745");
                OESHI1detail3 = mDBLinkCmpRW.OpenView("OE0190");
                OESHI1detail4 = mDBLinkCmpRW.OpenView("OE0694");
                OESHI1detail5 = mDBLinkCmpRW.OpenView("OE0704");
                OESHI1detail6 = mDBLinkCmpRW.OpenView("OE0708");
                OESHI1detail7 = mDBLinkCmpRW.OpenView("OE0709");
                OESHI1detail8 = mDBLinkCmpRW.OpenView("OE0702");
                OESHI1detail9 = mDBLinkCmpRW.OpenView("OE0703");
                OESHI1detail10 = mDBLinkCmpRW.OpenView("OE0706");
                OESHI1detail11 = mDBLinkCmpRW.OpenView("OE0707");
                OESHI1detail12 = mDBLinkCmpRW.OpenView("OE0705");

                OESHI1header.Compose(new[] { OESHI1detail1, null, OESHI1detail3, OESHI1detail2, OESHI1detail4, OESHI1detail5 });
                OESHI1detail1.Compose(new[] { OESHI1header, null, OESHI1detail8, OESHI1detail12, OESHI1detail9, OESHI1detail7, OESHI1detail6 });
                OESHI1detail2.Compose(new[] { OESHI1header });
                OESHI1detail3.Compose(new[] { OESHI1header, OESHI1detail1 });
                OESHI1detail4.Compose(new[] { OESHI1header });
                OESHI1detail5.Compose(new[] { OESHI1header });
                OESHI1detail6.Compose(new[] { OESHI1detail1, null });
                OESHI1detail7.Compose(new[] { OESHI1detail1, null });
                OESHI1detail8.Compose(new[] { OESHI1detail1 });
                OESHI1detail9.Compose(new[] { OESHI1detail1, OESHI1detail10, null, OESHI1detail11 });
                OESHI1detail10.Compose(new[] { OESHI1detail9, null });
                OESHI1detail11.Compose(new[] { OESHI1detail9, null });
                OESHI1detail12.Compose(new[] { OESHI1detail1 });

                // Check if customer already exist
                tfp.ReadLine(); //skip header
                int numrow = 1;

                while (tfp.EndOfData == false)
                {
                    string[] fields = tfp.ReadFields();

                    if (numrow == 1)
                    {
                        OESHI1header.Init();
                        OESHI1header.Fields.FieldByName("SHINUMBER").SetValue(fields[0], false);
                        OESHI1header.Fields.FieldByName("SHIDATE").SetValue(DateTime.ParseExact(fields[1].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture), false);
                        OESHI1header.Fields.FieldByName("CUSTOMER").SetValue(fields[2], false);
                        OESHI1header.Fields.FieldByName("DESC").SetValue(fields[3], false);
                        OESHI1header.Fields.FieldByName("TCLASS1").SetValue("3", false);
                    }
                    OESHI1detail1.RecordCreate(ViewRecordCreate.NoInsert);
                    OESHI1detail1.Fields.FieldByName("ITEM").SetValue(fields[4], false);
                    OESHI1detail1.Fields.FieldByName("CATEGORY").SetValue(fields[4], false);
                    OESHI1detail1.Fields.FieldByName("LOCATION").SetValue(fields[5], false);
                    OESHI1detail1.Fields.FieldByName("PRIUNTPRC").SetValue(fields[6], false);
                    OESHI1detail1.Fields.FieldByName("EXTSHIMISC").SetValue(fields[6], false);
                    OESHI1detail1.Fields.FieldByName("SHIUNIT").SetValue("UOM", false);
                    OESHI1detail1.Fields.FieldByName("QTYSHIPPED").SetValue("1", false);
                    OESHI1detail1.Fields.FieldByName("TCLASS1").SetValue("3", false);
                    OESHI1detail1.Insert();
                    numrow++;
                }
                OESHI1header.Insert();

                session.Dispose();
                tfp.Dispose();
                tfp.Close();
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                tfp.Dispose();
                tfp.Close();
                List<string> errors = new List<string>();
                FileStream files = File.Create(logErrorLocation + "/" + rawfilename + ".txt");
                files.Close();

                for (int k = 0; k <= session.Errors.Count - 1; k++)
                {
                    errors.Add(session.Errors[k].Message);
                }

                string errorMessage = string.Join(" ", errors);


                FileSystem.WriteAllText(logErrorLocation + "/" + rawfilename + ".txt", errorMessage, true);
                File.Move(fileLocation + "/" + rawfilename, errorLocation + "/" + rawfilename);
                session.Errors.Clear();
            }
        }

        private void LoadSaveFile()
        {
            try
            {
                string fileload = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Liputan Enam/OE Shipment/Save/DatabaseSetupShipmentLE.txt";
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

