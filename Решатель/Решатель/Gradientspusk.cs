using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class Gradientspusk
    {

        private double[][] proiz;
        private double[] ylearn;
        private double[] delta;
        private double[] koef;
        private double[] oldkoef;
        private MainWindow mainWindow;

        public Gradientspusk(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private void setmass(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues)
        {
            proiz = new double[leanvalues.Count][];
            ylearn = new double[leanvalues.Count];
            delta = new double[allst.Count];
            koef = new double[allst.Count];
            oldkoef = new double[allst.Count];
            for (int i=0;i<leanvalues.Count;i++)
            {
                int j;
                proiz[i] = new double[allst.Count];
                for (j = 0; j < listPeremens.Count; j++)
                    if (listPeremens[j].getIfKategor())
                        listPeremens[j].setValueKategor(leanvalues[i][j].getValueKat());
                    else
                        listPeremens[j].setDouble(leanvalues[i][j].getDouble());
                ylearn[i] = leanvalues[i][j].getDouble();
                for (j = 0; j < allst.Count; j++)
                    proiz[i][j] = allst[j].getPrizvedenie();
            }
            for (int i=0;i<allst.Count;i++)
            {
                delta[i] = 0;
                koef[i] = 0;
                oldkoef[i] = 0;
            }
        }
        /*private List<Kombinacia> getJnewOld(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues, out double J)
        {
            double Y; int j;
            J = 0;
            allst.ForEach((x) => { x.setDelta(0); });
            for (int i = 0; i < leanvalues.Count; i++) //верхний цикл циферкам для обучения
            {
                Y = 0;
                for (j = 0; j < listPeremens.Count; j++)
                {
                    if (listPeremens[j].getIfKategor())
                        listPeremens[j].setValueKategor(leanvalues[i][j].getValueKat());
                    else
                        listPeremens[j].setDouble((leanvalues[i][j].getDouble()));
                }
                allst.ForEach((x) => { Y += x.getPrizvedenie(); });
                double tm = (Y - leanvalues[i][j].getDouble()),
                    oneminuslen = 1.0 / (double)leanvalues.Count;
                J += (oneminuslen / 2.0) * tm * tm;
                allst.ForEach((x) => { double del = oneminuslen * tm * x.getPrizvedenie(); x.setDelta(del); });
            }
            return allst;
        }*/

        private double getJnew()
        {
            double J = 0; double onemi = 1.0 / proiz.Length, omni2 = onemi / 2;
            for (int i = 0; i < delta.Length; i++)
            //Parallel.For(0, delta.Length, (i, state) =>
                {
                    delta[i] = 0;
                }//);
            for (int i = 0; i < proiz.Length; i++)
            //Parallel.For(0,proiz.Length,(i,state)=>
            {
                double Y = 0;int j;
                for (j = 0; j < proiz[i].Length; j++)
                    Y += proiz[i][j] * koef[j];
                //Y += koef[j];
                double tm = (Y - ylearn[i]), onemi_tm = onemi * tm;
                J += (omni2) * tm * tm;
                for (j=0;j<delta.Length;j++)
                //Parallel.For(0,delta.Length,(j,state)=>
                {
                    delta[j] += onemi_tm * proiz[i][j];
                }//);
            }//);
            return J;
        }

        public double[] runGradientspusk(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues)
        {
            if (leanvalues.Count > 10000)
               leanvalues = new List<List<ValuePeremen>>(leanvalues.GetRange(0, 10000));
            setmass(listPeremens, allst, leanvalues);
            Console.WriteLine("Start graddown");
            double err = 1, L = 0.0001;
            double nowJ = 0, oldJ = 0;
            nowJ = getJnew();
            oldJ = 0;
            int iter = 0, olditer = 0;
            int m = allst.Count;
            DateTime end, start = DateTime.Now;
            Console.WriteLine("time start: " + DateTime.Now);
            while (Math.Abs(oldJ - nowJ) > err)
            {
                iter++;
                oldkoef = (double[])koef.Clone();
                for (int i = 0; i < allst.Count; i++)
                //Parallel.For(0,m,(i,state)=>
                {
                    koef[i] -= (L * delta[i]);
                }//);
                double tmpoldJ = oldJ;
                oldJ = nowJ;
                nowJ = getJnew();
                err = oldJ * 0.00001;
                if (Math.Abs(tmpoldJ - oldJ) < Math.Abs(oldJ - nowJ))
                {
                    L = L / 2;
                    oldJ = tmpoldJ;
                    koef = (double[])oldkoef.Clone();
                    nowJ = getJnew();
                }
                else
                {
                    L = L * 2;
                }
                if (iter - olditer == 500)
                {
                    olditer = iter;
                    Console.WriteLine(iter + ")" + nowJ + "\t" + L + "\t time: " + (DateTime.Now - start));
                }
            }
            end = DateTime.Now;
            Console.WriteLine(iter + ")" + nowJ + "\t" + L);
            Console.WriteLine("time end: " + end + "\n time while: " + (end-start));
            return koef;
        }


    }
}
