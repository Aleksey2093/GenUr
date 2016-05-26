using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Решатель
{
    public partial class DialogQ : Form
    {
        public DialogQ()
        {
            InitializeComponent();
        }

        /// <summary>
        /// устанавливает текст в поле метки
        /// </summary>
        /// <param name="text">то что будет отображено в метке</param>
        public void setText(string text)
        {
            label1.Text = text;
        }

        /// <summary>
        /// получить введенное в числовое поле значение типа int
        /// </summary>
        /// <returns></returns>
        public int getValueInt()
        {
            int i;
            if (int.TryParse(textBox1.Text, out i))
                return i;
            else
                return -1;
        }
        public double getValueDouble()
        {
            double i;
            if (double.TryParse(textBox1.Text, out i))
            {
                return i;
            }
            else if (double.TryParse(textBox1.Text.Replace(".", ","), out i))
            {
                return i;
            }
            else if (double.TryParse(textBox1.Text.Replace(",", "."), out i))
            {
                return i;
            }
            else
                return -1;
        }

        private void DialogQ_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double resultdouble = -1; int resultint = -1; string[] str = new string[3];
            str[0] = textBox1.Text;
            str[1] = textBox1.Text.Replace(".", ",");
            str[2] = textBox1.Text.Replace(".", ",");
            int ifi = 0;
            for (int i = 0; i < str.Length; i++)
                if (int.TryParse(str[i], out resultint))
                {
                    ifi = 2;
                    break;
                }
                else if (double.TryParse(str[i], out resultdouble))
                {
                    ifi = 1;
                    break;
                }
            string text = "";
            switch (ifi)
            {
                case 0:
                    text = "Введено не числовое значение";
                    break;
                case 1:
                    if (resultdouble < 0)
                    {
                        text = "Недопустим ввод отрицательных чисел";
                        ifi = 0;
                    }
                    break;
                case 2:
                    if (resultint < 1)
                    {
                        text = "Введите число больше 1";
                        ifi = 0;
                    }
                    break;
            }
            if (ifi == 0)
                MessageBox.Show(text + ".\nВведено: " + str[0], "Неправильный ввод", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = -2 + "";
            this.Close();
        }
    }
}
