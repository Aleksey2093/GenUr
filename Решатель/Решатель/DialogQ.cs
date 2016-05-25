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

        public void setText(string text)
        {
            label1.Text = text;
        }

        public int getValue()
        {
            int i;
            if (int.TryParse(textBox1.Text, out i))
                return i;
            else
                return -1;
        }

        private void DialogQ_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int i;
            if (!int.TryParse(textBox1.Text, out i))
            {
                MessageBox.Show("Значение не может быть отрицательным", "Неправильный ввод", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
