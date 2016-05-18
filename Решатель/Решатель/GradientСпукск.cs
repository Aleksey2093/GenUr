using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class GradientСпукск
    {
        private double getResUr(List<Kombinacia> urav)
        {
            double res = 0;
            for (int i = 0; i < urav.Count; i++)
            {
                try
                {
                    if (urav[i] != null)
                    res += urav[i].GetПроизведение;
                }
                catch
                {
                    Console.WriteLine("Нулевое зн найдено в столбце " + i);
                }
            }
            return res;
        }

        public double Gradient(List<Peremennaya> listPeremen, List<Kombinacia> uravnenie, List<List<ValuePeremen>> valuesLean)
        {
            double err = 0; double res;
            for (int j = 0; j < listPeremen.Count; j++)
            {
                if (listPeremen[j].Kategor)
                {
                    listPeremen[j].setValueKategor(valuesLean[0][j].ValueKategor);
                }
                else
                {
                    listPeremen[j].ValueПеремен = valuesLean[0][j].ValueDouble;
                }
                res = getResUr(uravnenie);
                res = res - valuesLean[0][valuesLean[0].Count - 1].ValueDouble;
                res = Math.Abs(res);
            }
            for (int i = 1; i < valuesLean.Count; i++)
            {
                for (int j = 0; j < listPeremen.Count; j++)
                {
                    if (listPeremen[j].Kategor)
                    {
                        listPeremen[j].setValueKategor(valuesLean[i][j].ValueKategor);
                    }
                    else
                    {
                        listPeremen[j].ValueПеремен = valuesLean[i][j].ValueDouble;
                    }
                }
                res = getResUr(uravnenie);
                res = res - valuesLean[i][valuesLean[0].Count - 1].ValueDouble;
                res = Math.Abs(res);
                err += res;
                err = err / 2;
            }
            return err;
        }
    }
}
