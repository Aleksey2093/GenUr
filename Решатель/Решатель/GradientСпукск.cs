using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class GradientСпукск
    {
        List<double> getYvalue(List<Peremennaya> peremens, List<Kombinacia> allur, List<List<ValuePeremen>> leanvals)
        {
            List<double> listY = new List<double>();
            for (int i = 0; i < leanvals.Count; i++)
            {
                Parallel.For(0, peremens.Count, (j, state) =>
                    {
                        if (peremens[j].Kategor)
                            peremens[j].setValueKategor(leanvals[i][j].ValueKategor);
                        else
                            peremens[j].ValueПеремен = leanvals[i][j].ValueDouble;
                    });
                double urY = 0;
                Parallel.For(0, allur.Count, (j, state) =>
                    {
                        urY += allur[j].GetПроизведение;
                    });
                listY.Add(urY);
            }
            return listY;
        }

        public List<Kombinacia> gradientСпуск(List<Peremennaya> peremens, List<Kombinacia> allur, List<List<ValuePeremen>> leanvalues)
        {
            List<Kombinacia> resur = new List<Kombinacia>();
            List<double> listY = getYvalue(peremens, allur, leanvalues.GetRange(0,10000));

            return resur;
        }
    }
}
