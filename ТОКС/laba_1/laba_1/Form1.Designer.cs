namespace laba_1
{
    partial class Form1
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
            this.ImputLabel = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.COMList = new System.Windows.Forms.ComboBox();
            this.SpeedList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ImputLabel
            // 
            this.ImputLabel.AutoSize = true;
            this.ImputLabel.Location = new System.Drawing.Point(0, 0);
            this.ImputLabel.Name = "ImputLabel";
            this.ImputLabel.Size = new System.Drawing.Size(47, 17);
            this.ImputLabel.TabIndex = 0;
            this.ImputLabel.Text = "Input: ";
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(0, 125);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(59, 17);
            this.OutputLabel.TabIndex = 1;
            this.OutputLabel.Text = "Output: ";
            // 
            // InputTextBox
            // 
            this.InputTextBox.Location = new System.Drawing.Point(12, 20);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(723, 73);
            this.InputTextBox.TabIndex = 2;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.OutputTextBox.Enabled = false;
            this.OutputTextBox.HideSelection = false;
            this.OutputTextBox.Location = new System.Drawing.Point(12, 145);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.Size = new System.Drawing.Size(723, 102);
            this.OutputTextBox.TabIndex = 3;
            // 
            // COMList
            // 
            this.COMList.DisplayMember = "0";
            this.COMList.FormattingEnabled = true;
            this.COMList.Items.AddRange(new object[] {
            "COM1",
            "COM2"});
            this.COMList.Location = new System.Drawing.Point(389, 264);
            this.COMList.Name = "COMList";
            this.COMList.Size = new System.Drawing.Size(346, 24);
            this.COMList.TabIndex = 8;
            // 
            // SpeedList
            // 
            this.SpeedList.FormattingEnabled = true;
            this.SpeedList.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "28800",
            "36000",
            "115000"});
            this.SpeedList.Location = new System.Drawing.Point(389, 307);
            this.SpeedList.Name = "SpeedList";
            this.SpeedList.Size = new System.Drawing.Size(346, 24);
            this.SpeedList.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(346, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "Speed:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(12, 264);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(346, 23);
            this.ConnectButton.TabIndex = 17;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(12, 100);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(346, 23);
            this.SendButton.TabIndex = 18;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 352);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SpeedList);
            this.Controls.Add(this.COMList);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.ImputLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ImputLabel;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.ComboBox COMList;
        private System.Windows.Forms.ComboBox SpeedList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button SendButton;
    }
}

