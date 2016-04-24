using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{

    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
        }

        /// <summary>
        /// единый рандом на весь класс
        /// </summary>
        Random random;

        private void getLenMinMaxValue(ref int lenXN, ref int min_X, ref int max_X, ref int count_cats, ref int count_catcount)
        {
            if (int.TryParse(minXtextBox3.Text, out min_X) == false)
            {
                MessageBox.Show("Введите пожалуйста число в поле минимального значения Х", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                minXtextBox3.Focus();
                minXtextBox3.SelectAll();
                return;
            }
            if (int.TryParse(maxXtextBox4.Text, out max_X) == false)
            {
                MessageBox.Show("Введите пожалуйста число в поле максимального значения Х", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                maxXtextBox4.Focus();
                maxXtextBox4.SelectAll();
                return;
            }
            if (min_X > max_X)
            {
                MessageBox.Show("Введите пожалуйста число в поле максимального значения Х", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                minXtextBox3.Focus();
                minXtextBox3.Clear();
                maxXtextBox4.Clear();
                return;
            }
            if (int.TryParse(coutXtextBox2.Text, out lenXN) == false)
            {
                MessageBox.Show("Введете пожалуйста в поле количества иксов целое положительное число", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                coutXtextBox2.Focus();
                coutXtextBox2.SelectAll();
                return; //выходим из текущего метода
            }
            if (lenXN <= 4)
            {
                if (lenXN < 0)
                {
                    MessageBox.Show("Как вы себе представляете отрицательную длинну? Исправляйте", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    coutXtextBox2.Focus();
                    coutXtextBox2.SelectAll();
                    return;
                }
                MessageBox.Show("Длинна массива переменных должна быть >= 5, в противном случае в этой программе нет смысла, а посему делайте свое уравнение сами", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                coutXtextBox2.Focus();
                coutXtextBox2.SelectAll();
                return;
            }
            if ((int.TryParse(textBox1_Cattegor.Text, out count_cats) == false) && count_cats >= 0)
            {
                MessageBox.Show("Введете пожалуйста в поле количества категорий целое положительное число", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                textBox1_Cattegor.Focus();
                textBox1_Cattegor.SelectAll();
                return; //выходим из текущего метода
            }
            if ((int.TryParse(textBox2_cat_cat_count.Text, out count_catcount) == false) && count_catcount >=0)
            {
                MessageBox.Show("Введете пожалуйста в поле среднее количество значений в категориях целое положительное число", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                textBox2_cat_cat_count.Focus();
                textBox2_cat_cat_count.SelectAll();
                return; //выходим из текущего метода
            }
            if (count_cats <= lenXN / 2)
            {
                MessageBox.Show("Введете пожалуйста в поле количества категорий целое положительное число, которое меньше половины от количества переменных ("+ (lenXN/2) +")", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                textBox1_Cattegor.Focus();
                textBox1_Cattegor.SelectAll();
                return; //выходим из текущего метода
            }
        }

        Thread thr;

        private void button1_Click(object sender, EventArgs e)
        {
            int lenXN = -1, min_X = -1, max_X = -1, count_cats = -1, count_catcount = -1; getLenMinMaxValue(ref lenXN, ref min_X, ref max_X, ref count_cats, ref count_catcount);
            if (lenXN <= 4 || min_X == -1 || max_X == -1 || count_cats == -1 || count_catcount == -1)
                return;
            toolStripProgressBar1.Value = 0;
            richTextBox1.Clear();
            thr = new Thread(delegate()
            {
                Invoke(new AddMessageDelegate(ExcelentRes), new object[] { "Расчет выполняется" });
                float[] arrayxxxKoef = getKoeftoIArray(lenXN, min_X, max_X); //формируем массив коэф.

                Invoke(new AddMessageDelegate(LogAdd), new object[] { "15" });

                String[] arrayxxxXN = new String[lenXN]; //создаем массив иксов

                Parallel.For(0, lenXN, (i, state) => { arrayxxxXN[i] = "X" + (1+i); }); //заполняем массив исков
                
                Invoke(new AddMessageDelegate(LogAdd), new object[] { "30" });

                skob_begin_end[] arrayskob = getSkobArray(lenXN); //объединяем массив коэф. и исков с массивом скобок

                Invoke(new AddMessageDelegate(LogAdd), new object[] { "45" });

                stepen_koef_or_x[] arraystep = getStepArray(lenXN);

                Invoke(new AddMessageDelegate(LogAdd), new object[] { "50" });

                Invoke(new PrintRes(getFinishArray), new object[] { arrayxxxXN, arrayxxxKoef, arrayskob, arraystep, lenXN });

                Invoke(new AddMessageDelegate(LogAdd), new object[] { "100" });

                Invoke(new AddMessageDelegate(ExcelentRes), new object[] { "Расчет завершен. Уравнение готово." });
            });
            thr.Priority = ThreadPriority.Highest;
            thr.Name = "Res mass forming";
            thr.Start();

            //теперь у нас есть массив переменных и коэф. к ним. Теперь надо подставить знаки и скобки
        }

        private void getFinishArray(string[] arrayxxxXN, float[] arrayxxxKoef, skob_begin_end[] arrayskob, stepen_koef_or_x[] arraystep, int lenXN)
        {
            richTextBox1.Clear();
            for (int i = 0; i < lenXN; i++)
            {
                String s = arrayxxxXN[i] + "\t";
                /*s += arrayxxxKoef[i] +"\t";
                if (arrayskob.Length > i)
                    s += arrayskob[i].start + "\t" + arrayskob[i].end + "\t";
                if (arraystep.Length > i)
                    s += arraystep[i].index + "\t" + arraystep[i].koef_or_x; */
                s += "\n";
                richTextBox1.Text += s;
            }
            List<String> lineslist = new List<string>();
            int cati = 0, maxcat = 5; Random r = new Random();
            lineslist.Add("<variables>");
            for (int i = 0; i < lenXN; i++)
            {
                if (cati <= maxcat && r.Next(-40, 100) < -1)
                {
                    int n = r.Next(3, 5);
                    cati++;
                    lineslist.Add("	<category>");
                    lineslist.Add("		<name>" + arrayxxxXN[i] + "</name>");
                    for (int j = 0; j < n;j++ )
                    {
                        lineslist.Add("		<value>Val" + (j + 1) + "</value>");
                    }
                    lineslist.Add("	</category>");
                }
                else
                {
                    lineslist.Add("	<number>" + arrayxxxXN[i] + "</number>");
                }
            }
            lineslist.Add("</variables>");
            string[] lines = new string[lineslist.Count];
            for (int i = 0; i < lines.Length;i++ )
            {
                lines[i] = lineslist[i];
            }
            System.IO.File.WriteAllLines(@"C:\Users\aleks\Desktop\WriteLines.xml", lines);
        }


        private stepen_koef_or_x[] getStepArray(int lenXN)
        {
 	        int lenXN10 = (int)((double)lenXN * 0.05);
            if (lenXN10 == 0) 
                return null;
            stepen_koef_or_x[] arraystep = new stepen_koef_or_x[lenXN10];

            for (int i=0;i<arraystep.Length;i++)
            {
                int r;
                bool ifi;
                do 
                {
                    r = random.Next(0,lenXN-1);
                    ifi = false;
                    for (int j=0;j<i-1;j++)
                    {
                        if (arraystep[j].index == r)
                        {
                            ifi = true;
                            break;
                        }
                    }
                } while(ifi);
                arraystep[i].index = r;
                if (random.Next(-100,100) > 0)
                    arraystep[i].koef_or_x = true;
                else
                    arraystep[i].koef_or_x = false;
            }

            return arraystep;
        }

        public delegate void AddMessageDelegate(string message);
        public delegate void PrintRes(string[] arrayxxxXN, float[] arrayxxxKoef, skob_begin_end[] arrayskob, stepen_koef_or_x[] arraystep, int lenXN);

        public void ExcelentRes(string message)
        {
            toolStripLabel1.Text = message;
        }

        public void LogAdd(string message)
        {
            toolStripProgressBar1.Value = int.Parse(message);
        }

        public void ResAddmass(string message)
        {
            richTextBox1.Text += message; //"Y = ";
        }

        /// <summary>
        /// получение массива скобок
        /// </summary>
        /// <param name="mass_skob">передаем указатель на пустой массив строк</param>
        /// <param name="lenXN">указатель на длинну массива переменных</param>
        private skob_begin_end[] getSkobArray(int lenXN)
        {
            int lenXN10 = (int)((double)lenXN * 0.05);
            if (lenXN10 == 0) 
                return null;
            skob_begin_end[] mass_skob = new skob_begin_end[lenXN10];
            mass_skob[0].start = random.Next(0, lenXN - 3);
            mass_skob[0].end = random.Next(mass_skob[0].start + 1, lenXN - 1);
            skob_begin_end sbe;
            random = new Random();
            for (int i = 1; i < lenXN10; i++)
            {
            restart:
                sbe.start = random.Next(0, lenXN - 3);
                sbe.end = random.Next(sbe.start + 1, lenXN - 1);
                for (int j = 0; j < i - 1; j++)
                {
                    if (mass_skob[j].start == sbe.start && mass_skob[j].end == sbe.end)
                        goto restart;
                }
                mass_skob[i] = sbe;
            }
            return mass_skob;
        }

        /// <summary>
        /// структура для того, чтобы можно было удобно оперировать индексами скобок
        /// </summary>
        public struct skob_begin_end
        {
            public int start;
            public int end;
        };

        /// <summary>
        /// структура для массива степеней, в роли степени может быть в результате коэф или переменная
        /// </summary>
        public struct stepen_koef_or_x
        {
            public int index;
            public bool koef_or_x;
        };

        /// <summary>
        /// генератор массива коэффициентов и неизвестных переменных уравнения
        /// </summary>
        /// <param name="arrayxxxXN"></param>
        private float[] getKoeftoIArray(int lenXN, int X_min, int X_max)
        {
            float[] arrayxxxKoef = new float[lenXN];
            Parallel.For(0, arrayxxxKoef.Length, (i, state) =>
            {
                float Koef = -99999999999;

                if (random.Next(-10, 100) > 0) //выбираем генерировать целое число или дробь
                    while (true)
                    {
                        int k1 = random.Next(X_min, X_max);
                        if (k1 != 0)
                            Koef = k1;
                        break;
                    }
                else
                    while (true)
                    {
                    restartk1k2:
                        int k1, k2, t = 0;
                        do
                        {
                            k2 = random.Next(X_min, X_max);
                        } while (k2 == 0);
                        do
                        {
                            k1 = random.Next(Math.Abs(k2) * (-1) + 1, Math.Abs(k2) - 1);
                            t++;
                            if (t > 20) goto restartk1k2;
                        } while (k1 == 0);

                        Koef = (float)k1 / (float)k2;
                        break;
                    }
                arrayxxxKoef[i] = Koef;
            });
            return arrayxxxKoef;
        }

        private void button2Stop_Click(object sender, EventArgs e)
        {
            if (thr.IsAlive)
                thr.Abort();
            toolStripProgressBar1.Value = 0;
            richTextBox1.Clear();
            toolStripLabel1.Text = "Расчет прерван";
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (e.Button == MouseButtons.Right)
            {
                node_sub = true;
                contextMenuStrip_Категории.Show(e.X,e.Y);
            }*/
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            /*if (e.Button == MouseButtons.Right && e.Button == MouseButtons.Right && !contextMenuStrip_Категории.Visible)
            {
                contextMenuStrip_Категории.Items.Clear();
                contextMenuStrip_Категории.Items.Add("Добавить категорию");
                contextMenuStrip_Категории.Show(Cursor.Position.X + 3, Cursor.Position.Y);
            }*/
        }

        private void contextMenuStrip_Категории_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Text)
                {
                    case "Добавить категорию":
                        treeView1.Nodes.Add("New Node");
                        break;
                    case "Удалить категорию":
                        treeView1.Nodes.Remove(treeView1.SelectedNode);
                        break;
                    case "Добавить значение категории":
                        treeView1.Nodes[treeView1.SelectedNode.Index].Nodes.Add("New Value_" + (treeView1.SelectedNode.Index + 1) + (1 + treeView1.SelectedNode.Nodes.Count));
                        if (!treeView1.SelectedNode.IsExpanded)
                            treeView1.SelectedNode.Expand();
                        break;
                    case "Удалить значение категории":
                        treeView1.Nodes.Remove(treeView1.SelectedNode);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            contextMenuStrip_Категории.Items.Clear();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    contextMenuStrip_Категории.Show(Cursor.Position.X + 3, Cursor.Position.Y);
                    treeView1.SelectedNode = e.Node;
                    switch (e.Node.Level)
                    {
                        case 0:
                            contextMenuStrip_Категории.Items.Clear();
                            contextMenuStrip_Категории.Items.Add("Удалить категорию");
                            contextMenuStrip_Категории.Items.Add("Добавить значение категории");
                            break;
                        case 1:
                            contextMenuStrip_Категории.Items.Clear();
                            contextMenuStrip_Категории.Items.Add("Удалить значение категории");
                            break;
                    }
                    break;
            }
        }

        private void button2_addnode_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("New Node_" + (treeView1.Nodes.Count + 1));
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                if (treeView1.Nodes[i].Text == e.Label && i != e.Node.Index)
                {
                    e.CancelEdit = true;
                    MessageBox.Show("Категория с таким названием уже есть", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
        }
    }
}
