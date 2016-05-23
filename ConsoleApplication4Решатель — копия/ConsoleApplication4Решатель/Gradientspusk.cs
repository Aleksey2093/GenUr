using System;
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
                    oneminuslen = 1.0 / (double)leanvalues.Count;
                J += (oneminuslen / 2.0) * tm * tm;
                allst.ForEach((x) => { double del = oneminuslen * tm * x.getPrizvedenie(false); x.setDelta(del); });
            }
            return allst;
        }

        public List<Kombinacia> runGradientspusk(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues)
        {
            if (leanvalues.Count > 10000)
                leanvalues = new List<List<ValuePeremen>>(leanvalues.GetRange(0, 3000));
            Parallel.For(0, allst.Count, (i, state) => { allst[i].setKoef(0); allst[i].setDelta(0); });
            Console.WriteLine("Start graddown");
            double err = 1, L = 0.0001;
            double nowJ = 0, oldJ = 0;
            allst = getJnew(listPeremens, allst, leanvalues, out nowJ);
            oldJ = 0;
            /*List<double> tmp = new List<double>();
            for (int i = 0; i < allst.Count; i++)
            {
                tmp.Add(0);
            }*/
            int iter = 0;
            double[] tmp = new double[allst.Count];
            while (Math.Abs(oldJ - nowJ) > err)
            {
                iter++;
                for (int i = 0; i < allst.Count; i++)
                {
                    tmp[i] = (allst[i].getKoef());
                    allst[i].setKoef(allst[i].getKoef() - (L * allst[i].getDelta()));
                }
                double tmpoldJ = oldJ;
                oldJ = nowJ;
                allst = getJnew(listPeremens, allst, leanvalues, out nowJ);
                err = oldJ * 0.00001;
                if (Math.Abs(tmpoldJ - oldJ) < Math.Abs(oldJ - nowJ))
                {
                    L = L / 2;
                    oldJ = tmpoldJ;
                    for (int i = 0; i < allst.Count; i++)
                    {
                        allst[i].setKoef(tmp[i]);
                    }
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
