using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Globalization;

namespace Gradient
{
    public partial class Qbox : Form
    {
        List<Var> variables;
        Label processLabel;
        double lambda = 0.0001;
        double epsilon = 0.01;
        public Qbox()
        {
            InitializeComponent();
            processLabel = new Label();
            processLabel.Location = new Point(5,30);
            processLabel.ForeColor = Color.Red;
            processLabel.Font = new Font("Arial",20);
            processLabel.Size = new Size(190,40);
            processLabel.Text = "Processing....";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            create_vars("vars.xml");
            List<Summand> summands = create_all_summands();
            double[] Ys;
            double[] Q = gradient_descent(summands, lambda, epsilon);
            List<Dictionary<string, double>> allX = read_data_from_file("test.csv", out Ys);
            Ys = ReadAnswerData("test_with_answer.csv");
            double[] deviations = new double[Ys.Length];
            string[] strings = new string[Ys.Length + Q.Length +1];
            double avr_deviation = 0;
            for (int i = 0; i < Q.Length; i++)
            {
                strings[i] = "Q[" + i + "] = " + Q[i];
                QsBox.Items.Add("Q[" + i + "] = " + Q[i].ToString());
            }
            for (int i = 0; i < Ys.Length; i++)
            {
                double res = FindY(summands, allX[i], Q);
                YBox.Items.Add(res.ToString());
                deviations[i] = Math.Abs((res - Ys[i]) / Ys[i]);
                avr_deviation += deviations[i];
                strings[i + Q.Length] = "Y: " + res.ToString() + ",\tDevation: " + deviations[i].ToString();

            }
            avr_deviation = avr_deviation / Ys.Length;
            mediumErr.Text = avr_deviation.ToString();
            Array.Sort(deviations);
            Median.Text = deviations[deviations.Length / 2].ToString();
            strings[strings.Length - 1] += "Average deviation: " + avr_deviation + "\nMedian: " + deviations[deviations.Length / 2];
            File.WriteAllLines("ProgramLog.txt", strings);
            EndDataBox.Text = DateTime.Now.TimeOfDay.ToString();
            this.Controls.Remove(processLabel);
        }
        
        void create_vars(string path)
        {

            XmlDocument document = new XmlDocument();
            variables = new List<Var>();
            document.Load(path);
            for (int i = 0; i < document.DocumentElement.ChildNodes.Count; i++)
            {

                XmlNode node = document.DocumentElement.ChildNodes[i];
                if (node.Name == "category")
                {
                    List<Value> states = new List<Value>();
                    for (int j = 0; j < node.ChildNodes.Count; j++)
                    {
                        if (node.ChildNodes[j].InnerText != "X" + (i + 1))
                        {
                            if (j != node.ChildNodes.Count - 1)
                            {
                               Value cur_state = new Value("X" + (i + 1) + "_" + (states.Count + 1), node.ChildNodes[j].InnerText);
                                states.Add(cur_state);
                            }
                        }
                    }
                    Category newCateg = new Category("X" + (i + 1), states);
                    variables.Add(newCateg);
                }
                else
                {
                    Number newNum = new Number("X" + (i + 1));
                    variables.Add(newNum);
                }
            }
        }
        List<Summand> create_all_summands()
        {
            Random rnd = new Random();
            List<Summand> summands = new List<Summand>();
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].GetType() == typeof(Number))
                {
                    Summand new_oper = new Summand(1, variables[i]);
                    summands.Add(new_oper);
                }
                else
                {
                    foreach (Value cur_state in ((Category)variables[i]).states)
                    {
                        Summand new_oper = new Summand(1, cur_state);
                        summands.Add(new_oper);
                    }
                }
            }
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].GetType() == typeof(Number))
                {
                    for (int j = i; j < variables.Count; j++)
                    {
                        if (variables[j].GetType() == typeof(Number))
                        {
                            Summand new_oper = new Summand(1, variables[i], variables[j]);
                            summands.Add(new_oper);
                        }
                        else
                        {
                            foreach (Value cur_state in ((Category)variables[j]).states)
                            {
                                Summand new_oper = new Summand(1, variables[i], cur_state);
                                summands.Add(new_oper);
                            }
                        }
                    }
                }
                else
                {
                    foreach (Value cur_state in ((Category)variables[i]).states)
                    {
                        for (int j = i + 1; j < variables.Count; j++)
                        {
                            if (variables[j].GetType() == typeof(Number))
                            {
                                Summand new_oper = new Summand(1, cur_state, variables[j]);
                                summands.Add(new_oper);
                            }
                            else
                            {
                                foreach (Value cur_state2 in ((Category)variables[j]).states)
                                {
                                    Summand new_oper = new Summand(1, cur_state, cur_state2);
                                    summands.Add(new_oper);
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].GetType() == typeof(Number))
                {
                    for (int j = i; j < variables.Count; j++)
                    {
                        if (variables[j].GetType() == typeof(Number))
                        {
                            for (int k = j; k < variables.Count; k++)
                            {
                                if (variables[k].GetType() == typeof(Number))
                                {
                                    Summand new_oper = new Summand(1, variables[i], variables[j], variables[k]);
                                    summands.Add(new_oper);
                                }
                                else
                                {
                                    foreach (Value cur_state in ((Category)variables[k]).states)
                                    {
                                        Summand new_oper = new Summand(1, variables[i], variables[j], cur_state);
                                        summands.Add(new_oper);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (Value cur_state in ((Category)variables[j]).states)
                            {
                                for (int k = j + 1; k < variables.Count; k++)
                                {
                                    if (variables[k].GetType() == typeof(Number))
                                    {
                                        Summand new_oper = new Summand(1, variables[i], cur_state, variables[k]);
                                        summands.Add(new_oper);
                                    }
                                    else
                                    {
                                        foreach (Value cur_state2 in ((Category)variables[k]).states)
                                        {
                                            Summand new_oper = new Summand(1, variables[i], cur_state, cur_state2);
                                            summands.Add(new_oper);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (Value cur_state in ((Category)variables[i]).states)
                    {
                        for (int j = i + 1; j < variables.Count; j++)
                        {
                            if (variables[j].GetType() == typeof(Number))
                            {
                                for (int k = j; k < variables.Count; k++)
                                {
                                    if (variables[k].GetType() == typeof(Number))
                                    {
                                        Summand new_oper = new Summand(1, cur_state, variables[j], variables[k]);
                                        summands.Add(new_oper);
                                    }
                                    else
                                    {
                                        foreach (Value cur_state1 in ((Category)variables[k]).states)
                                        {
                                            Summand new_oper = new Summand(1, cur_state, variables[j], cur_state1);
                                            summands.Add(new_oper);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (Value cur_state1 in ((Category)variables[j]).states)
                                {
                                    for (int k = j + 1; k < variables.Count; k++)
                                    {
                                        if (variables[k].GetType() == typeof(Number))
                                        {
                                            Summand new_oper = new Summand(1, cur_state, cur_state1, variables[k]);
                                            summands.Add(new_oper);
                                        }
                                        else
                                        {
                                            foreach (Value cur_state2 in ((Category)variables[k]).states)
                                            {
                                                Summand new_oper = new Summand(1, cur_state, cur_state1, cur_state2);
                                                summands.Add(new_oper);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return summands;
        }
        double[][] compute_summands(List<Summand> summands, out double[] Y)
        {
            List<Dictionary<string, double>> inputData = read_data_from_file("learn_data.csv", out Y);
            double[][] res = new double[inputData.Count][];
            for (int i = 0; i < inputData.Count; i++)
            {
                res[i] = new double[summands.Count +1];
                res[i][ 0] = 1;
                for (int j = 0; j < summands.Count; j++)
                    res[i][ j + 1] = summands[j].Calculate(inputData[i], 1);
            }
            return res;
        }
        List<Dictionary<string, double>> read_data_from_file(string path, out double[] Ys)
        {
            List<Dictionary<string, double>> res = new List<Dictionary<string, double>>();
            List<double> Y = new List<double>();
            string[] strs = File.ReadAllLines(path);
            char[] chars = { ',', ' ', '\n', '\t' };
            for (int i = 0; i < strs.Length; i++)
            {
                string[] tmp = strs[i].Split(chars, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, double> current_vars = new Dictionary<string, double>();
                int j;
                for (j = 0; j < variables.Count; j++)
                {
                    if (variables[j].GetType() == typeof(Number))
                    {
                        current_vars.Add(variables[j].name, Convert.ToDouble(tmp[j], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        int k = 0;
                        foreach (Value cur_state in ((Category)variables[j]).states)
                        {
                            if (cur_state.state == tmp[j])
                                current_vars.Add(cur_state.name, 1);
                            else
                                current_vars.Add(cur_state.name, 0);
                            k++;
                        }
                    }
                }
                res.Add(current_vars);
                if (tmp.Length > j)
                    Y.Add(Convert.ToDouble(tmp[j], CultureInfo.InvariantCulture));

            }
            Ys = Y.ToArray();
            return res;
        }
        double[] gradient_descent(List<Summand> summands, double la, double eps)
        {
            double[] Y;
            double[][] summand_value = compute_summands(summands, out Y);
            double[] Q = new double[summands.Count + 1];
            double[] prev_Q = new double[summands.Count + 1];
            for (int i = 0; i < Q.Length; i++)
                if (i == 0)
                    Q[i] = 0;
                else
                    Q[i] = 1;
            int M = summand_value.Length;
            double J = 0, previous_J = 0;
            double factor = la / (double)M;
            double[] Deviations = compute_deviation(Q, summand_value, Y, M);
            while(true)
            {
                previous_J = J;
                for (int i = 0; i < Q.Length; i++)
                    Q[i] = Q[i] - factor * adjustmentQ(summand_value, Deviations, i);
                Deviations = compute_deviation(Q, summand_value, Y, M);
                J = computing_JQ(M, Deviations);
                if (Math.Abs(previous_J - J) < eps)
                    break;
            }
            return Q;
        }
        double[] compute_deviation(double[] Q, double[][] summands, double[] Y, int size)
        {
            double[] result = new double[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = 0;
                for (int j = 0; j < Q.Length; j++)
                    result[i] += Q[j] * summands[i][j];
                result[i] -= Y[i];
            }
            return result;
        }
        double adjustmentQ(double[][] summands, double[] delta, int index)
        {
            double result = 0;
            for (int i = 0; i < delta.Length; i++)
                result += summands[i][index] * delta[i];
            return result;
        }
        double computing_JQ(int M, double[] gradientDel)
        {
            double res = 0;
            for (int i = 0; i < gradientDel.Length; i++)
            {
                res += gradientDel[i] * gradientDel[i];
            }
            return res / (double)(2 * M);
        }
        double FindY(List<Summand> summands, Dictionary<string, double> values, double[] Q)
        {
            double Y = Q[0];
            for (int i = 0; i < summands.Count;i++ )
                Y += summands[i].Calculate(values, Q[i + 1]);
            return Y;
        }
        double[] ReadAnswerData(string path)
        {
            string[] strs = File.ReadAllLines(path);
            char[] separators = { ',', ' ', '\n' };
            double[] Y = new double[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                string[] elements = strs[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                Y[i] = Convert.ToDouble(elements[elements.Length - 1], CultureInfo.InvariantCulture);
            }
            return Y;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Controls.Add(processLabel);
            StDataBox.Text = DateTime.Now.TimeOfDay.ToString();
            EndDataBox.Text = "";
            Median.Text = "";
            mediumErr.Text = "";
            QsBox.Items.Clear();
            YBox.Items.Clear();
        }
    }
    public abstract class Var
    {
        public string name;
    }
    public class Number : Var
    {
        public Number(string varName)
        {
            name = varName;
        }
    }
    public class Category : Var
    {
        public List<Value> states;
        public Category(string n, List<Value> cur_states)
        {
            name = n;
            states = cur_states;
        }
    }
    public class Value : Var
    {
        public string state;
        public Value(string n, string st)
        {
            name = n;
            state = st;
        }
    }
    public class Summand
    {
        List<Var> elements;
        public int factor;
        public Summand(int k, Var v1)
        {
            elements = new List<Var>();
            elements.Add(v1);
            factor = k;
        }
        public Summand(int k, Var v1, Var v2)
        {
            elements = new List<Var>();
            elements.Add(v1);
            elements.Add(v2);
            factor = k;
        }
        public Summand(int k, Var v1, Var v2, Var v3)
        {
            elements = new List<Var>();
            elements.Add(v1);
            elements.Add(v2);
            elements.Add(v3);
            factor = k;
        }
        public override string ToString()
        {
            string str = factor.ToString();
            for (int i = 0; i < elements.Count; i++)
                str += "*" + elements[i].name;
            return str;
        }
        public double Calculate(Dictionary<string, double> values, double factor)
        {
            if (factor == 0)
                return 0;
            double result = factor;
            foreach (Var var in elements)
                result *= values[var.name];
            return result;
        }
    }
}
