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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Peremennaya> listPeremens = new List<Peremennaya>();
        List<Kombinacia> resUr = new List<Kombinacia>();

        private void button1xml_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
            treeView1.Nodes.Clear();
            listPeremens = new GeneratorCombi().getPeremens(open.FileName);
            int index = 0;
            foreach(var i in listPeremens)
            {
                treeView1.Nodes.Add(i.getName());
                if (i.getIfKategor())
                {
                    foreach (var j in i.getListKat())
                    {
                        treeView1.Nodes[index].Nodes.Add(j.getName());
                    }
                }
                index++;
            }
        }

        private void button2Gradient_Click(object sender, EventArgs e)
        {
            if (listPeremens.Count == 0)
                return;
            resUr = new GeneratorCombi().GeneratorVars(listPeremens);
            if (resUr == null)
                return;
            richTextBox2.Text = "";
            resUr.ForEach((x) => { richTextBox2.Text += x.getKoef(); richTextBox2.Text += "\t"; });
        }

        private void button1Prov_Click(object sender, EventArgs e)
        {
            if (resUr != null && listPeremens != null && resUr.Count != 0 && listPeremens.Count != 0)
            {
                List<List<double>> list = new List<List<double>>();
                list = new ProvY().runProv(listPeremens, resUr);
                richTextBox3.Text = "";
                list.ForEach((x) => { x.ForEach((y) => { richTextBox3.Text += y + " \t \t "; }); richTextBox3.Text += "\n"; });
            }
        }
    }
}
