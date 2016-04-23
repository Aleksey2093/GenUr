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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            random = new Random();
        }

        /// <summary>
        /// единый рандом на весь класс
        /// </summary>
        Random random;

        private void button1_Click(object sender, EventArgs e)
        {
            int lenXN, min_X, max_X;
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
            if (int.TryParse(coutXtextBox2.Text, out lenXN) == false)
            {
                MessageBox.Show("Введете пожалуйста в поле количества иксов целое положительное число", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                coutXtextBox2.Focus();
                coutXtextBox2.SelectAll();
                return; //выходим из текущего метода
            }
            if (lenXN < 0)
            {
                MessageBox.Show("Как вы себе представляете отрицательную длинну? Исправляйте", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                coutXtextBox2.Focus();
                coutXtextBox2.SelectAll();
                return;
            }
            if (lenXN <= 4)
            {
                MessageBox.Show("Длинна массива переменных должна быть >= 5, в противном случае в этой программе нет смысла, а посему делайте свое уравнение сами", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                coutXtextBox2.Focus();
                coutXtextBox2.SelectAll();
                return;
            }
            toolStripProgressBar1.Value = 0;
            ResulttextBox1.Clear();
            thr = new Thread(delegate()
            {
                Invoke(new AddMessageDelegate(ExcelentRes), new object[] { "Расчет выполняется" });
                String[] arrayxxxXN = getXNtoIArray(lenXN, min_X, max_X); //формируем массив Xв для выдачи на экран
                Invoke(new AddMessageDelegate(LogAdd), new object[] { "30" });
                arrayxxxXN = getPrewFinishArray(arrayxxxXN, ref lenXN); //объединяем массив коэф. и исков с массивом скобок
                Invoke(new AddMessageDelegate(LogAdd), new object[] { "60" });
                Invoke(new PrintRes(getFinishArray), new object[] {arrayxxxXN, lenXN});
                Invoke(new AddMessageDelegate(ExcelentRes), new object[] { "Расчет завершен. Уравнение готво." });
            });
            thr.Priority = ThreadPriority.Highest;
            thr.Name = "Res mass forming";
            thr.Start();

            //теперь у нас есть массив переменных и коэф. к ним. Теперь надо подставить знаки и скобки
        }

        Thread thr;

        public delegate void AddMessageDelegate(string message);
        public delegate void PrintRes(String[] array, int len);

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
            ResulttextBox1.Text += message; //"Y = ";
        }

        /// <summary>
        /// получение результата и вывод на экран
        /// </summary>
        /// <param name="arrayxxxXN">указатель на массив</param>
        /// <param name="lenXN">указатель на длину массива</param>
        private void getFinishArray(String[] arrayxxxXN, int lenXN)
        {
            //ResulttextBox1.Text = "Y = ";
            Invoke(new AddMessageDelegate(ResAddmass), new object[] { "Y = " });
            int i, len = lenXN - 1;
            for (i=0;i<len;i++)
            {
                Invoke(new AddMessageDelegate(LogAdd), new object[] { ((int)(60 + i * 40 / len)).ToString() });
                int ifi = random.Next(-5, 100);
                if (ifi > 0)
                    ResulttextBox1.Text += arrayxxxXN[i] + " * ";
                else
                    ResulttextBox1.Text += arrayxxxXN[i] + " * ";
            }
            ResulttextBox1.Text += arrayxxxXN[len];
            Invoke(new AddMessageDelegate(LogAdd), new object[] { "100" });
        }
        /// <summary>
        /// получение массива готовых переменных
        /// </summary>
        /// <param name="arrayxxxXN">указатель на массив</param>
        /// <param name="lenXN">указатель на длинну массива</param>
        private String[] getPrewFinishArray(String[] arrayxxxXN, ref int lenXN)
        {
            skob_begin_end[] mass_skob = /*new skob_begin_end[lenXN10];*/ getSkobArray(lenXN);
            //for (int i=0;i<mass_skob.Length;i++)
            Parallel.For(0, mass_skob.Length, (i, state) =>
            {
                arrayxxxXN[mass_skob[i].start] = "(" + arrayxxxXN[mass_skob[i].start];
                arrayxxxXN[mass_skob[i].end] = arrayxxxXN[mass_skob[i].end] + ")";
            });
            return arrayxxxXN;
        }

        /// <summary>
        /// получение массива скобок
        /// </summary>
        /// <param name="mass_skob">передаем указатель на пустой массив строк</param>
        /// <param name="lenXN">указатель на длинну массива переменных</param>
        private skob_begin_end[] getSkobArray(int lenXN)
        {
            int lenXN10 = (int)((double)lenXN * 0.1);
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
            }//);
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
        /// генератор массива коэффициентов и неизвестных переменных уравнения
        /// </summary>
        /// <param name="arrayxxxXN"></param>
        private String[] getXNtoIArray(int lenXN, int X_min, int X_max)
        {
            String[] arrayxxxXN = new String[lenXN];
            //for (int i = 0; i < arrayxxxXN.Length; i++) //формируем массив Xв для выдачи на экран
            Parallel.For(0, arrayxxxXN.Length, (i, state) =>
            {
                String X = "X_" + i.ToString();
                String Koef = "";
                bool skob = false;

                if (random.Next(-10, 100) > 0) //выбираем генерировать целое число или дробь
                    while (true)
                    {
                        int k1 = random.Next(X_min, X_max);
                        if (k1 != 0)
                            Koef = k1.ToString();
                        if (k1 < 0)
                            skob = true;
                        break;
                    }
                else
                    while (true)
                    {
                        int k1, k2;
                        do
                        {
                            k2 = random.Next(X_min, X_max);
                        } while (k2 == 0);
                        do
                        {
                            k1 = random.Next(Math.Abs(k2) * (-1) + 1, Math.Abs(k2) - 1);
                        } while (k1 == 0);
                        
                        if ((k1 <= 0 && k2 >= 0) || (k1 >= 0 && k2 <= 0))
                        {
                            skob = true;
                            Koef = "";
                        }
                        else
                            Koef = "-";
                        if (k1 <= 0) k1 *= -1;
                        if (k2 <= 0) k2 *= -1;
                        Koef += k1 + "/" + k2;
                        break;
                    }
                bool k_x;
                if (random.Next(-10, 100) >= 0)
                    k_x = true; //значит будем умножать
                else
                    k_x = false; //будем делить

                if (Koef == "")
                    arrayxxxXN[i] = X;
                else if (!skob) //отрицательный коэф
                {
                    if (k_x)
                        arrayxxxXN[i] = (Koef + "*" + X);
                    else
                        arrayxxxXN[i] = (Koef + "/" + X);
                }
                else //положительный коэф
                {
                    if (k_x)
                        arrayxxxXN[i] = ("(" + Koef + "*" + X + ")");
                    else
                        arrayxxxXN[i] = ("(" + Koef + "/" + X + ")");
                }
            });
            
            for (int i = 0; i < random.Next(1,lenXN / 2); i++)
            {
                int index = random.Next(0, lenXN - 1);
                int r = random.Next(-3, 3);
                if (r < 0)
                    arrayxxxXN[index] += "^(" + r + ")";
                else arrayxxxXN[index] += "^" + r;
            }
            return arrayxxxXN;
        }

        private void button2Stop_Click(object sender, EventArgs e)
        {
            if (thr.IsAlive)
                thr.Abort();
            toolStripProgressBar1.Value = 0;
            ResulttextBox1.Clear();
            toolStripLabel1.Text = "Расчет прерван";
        }
    }
}
