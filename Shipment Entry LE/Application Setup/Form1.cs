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

namespace Application_Setup
{
    public partial class ApplicationSetup : Form
    {
        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Liputan Enam/OE Shipment/Save/DatabaseSetupShipmentLE.txt";
        List<TextBox> TextBoxList = new List<TextBox>();

        public ApplicationSetup()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            File_Load();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<string> linesToSave = new List<string>();

            foreach (TextBox Box in TextBoxList)
            {
                if (Box.Text == "")
                {
                    linesToSave.Add("1");
                    linesToSave.Add("undefined");
                }
                else
                {
                    linesToSave.Add(Box.Lines.Length.ToString());
                    linesToSave.AddRange(Box.Lines);
                }
            }

            File.WriteAllLines(fileName, linesToSave);
            MessageBox.Show("Data Saved", "Success");
        }

        private void File_Load()
        {
            TextBoxList.Add(UsernameTextBox);
            TextBoxList.Add(PasswordTextBox);
            TextBoxList.Add(DatabaseTextBox);
            TextBoxList.Add(ShipmentTextBox);
            TextBoxList.Add(SuccessTextBox);
            TextBoxList.Add(ErrorTextBox);
            TextBoxList.Add(LogErrorTextBox);

            try
            {
                string[] lines;
                string[] loadedLines = File.ReadAllLines(fileName);

                int index = 0;
                int n;

                foreach (TextBox Box in TextBoxList)
                {
                    n = int.Parse(loadedLines[index]);
                    lines = new string[n];
                    Array.Copy(loadedLines, index + 1, lines, 0, n);
                    Box.Lines = lines;

                    index = index + 2;
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Liputan Enam/OE Shipment/Save");
            }
            catch (FileNotFoundException ex) { }
            catch (IndexOutOfRangeException ex) { }
        }

        private void ShipmentButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ShipmentBrowser = new FolderBrowserDialog();
            ShipmentBrowser.Description = "Choose folder to save the shipment files";
            ShipmentBrowser.SelectedPath = ShipmentTextBox.Text;
            if (ShipmentBrowser.ShowDialog() == DialogResult.OK)
            {
                ShipmentTextBox.Text = ShipmentBrowser.SelectedPath;
            }
        }

        private void SuccessButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog SuccessBrowser = new FolderBrowserDialog();
            SuccessBrowser.Description = "Choose folder to save the succeed shipment files";
            SuccessBrowser.SelectedPath = SuccessTextBox.Text;
            if (SuccessBrowser.ShowDialog() == DialogResult.OK)
            {
                SuccessTextBox.Text = SuccessBrowser.SelectedPath;
            }
        }

        private void ErrorButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ErrorBrowser = new FolderBrowserDialog();
            ErrorBrowser.Description = "Choose folder to save the failed shipment files";
            ErrorBrowser.SelectedPath = ErrorTextBox.Text;
            if (ErrorBrowser.ShowDialog() == DialogResult.OK)
            {
                ErrorTextBox.Text = ErrorBrowser.SelectedPath;
            }
        }

        private void LogErrorButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog LogErrorBrowser = new FolderBrowserDialog();
            LogErrorBrowser.Description = "Choose folder to save the error log files";
            LogErrorBrowser.SelectedPath = LogErrorTextBox.Text;
            if (LogErrorBrowser.ShowDialog() == DialogResult.OK)
            {
                LogErrorTextBox.Text = LogErrorBrowser.SelectedPath;
            }
        }
    }
}
