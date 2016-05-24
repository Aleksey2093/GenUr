using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        double[] koef;

        private void button1xml_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult r = open.ShowDialog();
            if (r != System.Windows.Forms.DialogResult.OK)
                return;
            treeView1.Nodes.Clear();
            listPeremens = new GeneratorCombi(this).getPeremens(open.FileName);
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

        private String getPathLeanFile()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Открыть файл с обучающим набором";
            DialogResult r = new DialogResult();
            Invoke(new MethodInvoker(() => { r = op.ShowDialog(); }));
            if (r == System.Windows.Forms.DialogResult.OK)
                return op.FileName;
            else
                return "";
        }

        private void button2Gradient_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate()
            {
                if (listPeremens.Count == 0)
                    return;
                GeneratorCombi gen = new GeneratorCombi(this);
                resUr = gen.GeneratorVars(listPeremens);
                if (resUr == null || resUr.Count == 0)
                    return;
                List<List<ValuePeremen>> leanvals = new List<List<ValuePeremen>>();
                String path = getPathLeanFile();
                if (path == "")
                    return;
                leanvals = gen.getLeanValueFromFile(path);
                if (leanvals == null || leanvals.Count == 0)
                    return;
                Gradientspusk grad = new Gradientspusk(this);
                koef = grad.runGradientspusk(listPeremens, resUr, leanvals);
                Invoke(new MethodInvoker(() =>
                {
                    richTextBox2.Text = "";
                    //resUr.ForEach((x) => { richTextBox2.Text += x.getKoef(); richTextBox2.Text += "\t"; });
                    foreach (var x in koef)
                    {
                        richTextBox2.Text += x + "\t";
                    }
                }));
            });
            th.Name = "Градиентный спукс";
            th.Start();
        }

        private void button1Prov_Click(object sender, EventArgs e)
        {
            if (resUr != null && listPeremens != null && resUr.Count != 0 && listPeremens.Count != 0)
            {
                List<List<double>> list = new List<List<double>>();
                list = new ProvY().runProv(listPeremens, resUr, koef);
                richTextBox3.Text = "";
                list.ForEach((x) => { x.ForEach((y) => { richTextBox3.Text += y + " \t \t "; }); richTextBox3.Text += "\n"; });
            }
        }
    }
}
