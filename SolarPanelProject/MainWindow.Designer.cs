namespace SolarPanelProject
{
    partial class MainWindow
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
            this.PortTextBox = new System.Windows.Forms.ComboBox();
            this.ParityTextBox = new System.Windows.Forms.ComboBox();
            this.StopBitsTextBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DataBitsTextBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BaudRateTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Rts = new System.Windows.Forms.CheckBox();
            this.OpenPortButton = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.LongitudeTextBox = new System.Windows.Forms.TextBox();
            this.LatitudeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PortTextBox
            // 
            this.PortTextBox.FormattingEnabled = true;
            this.PortTextBox.Location = new System.Drawing.Point(286, 49);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(121, 21);
            this.PortTextBox.TabIndex = 0;
            // 
            // ParityTextBox
            // 
            this.ParityTextBox.FormattingEnabled = true;
            this.ParityTextBox.Location = new System.Drawing.Point(286, 76);
            this.ParityTextBox.Name = "ParityTextBox";
            this.ParityTextBox.Size = new System.Drawing.Size(121, 21);
            this.ParityTextBox.TabIndex = 1;
            // 
            // StopBitsTextBox
            // 
            this.StopBitsTextBox.FormattingEnabled = true;
            this.StopBitsTextBox.Location = new System.Drawing.Point(286, 103);
            this.StopBitsTextBox.Name = "StopBitsTextBox";
            this.StopBitsTextBox.Size = new System.Drawing.Size(121, 21);
            this.StopBitsTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Parity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stop Bits";
            // 
            // DataBitsTextBox
            // 
            this.DataBitsTextBox.FormattingEnabled = true;
            this.DataBitsTextBox.Location = new System.Drawing.Point(286, 130);
            this.DataBitsTextBox.Name = "DataBitsTextBox";
            this.DataBitsTextBox.Size = new System.Drawing.Size(121, 21);
            this.DataBitsTextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data Bits";
            // 
            // BaudRateTextBox
            // 
            this.BaudRateTextBox.Location = new System.Drawing.Point(288, 23);
            this.BaudRateTextBox.Name = "BaudRateTextBox";
            this.BaudRateTextBox.Size = new System.Drawing.Size(100, 20);
            this.BaudRateTextBox.TabIndex = 8;
            this.BaudRateTextBox.Text = "9600";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Baud Rate";
            // 
            // Rts
            // 
            this.Rts.AutoSize = true;
            this.Rts.Location = new System.Drawing.Point(275, 168);
            this.Rts.Name = "Rts";
            this.Rts.Size = new System.Drawing.Size(42, 17);
            this.Rts.TabIndex = 10;
            this.Rts.Text = "Rts";
            this.Rts.UseVisualStyleBackColor = true;
            // 
            // OpenPortButton
            // 
            this.OpenPortButton.Location = new System.Drawing.Point(332, 168);
            this.OpenPortButton.Name = "OpenPortButton";
            this.OpenPortButton.Size = new System.Drawing.Size(75, 23);
            this.OpenPortButton.TabIndex = 11;
            this.OpenPortButton.Text = "Open Port";
            this.OpenPortButton.UseVisualStyleBackColor = true;
            this.OpenPortButton.Click += new System.EventHandler(this.OpenPortButton_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(306, 259);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(101, 31);
            this.Start.TabIndex = 12;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // LongitudeTextBox
            // 
            this.LongitudeTextBox.Location = new System.Drawing.Point(60, 61);
            this.LongitudeTextBox.Name = "LongitudeTextBox";
            this.LongitudeTextBox.Size = new System.Drawing.Size(100, 20);
            this.LongitudeTextBox.TabIndex = 13;
            // 
            // LatitudeTextBox
            // 
            this.LatitudeTextBox.Location = new System.Drawing.Point(61, 35);
            this.LatitudeTextBox.Name = "LatitudeTextBox";
            this.LatitudeTextBox.Size = new System.Drawing.Size(100, 20);
            this.LatitudeTextBox.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Manual Cords Settings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Latitude";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Longitude";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 302);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LatitudeTextBox);
            this.Controls.Add(this.LongitudeTextBox);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.OpenPortButton);
            this.Controls.Add(this.Rts);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BaudRateTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DataBitsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StopBitsTextBox);
            this.Controls.Add(this.ParityTextBox);
            this.Controls.Add(this.PortTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "Solar Panel Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PortTextBox;
        private System.Windows.Forms.ComboBox ParityTextBox;
        private System.Windows.Forms.ComboBox StopBitsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DataBitsTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox BaudRateTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox Rts;
        private System.Windows.Forms.Button OpenPortButton;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox LongitudeTextBox;
        private System.Windows.Forms.TextBox LatitudeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

