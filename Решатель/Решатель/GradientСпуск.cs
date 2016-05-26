using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public GradientСпуск(double[][] proiz, double[] ylean, double eps, double la, int coutkoef)
        {
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
            
            for (int i=0;i<koef.Length;i++)
            {
                delta[i] = 0;
            }
            for (int i = 0; i < ylean.Length; i++)
            {
                double Y = 0;
                for (int j=1;j<koef.Length;j++)
                {
                    Y += koef[j] * proiz[i][j];
                }
                Jb += (1.0 / ylean.Length / 2.0) * (Y - ylean[i]) * (Y - ylean[i]);
                for (int j=1;j<koef.Length;j++)
                {
                    delta[j] += (1.0 / ylean.Length) * (Y - ylean[i]); //*proiz[i][j];
                }
            }
            return Jb;
        }

        public double[] runСпуск()
        {
            double nowJ, oldJ = 0;
            nowJ = getJ();
            while(Math.Abs(oldJ-nowJ)>eps)
            {
                oldkoef = (double[])koef.Clone();
                for (int i=0;i<koef.Length;i++)
                {
                    koef[i] -= la * delta[i];
                }
                double tmpold = oldJ;
                oldJ = nowJ;
                nowJ = getJ();
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
                Console.WriteLine(nowJ + "\t" + la);
            }

            return koef;
        }
    }
}
