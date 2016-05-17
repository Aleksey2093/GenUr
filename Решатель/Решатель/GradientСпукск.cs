using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class GradientСпукск
    {
        public double Gradient(List<Peremennaya> listPeremen, List<Kombinacia> uravnenie, List<List<ValuePeremen>> values)
        {
            List<double> yss = new List<double>();
            for (int i = 0; i < values.Count;i++ )
            {
                for (int j=0;j<values[i].Count;j++)
                {
                    if (listPeremen[j].Kategor == true)
                    {
                        listPeremen[j].setValueKategor(values[i][j].ValueKategor);
                    }
                    else
                    {
                        listPeremen[j].ValueПеремен = values[i][j].ValueDouble;
                    }
                }
            }
            return 1;
        }
    }
}
