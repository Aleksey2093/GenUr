using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class Proiz
    {

        /// <summary>
        /// возвращает произведения по комбинациям без коэфф.
        /// </summary>
        /// <param name="pers">массив переменных</param>
        /// <param name="allkombi">все комбинации переменных</param>
        /// <param name="datafile">значения из файла</param>
        /// <param name="proiz">переменная в которую вернутся произведения</param>
        /// <param name="yfile">перемення в которую вернутся значения Y из файла</param>
        public void getProiz(List<Peremennaya> pers, List<Kombinacia> allkombi, List<List<ValueFile>> datafile, out double[][] proiz, out double[] yfile)
        {
            proiz = new double[datafile.Count][];
            yfile = new double[datafile.Count];
            for (int i = 0, j; i < datafile.Count; i++)
            {
                proiz[i] = new double[allkombi.Count+1];
                for (j=0;j<pers.Count;j++)
                {
                    if (pers[j].IfKategori)
                        pers[j].setKatValue(datafile[i][j].getValueKat());
                    else
                        pers[j].ValueDouble = datafile[i][j].getValueDob();
                }
                yfile[i] = datafile[i][j].getValueDob();
                proiz[i][0] = 1;
                for (j=0;j<allkombi.Count;j++)
                    proiz[i][j + 1] = allkombi[j].getProiz();
            }
            return;
        }
    }
}
