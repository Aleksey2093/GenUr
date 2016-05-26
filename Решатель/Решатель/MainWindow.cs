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
            int index = 0;
            treeView1.Nodes.Clear();
            listPeremens.Clear();
            listPeremens = new Fileload().getXML();
            if (listPeremens == null || listPeremens.Count == 0)
                return;
            foreach(var per in listPeremens)
            {
                treeView1.Nodes.Add(per.Name);
                if (per.IfKategori)
                {
                    for (int i = 0; i < per.getCountKat(); i++)
                        treeView1.Nodes[index].Nodes.Add(per.getListCatNames(i));
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

        /// <summary>
        /// проверка массива произведений и Y(из файла) на корректность
        /// </summary>
        /// <param name="proiz">массив произведений</param>
        /// <param name="ylean">массив Y из файла</param>
        /// <param name="learncout">длинна обоих массивов</param>
        /// <param name="rowlen">длинна одной строки массива произведений</param>
        /// <returns></returns>
        private bool provPrizAndYlean(double[][] proiz, double[] ylean, int learncout, int rowlen)
        {
            if (ylean.Length != learncout)
                return false;
            if (proiz.Length != learncout)
                return false;
            for (int i=0;i<proiz.Length;i++)
            {
                if (proiz[i].Length != rowlen)
                    return false;
            }
            return true;
        }

        private void button2Gradient_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            if (listPeremens == null || listPeremens.Count == 0)
            {
                MessageBox.Show("Загрузите файл с переменными", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                return;
            }
            GeneratorКомбинаций genkombi = new GeneratorКомбинаций(listPeremens);
            List<Kombinacia> allstkombo = genkombi.runGen();
            Fileload file = new Fileload();
            List<List<ValueFile>> learn = file.getDataFile();
            if (learn == null || learn.Count == 0)
                return;
            Proiz classProiz = new Proiz();
            double[][] proiz;
            double[] ylean;
            classProiz.getProiz(listPeremens, allstkombo, learn, out proiz, out ylean);
            if (provPrizAndYlean(proiz, ylean, learn.Count, allstkombo.Count + 1) == false)
                return;
            double eps = 1.0, la = 0.0001;
            textBox1.Text = "start time: " + DateTime.Now;
            new Thread(delegate()
            {
                GradientСпуск grad = new GradientСпуск(proiz, ylean, eps, la, allstkombo.Count + 1, this);
                koef = grad.runСпуск();
                resUr = allstkombo;
                Invoke(new MethodInvoker(() =>
                {
                    foreach (var i in koef)
                        richTextBox2.Text += i + "\t";
                    richTextBox1.Text = "end time: " + DateTime.Now + "\n" + richTextBox1.Text;
                }));
            }).Start();
        }

        public void setIterData(int iter, double nowJ, double L)
        {
            Invoke(new MethodInvoker(() => {
                if (richTextBox1.Text.Length > 5000)
                    richTextBox1.Text = "";
                richTextBox1.Text = iter + ")" + nowJ + "\t" + L + "\n" + richTextBox1.Text;
            }));
        }

        private void button1Prov_Click(object sender, EventArgs e)
        {
            if (koef != null && koef.Length > 0 && resUr != null && listPeremens != null 
                && resUr.Count != 0 && listPeremens.Count != 0)
            {
                List<List<double>> list = new List<List<double>>();
                list = new ProvY().runProv(listPeremens, resUr, koef);
                richTextBox3.Text = "";
                list.ForEach((x) => { x.ForEach((y) => { richTextBox3.Text += y + " \t \t "; }); richTextBox3.Text += "\n"; });
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1СохранитьКоэф_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text.Length > 0 && koef != null && koef.Length > 0)
            {
                new Fileload().saveKoef(koef);
            }
        }

        /// <summary>
        /// открыть коэфициенты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            if (listPeremens == null || listPeremens.Count == 0)
            {
                MessageBox.Show("Загрузите файл с переменными", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                return;
            }
            GeneratorКомбинаций genkombi = new GeneratorКомбинаций(listPeremens);
            List<Kombinacia> allstkombo = genkombi.runGen();
            Fileload file = new Fileload();
            List<List<ValueFile>> learn = file.getDataFile();
            if (learn == null || learn.Count == 0)
                return;
            Proiz classProiz = new Proiz();
            double[][] proiz;
            double[] ylean;
            classProiz.getProiz(listPeremens, allstkombo, learn, out proiz, out ylean);
            if (provPrizAndYlean(proiz, ylean, learn.Count, allstkombo.Count + 1) == false)
                return;
            koef = new Fileload().getLoadKoef();
            if (koef == null || koef.Length == 0 && koef.Length == allstkombo.Count + 1)
                return;
            button1Prov.PerformClick();
        }

        private void button1provpoKoef_Click(object sender, EventArgs e)
        {

        }
    }
}
