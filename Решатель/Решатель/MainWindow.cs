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
            resUr = null; koef = null;
            richTextBox1.Text = richTextBox2.Text = richTextBox3.Text = "";
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
            bool ifi = true;
            Parallel.ForEach(proiz, (row,state) =>
            {
                if (row.Length != rowlen)
                {
                    ifi = false;
                    state.Stop();
                }
            });
            return ifi;
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
            DialogQ q = new DialogQ();
            if (learn.Count > 10000)
            {
                DialogResult r = MessageBox.Show("Файл обучения содержит " + learn.Count +
                    " строк. Умешить для ускорения работы алгоритма градиентного спуска",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                retq:
                    q.Text = "Количество элементов файла обучения";
                    q.setText("Введите количество элементов, которые необходимо забрать из файла");
                    q.ShowDialog();
                    int len = q.getValueInt();
                    if (len > learn.Count || len == -1)
                        goto retq;
                    else if (len != -2)
                        learn = learn.GetRange(0, len);
                }
            }
            Proiz classProiz = new Proiz();
            double[][] proiz;
            double[] ylean;
            classProiz.getProiz(listPeremens, allstkombo, learn, out proiz, out ylean);
            if (provPrizAndYlean(proiz, ylean, learn.Count, allstkombo.Count + 1) == false)
                return;
            double eps = 1.0, la = 0.0001;
            eps = Properties.Settings.Default.eps;
            la = Properties.Settings.Default.lamda;
            textBox1.Text = "start time: " + DateTime.Now;
            toolStripLabel1.Text = "Работает градиентный спуск";
            toolStripProgressBar1.Value = 1;
            learn.Clear();
            new Thread(delegate()
            {
                GradientСпуск grad = new GradientСпуск(proiz, ylean, eps, la, allstkombo.Count + 1, this);
                koef = grad.runСпуск();
                Invoke(new MethodInvoker(() =>
                {
                    toolStripLabel1.Text = "Градиентный спуск закончил работу";
                    foreach (var i in koef)
                        richTextBox2.Text += i + "\t";
                    richTextBox1.Text = "end time: " + DateTime.Now + "\n" + richTextBox1.Text;
                }));
                resUr = allstkombo;
            }).Start();
        }

        /// <summary>
        /// обновляет значение процесс бара и тестового блока, для отображения информации
        /// о работе градиентного спуска
        /// </summary>
        /// <param name="iter">номер итерации градиентного спуска</param>
        /// <param name="nowJ">значение J на текущей итерации</param>
        /// <param name="lamda">значение лямды на текущей итерации</param>
        /// <param name="t">значение промежутка времени от старта программы до момента который засек таймер</param>
        public void setIterData(int iter, double nowJ, double lamda, TimeSpan t)
        {
            try
            {
                Invoke(new MethodInvoker(() =>
                {
                    if (toolStripProgressBar1.Value == 100)
                        toolStripProgressBar1.Value = 1;
                    toolStripProgressBar1.Value++;

                    if (richTextBox1.Text.Length > 5000)
                        richTextBox1.Text = "";
                    else
                        richTextBox1.Text = iter + ") J: " + nowJ + "\t la: " + lamda + "\t time: " + t + "\n" + richTextBox1.Text;
                }));
            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine("Фигня фигней просто, приложение закрылось в тот момент, когда вторичный поток пытался что-то сделать с элементами управления уже закрытого окна," + 
                "но у него ничего не получилось потому, что это окно уже закрыто и элементы управления ликвидированы. Текст ошибки: " + ex.ToString());
            }
        }

        private void button1Prov_Click(object sender, EventArgs e)
        {
            new Thread(delegate()
            {
                if (koef != null && koef.Length > 0 && resUr != null && listPeremens != null
                    && resUr.Count != 0 && listPeremens.Count != 0)
                {
                    double[][] list = new ProvY().runProv(listPeremens, resUr, koef);
                    Invoke(new MethodInvoker(() =>
                    {
                        richTextBox3.Text = "";
                        list.ToList().ForEach((x) => { richTextBox3.Text += x[2] + "\n"; });
                    }));
                }
            }).Start();
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

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }
    }
}
