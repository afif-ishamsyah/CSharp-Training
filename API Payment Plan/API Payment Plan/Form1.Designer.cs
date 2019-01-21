namespace API_Payment_Plan
{
    partial class PaymentForm
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
            this.objectListView = new BrightIdeasSoftware.FastObjectListView();
            this.IDVendor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.VendorName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DocumentNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Description = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DateDoc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DateDue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Currency = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.OriginalInvoice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DaysOver = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BalanceSource = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BalanceFunction = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.SendCashBookButton = new System.Windows.Forms.Button();
            this.CheckAllButton = new System.Windows.Forms.Button();
            this.UncheckAllButton = new System.Windows.Forms.Button();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.CheckFilteredButton = new System.Windows.Forms.Button();
            this.UncheckFIlteredButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CashbookGroupBox = new System.Windows.Forms.GroupBox();
            this.ChooseBankLabel = new System.Windows.Forms.Label();
            this.BankCodeLabel = new System.Windows.Forms.Label();
            this.BankCodeTextBox = new System.Windows.Forms.TextBox();
            this.BankComboBox = new System.Windows.Forms.ComboBox();
            this.ManageGroupBox = new System.Windows.Forms.GroupBox();
            this.DatabaseGroupBox = new System.Windows.Forms.GroupBox();
            this.DatabaseTextBox = new System.Windows.Forms.TextBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.DatabaseComboBox = new System.Windows.Forms.ComboBox();
            this.ChooseDatabaseLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
            this.CashbookGroupBox.SuspendLayout();
            this.ManageGroupBox.SuspendLayout();
            this.DatabaseGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectListView
            // 
            this.objectListView.AllColumns.Add(this.IDVendor);
            this.objectListView.AllColumns.Add(this.VendorName);
            this.objectListView.AllColumns.Add(this.DocumentNo);
            this.objectListView.AllColumns.Add(this.Description);
            this.objectListView.AllColumns.Add(this.DateDoc);
            this.objectListView.AllColumns.Add(this.DateDue);
            this.objectListView.AllColumns.Add(this.Currency);
            this.objectListView.AllColumns.Add(this.OriginalInvoice);
            this.objectListView.AllColumns.Add(this.DaysOver);
            this.objectListView.AllColumns.Add(this.BalanceSource);
            this.objectListView.AllColumns.Add(this.BalanceFunction);
            this.objectListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView.CellEditUseWholeCell = false;
            this.objectListView.CheckBoxes = true;
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDVendor,
            this.VendorName,
            this.DocumentNo,
            this.Description,
            this.DateDoc,
            this.DateDue,
            this.Currency,
            this.OriginalInvoice,
            this.DaysOver,
            this.BalanceSource,
            this.BalanceFunction});
            this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView.FullRowSelect = true;
            this.objectListView.Location = new System.Drawing.Point(12, 120);
            this.objectListView.Name = "objectListView";
            this.objectListView.ShowGroups = false;
            this.objectListView.ShowImagesOnSubItems = true;
            this.objectListView.Size = new System.Drawing.Size(1097, 392);
            this.objectListView.TabIndex = 1;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.UseFilterIndicator = true;
            this.objectListView.UseFiltering = true;
            this.objectListView.View = System.Windows.Forms.View.Details;
            this.objectListView.VirtualMode = true;
            // 
            // IDVendor
            // 
            this.IDVendor.AspectName = "IDVendor";
            this.IDVendor.Hideable = false;
            this.IDVendor.Text = "ID VENDOR";
            this.IDVendor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VendorName
            // 
            this.VendorName.AspectName = "VendorName";
            this.VendorName.Hideable = false;
            this.VendorName.Text = "VENDOR NAME";
            this.VendorName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DocumentNo
            // 
            this.DocumentNo.AspectName = "DocumentNo";
            this.DocumentNo.Hideable = false;
            this.DocumentNo.Text = "DOCUMENT NO";
            this.DocumentNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Description
            // 
            this.Description.AspectName = "Description";
            this.Description.Hideable = false;
            this.Description.Text = "DESCRIPTION";
            this.Description.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DateDoc
            // 
            this.DateDoc.AspectName = "DateDoc";
            this.DateDoc.Hideable = false;
            this.DateDoc.Text = "DATE DOC";
            this.DateDoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DateDue
            // 
            this.DateDue.AspectName = "DateDue";
            this.DateDue.Hideable = false;
            this.DateDue.Text = "DATE DUE";
            this.DateDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Currency
            // 
            this.Currency.AspectName = "Currency";
            this.Currency.Hideable = false;
            this.Currency.Text = "CURRENCY";
            this.Currency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OriginalInvoice
            // 
            this.OriginalInvoice.AspectName = "OriginalInvoice";
            this.OriginalInvoice.Hideable = false;
            this.OriginalInvoice.Text = "ORIGINAL INVOICE";
            this.OriginalInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DaysOver
            // 
            this.DaysOver.AspectName = "DaysOver";
            this.DaysOver.Hideable = false;
            this.DaysOver.Text = "DAYS OVER";
            this.DaysOver.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BalanceSource
            // 
            this.BalanceSource.AspectName = "BalanceSource";
            this.BalanceSource.Hideable = false;
            this.BalanceSource.Text = "BALANCE SOURCE";
            this.BalanceSource.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BalanceFunction
            // 
            this.BalanceFunction.AspectName = "BalanceFunction";
            this.BalanceFunction.Hideable = false;
            this.BalanceFunction.Text = "BALANCE FUNCTION";
            this.BalanceFunction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SendCashBookButton
            // 
            this.SendCashBookButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SendCashBookButton.Location = new System.Drawing.Point(260, 35);
            this.SendCashBookButton.Name = "SendCashBookButton";
            this.SendCashBookButton.Size = new System.Drawing.Size(115, 49);
            this.SendCashBookButton.TabIndex = 2;
            this.SendCashBookButton.Text = "Send to Cashbook";
            this.SendCashBookButton.UseVisualStyleBackColor = true;
            this.SendCashBookButton.Click += new System.EventHandler(this.PostButton_Click);
            // 
            // CheckAllButton
            // 
            this.CheckAllButton.Location = new System.Drawing.Point(6, 44);
            this.CheckAllButton.Name = "CheckAllButton";
            this.CheckAllButton.Size = new System.Drawing.Size(94, 23);
            this.CheckAllButton.TabIndex = 3;
            this.CheckAllButton.Text = "Check All";
            this.CheckAllButton.UseVisualStyleBackColor = true;
            this.CheckAllButton.Click += new System.EventHandler(this.CheckAllButton_Click);
            // 
            // UncheckAllButton
            // 
            this.UncheckAllButton.Location = new System.Drawing.Point(106, 44);
            this.UncheckAllButton.Name = "UncheckAllButton";
            this.UncheckAllButton.Size = new System.Drawing.Size(100, 23);
            this.UncheckAllButton.TabIndex = 4;
            this.UncheckAllButton.Text = "Uncheck All";
            this.UncheckAllButton.UseVisualStyleBackColor = true;
            this.UncheckAllButton.Click += new System.EventHandler(this.UncheckAllButton_Click);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(63, 16);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(197, 20);
            this.SearchTextBox.TabIndex = 5;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(6, 19);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(51, 13);
            this.SearchLabel.TabIndex = 6;
            this.SearchLabel.Text = "SEARCH";
            // 
            // CheckFilteredButton
            // 
            this.CheckFilteredButton.Location = new System.Drawing.Point(6, 73);
            this.CheckFilteredButton.Name = "CheckFilteredButton";
            this.CheckFilteredButton.Size = new System.Drawing.Size(94, 23);
            this.CheckFilteredButton.TabIndex = 7;
            this.CheckFilteredButton.Text = "Check Filtered";
            this.CheckFilteredButton.UseVisualStyleBackColor = true;
            this.CheckFilteredButton.Click += new System.EventHandler(this.CheckFilteredButton_Click);
            // 
            // UncheckFIlteredButton
            // 
            this.UncheckFIlteredButton.Location = new System.Drawing.Point(106, 73);
            this.UncheckFIlteredButton.Name = "UncheckFIlteredButton";
            this.UncheckFIlteredButton.Size = new System.Drawing.Size(100, 23);
            this.UncheckFIlteredButton.TabIndex = 8;
            this.UncheckFIlteredButton.Text = "Uncheck Filtered";
            this.UncheckFIlteredButton.UseVisualStyleBackColor = true;
            this.UncheckFIlteredButton.Click += new System.EventHandler(this.UncheckFIlteredButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(266, 14);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 9;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CashbookGroupBox
            // 
            this.CashbookGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CashbookGroupBox.Controls.Add(this.ChooseBankLabel);
            this.CashbookGroupBox.Controls.Add(this.BankCodeLabel);
            this.CashbookGroupBox.Controls.Add(this.BankCodeTextBox);
            this.CashbookGroupBox.Controls.Add(this.BankComboBox);
            this.CashbookGroupBox.Controls.Add(this.SendCashBookButton);
            this.CashbookGroupBox.Location = new System.Drawing.Point(728, 12);
            this.CashbookGroupBox.Name = "CashbookGroupBox";
            this.CashbookGroupBox.Size = new System.Drawing.Size(381, 102);
            this.CashbookGroupBox.TabIndex = 10;
            this.CashbookGroupBox.TabStop = false;
            this.CashbookGroupBox.Text = "Cashbook";
            // 
            // ChooseBankLabel
            // 
            this.ChooseBankLabel.AutoSize = true;
            this.ChooseBankLabel.Location = new System.Drawing.Point(6, 19);
            this.ChooseBankLabel.Name = "ChooseBankLabel";
            this.ChooseBankLabel.Size = new System.Drawing.Size(84, 13);
            this.ChooseBankLabel.TabIndex = 6;
            this.ChooseBankLabel.Text = "CHOOSE BANK";
            // 
            // BankCodeLabel
            // 
            this.BankCodeLabel.AutoSize = true;
            this.BankCodeLabel.Location = new System.Drawing.Point(6, 67);
            this.BankCodeLabel.Name = "BankCodeLabel";
            this.BankCodeLabel.Size = new System.Drawing.Size(69, 13);
            this.BankCodeLabel.TabIndex = 5;
            this.BankCodeLabel.Text = "BANK CODE";
            // 
            // BankCodeTextBox
            // 
            this.BankCodeTextBox.Enabled = false;
            this.BankCodeTextBox.Location = new System.Drawing.Point(81, 64);
            this.BankCodeTextBox.Name = "BankCodeTextBox";
            this.BankCodeTextBox.Size = new System.Drawing.Size(173, 20);
            this.BankCodeTextBox.TabIndex = 4;
            // 
            // BankComboBox
            // 
            this.BankComboBox.FormattingEnabled = true;
            this.BankComboBox.Location = new System.Drawing.Point(9, 35);
            this.BankComboBox.Name = "BankComboBox";
            this.BankComboBox.Size = new System.Drawing.Size(245, 21);
            this.BankComboBox.TabIndex = 3;
            this.BankComboBox.SelectedIndexChanged += new System.EventHandler(this.BankComboBox_SelectedIndexChanged);
            // 
            // ManageGroupBox
            // 
            this.ManageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ManageGroupBox.Controls.Add(this.SearchLabel);
            this.ManageGroupBox.Controls.Add(this.SearchTextBox);
            this.ManageGroupBox.Controls.Add(this.UncheckAllButton);
            this.ManageGroupBox.Controls.Add(this.CheckFilteredButton);
            this.ManageGroupBox.Controls.Add(this.UncheckFIlteredButton);
            this.ManageGroupBox.Controls.Add(this.ClearButton);
            this.ManageGroupBox.Controls.Add(this.CheckAllButton);
            this.ManageGroupBox.Location = new System.Drawing.Point(375, 12);
            this.ManageGroupBox.Name = "ManageGroupBox";
            this.ManageGroupBox.Size = new System.Drawing.Size(347, 102);
            this.ManageGroupBox.TabIndex = 11;
            this.ManageGroupBox.TabStop = false;
            this.ManageGroupBox.Text = "Manage";
            // 
            // DatabaseGroupBox
            // 
            this.DatabaseGroupBox.Controls.Add(this.DatabaseTextBox);
            this.DatabaseGroupBox.Controls.Add(this.GenerateButton);
            this.DatabaseGroupBox.Controls.Add(this.DatabaseComboBox);
            this.DatabaseGroupBox.Controls.Add(this.ChooseDatabaseLabel);
            this.DatabaseGroupBox.Location = new System.Drawing.Point(12, 12);
            this.DatabaseGroupBox.Name = "DatabaseGroupBox";
            this.DatabaseGroupBox.Size = new System.Drawing.Size(292, 102);
            this.DatabaseGroupBox.TabIndex = 12;
            this.DatabaseGroupBox.TabStop = false;
            this.DatabaseGroupBox.Text = "Database";
            // 
            // DatabaseTextBox
            // 
            this.DatabaseTextBox.Enabled = false;
            this.DatabaseTextBox.Location = new System.Drawing.Point(9, 64);
            this.DatabaseTextBox.Name = "DatabaseTextBox";
            this.DatabaseTextBox.Size = new System.Drawing.Size(143, 20);
            this.DatabaseTextBox.TabIndex = 3;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(158, 62);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(128, 23);
            this.GenerateButton.TabIndex = 2;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // DatabaseComboBox
            // 
            this.DatabaseComboBox.FormattingEnabled = true;
            this.DatabaseComboBox.Location = new System.Drawing.Point(6, 35);
            this.DatabaseComboBox.Name = "DatabaseComboBox";
            this.DatabaseComboBox.Size = new System.Drawing.Size(280, 21);
            this.DatabaseComboBox.TabIndex = 1;
            this.DatabaseComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ChooseDatabaseLabel
            // 
            this.ChooseDatabaseLabel.AutoSize = true;
            this.ChooseDatabaseLabel.Location = new System.Drawing.Point(6, 19);
            this.ChooseDatabaseLabel.Name = "ChooseDatabaseLabel";
            this.ChooseDatabaseLabel.Size = new System.Drawing.Size(112, 13);
            this.ChooseDatabaseLabel.TabIndex = 0;
            this.ChooseDatabaseLabel.Text = "CHOOSE DATABASE";
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 520);
            this.Controls.Add(this.DatabaseGroupBox);
            this.Controls.Add(this.ManageGroupBox);
            this.Controls.Add(this.CashbookGroupBox);
            this.Controls.Add(this.objectListView);
            this.Name = "PaymentForm";
            this.Text = "API Payment Plan";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
            this.CashbookGroupBox.ResumeLayout(false);
            this.CashbookGroupBox.PerformLayout();
            this.ManageGroupBox.ResumeLayout(false);
            this.ManageGroupBox.PerformLayout();
            this.DatabaseGroupBox.ResumeLayout(false);
            this.DatabaseGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private BrightIdeasSoftware.OLVColumn IDVendor;
        private BrightIdeasSoftware.OLVColumn VendorName;
        private BrightIdeasSoftware.OLVColumn DocumentNo;
        private BrightIdeasSoftware.OLVColumn Description;
        private BrightIdeasSoftware.OLVColumn DateDoc;
        private BrightIdeasSoftware.OLVColumn DateDue;
        private BrightIdeasSoftware.OLVColumn Currency;
        private BrightIdeasSoftware.OLVColumn OriginalInvoice;
        private BrightIdeasSoftware.OLVColumn DaysOver;
        private BrightIdeasSoftware.OLVColumn BalanceSource;
        private BrightIdeasSoftware.OLVColumn BalanceFunction;
        private System.Windows.Forms.Button SendCashBookButton;
        private System.Windows.Forms.Button CheckAllButton;
        private System.Windows.Forms.Button UncheckAllButton;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Button CheckFilteredButton;
        private System.Windows.Forms.Button UncheckFIlteredButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.GroupBox CashbookGroupBox;
        private System.Windows.Forms.ComboBox BankComboBox;
        private System.Windows.Forms.TextBox BankCodeTextBox;
        private System.Windows.Forms.Label BankCodeLabel;
        private System.Windows.Forms.GroupBox ManageGroupBox;
        private System.Windows.Forms.Label ChooseBankLabel;
        private System.Windows.Forms.GroupBox DatabaseGroupBox;
        private System.Windows.Forms.Label ChooseDatabaseLabel;
        private System.Windows.Forms.ComboBox DatabaseComboBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.TextBox DatabaseTextBox;
        private BrightIdeasSoftware.FastObjectListView objectListView;
    }
}

