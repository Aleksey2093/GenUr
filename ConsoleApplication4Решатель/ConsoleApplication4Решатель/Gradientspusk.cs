using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4Решатель
{
    class Gradientspusk
    {
        double getJnew(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues)
        {
            double J = 0;
            Parallel.For(0, allst.Count, (i, state) =>
            {
                allst[i].delta = 0;
            });
            for (int i = 0; i < leanvalues.Count; i++) //верхний цикл циферкам для обучения
            {
                double Y = 0;
                Parallel.For(0, listPeremens.Count, (j, state) =>
                {
                    if (listPeremens[j].kategor)
                        listPeremens[j].setValueKategor(leanvalues[i][j].getValueKat());
                    else
                        listPeremens[j].valueDouble = (leanvalues[i][j].getDouble());
                });
                Parallel.ForEach(allst, (st, state) => { Y += st.getPrizvedenie(true); });

                double tm = (Y - leanvalues[i][leanvalues[i].Count - 1].getDouble()), oneminuslen = 1.0 / leanvalues.Count;
                J += (oneminuslen / 2) * tm * tm;
                Parallel.For(0,allst.Count,(j,state) =>
                {
                    double delta = (oneminuslen) * (tm);
                    delta *= allst[j].getPrizvedenie(false);
                    allst[j].delta += delta;
                });
            }
            return J;
        }

        public List<Kombinacia> runGradientspusk(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues)
        {
            Console.WriteLine("Start graddown");
            double err = 1.0, L = 0.0001;
            double nowJ, oldJ = 0;
            nowJ = getJnew(listPeremens, allst, leanvalues);
            List<double> tmp = new List<double>();
            for (int i = 0; i < allst.Count; i++)
            {
                tmp.Add(0);
            }
            while (Math.Abs(oldJ - nowJ) > err)
            {
                //for (int i = 0; i < allst.Count; i++)
                Parallel.For(0, allst.Count, (i,state) =>
                {
                    tmp[i] = (allst[i].getKoef());
                    allst[i].setKoef(tmp[i] - (L * allst[i].getDelta()));
                });
                double tmpoldJ = oldJ;
                oldJ = nowJ;
                nowJ = getJnew(listPeremens, allst, leanvalues);
                double tmpnowJ = nowJ;
                err = oldJ * 0.00001;
                if (Math.Abs(tmpoldJ - oldJ) < Math.Abs(oldJ - nowJ))
                {
                    L = L / 2;
                    oldJ = tmpoldJ;
                    Parallel.For(0, allst.Count, (i, state) =>//for (int i = 0; i < allst.Count; i++)
                    {
                        allst[i].setKoef(tmp[i]);
                    });
                    //nowJ = tmpnowJ;
                    nowJ = getJnew(listPeremens, allst, leanvalues);
                    Console.WriteLine(nowJ + "\t" + L);
                }
                else
                {
                    L = L * 2;
                    Console.WriteLine(nowJ + "\t" + L);
                }
            }
            return allst;
        }


    }
}
