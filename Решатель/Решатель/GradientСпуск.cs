using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private double argdelta;
        private double argJb;
        private int iter = 0;
        private double nowJ, oldJ = 0;

        private void shetchik()
        {
            while (iter != -1)
            {
                Thread.Sleep(60000);
                    TimeSpan end = DateTime.Now - starttime;
                    Console.WriteLine(iter + ")\tJ: " + nowJ + "\tla: " + la + "\t time" + (end));
                    mainWindow.setIterData(iter.ToString(), nowJ, oldJ, la, (end));
            }
        }
        DateTime starttime;
        public double[] runСпуск()
        {
            la = 0.0001;
            starttime = DateTime.Now;
            argdelta = (1.0 / ylean.Length);
            argJb = (1.0 / ylean.Length / 2.0);
            double tmpold; oldJ = 0;
            nowJ = getJ();
            Thread thread = new Thread(delegate() { shetchik(); });
            thread.Start();
            while (Math.Abs(oldJ - nowJ) > eps)
            {
            ret:
                iter++;
                oldkoef = (double[])koef.Clone();
                Parallel.For(0, koef.Length, (i) => { koef[i] -= la * delta[i].Sum(); });
                tmpold = oldJ;
                oldJ = nowJ;
                nowJ = getJ();
                if (Math.Abs(tmpold - oldJ) < Math.Abs(oldJ - nowJ))
                {
                    la = la / 2.0;
                    oldJ = tmpold;
                    koef = (double[])oldkoef.Clone();
                    nowJ = getJ();
                    goto ret;
                }
                else
                {
                    la = la * 2.0;
                }
                eps = oldJ * 0.00001;
            }
            mainWindow.setIterData("finish " + iter, nowJ, oldJ, la, (starttime - DateTime.Now));
            Console.WriteLine("finish "+iter+"\t" + nowJ + "\t" + la, (starttime - DateTime.Now));
            iter = -1;
            return (double[])koef.Clone();
        }
    }
}
