﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication4Решатель
{
    class ProvY
    {
        private double getDouble(string tmp)
        {
            double res = new double();
            if (double.TryParse(tmp, out res))
                return res;
            else if (double.TryParse(tmp.Replace(",", "."), out res))
                return res;
            else if (double.TryParse(tmp.Replace(".", ","), out res))
                return res;
            else
            {
                //MessageBox.Show("Ошибка преобразования числового значения из файла обучения. Возможно в файле ошибка.", 
                //    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return -99999999;
            }
        }

        private List<List<ValuePeremen>> getLeanValueFromFile()
        {
            List<List<ValuePeremen>> list = new List<List<ValuePeremen>>();
            String[] lines = System.IO.File.ReadAllLines("test.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                String tmp = "";
                List<ValuePeremen> line = new List<ValuePeremen>();
                for (int j = 0; j < lines[i].Length; j++)
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
                }
                list.Add(line);
            }
            return list;
        }

        public void runProv(List<Peremennaya> listPeremens, List<Kombinacia> allst)
        {
            List<List<ValuePeremen>> testvalue = new List<List<ValuePeremen>>();
            List<double> restest = new List<double>();
            Console.WriteLine("------------------res Y:");
            for (int i=0;i<testvalue.Count;i++)
            {
                for (int j=0;j<listPeremens.Count;j++)
                {
                    if (listPeremens[j].getIfKategor())
                        listPeremens[j].setValueKategor(testvalue[i][j].getValueKat());
                    else
                        listPeremens[j].setDouble(testvalue[i][j].getDouble());
                }
                double Y = 0;
                for (int j=0;j<allst.Count;j++)
                {
                    Y += allst[i].getPrizvedenie(true);
                }
                Console.WriteLine(Y);
            }
        }
    }
}
