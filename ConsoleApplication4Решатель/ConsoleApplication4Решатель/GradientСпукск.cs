using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4Решатель
{
    class GradientСпукск
    {
        public List<Kombinacia> gradientСпуск(List<Peremennaya> peremens, List<Kombinacia> allur, List<List<ValuePeremen>> leanvalues)
        {
            List<Kombinacia> result = new List<Kombinacia>();
            for (int i = 0; i < allur.Count; i++)
                allur[i].koef = 0;
            double err = 1.0, L = 0.0001;
            double nowJ = getJnew(peremens, allur, leanvalues), oldJ = 0;
            while(Math.Abs(oldJ-nowJ)>err)
            {
                List<double> koefold = new List<double>();
                for (int i=0;i<allur.Count;i++)
                {
                    double t = allur[i].koef;
                    koefold.Add(t);
                    allur[i].koef -= L - allur[i].delta;
                }
                double tmpOldJ = oldJ;
                oldJ = nowJ;
                nowJ = getJnew(peremens, allur, leanvalues);
                err = oldJ * 0.00001;

                if (Math.Abs(tmpOldJ - oldJ)<Math.Abs(oldJ - nowJ))
                {
                    L = L / 2;
                    oldJ = tmpOldJ;
                    for (int i=0;i<allur.Count;i++)
                    {
                        allur[i].koef = koefold[i];
                    }
                    nowJ = getJnew(peremens, allur, leanvalues);
                }
                else
                {
                    L = L * 2;
                }
                Console.WriteLine(nowJ + " \t " + L);
            }
            return result;
        }

        private double getJnew(List<Peremennaya> peremens, List<Kombinacia> allur, List<List<ValuePeremen>> leanvalues)
        {
            int len = peremens.Count;
            double J = 0;
            //for (int i=0;i<allur.Count;i++)
            Parallel.For(0, allur.Count, (i, state) =>
                {
                    allur[i].delta = 0; //обнуляем везде дельту
                });
            for (int i=0;i<leanvalues.Count;i++)
            {
                double Y = 0;
                for (int j = 0;j<peremens.Count;j++)
                {
                    if (peremens[j].Kategor)
                        peremens[j].setValueKategor(leanvalues[i][j].ValueKategor);
                    else
                        peremens[j].ValueПеремен = leanvalues[i][j].ValueDouble;
                }
                for (int j=0;j<allur.Count;j++)
                {
                    Y += allur[j].GetПроизведение(true);
                }
                J += (1.0 / leanvalues.Count / 2) * (Y - leanvalues[i][len].ValueDouble) * (Y - leanvalues[i][len].ValueDouble);
                //for (int j=0;j<allur.Count;j++)
                Parallel.For(0,allur.Count,(j,state)=>
                {
                    double delta = (1.0 / leanvalues.Count) * (Y - leanvalues[i][len].ValueDouble);
                    delta *= allur[j].GetПроизведение(false);
                    allur[j].delta += delta;
                });
            }
            return J;
        }
    }
}
