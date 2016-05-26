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
        private double[] delta;
        private double eps;
        private double la;
        private MainWindow mainWindow;

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
            delta = new double[coutkoef];
            for (int i = 0; i < coutkoef; i++)
            {
                koef[i] = 0;
                oldkoef[i] = 0;
                delta[i] = 0;
            }
            koef[0] = 0;
        }
        private double getJ()
        {
            double Jb = 0;
            delta.AsParallel().ForAll(x=> { x = 0; });
            for (int i = 0; i < ylean.Length; i++)
            {
                double Y = 0;
                for (int j=0;j<koef.Length;j++)
                    Y += koef[j] * proiz[i][j];
                double arg2 = (Y - ylean[i]);
                Jb += argJb * Math.Pow(arg2,2);
                double argdelta2 = arg2 * argdelta;
                for (int j=0;j<koef.Length;j++)
                    delta[j] += argdelta2 *proiz[i][j];
            }
            return Jb;
        }
        double argdelta;
        double argJb;
        public double[] runСпуск()
        {
            argdelta = (1.0 / ylean.Length);
            argJb = (1.0 / ylean.Length / 2.0);
            double nowJ, oldJ = 0;
            nowJ = getJ();
            int iter = 0, oiter = 0;
            while(Math.Abs(oldJ-nowJ)>eps)
            {
                iter++;
                oldkoef = (double[])koef.Clone();
                for (int i=0;i<koef.Length;i++)
                {
                    koef[i] -= la * delta[i];
                }
                double tmpold = oldJ;
                oldJ = nowJ;
                nowJ = getJ();
                eps = oldJ * 0.00001;
                if (Math.Abs(tmpold-oldJ) < Math.Abs(oldJ - nowJ))
                {
                    la = la / 2.0;
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
                    oiter = iter;
                    Console.WriteLine(iter+")\t"+nowJ + "\t" + la);
                    mainWindow.setIterData(iter, nowJ, la);
                }
            }
            mainWindow.setIterData(iter, nowJ, la);
            Console.WriteLine("finish \t" + nowJ + "\t" + la);
            return (double[])koef.Clone();
        }
    }
}
