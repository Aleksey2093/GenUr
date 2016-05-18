using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string otvet="";
            InputBox.Query("Генератор", "Введите количество переменных:", ref otvet);
            int allVar = int.Parse(otvet);
            InputBox.Query("Генератор", "Введите количество переменных вида категория:", ref otvet);
            int allCatVar = int.Parse(otvet);
            InputBox.Query("Генератор", "Введите min количество значений переменных вида категория:", ref otvet);
            int minCat = int.Parse(otvet);
            InputBox.Query("Генератор", "Введите max количество значений переменных вида категория:", ref otvet);
            int maxCat = int.Parse(otvet);
            SaveFileDialog fd = new SaveFileDialog();
            fd.FileName = "vars.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(fd.FileName, Generation.generateXML(allVar,allCatVar,minCat,maxCat));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.FileName = "vars.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                List<variables> lst = Generation.generateAllVariabls(fd.FileName);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "allVarForFormula.txt";
                if(sfd.ShowDialog()== DialogResult.OK)
                {
                    string allVars = Generation.generateVar(lst);
                    File.WriteAllText(sfd.FileName, allVars);
                    sfd.FileName = "Formula.txt";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string formula = Generation.generateFormula(allVars);
                        File.WriteAllText(sfd.FileName, formula);
                        sfd.FileName = "data.csv";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            string fn = Path.GetFileNameWithoutExtension(sfd.FileName);
                            Generation.generateDataLearning(Path.GetDirectoryName(sfd.FileName) + "\\" + fn, formula, lst);
                        }
                    }

                }
                
            }
            
        }

        private Solver slv;
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.FileName = "vars.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                slv = new Solver(fd.FileName);

                fd.FileName = "data_fin.csv";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    //textBox1.Text = slv.findFormula(fd.FileName);
                    textBox1.Text = slv.newGradient(fd.FileName);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.FileName = "vars.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                slv = new Solver(fd.FileName);
                fd.FileName = "data_fin_2.csv";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    
                    File.WriteAllText(Path.GetDirectoryName(fd.FileName) + "\\Y.txt",String.Join("\n", slv.searchY(fd.FileName, textBox1.Text)));
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.FileName = "data_fin_2.csv";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string path = fd.FileName;
                fd.FileName = "Y.txt";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    double []myY = File.ReadAllLines(fd.FileName).Select(x => double.Parse(x)).ToArray();
                    double []Y = File.ReadAllLines(path).Select(x => double.Parse(x.Split(',').Last().Replace('.', ','))).ToArray();
                    textBox1.Text = Solver.gradeY(Y, myY);
                    //MessageBox.Show(Solver.gradeY(Y, myY).ToString());
                }
            }
        }
    }
}
