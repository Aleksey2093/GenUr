﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication4Решатель
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

        private List<List<ValuePeremen>> getLeanValueFromFile()
        {
            List<List<ValuePeremen>> list = new List<List<ValuePeremen>>();
            String[] lines = System.IO.File.ReadAllLines("test.csv");
            for (int i = 0; i < /*lines.Length*/10; i++)
            {
                //String tmp = "";
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
                /*for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] != ',' && j != lines[i].Length - 1)
                        tmp += lines[i][j];
                    else
                    {
                        ValuePeremen v = new ValuePeremen();
                        double r = getDouble(tmp);
                        if (r != -99999999)
                        {
                            v.setKategor(false);
                            v.setDouble(r);
                        }
                        else
                        {
                            v.setKategor(true);
                            v.setValueKat(tmp);
                        }
                        line.Add(v);
                        tmp = "";
                    }
                }*/
                list.Add(line);
            }
            return list;
        }

        public void runProv(List<Peremennaya> listPeremens, List<Kombinacia> allst)
        {
            List<List<ValuePeremen>> testvalue = new List<List<ValuePeremen>>();
            testvalue = getLeanValueFromFile();
            List<double> restest = new List<double>();
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
                    Y += allst[j].getPrizvedenie(true);
                }
                Console.WriteLine(Y + " - " +testvalue[i][listPeremens.Count].getDouble() + " = " + Math.Abs(Y - testvalue[i][listPeremens.Count].getDouble()));
            }
        }
    }
}
