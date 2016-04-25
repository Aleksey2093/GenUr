using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите команду от 0 до 3. 0 - выход\n");
                int con = -99;
                try
                {
                    con = int.Parse(Console.In.ReadLine());
                }
                catch(Exception) 
                {
                    Console.WriteLine("Некорретный ввод.");
                }
                if (con == 1)
                {
                    getXMLdesktop();
                }
                else if (con == 2)
                {
                    getListVar();
                }
                else if (con == 3)
                {
                    getTestFile();
                }
                else if (con == 0)
                {
                    goto exit;
                }
            }
        exit: ;
        }

        public struct cattigor
        {
            public string name;
            public int parse;
            public List<String> value;
            public List<int> zn_value;
        };

        private static void getTestFile()
        {
            //double Y = 0;
            List<cattigor> qqcatigoriyas = new List<cattigor>();
            Random random = new Random();

            XmlDocument doc = new XmlDocument();
            doc.Load("XMLFile1.xml");
            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "category")
                {
                    int count = 0;
                    cattigor item = new cattigor();
                    item.name = node.ChildNodes[0].InnerText;
                    item.value = new List<string>(1);
                    item.zn_value = new List<int>(1);
                    for (int i = 1; i < node.ChildNodes.Count; i++)
                    {
                        item.value.Add(node.ChildNodes[i].InnerText);
                        item.zn_value.Add(-1);
                        count++;
                    }
                    String tmp = item.name.Replace("X", "");
                    item.parse = int.Parse(tmp);
                    qqcatigoriyas.Add(item);
                }
            }
            int catразмермаксимальный = 0;
            for (int i = 0; i < qqcatigoriyas.Count; i++)
                if (catразмермаксимальный < qqcatigoriyas[i].zn_value.Count)
                    catразмермаксимальный = qqcatigoriyas[i].zn_value.Count;
            catразмермаксимальный += 1;
            int nlist = 107;

            String line = null;
            int kolvotest = 300000, scet = 0;
            //String[] lines = new String[kolvotest];
            List<String> lines = new List<string>();
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

            //FileStream file_lean_csv = File.Open(@"C:\Users\aleks\Desktop\lean.csv", System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Write);
            //System.IO.FileStream file_excel_lean = System.IO.File.OpenWrite(@"C:\Users\aleks\Desktop\lean.csv");

            /*StreamWriter file_lean_csv = new StreamWriter("lean.csv");
            StreamWriter file_excel_lean = new StreamWriter("lean_excel.csv");*/
            
            int st=0, en=kolvotest;
            /*while(true)*/
            {
                /*if (st+100000 < kolvotest-100)
                {
                    en = st + 100000;
                } 
                else if (st < kolvotest)
                {
                    en = kolvotest;
                }
                else
                {
                    break;
                }*/
                /*Parallel.For(st, en, (i, state) =>*/
                /*Parallel.For(0, kolvotest, (i, state) =>*/
                for (int i = 0; i < kolvotest; i++)
                {
                    double[,] X = new double[nlist, catразмермаксимальный];
                    double Y = getTestFileNumberllll(i + 3, ref X, ref nlist, ref qqcatigoriyas);
                    line = null; int kiti = 0;
                    for (int j = 1; j < nlist; j++)
                    {
                        if (X[j, 1] != -1)
                        {
                            int index = -50;
                            for (int k = 1; k < catразмермаксимальный; k++)
                                if (X[j, k] == 1)
                                {
                                    index = k - 1;
                                    break;
                                }
                            int aaqew = (int)X[j, 0];
                            line += qqcatigoriyas[aaqew].value[index] + ","; kiti++;
                        }
                        else
                        {
                            line += Math.Round(X[j, 0], 2).ToString(cultureinfo) + ",";
                        }
                    }
                    line += Math.Round(Y, 5).ToString(cultureinfo) + "";
                    //Console.WriteLine(line+"\n\n\n");
                    //lines[i] = line;
                    lines.Add(line);
                    /*if (lines.Count > 100000)
                    {
                        thrfile = new Thread(delegate()
                        {
                            for (int j = 0; j < lines.Count; j++)
                            {
                                file_lean_csv.WriteLine(lines[j] + "\n");
                                lines.RemoveAt(j);
                            }
                        });
                        thrfile.Start();
                    }*/

                    scet++;
                    if (scet % 100000 == 0)
                        Console.WriteLine("сейчас " + scet + ", осталось " + (kolvotest - scet) + ", всего " + kolvotest + ";");
                }//);

               /* for (int i = 0; i < kolvotest; )
                {
                    file_lean_csv.WriteLine(line);
                    file_excel_lean.WriteLine(line.Replace(",", ";"));
                }*/
                System.IO.File.WriteAllLines(@"C:\Users\aleks\Desktop\lean.csv", lines, Encoding.UTF8);
                Parallel.For(0, kolvotest, (i, state) =>
                {
                    lines[i] = lines[i].Replace(",", ";");
                });
                System.IO.File.WriteAllLines(@"C:\Users\aleks\Desktop\lean_excel.csv", lines, Encoding.UTF8);
                st += 100000;
            }
        }



        private static double getTestFileNumberllll(int rand, ref double[,] X, ref int nlist, ref List<cattigor> qqcatigoriyas)
        {
            int randf = rand;
            if (randf > 100)
                randf = 100;
            Random random = new Random(rand);
            int catразмермаксимальный = 0;
            for (int i = 0; i < qqcatigoriyas.Count; i++)
                if (catразмермаксимальный < qqcatigoriyas[i].zn_value.Count)
                    catразмермаксимальный = qqcatigoriyas[i].zn_value.Count;
            catразмермаксимальный+=1;
            for (int i = 1; i < nlist; i++)
                {
                    X[i, 0] = random.Next(1, randf);
                    for (int j = 1; j < catразмермаксимальный; j++)
                        X[i, j] = -1;
                } //отрандомли все включая категории, потом будем переделывать
            for (int i=0;i<qqcatigoriyas.Count;i++)
            {
                int index = qqcatigoriyas[i].parse;
                int what = random.Next(0, qqcatigoriyas[i].zn_value.Count);
                X[index, 0] = i;
                for (int j=0;j<qqcatigoriyas[i].zn_value.Count;j++)
                {
                    if (what == j)
                    {
                        qqcatigoriyas[i].zn_value[j] = 1;
                        X[index, 1 + j] = 1;
                    }
                    else
                    {
                        qqcatigoriyas[i].zn_value[j] = 0;
                        X[index, 1 + j] = 0;
                    }
                }
            }
            double rrr;
            for (int i = 1; i < nlist; i++)
            {
                while (true)
                {
                    rrr = random.NextDouble();
                    if (rrr <= 0.9)
                        break;
                }
                X[i, 0] += rrr;
            }


            /*
             * -------------------------
             * в Y положить свою формулу
             * -------------------------
             */
            double Y = 1;
            random = new Random(DateTime.Now.Millisecond);
            //Console.WriteLine("!!!!!!!!!!!\t"+ rand +"\tY = " + Y);
            while (true)
            {
                rrr = random.NextDouble();
                if (rrr <= 0.9)
                    break;
            }
            Y += rrr;
            //Console.WriteLine(":::::::::::\t" + rand + "\tY = " + Y);
            return Y;
        }

        /// <summary>
        /// базовая функция по нахождению матрицы значений
        /// </summary>
        static void getListVar()
        {
            String line = "";
            List<XlistiXXX> listX = new List<XlistiXXX>();
            XmlDocument doc = new XmlDocument();
            doc.Load("XMLFile1.xml");
            int ind = 1;
            foreach (XmlNode node in doc.DocumentElement)
            {
                XlistiXXX lx = new XlistiXXX();
                lx.line2 = new List<ListingXVars>();
                lx.line3 = new List<ListingXVars>();
                if (node.Name == "number")
                {
                    lx.line1 = node.InnerText + "";
                    lx.categor_1 = ind; ind++;
                    lx.c_bool_1 = false;
                    listX.Add(lx);
                    line += lx.line1 + "\t";
                }
                else if (node.Name == "category")
                {
                    for (int i = 1; i < node.ChildNodes.Count; i++)
                    {
                        lx.line1 = node["name"].InnerText + "_" + i + "";
                        lx.categor_1 = ind;
                        lx.c_bool_1 = true;
                        listX.Add(lx);
                        line += lx.line1 + "\t";
                    }
                    ind++;
                }
            }
            //на этом этапе у нас есть первая линия значений отправляем ее в файл
            System.IO.File.WriteAllText(@"C:\Users\aleks\Desktop\line1.txt", line, Encoding.UTF8);

            line = null;
            //делаем вторую степень возможно сразу и третью по глубине массива будет видно
            for (int i = 0; i < listX.Count; i++)
            {
                var c = listX[i].categor_1;
                var b = listX[i].c_bool_1;
                var tlist = listX[i].line1;
                Random random = new Random(i);
                for (int j = 0; j < listX.Count; j++)
                {
                    var tmpc = listX[j].categor_1;
                    var tmpb = listX[j].c_bool_1;
                    var tmptlist = listX[j].line1;
                    if ((c != tmpc && b == true) /*если категория разные и это категория*/
                        || (b == false))
                    {
                        ListingXVars lxvar = new ListingXVars();
                        lxvar.categor_ind = new List<int>();
                        lxvar.categor_ind.Add(c);
                        lxvar.categor_ind.Add(tmpc);
                        lxvar.varik = tmptlist + "*" + tlist;
                        listX[j].line2.Add(lxvar);
                        line += tmptlist + "*" + tlist + "\t";
                    }
                }
                line += "\n";
            }
            System.IO.File.WriteAllText(@"C:\Users\aleks\Desktop\line2.txt", line, Encoding.UTF8);
            //сделана и распечатана вторая степень

            line = null;
            //приступаем к третьей степени
            for (int i = 0; i < listX.Count; i++)
            {
                for (int j = 0; j < listX[i].line2.Count; j++)
                {
                    /*на этом этапе мне нужно убедиться что две категории в числе которое я использую (listX[i].line2[j]) 
                     * не соответствуют категории из первой линии (listX[i])*/
                    if (!listX[i].c_bool_1) /*рассмотрим случай если первое не категория, а значит все нормально*/
                    {
                        ListingXVars lxvar = new ListingXVars();
                        lxvar.categor_ind = new List<int>();
                        lxvar.categor_ind.Add(listX[i].categor_1); //не знаю зачем запомнимать категории нанаверное на случай если третья степень появится
                        lxvar.categor_ind.Add(listX[i].line2[j].categor_ind[0]);
                        lxvar.categor_ind.Add(listX[i].line2[j].categor_ind[1]);
                        lxvar.varik = listX[i].line1 + "*" + listX[i].line2[j].varik;
                        listX[i].line3.Add(lxvar);
                        line += lxvar.varik + "\t";
                    }
                    else /*если у нас не категория то нужно убедиться что не появятся повторы при слиянии второго и первого уровня*/
                    {
                        for (int k = 0; k < listX[i].line2[j].categor_ind.Count; k++)
                            if (listX[i].line2[j].categor_ind[k] == listX[i].categor_1)
                                goto nooooo;
                        ListingXVars lxvar = new ListingXVars();
                        lxvar.categor_ind = new List<int>();
                        lxvar.categor_ind.Add(listX[i].categor_1); //не знаю зачем запомнимать категории нанаверное на случай если третья степень появится
                        lxvar.categor_ind.Add(listX[i].line2[j].categor_ind[0]);
                        lxvar.categor_ind.Add(listX[i].line2[j].categor_ind[1]);
                        lxvar.varik = listX[i].line1 + "*" + listX[i].line2[j].varik;
                        listX[i].line3.Add(lxvar);
                        line += lxvar.varik + "\t";
                    }
                nooooo: ;
                }
                line += "\n";
            }
            System.IO.File.WriteAllText(@"C:\Users\aleks\Desktop\line3.txt", line, Encoding.UTF8);
            line = null;
            // = new String[listX[0].line3.Count+listX[0].line2.Count+1];
            List<String> listlines = new List<string>(); int maxlen = 0; //bool fir = false;

            for (int i = 0; i < listX.Count;i++)
            {
                int ij = 1;
                for (int j = 0; j < listX[i].line2.Count; j++, ij++)
                {

                }
                for (int j = 0; j < listX[i].line3.Count; j++, ij++)
                {

                }
                if (maxlen < ij)
                    maxlen = ij;
            }

            String[,] mat = new string[listX.Count, maxlen];

            for (int i = listX.Count-2; i >= 0; i--)
            {
                try
                {
                    listlines[0] = listX[i].line1 + ";" + listlines[0];
                }
                catch
                {
                    listlines.Add(listX[i].line1 + ";");
                }
                mat[i,0] = listX[i].line1;
                int ij = 1;
                for (int j = 0; j < listX[i].line2.Count; j++, ij++)
                {
                    try
                    {
                        listlines[ij] = listX[i].line2[j].varik + ";" + listlines[ij];
                    }
                    catch
                    {
                        listlines.Add(listX[i].line2[j].varik + ";");
                    }
                    mat[i, ij] = listX[i].line2[j].varik;
                }
                for (int j = 0; j < listX[i].line3.Count; j++, ij++)
                {
                    try
                    {
                        listlines[ij] = listX[i].line3[j].varik + ";" + listlines[ij];
                    }
                    catch
                    {
                        listlines.Add(listX[i].line3[j].varik + ";");
                    }
                    mat[i, ij] = listX[i].line3[j].varik;
                }
                for (int j=ij;j<maxlen;j++)
                {
                    try
                    {
                        listlines[j] = ";" + listlines[j];
                    }
                    catch
                    {
                        listlines.Add(";");// = ";" + listlines[j];
                    }
                    mat[i, ij] = null;
                }
            }
            System.IO.File.WriteAllLines(@"C:\Users\aleks\Desktop\line_all.csv", listlines, Encoding.UTF8);
            line = null;
            for (int i = 0; i < listX.Count-1; i++)
            {
                String tmp = null;
                Random r = new Random(i);
                int maxii = 0;
                for (int j = 0; j < maxlen; j++, maxii++)
                {
                    if (mat[i,j] == null)
                        break;
                }
                while (tmp == null)
                {
                    int rannn = r.Next(0, maxii);
                    tmp = mat[i, rannn];
                }
                line += tmp + " + ";
            }
            System.IO.File.WriteAllText(@"C:\Users\aleks\Desktop\line_all_res.txt", line, Encoding.UTF8);
        }

        /// <summary>
        /// первая строка матрицы
        /// </summary>
        public struct XlistiXXX
        {
            public string line1; //первая линия значений
            public int categor_1; //тип первой линии
            public bool c_bool_1; //категория или нет
            public List<ListingXVars> line2; //измерении второй степени
            public List<ListingXVars> line3; //измерение третьей степени
        };

        /// <summary>
        /// вторая строка матрицы
        /// </summary>
        public struct ListingXVars
        {
            public string varik; //вариант
            public List<int> categor_ind; //массив для варианта
        };

        /// <summary>
        /// рандомная генерация файла описания
        /// </summary>
        static void getXMLdesktop()
        {
            List<String> lineslist = new List<string>();
            int cati = 0, maxcat = 5, lenXN = 111; Random r = new Random();
            lineslist.Add("<variables>");
            for (int i = 0; i < lenXN; i++)
            {
                if (cati <= maxcat && r.Next(-40, 100) < -1)
                {
                    int n = r.Next(3, 5);
                    cati++;
                    lineslist.Add("	<category>");
                    lineslist.Add("		<name>X" + (i + 1) + "</name>");
                    for (int j = 0; j < n; j++)
                    {
                        lineslist.Add("		<value>Val" + (j + 1) + "</value>");
                    }
                    lineslist.Add("	</category>");
                }
                else
                {
                    lineslist.Add("	<number>X" + (i + 1) + "</number>");
                }
            }
            lineslist.Add("</variables>");
            string[] lines = new string[lineslist.Count];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lineslist[i];
            }
            System.IO.File.WriteAllLines(@"C:\Users\aleks\Desktop\WriteLines.xml", lines);
        }
    }
}
