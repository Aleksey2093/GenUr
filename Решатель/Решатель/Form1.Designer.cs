namespace Решатель
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
            this.button1открытьфайлспеременными = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1открытьфайлспеременными
            // 
            this.button1открытьфайлспеременными.Location = new System.Drawing.Point(49, 366);
            this.button1открытьфайлспеременными.Name = "button1открытьфайлспеременными";
            this.button1открытьфайлспеременными.Size = new System.Drawing.Size(199, 23);
            this.button1открытьфайлспеременными.TabIndex = 0;
            this.button1открытьфайлспеременными.Text = "Открыть файл с переменным";
            this.button1открытьфайлспеременными.UseVisualStyleBackColor = true;
            this.button1открытьфайлспеременными.Click += new System.EventHandler(this.button1открытьфайлспеременными_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 414);
            this.Controls.Add(this.button1открытьфайлспеременными);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1открытьфайлспеременными;
    }
}

