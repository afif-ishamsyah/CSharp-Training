namespace Vendor_Export_and_Import
{
    partial class VendorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DatabaseBox = new System.Windows.Forms.ComboBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.SearchDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileLabel = new System.Windows.Forms.Label();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.CheckExistButton = new System.Windows.Forms.Button();
            this.SearchIDGroup = new System.Windows.Forms.GroupBox();
            this.ResultComboBox = new System.Windows.Forms.ComboBox();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.SearchIDButton = new System.Windows.Forms.Button();
            this.FirstCharacterTextBox = new System.Windows.Forms.TextBox();
            this.FirstCharacterLabel = new System.Windows.Forms.Label();
            this.SearchNameGroup = new System.Windows.Forms.GroupBox();
            this.VendorNameListView = new System.Windows.Forms.ListView();
            this.SearchNameButton = new System.Windows.Forms.Button();
            this.SearchNameTextBox = new System.Windows.Forms.TextBox();
            this.SearchNameLabel = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.CancelButtons = new System.Windows.Forms.Button();
            this.ExportGroup = new System.Windows.Forms.GroupBox();
            this.ExportButton = new System.Windows.Forms.Button();
            this.SearchIDGroup.SuspendLayout();
            this.SearchNameGroup.SuspendLayout();
            this.ExportGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // DatabaseBox
            // 
            this.DatabaseBox.FormattingEnabled = true;
            this.DatabaseBox.Items.AddRange(new object[] {
            "CMWIDT",
            "VIDDAT",
            "KLYKPL",
            "KLYKKI",
            "KLYBBI",
            "CMWTRN",
            "LIPDAT"});
            this.DatabaseBox.Location = new System.Drawing.Point(82, 12);
            this.DatabaseBox.Name = "DatabaseBox";
            this.DatabaseBox.Size = new System.Drawing.Size(292, 21);
            this.DatabaseBox.TabIndex = 0;
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.AutoSize = true;
            this.DatabaseLabel.Location = new System.Drawing.Point(12, 15);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(64, 13);
            this.DatabaseLabel.TabIndex = 1;
            this.DatabaseLabel.Text = "DATABASE";
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Location = new System.Drawing.Point(12, 50);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(29, 13);
            this.FileLabel.TabIndex = 2;
            this.FileLabel.Text = "FILE";
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(47, 47);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(327, 20);
            this.FileNameTextBox.TabIndex = 3;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(380, 47);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(121, 23);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search File";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // CheckExistButton
            // 
            this.CheckExistButton.Location = new System.Drawing.Point(507, 46);
            this.CheckExistButton.Name = "CheckExistButton";
            this.CheckExistButton.Size = new System.Drawing.Size(120, 23);
            this.CheckExistButton.TabIndex = 5;
            this.CheckExistButton.Text = "Check Exist";
            this.CheckExistButton.UseVisualStyleBackColor = true;
            this.CheckExistButton.Click += new System.EventHandler(this.CheckExistButton_Click);
            // 
            // SearchIDGroup
            // 
            this.SearchIDGroup.Controls.Add(this.ResultComboBox);
            this.SearchIDGroup.Controls.Add(this.ResultLabel);
            this.SearchIDGroup.Controls.Add(this.SearchIDButton);
            this.SearchIDGroup.Controls.Add(this.FirstCharacterTextBox);
            this.SearchIDGroup.Controls.Add(this.FirstCharacterLabel);
            this.SearchIDGroup.Location = new System.Drawing.Point(11, 85);
            this.SearchIDGroup.Name = "SearchIDGroup";
            this.SearchIDGroup.Size = new System.Drawing.Size(237, 140);
            this.SearchIDGroup.TabIndex = 6;
            this.SearchIDGroup.TabStop = false;
            this.SearchIDGroup.Text = "Get Vendor ID";
            // 
            // ResultComboBox
            // 
            this.ResultComboBox.FormattingEnabled = true;
            this.ResultComboBox.Location = new System.Drawing.Point(50, 95);
            this.ResultComboBox.Name = "ResultComboBox";
            this.ResultComboBox.Size = new System.Drawing.Size(166, 21);
            this.ResultComboBox.TabIndex = 9;
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(6, 95);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(37, 13);
            this.ResultLabel.TabIndex = 7;
            this.ResultLabel.Text = "Result";
            // 
            // SearchIDButton
            // 
            this.SearchIDButton.Location = new System.Drawing.Point(116, 55);
            this.SearchIDButton.Name = "SearchIDButton";
            this.SearchIDButton.Size = new System.Drawing.Size(100, 23);
            this.SearchIDButton.TabIndex = 8;
            this.SearchIDButton.Text = "Search";
            this.SearchIDButton.UseVisualStyleBackColor = true;
            this.SearchIDButton.Click += new System.EventHandler(this.SearchIDButton_Click);
            // 
            // FirstCharacterTextBox
            // 
            this.FirstCharacterTextBox.Location = new System.Drawing.Point(116, 28);
            this.FirstCharacterTextBox.MaxLength = 1;
            this.FirstCharacterTextBox.Name = "FirstCharacterTextBox";
            this.FirstCharacterTextBox.Size = new System.Drawing.Size(100, 20);
            this.FirstCharacterTextBox.TabIndex = 7;
            // 
            // FirstCharacterLabel
            // 
            this.FirstCharacterLabel.AutoSize = true;
            this.FirstCharacterLabel.Location = new System.Drawing.Point(6, 31);
            this.FirstCharacterLabel.Name = "FirstCharacterLabel";
            this.FirstCharacterLabel.Size = new System.Drawing.Size(104, 13);
            this.FirstCharacterLabel.TabIndex = 7;
            this.FirstCharacterLabel.Text = "Insert First Character";
            // 
            // SearchNameGroup
            // 
            this.SearchNameGroup.Controls.Add(this.VendorNameListView);
            this.SearchNameGroup.Controls.Add(this.SearchNameButton);
            this.SearchNameGroup.Controls.Add(this.SearchNameTextBox);
            this.SearchNameGroup.Controls.Add(this.SearchNameLabel);
            this.SearchNameGroup.Location = new System.Drawing.Point(265, 85);
            this.SearchNameGroup.Name = "SearchNameGroup";
            this.SearchNameGroup.Size = new System.Drawing.Size(389, 207);
            this.SearchNameGroup.TabIndex = 7;
            this.SearchNameGroup.TabStop = false;
            this.SearchNameGroup.Text = "Get Vendor Name";
            // 
            // VendorNameListView
            // 
            this.VendorNameListView.Location = new System.Drawing.Point(20, 66);
            this.VendorNameListView.Name = "VendorNameListView";
            this.VendorNameListView.Size = new System.Drawing.Size(353, 127);
            this.VendorNameListView.TabIndex = 4;
            this.VendorNameListView.UseCompatibleStateImageBehavior = false;
            // 
            // SearchNameButton
            // 
            this.SearchNameButton.Location = new System.Drawing.Point(272, 28);
            this.SearchNameButton.Name = "SearchNameButton";
            this.SearchNameButton.Size = new System.Drawing.Size(101, 23);
            this.SearchNameButton.TabIndex = 2;
            this.SearchNameButton.Text = "Search";
            this.SearchNameButton.UseVisualStyleBackColor = true;
            this.SearchNameButton.Click += new System.EventHandler(this.SearchNameButton_Click);
            // 
            // SearchNameTextBox
            // 
            this.SearchNameTextBox.Location = new System.Drawing.Point(113, 28);
            this.SearchNameTextBox.Name = "SearchNameTextBox";
            this.SearchNameTextBox.Size = new System.Drawing.Size(153, 20);
            this.SearchNameTextBox.TabIndex = 1;
            // 
            // SearchNameLabel
            // 
            this.SearchNameLabel.AutoSize = true;
            this.SearchNameLabel.Location = new System.Drawing.Point(6, 31);
            this.SearchNameLabel.Name = "SearchNameLabel";
            this.SearchNameLabel.Size = new System.Drawing.Size(101, 13);
            this.SearchNameLabel.TabIndex = 0;
            this.SearchNameLabel.Text = "Insert Vendor Name";
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(419, 297);
            this.UploadButton.Margin = new System.Windows.Forms.Padding(2);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(112, 34);
            this.UploadButton.TabIndex = 9;
            this.UploadButton.Text = "Upload File";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // CancelButtons
            // 
            this.CancelButtons.Location = new System.Drawing.Point(535, 297);
            this.CancelButtons.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButtons.Name = "CancelButtons";
            this.CancelButtons.Size = new System.Drawing.Size(118, 34);
            this.CancelButtons.TabIndex = 8;
            this.CancelButtons.Text = "Cancel";
            this.CancelButtons.UseVisualStyleBackColor = true;
            this.CancelButtons.Click += new System.EventHandler(this.CancelButtons_Click);
            // 
            // ExportGroup
            // 
            this.ExportGroup.Controls.Add(this.ExportButton);
            this.ExportGroup.Location = new System.Drawing.Point(12, 231);
            this.ExportGroup.Name = "ExportGroup";
            this.ExportGroup.Size = new System.Drawing.Size(236, 100);
            this.ExportGroup.TabIndex = 5;
            this.ExportGroup.TabStop = false;
            this.ExportGroup.Text = "Export Vendor";
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(8, 23);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(222, 38);
            this.ExportButton.TabIndex = 0;
            this.ExportButton.Text = "Export Vendor";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // VendorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 339);
            this.Controls.Add(this.ExportGroup);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.CancelButtons);
            this.Controls.Add(this.SearchNameGroup);
            this.Controls.Add(this.SearchIDGroup);
            this.Controls.Add(this.CheckExistButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.FileLabel);
            this.Controls.Add(this.DatabaseLabel);
            this.Controls.Add(this.DatabaseBox);
            this.Name = "VendorForm";
            this.Text = "Vendor Export and Import";
            this.Load += new System.EventHandler(this.VendorForm_Load);
            this.SearchIDGroup.ResumeLayout(false);
            this.SearchIDGroup.PerformLayout();
            this.SearchNameGroup.ResumeLayout(false);
            this.SearchNameGroup.PerformLayout();
            this.ExportGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox DatabaseBox;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.OpenFileDialog SearchDialog;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button CheckExistButton;
        private System.Windows.Forms.GroupBox SearchIDGroup;
        private System.Windows.Forms.TextBox FirstCharacterTextBox;
        private System.Windows.Forms.Label FirstCharacterLabel;
        private System.Windows.Forms.Button SearchIDButton;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.ComboBox ResultComboBox;
        private System.Windows.Forms.GroupBox SearchNameGroup;
        private System.Windows.Forms.Label SearchNameLabel;
        private System.Windows.Forms.TextBox SearchNameTextBox;
        private System.Windows.Forms.Button SearchNameButton;
        internal System.Windows.Forms.ListView VendorNameListView;
        internal System.Windows.Forms.Button UploadButton;
        internal System.Windows.Forms.Button CancelButtons;
        private System.Windows.Forms.GroupBox ExportGroup;
        private System.Windows.Forms.Button ExportButton;
    }
}

