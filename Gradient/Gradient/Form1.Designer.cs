namespace Gradient
{
    partial class Qbox
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
            this.button1 = new System.Windows.Forms.Button();
            this.mediumErr = new System.Windows.Forms.TextBox();
            this.Median = new System.Windows.Forms.TextBox();
            this.QsBox = new System.Windows.Forms.ListBox();
            this.YBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AverageLabel = new System.Windows.Forms.Label();
            this.medianLabel = new System.Windows.Forms.Label();
            this.StDataBox = new System.Windows.Forms.TextBox();
            this.EndDataBox = new System.Windows.Forms.TextBox();
            this.startLabel = new System.Windows.Forms.Label();
            this.EndLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Processing";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.button1_KeyPress);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            // 
            // mediumErr
            // 
            this.mediumErr.Location = new System.Drawing.Point(316, 354);
            this.mediumErr.Name = "mediumErr";
            this.mediumErr.Size = new System.Drawing.Size(205, 20);
            this.mediumErr.TabIndex = 3;
            // 
            // Median
            // 
            this.Median.Location = new System.Drawing.Point(558, 354);
            this.Median.Name = "Median";
            this.Median.Size = new System.Drawing.Size(200, 20);
            this.Median.TabIndex = 4;
            // 
            // QsBox
            // 
            this.QsBox.FormattingEnabled = true;
            this.QsBox.Location = new System.Drawing.Point(316, 38);
            this.QsBox.Name = "QsBox";
            this.QsBox.Size = new System.Drawing.Size(205, 290);
            this.QsBox.TabIndex = 5;
            this.QsBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // YBox
            // 
            this.YBox.FormattingEnabled = true;
            this.YBox.Location = new System.Drawing.Point(558, 38);
            this.YBox.Name = "YBox";
            this.YBox.Size = new System.Drawing.Size(200, 290);
            this.YBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Q:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(558, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y:";
            // 
            // AverageLabel
            // 
            this.AverageLabel.AutoSize = true;
            this.AverageLabel.Location = new System.Drawing.Point(316, 335);
            this.AverageLabel.Name = "AverageLabel";
            this.AverageLabel.Size = new System.Drawing.Size(70, 13);
            this.AverageLabel.TabIndex = 9;
            this.AverageLabel.Text = "average error";
            // 
            // medianLabel
            // 
            this.medianLabel.AutoSize = true;
            this.medianLabel.Location = new System.Drawing.Point(558, 335);
            this.medianLabel.Name = "medianLabel";
            this.medianLabel.Size = new System.Drawing.Size(42, 13);
            this.medianLabel.TabIndex = 10;
            this.medianLabel.Text = "Median";
            // 
            // StDataBox
            // 
            this.StDataBox.Location = new System.Drawing.Point(12, 106);
            this.StDataBox.Name = "StDataBox";
            this.StDataBox.Size = new System.Drawing.Size(100, 20);
            this.StDataBox.TabIndex = 11;
            // 
            // EndDataBox
            // 
            this.EndDataBox.Location = new System.Drawing.Point(13, 152);
            this.EndDataBox.Name = "EndDataBox";
            this.EndDataBox.Size = new System.Drawing.Size(100, 20);
            this.EndDataBox.TabIndex = 12;
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(12, 87);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(32, 13);
            this.startLabel.TabIndex = 13;
            this.startLabel.Text = "Start:";
            // 
            // EndLabel
            // 
            this.EndLabel.AutoSize = true;
            this.EndLabel.Location = new System.Drawing.Point(12, 133);
            this.EndLabel.Name = "EndLabel";
            this.EndLabel.Size = new System.Drawing.Size(26, 13);
            this.EndLabel.TabIndex = 14;
            this.EndLabel.Text = "End";
            // 
            // Qbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 416);
            this.Controls.Add(this.EndLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.EndDataBox);
            this.Controls.Add(this.StDataBox);
            this.Controls.Add(this.medianLabel);
            this.Controls.Add(this.AverageLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.YBox);
            this.Controls.Add(this.QsBox);
            this.Controls.Add(this.Median);
            this.Controls.Add(this.mediumErr);
            this.Controls.Add(this.button1);
            this.Name = "Qbox";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox mediumErr;
        private System.Windows.Forms.TextBox Median;
        private System.Windows.Forms.ListBox QsBox;
        private System.Windows.Forms.ListBox YBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label AverageLabel;
        private System.Windows.Forms.Label medianLabel;
        private System.Windows.Forms.TextBox StDataBox;
        private System.Windows.Forms.TextBox EndDataBox;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label EndLabel;
    }
}

