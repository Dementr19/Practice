using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using ExtrapolationApp.Classes;


namespace ExtrapolationApp
{
    public partial class Form1 : Form
    {
        Extrapolation Extr;
        private delegate OutData Regression();
        public Form1()
        {
            InitializeComponent();
            Extr = new Extrapolation();
            Extr.SetLogging(WriteHorizontal, WriteVertical, Log); // включаем логирование из модуля
        }

        private void WriteVertical(string[] colname, double[][] tbl)
        {
            const int colwidth = 12;
            int col_count = tbl.Length;
            string cname = "";
            StringBuilder underl = new StringBuilder("----------",0,colwidth-2,colwidth-2);
            string undrl = underl.ToString()+"  ";
            underl = new StringBuilder().Insert(0, undrl, col_count);
            undrl = underl.ToString();
            foreach (var str in colname) 
                cname += $"{str,-colwidth}";
            string stbl = "";
            for (int j=0; j < tbl[0].Length; j++)
            {
                for (int i = 0; i < col_count; i++)
                {
                    stbl += $"{Math.Round(tbl[i][j],4).ToString().Replace(",","."),-colwidth}";
                }
                stbl += "\n";
            }
            Log($"{cname}\n{undrl}\n{stbl}");
        }

        private void WriteHorizontal(string[] colname, double[][] tbl)
        {
            const int colwidth = 5;
            int col_count = tbl.Length;
            string line = "";
            for (int i=0; i<col_count;i++)
            {
                line += $"{colname[i],-colwidth}";
                for (int j = 0; j < tbl[0].Length; j++)
                {
                    line += $"{Math.Round(tbl[i][j],4).ToString().Replace(",", "."),-colwidth}";
                }
                line += "\n";
            }
            Log(line);
        }

        public void Log(string mes = "")
        {
            string[] lines = mes.Split('\n');
            foreach (var str in lines)
            {
                lbResult.Items.Add(str);
            }
        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            lbResult.Items.Clear();
        }

        private bool GetArgs()
        {
            bool res = false;
            var pattern = @"\s+";
            string[] strX = Regex.Replace(tbxVectorX.Text, pattern," ").Trim().Split(' ');
            string[] strY = Regex.Replace(tbxVectorY.Text, pattern, " ").Trim().Split(' ');
            if (strX.Length == strY.Length)
            {
                int n = strX.Length;
                float[] vecX = new float[n];
                float[] vecY = new float[n];
                for (int i=0; i < n; i++)
                {
                    float.TryParse(strX[i].Replace(".",","), out vecX[i]);
                    float.TryParse(strY[i].Replace(".", ","), out vecY[i]);
                }
                if (!(res = Extr.SetArguments(vecX, vecY))) 
                    Log("Error: Значений элементов должно быть не меньше трёх");
            }
            else
            {
                Log("Error: Количество элементов X и Y неодинаково.");
            }
            return res;
        }

        private void StartRegression(Regression Regres)
        {
            if (GetArgs())
            {
                Log("Processing...");
                OutData res = Regres();
                string str = "";
                if (!res.name.Contains("кубическая"))
                {
                    if (!res.name.Contains("квадратичная"))
                    {
                        str = $"Наименование: {res.name}\nКоэффициенты уравнения:\na = {res.a}" +
                            $"\nb = {res.b}\nУравнение: {res.equation}\nИндекс детерминации R2 = {res.R2}";
                    }
                    else
                    {
                        str = $"Наименование: {res.name}\nКоэффициенты уравнения:\na = {res.a}" +
                            $"\nb = {res.b}\nc = {res.c}\nУравнение: {res.equation}" +
                            $"\nИндекс детерминации R2 = {res.R2}";

                    }
                }
                else
                {
                    str = $"Наименование: {res.name}\nКоэффициенты уравнения:\na = {res.a}" +
                        $"\nb = {res.b}\nc = {res.c}\nd = {res.d}\nУравнение: {res.equation}" +
                        $"\nИндекс детерминации R2 = {res.R2}";
                }
                Log($"{str}\n"); 
             }
        }

        private void btnProbe_Click(object sender, EventArgs e)
        {
            if (GetArgs())
            {
                var data = Extr.Run();
                //string[] colname = { "X", "Y", "X2*Y" };
                //WriteHorizontal(colname, new double[][] { extr.X, extr.Y });
                //WriteVertical(colname, new double[][]{ extr.X, extr.Y});
            }
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            StartRegression(Extr.PowerRegression);
        }

        private void btnQuadratic_Click(object sender, EventArgs e)
        {
            StartRegression(Extr.QuadraticRegression);
        }

        private void btnCubic_Click(object sender, EventArgs e)
        {
            StartRegression(Extr.CubicRegression);
        }

        private void btnHyperbolic_Click(object sender, EventArgs e)
        {
            StartRegression(Extr.HyperbolicRegression);
        }

        private void btnPokazatel_Click(object sender, EventArgs e)
        {
            StartRegression(Extr.DemonstrativeRegression);
        }

        private void btnLogarithmic_Click(object sender, EventArgs e)
        {
            StartRegression(Extr.LogarithmicRegression);
        }

        private void btтExponential_Click(object sender, EventArgs e)
        {
            StartRegression(Extr.ExponentialRegression);
        }
    }
}
