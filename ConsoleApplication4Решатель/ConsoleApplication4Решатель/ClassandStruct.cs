using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4Решатель
{
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
        public double getValueDouble()
        {
            if (valueKat)
                return 1;
            else
                return 0;
        }
    };

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

    class Peremennaya
    {
        public String name;
        public bool kategor;
        public double valueDouble;
        public List<KategorPeremen> kategorValue;

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
        public bool setValueKategor(String valuestr)
        {
            if (kategor == false)
                return false;
            for (int i = 0; i < kategorValue.Count; i++)
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
        public double koef = 1;
        public double delta = 0;
        public int stepengen = -1;
        public Peremennaya per1;
        public int number1 = -1;
        public Peremennaya per2;
        public int number2 = -1;
        public Peremennaya per3;
        public int number3 = -1;

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

        public double getPrizvedenie(bool k)
        {
            double res = 1;
            if (k)
                res = koef;
            switch(stepengen)
            {
                case 1:
                    if (number1 == -1)
                        res *= per1.getDouble();
                    else
                        res *= per1.kategorValue[number1].getValueDouble();
                    break;
                case 2:
                    if (number1 == -1)
                        res *= per1.getDouble();
                    else
                        res *= per1.kategorValue[number1].getValueDouble();
                    if (number2 == -1)
                        res *= per2.getDouble();
                    else
                        res *= per2.kategorValue[number2].getValueDouble();
                    break;
                case 3:
                    if (number1 == -1)
                        res *= per1.getDouble();
                    else
                        res *= per1.kategorValue[number1].getValueDouble();
                    if (number2 == -1)
                        res *= per2.getDouble();
                    else
                        res *= per2.kategorValue[number2].getValueDouble();
                    if (number3 == -1)
                        res *= per3.getDouble();
                    else
                        res *= per3.kategorValue[number3].getValueDouble();
                    break;
            }
            /*if (number1 == -1)
                res *= (per1.getDouble());
            else
                res *= per1.getListKat()[number1].getValueDouble();
            if (stepengen >= 2)
            {
                if (number2 == -1)
                    res *= (per2.getDouble());
                else
                    res *= per2.getListKat()[number2].getValueDouble();
                if (stepengen >= 3)
                {
                    if (number3 == -1)
                        res *= per3.getDouble();
                    else
                        res *= per3.getListKat()[number3].getValueDouble();
                }
            }*/
            return res;
        }
    };
}
