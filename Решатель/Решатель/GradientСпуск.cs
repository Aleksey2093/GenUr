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
                koef[i] = 1;
                oldkoef[i] = 0;
                delta[i] = 0;
            }
        }

        /// <summary>
        /// считает J
        /// </summary>
        /// <returns></returns>
        private double getJnew()
        {
            for (int i = 0; i < delta.Length; i++)
                delta[i] = 0;
            double J = 0;
            for (int i=0;i<ylean.Length;i++)
            {
                double Y = 0;
                for (int j=0;j<koef.Length;j++)
                {
                    Y += koef[j] * proiz[i][j];
                }
                double tm = (Y - ylean[i]);
                J += (1.0 / ylean.Length / 2.0)*tm*tm;
                for (int j=0;j<delta.Length;j++)
                {
                    delta[j] += (1.0 / ylean.Length) * (tm) * proiz[i][j];
                }
            }
            return J;
        }

        public double[] runGradientСпуск()
        {
            double nowJ, oldJ = 0, tmpnowJ, tmpoldJ;
            double f1 = la / ylean.Length;
            nowJ = getJnew();
            while(Math.Abs(oldJ - nowJ) > eps)
            {
                //расчитать значения коэфциентов
                oldkoef = (double[])koef.Clone(); //сохраняем старые коэффициенты
                for (int i = 0; i < koef.Length;i++ )
                {
                    koef[i] = koef[i] - la * delta[i];
                }
                tmpnowJ = nowJ; //старое nowJ
                tmpoldJ = oldJ; //старое oldJ
                oldJ = nowJ; //новое oldJ
                nowJ = getJnew();//новое nowJ
                if (Math.Abs(oldJ-nowJ)>Math.Abs(tmpoldJ-tmpnowJ)) //если новые значения больше старых делаем откат
                {
                    la = la / 20;
                    koef = (double[])oldkoef.Clone(); //возвращаем значения коэффициентов
                    oldJ = tmpoldJ; //возвращаем старое oldJ
                    nowJ = getJnew(); //перерасчитывает J
                }
                else
                {
                    la = la * 2;
                }
                
            }
            Console.WriteLine("J = " + nowJ + " lamda = " + la);
            return koef;
        }
    }
}
