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

        private void setmass(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues, int z, int m, int n)
        {
            proiz = new double[n][];
            ylearn = new double[n];
            delta = new double[m];
            koef = new double[m];
            oldkoef = new double[m];
            for (int i = 0; i < n; i++)
            {
                int j;
                proiz[i] = new double[allst.Count];
                for (j = 0; j < z; j++)
                    if (listPeremens[j].getIfKategor())
                        listPeremens[j].setValueKategor(leanvalues[i][j].getValueKat());
                    else
                        listPeremens[j].setDouble(leanvalues[i][j].getDouble());
                ylearn[i] = leanvalues[i][j].getDouble();
                for (j = 0; j < m; j++)
                    proiz[i][j] = (double)allst[j].getPrizvedenie();
            }
            for (int i = 0; i < allst.Count; i++)
            {
                delta[i] = 0;
                koef[i] = 10;
                oldkoef[i] = 0;
            }
        }

        private double getJnew()
        {
            double J = 0; double onemi = (double)1.0 / (double)proiz.Length, omni2 = onemi / (double)2;
            for (int i = 0; i < delta.Length; i++)
            {
                delta[i] = 0;
            }
            for (int i = 0; i < proiz.Length; i++)
            {
                double Y = 0; int j;
                for (j = 0; j < koef.Length; j++)
                    Y += proiz[i][j] * koef[j];
                double tm = (Y - ylearn[i]); double onemi_tm = onemi * tm;
                J += (omni2) * tm * tm;
                for (j = 0; j < delta.Length; j++)
                {
                    delta[j] += onemi_tm * proiz[i][j];
                }
            }
            return J;
        }

        public double[] runGradientspusk(List<Peremennaya> listPeremens, List<Kombinacia> allst, List<List<ValuePeremen>> leanvalues)
        {
            //if (leanvalues.Count > 10000) leanvalues = new List<List<ValuePeremen>>(leanvalues.GetRange(0, 10000));
            int cper = listPeremens.Count, stcount = allst.Count, leancount = leanvalues.Count;
            setmass(listPeremens, allst, leanvalues, cper, stcount, leancount);
            Console.WriteLine("Start graddown");
            double eps = 0.00000000001, L = 0.0000001;
            double nowJ = 0, oldJ = 0;
            nowJ = getJnew();
            int iter = 0, olditer = 0;
            DateTime end, start = DateTime.Now;
            Console.WriteLine("time start: " + DateTime.Now);
            while (Math.Abs(oldJ - nowJ) > eps)
            {
                iter++;
                oldkoef = (double[])koef.Clone();
                for (int i = 0; i < stcount; i++)
                {
                    koef[i] -= (L * delta[i]);
                }
                double tmpoldJ = oldJ;
                oldJ = nowJ;
                nowJ = getJnew();
                if (Math.Abs(tmpoldJ - oldJ) < Math.Abs(oldJ - nowJ))
                {
                    L = L / 10.0;
                    oldJ = tmpoldJ;
                    koef = (double[])oldkoef.Clone();
                    nowJ = getJnew();
                }
                else
                {
                    L = L * 2.0;
                }
                if (iter - olditer == 500)
                {
                    olditer = iter;
                    mainWindow.setIterData(iter, nowJ, L, (DateTime.Now - start));
                    Console.WriteLine(iter + ")" + nowJ + "\t" + L + "\t time: " + (DateTime.Now - start));
                }
            }
            end = DateTime.Now;
            Console.WriteLine(iter + ")" + nowJ + "\t" + L);
            Console.WriteLine("time end: " + end + "\n time while: " + (end - start));
            mainWindow.setIterData(iter, nowJ, L, (DateTime.Now - start));
            return koef;
        }


    }
}
