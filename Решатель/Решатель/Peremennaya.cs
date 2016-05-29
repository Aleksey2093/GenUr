using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    /// <summary>
    /// переменная уравнения типа переменная или категория
    /// </summary>
    class Peremennaya
    {
        private string name;
        private bool kat;
        private double dob;
        private List<bool> valcatbool;
        private List<string> valcatname;

        /// <summary>
        /// возвращает имя значения категории по индексу
        /// </summary>
        /// <param name="i">индекс значения</param>
        /// <returns></returns>
        public string getListCatNames(int i)
        {
            return valcatname[i];
        }

        /// <summary>
        /// возвращает имя переменной
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// возвращает количество переменных в категории
        /// </summary>
        /// <returns></returns>
        public int getCountKat()
        {
            return valcatname.Count;
        }

        /// <summary>
        /// данная переменная категория или нет
        /// </summary>
        public bool IfKategori
        {
            get { return kat; }
        }

        /// <summary>
        /// установка значений переменных категории по значению
        /// </summary>
        /// <param name="nameCat">строковое значение категории</param>
        public void setKatValue(string nameCat)
        {
            for (int i = 0; i < valcatname.Count; i++)
                if (nameCat == valcatname[i])
                    valcatbool[i] = true;
                else
                    valcatbool[i] = false;
        }

        /// <summary>
        /// возвращает значение категории по значению
        /// </summary>
        /// <param name="nameCat">имя переменной категории</param>
        /// <returns>1 или 0, а так же -1 если ошибка</returns>
        public double getKatValue(string nameCat)
        {
            for (int i=0;i<valcatname.Count;i++)
            {
                if (valcatname[i] == nameCat)
                    if (valcatbool[i])
                        return 1;
                    else
                        return 0;
            }
            return -1;
        }

        /// <summary>
        /// возвращает значение категории по индексу
        /// </summary>
        /// <param name="j">индекс в листе значений</param>
        /// <returns></returns>
        public double getKatValue(int j)
        {
            if (valcatbool[j])
                return 1;
            else return 0;
        }

        /// <summary>
        /// можно получить значение перменной, если это просто переменная или установить значение
        /// </summary>
        public double ValueDouble
        {
            set { dob = value; }
            get { return dob; }
        }

        /// <summary>
        /// добавление новой переменной и указание того, что переменная типа double
        /// </summary>
        /// <param name="NamePer">название переменной</param>
        public void Create(string NamePer)
        {
            name = NamePer;
            kat = false;
        }
        /// <summary>
        /// добавление новой переменной категории при генерации переменных и указание того, что переменная категория
        /// </summary>
        /// <param name="NamePer">название переменной</param>
        /// <param name="NamesCat">значений переменных категории</param>
        public void Create(string NamePer,List<string> NamesCat)
        {
            name = NamePer;
            kat = true;
            valcatname = new List<string>(NamesCat);
            valcatbool = new List<bool>();
            for (int i = 0; i < valcatname.Count; i++)
                valcatbool.Add(false);
        }
    }
    /// <summary>
    /// комбинации переменных любой степени
    /// </summary>
    class Kombinacia
    {
        private List<Peremennaya> pers;
        private List<int> numberscat;

        /// <summary>
        /// Создает новую комбинацию
        /// </summary>
        public Kombinacia()
        {
            this.pers = new List<Peremennaya>();
            this.numberscat = new List<int>();
        }

        /// <summary>
        /// Создает новую комбинацию
        /// </summary>
        /// <param name="kombo">копинация значения которой копируются в новую</param>
        public Kombinacia(Kombinacia kombo)
        {
            this.pers = new List<Peremennaya>(kombo.pers);
            this.numberscat = new List<int>(kombo.numberscat);
        }

        /// <summary>
        /// устанвливает значение комбинации.
        /// </summary>
        /// <param name="listpers">список переменных входящих в комбинацию</param>
        /// <param name="listcatnums">индекс значений типа категория, если не категория, то -1</param>
        public void Create(List<Peremennaya> listpers, List<int> listcatnums)
        {
            pers = new List<Peremennaya>(listpers);
            numberscat = new List<int>(listcatnums);
        }

        /// <summary>
        /// добавляет к этой комбинации новую переменную
        /// </summary>
        /// <param name="per">переменная которая добавляется</param>
        /// <param name="num">номер значения категории</param>
        public void Create(Peremennaya per, int num)
        {
            if (pers == null)
                Parallel.Invoke(() => { pers = new List<Peremennaya>(); }, () => { numberscat = new List<int>(); });
            Parallel.Invoke(() => { pers.Add(per); }, () => { numberscat.Add(num); });
        }

        /// <summary>
        /// добавляет к этой комбинации новую переменную
        /// </summary>
        /// <param name="per">переменная которая добавляется</param>
        public void Create(Peremennaya per)
        {
            if (pers == null)
                Parallel.Invoke(() => { pers = new List<Peremennaya>(); }, () => { numberscat = new List<int>(); });
            Parallel.Invoke(() => { pers.Add(per); }, () => { numberscat.Add(-1); });
        }

        public void printKombo()
        {
            string tmp ="";
            for(int i=0;i<pers.Count;i++)
            {
                var c = pers[i];
                if (c.IfKategori)
                {
                    tmp += c.Name + "_" + numberscat[i] + "\t";
                }
                else
                    tmp += c.Name + "\t";
            }
            Console.WriteLine(tmp);
        }

        /// <summary>
        /// возвращает произведение комбинации
        /// </summary>
        /// <returns></returns>
        public double getProiz()
        {
            if (pers.Count == 0)
                return 0;
            double res = 1;
            for (int i = 0; i < pers.Count;i++ )
            {
                var per = pers[i];
                var j = numberscat[i];
                if (per.IfKategori)
                    res *= per.getKatValue(j);
                else
                    res *= per.ValueDouble;
            }
            return res;
        }
    }

    /// <summary>
    /// значения из файлов
    /// </summary>
    class ValueFile
    {
        private string valcat;
        private double valdob;

        /// <summary>
        /// присваивает значение
        /// </summary>
        /// <param name="value">значение из файла обучения в строковой форме</param>
        public void setValue(string value)
        {
            double v;
            if (double.TryParse(value, out v))
            {
                valdob = v; valcat = "";
            }
            else if (double.TryParse(value.Replace(".", ","), out v))
            {
                valdob = v; valcat = "";
            }
            else if (double.TryParse(value.Replace(",", "."), out v))
            {
                valdob = v; valcat = "";
            }
            else
                valcat = value;
        }

        /// <summary>
        /// значение категории
        /// </summary>
        /// <returns></returns>
        public string getValueKat()
        {
            return valcat;
        }
        /// <summary>
        /// значение переменной
        /// </summary>
        /// <returns></returns>
        public double getValueDob()
        {
            return valdob;
        }
        /// <summary>
        /// возвращает категория это или нет
        /// </summary>
        /// <returns></returns>
        public bool IfKategori()
        {
            if (valcat == "")
                return true;
            else
                return false;
        }
    }
}
