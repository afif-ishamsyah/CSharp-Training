using ACCPAC.Advantage;
using System;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Globalization;

namespace AssetManagement
{
    public partial class Form1 : Form
    {
        private Session session;
        private DBLink mDBLinkCmpRW;
        private string username;
        private string password;
        private string database;
        private string fullFileName;
        private string rawFileName;
        private string fileLocation;
        private string fileExtension;
        private FileInfo fileInfo;
        private string successLocation;
        private string errorLocation;
        private string logErrorLocation;
        TextFieldParser tfp;

        public Form1()
        {
            InitializeComponent();
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

            try {

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

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open(username, password, database, DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);
        }

        private void SendtoSage()
        {
            try
            {
                Connect();

                tfp = new TextFieldParser(fullFileName);
                tfp.Delimiters = new string[] { "," };
                tfp.TextFieldType = FieldType.Delimited;

                ACCPAC.Advantage.View AMACQASST1batch;
                ACCPAC.Advantage.View AMACQASST1header;
                ACCPAC.Advantage.View AMACQASST1detail1;
                ACCPAC.Advantage.View AMACQASST1detail2;
                ACCPAC.Advantage.View AMACQASST1detail3;

                AMACQASST1batch = mDBLinkCmpRW.OpenView("AM0352");
                AMACQASST1header = mDBLinkCmpRW.OpenView("AM0353");
                AMACQASST1detail1 = mDBLinkCmpRW.OpenView("AM0354");
                AMACQASST1detail2 = mDBLinkCmpRW.OpenView("AM0356");
                AMACQASST1detail3 = mDBLinkCmpRW.OpenView("AM0355");

                AMACQASST1batch.Compose(new[] { AMACQASST1header });
                AMACQASST1header.Compose(new[] { AMACQASST1batch, AMACQASST1detail1, null, null, null });
                AMACQASST1detail1.Compose(new[] { AMACQASST1header, AMACQASST1detail2, AMACQASST1detail3, null, null, null, null });
                AMACQASST1detail2.Compose(new[] { AMACQASST1detail1 });
                AMACQASST1detail3.Compose(new[] { AMACQASST1detail1 });

                // Check if customer already exist
                tfp.ReadLine(); //skip header
                int numrow = 1;

                while (tfp.EndOfData == false)
                {
                    string[] fields = tfp.ReadFields();

                    if (numrow == 1)
                    {
                        AMACQASST1batch.Init();
                        
                        AMACQASST1batch.Fields.FieldByName("BATDESC").SetValue(fields[0], false);
                        AMACQASST1batch.Fields.FieldByName("TXTYPE").SetValue(fields[1], false);                      
                        AMACQASST1batch.Update();
                  
                        AMACQASST1header.Init();
                        AMACQASST1header.Fields.FieldByName("ACQENTRY").SetValue("1", false);
                        AMACQASST1header.Fields.FieldByName("FISCY").SetValue(fields[6], false);
                        AMACQASST1header.Fields.FieldByName("TRANSDATE").SetValue(DateTime.ParseExact(fields[5].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture), false);
                        AMACQASST1header.Fields.FieldByName("FISCPERD").SetValue(fields[7], false);
                        AMACQASST1header.Fields.FieldByName("VENDOR").SetValue(fields[11], false);
                        AMACQASST1header.Fields.FieldByName("RATETYPE").SetValue("TX", false);
                        AMACQASST1header.Fields.FieldByName("AQUCODE").SetValue(fields[8], false);
                        AMACQASST1header.Fields.FieldByName("APINVCNO").SetValue(fields[13], false);
                        AMACQASST1header.Fields.FieldByName("ACCTID").SetValue(fields[14], false);
                        AMACQASST1header.Fields.FieldByName("PONO").SetValue(fields[12], false);
                        AMACQASST1header.Fields.FieldByName("ENTRYDESC").SetValue(fields[3], false);
                        AMACQASST1header.Fields.FieldByName("DOCNO").SetValue(fields[10], false);
                    }
                    AMACQASST1detail1.RecordCreate(ViewRecordCreate.NoInsert);
                    AMACQASST1detail1.Fields.FieldByName("ASSETNO").SetValue(fields[15], false);
                    AMACQASST1detail1.Fields.FieldByName("ASSETDESC").SetValue(fields[16], false);
                    AMACQASST1detail1.Fields.FieldByName("BKVALUE").SetValue(fields[28], false);
                    AMACQASST1detail1.Fields.FieldByName("BKLTPERD").SetValue(fields[27], false);
                    AMACQASST1detail1.Fields.FieldByName("COSTCENT").SetValue(fields[21], false);
                    AMACQASST1detail1.Fields.FieldByName("GROUP").SetValue(fields[17], false);
                    AMACQASST1detail1.Fields.FieldByName("LOCATION").SetValue(fields[18], false);
                    AMACQASST1detail1.Fields.FieldByName("CATEGORY").SetValue(fields[19], false);
                    AMACQASST1detail1.Fields.FieldByName("ACCSET").SetValue(fields[20], false);
                    AMACQASST1detail1.Fields.FieldByName("BKMETHOD").SetValue(fields[22], false);
                    AMACQASST1detail1.Fields.FieldByName("BKPERDID").SetValue(fields[23], false);
                    AMACQASST1detail1.Fields.FieldByName("BKLIFE").SetValue(fields[24], false);
                    AMACQASST1detail1.Insert();

                    numrow++;
                }
                AMACQASST1header.Insert();

                session.Dispose();
                tfp.Dispose();
                tfp.Close();
            }
            catch (System.Runtime.InteropServices.COMException e)
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

        private void LoadSaveFile()
        {
            try
            {
                string fileload = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Kreatif Media Karya/Asset Management/Save/DatabaseSetupAssetManagement.txt";
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
