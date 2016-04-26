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

        static String directoriya_to_file = "";//@"C:\Users\aleks\Desktop\";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("1-собрать XML файл, 2-сгенерировать файлы случайных комбинаций, 3 - сгенерировать тесты или данные для обучения." 
                + " Перед запуском проверить код на корректность выставленных значений. Введите команду от 0 до 3. 0 - выход\n");
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
                else if (con == 4)
                {
                    строковыйАнализФайла();
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
            String line = null; bool test_or_learn;
            int nlist = 107, kolvotest = 300000, scet = 0;

            Console.WriteLine("-------------------------------");
            {
                Console.WriteLine("Введите количество переменных: ");
            rколичествопеременных:
                try
                {
                    nlist = int.Parse(Console.ReadLine());
                    nlist++;
                }
                catch (Exception)
                {
                    Console.WriteLine("Неправельный ввод. Введите количество переменных еще раз:");
                    goto rколичествопеременных;
                }
            }
            Console.WriteLine("-------------------------------");
            {
                Console.WriteLine("Введите количество строчек вариаций: ");
            rколичествопеременных:
                try
                {
                    kolvotest = int.Parse(Console.ReadLine());
                    if (kolvotest >= 600000)
                        goto rколичествопеременных;
                }
                catch (Exception)
                {
                    Console.WriteLine("Неправельный ввод. Введите количество строк вариаций еще раз:");
                    goto rколичествопеременных;
                }
            }
            Console.WriteLine("-------------------------------");
            {
                Console.WriteLine("Добавить или не добавлять шум (true/false): ");
            rколичествопеременных:
                try
                {
                    test_or_learn = Boolean.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Неправельный ввод. Введите количество строк вариаций еще раз:");
                    goto rколичествопеременных;
                }
            }

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

            StreamWriter file_lean_csv = null;
            StreamWriter file_excel_lean = null;
            StreamWriter file_test_csv = null;
            StreamWriter file_excel_test = null;

            if (test_or_learn)
            {
                file_lean_csv = new StreamWriter("learn.csv");
                file_excel_lean = new StreamWriter("learn_excel.csv");
            }
            else
            {
                file_test_csv = new StreamWriter("test.csv");
                file_excel_test = new StreamWriter("test_excel.csv");
            }

            int st = 0, en = kolvotest;
            //while(true)
            {
                /*if (st+300000 < kolvotest-100)
                {
                    en = st + 300000;
                } 
                else if (st < kolvotest)
                {
                    en = kolvotest;
                }
                else
                {
                    break;
                }*/
                String[] lines = new String[en - st];
                /*Parallel.For(st, en, (i, state) =>*/
                for (int i = st; i < en; i++)
                {
                    double[,] X = new double[nlist, catразмермаксимальный];
                    double Y = getTestFileNumberllll(i + 10, ref X, ref nlist, ref qqcatigoriyas, ref test_or_learn, ref catразмермаксимальный);
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
                    lines[i - st] = line;
                    if (line == null || lines[i - st] == null)
                    {

                    }
                    scet++;
                    if (scet % 100000 == 0)
                        Console.WriteLine("сейчас " + scet + ", осталось " + (kolvotest - scet) + ", всего " + kolvotest + ";");
                }//);

                String tmpp;// = lines[i].Replace(",", ";");
                if (test_or_learn)
                    for (int i = 0; i < lines.Length; i++)
                    {
                        tmpp = lines[i];
                        //if (tmpp != null)
                        {
                            file_lean_csv.WriteLine(tmpp);
                            tmpp = lines[i].Replace(",", ";");
                            file_excel_lean.WriteLine(tmpp);
                        }
                    }
                else
                    for (int i = 0; i < lines.Length; i++)
                    {
                        tmpp = lines[i];
                        //if (tmpp != null)
                        {
                            file_test_csv.WriteLine(tmpp);
                            tmpp = lines[i].Replace(",", ";");
                            file_excel_test.WriteLine(tmpp);
                        }
                    }
                /*String filenamest;
                if (!test_or_learn)
                    filenamest = "test";
                else
                    filenamest = "learn";
                System.IO.File.WriteAllLines(directoriya_to_file + filenamest +".csv", lines, Encoding.UTF8);
                Parallel.For(0, kolvotest, (i, state) =>
                {
                    String tmp = lines[i].Replace(",", ";");
                    lines[i] = tmp;
                });
                System.IO.File.WriteAllLines(directoriya_to_file + filenamest +"_excel.csv", lines, Encoding.UTF8);*/
                st += 300000;
            }
        exit: ;
            if (test_or_learn)
            {
                file_excel_lean.Close();
                file_lean_csv.Close();
            }
            else
            {
                file_excel_test.Close();
                file_test_csv.Close();
            }
        }



        private static double getTestFileNumberllll(int rand, ref double[,] X, ref int nlist, ref List<cattigor> qqcatigoriyas, ref bool test_or_learn, ref int catразмермаксимальный)
        {
            int randf = rand;
            if (randf > 100)
                randf = 100;
            int dateee = DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Millisecond;
            Random random = new Random(rand+dateee);
            /*int catразмермаксимальный = 0;
            for (int i = 0; i < qqcatigoriyas.Count; i++)
                if (catразмермаксимальный < qqcatigoriyas[i].zn_value.Count)
                    catразмермаксимальный = qqcatigoriyas[i].zn_value.Count;
            catразмермаксимальный += 1;*/
            for (int i = 1; i < nlist; i++)
            {
                X[i, 0] = random.Next(1, randf);
                for (int j = 1; j < catразмермаксимальный; j++)
                    X[i, j] = -1;
            } //отрандомли все включая категории, потом будем переделывать
            for (int i = 0; i < qqcatigoriyas.Count; i++)
            {
                int index = qqcatigoriyas[i].parse;
                int what = random.Next(0, qqcatigoriyas[i].zn_value.Count);
                X[index, 0] = i;
                for (int j = 0; j < qqcatigoriyas[i].zn_value.Count; j++)
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


            /* -------------------------
             * -------------------------
             * -------------------------
             * -------------------------
             * -------------------------
             * в Y положить свою формулу
             * -------------------------
             * -------------------------
             * -------------------------
             * -------------------------
             * -------------------------
             */
            double Y = X[1, 0];
            random = new Random(DateTime.Now.Millisecond);
            //Console.WriteLine("!!!!!!!!!!!\t"+ rand +"\tY = " + Y);
            double rrr = 0;
            if (test_or_learn)
                for (int i = 1; i < nlist; i++)
                {
                    while (true)
                    {
                        rrr = random.NextDouble();
                        //if (rrr <= 0.9)
                        break;
                    }
                    if (X[i, 1] == -1)
                        X[i, 0] += rrr;
                }
            if (test_or_learn)
            {
                while (true)
                {
                    rrr = random.Next(0, 1);
                    rrr += random.NextDouble();
                    //if (rrr <= 0.9)
                        break;
                }
                Y += rrr;
                //Console.WriteLine(":::::::::::\t" + rand + "\tY = " + Y);
            }
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

            String[] filenames = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"*.xml");
            int file_index = -1;
            Console.WriteLine("---------------------");
            reti:
            Console.WriteLine("Выберите файл введя его номер из списка:");
            for (int i = 0; i < filenames.Length-1; i++)
                Console.WriteLine((i+1)+": " + filenames[i] + ";");
            Console.WriteLine(filenames.Length + ": " + filenames[filenames.Length-1] + ".");
            String str = Console.ReadLine();
            if (int.TryParse(str, out file_index))
            {
                file_index--;
                if (file_index > 0 && file_index < filenames.Length)
                {
                    Console.WriteLine("Введенное число не соответствует ни одному номеру из списка. Повторите выбор, если вы забили сгенировать файл, то введите '-exit', чтобы вернуться в основное меню");
                    goto reti;
                }
            }
            else if (Object.Equals(str,"-exit") || Object.Equals(str,"-выход"))
            {
                Console.WriteLine("Возврат в главное меню");
                return;
            }
            else
            {
                Console.WriteLine("Вы ввели совершенно что-то непонятное");
                goto reti;
            }
            Console.WriteLine("---------------------");
            Console.WriteLine("Файл - " + filenames[file_index] + ". Запущена обработка первой линии.");
            doc.Load(filenames[file_index]);
            int ind = 1;
            foreach (XmlNode node in doc.DocumentElement)
            {
                XlistiXXX lx = new XlistiXXX();
                lx.line2 = new List<ListingXVars>();
                lx.line3 = new List<ListingXVars>();
                if (node.Name == "number")
                {
                    lx.line1 = "X[" + node.InnerText.Remove(0,1) + ",0]";
                    lx.categor_1 = ind; ind++;
                    lx.c_bool_1 = false;
                    listX.Add(lx);
                    line += lx.line1 + "\t";
                }
                else if (node.Name == "category")
                {
                    for (int i = 1; i < node.ChildNodes.Count-1; i++)
                    {
                        lx.line1 = "X[" + node["name"].InnerText.Remove(0,1) + "," + i + "]";
                        lx.categor_1 = ind;
                        lx.c_bool_1 = true;
                        listX.Add(lx);
                        line += lx.line1 + "\t";
                    }
                    ind++;
                }
            }
            Console.WriteLine("Первая линия сгенерирована и сохранена в файле line1.txt");
            System.IO.File.WriteAllText(directoriya_to_file+"line1.txt", line, Encoding.UTF8);

            line = null;
            Console.WriteLine("Запущен процесс создания второй линии.");
            for (int i = 0; i < listX.Count; i++)
            {
                var c = listX[i].categor_1;
                var b = listX[i].c_bool_1;
                var tlist = listX[i].line1;
                Random random = new Random(i);
                //for (int j = 0; j < listX.Count; j++)
                Parallel.For(0, listX.Count, (j, state) =>
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
                });
                line += "\n";
            }
            System.IO.File.WriteAllText(directoriya_to_file+"line2.txt", line, Encoding.UTF8);
            Console.WriteLine("Окончен процесс создания второй линии line2.txt.");

            line = null;
            Console.WriteLine("Запущен процесс создания третьей линии.");
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
            System.IO.File.WriteAllText(directoriya_to_file + "line3.txt", line, Encoding.UTF8);
            line = null;
            Console.WriteLine("Окончен процесс создания третьей линии line3.txt.");
            List<String> listlines = new List<string>(); int maxlen = 0; //bool fir = false;

            for (int i = 0; i < listX.Count;i++)
            {
                int ij = 1 + listX[i].line2.Count + listX[i].line3.Count;
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
            System.IO.File.WriteAllLines(directoriya_to_file + "line_all.csv", listlines, Encoding.UTF8);
            Console.WriteLine("Окончен процесс создания общей линии line_all.csv.");
            line = null;
            for (int i = 0; i < listX.Count-1; i++)
            {
                String tmp = null;
                Random r = new Random(i+DateTime.Now.Millisecond);
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
                if (r.Next(-100, 100) < 0)
                {
                    if (r.Next(-30, 100) < 0)
                        tmp = (Math.Round(r.NextDouble(),2)).ToString().Remove(0, 1) + "*" + tmp;
                    else 
                        tmp = "*" + tmp;
                    tmp = (r.Next(1, 3)).ToString() + tmp;
                }
                line += tmp + " + ";
            }
            System.IO.File.WriteAllText(directoriya_to_file + "line_all_res.txt", line.Remove(line.Length-3), Encoding.UTF8);
            Console.WriteLine("Окончен процесс создания уравнения line_all_res.txt.");
            Console.WriteLine("-----------------");
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
            int cati = 0, catonemin = 3, catonemax = 5, maxcat = 5, lenXN = 111;
            Random r = new Random(DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Загрузка процесса создания XML файла");
            Console.WriteLine("-------------------------------");
        retcat1:
            Console.WriteLine("Введите пожалуйста количество переменных + категорий одним числом");
            if (!int.TryParse(Console.ReadLine(), out lenXN))
                goto retcat1;
            Console.WriteLine("-------------------------------");
        retcat2:
            Console.WriteLine("Введите пожалуйста максимальное количество категорий среди переменных");
            if (!int.TryParse(Console.ReadLine(), out maxcat))
                goto retcat2;
            Console.WriteLine("-------------------------------");
        retcat3:
            Console.WriteLine("Введите пожалуйста минимально возможное количество значений в одной категории не менее двух");
            if (!int.TryParse(Console.ReadLine(), out catonemin) || catonemin < 2)
                goto retcat3;
            Console.WriteLine("-------------------------------");
        retcat4:
            Console.WriteLine("Введите пожалуйста максимально возможное количество значений в одной категории не менее двух и не меньше предыдущего вводного значения");
            if (!int.TryParse(Console.ReadLine(), out catonemax) || catonemin >= catonemax)
                goto retcat4;
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Начался процесс создания файла");
            Console.WriteLine("-------------------------------");
            lineslist.Add("<variables>");
            for (int i = 0; i < lenXN; i++)
            {
                if (cati <= maxcat && r.Next(-40, 100) < -1)
                {
                    int n = r.Next(catonemin, catonemax);
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
            System.IO.File.WriteAllLines(directoriya_to_file + "XMLFile1.xml", lines);
            Console.WriteLine("Файл создан и сохранен на диск. Информация по файлу: \n"
            +"1. Количество переменных " + lenXN + "\n"
            +"2. Количество числовых переменных " + (lenXN - 1 - cati) + "\n"
            +"3. Количество категорий " + (cati-1) + "\n");
        }

        static void строковыйАнализФайла()
        {
            String filename = "";
            Console.WriteLine("Введите имя файла без расширения");
            filename = Console.ReadLine();
            filename += ".csv";
            //StreamReader file_lean_csv = new StreamReader();
            string[] str = System.IO.File.ReadAllLines(filename, Encoding.UTF8);
            int max = 0;
            for (int i=0;i<str[0].Length;i++)
            {
                
                if (str[0][i] == (';') || str[0][i] == (','))
                {
                    max++;
                }
            }

            for (int i = 0; i < str.Length; i++)
            {
                int kavcout = 0;
                for (int j = 0; j < str[i].Length; j++)
                {
                    if (str[i][j] == (';') || str[i][j] == (','))
                    {
                        kavcout++;
                    }
                }
                if (kavcout != max)
                {
                    Console.WriteLine("Строка " + (i+1) + " kavcout " + kavcout + " max " + max);
                }
            }
        }
    }
}
