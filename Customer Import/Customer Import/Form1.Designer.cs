namespace Customer_Import
{
    partial class CustomerImport
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
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileLabel = new System.Windows.Forms.Label();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.FileNameTextbox = new System.Windows.Forms.TextBox();
            this.UploadButton = new System.Windows.Forms.Button();
            this.CancelsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DatabaseBox
            // 
            this.DatabaseBox.FormattingEnabled = true;
            this.DatabaseBox.Items.AddRange(new object[] {
            "LIPDAT - Liputan 6",
            "KLYBVI - Brilio Ventura Indonesia",
            "KLYKKI - Kreator Kreatif Indonesia",
            "KLYKPL - Kapan Lagi",
            "CMWTRN - Training Only"});
            this.DatabaseBox.Location = new System.Drawing.Point(69, 6);
            this.DatabaseBox.Name = "DatabaseBox";
            this.DatabaseBox.Size = new System.Drawing.Size(210, 21);
            this.DatabaseBox.TabIndex = 10;
            this.DatabaseBox.Text = "Select Database";
            // 
            // SearchButton
            // 
            this.SearchButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SearchButton.Location = new System.Drawing.Point(44, 71);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(112, 41);
            this.SearchButton.TabIndex = 8;
            this.SearchButton.Text = "Search File";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Location = new System.Drawing.Point(15, 41);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(23, 13);
            this.FileLabel.TabIndex = 15;
            this.FileLabel.Text = "File";
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.AutoSize = true;
            this.DatabaseLabel.Location = new System.Drawing.Point(12, 9);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(53, 13);
            this.DatabaseLabel.TabIndex = 14;
            this.DatabaseLabel.Text = "Database";
            // 
            // LoadingLabel
            // 
            this.LoadingLabel.AutoSize = true;
            this.LoadingLabel.BackColor = System.Drawing.SystemColors.Control;
            this.LoadingLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadingLabel.Location = new System.Drawing.Point(310, 38);
            this.LoadingLabel.Name = "LoadingLabel";
            this.LoadingLabel.Size = new System.Drawing.Size(77, 15);
            this.LoadingLabel.TabIndex = 13;
            this.LoadingLabel.Text = "PLEASE WAIT";
            // 
            // FileNameTextbox
            // 
            this.FileNameTextbox.Location = new System.Drawing.Point(44, 38);
            this.FileNameTextbox.Name = "FileNameTextbox";
            this.FileNameTextbox.Size = new System.Drawing.Size(235, 20);
            this.FileNameTextbox.TabIndex = 12;
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(167, 71);
            this.UploadButton.Margin = new System.Windows.Forms.Padding(2);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(112, 41);
            this.UploadButton.TabIndex = 11;
            this.UploadButton.Text = "Upload File";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // CancelsButton
            // 
            this.CancelsButton.Location = new System.Drawing.Point(293, 86);
            this.CancelsButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelsButton.Name = "CancelsButton";
            this.CancelsButton.Size = new System.Drawing.Size(112, 26);
            this.CancelsButton.TabIndex = 9;
            this.CancelsButton.Text = "Cancel";
            this.CancelsButton.UseVisualStyleBackColor = true;
            this.CancelsButton.Click += new System.EventHandler(this.CancelsButton_Click);
            // 
            // CustomerImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 123);
            this.Controls.Add(this.DatabaseBox);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.FileLabel);
            this.Controls.Add(this.DatabaseLabel);
            this.Controls.Add(this.LoadingLabel);
            this.Controls.Add(this.FileNameTextbox);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.CancelsButton);
            this.Name = "CustomerImport";
            this.Text = "Customer Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox DatabaseBox;
        internal System.Windows.Forms.Button SearchButton;
        internal System.Windows.Forms.OpenFileDialog SearchDialog;
        internal System.Windows.Forms.Label FileLabel;
        internal System.Windows.Forms.Label DatabaseLabel;
        internal System.Windows.Forms.Label LoadingLabel;
        internal System.Windows.Forms.TextBox FileNameTextbox;
        internal System.Windows.Forms.Button UploadButton;
        internal System.Windows.Forms.Button CancelsButton;
    }
}

