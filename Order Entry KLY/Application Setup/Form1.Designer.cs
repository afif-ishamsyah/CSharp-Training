namespace Application_Setup
{
    partial class ApplicationSetup
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
            this.AccpacGroup = new System.Windows.Forms.GroupBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.DatabaseTextBox = new System.Windows.Forms.TextBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.FolderGroup = new System.Windows.Forms.GroupBox();
            this.LogErrorButton = new System.Windows.Forms.Button();
            this.LogErrorTextBox = new System.Windows.Forms.TextBox();
            this.LogErrorLabel = new System.Windows.Forms.Label();
            this.ErrorButton = new System.Windows.Forms.Button();
            this.ErrorTextBox = new System.Windows.Forms.TextBox();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.SuccessButton = new System.Windows.Forms.Button();
            this.SuccessTextBox = new System.Windows.Forms.TextBox();
            this.SuccessLabel = new System.Windows.Forms.Label();
            this.ShipmentButton = new System.Windows.Forms.Button();
            this.ShipmentTextBox = new System.Windows.Forms.TextBox();
            this.ShipmentFileLabel = new System.Windows.Forms.Label();
            this.AccpacGroup.SuspendLayout();
            this.FolderGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccpacGroup
            // 
            this.AccpacGroup.Controls.Add(this.PasswordTextBox);
            this.AccpacGroup.Controls.Add(this.PasswordLabel);
            this.AccpacGroup.Controls.Add(this.DatabaseTextBox);
            this.AccpacGroup.Controls.Add(this.DatabaseLabel);
            this.AccpacGroup.Controls.Add(this.UsernameTextBox);
            this.AccpacGroup.Controls.Add(this.UsernameLabel);
            this.AccpacGroup.Location = new System.Drawing.Point(12, 12);
            this.AccpacGroup.Name = "AccpacGroup";
            this.AccpacGroup.Size = new System.Drawing.Size(271, 146);
            this.AccpacGroup.TabIndex = 13;
            this.AccpacGroup.TabStop = false;
            this.AccpacGroup.Text = "ACCPAC Connection";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.PasswordTextBox.Location = new System.Drawing.Point(90, 63);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(153, 20);
            this.PasswordTextBox.TabIndex = 5;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(16, 66);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(70, 13);
            this.PasswordLabel.TabIndex = 4;
            this.PasswordLabel.Text = "PASSWORD";
            // 
            // DatabaseTextBox
            // 
            this.DatabaseTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DatabaseTextBox.Location = new System.Drawing.Point(90, 97);
            this.DatabaseTextBox.Name = "DatabaseTextBox";
            this.DatabaseTextBox.Size = new System.Drawing.Size(153, 20);
            this.DatabaseTextBox.TabIndex = 3;
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.AutoSize = true;
            this.DatabaseLabel.Location = new System.Drawing.Point(16, 100);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(64, 13);
            this.DatabaseLabel.TabIndex = 2;
            this.DatabaseLabel.Text = "DATABASE";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.UsernameTextBox.Location = new System.Drawing.Point(90, 30);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(153, 20);
            this.UsernameTextBox.TabIndex = 1;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(16, 33);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(68, 13);
            this.UsernameLabel.TabIndex = 1;
            this.UsernameLabel.Text = "USERNAME";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(690, 172);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 15;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(771, 172);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 14;
            this.CancelButton.Text = "Exit";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // FolderGroup
            // 
            this.FolderGroup.Controls.Add(this.LogErrorButton);
            this.FolderGroup.Controls.Add(this.LogErrorTextBox);
            this.FolderGroup.Controls.Add(this.LogErrorLabel);
            this.FolderGroup.Controls.Add(this.ErrorButton);
            this.FolderGroup.Controls.Add(this.ErrorTextBox);
            this.FolderGroup.Controls.Add(this.ErrorLabel);
            this.FolderGroup.Controls.Add(this.SuccessButton);
            this.FolderGroup.Controls.Add(this.SuccessTextBox);
            this.FolderGroup.Controls.Add(this.SuccessLabel);
            this.FolderGroup.Controls.Add(this.ShipmentButton);
            this.FolderGroup.Controls.Add(this.ShipmentTextBox);
            this.FolderGroup.Controls.Add(this.ShipmentFileLabel);
            this.FolderGroup.Location = new System.Drawing.Point(289, 12);
            this.FolderGroup.Name = "FolderGroup";
            this.FolderGroup.Size = new System.Drawing.Size(568, 146);
            this.FolderGroup.TabIndex = 16;
            this.FolderGroup.TabStop = false;
            this.FolderGroup.Text = "Folder Settings";
            // 
            // LogErrorButton
            // 
            this.LogErrorButton.Location = new System.Drawing.Point(482, 119);
            this.LogErrorButton.Name = "LogErrorButton";
            this.LogErrorButton.Size = new System.Drawing.Size(75, 20);
            this.LogErrorButton.TabIndex = 17;
            this.LogErrorButton.Text = "Search";
            this.LogErrorButton.UseVisualStyleBackColor = true;
            this.LogErrorButton.Click += new System.EventHandler(this.LogErrorButton_Click);
            // 
            // LogErrorTextBox
            // 
            this.LogErrorTextBox.Enabled = false;
            this.LogErrorTextBox.Location = new System.Drawing.Point(143, 119);
            this.LogErrorTextBox.Name = "LogErrorTextBox";
            this.LogErrorTextBox.Size = new System.Drawing.Size(333, 20);
            this.LogErrorTextBox.TabIndex = 16;
            // 
            // LogErrorLabel
            // 
            this.LogErrorLabel.AutoSize = true;
            this.LogErrorLabel.Location = new System.Drawing.Point(20, 122);
            this.LogErrorLabel.Name = "LogErrorLabel";
            this.LogErrorLabel.Size = new System.Drawing.Size(117, 13);
            this.LogErrorLabel.TabIndex = 15;
            this.LogErrorLabel.Text = "LOG ERROR FOLDER";
            // 
            // ErrorButton
            // 
            this.ErrorButton.Location = new System.Drawing.Point(482, 90);
            this.ErrorButton.Name = "ErrorButton";
            this.ErrorButton.Size = new System.Drawing.Size(75, 20);
            this.ErrorButton.TabIndex = 14;
            this.ErrorButton.Text = "Search";
            this.ErrorButton.UseVisualStyleBackColor = true;
            this.ErrorButton.Click += new System.EventHandler(this.ErrorButton_Click);
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.Enabled = false;
            this.ErrorTextBox.Location = new System.Drawing.Point(143, 90);
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.Size = new System.Drawing.Size(333, 20);
            this.ErrorTextBox.TabIndex = 13;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(20, 93);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(92, 13);
            this.ErrorLabel.TabIndex = 12;
            this.ErrorLabel.Text = "ERROR FOLDER";
            // 
            // SuccessButton
            // 
            this.SuccessButton.Location = new System.Drawing.Point(482, 59);
            this.SuccessButton.Name = "SuccessButton";
            this.SuccessButton.Size = new System.Drawing.Size(75, 20);
            this.SuccessButton.TabIndex = 11;
            this.SuccessButton.Text = "Search";
            this.SuccessButton.UseVisualStyleBackColor = true;
            this.SuccessButton.Click += new System.EventHandler(this.SuccessButton_Click);
            // 
            // SuccessTextBox
            // 
            this.SuccessTextBox.Enabled = false;
            this.SuccessTextBox.Location = new System.Drawing.Point(143, 59);
            this.SuccessTextBox.Name = "SuccessTextBox";
            this.SuccessTextBox.Size = new System.Drawing.Size(333, 20);
            this.SuccessTextBox.TabIndex = 10;
            // 
            // SuccessLabel
            // 
            this.SuccessLabel.AutoSize = true;
            this.SuccessLabel.Location = new System.Drawing.Point(20, 62);
            this.SuccessLabel.Name = "SuccessLabel";
            this.SuccessLabel.Size = new System.Drawing.Size(103, 13);
            this.SuccessLabel.TabIndex = 9;
            this.SuccessLabel.Text = "SUCCESS FOLDER";
            // 
            // ShipmentButton
            // 
            this.ShipmentButton.Location = new System.Drawing.Point(482, 27);
            this.ShipmentButton.Name = "ShipmentButton";
            this.ShipmentButton.Size = new System.Drawing.Size(75, 20);
            this.ShipmentButton.TabIndex = 8;
            this.ShipmentButton.Text = "Search";
            this.ShipmentButton.UseVisualStyleBackColor = true;
            this.ShipmentButton.Click += new System.EventHandler(this.ShipmentButton_Click);
            // 
            // ShipmentTextBox
            // 
            this.ShipmentTextBox.Enabled = false;
            this.ShipmentTextBox.Location = new System.Drawing.Point(143, 27);
            this.ShipmentTextBox.Name = "ShipmentTextBox";
            this.ShipmentTextBox.Size = new System.Drawing.Size(333, 20);
            this.ShipmentTextBox.TabIndex = 7;
            // 
            // ShipmentFileLabel
            // 
            this.ShipmentFileLabel.AutoSize = true;
            this.ShipmentFileLabel.Location = new System.Drawing.Point(20, 30);
            this.ShipmentFileLabel.Name = "ShipmentFileLabel";
            this.ShipmentFileLabel.Size = new System.Drawing.Size(109, 13);
            this.ShipmentFileLabel.TabIndex = 6;
            this.ShipmentFileLabel.Text = "SHIPMENT FOLDER";
            // 
            // ApplicationSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 204);
            this.Controls.Add(this.AccpacGroup);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.FolderGroup);
            this.Name = "ApplicationSetup";
            this.Text = "Application Setup";
            this.Load += new System.EventHandler(this.ApplicationSetup_Load);
            this.AccpacGroup.ResumeLayout(false);
            this.AccpacGroup.PerformLayout();
            this.FolderGroup.ResumeLayout(false);
            this.FolderGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox AccpacGroup;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox DatabaseTextBox;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.GroupBox FolderGroup;
        private System.Windows.Forms.Button LogErrorButton;
        private System.Windows.Forms.TextBox LogErrorTextBox;
        private System.Windows.Forms.Label LogErrorLabel;
        private System.Windows.Forms.Button ErrorButton;
        private System.Windows.Forms.TextBox ErrorTextBox;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button SuccessButton;
        private System.Windows.Forms.TextBox SuccessTextBox;
        private System.Windows.Forms.Label SuccessLabel;
        private System.Windows.Forms.Button ShipmentButton;
        private System.Windows.Forms.TextBox ShipmentTextBox;
        private System.Windows.Forms.Label ShipmentFileLabel;
    }
}

