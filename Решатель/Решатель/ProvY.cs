using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class ProvY
    {


        internal double[][] runProv(List<Peremennaya> listPeremens, List<Kombinacia> resUr, double[] koef)
        {
            double[] yfile = new double[1];
            double[][] proiz = new double[1][];
            try
            {
                proiz = getProiz(listPeremens, resUr, koef, out yfile);
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            int len = 100;
            if (proiz.Length < 100)
                len = proiz.Length;
            double[][] list = new double[len][];
            Parallel.For(0, len, (i, state) =>
            {
                double[] tmp = new double[3];
                double Y = 0;
                for (int j = 0; j < proiz[i].Length; j++)
                    Y += proiz[i][j];
                tmp[0] = Y;
                tmp[1] = yfile[i];
                tmp[2] = Math.Abs(tmp[0] - tmp[1]) / yfile[i];
                tmp[2] = Math.Abs(tmp[2]);
                while (tmp[2] > 0.01)
                    tmp[2] *= 0.01;
                list[i] = tmp;
            });
            return list;
        }

        private double[][] getProiz(List<Peremennaya> listPeremens, List<Kombinacia> resUr, double[] koef, out double[] yfile)
        {
            List<List<ValueFile>> list = new List<List<ValueFile>>();
            list = new Fileload().getDataFile();
            double[][] proiz = new double[list.Count][];
            yfile = new double[list.Count];
            for (int i = 0, j; i < proiz.Length;i++ )
            {
                proiz[i] = new double[koef.Length];
                for (j = 0; j < listPeremens.Count; j++)
                    if (listPeremens[j].IfKategori)
                        listPeremens[j].setKatValue(list[i][j].getValueKat());
                    else
                        listPeremens[j].ValueDouble = list[i][j].getValueDob();
                yfile[i] = list[i][j].getValueDob();
                proiz[i][0] = 1 * koef[j + 1];
                for (j = 0; j < resUr.Count; j++)
                    proiz[i][j + 1] = resUr[j].getProiz() * koef[j+1];
            }
            return proiz;
        }
    }
}
