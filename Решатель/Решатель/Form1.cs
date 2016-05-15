using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Решатель
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            form1class = this;
        }

        public static Form1 form1class;
        List<Peremennaya> massiv_переменных;

        private Peremennaya addNewPer(string name)
        {
            Peremennaya p = new Peremennaya();
            p.Name = name;
            p.Kategor = false;
            return p;
        }
        private Peremennaya addNewKategor(string name, List <string> list)
        {
            Peremennaya p = new Peremennaya();
            p.Name = name;
            p.Kategor = true;
            p.ValueКатегория = new List<KategorValue>();
            foreach(var str in list)
            {
                KategorValue kat = new KategorValue();
                kat.NameKat = str;
                p.ValueКатегория.Add(kat);
            }
            return p;
        }

        private void button1открытьфайлспеременными_Click(object sender, EventArgs e)
        {
            massiv_переменных = new List<Peremennaya>();
            string path = "";
            OpenFileDialog openfile = new OpenFileDialog();
            DialogResult res = openfile.ShowDialog();
            if (res != System.Windows.Forms.DialogResult.OK)
                return;
            path = openfile.FileName;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            int i = 0;
            foreach(XmlNode nod in doc.DocumentElement)
            {
                i++;
                if (nod.Name == "number")
                   massiv_переменных.Add(addNewPer("X" + i.ToString()));      
                else
                {
                    List<string> valueName = new List<string>();
                    foreach(XmlNode child in nod.ChildNodes)
                        if (child.Name == "value")
                            valueName.Add(child.InnerText);
                    massiv_переменных.Add(addNewKategor("X" + i.ToString(), valueName));
                }
            }
        }
    }
}
