using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4Решатель
{
    public class Uravnenie
    {
        public List<Kombinacia> list;
    }
    public class Kombinacia
    {
        public int stepengen = -1;
        public double koef;
        public double delta;
        public Peremennaya onePer = null;
        public int oneValueNumbler = -1;
        public Peremennaya twoPer = null;
        public int twoValueNumbler = -1;
        public Peremennaya threePer = null;
        public int threeValueNumbler = -1;

        public double GetПроизведение(bool k)
        {
            double res = 1;
            if (k)
                res = koef;
            if (oneValueNumbler == -1)
                res *= onePer.ValueПеремен;
            else
                res *= onePer.ValueКатегория[oneValueNumbler].ValueKatDouble;
            if (stepengen >= 2)
            {
                if (twoValueNumbler == -1)
                    res *= twoPer.ValueПеремен;
                else
                    res *= twoPer.ValueКатегория[twoValueNumbler].ValueKatDouble;
            }
            if (stepengen >= 3)
            {
                if (threeValueNumbler == -1)
                    res *= threePer.ValueПеремен;
                else
                    res *= threePer.ValueКатегория[threeValueNumbler].ValueKatDouble;
            }
            return res;
        }

        public string GetStringPrint
        {
            get 
            {
                string res = "";
                if (onePer.Kategor)
                    res += onePer.Name + (oneValueNumbler + 1);
                else
                    res += onePer.Name;
                res += "*";
                if (twoPer.Kategor)
                    res += twoPer.Name + (twoValueNumbler + 1);
                else
                    res += twoPer.Name;
                res += "*";
                if (threePer.Kategor)
                    res += threePer.Name + (threeValueNumbler + 1);
                else
                    res += threePer.Name;
                return res;
            }
        }
    }

    public class KategorValue
    {
        private bool valueKat;
        private string valueKat_name;

        public double ValueKatDouble
        {
            get { if (valueKat) return 1; else return 0;}
            set { if (value == 1) valueKat = true; else if (value == 0) valueKat = false; }
        }
        public bool ValueKatBool
        {
            get { return valueKat; }
            set { valueKat = value; }
        }
        public string NameKat
        {
            get { return valueKat_name; }
            set { valueKat_name = value; }
        }
    }

    public class ValuePeremen
    {
        private bool boolkat;
        private double valuedob;
        private string valuekat;

        public bool KatOrValue
        {
            get { return boolkat; }
            set { boolkat = value; }
        }
        public double ValueDouble
        {
            get { return valuedob; }
            set { valuedob = value; }
        }
        public string ValueKategor
        {
            get { return valuekat; }
            set { valuekat = value; }
        }
    }

    public class Peremennaya
    {
        private string name;
        private bool kategor;
        private double valuePerement;
        private List<KategorValue> kategorValue;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool Kategor
        {
            get { return kategor; }
            set { kategor = value; }
        }

        public double ValueПеремен 
        {
            get { return valuePerement; }
            set { valuePerement = value; }
        }
        public List<KategorValue> ValueКатегория
        {
            get { return kategorValue; }
            set { kategorValue = value; }
        }
        public int[] getKatValues(string value)
        {
            int[] res = new int[kategorValue.Count];
            for (int i = 0; i < kategorValue.Count;i++)
            {
                if (kategorValue[i].NameKat == value)
                    res[i] = 1;
                else
                    res[i] = 0;
            }
            return res;
        }

        public bool setValueKategor(string value)
        {
            if (Kategor)
            {
                for (int i = 0; i < kategorValue.Count;i++ )
                {
                    if (value == kategorValue[i].NameKat)
                        kategorValue[i].ValueKatBool = true;
                    else
                        kategorValue[i].ValueKatBool = false;
                }                    
                return true;
            }
            else
                return false;
        }
    }
}
