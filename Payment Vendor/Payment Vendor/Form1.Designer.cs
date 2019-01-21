using BrightIdeasSoftware;

namespace Payment_Vendor
{
    partial class PaymentVendorForm
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
            this.BatchView = new BrightIdeasSoftware.FastObjectListView();
            this.ROWNUMBER = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BATCHID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.VENDORID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.VENDORNAME = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DESCRIPTION = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DATE = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BANKNAME = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BENEFICIARYNAME = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ACCOUNTNUMBER = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.AMOUNT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.CURRENCY = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.TYPE = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BANKCODE = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.VendorComboBox = new System.Windows.Forms.ComboBox();
            this.CsvButton = new System.Windows.Forms.Button();
            this.FromCalendar = new System.Windows.Forms.MonthCalendar();
            this.VendorLabel = new System.Windows.Forms.Label();
            this.ToCalendar = new System.Windows.Forms.MonthCalendar();
            this.FromTextBox = new System.Windows.Forms.TextBox();
            this.ToTextBox = new System.Windows.Forms.TextBox();
            this.VendorIDTextBox = new System.Windows.Forms.TextBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.FromLabel = new System.Windows.Forms.Label();
            this.ToLabel = new System.Windows.Forms.Label();
            this.FromClearButton = new System.Windows.Forms.Button();
            this.ToClearButton = new System.Windows.Forms.Button();
            this.GenerateGroupBox = new System.Windows.Forms.GroupBox();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ManageGroupBox = new System.Windows.Forms.GroupBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.UncheckAllButton = new System.Windows.Forms.Button();
            this.CheckFilteredButton = new System.Windows.Forms.Button();
            this.UncheckFIlteredButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CheckAllButton = new System.Windows.Forms.Button();
            this.ActionGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.BatchView)).BeginInit();
            this.GenerateGroupBox.SuspendLayout();
            this.ManageGroupBox.SuspendLayout();
            this.ActionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BatchView
            // 
            this.BatchView.AllColumns.Add(this.ROWNUMBER);
            this.BatchView.AllColumns.Add(this.BATCHID);
            this.BatchView.AllColumns.Add(this.VENDORID);
            this.BatchView.AllColumns.Add(this.VENDORNAME);
            this.BatchView.AllColumns.Add(this.DESCRIPTION);
            this.BatchView.AllColumns.Add(this.DATE);
            this.BatchView.AllColumns.Add(this.BANKNAME);
            this.BatchView.AllColumns.Add(this.BENEFICIARYNAME);
            this.BatchView.AllColumns.Add(this.ACCOUNTNUMBER);
            this.BatchView.AllColumns.Add(this.AMOUNT);
            this.BatchView.AllColumns.Add(this.CURRENCY);
            this.BatchView.AllColumns.Add(this.TYPE);
            this.BatchView.AllColumns.Add(this.BANKCODE);
            this.BatchView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchView.CellEditUseWholeCell = false;
            this.BatchView.CheckBoxes = true;
            this.BatchView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ROWNUMBER,
            this.BATCHID,
            this.VENDORID,
            this.VENDORNAME,
            this.DESCRIPTION,
            this.DATE,
            this.BANKNAME,
            this.BENEFICIARYNAME,
            this.ACCOUNTNUMBER,
            this.AMOUNT,
            this.CURRENCY,
            this.TYPE,
            this.BANKCODE});
            this.BatchView.Cursor = System.Windows.Forms.Cursors.Default;
            this.BatchView.FullRowSelect = true;
            this.BatchView.Location = new System.Drawing.Point(12, 307);
            this.BatchView.Name = "BatchView";
            this.BatchView.ShowGroups = false;
            this.BatchView.ShowImagesOnSubItems = true;
            this.BatchView.Size = new System.Drawing.Size(1014, 407);
            this.BatchView.TabIndex = 0;
            this.BatchView.UseCompatibleStateImageBehavior = false;
            this.BatchView.UseFiltering = true;
            this.BatchView.View = System.Windows.Forms.View.Details;
            this.BatchView.VirtualMode = true;
            this.BatchView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.HandleCellEditFinished);
            this.BatchView.Click += new System.EventHandler(this.BatchView_Click);
            // 
            // ROWNUMBER
            // 
            this.ROWNUMBER.AspectName = "ROWNUMBER";
            this.ROWNUMBER.IsEditable = false;
            this.ROWNUMBER.Text = "NO";
            // 
            // BATCHID
            // 
            this.BATCHID.AspectName = "BATCHID";
            this.BATCHID.IsEditable = false;
            this.BATCHID.Text = "BATCH ID";
            this.BATCHID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.BATCHID.Width = 69;
            // 
            // VENDORID
            // 
            this.VENDORID.AspectName = "VENDORID";
            this.VENDORID.IsEditable = false;
            this.VENDORID.Text = "VENDOR ID";
            this.VENDORID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VENDORNAME
            // 
            this.VENDORNAME.AspectName = "VENDORNAME";
            this.VENDORNAME.IsEditable = false;
            this.VENDORNAME.Text = "VENDOR NAME";
            this.VENDORNAME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DESCRIPTION
            // 
            this.DESCRIPTION.AspectName = "DESCRIPTION";
            this.DESCRIPTION.IsEditable = false;
            this.DESCRIPTION.Text = "DESCRIPTION";
            this.DESCRIPTION.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DATE
            // 
            this.DATE.AspectName = "DATE";
            this.DATE.IsEditable = false;
            this.DATE.Text = "DATE";
            this.DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BANKNAME
            // 
            this.BANKNAME.AspectName = "BANKNAME";
            this.BANKNAME.Text = "BANK NAME";
            this.BANKNAME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BENEFICIARYNAME
            // 
            this.BENEFICIARYNAME.AspectName = "BENEFICIARYNAME";
            this.BENEFICIARYNAME.Text = "BENEFICIARY NAME";
            this.BENEFICIARYNAME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ACCOUNTNUMBER
            // 
            this.ACCOUNTNUMBER.AspectName = "ACCOUNTNUMBER";
            this.ACCOUNTNUMBER.Text = "ACCOUNT NUMBER";
            this.ACCOUNTNUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AMOUNT
            // 
            this.AMOUNT.AspectName = "AMOUNT";
            this.AMOUNT.IsEditable = false;
            this.AMOUNT.Text = "AMOUNT";
            this.AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CURRENCY
            // 
            this.CURRENCY.AspectName = "CURRENCY";
            this.CURRENCY.IsEditable = false;
            this.CURRENCY.Text = "CURRENCY";
            this.CURRENCY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TYPE
            // 
            this.TYPE.AspectName = "TYPE";
            this.TYPE.Text = "TYPE";
            this.TYPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BANKCODE
            // 
            this.BANKCODE.AspectName = "BANKCODE";
            this.BANKCODE.IsEditable = false;
            this.BANKCODE.Text = "BANK CODE";
            this.BANKCODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VendorComboBox
            // 
            this.VendorComboBox.FormattingEnabled = true;
            this.VendorComboBox.Location = new System.Drawing.Point(65, 18);
            this.VendorComboBox.Name = "VendorComboBox";
            this.VendorComboBox.Size = new System.Drawing.Size(279, 21);
            this.VendorComboBox.TabIndex = 1;
            this.VendorComboBox.SelectedIndexChanged += new System.EventHandler(this.VendorComboBox_SelectedIndexChanged);
            // 
            // CsvButton
            // 
            this.CsvButton.Location = new System.Drawing.Point(149, 28);
            this.CsvButton.Name = "CsvButton";
            this.CsvButton.Size = new System.Drawing.Size(211, 37);
            this.CsvButton.TabIndex = 3;
            this.CsvButton.Text = "Download CSV";
            this.CsvButton.UseVisualStyleBackColor = true;
            this.CsvButton.Click += new System.EventHandler(this.CsvButton_Click);
            // 
            // FromCalendar
            // 
            this.FromCalendar.Location = new System.Drawing.Point(12, 49);
            this.FromCalendar.MaxSelectionCount = 1;
            this.FromCalendar.Name = "FromCalendar";
            this.FromCalendar.TabIndex = 4;
            this.FromCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.FromCalendar_DateChanged);
            // 
            // VendorLabel
            // 
            this.VendorLabel.AutoSize = true;
            this.VendorLabel.Location = new System.Drawing.Point(6, 21);
            this.VendorLabel.Name = "VendorLabel";
            this.VendorLabel.Size = new System.Drawing.Size(53, 13);
            this.VendorLabel.TabIndex = 5;
            this.VendorLabel.Text = "VENDOR";
            // 
            // ToCalendar
            // 
            this.ToCalendar.Location = new System.Drawing.Point(238, 49);
            this.ToCalendar.MaxSelectionCount = 1;
            this.ToCalendar.Name = "ToCalendar";
            this.ToCalendar.TabIndex = 6;
            this.ToCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.ToCalendar_DateChanged);
            // 
            // FromTextBox
            // 
            this.FromTextBox.Location = new System.Drawing.Point(53, 217);
            this.FromTextBox.Name = "FromTextBox";
            this.FromTextBox.ReadOnly = true;
            this.FromTextBox.Size = new System.Drawing.Size(97, 20);
            this.FromTextBox.TabIndex = 7;
            // 
            // ToTextBox
            // 
            this.ToTextBox.Location = new System.Drawing.Point(263, 217);
            this.ToTextBox.Name = "ToTextBox";
            this.ToTextBox.ReadOnly = true;
            this.ToTextBox.Size = new System.Drawing.Size(116, 20);
            this.ToTextBox.TabIndex = 8;
            // 
            // VendorIDTextBox
            // 
            this.VendorIDTextBox.Location = new System.Drawing.Point(350, 18);
            this.VendorIDTextBox.Name = "VendorIDTextBox";
            this.VendorIDTextBox.ReadOnly = true;
            this.VendorIDTextBox.Size = new System.Drawing.Size(108, 20);
            this.VendorIDTextBox.TabIndex = 9;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(312, 246);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(146, 36);
            this.GenerateButton.TabIndex = 10;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Location = new System.Drawing.Point(9, 220);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(38, 13);
            this.FromLabel.TabIndex = 11;
            this.FromLabel.Text = "FROM";
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Location = new System.Drawing.Point(235, 220);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(22, 13);
            this.ToLabel.TabIndex = 12;
            this.ToLabel.Text = "TO";
            // 
            // FromClearButton
            // 
            this.FromClearButton.Location = new System.Drawing.Point(156, 217);
            this.FromClearButton.Name = "FromClearButton";
            this.FromClearButton.Size = new System.Drawing.Size(73, 23);
            this.FromClearButton.TabIndex = 13;
            this.FromClearButton.Text = "Clear";
            this.FromClearButton.UseVisualStyleBackColor = true;
            this.FromClearButton.Click += new System.EventHandler(this.FromClearButton_Click);
            // 
            // ToClearButton
            // 
            this.ToClearButton.Location = new System.Drawing.Point(385, 217);
            this.ToClearButton.Name = "ToClearButton";
            this.ToClearButton.Size = new System.Drawing.Size(73, 23);
            this.ToClearButton.TabIndex = 14;
            this.ToClearButton.Text = "Clear";
            this.ToClearButton.UseVisualStyleBackColor = true;
            this.ToClearButton.Click += new System.EventHandler(this.ToClearButton_Click);
            // 
            // GenerateGroupBox
            // 
            this.GenerateGroupBox.Controls.Add(this.ToClearButton);
            this.GenerateGroupBox.Controls.Add(this.FromClearButton);
            this.GenerateGroupBox.Controls.Add(this.ToLabel);
            this.GenerateGroupBox.Controls.Add(this.FromLabel);
            this.GenerateGroupBox.Controls.Add(this.GenerateButton);
            this.GenerateGroupBox.Controls.Add(this.ToTextBox);
            this.GenerateGroupBox.Controls.Add(this.VendorLabel);
            this.GenerateGroupBox.Controls.Add(this.VendorIDTextBox);
            this.GenerateGroupBox.Controls.Add(this.FromTextBox);
            this.GenerateGroupBox.Controls.Add(this.ToCalendar);
            this.GenerateGroupBox.Controls.Add(this.FromCalendar);
            this.GenerateGroupBox.Controls.Add(this.VendorComboBox);
            this.GenerateGroupBox.Location = new System.Drawing.Point(12, 6);
            this.GenerateGroupBox.Name = "GenerateGroupBox";
            this.GenerateGroupBox.Size = new System.Drawing.Size(471, 295);
            this.GenerateGroupBox.TabIndex = 18;
            this.GenerateGroupBox.TabStop = false;
            this.GenerateGroupBox.Text = "Generate Data";
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(6, 28);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(137, 37);
            this.ExitButton.TabIndex = 19;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ManageGroupBox
            // 
            this.ManageGroupBox.Controls.Add(this.SearchLabel);
            this.ManageGroupBox.Controls.Add(this.SearchTextBox);
            this.ManageGroupBox.Controls.Add(this.UncheckAllButton);
            this.ManageGroupBox.Controls.Add(this.CheckFilteredButton);
            this.ManageGroupBox.Controls.Add(this.UncheckFIlteredButton);
            this.ManageGroupBox.Controls.Add(this.ClearButton);
            this.ManageGroupBox.Controls.Add(this.CheckAllButton);
            this.ManageGroupBox.Location = new System.Drawing.Point(489, 12);
            this.ManageGroupBox.Name = "ManageGroupBox";
            this.ManageGroupBox.Size = new System.Drawing.Size(372, 205);
            this.ManageGroupBox.TabIndex = 20;
            this.ManageGroupBox.TabStop = false;
            this.ManageGroupBox.Text = "Manage";
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
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(63, 16);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(197, 20);
            this.SearchTextBox.TabIndex = 5;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // UncheckAllButton
            // 
            this.UncheckAllButton.Location = new System.Drawing.Point(81, 44);
            this.UncheckAllButton.Name = "UncheckAllButton";
            this.UncheckAllButton.Size = new System.Drawing.Size(84, 29);
            this.UncheckAllButton.TabIndex = 4;
            this.UncheckAllButton.Text = "Uncheck All";
            this.UncheckAllButton.UseVisualStyleBackColor = true;
            this.UncheckAllButton.Click += new System.EventHandler(this.UncheckAllButton_Click);
            // 
            // CheckFilteredButton
            // 
            this.CheckFilteredButton.Location = new System.Drawing.Point(171, 44);
            this.CheckFilteredButton.Name = "CheckFilteredButton";
            this.CheckFilteredButton.Size = new System.Drawing.Size(89, 29);
            this.CheckFilteredButton.TabIndex = 7;
            this.CheckFilteredButton.Text = "Check Filtered";
            this.CheckFilteredButton.UseVisualStyleBackColor = true;
            this.CheckFilteredButton.Click += new System.EventHandler(this.CheckFilteredButton_Click);
            // 
            // UncheckFIlteredButton
            // 
            this.UncheckFIlteredButton.Location = new System.Drawing.Point(266, 45);
            this.UncheckFIlteredButton.Name = "UncheckFIlteredButton";
            this.UncheckFIlteredButton.Size = new System.Drawing.Size(96, 28);
            this.UncheckFIlteredButton.TabIndex = 8;
            this.UncheckFIlteredButton.Text = "Uncheck Filtered";
            this.UncheckFIlteredButton.UseVisualStyleBackColor = true;
            this.UncheckFIlteredButton.Click += new System.EventHandler(this.UncheckFIlteredButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(266, 14);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(96, 23);
            this.ClearButton.TabIndex = 9;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CheckAllButton
            // 
            this.CheckAllButton.Location = new System.Drawing.Point(9, 45);
            this.CheckAllButton.Name = "CheckAllButton";
            this.CheckAllButton.Size = new System.Drawing.Size(66, 28);
            this.CheckAllButton.TabIndex = 3;
            this.CheckAllButton.Text = "Check All";
            this.CheckAllButton.UseVisualStyleBackColor = true;
            this.CheckAllButton.Click += new System.EventHandler(this.CheckAllButton_Click);
            // 
            // ActionGroupBox
            // 
            this.ActionGroupBox.Controls.Add(this.ExitButton);
            this.ActionGroupBox.Controls.Add(this.CsvButton);
            this.ActionGroupBox.Location = new System.Drawing.Point(495, 223);
            this.ActionGroupBox.Name = "ActionGroupBox";
            this.ActionGroupBox.Size = new System.Drawing.Size(366, 78);
            this.ActionGroupBox.TabIndex = 21;
            this.ActionGroupBox.TabStop = false;
            this.ActionGroupBox.Text = "Action";
            // 
            // PaymentVendorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 726);
            this.Controls.Add(this.ActionGroupBox);
            this.Controls.Add(this.ManageGroupBox);
            this.Controls.Add(this.GenerateGroupBox);
            this.Controls.Add(this.BatchView);
            this.Name = "PaymentVendorForm";
            this.Text = "Payment Vendor";
            this.Load += new System.EventHandler(this.PaymentVendorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BatchView)).EndInit();
            this.GenerateGroupBox.ResumeLayout(false);
            this.GenerateGroupBox.PerformLayout();
            this.ManageGroupBox.ResumeLayout(false);
            this.ManageGroupBox.PerformLayout();
            this.ActionGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox VendorComboBox;
        private System.Windows.Forms.Button CsvButton;
        private System.Windows.Forms.MonthCalendar FromCalendar;
        private System.Windows.Forms.Label VendorLabel;
        private System.Windows.Forms.MonthCalendar ToCalendar;
        private System.Windows.Forms.TextBox FromTextBox;
        private System.Windows.Forms.TextBox ToTextBox;
        private System.Windows.Forms.TextBox VendorIDTextBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.Label ToLabel;
        private System.Windows.Forms.Button FromClearButton;
        private System.Windows.Forms.Button ToClearButton;
        private System.Windows.Forms.GroupBox GenerateGroupBox;
        private System.Windows.Forms.Button ExitButton;
        private BrightIdeasSoftware.OLVColumn BATCHID;
        private BrightIdeasSoftware.OLVColumn VENDORID;
        private BrightIdeasSoftware.OLVColumn DESCRIPTION;
        private BrightIdeasSoftware.OLVColumn DATE;
        private BrightIdeasSoftware.OLVColumn BANKNAME;
        private BrightIdeasSoftware.OLVColumn BENEFICIARYNAME;
        private BrightIdeasSoftware.OLVColumn ACCOUNTNUMBER;
        private BrightIdeasSoftware.OLVColumn AMOUNT;
        private BrightIdeasSoftware.OLVColumn CURRENCY;
        private BrightIdeasSoftware.OLVColumn TYPE;
        private BrightIdeasSoftware.OLVColumn BANKCODE;
        private BrightIdeasSoftware.FastObjectListView BatchView;
        private System.Windows.Forms.GroupBox ManageGroupBox;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button UncheckAllButton;
        private System.Windows.Forms.Button CheckFilteredButton;
        private System.Windows.Forms.Button UncheckFIlteredButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button CheckAllButton;
        private System.Windows.Forms.GroupBox ActionGroupBox;
        private BrightIdeasSoftware.OLVColumn VENDORNAME;
        private OLVColumn ROWNUMBER;
    }
}

