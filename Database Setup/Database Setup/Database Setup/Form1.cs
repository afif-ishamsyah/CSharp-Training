using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Database_Setup
{
    public partial class SetupCMWTRN : Form
    {
        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Save/DatabaseSetup.txt";

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

            linesToSave.Add(UsernameTextBox.Lines.Length.ToString());
            linesToSave.AddRange(UsernameTextBox.Lines);

            linesToSave.Add(PasswordTextBox.Lines.Length.ToString());
            linesToSave.AddRange(PasswordTextBox.Lines);

            linesToSave.Add(DatabaseTextBox.Lines.Length.ToString());
            linesToSave.AddRange(DatabaseTextBox.Lines);

            linesToSave.Add(ShipmentTextBox.Lines.Length.ToString());
            linesToSave.AddRange(ShipmentTextBox.Lines);

            linesToSave.Add(SuccessTextBox.Lines.Length.ToString());
            linesToSave.AddRange(SuccessTextBox.Lines);

            linesToSave.Add(ErrorTextBox.Lines.Length.ToString());
            linesToSave.AddRange(ErrorTextBox.Lines);

            linesToSave.Add(LogErrorTextBox.Lines.Length.ToString());
            linesToSave.AddRange(LogErrorTextBox.Lines);

            File.WriteAllLines(fileName, linesToSave);
            MessageBox.Show("Data Saved","Success");
        }

        private void SetupCMWTRN_Load(object sender, EventArgs e)
        {
            try
            {
                string[] lines;
                string[] loadedLines = File.ReadAllLines(fileName);

                int index = 0;

                int n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                UsernameTextBox.Lines = lines;

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                PasswordTextBox.Lines = lines;

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                DatabaseTextBox.Lines = lines;

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                ShipmentTextBox.Lines = lines;

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                SuccessTextBox.Lines = lines;

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                ErrorTextBox.Lines = lines;

                index = index + 2;
                n = int.Parse(loadedLines[index]);
                lines = new string[n];
                Array.Copy(loadedLines, index + 1, lines, 0, n);
                LogErrorTextBox.Lines = lines;
            }
            catch(DirectoryNotFoundException ex)
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Interface Sage/Save");
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
