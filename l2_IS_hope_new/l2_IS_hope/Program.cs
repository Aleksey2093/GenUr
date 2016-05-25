using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace l2_IS_hope
{
    class Program
    {
        private static IEnumerable<int> constructSetFromBits(int i)
        {
            for (int n = 0; i != 0; i /= 2, n++)
            {
                if ((i & 1) != 0)
                    yield return n;
            }
        }
        private static List<Variable> vList = new List<Variable>();
        private static List<Operand> operands = new List<Operand>();
        private static List<Operand> operandsGen = new List<Operand>();
        private static List<Computation> computations = new List<Computation>();
        private static List<List<Operand>> HypotesisAll = new List<List<Operand>>();
        public static IEnumerable<List<Operand>> produceEnumeration()
        {
            for (int i = 0; i < (1 << operands.Count); i++)
            {
                yield return
                    constructSetFromBits(i).Select(n => operands[n]).ToList();
            }
        }
        public static double GetMedian(double[] sourceNumbers)
        {
            //Framework 2.0 version of this method. there is an easier way in F4        
            if (sourceNumbers == null || sourceNumbers.Length == 0)
                throw new System.Exception("Median of empty array not defined.");

            //make sure the list is sorted, but use a new array
            double[] sortedPNumbers = (double[])sourceNumbers.Clone();
            Array.Sort(sortedPNumbers);

            //get the median
            int size = sortedPNumbers.Length;
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;
            return median;
        }
        public static double[] GetAnswers(string file)
        {
            string[] strs = File.ReadAllLines(file);
            char[] separators = { ',', ' ', '\n' };
            double[] Y = new double[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                string[] elements = strs[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                Y[i] = Convert.ToDouble(elements[elements.Length - 1], CultureInfo.InvariantCulture);
            }
            return Y;
        }
        public static List< double> getDelta(List<Operand> H)
        {

            List <double> sum = new List< double> ();
            
            foreach (var C in computations)
            {
                C.operands = H;
                 C.CalculateWithoutQ0();
                sum.Add( C.delta);
            }
            return sum;
        }
        public static double getDeltaAbs(List<Operand> H)
        {
            double sum = 0;
            foreach (var C in computations)
            {
                C.operands = H;
                sum += Math.Abs(C.Calculate() - C.noisedY);

            }
            return sum;
        }
        public static double getPers(List<Operand> H)
        {
            double sum = 0;
            foreach (var C in computations)
            {
                C.operands = H;
                sum += Math.Abs((C.Calculate()- C.noisedY) / C.noisedY);

            }
            return sum/computations.Count;
        }
        public static double getAllYAbs()
        {
            double sum = 0;
            foreach (var C in computations)
            {
                
                sum += Math.Abs( C.noisedY);

            }
            return sum;
        }
        public static double getMul_c_old(Operand part_H,List<double> delta)
        {
            List<Operand> H = new List<Operand>();
            H.Add(part_H);
            double sum = 0;
            int counter = 0;
            foreach (var C in computations)
            {
                C.operands = H;
                sum += C.Calculate_old()*delta[counter++];
            }
            return sum;
        }

        public static void newGradient()
        {
           // Dictionary<ListO, int> dictionary =new Dictionary<string, int>();
            double lam = 0.0000001; 
            double eps = 1;
            //var stats = ;
                                  
            double[] delta ;
            int M = computations.Count;
            double koefL = lam / M;
            System.IO.StreamWriter file = new System.IO.StreamWriter("logs.csv");
            file.WriteLine("All Y" + getAllYAbs());
            int hypNumb = 0;
            int minNumb = 0;
            double minumum = double.MaxValue;
            foreach (var H in HypotesisAll)
            {
              
                H[0].old_coeff = 1; 
                int counter = 0;
                double J = 0; double oldJ ;
                var deltaF = getDelta(H);
                //var dl = deltaF.ToList();
                J=deltaF.Select(el => el * el).Sum()/ (2*M);

                //Array.ForEach(deltaF, el => el * el); 
                double old_del = double.MaxValue;
                double new_del = 0;
                do {
                    oldJ = J;

                    List<double> ld = new List<double>();
                    foreach (var Opr in H)
                    {
                        //var comp = new Computation(H, oldQ);
                        double tmp = Opr.old_coeff;
                        Opr.old_coeff = 1;
                        double deltaOpr = getMul_c_old(Opr,deltaF);
                        Opr.old_coeff = tmp;
                        ld.Add(deltaOpr);
                        Opr.coeff = Opr.old_coeff - koefL * deltaOpr;
                        //foreach ()
                        //    var comp = new Computation(H, oldQ);
                    }

                    foreach (var Opr in H)
                    {                      
                        Opr.old_coeff = Opr.coeff;                       
                    }
                    //computations.First().operands = H;
                    

                        //counter =counter;

                    deltaF = getDelta(H);
                    
                    J = deltaF.Select(el => el * el).Sum() / (2 * M);
                    if (counter % 100 == 0)
                        Console.WriteLine("lam - "+lam+"\nDelta - " + Math.Abs(oldJ - J));
                    //if (Math.Abs(deltaF-delta) < 0.1)
                    //  break;

                   
                } while (Math.Abs(oldJ - J) > eps) ;// Console.WriteLine("found!!!");
                double delta_Abs_V = getDeltaAbs(H);
                if (delta_Abs_V<minumum)
                {
                    minumum = delta_Abs_V;
                        minNumb = hypNumb;
                }
                Console.WriteLine("-----------------------");
                Console.WriteLine("Delta" +deltaF);
                string sOut=""+hypNumb+";";
                foreach (var obj1 in H)
                {
                    sOut=sOut+obj1 + "+";
                }

                Console.WriteLine(sOut);
               
                Console.WriteLine("-----------------------");

                
                file.WriteLine(deltaF+";"+ delta_Abs_V + ";"+sOut);


                if (hypNumb==8)
                    Console.WriteLine("test");
                hypNumb++;

                // if (Math.Abs(delta) < 0.8) break;
            }
           
            hypNumb--;

            double[] med = new double[computations.Count];
            var arrY = GetAnswers("test_with_answers.csv");
            for (int i = 0; i < computations.Count; i++)
            {
                med[i] = Math.Abs((arrY[i] - computations[i].Value) / arrY[i]);
                computations[i].realY = arrY[i];
            }
            Console.WriteLine("Median - " + GetMedian(med));
            file.WriteLine("Median - " + GetMedian(med));
            //GetAnswers
            double medL = med.ToList().Sum()/M;
            Console.WriteLine("Srednee - "+ medL);

            file.WriteLine("Srednee - " + medL);
            file.Close();

            using (StreamWriter sw = new StreamWriter("generated_answers.csv", false, System.Text.Encoding.Default))
            {
                //StringBuilder sb = new StringBuilder("");
                foreach (var com in computations)
                {
                    com.operands = HypotesisAll[hypNumb];
                    StringBuilder sb = new StringBuilder("");
                    foreach (var v in vList)
                    {
                        if (v.IsCategory)
                        {
                            if (com.values[v] == 1)
                            {
                                sb.Append(v.StringValue);
                                sb.Append(";");
                            }

                        }
                        else
                        {
                            sb.Append(com.values[v]);
                            sb.Append(";");
                        }
                    }
                    double tmpres = com.Value;
                    sb.Append(tmpres);
                    sb.Append(";"+com.noisedY);
                    //sb.Append("\n");
                    sw.WriteLine(sb);
                }
                //   sw.Write(sb);
            }


        }
        static void Main(string[] args)
        {
            //открыть хмл
            var xml = XDocument.Load("vars.xml");//"D:\\DyuminL2\\DyuminL2\\bin\\Release\\var3.xml");            
           // var numbers = from c in xml.Descendants("number")                          select new { name = c.Value };
           // var categories = from c in xml.Descendants("category")                             select new { name = c.Element("name").Value, values = c.Elements("value").Select(l => l.Value).ToList() };
            var varsXML = xml.Descendants("variables");


            //заполнить Variable
            int numbCount = 2;
            foreach (var item in varsXML.Elements())
            {
               // if (item.NodeType.ToString== "number")
                Console.WriteLine(item.Name);
                // var numbers = from c in item.Document.Descendants("number") select new { name = c.Value };
                // var categories = from c in item.Document.Descendants("category") select new { name = c.Element("name").Value, values = c.Elements("value").Select(l => l.Value).ToList() };
                if (item.Name == "number")
                {
                    var v = new Variable() { IsCategory = false };
                    if (item.Value == "") v.Name = "X" + numbCount;
                        else v.Name = item.Value;
                    vList.Add(v);
                    numbCount++;
                }
                else
                {
                    Console.WriteLine(item.Name);
                    var values = item.Elements("value").Select(l => l.Value).ToList();
                    foreach (var value in values.Select((value, i) => new { i, value }))
                    {
                        var vc = new Variable
                        {
                            Values = values,
                            CategoryIndex = value.i,
                            Name = item.Name.ToString(),
                            IsCategory = true
                        };
                        vList.Add(vc);
                        Console.WriteLine(value);
                    }
                }      
            }
            //генерация операндов
            var rand = new Random();
            operands.Clear();
                      
            var t = operands.Count(el => el.var1 == null);
            //operands = vList.SelectMany(op1 => vList.SelectMany(op2 => vList.Select(op3 => new Operand(op1, op2, op3)))).ToList();
            operands.AddRange(vList.SelectMany(op1 => vList.Select(op2 => new Operand(op1, op2))));
            operands.AddRange(vList.Select(op => new Operand(op)));
            operands = operands.Distinct(new OperandComparer()).ToList();
            operands.RemoveAll(op => op.var1 == null);
            Operand Q0var=new Operand();
         //   Q0var.coeff = 0;
           // Q0var.old_coeff = 0;
            operands.Insert(0, Q0var);

            //operands//operands
            double c_mul = 1;
           /* operands.ForEach(op =>
            {                
                    op.coeff = 20*c_mul;
                c_mul *= 2;
                Console.WriteLine(op.coeff);
            });*/
            //var onr_not_null = operands.Where(op=>op.coeff!=0).ToList();
            Console.WriteLine("Operands");
            foreach (var obj1 in operands)
            {
                Console.WriteLine(obj1 + " ");
            }
            //Console.WriteLine("=");


            //генерация гипотез
           HypotesisAll.Add( operands);//produceEnumeration().ToList();
            //HypotesisAll=produceEnumeration().ToList(); HypotesisAll.RemoveAt(0);


            //загрузка learn.csv
            using (StreamReader sr = new StreamReader("learn.csv"))
            {
                List<string> lineS = new List<string>();
               // List<double> lineD = new List<double>();
                string line;
                int countFromFile = 0;
                int totalFromFile = 5000;
                while (((line = sr.ReadLine()) != null) )
                {
                    lineS = line.Split(',').ToList();
                    List<double> lineD = new List<double>();



                    //  StringBuilder sb = new StringBuilder("");
                    //foreach (var com in computations)
                    //{
                    //  StringBuilder sb = new StringBuilder("");



                    //foreach
                    int k = 0;
                    foreach (var v in vList)
                    {
                        if (v.IsCategory)
                        {
                            bool flag = true;
                            for (int j = 0; j < v.Values.Count; j++)
                            {

                                if (v.StringValue == lineS[k])
                                { lineD.Add(v.CategoryIndex); flag = false; k++; break; }

                            }
                            if (flag) lineD.Add(0);

                            k --;
                        }
                        else
                        {
                            double outParse = 0.0;
                            double.TryParse(lineS[k], System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out outParse);
                            lineD.Add(outParse);
                        }
                        k++;
                    }

                    var d = vList.Zip(lineD, (s, i) => new { s, i }).ToDictionary(item => item.s, item => item.i);
                    var comp = new Computation(HypotesisAll[0], d);
                    double outParseY = 0.0;
                    double.TryParse(lineS[k], System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out outParseY);
                    comp.noisedY = outParseY;
                    computations.Add(comp);
                }
            }


newGradient();
            Console.ReadLine();
        }

        class OperandComparer : IEqualityComparer<Operand>
        {
            public bool Equals(Operand x, Operand y)
            {
                if (Object.ReferenceEquals(x, y)) return true;

                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                return x.var1 == y.var1 && x.var2 == y.var2 && x.var3 == y.var3;
            }

            public int GetHashCode(Operand obj)
            {
                if (Object.ReferenceEquals(obj, null)) return 0;

                int hashProductName = obj.var1?.Name?.GetHashCode() ?? 0;

                return hashProductName;
            }
        }
        [Serializable]
        public class Variable
        {
            public string Name { get; set; }
            public bool IsCategory { get; set; }
            public int CategoryIndex
            {
                get { return Values.IndexOf(StringValue); }
                set
                {
                    if (value >= 0 && value <= Values.Count)
                        StringValue = Values[value];
                    else StringValue = "";
                }
            }

            public string StringValue { get; set; }
            public List<string> Values { get; set; }

            public Variable()
            {
                Values = new List<string>();
            }
            public override string ToString()
            {
                if (IsCategory) return StringValue;
                return Name;
            }
        }
        [Serializable]
        public class Operand
        {
            public Operand(Variable vr1, Variable vr2, Variable vr3)
            {
                var l = new List<Variable> { vr1, vr2, vr3 };
                l = l.OrderBy(e => e.Name).ToList();
                var1 = l[0];
                var2 = l[1];
                var3 = l[2];

                if (l.Where(e => e.IsCategory).Select(e => e.CategoryIndex).Count() > 1)
                {
                    var1 = var2 = var3 = null;
                }
            }
            public Operand(Variable vr1, Variable vr2)
            {
                var l = new List<Variable> { vr1, vr2 };
                l = l.OrderBy(e => e.Name).ToList();
                var1 = l[0];
                var2 = l[1];
                var3 = null;

                if (l.Where(e => e.IsCategory).Select(e => e.CategoryIndex).Count() > 1)
                {
                    var1 = var2 = var3 = null;
                }
            }
            public Operand(Variable vr1)
            {
                var1 = vr1;
                var2 = null;
                var3 = null;
            }
            public Operand()
            {
                var1 = null;
                var2 = null;
                var3 = null;
            }
            //public Operand() { }
            public Variable var1 { get; set; }
            public Variable var2 { get; set; }
            public Variable var3 { get; set; }
            public string operation { get; set; }
            public double coeff { get; set; } = 1;
            public double old_coeff { get; set; } = 1;
            public override string ToString()
            {
                if (var2 == null) return coeff + "*" + var1;
                if (var3 == null) return coeff + "*" + var1 + "*" + var2;
                return coeff + "*" + var1 + "*" + var2 + "*" + var3;
            }
        }

        [Serializable]
        public class Computation
        {
            public List<Operand> operands = new List<Operand>();
            public double Value { get; set; }
            public double delta { get; set; }
            public Dictionary<Variable, double> values = new Dictionary<Variable, double>();
            public double realY { get; set; }
            public double noisedY { get; set; } = 0.0;

            public Computation(List<Operand> ops, Dictionary<Variable, double> vals)
            {
                operands = ops;
                values = vals;
            }

            public double Calculate()
            {
                var res =
                    operands.Select(
                        op => op.coeff * (op.var1 !=null ? values[op.var1]:1 ) * (op.var2 != null ? values[op.var2] : 1) * (op.var3 != null ? values[op.var3] : 1)).Sum();
                Value = res;
                return res;
            }
            public double Calculate_old()
            {
                var res =

                    operands.Select(
                        op => op.old_coeff * (op.var1 != null ? values[op.var1] : 1) * (op.var2 != null ? values[op.var2] : 1) * (op.var3 != null ? values[op.var3] : 1)).Sum();
                Value = res;
                return res;
            }
            public double CalculateWithoutQ0()
            {
                var res =

                    operands.Skip(1)
                    .Select(
                        op => op.old_coeff * (op.var1 != null ? values[op.var1] : 1) * (op.var2 != null ? values[op.var2] : 1) * (op.var3 != null ? values[op.var3] : 1)).Sum();
                Value = res;
                delta = res - noisedY;
                return res;
            }
            

        }

    }
}
