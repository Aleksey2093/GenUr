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
            for (int i = 0; i < allst.Count; i++)
            {
                allst[i].setDelta(0);
            }
            for (int i = 0; i < leanvalues.Count; i++) //верхний цикл циферкам для обучения
            {
                double Y = 0;
                for (int j = 0; j < listPeremens.Count; j++)
                {
                    if (listPeremens[j].getIfKategor())
                        listPeremens[j].setValueKategor(leanvalues[i][j].getValueKat());
                    else
                        listPeremens[j].setDouble(leanvalues[i][j].getDouble());
                }

                /*for (int j = 0; j < allst.Count; j++)
                {
                    Y += allst[j].getPrizvedenie(true);
                }*/
                Parallel.ForEach(allst, (st, state) => { Y += st.getPrizvedenie(true); });

                J += (1.0 / leanvalues.Count / 2) * (Y - leanvalues[i][leanvalues[i].Count - 1].getDouble())
                        * (Y - leanvalues[i][leanvalues[i].Count - 1].getDouble());
                //пеперь надо посчитать дельта
                //for (int j = 0; j < allst.Count; j++)
                Parallel.For(0,allst.Count,(j,state) =>
                {
                    double delta = (1.0 / leanvalues.Count) * (Y - leanvalues[i][leanvalues[i].Count - 1].getDouble());
                    delta *= allst[j].getPrizvedenie(false);
                    double oldel = allst[j].getDelta();
                    allst[j].setDelta(oldel + delta);
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

            while (Math.Abs(oldJ - nowJ) > err)
            {
                //double tmp[allst.Count];
                List<double> tmp = new List<double>();
                for (int i = 0; i < allst.Count; i++)
                {
                    tmp.Add(allst[i].getKoef());
                    allst[i].setKoef(tmp[i] - (L * allst[i].getDelta()));
                }
                double tmpoldJ = oldJ;
                oldJ = nowJ;
                nowJ = getJnew(listPeremens, allst, leanvalues);
                err = oldJ * 0.00001;
                if (Math.Abs(tmpoldJ - oldJ) < Math.Abs(oldJ - nowJ))
                {
                    L = L / 2;
                    oldJ = tmpoldJ;
                    for (int i = 0; i < allst.Count; i++)
                    {
                        allst[i].setKoef(tmp[i]);
                    }
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
