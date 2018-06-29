using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Database_Setup
{
    public partial class SetupCMWTRN : Form
    {
        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Kreator Kreatif/Save/DatabaseSetupShipmentKKI.txt";
        List<TextBox> TextBoxList = new List<TextBox>();
        

        public SetupCMWTRN()
        {
            InitializeComponent();
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
                linesToSave.Add(Box.Lines.Length.ToString());
                linesToSave.AddRange(Box.Lines);
            }

            File.WriteAllLines(fileName, linesToSave);
            MessageBox.Show("Data Saved","Success");
        }

        private void SetupCMWTRN_Load(object sender, EventArgs e)
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

                foreach(TextBox Box in TextBoxList)
                {
                    n = int.Parse(loadedLines[index]);
                    lines = new string[n];
                    Array.Copy(loadedLines, index + 1, lines, 0, n);
                    Box.Lines = lines;

                    index = index + 2;
                }
            }
            catch(DirectoryNotFoundException ex)
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Kreator Kreatif/Save");
            }
            catch (FileNotFoundException ex) { }
            catch (IndexOutOfRangeException ex) { }
        }

        private void ShipmentButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Shipment = new FolderBrowserDialog();
            if(Shipment.ShowDialog() == DialogResult.OK)
            {
                ShipmentTextBox.Text = Shipment.SelectedPath;
            }
        }

        private void SuccessButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Success = new FolderBrowserDialog();
            if (Success.ShowDialog() == DialogResult.OK)
            {
                SuccessTextBox.Text = Success.SelectedPath;
            }
        }

        private void ErrorButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Error = new FolderBrowserDialog();
            if (Error.ShowDialog() == DialogResult.OK)
            {
                ErrorTextBox.Text = Error.SelectedPath;
            }
        }

        private void LogErrorButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog LogError = new FolderBrowserDialog();
            if (LogError.ShowDialog() == DialogResult.OK)
            {
                LogErrorTextBox.Text = LogError.SelectedPath;
            }
        }
    }
}
