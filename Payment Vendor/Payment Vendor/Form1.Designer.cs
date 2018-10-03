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
            this.BatchView = new System.Windows.Forms.ListView();
            this.VendorComboBox = new System.Windows.Forms.ComboBox();
            this.CsvButton = new System.Windows.Forms.Button();
            this.FromCalendar = new System.Windows.Forms.MonthCalendar();
            this.VendorLabel = new System.Windows.Forms.Label();
            this.ToCalendar = new System.Windows.Forms.MonthCalendar();
            this.FromTextBox = new System.Windows.Forms.TextBox();
            this.ToTextBox = new System.Windows.Forms.TextBox();
            this.VendorNameTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.FromLabel = new System.Windows.Forms.Label();
            this.ToLabel = new System.Windows.Forms.Label();
            this.FromClearButton = new System.Windows.Forms.Button();
            this.ToClearButton = new System.Windows.Forms.Button();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.UnselectAllButton = new System.Windows.Forms.Button();
            this.DateFilter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BatchView
            // 
            this.BatchView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchView.Location = new System.Drawing.Point(12, 308);
            this.BatchView.Name = "BatchView";
            this.BatchView.Size = new System.Drawing.Size(587, 169);
            this.BatchView.TabIndex = 0;
            this.BatchView.UseCompatibleStateImageBehavior = false;
            // 
            // VendorComboBox
            // 
            this.VendorComboBox.FormattingEnabled = true;
            this.VendorComboBox.Location = new System.Drawing.Point(71, 12);
            this.VendorComboBox.Name = "VendorComboBox";
            this.VendorComboBox.Size = new System.Drawing.Size(123, 21);
            this.VendorComboBox.TabIndex = 1;
            this.VendorComboBox.SelectedIndexChanged += new System.EventHandler(this.VendorComboBox_SelectedIndexChanged);
            // 
            // CsvButton
            // 
            this.CsvButton.Location = new System.Drawing.Point(482, 144);
            this.CsvButton.Name = "CsvButton";
            this.CsvButton.Size = new System.Drawing.Size(117, 52);
            this.CsvButton.TabIndex = 3;
            this.CsvButton.Text = "Download CSV";
            this.CsvButton.UseVisualStyleBackColor = true;
            this.CsvButton.Click += new System.EventHandler(this.CsvButton_Click);
            // 
            // FromCalendar
            // 
            this.FromCalendar.Location = new System.Drawing.Point(12, 69);
            this.FromCalendar.MaxSelectionCount = 1;
            this.FromCalendar.Name = "FromCalendar";
            this.FromCalendar.TabIndex = 4;
            this.FromCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.FromCalendar_DateChanged);
            // 
            // VendorLabel
            // 
            this.VendorLabel.AutoSize = true;
            this.VendorLabel.Location = new System.Drawing.Point(12, 15);
            this.VendorLabel.Name = "VendorLabel";
            this.VendorLabel.Size = new System.Drawing.Size(53, 13);
            this.VendorLabel.TabIndex = 5;
            this.VendorLabel.Text = "VENDOR";
            // 
            // ToCalendar
            // 
            this.ToCalendar.Location = new System.Drawing.Point(250, 69);
            this.ToCalendar.MaxSelectionCount = 1;
            this.ToCalendar.Name = "ToCalendar";
            this.ToCalendar.TabIndex = 6;
            this.ToCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.ToCalendar_DateChanged);
            // 
            // FromTextBox
            // 
            this.FromTextBox.Location = new System.Drawing.Point(56, 241);
            this.FromTextBox.Name = "FromTextBox";
            this.FromTextBox.ReadOnly = true;
            this.FromTextBox.Size = new System.Drawing.Size(97, 20);
            this.FromTextBox.TabIndex = 7;
            // 
            // ToTextBox
            // 
            this.ToTextBox.Location = new System.Drawing.Point(275, 243);
            this.ToTextBox.Name = "ToTextBox";
            this.ToTextBox.ReadOnly = true;
            this.ToTextBox.Size = new System.Drawing.Size(116, 20);
            this.ToTextBox.TabIndex = 8;
            // 
            // VendorNameTextBox
            // 
            this.VendorNameTextBox.Location = new System.Drawing.Point(200, 12);
            this.VendorNameTextBox.Name = "VendorNameTextBox";
            this.VendorNameTextBox.ReadOnly = true;
            this.VendorNameTextBox.Size = new System.Drawing.Size(270, 20);
            this.VendorNameTextBox.TabIndex = 9;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(482, 69);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(117, 69);
            this.SearchButton.TabIndex = 10;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Location = new System.Drawing.Point(12, 246);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(38, 13);
            this.FromLabel.TabIndex = 11;
            this.FromLabel.Text = "FROM";
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Location = new System.Drawing.Point(247, 246);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(22, 13);
            this.ToLabel.TabIndex = 12;
            this.ToLabel.Text = "TO";
            // 
            // FromClearButton
            // 
            this.FromClearButton.Location = new System.Drawing.Point(159, 240);
            this.FromClearButton.Name = "FromClearButton";
            this.FromClearButton.Size = new System.Drawing.Size(73, 23);
            this.FromClearButton.TabIndex = 13;
            this.FromClearButton.Text = "Clear";
            this.FromClearButton.UseVisualStyleBackColor = true;
            this.FromClearButton.Click += new System.EventHandler(this.FromClearButton_Click);
            // 
            // ToClearButton
            // 
            this.ToClearButton.Location = new System.Drawing.Point(397, 241);
            this.ToClearButton.Name = "ToClearButton";
            this.ToClearButton.Size = new System.Drawing.Size(73, 23);
            this.ToClearButton.TabIndex = 14;
            this.ToClearButton.Text = "Clear";
            this.ToClearButton.UseVisualStyleBackColor = true;
            this.ToClearButton.Click += new System.EventHandler(this.ToClearButton_Click);
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.Location = new System.Drawing.Point(12, 275);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAllButton.TabIndex = 15;
            this.SelectAllButton.Text = "Select All";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // UnselectAllButton
            // 
            this.UnselectAllButton.Location = new System.Drawing.Point(93, 275);
            this.UnselectAllButton.Name = "UnselectAllButton";
            this.UnselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.UnselectAllButton.TabIndex = 16;
            this.UnselectAllButton.Text = "Unselect All";
            this.UnselectAllButton.UseVisualStyleBackColor = true;
            this.UnselectAllButton.Click += new System.EventHandler(this.UnselectAllButton_Click);
            // 
            // DateFilter
            // 
            this.DateFilter.AutoSize = true;
            this.DateFilter.Location = new System.Drawing.Point(12, 47);
            this.DateFilter.Name = "DateFilter";
            this.DateFilter.Size = new System.Drawing.Size(76, 13);
            this.DateFilter.TabIndex = 17;
            this.DateFilter.Text = "DATE FILTER";
            // 
            // PaymentVendorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 489);
            this.Controls.Add(this.DateFilter);
            this.Controls.Add(this.UnselectAllButton);
            this.Controls.Add(this.SelectAllButton);
            this.Controls.Add(this.ToClearButton);
            this.Controls.Add(this.FromClearButton);
            this.Controls.Add(this.ToLabel);
            this.Controls.Add(this.FromLabel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.VendorNameTextBox);
            this.Controls.Add(this.ToTextBox);
            this.Controls.Add(this.FromTextBox);
            this.Controls.Add(this.ToCalendar);
            this.Controls.Add(this.VendorLabel);
            this.Controls.Add(this.FromCalendar);
            this.Controls.Add(this.CsvButton);
            this.Controls.Add(this.VendorComboBox);
            this.Controls.Add(this.BatchView);
            this.Name = "PaymentVendorForm";
            this.Text = "Payment Vendor";
            this.Load += new System.EventHandler(this.PaymentVendorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView BatchView;
        private System.Windows.Forms.ComboBox VendorComboBox;
        private System.Windows.Forms.Button CsvButton;
        private System.Windows.Forms.MonthCalendar FromCalendar;
        private System.Windows.Forms.Label VendorLabel;
        private System.Windows.Forms.MonthCalendar ToCalendar;
        private System.Windows.Forms.TextBox FromTextBox;
        private System.Windows.Forms.TextBox ToTextBox;
        private System.Windows.Forms.TextBox VendorNameTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.Label ToLabel;
        private System.Windows.Forms.Button FromClearButton;
        private System.Windows.Forms.Button ToClearButton;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.Button UnselectAllButton;
        private System.Windows.Forms.Label DateFilter;
    }
}

