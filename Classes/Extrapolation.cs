using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtrapolationApp.Classes
{
    class Extrapolation
    {
        private float[] x { get; set; }
        private float[] y { get; set; }
        private int N { get; set; }
        public delegate void WriteTable(string[] colnames, double[][] table);
        public delegate void WriteText(string mes);
        private WriteTable WriteV;
        private WriteTable WriteH;
        private WriteText WriteStr;
        private bool isLogging = false;
        public Extrapolation() { }
        public Extrapolation(float[] vec_x, float[] vec_y) => SetArguments(vec_x, vec_y);
        public bool SetArguments(float[] vector_x, float[] vector_y)
        {
            bool args_valid = true;
            x = vector_x;
            y = vector_y;
            N = y.Length;
            if (N < 3) args_valid = false;
            return args_valid;
        }
        public void SetLogging(WriteTable WriteHorizontalMethod, WriteTable WriteVerticalMethod, WriteText WriteTextMethod)
        {
            WriteH = WriteHorizontalMethod;
            WriteV = WriteVerticalMethod;
            WriteStr = WriteTextMethod;
            isLogging = true;
        }
        private void WriteVertical(string[] colnames, double[][] table)
        {
            if (isLogging) WriteV(colnames,table);
        }
        private void WriteHorizontal (string[] colnames, double[][] table)
        {
            if (isLogging) WriteH(colnames, table);
        }
        private void Write(string str)
        {
            if (isLogging) WriteStr(str);
        }
        public OutData QuadraticRegression()
        {
            double sumx = 0, sumx2 = 0, sumx3 = 0, sumx4 = 0, sumxy = 0, sumx2y = 0, sumy = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sumx += x[i];
                sumx2 += x[i] * x[i];
                sumx3 += x[i] * x[i] * x[i];
                sumx4 += x[i] * x[i] * x[i] * x[i];
                sumxy += x[i] * y[i];
                sumx2y += x[i] * x[i] * y[i];
                sumy += y[i];
            }
            double det = sumx2 * (sumx2 * sumx2 - sumx * sumx3) - sumx * (sumx3 * sumx2 - sumx * sumx4) + x.Length * (sumx3 * sumx3 - sumx2 * sumx4),
                detA = sumy * (sumx2 * sumx2 - sumx * sumx3) - sumx * (sumxy * sumx2 - sumx * sumx2y) + x.Length * (sumxy * sumx3 - sumx2 * sumx2y),
                detB = sumx2 * (sumxy * sumx2 - sumx * sumx2y) - sumy * (sumx3 * sumx2 - sumx * sumx4) + x.Length * (sumx3 * sumx2y - sumxy * sumx4),
                detC = sumx2 * (sumx2 * sumx2y - sumxy * sumx3) - sumx * (sumx3 * sumx2y - sumxy * sumx4) + sumy * (sumx3 * sumx3 - sumx2 * sumx4);
            double a = detA / det, b = detB / det, c = detC / det;
            double[] yd = new double[y.Length];
            double ym = sumy / y.Length, difyyd = 0, difyym = 0;
            for (int i = 0; i < x.Length; i++)
            {
                yd[i] = a * x[i] * x[i] + b * x[i] + c;
                difyyd += (y[i] - yd[i]) * (y[i] - yd[i]);
                difyym += (y[i] - ym) * (y[i] - ym);
            }
            double r = Math.Sqrt(1 - (difyyd / difyym)), index = Math.Pow(r, 2);
            OutData res = new OutData { a = (float)a, b = (float)b, c = (float)c, R2 = (float)index,
                name = "квадратичная регрессия",
                equation = $"{Math.Round(a, 4)}*x2 {(b > 0 ? "+ " : "")}{Math.Round(b, 4)} * x " + 
                    $"{(c > 0 ? "+ " : "")}{Math.Round(c, 4)}"                    
            };
            return res;
        }
        public OutData CubicRegression()
        {
            OutData res = new OutData { name = "кубическая регрессия" };
            if (!(N < 4))
            {
                int[] n = new int[N];
                double[][] t = new double[11][];    //вспомогательная таблица
                double[] sumc = new double[11];     //суммы столбцов вспомогательной таблицы

                //инициализация вспомогательной таблицы
                for (int i = 0; i < 11; i++) t[i] = new double[N];

                // 0 - номера строк: 1..N
                // 1 - x
                // 2 - x2 
                // 3 - x3
                // 4 - x4
                // 5 - x5
                // 6 - x6
                // 7 - y 
                // 8 - xy
                // 9 - x2y
                // 10 - x3y
                for (int j = 0; j < N; j++)
                {
                    double x = this.x[j];
                    double y = this.y[j];

                    t[0][j] = j + 1;
                    t[1][j] = x;
                    t[6][j] = (t[5][j] = (t[4][j] = (t[3][j] = (t[2][j] = x * x) * x) * x) * x) * x;
                    t[7][j] = y;
                    t[8][j] = x * y;
                    t[9][j] = t[2][j] * y;  //x2y
                    t[10][j] = t[3][j] * y;  //x3y
                }
                sumc[0] = N;
                //суммирование значений в столобцах
                for (int i = 1; i < 11; i++)
                    sumc[i] = SumVector(t[i]);

                var matrix_k = new Matrix(4, 4);
                int offset = 3;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        matrix_k[i, j] = sumc[offset + j];
                    }
                    offset--;
                }
                double det_k = matrix_k.CalculateDeterminant();
                //Write($"Определитель системы уравнений = {Math.Round(det_k,4)}");
                //a
                var matrix = new Matrix(4, 4);
                matrix = CopySquareMatrix(matrix_k);
                for (int j = 0; j < 4; j++) matrix[0, j] = sumc[7 + j];
                double det = matrix.CalculateDeterminant();
                double a = det / det_k;
                //Write($"Определитель а = {Math.Round(det,4)}, коэффициент a = {Math.Round(a,4)}");
                //b
                matrix = CopySquareMatrix(matrix_k);
                for (int j = 0; j < 4; j++) matrix[1, j] = sumc[7 + j];
                det = matrix.CalculateDeterminant();
                double b = det / det_k;
                //Write($"Определитель b = {Math.Round(det,4)}, коэффициент b = {Math.Round(b,4)}");
                //c
                matrix = CopySquareMatrix(matrix_k);
                for (int j = 0; j < 4; j++) matrix[2, j] = sumc[7 + j];
                det = matrix.CalculateDeterminant();
                double c = det / det_k;
                //Write($"Определитель c = {Math.Round(det,4)}, коэффициент c = {Math.Round(c, 4)}");
                //d
                matrix = CopySquareMatrix(matrix_k);
                for (int j = 0; j < 4; j++) matrix[3, j] = sumc[7 + j];
                det = matrix.CalculateDeterminant();
                double d = det / det_k;
                //Write($"Определитель d = {Math.Round(det,4)}, коэффициент d = {Math.Round(d, 4)}");
                //считаем индексы
                double y_cp = sumc[7] / sumc[0]; //  y среднее
                                                 //Write($"y среднее = {Math.Round(y_cp,4)}");

                sumc[5] = (sumc[6] = (sumc[10] = 0));
                for (int j = 0; j < N; j++)
                {
                    double y = t[7][j];
                    t[8][j] = a * t[3][j] + b * t[2][j] + c * t[1][j] + d;  // y'
                    t[4][j] = y - y_cp;                                     // y-(y среднее)
                    sumc[5] += (t[5][j] = t[4][j] * t[4][j]);               // (4) в квадрате и сумма
                    t[9][j] = y - t[8][j];                                  // y-y' 
                    sumc[10] += (t[10][j] = t[9][j] * t[9][j]);             // (y-y')2 и сумма
                                                                            //sumc[6] += (t[6][j] = Math.Abs(t[9][j] / y));           // Ai и сумма
                }
                double R = Math.Sqrt(1 - sumc[10] / sumc[5]);
                double R2 = R * R;
                //Write($"Индекс корреляции R = {Math.Round(R, 4)}");
                //Write($"Индекс детерминации R² = {Math.Round(R2,4)}");

                res = new OutData
                {
                    a = (float)a,
                    b = (float)b,
                    c = (float)c,
                    d = (float)d,
                    R2 = (float)R2,
                    name = "кубическая регрессия",
                    equation = $"y = {Math.Round(a, 4)} * x3 {(b > 0 ? "+ " : "")}{Math.Round(b, 4)} * x2 " +
                        $"{(c > 0 ? "+ " : "")}{Math.Round(c, 4)}*x {(d > 0 ? "+ " : "")}{Math.Round(d, 4)}"
                };
            }
            else
            {
                //(0, 0, 0, 0, -1);
                res.equation = "Error: Уравнение кубической регрессии не может быть построено для " +
                    "выборки с количеством элементов X и Y менше четырёх";
                res.R2 = -1;

            }
            return res;
        }
        public OutData HyperbolicRegression()
        {
            OutData res = new OutData { name = "гиперболическая регрессия" };
            bool indata_valid = true;
            for (int i = 0; i < x.Length; i++) if (x[i] == 0) indata_valid = false;
            if (indata_valid)
            {
                float ydivx = 0, ldivx = 0, ldivx2 = 0, sumy = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    ydivx += y[i] / x[i];
                    ldivx += 1 / x[i];
                    ldivx2 += 1 / (x[i] * x[i]);
                    sumy += y[i];
                }
                float b = (x.Length * ydivx - ldivx * sumy) / (x.Length * ldivx2 - ldivx * ldivx), a = sumy / (x.Length) - (b / x.Length) * ldivx;
                float[] yd = new float[y.Length];
                float ym = sumy / y.Length, difyyd = 0, difyym = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    yd[i] = a + b / x[i];
                    difyyd += (y[i] - yd[i]) * (y[i] - yd[i]);
                    difyym += (y[i] - ym) * (y[i] - ym);
                }
                double r = Math.Sqrt(1 - (difyyd / difyym)), index = Math.Pow(r, 2);
                //заполнение результата
                res.a = a;
                res.b = b;
                res.equation = $"y = {Math.Round(a, 4)} {(b > 0 ? "+ " : "")}{Math.Round(b, 4)} /x";
                res.R2 = (float)index;
            }
            else
            {
                //(0, 0, -1);
                res.equation = "Error: Уравнение гиперболической регрессии не может быть построено для" +
                    "выборки, содержащей значения X равные нулю";
                res.R2 = -1;
            }
            return res; 
        }
        public OutData PowerRegression()
        {
            OutData res = new OutData { name = "степенная регрессия" };
            bool indata_valid = true;
            for (int i = 0; i < x.Length; i++) if (x[i] <= 0 || y[i] <= 0) indata_valid = false;
            if (indata_valid)
            {
                double lnx = 0, ln2x = 0, lny = 0, lnxlny = 0;
                float sumy = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    lnx += Math.Log(x[i]);
                    ln2x += Math.Pow(Math.Log(x[i]), 2);
                    lny += Math.Log(y[i]);
                    lnxlny += Math.Log(x[i]) * Math.Log(y[i]);
                    sumy += y[i];
                }
                float b = (float)((x.Length * lnxlny - lnx * lny) / (x.Length * ln2x - lnx * lnx)), a = (float)(Math.Exp(lny / (x.Length) - (b / x.Length) * lnx));
                float[] yd = new float[y.Length];
                float ym = sumy / y.Length, difyyd = 0, difyym = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    yd[i] = (float)(a * Math.Pow(x[i], b));
                    difyyd += (y[i] - yd[i]) * (y[i] - yd[i]);
                    difyym += (y[i] - ym) * (y[i] - ym);
                }
                double r = Math.Sqrt(1 - (difyyd / difyym)), index = Math.Pow(r, 2);
                //заполнение выходного результата
                res.a = a;
                res.b = b;
                res.equation = $"y = {Math.Round(a, 4)} * x ^ {(b > 0 ? "" : "(")}{Math.Round(b, 4)}{(b > 0 ? "" : ")")}";
                res.R2 = (float)index;
            }
            else
            {
                //(0, 0, -1);
                res.equation = "Error: Уравнение степенной регрессии не может быть построено для " + 
                    "выборки, содержащей значения X или Y меньшие либо равные нулю";
                res.R2 = -1;
            }

            return res;
        }
        public OutData LogarithmicRegression()
        {
            OutData res = new OutData { name = "логарифмическая регрессия" };
            bool indata_valid = true;
            for (int i = 0; i < x.Length; i++) if (x[i] <= 0) indata_valid = false;
            if (indata_valid)
            {
                double lnx = 0, ln2x = 0, ylnx = 0, sumy = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    lnx += Math.Log(x[i]);
                    ln2x += Math.Pow(Math.Log(x[i]), 2);
                    ylnx += y[i] * Math.Log(x[i]);
                    sumy += y[i];
                }
                float b = (float)((x.Length * ylnx - lnx * sumy) / (x.Length * ln2x - lnx * lnx)), a = (float)(sumy / (x.Length) - (b / x.Length) * lnx);
                double[] yd = new double[y.Length];
                double ym = sumy / y.Length, difyyd = 0, difyym = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    yd[i] = a + b * Math.Log(x[i]);
                    difyyd += (y[i] - yd[i]) * (y[i] - yd[i]);
                    difyym += (y[i] - ym) * (y[i] - ym);
                }
                double r = Math.Sqrt(1 - (difyyd / difyym)), index = Math.Pow(r, 2);
                //заполнение выходного результата
                res.a = a;
                res.b = b;
                res.equation = $"y = {Math.Round(a, 4)} {(b > 0 ? "+ " : "")}{Math.Round(b, 4)} * ln x";
                res.R2 = (float)index;
            }
            else
            {
                //(0, 0, -1);
                res.equation = "Error: Уравнение логарифмической регрессии не может быть построено для " +
                    "выборки, содержащей значения X меньшие либо равные нулю";
                res.R2 = -1;
            }
            return res;
        }
        public OutData DemonstrativeRegression()
        {
            OutData res = new OutData { name = "показательная регрессия" };
            bool indata_valid = true;
            for (int i = 0; i < x.Length; i++) if (y[i] <= 0) indata_valid = false;
            if (indata_valid)
            {
                double sumx = 0, sumx2 = 0, lny = 0, xlny = 0, sumy = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    sumx += x[i];
                    sumx2 += x[i] * x[i];
                    lny += Math.Log(y[i]);
                    xlny += x[i] * Math.Log(y[i]);
                    sumy += y[i];
                }
                float b = (float)Math.Exp((x.Length * xlny - sumx * lny) / (x.Length * sumx2 - sumx * sumx)), a = (float)Math.Exp(lny / (x.Length) - (Math.Log(b) / x.Length) * sumx);
                double[] yd = new double[y.Length];
                double ym = sumy / y.Length, difyyd = 0, difyym = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    yd[i] = a * Math.Pow(b, x[i]);
                    difyyd += (y[i] - yd[i]) * (y[i] - yd[i]);
                    difyym += (y[i] - ym) * (y[i] - ym);
                }
                double r = Math.Sqrt(1 - (difyyd / difyym)), index = Math.Pow(r, 2);
                //заполнение выходного результата
                res.a = a;
                res.b = b;
                res.equation = $"y = {Math.Round(a, 4)} * {(b > 0 ? "" : "(")}{Math.Round(b, 4)}{(b > 0 ? "" : ")")} ^ x";
                res.R2 = (float)index;
            }
            else
            {
                //(0, 0, -1);
                res.equation = "Error: Уравнение показательной регрессии не может быть построено для " +
                    "выборки, содержащей значения Y меньшие либо равные нулю";
                res.R2 = -1;
            }
            return res;
        }
        public OutData ExponentialRegression()
        {
            OutData res = new OutData { name = "экспоненциальная регрессия" };
            bool indata_valid = true;
            for (int i = 0; i < x.Length; i++) if (y[i] <= 0) indata_valid = false;
            if (indata_valid)
            {
                double sumx = 0, sumx2 = 0, lny = 0, xlny = 0, sumy = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    sumx += x[i];
                    sumx2 += x[i] * x[i];
                    lny += Math.Log(y[i]);
                    xlny += x[i] * Math.Log(y[i]);
                    sumy += y[i];
                }
                float b = (float)((x.Length * xlny - sumx * lny) / (x.Length * sumx2 - sumx * sumx)), a = (float)(lny / (x.Length) - (b / x.Length) * sumx);
                double[] yd = new double[y.Length];
                double ym = sumy / y.Length, difyyd = 0, difyym = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    yd[i] = Math.Exp(a + b * x[i]);
                    difyyd += (y[i] - yd[i]) * (y[i] - yd[i]);
                    difyym += (y[i] - ym) * (y[i] - ym);
                }
                double r = Math.Sqrt(1 - (difyyd / difyym)), index = Math.Pow(r, 2);
                //заполнение выходного результата
                res.a = a;
                res.b = b;
                res.equation = $"y = e ^ ({Math.Round(a, 4)} {(b > 0 ? "+ " : "")}{Math.Round(b, 4)}";
                res.R2 = (float)index;
            }
            else
            {
                //(0, 0, -1);
                res.equation = "Error: Уравнение экспоненциальной регрессии не может быть построено для " +
                    "выборки, содержащей значения Y меньшие либо равные нулю";
                res.R2 = -1;
            }
            return res;
        }
        private double SumVector(double[] vec)
        {
            double sum = 0;
            for (int i = 0; i < vec.Length; i++) sum += vec[i];
            return sum;
        }
        private void WriteMatrix(string[]colnames, Matrix matrix)
        {
            double[][] matrix_tbl = new double[4][];

            for (int i = 0; i < 4; i++)
            {
                matrix_tbl[i] = new double[4];
                for (int j = 0; j < 4; j++)
                {
                    matrix_tbl[i][j] = matrix[i, j];
                }
            }
            WriteVertical(colnames, matrix_tbl);
        }
        private Matrix CopySquareMatrix(Matrix matrix)
        {
            int n = 0;
            Matrix res;
            if (matrix.IsSquare)
            {
                n = matrix.N;
                res = new Matrix(n, n);
                for (int j = 0; j < n; j++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        res[i, j] = matrix[i, j];
                    }
                }
            }else
            {
                res = new Matrix(matrix.N, matrix.N);
            }
            return res;
        }
        public OutData Run()
        {
            OutData data = new OutData();
            if (x.Length == y.Length)
                if (!(x.Length < 3))
                {
                    Dictionary<int, OutData> result = new Dictionary<int, OutData>();
                    //запуск по очереди функций:
                    // 1 - степенная регрессия
                    // 2 - квадратичная
                    // 3 - кубическая
                    // 4 - гиперболическая
                    // 5 - показательная
                    // 6 - логарифмическая
                    // 7 - экспоненциальная
                    result.Add(1, PowerRegression());
                    result.Add(2, QuadraticRegression());
                    result.Add(3, CubicRegression());
                    result.Add(4, HyperbolicRegression());
                    result.Add(5, DemonstrativeRegression());
                    result.Add(6, LogarithmicRegression());
                    result.Add(7, ExponentialRegression());
                    float maxR2 = 0;
                    int regr = 0;
                    foreach (var pair in result)
                    {
                        Write($"{pair.Value.name} - R2 = {Math.Round(pair.Value.R2, 15)}");
                        if (pair.Value.R2 > maxR2)
                        {
                            maxR2 = pair.Value.R2;
                            regr = pair.Key;
                            data = pair.Value;
                        }
                    }
                    string outstr = $"\nНаилучший результат выдала '{data.name}'" +
                        $"\nc уравнением {data.equation}" +
                        $"\nи индексом детерминации R2 = {Math.Round(data.R2, 4)}";
                    Write(outstr);
                }
                else
                {
                    Write("Error: Количество элементов X и Y не может быть меньше трёх");
                }
            return data;
        }
    }
    class OutData
    {
        public float a = 0;
        public float b = 0;
        public float c = 0;
        public float d = 0;
        public float R2 = 0;
        public string name = "результат не получен";
        public string equation = "без уравнения";
    }
}
