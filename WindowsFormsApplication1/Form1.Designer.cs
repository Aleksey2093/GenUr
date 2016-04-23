namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ResulttextBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.coutXtextBox2 = new System.Windows.Forms.TextBox();
            this.minXtextBox3 = new System.Windows.Forms.TextBox();
            this.maxXtextBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button2Stop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ResulttextBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1222, 499);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ResulttextBox1
            // 
            this.ResulttextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResulttextBox1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResulttextBox1.Location = new System.Drawing.Point(3, 43);
            this.ResulttextBox1.Multiline = true;
            this.ResulttextBox1.Name = "ResulttextBox1";
            this.ResulttextBox1.ReadOnly = true;
            this.ResulttextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResulttextBox1.Size = new System.Drawing.Size(1216, 433);
            this.ResulttextBox1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.ResulttextBox1, "Сгенерированное уравнение");
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.coutXtextBox2);
            this.flowLayoutPanel1.Controls.Add(this.minXtextBox3);
            this.flowLayoutPanel1.Controls.Add(this.maxXtextBox4);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2Stop);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1216, 34);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // coutXtextBox2
            // 
            this.coutXtextBox2.Location = new System.Drawing.Point(3, 3);
            this.coutXtextBox2.Name = "coutXtextBox2";
            this.coutXtextBox2.Size = new System.Drawing.Size(100, 20);
            this.coutXtextBox2.TabIndex = 0;
            this.coutXtextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.coutXtextBox2, "Введите количество n (x1.....xn)");
            // 
            // minXtextBox3
            // 
            this.minXtextBox3.Location = new System.Drawing.Point(109, 3);
            this.minXtextBox3.Name = "minXtextBox3";
            this.minXtextBox3.Size = new System.Drawing.Size(100, 20);
            this.minXtextBox3.TabIndex = 1;
            this.minXtextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.minXtextBox3, "Введите минимальное значение Х");
            // 
            // maxXtextBox4
            // 
            this.maxXtextBox4.Location = new System.Drawing.Point(215, 3);
            this.maxXtextBox4.Name = "maxXtextBox4";
            this.maxXtextBox4.Size = new System.Drawing.Size(100, 20);
            this.maxXtextBox4.TabIndex = 2;
            this.maxXtextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.maxXtextBox4, "Введите максимальное значение Х");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 20);
            this.button1.TabIndex = 3;
            this.button1.Text = "Генерировать уравнение";
            this.toolTip1.SetToolTip(this.button1, "Нажмите сюда, чтобы сгенировать новое уравнение");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 479);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1222, 20);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 17);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // button2Stop
            // 
            this.button2Stop.Location = new System.Drawing.Point(503, 3);
            this.button2Stop.Name = "button2Stop";
            this.button2Stop.Size = new System.Drawing.Size(75, 23);
            this.button2Stop.TabIndex = 4;
            this.button2Stop.Text = "Остановить расчет";
            this.button2Stop.UseVisualStyleBackColor = true;
            this.button2Stop.Click += new System.EventHandler(this.button2Stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 499);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Генератор уравнений";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox ResulttextBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox coutXtextBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox minXtextBox3;
        private System.Windows.Forms.TextBox maxXtextBox4;
        public System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button button2Stop;
    }
}

