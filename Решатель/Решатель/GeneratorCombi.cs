using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Решатель
{
    class GeneratorCombi
    {
        private MainWindow mainWindow;
        public GeneratorCombi(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        private Peremennaya addNewPer(string name)
        {
            Peremennaya p = new Peremennaya();
            p.setName(name);
            p.setIfKategor(false);
            return p;
        }
        private Peremennaya addNewKategor(string name, List<string> list)
        {
            Peremennaya p = new Peremennaya();
            p.setName(name);
            p.setIfKategor(true);
            List<KategorPeremen> qlist = new List<KategorPeremen>();
            foreach (var str in list)
            {
                KategorPeremen kat = new KategorPeremen();
                kat.setName(str);
                qlist.Add(kat);
            }
            p.setListKat(qlist);
            return p;
        }
        public List<Peremennaya> getPeremens(string path)
        {
            List<Peremennaya> massiv_переменных = new List<Peremennaya>();
            Peremennaya per = new Peremennaya();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            int i = 0;
            foreach (XmlNode nod in doc.DocumentElement)
            {
                i++;
                if (nod.Name == "number")
                    massiv_переменных.Add(addNewPer("X" + i.ToString()));
                else
                {
                    List<string> valueName = new List<string>();
                    foreach (XmlNode child in nod.ChildNodes)
                        if (child.Name == "value")
                            valueName.Add(child.InnerText);
                    massiv_переменных.Add(addNewKategor("X" + i.ToString(), valueName));
                }
            }
            return massiv_переменных;
        }

        List<Kombinacia> getOneStepen(List<Peremennaya> listPeremens)
        {
            List<Kombinacia> onest = new List<Kombinacia>();
            for (int i = 0; i < listPeremens.Count; i++)
            {
                Kombinacia k = new Kombinacia();
                if (listPeremens[i].getIfKategor())
                {
                    for (int j = 0; j < listPeremens[i].getListKat().Count-1; j++)
                    {
                        k = new Kombinacia();
                        k.setPeremens(1, listPeremens[i], null, null, j, -1, -1);
                        onest.Add(k);
                    }
                }
                else
                {
                    k.setPeremens(1, listPeremens[i], null, null, -1, -1, -1);
                    onest.Add(k);
                }
            }
            return onest;
        }

        List<Kombinacia> getTwoStepen(List<Peremennaya> listPeremens)
        {
            List<Kombinacia> twostepen = new List<Kombinacia>();
            for (int i = 0; i < listPeremens.Count; i++) //цикл по первой переменной
            {
                if (listPeremens[i].getIfKategor())
                {
                    for (int j = 0; j < listPeremens[i].getListKat().Count-1; j++)
                    {
                        for (int k = i + 1; k < listPeremens.Count; k++) //цикл по второй переменной
                        {
                            if (listPeremens[k].getIfKategor())
                            {
                                for (int l = 0; l < listPeremens[k].getListKat().Count; l++)
                                {
                                    Kombinacia kombo = new Kombinacia();
                                    kombo.setPeremens(2, listPeremens[i], listPeremens[k], null, j, l, -1);
                                    twostepen.Add(kombo);
                                }
                            }
                            else
                            {
                                Kombinacia kombo = new Kombinacia();
                                kombo.setPeremens(2, listPeremens[i], listPeremens[k], null, j, -1, -1);
                                twostepen.Add(kombo);
                            }
                        }
                    }
                }
                else
                {
                    for (int j = i; j < listPeremens.Count; j++)
                    {
                        if (listPeremens[j].getIfKategor())
                        {
                            for (int p = 0; p < listPeremens[j].getListKat().Count-1; p++)
                            {
                                Kombinacia k = new Kombinacia();
                                k.setPeremens(2, listPeremens[i], listPeremens[j],
                                               null, -1, p, -1);
                                twostepen.Add(k);
                            }
                        }
                        else
                        {
                            Kombinacia k = new Kombinacia();
                            k.setPeremens(2, listPeremens[i], listPeremens[j], null, -1, -1, -1);
                            twostepen.Add(k);
                        }
                    }
                }
            }
            return twostepen;
        }

        List<Kombinacia> getThreeStepenAdd3(List<Peremennaya> listPeremens, int i, int j, int sled, int i1, int j1)
        {
            List<Kombinacia> threestep = new List<Kombinacia>();
            for (int k = sled; k < listPeremens.Count; k++)
            {
                if (listPeremens[k].getIfKategor())
                {
                    for (int p = 0; p < listPeremens[k].getListKat().Count-1; p++)
                    {
                        Kombinacia kombo = new Kombinacia();
                        kombo.setPeremens(3, listPeremens[i], listPeremens[j], listPeremens[k],
                                       i1, j1, p);
                        threestep.Add(kombo);
                    }
                }
                else
                {
                    Kombinacia kombo = new Kombinacia();
                    kombo.setPeremens(3, listPeremens[i], listPeremens[j], listPeremens[k],
                                   i1, j1, -1);
                    threestep.Add(kombo);
                }
            }
            return threestep;
        }

        List<Kombinacia> getThreeStepenAdd2(List<Peremennaya> listPeremens, int i, int sled, int i1)
        {
            List<Kombinacia> threestep = new List<Kombinacia>();
            for (int j = sled; j < listPeremens.Count; j++)
            {
                if (listPeremens[j].getIfKategor())
                {
                    for (int k = 0; k < listPeremens[j].getListKat().Count-1; k++)
                        threestep.AddRange(getThreeStepenAdd3(listPeremens, i, j, j + 1, i1, k));
                }
                else
                {
                    threestep.AddRange(getThreeStepenAdd3(listPeremens, i, j, j, i1, -1));
                }
            }
            return threestep;
        }

        List<Kombinacia> getThreeStepen(List<Peremennaya> listPeremens)
        {
            List<Kombinacia> threestep = new List<Kombinacia>();
            for (int i = 0; i < listPeremens.Count; i++)
            {
                if (listPeremens[i].getIfKategor())
                {
                    for (int j = 0; j < listPeremens[i].getListKat().Count-1; j++)
                        threestep.AddRange(getThreeStepenAdd2(listPeremens, i, i + 1, j));
                }
                else
                {
                    threestep.AddRange(getThreeStepenAdd2(listPeremens, i, i, -1));
                }
            }
            return threestep;
        }

        public List<Kombinacia> GeneratorVars(List<Peremennaya> listPeremens)
        {
            //List<Peremennaya> listPeremens = getPeremens();
            List<Kombinacia> onest = getOneStepen(listPeremens);
            List<Kombinacia> twost = getTwoStepen(listPeremens);
            List<Kombinacia> threst = getThreeStepen(listPeremens);
            List<Kombinacia> allst = new List<Kombinacia>();
            allst.AddRange(onest);
            allst.AddRange(twost);
            allst.AddRange(threst);
            //List<List<ValuePeremen>> leanvalues = getLeanValueFromFile();
            //if (leanvalues.Count == 0)
            //    return null;
            //Gradientspusk grad = new Gradientspusk();
            //List<Kombinacia> res = grad.runGradientspusk(listPeremens, allst, leanvalues);
            //ProvY prov = new ProvY();
            //prov.runProv(listPeremens, allst);
            return allst;
        }

        public List<List<ValuePeremen>> getLeanValueFromFile(String path)
        {
            List<List<ValuePeremen>> list = new List<List<ValuePeremen>>();
            String[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                return list;
            }
            for (int i = 0; i < lines.Length; i++)
            {
                List<ValuePeremen> line = new List<ValuePeremen>();
                    String[] nowline = lines[i].Split(',');
                    nowline.ToList<String>().ForEach((x) => 
                    {
                        double r;
                        ValuePeremen v = new ValuePeremen();
                        if (getDouble(x,out r))
                        {
                            v.setKategor(false);
                            v.setDouble(r);
                        }
                        else
                        {
                            v.setKategor(true);
                            v.setValueKat(x);
                        }
                        line.Add(v);
                    });
                list.Add(line);
            }
            return list;
        }

        private bool getDouble(string tmp, out double res)
        {
            if (double.TryParse(tmp, out res))
                return true;
            else if (double.TryParse(tmp.Replace(",", "."), out res))
                return true;
            else if (double.TryParse(tmp.Replace(".", ","), out res))
                return true;
            else
            {
                //MessageBox.Show("Ошибка преобразования числового значения из файла обучения. Возможно в файле ошибка.", 
                //    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

    }
}
