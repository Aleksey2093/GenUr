using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Решатель
{
    class Fileload
    {
        /// <summary>
        /// Возвращает обычную переменную
        /// </summary>
        /// <param name="name">имя переменной</param>
        /// <returns></returns>
        private Peremennaya addNewPer(string name)
        {
            Peremennaya p = new Peremennaya();
            p.AddValueDouble(name);
            return p;
        }
        /// <summary>
        /// возвращает переменну типа категория
        /// </summary>
        /// <param name="name">имя переменной</param>
        /// <param name="list">значения переменной типа категория</param>
        /// <returns></returns>
        private Peremennaya addNewKategor(string name, List<string> list)
        {
            Peremennaya p = new Peremennaya();
            p.AddValuesKat(name, list);
            return p;
        }
        /// <summary>
        /// загрузка данных из XML файла
        /// </summary>
        /// <returns></returns>
        public List<Peremennaya> getXML()
        {
            List<Peremennaya> list = new List<Peremennaya>();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XML files (*.xml)|*.xml";
            DialogResult r = open.ShowDialog();
            if (r != System.Windows.Forms.DialogResult.OK)
                return null;

            XmlDocument doc = new XmlDocument();
            doc.Load(open.FileName);
            int i = 0;
            foreach (XmlNode nod in doc.DocumentElement)
            {
                i++;
                if (nod.Name == "number")
                    list.Add(addNewPer("X" + i.ToString()));
                else
                {
                    List<string> valueName = new List<string>();
                    foreach (XmlNode child in nod.ChildNodes)
                        if (child.Name == "value")
                            valueName.Add(child.InnerText);
                    list.Add(addNewKategor("X" + i.ToString(), valueName));
                }
            }
            return list;
        }

        /// <summary>
        /// значения из файлов обучения или тестов
        /// </summary>
        /// <returns></returns>
        public List<List<ValueFile>> getDataFile()
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Data files (*.csv)|*.csv";
                DialogResult r = open.ShowDialog();
                if (r != System.Windows.Forms.DialogResult.OK)
                    return null;
                List<List<ValueFile>> list = new List<List<ValueFile>>();
                String[] lines;
                lines = System.IO.File.ReadAllLines(open.FileName);
                foreach (var linefile in lines)
                {
                    String[] linestr = linefile.Split(',');
                    List<ValueFile> tmplist = new List<ValueFile>();
                    foreach (var strval in linestr)
                    {
                        ValueFile v = new ValueFile();
                        v.setValue(strval);
                        tmplist.Add(v);
                    }
                    list.Add(tmplist);
                }
                return list;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
