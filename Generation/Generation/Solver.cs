using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Generation
{
    class Solver
    {
        List<variables> lst;
        double eps;
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
                        if(child.SelectNodes("name")[0] == null)
                            lst.Add(new variables("X" + (i + 1).ToString(), result, 1));
                        else
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

        public Solver(string fileName, double epsilon = 1)
        {
            lst = generateAllVariabls(fileName);
            eps = epsilon;
        }

        public string newGradient(string fileData)
        {
            string formula = "";
            string allVars; // = Generation.generateVar(lst);
            allVars = Generation.generateVarRecurs(lst, "", -1, 3);
            while (allVars.IndexOf("  ") > 0)
                allVars = allVars.Replace("  ", " ");
            string[] Vars = allVars.Split(' ');
            double[] Q = new double[Vars.Length];
            double[] oldQ = new double[Vars.Length];
            for (int i = 0; i < Q.Length; i++)
                Q[i] = 1;
            double lam = 0.0001; //0.0001;
            eps = 0.1;
            //Array.ForEach(oldQ, x => x = 1);
            string[] allData = File.ReadAllLines(fileData);
            int M = allData.Length;
            double oldQQuad = 0;
            double newQQuad = 0;
            Q[0] = 0;
            double delta = 0;
            double koefL = lam / (double)M;
            double[,] X = generateAllX(allData, allVars);
            double[] Y = getY(allData);
            double[] gradDelta = gradDeltaSumm(Q, allData, X, Y);
            //oldQQuad = funcErrJ(M, gradDeltaSumm(Q, allData, X, Y));
            //double[] oldGradDelta = new double[gradDelta.Length];
            //int iter = 0;
            do
            {
                //Array.Copy(Q, oldQ, Q.Length);
                //Array.Copy(gradDelta, oldGradDelta, gradDelta.Length);
                
                for (int k = 0; k < Q.Length; k++)
                    Q[k] = Q[k] - koefL * summDelta(X, gradDelta, k);
                gradDelta = gradDeltaSumm(Q, allData, X, Y);
                //= funcErrJ(M, oldGradDelta);
                oldQQuad = newQQuad;
                newQQuad = funcErrJ(M, gradDelta);
                
                /*
                if (iter > 0 && newQQuad > oldQQuad)
                {
                    lam /= 10;
                    koefL = lam / (double)M;
                    Array.Copy(oldQ, Q, Q.Length);
                    newQQuad = oldQQuad;
                    delta = eps + 1;
                    continue;
                }
                else
                    lam *= 2;
                */
                delta = Math.Abs(oldQQuad - newQQuad);
                //newQQuad = funcErrRegulJ(M, gradDelta, Q);
                //oldQQuad = funcErrRegulJ(M, oldGradDelta, oldQ);//= funcErrJ(M, oldGradDelta);
                //newQQuad = funcErrRegulJ(M, gradDelta, Q);//= funcErrJ(M, gradDelta);
            } while (delta > eps);
            for (int i = 0; i < Q.Length; i++)
                formula += string.Format("{0:0.000000}", Q[i]) + " ";
            formula = formula.Trim() + "\n";
            return formula;
        }

        private double funcErrJ(int M, double[] deltaY)
        {
            double result = 0;
            for (int i = 0; i < deltaY.Length; i++)
                result += Math.Pow(deltaY[i], 2);
            return (double)1 / ((double)2 * (double)M) * result;
        }

        private double funcErrRegulJ(int M, double[] deltaY, double []Q)
        {
            double result = 0;
            for (int i = 0; i < deltaY.Length; i++)
                result += Math.Pow(deltaY[i], 2);
            double alpha = 0.0001;
            double tmpRes = 0;
            for (int i = 0; i < Q.Length; i++)
                tmpRes += Math.Pow(Q[i], 2);
            tmpRes *= alpha;
            return (double)1 / ((double)2 * (double)M) * result + tmpRes;
        }

        private double summDelta(double[,] X, double[] delta, int k)
        {
            double result = 0;
            for (int i = 0; i < delta.Length; i++)
                result += X[i, k] * delta[i];
            return result;
        }
        private double[,] generateAllX(string[] allData, string allVars)
        {
            string[] vars = allVars.Trim().Split(' ');
            double[,] result = new double[allData.Length, vars.Length + 1];

            int allVar = 0;
            foreach (variables nd in lst)
                if (nd._type == 0) allVar++;
                else allVar += nd._value.Length - 1;
            double[] arrVal = new double[allVar];
            string[] arrName = new string[allVar];
            int ind = 0;
            int index = 0;
            foreach (variables nd in lst)
            {
                if (nd._type == 0)
                {
                    arrName[ind] = nd._name;
                }
                else
                {
                    for (int i = 1; i < nd._value.Length; i++)
                    {
                        arrName[ind + i - 1] = nd._name + "_" + i.ToString();
                    }
                    ind += nd._value.Length - 2;
                }
                ind++;
                index++;
            }

            for (int i = 0; i < allData.Length; i++)
            {
                string[] src = allData[i].Trim().Split(',');
                ind = 0;
                index = 0;
                foreach (variables nd in lst)
                {
                    if (nd._type == 0)
                    {
                        arrVal[ind] = double.Parse(src[index].Replace('.', ','));
                    }
                    else
                    {
                        for (int k = 1; k < nd._value.Length; k++)
                        {
                            arrVal[ind + k - 1] = 0;
                            if (src[index].Trim() == nd._value[k].Trim())
                                arrVal[ind + k - 1] = 1;
                        }
                        ind += nd._value.Length - 2;
                        //index += nd._value.Length - 2;
                    }
                    ind++;
                    index++;
                }
                string tmpAllVars = allVars;
                for (int z = 0; z < allVar; z++)
                {
                    if (arrName[z] != null)
                        tmpAllVars = tmpAllVars.Replace(arrName[z], arrVal[z].ToString());
                }
                vars = tmpAllVars.Trim().Split(' ');
                for (int z = 0; z < vars.Length + 1; z++)
                    result[i, z] = 1;
                for (int z = 0; z < vars.Length; z++)
                {
                    string[] myData = vars[z].Trim().Split('*');
                    for (int k = 0; k < myData.Length; k++)
                        result[i, 1 + z] *= double.Parse(myData[k]);
                }

            }
            return result;

        }

        private double[] getY(string[] allData)
        {
            double[] result = new double[allData.Length];
            int i = 0;
            foreach (string str in allData)
                result[i++] = double.Parse(str.Split(',').Last().Replace('.', ','));
            return result;
        }
        private double[] gradDeltaSumm(double[] Q, string[] learnData, double[,] allX, double[] allY)
        {
            double[] result = new double[learnData.Length];

            for (int j = 0; j < learnData.Length; j++)
            {
                double tmpResult = 0;
                for (int i = 0; i < Q.Length; i++)
                {
                    tmpResult += Q[i] * allX[j, i];
                }
                result[j] += (tmpResult - allY[j]);
            }
            return result;
        }

        public static string gradeY(double []Y, double []myY)
        {
            double procent = 0;
            string otvet = "";
            for (int i = 0; i < Y.Length; i++)
            {
                procent = Math.Abs(Y[i] - myY[i]) / Math.Abs(Y[i]) * 100;
                otvet += procent.ToString() + " \r\n";
            }
            procent /= Y.Length;
            return otvet;
        }

        public string findFormula(string fileData)
        {
            string formula = "";
            string allVars = Generation.generateVar(lst);
            string[] Vars = allVars.Split(' ');
            double[] Q = new double[Vars.Length];
            double[] oldQ = new double[Vars.Length];
            for (int i = 0; i < Q.Length; i++)
                Q[i] = 1;

            //Array.ForEach(Q, x => x = 1.0);
            double lam = 0.00001; //0.0001;
            //Array.ForEach(oldQ, x => x = 1);
            string[] allData = File.ReadAllLines(fileData);
            int M = allData.Length;
            double oldQQuad = 0;
            double newQQuad = 0;
            Q[0] = 0;
            double delta = 0;
            do
            {
                Array.Copy(Q, oldQ, Q.Length);
                //File.AppendAllText("myLog.log", String.Join(" ", Q) + "\n");
                Q[0] = Q[0] - ((lam / M) * myFunc(oldQ, allData, allVars));
                for (int k = 1; k < Q.Length; k++)
                {
                    Q[k] = Q[k] - ((lam / M) * myFunc(oldQ, allData, allVars, k));
                }
                oldQQuad = J(oldQ, allData, allVars);
                newQQuad = J(Q, allData, allVars);
                delta = Math.Abs(oldQQuad - newQQuad);
                //if (delta < lam)
                //   lam = delta;
            } while (delta>eps);
            for (int i = 0; i < Q.Length; i++)
                formula += string.Format("{0:0.000000}", Q[i]) + " ";
            formula = formula.Trim() + "\n";
            return formula;
        }

        public double[] searchY(string fileData, string Qs)
        {
            string allVars = Generation.generateVarRecurs(lst, "", -1, 3);// = Generation.generateVar(lst);
            while (allVars.IndexOf("  ") > 0)
                allVars = allVars.Replace("  ", " ");

            string[] Vars = allVars.Split(' ');
            double[] Q = new double[Vars.Length];
            int ind = 0;
            try
            {
                Array.ForEach(Qs.Trim().Split(' '), x => Q[ind++] = double.Parse(x.Replace('.', ',')));
            }
            catch { }
                string[] allData = File.ReadAllLines(fileData);
            double[] result = new double[allData.Length];
            for (int i = 0; i < allData.Length; i++)
            {
                //double tmp = 0;
                double[] X = generateX(allData[i], allVars);
                for (int k = 0; k < Q.Length; k++)
                    result[i] += Q[k] * X[k];   
            }
            return result;
        }
        private double[] generateX(string RowLearnData, string allVars)
        {
            
            int allVar = 0;
            foreach (variables nd in lst)
                if (nd._type == 0) allVar++;
                else allVar += nd._value.Length - 1;
            double[] arrVal =  new double[allVar];
            string[] src = RowLearnData.Split(',');

            string[] arrName = new string[allVar];
            int ind = 0;
            int index = 0;
            foreach (variables nd in lst)
            {
                if (nd._type == 0)
                {
                    arrName[ind] = nd._name;
                    arrVal[index] = double.Parse(src[index].Replace('.',','));
                }
                else
                {
                    for (int i = 1; i < nd._value.Length; i++)
                    {
                        arrName[ind + i - 1] = nd._name + "_" + i.ToString();
                        arrVal[ind + i - 1] = 0;
                        if (src[index].Trim() == nd._value[i].Trim())
                            arrVal[ind + i - 1] = 1;
                        
                    }
                    ind += nd._value.Length - 2;
                }
                ind++;
                index++;
            }

            for (int i = 0; i < allVar; i++)
            {
                if (arrName[i] != null)
                    allVars = allVars.Replace(arrName[i], arrVal[i].ToString());
            }
            string[] vars = allVars.Trim().Split(' ');
            double[] arrResult = new double[vars.Length + 1];
            for (int i = 0; i < arrResult.Length; i++)
                arrResult[i] = 1;
            //Array.ForEach(arrResult, x => x = 1);
            
            for (int i = 0; i < vars.Length; i++)
            {
                string[] myData = vars[i].Trim().Split('*');
                for (int k = 0; k < myData.Length; k++)
                    arrResult[1 + i] *= double.Parse(myData[k]);
            }

            return arrResult;
        }
        private double myFunc(double[] Q, string[] learnData, string allVars, int ind = 0)
        {
            double result = 0;
            
            for (int j = 0; j < learnData.Length; j++)
            {
                double[] X = generateX(learnData[j], allVars);
                double Y = double.Parse(learnData[j].Split(',').Last().Replace('.',','));
                double tmpResult = 0;
                for (int i = 0; i < Q.Length; i++)
                {
                    tmpResult += Q[i] * X[i];
                }
                result += (tmpResult - Y) * X[ind];
            }
            return result;
        }

        private double J(double[] Q, string []learnData, string allVars)
        {
            double result = 0;
            for (int j = 0; j < learnData.Length; j++)
            {
                double[] X = generateX(learnData[j], allVars);
                double Y = double.Parse(learnData[j].Split(',').Last().Replace('.',','));
                double tmpResult = 0;
                for (int i = 0; i < Q.Length; i++)
                {
                    tmpResult += Q[i] * X[i];
                }
                result += Math.Pow((tmpResult - Y),2);
            }
            result *= (1.0/(2.0 * (double)learnData.Length));
            return result;
        }
    }
}
