using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class GradientСпуск
    {
        private double[][] proiz;
        private double[] ylean;
        private double[] koef;
        private double[] oldkoef;
        private double[][] delta;
        private double eps;
        private double la;
        private MainWindow mainWindow;
        private double[] JBmass;
        private double[] Ymass;

        public GradientСпуск(double[][] proiz, double[] ylean, double eps, double la, int coutkoef, MainWindow ma)
        {
            mainWindow = ma;
            // TODO: Complete member initialization
            this.proiz = proiz;
            this.ylean = ylean;
            this.eps = eps;
            this.la = la;
            koef = new double[coutkoef];
            oldkoef = new double[coutkoef];
            delta = new double[coutkoef][];
            for (int i = 0; i < coutkoef; i++)
            {
                koef[i] = 0;
                oldkoef[i] = 0;
                delta[i] = new double[ylean.Length];
            }
            koef[0] = 0;
            JBmass = new double[ylean.Length];
            Ymass = new double[ylean.Length];
        }
        private double getJ()
        {
            double Jb = 0;
            //Parallel.For(0, delta.Length, (i) => { for (int j = 0; j < delta[i].Length; j++) delta[i][j] = 0; });
            /*for (int i = 0; i < ylean.Length; i++)
            {
                double Y = 0;
                for (int j = 0; j < koef.Length; j++)
                    Y += koef[j] * proiz[i][j];
                double arg2 = (Y - ylean[i]);
                Jb += argJb * Math.Pow(arg2, 2);
                double argdelta2 = arg2 * argdelta;
                for (int j = 0; j < koef.Length; j++)
                    delta[j] += argdelta2 * proiz[i][j];
            }*/
            Parallel.For(0, ylean.Length, (i, state) =>
            {
                Ymass[i] = 0;
                for (int j = 0; j < koef.Length; j++)
                    Ymass[i] += koef[j] * proiz[i][j];
                double arg2 = (Ymass[i] - ylean[i]);
                JBmass[i] = argJb * Math.Pow(arg2, 2);
                double argdelta2 = arg2 * argdelta;
                for (int j = 0; j < koef.Length; j++)
                    delta[j][i] = argdelta2 * proiz[i][j];
            });
            Jb = JBmass.AsParallel().AsOrdered().Sum();
            return Jb;
        }
        double argdelta;
        double argJb;
        public double[] runСпуск()
        {
            DateTime starttime = DateTime.Now;
            argdelta = (1.0 / ylean.Length);
            argJb = (1.0 / ylean.Length / 2.0);
            double nowJ, oldJ = 0, tmpold;
            nowJ = getJ(); bool proh = true;
            int iter = 0, oiter = 0; 
            rwa:
            while(Math.Abs(oldJ-nowJ)>eps)
            {
                iter++;
                oldkoef = (double[])koef.Clone();
                Parallel.For(0, koef.Length, (i) => { koef[i] = koef[i] - la * delta[i].AsParallel().AsOrdered().Sum(); });
                tmpold = oldJ;
                oldJ = nowJ;
                nowJ = getJ();
                eps = oldJ * 0.0000000000000000000000000000000001;
                if (Math.Abs(tmpold-oldJ) < Math.Abs(oldJ - nowJ))
                {
                    la = la / 2;
                    oldJ = tmpold;
                    koef = (double[])oldkoef.Clone();
                    nowJ = getJ();
                }
                else
                {
                    la = la * 2.0;
                }
                if (iter - oiter > 99)
                {
                    Parallel.Invoke(() =>
                    {
                        oiter = iter;
                    }, () =>
                    {
                        Console.WriteLine(iter + ")\tJ: " + nowJ + "\tla: " + la + "\t time" + (starttime-DateTime.Now));
                    }, () =>
                    {
                        mainWindow.setIterData(iter, nowJ, la, (starttime - DateTime.Now));
                    });
                }
                if (nowJ == double.MaxValue || oldJ == double.MaxValue || nowJ == double.MinValue || oldJ == double.MinValue)
                {

                }
            }
            if (!proh)
            {
                oldJ = 0;
                proh = false;
                goto rwa;
            }
            mainWindow.setIterData(iter, nowJ, la, (starttime - DateTime.Now));
            Console.WriteLine("finish \t" + nowJ + "\t" + la, (starttime - DateTime.Now));
            return (double[])koef.Clone();
        }
    }
}
