using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication4Решатель
{
    class Program
    {
        static private Peremennaya addNewPer(string name)
        {
            Peremennaya p = new Peremennaya();
            p.Name = name;
            p.Kategor = false;
            return p;
        }
        static private Peremennaya addNewKategor(string name, List<string> list)
        {
            Peremennaya p = new Peremennaya();
            p.Name = name;
            p.Kategor = true;
            p.ValueКатегория = new List<KategorValue>();
            foreach (var str in list)
            {
                KategorValue kat = new KategorValue();
                kat.NameKat = str;
                p.ValueКатегория.Add(kat);
            }
            return p;
        }

        static void Main(string[] args)
        {
            List<Peremennaya> massiv_переменных = new List<Peremennaya>();
            string path = "vars.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            int i = 0;
            foreach (XmlNode nod in doc.DocumentElement)
            {
                i++;
                if (nod.Name == "number")
                    massiv_переменных.Add(addNewPer("X" + i.ToString()));
                else
                {
                    List<string> valueName = new List<string>();
                    foreach (XmlNode child in nod.ChildNodes)
                        if (child.Name == "value")
                            valueName.Add(child.InnerText);
                    massiv_переменных.Add(addNewKategor("X" + i.ToString(), valueName));
                }
            }
            GeneratorВариантов gen = new GeneratorВариантов();
            gen.GeneratorVariantov(massiv_переменных);
        }
    }
}
