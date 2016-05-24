using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    /// <summary>
    /// используется в переменной, если она категория
    /// </summary>
    class KategorPeremen
    {
        private bool valueKat;
        private String nameKat;

        public String getName()
        {
            return nameKat;
        }
        public void setName(String name)
        {
            nameKat = name;
        }
        public bool getValueBool()
        {
            return valueKat;
        }
        public void setValueBool(bool value)
        {
            valueKat = value;
        }

        /// <summary>
        /// возвращает 1, если категория верна или 0, если нет
        /// </summary>
        /// <returns></returns>
        public double getValueDouble()
        {
            if (valueKat)
                return 1;
            else
                return 0;
        }
    };

    /// <summary>
    /// переменные обучающего файла
    /// </summary>
    class ValuePeremen
    {
        private bool boolkat;
        private double valuedob;
        private String valuekat;

        public void setKategor(bool value)
        {
            boolkat = value;
        }
        public bool getKategor()
        {
            return boolkat;
        }
        public void setDouble(double value)
        {
            valuedob = value;
        }
        public double getDouble()
        {
            return valuedob;
        }
        public String getValueKat()
        {
            return valuekat;
        }
        public void setValueKat(String value)
        {
            valuekat = value;
        }
    };

    /// <summary>
    /// переменные из которых состоят комбинации
    /// </summary>
    class Peremennaya
    {
        private String name;
        private bool kategor;
        private double valueDouble;
        private List<KategorPeremen> kategorValue;
        
        public void setName(String Name)
        {
            name = Name;
        }
        public String getName()
        {
            return name;
        }
        public bool getIfKategor()
        {
            return kategor;
        }
        public void setDouble(double value)
        {
            valueDouble = value;
        }
        public double getDouble()
        {
            return valueDouble;
        }
        public void setIfKategor(bool what)
        {
            kategor = what;
        }
        public void setListKat(List<KategorPeremen> list)
        {
            kategorValue = list;
        }
        public List<KategorPeremen> getListKat()
        {
            return kategorValue;
        }
        /// <summary>
        /// устанавливает значение категории в соответствии с данными файла обучения
        /// </summary>
        /// <param name="valuestr">значение категории из файла обучения</param>
        /// <returns></returns>
        public bool setValueKategor(String valuestr)
        {
            if (kategor == false)
                return false;
            for (int i = 0; i < kategorValue.Count-1; i++)
            {
                if (kategorValue[i].getName() == valuestr)
                    kategorValue[i].setValueBool(true);
                else
                    kategorValue[i].setValueBool(false);
            }
            return true;
        }
    };

    class Kombinacia
    {
        private double koef;
        private double delta;
        private int stepengen = -1;
        private Peremennaya per1;
        private int number1 = -1;
        private Peremennaya per2;
        private int number2 = -1;
        private Peremennaya per3;
        private int number3 = -1;

        /// <summary>
        /// устснавливает значения комбинации
        /// </summary>
        /// <param name="st">степень</param>
        /// <param name="p1">первая переменная</param>
        /// <param name="p2">вторая переменная</param>
        /// <param name="p3">третья переменная</param>
        /// <param name="i">номер значения категории 1, если переменная 1 категория</param>
        /// <param name="j">номер значения категории 2, если переменная 2 категория</param>
        /// <param name="k">номер значения категории 3, если переменная 3 категория</param>
        public void setPeremens(int st, Peremennaya p1, Peremennaya p2, Peremennaya p3, int i, int j, int k)
        {
            stepengen = st;
            per1 = p1;
            if (p1.getIfKategor())
                number1 = i;
            if (st >= 2)
            {
                per2 = p2;
                if (p2.getIfKategor())
                    number2 = j;
                if (st == 3)
                {
                    per3 = p3;
                    if (p3.getIfKategor())
                        number3 = k;
                }
            }
        }
        public void setKoef(double value)
        {
            koef = value;
        }

        public void setDelta(double value)
        {
            delta = value;
        }

        public double getKoef()
        {
            return koef;
        }

        public double getDelta()
        {
            return delta;
        }

        /// <summary>
        /// получает произведение категории
        /// </summary>
        /// <param name="k">если правда то в расчет включается коэффициент</param>
        /// <returns></returns>
        public double getPrizvedenie(bool k)
        {
            double res;
            if (k)
                res = koef;
            else
                res = 1;
            switch(stepengen)
            {
                case 1:
                    if (number1 == -1)
                        res *= per1.getDouble();
                    else
                        res *= per1.getListKat()[number1].getValueDouble();
                    break;
                case 2:
                    if (number1 == -1)
                        res *= per1.getDouble();
                    else
                        res *= per1.getListKat()[number1].getValueDouble();
                    if (number2 == -1)
                        res *= per2.getDouble();
                    else
                        res *= per2.getListKat()[number2].getValueDouble();
                    break;
                case 3:
                    if (number1 == -1)
                        res *= per1.getDouble();
                    else
                        res *= per1.getListKat()[number1].getValueDouble();
                    if (number2 == -1)
                        res *= per2.getDouble();
                    else
                        res *= per2.getListKat()[number2].getValueDouble();
                    if (number3 == -1)
                        res *= per3.getDouble();
                    else
                        res *= per3.getListKat()[number3].getValueDouble();
                    break;
            }
            return res;
        }
    };
}
