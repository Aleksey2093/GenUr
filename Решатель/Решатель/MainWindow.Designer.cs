namespace Решатель
{
    partial class MainWindow
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
            this.button1xml = new System.Windows.Forms.Button();
            this.button2Gradient = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button1Prov = new System.Windows.Forms.Button();
            this.button1СохранитьКоэф = new System.Windows.Forms.Button();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1xml
            // 
            this.button1xml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1xml.Location = new System.Drawing.Point(3, 3);
            this.button1xml.Name = "button1xml";
            this.button1xml.Size = new System.Drawing.Size(244, 23);
            this.button1xml.TabIndex = 0;
            this.button1xml.Text = "Открыть vars.xml";
            this.button1xml.UseVisualStyleBackColor = true;
            this.button1xml.Click += new System.EventHandler(this.button1xml_Click);
            // 
            // button2Gradient
            // 
            this.button2Gradient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2Gradient.Location = new System.Drawing.Point(253, 3);
            this.button2Gradient.Name = "button2Gradient";
            this.button2Gradient.Size = new System.Drawing.Size(244, 23);
            this.button2Gradient.TabIndex = 2;
            this.button2Gradient.Text = "Обучение";
            this.button2Gradient.UseVisualStyleBackColor = true;
            this.button2Gradient.Click += new System.EventHandler(this.button2Gradient_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(238, 207);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(3, 216);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(238, 208);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            // 
            // button1Prov
            // 
            this.button1Prov.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1Prov.Location = new System.Drawing.Point(503, 3);
            this.button1Prov.Name = "button1Prov";
            this.button1Prov.Size = new System.Drawing.Size(246, 23);
            this.button1Prov.TabIndex = 5;
            this.button1Prov.Text = "Прогноз Y";
            this.button1Prov.UseVisualStyleBackColor = true;
            this.button1Prov.Click += new System.EventHandler(this.button1Prov_Click);
            // 
            // button1СохранитьКоэф
            // 
            this.button1СохранитьКоэф.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1СохранитьКоэф.Location = new System.Drawing.Point(253, 465);
            this.button1СохранитьКоэф.Name = "button1СохранитьКоэф";
            this.button1СохранитьКоэф.Size = new System.Drawing.Size(244, 23);
            this.button1СохранитьКоэф.TabIndex = 6;
            this.button1СохранитьКоэф.Text = "Сохр. коэф.";
            this.button1СохранитьКоэф.UseVisualStyleBackColor = true;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.Location = new System.Drawing.Point(503, 32);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(246, 427);
            this.richTextBox3.TabIndex = 7;
            this.richTextBox3.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1СохранитьКоэф, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1xml, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2Gradient, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1Prov, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(752, 491);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(253, 32);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(244, 427);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 32);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(244, 427);
            this.treeView1.TabIndex = 10;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 491);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1xml;
        private System.Windows.Forms.Button button2Gradient;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button1Prov;
        private System.Windows.Forms.Button button1СохранитьКоэф;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView treeView1;
    }
}

