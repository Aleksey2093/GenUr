﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4Решатель
{
    class Gradientspusk
    {
        List<Kombinacia> getJnew(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues, out double J)
        {
            double /*J = 0, */Y; int /*len = leanvalues[0].Count - 1,*/ j;
            J = 0;
            allst.ForEach((x) => { x.setDelta(0); });
            for (int i = 0; i < leanvalues.Count; i++) //верхний цикл циферкам для обучения
            {
                Y = 0;
                for (j = 0; j < listPeremens.Count; j++)
                {
                    if (listPeremens[j].kategor)
                        listPeremens[j].setValueKategor(leanvalues[i][j].getValueKat());
                    else
                        listPeremens[j].valueDouble = (leanvalues[i][j].getDouble());
                }
                allst.ForEach((x) => { Y += x.getPrizvedenie(true); });
                double tm = (Y - leanvalues[i][j].getDouble()),
                    oneminuslen = 1.0 / leanvalues.Count;
                J += (oneminuslen / 2) * tm * tm;
                //allst.ForEach((x) => { x.setDelta(oneminuslen * tm * x.getPrizvedenie(false)); });
                listPeremens.ForEach((x) =>
                {
                    if (x.getIfKategor())
                    {
                        x.getListKat().ForEach((y) => { y.setDelta(oneminuslen * tm * x.getDouble(false)); });
                    }
                    else
                    {
                        x.setDelta(oneminuslen * tm * x.getDouble(false));
                    }
                });
            }
            return allst;
        }

        public List<Kombinacia> runGradientspusk(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues)
        {
            Parallel.For(0, allst.Count, (i, state) => { allst[i].setKoef(0); });
            Console.WriteLine("Start graddown");
            double err = 1, L = 0.0001;
            double nowJ = 0, oldJ = 0;
            allst = getJnew(listPeremens, allst, leanvalues, out nowJ);
            oldJ = 0;
            List<double> tmp = new List<double>();
            /*for (int i = 0; i < allst.Count; i++)
            {
                tmp.Add(0);
            }*/
            int iter = 0;
            while (Math.Abs(oldJ - nowJ) > err)
            {
                iter++;
                tmp = new List<double>();
                tmp.Clear();
                for (int i = 0; i < listPeremens.Count; i++)
                {
                    if (listPeremens[i].getIfKategor())
                    {
                        for (int j=0;j<listPeremens[i].getListKat().Count;j++)
                        {
                            var x = listPeremens[i].getListKat()[j];
                            tmp.Add(x.getKoef());
                            x.setKoef(x.getKoef() - (L * x.getDelta()));
                        }
                    }
                    else
                    {
                        tmp.Add(listPeremens[i].getKoef());
                        listPeremens[i].setKoef(listPeremens[i].getKoef() - (L * listPeremens[i].getDelta()));
                    }
                    //tmp[i](allst[i].getKoef());
                    //allst[i].setKoef(allst[i].getKoef() - (L * allst[i].getDelta()));
                }
                double tmpoldJ = oldJ;
                oldJ = nowJ;
                allst = getJnew(listPeremens, allst, leanvalues, out nowJ);
                err = oldJ * 0.00001;
                if (Math.Abs(tmpoldJ - oldJ) < Math.Abs(oldJ - nowJ))
                {
                    L = L / 2;
                    oldJ = tmpoldJ;
                    int i = 0;
                    listPeremens.ForEach((x) =>
                        {
                            if (x.getIfKategor())
                            {
                                x.getListKat().ForEach((y) => { y.setKoef(tmp[i]); i++; });
                            }
                            else
                            {
                                x.setKoef(tmp[i]); i++;
                            }
                        });
                    allst = getJnew(listPeremens, allst, leanvalues, out nowJ);
                    Console.WriteLine(iter +")"+nowJ + "\t" + L);
                }
                else
                {
                    L = L * 2;
                    Console.WriteLine(iter + ")" +nowJ + "\t" + L);
                }
            }
            return allst;
        }


    }
}
