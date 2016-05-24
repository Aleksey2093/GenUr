using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Решатель
{
    class ProvY
    {
        private bool getDouble(string tmp, out double res)
        {
            if (double.TryParse(tmp, out res))
                return true;
            else if (double.TryParse(tmp.Replace(",", "."), out res))
                return true;
            else if (double.TryParse(tmp.Replace(".", ","), out res))
                return true;
            else
            {
                //MessageBox.Show("Ошибка преобразования числового значения из файла обучения. Возможно в файле ошибка.", 
                //    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        private String getPathLeanFile()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Открыть файл с тестовым набором";
            op.ShowDialog();

            return op.FileName;
        }

        private List<List<ValuePeremen>> getLeanValueFromFile()
        {
            List<List<ValuePeremen>> list = new List<List<ValuePeremen>>();
            String[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(getPathLeanFile());
            }
            catch (Exception ex)
            {
                return list;
            }
            for (int i = 0; i < lines.Length; i++)
            {
                List<ValuePeremen> line = new List<ValuePeremen>();
                List<String> strline = new List<string>(lines[i].Split(','));
                strline.ForEach((x) => 
                {
                    ValuePeremen val = new ValuePeremen();
                    double r;
                    if (getDouble(x,out r) == false)
                    {
                        val.setKategor(true);
                        val.setValueKat(x);
                    }
                    else
                    {
                        val.setKategor(false);
                        val.setDouble(r);
                    }
                    line.Add(val);
                });
                list.Add(line);
            }
            return list;
        }

        public List<List<double>> runProv(List<Peremennaya> listPeremens, List<Kombinacia> allst, double[] koef)
        {
            List<List<ValuePeremen>> testvalue = new List<List<ValuePeremen>>();
            List<List<double>> res = new List<List<double>>();
            testvalue = getLeanValueFromFile();
            Console.WriteLine("------------------res Y:");
            int j;
            for (int i=0;i<testvalue.Count;i++)
            {
                for (j=0;j<listPeremens.Count;j++)
                {
                    if (listPeremens[j].getIfKategor())
                        listPeremens[j].setValueKategor(testvalue[i][j].getValueKat());
                    else
                        listPeremens[j].setDouble(testvalue[i][j].getDouble());
                }
                double Y = 0;
                for (j=0;j<allst.Count;j++)
                {
                    Y += allst[j].getPrizvedenie() * koef[j];
                }
                List<double> temp = new List<double>();
                temp.Add(Y);
                if (listPeremens.Count == testvalue[i].Count - 1)
                {
                    temp.Add(testvalue[i][listPeremens.Count].getDouble());
                    temp.Add(Math.Abs(Y - testvalue[i][listPeremens.Count].getDouble()));
                }
                res.Add(temp);
                Console.WriteLine(Y + " - " +testvalue[i][listPeremens.Count].getDouble() + " = " + Math.Abs(Y - testvalue[i][listPeremens.Count].getDouble()));
            }
            return res;
        }
    }
}
