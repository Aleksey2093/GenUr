using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Generation
{
    class variables
    {
        public int _type;
        public string _name;
        public string[] _value;
        public variables(string name, string val = "", int type =0)
        {
            _type = type;
            _name = name;
            string [] MyValue = val.Split('\n');
            List<string> lst = MyValue.ToList<string>();
            lst.RemoveAll(x => x=="");
            _value = lst.ToArray();
        }
    }
    class Generation
    {
        public static string generateXML(int countAllVar, int countCatVar, int minCatVar = 3, int maxCatVar = 4)
        {
            string result = "<variables>\n";
            Random rnd = new Random();
            int countAddCat = 0;
            for (int i = 0; i < countAllVar; i++)
            {
                if (countAddCat < countCatVar)
                {
                    if (rnd.Next(10) > 6)
                    {
                        int curCat = rnd.Next(minCatVar, maxCatVar + 1);
                        result += "\t<category>\n";
                        result += "\t\t<name>X" + (i + 1).ToString() + "</name>\n";
                        for (int k = 0; k < curCat; k++)
                            result += "\t\t<value>Val" + (k + 1).ToString() + "</value>\n";
                        result += "\t</category>\n";
                        countAddCat++;
                        continue;
                    }

                }
                result += "\t<number>X" + (i + 1).ToString() + "</number>\n";

            }
            result += "</variables>\n";
            return result;
        }

        public static List<variables> generateAllVariabls(string pathXML)
        {
            List<variables> lst = new List<variables>();

            var doc = new XmlDocument();
            doc.Load(pathXML);
            int i = 0;
            foreach (XmlNode node in doc.SelectNodes("variables"))
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name == "category")
                    {
                        string result = "";
                        foreach (XmlNode child2 in child.SelectNodes("value"))
                            result += child2.InnerText + "\n";
                        lst.Add(new variables(child.SelectNodes("name")[0].InnerText, result, 1));
                    }
                    else
                        if (child.InnerText == "")
                        lst.Add(new variables("X" + (i + 1).ToString()));
                    else
                        lst.Add(new variables(child.InnerText));
                    i++;
                }

            }
            return lst;
        }

        public static string generateVarRecurs(List<variables> lst, string prefix, int start, int lvl = 3)
        {
            string result = "";
            if (lvl == 0)
                return "";
            for (int k1 = start+1; k1 < lst.Count; k1++)
            {
                variables nd1 = lst[k1];
                if (nd1._type == 1)
                {
                    for (int i = 1; i < nd1._value.Length; i++)
                        result += prefix + nd1._name + "_" + i + " " + generateVarRecurs(lst, prefix + nd1._name + "_" + i + "*", k1, lvl-1) + " ";
                }
                else
                    result += prefix + nd1._name + " " + generateVarRecurs(lst, prefix + nd1._name + "*", k1, lvl-1) + " ";
            }
            return result.Trim();
        }
        public static string generateVar(List<variables> lst)
        {
            string result = "";
            for (int k1 = 0; k1 < lst.Count; k1++)
            {
                variables nd1 = lst[k1];
                int shift = 0;
                if (nd1._type == 1)
                {
                    shift++;
                    for (int i = 1; i < nd1._value.Length; i++)
                    {
                        result += nd1._name + "_" + i + " ";
                        for (int k2 = k1 + shift; k2 < lst.Count; k2++)
                        {
                            variables nd2 = lst[k2];
                            int shift2 = 0;
                            if (nd2._type == 1)
                            {
                                for (int i2 = 1; i2 < nd2._value.Length; i2++)
                                    result += nd1._name + "_" + i + "*" + nd2._name + "_" + i2 + " ";
                                shift2++;
                            }
                            else
                            {
                                result += nd1._name + "_" + i + "*" + nd2._name + " ";
                                for (int k3 = k2 + shift2; k3 < lst.Count; k3++)
                                {
                                    variables nd3 = lst[k3];
                                    int shift3 = 0;
                                    if (nd3._type == 1)
                                    {
                                        for (int i2 = 1; i2 < nd3._value.Length; i2++)
                                            result += nd1._name + "_" + i + "*" + nd2._name + "*" + nd3._name + "_" + i2.ToString();
                                        shift3++;
                                    }
                                    else
                                    {
                                        result += nd1._name + "_" + i + "*" + nd2._name + "*" + nd3._name;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    result += nd1._name + " ";
                    for (int k2 = k1 + shift; k2 < lst.Count; k2++)
                    {
                        variables nd2 = lst[k2];
                        int shift2 = 0;
                        if (nd2._type == 1)
                        {
                            for (int i = 1; i < nd2._value.Length; i++)
                                result += nd1._name + "*" + nd2._name + "_" + i + " ";
                            shift2++;
                        }
                        else
                        {
                            result += nd1._name + "*" + nd2._name + " ";
                        }
                    }
                }

                /*for (int k3 = k2+ shift2; k3 < lst.Count; k3++)
                {
                    variables nd3 = lst[k3];
                    if (nd3._type == 1)
                    {
                        for (int i = 1; i < nd3._value.Length; i++)
                            result += nd1._name + "*" + nd2._name + "*" + nd3._name + "_" + i + " ";
                    }
                    else
                    {
                        result += nd1._name + "*" + nd2._name + "*" + nd3._name + " ";
                    }
                }*/
                //}
            }

            return result;
        }

        public static string generateFormula(string allVariable)
        {
            string result = "Y = ";
            string[] arrVar = allVariable.Split(' ');
            var random = new Random(DateTime.Now.Millisecond);
            arrVar = arrVar.OrderBy(x => random.Next()).ToArray();
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < arrVar.Length / 4; i++)
            {
                result += ((double)rnd.Next(-5, 6) + Math.Round(rnd.NextDouble(),2)).ToString() + "*" + arrVar[i] + " + ";
            }
            return result.Trim().Trim('+');
        }

        public static void generateDataLearning(string fileName, string formula, List<variables> list, int countRow = 200000)
        {
            int allVar = 0;
            foreach (variables nd in list)
                if (nd._type == 0) allVar++;
                else allVar += nd._value.Length - 1;
            formula = formula.Trim();
            string buFormula = formula;
            for (int p = 0; p < countRow; p++)
            {
                formula = buFormula;
                double[] arrVal = new double[allVar];
                Array.ForEach(arrVal, x => x = 0);
                string[] arrName = new string[allVar];

                int ind = 0;
                Random rnd = new Random(DateTime.Now.Millisecond);
                foreach (variables nd in list)
                {
                    if (nd._type == 0)
                    {
                        arrVal[ind] = ((double)rnd.Next(-5, 6) + Math.Round(rnd.NextDouble(), 2));
                        arrName[ind] = nd._name;
                    }
                    else
                    {
                        arrVal[ind] = 0;
                        int myInd = 0;
                        for (int i = 1; i < nd._value.Length; i++)
                        {
                            arrName[ind + i - 1] = nd._name + "_" + i.ToString();
                            if (myInd == 0)
                            {
                                arrVal[ind + i - 1] = rnd.Next(2);
                                
                                if (arrVal[ind + i - 1] == 1)
                                {
                                    myInd++;
                                }
                            }
                                
                        }
                        ind += nd._value.Length - 2;
                    }
                    ind++;
                }

                for (int i = 0; i < allVar; i++)
                {
                    if(arrName[i] != null)
                        formula = formula.Replace(arrName[i], arrVal[i].ToString());
                }
                string[] arrSum = formula.Trim(" Y=".ToCharArray()).Split('+');
                double y = 0;
                for (int i = 0; i < arrSum.Length; i++)
                {
                    string[] arrMul = arrSum[i].Trim().Split('*');
                    double mul = 1;
                    for (int k = 0; k < arrMul.Length; k++)
                    {
                        mul *= Double.Parse(arrMul[k].Trim());
                    }
                    y += mul;
                }

                string data = "";
                string data_fin = "";
                int newInd = 0;
                foreach (variables nd in list)
                {
                    if (nd._type == 0)
                    {
                        data += arrVal[newInd].ToString().Replace(',','.') + ", ";
                    }
                    else
                    {
                        string catData = "";
                        for (int i = 1; i < nd._value.Length; i++)
                        {
                            if (arrVal[newInd + i - 1] == 1)
                                catData += nd._value[i];
                        }
                        if (catData == "")
                            data += nd._value[0] + ", ";
                        else
                            data += catData + ", ";
                        newInd += nd._value.Length - 2;
                    }
                    newInd++;
                }
                data_fin = data;
                data += y.ToString().Replace(',','.') + "\n";
                data_fin += (y+(y*rnd.Next(-10,11)/100)).ToString().Replace(',', '.') + "\n";
                File.AppendAllText(fileName+"_orig.csv", data);
                File.AppendAllText(fileName + "_fin.csv", data_fin);
            }
        }
    }
}