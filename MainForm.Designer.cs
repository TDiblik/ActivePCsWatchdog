namespace ActivePCsWatchdog
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogWindow = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SettingBox = new System.Windows.Forms.GroupBox();
            this.SendTestMailButton = new System.Windows.Forms.Button();
            this.ReceiverMailsBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.FromEmailAddressBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PingTimeoutPicker = new System.Windows.Forms.NumericUpDown();
            this.FrequencyTimePicker = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ToTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.FromTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.SMTPPortBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SMTPIPBox = new System.Windows.Forms.TextBox();
            this.NewPcInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AddNewPcButton = new System.Windows.Forms.Button();
            this.PcsBox = new System.Windows.Forms.ListBox();
            this.LogGroupBox = new System.Windows.Forms.GroupBox();
            this.ImportFromCSV = new System.Windows.Forms.Button();
            this.SettingBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PingTimeoutPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyTimePicker)).BeginInit();
            this.LogGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogWindow
            // 
            this.LogWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogWindow.Location = new System.Drawing.Point(3, 19);
            this.LogWindow.Name = "LogWindow";
            this.LogWindow.Size = new System.Drawing.Size(463, 196);
            this.LogWindow.TabIndex = 0;
            this.LogWindow.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "SMTP IP";
            // 
            // SettingBox
            // 
            this.SettingBox.Controls.Add(this.SendTestMailButton);
            this.SettingBox.Controls.Add(this.ReceiverMailsBox);
            this.SettingBox.Controls.Add(this.label10);
            this.SettingBox.Controls.Add(this.FromEmailAddressBox);
            this.SettingBox.Controls.Add(this.label9);
            this.SettingBox.Controls.Add(this.label8);
            this.SettingBox.Controls.Add(this.label7);
            this.SettingBox.Controls.Add(this.PingTimeoutPicker);
            this.SettingBox.Controls.Add(this.FrequencyTimePicker);
            this.SettingBox.Controls.Add(this.label6);
            this.SettingBox.Controls.Add(this.label5);
            this.SettingBox.Controls.Add(this.ToTimePicker);
            this.SettingBox.Controls.Add(this.label1);
            this.SettingBox.Controls.Add(this.FromTimePicker);
            this.SettingBox.Controls.Add(this.SaveSettingsButton);
            this.SettingBox.Controls.Add(this.SMTPPortBox);
            this.SettingBox.Controls.Add(this.label3);
            this.SettingBox.Controls.Add(this.SMTPIPBox);
            this.SettingBox.Controls.Add(this.label2);
            this.SettingBox.Location = new System.Drawing.Point(254, 12);
            this.SettingBox.Name = "SettingBox";
            this.SettingBox.Size = new System.Drawing.Size(223, 425);
            this.SettingBox.TabIndex = 3;
            this.SettingBox.TabStop = false;
            this.SettingBox.Text = "Nastavení";
            // 
            // SendTestMailButton
            // 
            this.SendTestMailButton.Location = new System.Drawing.Point(8, 228);
            this.SendTestMailButton.Name = "SendTestMailButton";
            this.SendTestMailButton.Size = new System.Drawing.Size(121, 23);
            this.SendTestMailButton.TabIndex = 23;
            this.SendTestMailButton.Text = "Zaslat zkušební mail";
            this.SendTestMailButton.UseVisualStyleBackColor = true;
            this.SendTestMailButton.Click += new System.EventHandler(this.SendTestMailButton_Click);
            // 
            // ReceiverMailsBox
            // 
            this.ReceiverMailsBox.Location = new System.Drawing.Point(8, 129);
            this.ReceiverMailsBox.Multiline = true;
            this.ReceiverMailsBox.Name = "ReceiverMailsBox";
            this.ReceiverMailsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ReceiverMailsBox.Size = new System.Drawing.Size(210, 93);
            this.ReceiverMailsBox.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 15);
            this.label10.TabIndex = 21;
            this.label10.Text = "Maily pro výsledky (oddělit ;)";
            // 
            // FromEmailAddressBox
            // 
            this.FromEmailAddressBox.Location = new System.Drawing.Point(6, 85);
            this.FromEmailAddressBox.Name = "FromEmailAddressBox";
            this.FromEmailAddressBox.Size = new System.Drawing.Size(212, 23);
            this.FromEmailAddressBox.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "Odchozí e-mail adresa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 349);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "Ping timeout";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(83, 371);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "sekund";
            // 
            // PingTimeoutPicker
            // 
            this.PingTimeoutPicker.Location = new System.Drawing.Point(8, 367);
            this.PingTimeoutPicker.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.PingTimeoutPicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PingTimeoutPicker.Name = "PingTimeoutPicker";
            this.PingTimeoutPicker.Size = new System.Drawing.Size(69, 23);
            this.PingTimeoutPicker.TabIndex = 16;
            this.PingTimeoutPicker.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FrequencyTimePicker
            // 
            this.FrequencyTimePicker.Location = new System.Drawing.Point(8, 323);
            this.FrequencyTimePicker.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.FrequencyTimePicker.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.FrequencyTimePicker.Name = "FrequencyTimePicker";
            this.FrequencyTimePicker.Size = new System.Drawing.Size(69, 23);
            this.FrequencyTimePicker.TabIndex = 15;
            this.FrequencyTimePicker.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 325);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "minut";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Opakovat každých";
            // 
            // ToTimePicker
            // 
            this.ToTimePicker.CustomFormat = "";
            this.ToTimePicker.Location = new System.Drawing.Point(90, 276);
            this.ToTimePicker.Name = "ToTimePicker";
            this.ToTimePicker.Size = new System.Drawing.Size(70, 23);
            this.ToTimePicker.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Aktivní OD - DO";
            // 
            // FromTimePicker
            // 
            this.FromTimePicker.CustomFormat = "";
            this.FromTimePicker.Location = new System.Drawing.Point(8, 276);
            this.FromTimePicker.Name = "FromTimePicker";
            this.FromTimePicker.Size = new System.Drawing.Size(70, 23);
            this.FromTimePicker.TabIndex = 9;
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(8, 395);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(210, 23);
            this.SaveSettingsButton.TabIndex = 6;
            this.SaveSettingsButton.Text = "Uložit vše";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // SMTPPortBox
            // 
            this.SMTPPortBox.Location = new System.Drawing.Point(155, 37);
            this.SMTPPortBox.Name = "SMTPPortBox";
            this.SMTPPortBox.Size = new System.Drawing.Size(62, 23);
            this.SMTPPortBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "SMTP Port";
            // 
            // SMTPIPBox
            // 
            this.SMTPIPBox.Location = new System.Drawing.Point(6, 37);
            this.SMTPIPBox.Name = "SMTPIPBox";
            this.SMTPIPBox.Size = new System.Drawing.Size(138, 23);
            this.SMTPIPBox.TabIndex = 3;
            // 
            // NewPcInput
            // 
            this.NewPcInput.Location = new System.Drawing.Point(8, 27);
            this.NewPcInput.Name = "NewPcInput";
            this.NewPcInput.Size = new System.Drawing.Size(159, 23);
            this.NewPcInput.TabIndex = 4;
            this.NewPcInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewPcInput_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Název PC";
            // 
            // AddNewPcButton
            // 
            this.AddNewPcButton.Location = new System.Drawing.Point(173, 26);
            this.AddNewPcButton.Name = "AddNewPcButton";
            this.AddNewPcButton.Size = new System.Drawing.Size(75, 23);
            this.AddNewPcButton.TabIndex = 6;
            this.AddNewPcButton.Text = "Přidat";
            this.AddNewPcButton.UseVisualStyleBackColor = true;
            this.AddNewPcButton.Click += new System.EventHandler(this.AddNewPcButton_Click);
            // 
            // PcsBox
            // 
            this.PcsBox.FormattingEnabled = true;
            this.PcsBox.ItemHeight = 15;
            this.PcsBox.Location = new System.Drawing.Point(8, 56);
            this.PcsBox.Name = "PcsBox";
            this.PcsBox.Size = new System.Drawing.Size(240, 379);
            this.PcsBox.TabIndex = 7;
            this.PcsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PcsBox_MouseDoubleClick);
            // 
            // LogGroupBox
            // 
            this.LogGroupBox.Controls.Add(this.LogWindow);
            this.LogGroupBox.Location = new System.Drawing.Point(8, 443);
            this.LogGroupBox.Name = "LogGroupBox";
            this.LogGroupBox.Size = new System.Drawing.Size(469, 218);
            this.LogGroupBox.TabIndex = 8;
            this.LogGroupBox.TabStop = false;
            this.LogGroupBox.Text = "Log";
            // 
            // ImportFromCSV
            // 
            this.ImportFromCSV.Location = new System.Drawing.Point(71, 3);
            this.ImportFromCSV.Name = "ImportFromCSV";
            this.ImportFromCSV.Size = new System.Drawing.Size(90, 23);
            this.ImportFromCSV.TabIndex = 9;
            this.ImportFromCSV.Text = "Import z CSV";
            this.ImportFromCSV.UseVisualStyleBackColor = true;
            this.ImportFromCSV.Click += new System.EventHandler(this.ImportFromCSV_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 670);
            this.Controls.Add(this.ImportFromCSV);
            this.Controls.Add(this.LogGroupBox);
            this.Controls.Add(this.PcsBox);
            this.Controls.Add(this.AddNewPcButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NewPcInput);
            this.Controls.Add(this.SettingBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kontrola PC přes noc";
            this.SettingBox.ResumeLayout(false);
            this.SettingBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PingTimeoutPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyTimePicker)).EndInit();
            this.LogGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox LogWindow;
        private Label label2;
        private GroupBox SettingBox;
        private Button SaveSettingsButton;
        private TextBox SMTPIPBox;
        private TextBox NewPcInput;
        private Label label4;
        private Button AddNewPcButton;
        private ListBox PcsBox;
        private GroupBox LogGroupBox;
        private DateTimePicker FromTimePicker;
        private DateTimePicker ToTimePicker;
        private Label label1;
        private Label label5;
        private Label label6;
        private NumericUpDown FrequencyTimePicker;
        private Label label8;
        private Label label7;
        private NumericUpDown PingTimeoutPicker;
        private Button ImportFromCSV;
        private TextBox FromEmailAddressBox;
        private Label label9;
        private TextBox SMTPPortBox;
        private Label label3;
        private TextBox ReceiverMailsBox;
        private Label label10;
        private Button SendTestMailButton;
    }
}