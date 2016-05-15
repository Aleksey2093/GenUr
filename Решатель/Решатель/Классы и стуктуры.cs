using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    public class Uravnenie
    {
        public List<Kombinacia> list;
    }
    public class Kombinacia
    {
        public int stepengen = -1;
        public double koef;
        public Peremennaya onePer = null;
        public int oneValueNumbler = -1;
        public Peremennaya twoPer = null;
        public int twoValueNumbler = -1;
        public Peremennaya threePer = null;
        public int threeValueNumbler = -1;

        public double GetПроизведение
        {
            get 
            {
                double res = 1;
                if (oneValueNumbler == -1)
                    res *= onePer.ValueПеремен;
                else
                    res *= onePer.ValueКатегория[oneValueNumbler].ValueKatDouble;
                if (twoValueNumbler == -1)
                    res *= twoPer.ValueПеремен;
                else
                    res *= twoPer.ValueКатегория[twoValueNumbler].ValueKatDouble;
                if (threeValueNumbler == -1)
                    res *= threePer.ValueПеремен;
                else
                    res *= threePer.ValueКатегория[threeValueNumbler].ValueKatDouble;
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
    }
}
