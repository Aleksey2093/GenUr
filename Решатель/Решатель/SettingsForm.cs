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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private bool getDouble(string text, out double result)
        {
            result = 0;
            if (double.TryParse(text, out result))
                return true;
            else if (double.TryParse(text.Replace(".", ","), out result))
                return true;
            else if (double.TryParse(text.Replace(",", "."), out result))
                return true;
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double la, eps;
            if (!getDouble(textBox1.Text, out la))
                MessageBox.Show("Некорректное значение лямды, исправьте значение.", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else if (la <= 0.0)
                MessageBox.Show("Лямба не может быть меньше или равна нулю.", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else if (!getDouble(textBox2.Text,out eps))
                MessageBox.Show("Некорректное значение eps, исправьте пожалуйста.", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else if (eps <= 0.0)
                MessageBox.Show("Eps не может быть меньше или равна нулю.", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else
            {
                Properties.Settings.Default.lamda = la;
                Properties.Settings.Default.eps = eps;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.lamda.ToString();
            textBox2.Text = Properties.Settings.Default.eps.ToString();
        }
    }
}
