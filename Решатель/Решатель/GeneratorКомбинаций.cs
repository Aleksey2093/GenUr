using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class GeneratorКомбинаций
    {
        private List<Peremennaya> listpers;
        private int stepen;
        /// <summary>
        /// генератор комбинаций уравнения
        /// </summary>
        /// <param name="list">список переменных</param>
        public GeneratorКомбинаций(List<Peremennaya> list)
        {
            this.listpers = list;
            DialogQ d = new DialogQ();
            d.setText("Введите максимальную степень уравнения");
            d.ShowDialog();
            this.stepen = d.getValueInt();
        }

        /// <summary>
        /// генерирует первую степень комбинаций
        /// </summary>
        /// <returns></returns>
        private List<Kombinacia> genOneSt()
        {
            List<Kombinacia> list = new List<Kombinacia>();
            for (int i = 0; i < listpers.Count; i++)
            {
                if (listpers[i].IfKategori)
                {
                    for (int j = 0; j < listpers[i].getCountKat(); j++)
                    {
                        List<Peremennaya> p = new List<Peremennaya>();
                        List<int> ns = new List<int>();
                        p.Add(listpers[i]);
                        ns.Add(j);
                        Kombinacia k = new Kombinacia();
                        k.Create(p, ns);
                        list.Add(k);
                    }
                }
                else
                {
                    List<Peremennaya> p = new List<Peremennaya>();
                    List<int> j = new List<int>();
                    p.Add(listpers[i]);
                    j.Add(-1);
                    Kombinacia k = new Kombinacia();
                    k.Create(p, j);
                    list.Add(k);
                }
            }
            return list;
        }

        /// <summary>
        /// генерация комбинации второй степени по первому числу со всеми
        /// </summary>
        /// <param name="index1">индекс первого числа</param>
        /// <param name="num1">номер переменной типа категория</param>
        /// <param name="index2">индекс числа начиная с которого генерируем</param>
        /// <returns></returns>
        private List<Kombinacia> addKombo2perTwoStep(int index1, int num1, int index2)
        {
            List<Kombinacia> list = new List<Kombinacia>();
            var p1 = listpers[index1];
            for (int i = index2; i < listpers.Count; i++)
            {
                var p2 = listpers[i];
                if (p2.IfKategori)
                {
                    for (int j = 0; j < p2.getCountKat(); j++)
                    {
                        Kombinacia k = new Kombinacia();
                        List<Peremennaya> pl = new List<Peremennaya>();
                        List<int> numbers = new List<int>();
                        pl.Add(p1); pl.Add(p2);
                        numbers.Add(num1); numbers.Add(j);
                        k.Create(pl, numbers);
                        list.Add(k);
                    }
                }
                else
                {
                    Kombinacia k = new Kombinacia();
                    List<Peremennaya> pl = new List<Peremennaya>();
                    List<int> numbers = new List<int>();
                    pl.Add(p1); pl.Add(p2);
                    numbers.Add(num1); numbers.Add(-1);
                    k.Create(pl, numbers);
                    list.Add(k);
                }
            }
            return list;
        }

        /// <summary>
        /// генерирует вторую степень комбинаций
        /// </summary>
        /// <returns></returns>
        private List<Kombinacia> genTwoSt()
        {
            List<Kombinacia> list = new List<Kombinacia>();

            for (int i = 0; i < listpers.Count; i++)
            {
                var p1 = listpers[i];
                if (p1.IfKategori)
                {
                    for (int j = 0; j < p1.getCountKat(); j++)
                        list.AddRange(addKombo2perTwoStep(i, j, i + 1));
                }
                else
                {
                    list.AddRange(addKombo2perTwoStep(i, -1, i));
                }
            }
            return list;
        }

        /// <summary>
        /// возвращает комбинацию третей степени
        /// </summary>
        /// <param name="index1">индекс первой переменной</param>
        /// <param name="num1">номер переменной в категории или -1, если не категория</param>
        /// <param name="index2">индекс второй переменной</param>
        /// <param name="num2">номер переменной в категории или -1, если не категория</param>
        /// <param name="index3">индекс третьей переменной с которой начинается генерация</param>
        /// <returns></returns>
        private List<Kombinacia> addKombo3per3St(int index1, int num1, int index2, int num2, int index3)
        {
            List<Kombinacia> list = new List<Kombinacia>();
            var p1 = listpers[index1];
            var p2 = listpers[index2];
            for (int i = index3; i < listpers.Count; i++)
            {
                var p3 = listpers[i];
                if (p3.IfKategori)
                {
                    for (int j = 0; j < p3.getCountKat(); j++)
                    {
                        Kombinacia k = new Kombinacia();
                        List<Peremennaya> pl = new List<Peremennaya>();
                        List<int> nums = new List<int>();
                        pl.Add(p1); pl.Add(p2); pl.Add(p3);
                        nums.Add(num1); nums.Add(num2); nums.Add(j);
                        k.Create(pl, nums);
                        list.Add(k);
                    }
                }
                else
                {
                    Kombinacia k = new Kombinacia();
                    List<Peremennaya> pl = new List<Peremennaya>();
                    List<int> nums = new List<int>();
                    pl.Add(p1); pl.Add(p2); pl.Add(p3);
                    nums.Add(num1); nums.Add(num2); nums.Add(-1);
                    k.Create(pl, nums);
                    list.Add(k);
                }
            }
            return list;
        }

        /// <summary>
        /// возвращает комбинации третьей степени
        /// </summary>
        /// <param name="index1">индекс первой переменной</param>
        /// <param name="num1">номер значения в категории, если не категория, то -1</param>
        /// <param name="index2">индекс второй переменной с которой начинаем генерацию</param>
        /// <returns></returns>
        private List<Kombinacia> addKombo2per3St(int index1, int num1, int index2)
        {
            List<Kombinacia> list = new List<Kombinacia>();

            for (int i = index2; i < listpers.Count; i++)
            {
                var p2 = listpers[i];
                if (p2.IfKategori)
                {
                    for (int j = 0; j < p2.getCountKat(); j++)
                    {
                        list.AddRange(addKombo3per3St(index1, num1, i, j, i + 1));
                    }
                }
                else
                {
                    list.AddRange(addKombo3per3St(index1, num1, i, -1, i));
                }
            }

            return list;
        }

        /// <summary>
        /// возвращает комбинации третьей степени
        /// </summary>
        /// <returns></returns>
        private List<Kombinacia> gen3St()
        {
            List<Kombinacia> list = new List<Kombinacia>();
            for (int i = 0; i < listpers.Count; i++)
            {
                var p1 = listpers[i];
                if (p1.IfKategori)
                {
                    for (int j = 0; j < p1.getCountKat(); j++)
                        list.AddRange(addKombo2per3St(i, j, i + 1));
                }
                else
                {
                    list.AddRange(addKombo2per3St(i, -1, i));
                }
            }
            return list;
        }

        public List<Kombinacia> runGen()
        {
            List<Kombinacia> allst = new List<Kombinacia>();
            for (int i = 0; i < stepen; i++)
            {
                var tmp = getForKombiGen(0, 0, i, new Kombinacia());
                allst.AddRange(tmp);
            }
            /*var st1 = genOneSt();
            var st2 = genTwoSt();
            var st3 = gen3St();
            allst.AddRange(st1);
            allst.AddRange(st2);
            allst.AddRange(st3);
            for (int i = 0; i < allst.Count; i++)
            {
                allst[i].printKombo();   
            }*/
            return allst;
        }

        private List<Kombinacia> getForKombiGen(int index, int nowst, int st, Kombinacia kombo)
        {
            List<Kombinacia> list = new List<Kombinacia>();
            if (nowst < st)
            {
                for (int i = index; i < listpers.Count; i++)
                {
                    if (listpers[i].IfKategori)
                    {
                        for (int j = 0; j < listpers[i].getCountKat(); j++)
                        {
                            Kombinacia tmpkombo = new Kombinacia(kombo);
                            tmpkombo.Create(listpers[i], j);
                            var tmp = getForKombiGen(i + 1, nowst + 1, st, tmpkombo);
                            list.AddRange(tmp);
                        }
                    }
                    else
                    {
                        Kombinacia tmpkombo = new Kombinacia(kombo);
                        tmpkombo.Create(listpers[i]);
                        var tmp = getForKombiGen(i, nowst + 1, st, tmpkombo);
                        list.AddRange(tmp);
                    }
                }
            }
            else if (nowst == st)
            {
                for (int i = index; i < listpers.Count; i++)
                {
                    if (listpers[i].IfKategori)
                    {
                        for (int j = 0; j < listpers[i].getCountKat(); j++)
                        {
                            Kombinacia tmpkombo = new Kombinacia(kombo);
                            tmpkombo.Create(listpers[i], j);
                            list.Add(tmpkombo);
                        }
                    }
                    else
                    {
                        Kombinacia tmpkombo = new Kombinacia(kombo);
                        tmpkombo.Create(listpers[i]);
                        list.Add(tmpkombo);
                    }
                }
            }
            else
            {
                Console.WriteLine("Текущая комбинация больше максимальной. Как код сюда попал????");
            }
            return list;
        }
    }
}
