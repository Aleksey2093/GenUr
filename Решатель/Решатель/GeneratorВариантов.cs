﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Решатель
{
    class GeneratorВариантов
    {
        private List<Kombinacia> getПерваяСтепень(List<Peremennaya> listPeremen)
        {
            List<Kombinacia> oneСтепень = new List<Kombinacia>();
            foreach (var per in listPeremen)
            {
                Kombinacia item = new Kombinacia();
                if (per.Kategor)
                {
                    for (int i = 0; i < per.ValueКатегория.Count - 1; i++)
                    {
                        item = new Kombinacia();
                        item.koef = 1;
                        item.stepengen = 1;
                        item.onePer = per;
                        item.oneValueNumbler = i + 1;
                        oneСтепень.Add(item);
                    }
                }
                else
                {
                    item.koef = 1;
                    item.stepengen = 1;
                    item.onePer = per;
                    oneСтепень.Add(item);
                }
            }
            return oneСтепень;
        }

        private Kombinacia addВтороеЧисло(Peremennaya per, int i, Peremennaya per2, int j)
        {
            Kombinacia item = new Kombinacia();
            item.koef = 1;
            item.onePer = per;
            item.oneValueNumbler = i;
            item.stepengen = 2;
            item.twoPer = per2;
            item.twoValueNumbler = j;
            return item;
        }

        private List<Kombinacia> getВтораяСтепень(List<Peremennaya> listPeremen)
        {
            List<Kombinacia> twoСтепень = new List<Kombinacia>();
            for (int index = 0; index < listPeremen.Count; index++)
            {
                var per = listPeremen[index];
                if (per.Kategor)
                {
                    for (int i = 0; i < per.ValueКатегория.Count - 1; i++)
                    {
                        for (int index2 = index; index2 < listPeremen.Count; index2++)
                        {
                            var per2 = listPeremen[index2];
                            if (per2.Kategor == true && per.Name != per2.Name)
                            {
                                for (int j = 0; j < per.ValueКатегория.Count - 1; j++)
                                {
                                    twoСтепень.Add(addВтороеЧисло(per, i + 1, per2, j + 1));
                                }
                            }
                            else if (per2.Kategor == false)
                            {
                                twoСтепень.Add(addВтороеЧисло(per, i + 1, per2, -1));
                            }
                        }
                    }
                }
                else
                {
                    for (int index2 = index; index2 < listPeremen.Count; index2++)
                    {
                        var per2 = listPeremen[index2];
                        if (per2.Kategor == true && per.Name != per2.Name)
                        {
                            for (int j = 0; j < per2.ValueКатегория.Count - 1; j++)
                            {
                                twoСтепень.Add(addВтороеЧисло(per, -1, per2, j + 1));
                            }
                        }
                        else if (per2.Kategor == false)
                        {
                            twoСтепень.Add(addВтороеЧисло(per, -1, per2, -1));
                        }
                    }
                }
            }
            return twoСтепень;
        }

        private List<Kombinacia> addТретьеЧисло(Peremennaya per, int i, Peremennaya per2, int j, int index, List<Peremennaya> listPemen)
        {
            List<Kombinacia> items = new List<Kombinacia>();
            for (int index3 = index; index3 < listPemen.Count; index3++)
            {
                var per3 = listPemen[index3];
                if (per3.Kategor && per3.Name != per.Name && per3.Name != per2.Name)
                {
                    for (int k = 0; k < per3.ValueКатегория.Count - 1; k++)
                    {
                        Kombinacia item = new Kombinacia();
                        item.koef = 1;
                        item.onePer = per;
                        item.oneValueNumbler = i;
                        item.stepengen = 3;
                        item.twoPer = per2;
                        item.twoValueNumbler = j;
                        item.threePer = per3;
                        item.threeValueNumbler = k + 1;
                        items.Add(item);
                    }
                }
                else if (per3.Kategor == false)
                {
                    Kombinacia item = new Kombinacia();
                    item.koef = 1;
                    item.onePer = per;
                    item.oneValueNumbler = i;
                    item.stepengen = 3;
                    item.twoPer = per2;
                    item.twoValueNumbler = j;
                    item.threePer = per3;
                    item.threeValueNumbler = -1;
                    items.Add(item);
                }
            }
            return items;
        }

        private List<Kombinacia> getТретьяСтепень(List<Peremennaya> listPeremen)
        {
            List<Kombinacia> threeСтепень = new List<Kombinacia>();
            for (int index = 0; index < listPeremen.Count; index++)
            {
                var per = listPeremen[index];
                if (per.Kategor)
                {
                    for (int i = 0; i < per.ValueКатегория.Count - 1; i++)
                    {
                        for (int index2 = index; index2 < listPeremen.Count; index2++)
                        {
                            var per2 = listPeremen[index2];
                            if (per2.Kategor == true && per.Name != per2.Name)
                            {
                                for (int j = 0; j < per.ValueКатегория.Count - 1; j++)
                                {
                                    threeСтепень.AddRange(addТретьеЧисло(per, i + 1, per2, j + 1, index, listPeremen));
                                }
                            }
                            else if (per2.Kategor == false)
                            {
                                threeСтепень.AddRange(addТретьеЧисло(per, i + 1, per2, -1, index, listPeremen));
                            }
                        }
                    }
                }
                else
                {
                    for (int index2 = index; index2 < listPeremen.Count; index2++)
                    {
                        var per2 = listPeremen[index2];
                        if (per2.Kategor == true && per.Name != per2.Name)
                        {
                            for (int j = 0; j < per2.ValueКатегория.Count - 1; j++)
                            {
                                threeСтепень.AddRange(addТретьеЧисло(per, -1, per2, j + 1, index, listPeremen));
                            }
                        }
                        else if (per2.Kategor == false)
                        {
                            threeСтепень.AddRange(addТретьеЧисло(per, -1, per2, -1, index, listPeremen));
                        }
                    }
                }
            }
            return threeСтепень;
        }

        public void GeneratorVariantov(List<Peremennaya> listPeremen)
        {
            List<Kombinacia> oneСтепень = new List<Kombinacia>();//формирование первой степени
            List<Kombinacia> twoСтепень = new List<Kombinacia>();//формирование второй степени
            List<Kombinacia> threeСтепень = new List<Kombinacia>();//фомирование третьей степени

            Parallel.Invoke(() =>
                {
                    oneСтепень = getПерваяСтепень(listPeremen);
                },
                () =>
                {
                    twoСтепень = getВтораяСтепень(listPeremen);
                },
                () =>
                {
                    threeСтепень = getТретьяСтепень(listPeremen);
                });
            List<Kombinacia> alllist = new List<Kombinacia>();
            alllist.AddRange(oneСтепень);
            alllist.AddRange(twoСтепень);
            alllist.AddRange(threeСтепень);
            GeneratorUravneniy(listPeremen, alllist);
        }

        private List<Uravnenie> getKombinaciaElementa(List<Kombinacia> allkombo, int dlina, int kolvar)
        {
            List<Uravnenie> urlist = new List<Uravnenie>();
            List<List<Kombinacia>> kombivar = new List<List<Kombinacia>>();
            Parallel.For(0,kolvar,(i,state)=>
            {
                List<Kombinacia> t = new List<Kombinacia>();
                for (int j=0;j<dlina;j++)
                {
                    t.Add(new Kombinacia());
                }
                kombivar.Add(t);
            });
            for (int j = 0; j < dlina; j++)
            {
                int indexstart = j, i = 0;
                while (i < kolvar)
                {
                    for (int k = indexstart; k < allkombo.Count; k++)
                    {
                        kombivar[i][j] = allkombo[k];
                        i++;
                    }
                }
            }
            for (int i = 0; i < kolvar;i++ )
            {
                Uravnenie ur = new Uravnenie();
                ur.list.AddRange(kombivar[i]);
                urlist.Add(ur);
            }
            return urlist;
        }

        private int getFactorial(int dlinna)
        {
            int res = 0;
            Parallel.For(1, dlinna + 1, (i, state) => { res += i; });
            return res;
        }

        private void GeneratorUravneniy(List<Peremennaya> listPeremen, List<Kombinacia> allkombo)
        {
            List<Uravnenie> urlist = new List<Uravnenie>();
            int dlina = listPeremen.Count;
            while (dlina < listPeremen.Count * 2)
            {
                int kolvovar = (int)Math.Pow(dlina, allkombo.Count);
                kolvovar = kolvovar - getFactorial(dlina);
                urlist.AddRange(getKombinaciaElementa(allkombo, dlina, kolvovar));
                dlina++;
            }
            Console.WriteLine("Комбинаций уравнения " + urlist.Count);
        }
    }
}
