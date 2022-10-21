using SvetaLabs.MeasureTime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SvetaLabs.Laba3
{
    public class Laba3QuasiMinimalMethod
    {
        private static Random _random = new Random();
        private int _size = 300;

        private double[][] _matrixCoef;

        public Laba3QuasiMinimalMethod()
        {
            // https://jamesmccaffrey.wordpress.com/2020/04/20/implementing-matrix-qr-decomposition-from-scratch-using-the-gram-schmidt-algorithm-with-c/

            _matrixCoef = new double[_size][];
            for (int i = 0; i < _matrixCoef.Length; i++)
            {
                _matrixCoef[i] = new double[_size];
            }

            for (int i = 0; i < _matrixCoef.Length; i++)
            {
                for (int j = 0; j < _matrixCoef[0].Length; j++)
                {
                    _matrixCoef[i][j] = _random.Next(_size);
                }
            }
        }

        public void Start()
        {
            var measureTheTime = new MeasureTheTime(); // створюємо екземпляр классу який вимірює час 

            Console.WriteLine($"StartWithoutMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithoutMultiTreading)}"); // вимірюємо час роботи функції StartWithoutMultiTreading

            Console.WriteLine($"StartWithMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithMultiTreading)}"); // вимірюємо час роботи функції StartWithMultiTreading
        }

        public void StartWithoutMultiTreading()
        {
            // створюємо тимчасові масиви для розрунків
            double[][] Q = null;
            double[][] R = null;

            // визиваємо функцію для розрахунків MatDecompQR
            int dummy = MatDecompQR(_matrixCoef, out Q, out R);

            // визиваємо функцію для розрахунків MatProduct
            double[][] qr = MatProduct(Q, R);

            Console.WriteLine("\nEnd demo");

        }

        public void StartWithMultiTreading()
        {
            // створюємо тимчасові масиви для розрунків
            double[][] Q = null;
            double[][] R = null;

            // визиваємо функцію для розрахунків MatDecompQR
            int dummy = MatDecompQRWithThreads(_matrixCoef, out Q, out R);

            // визиваємо функцію для розрахунків MatProduct
            double[][] qr = MatProduct(Q, R);

            Console.WriteLine("\nEnd demo");
        }
        private int MatDecompQR(double[][] A, out double[][] Q, out double[][] R)
        {
            // work with rows of the transpose
            // return another transpose at end
            double[][] a = MatTranspose(A);
            double[][] u = MatDuplicate(a);
            int rows = a.Length;  // of the transpose
            int cols = a[0].Length;

            Q = MatCreate(cols, rows);
            R = MatCreate(cols, rows);

            // first row of a (first col of M)
            for (int j = 0; j < cols; ++j)
                u[0][j] = a[0][j];

            double[] accum = new double[cols];
            // remaining rows of a
            for (int i = 1; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    // accumulate projections
                    accum = new double[cols];
                    for (int t = 0; t < i; ++t)
                    {
                        double[] proj = VecProjection(u[t], a[i]);
                        for (int k = 0; k < cols; ++k)
                            accum[k] += proj[k];
                    }
                }
                for (int k = 0; k < cols; ++k)
                    u[i][k] = a[i][k] - accum[k];
            }

            for (int i = 0; i < rows; ++i)
            {
                double norm = VecNorm(u[i]);
                for (int j = 0; j < cols; ++j)
                    u[i][j] = u[i][j] / norm;
            }
            // at this point u is Q(trans)

            double[][] q = MatTranspose(u);
            for (int i = 0; i < q.Length; ++i)
                for (int j = 0; j < q[0].Length; ++j)
                    Q[i][j] = q[i][j];

            double[][] r = MatProduct(u, A);
            for (int i = 0; i < r.Length; ++i)
                for (int j = 0; j < r[0].Length; ++j)
                    R[i][j] = r[i][j];

            return 0;
        }
        private double VecNorm(double[] vec)
        {
            double sum = 0.0;
            for (int i = 0; i < vec.Length; ++i)
                sum += vec[i] * vec[i];
            return Math.Sqrt(sum);
        }
        private double[][] MatDuplicate(double[][] m)
        {
            int rows = m.Length;
            int cols = m[0].Length;
            double[][] result = MatCreate(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = m[i][j];
            return result;
        }
        private double VecDotProd(double[] u, double[] v)
        {
            double result = 0.0;
            for (int i = 0; i < u.Length; ++i)
                result += u[i] * v[i];
            return result;
        }
        private double[] VecProjection(double[] u, double[] a)
        {
            // proj(u, a) = (inner(u,a) / inner(u, u)) * u
            // u cannot be all 0s
            int n = u.Length;
            double dotUA = VecDotProd(u, a);
            double dotUU = VecDotProd(u, u);
            double[] result = new double[n];
            for (int i = 0; i < n; ++i)
                result[i] = (dotUA / dotUU) * u[i];
            return result;
        }
        private double[][] MatCreate(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }
        private double[][] MatProduct(double[][] matA, double[][] matB)
        {
            int aRows = matA.Length;
            int aCols = matA[0].Length;
            int bRows = matB.Length;
            int bCols = matB[0].Length;
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices");

            double[][] result = MatCreate(aRows, bCols);

            for (int i = 0; i < aRows; ++i)
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matA[i][k] * matB[k][j];

            return result;
        }
        private double[][] MatTranspose(double[][] m)
        {
            int nr = m.Length;
            int nc = m[0].Length;
            double[][] result = MatCreate(nc, nr);  // note
            for (int i = 0; i < nr; ++i)
                for (int j = 0; j < nc; ++j)
                    result[j][i] = m[i][j];
            return result;
        }
        private int MatDecompQRWithThreads(double[][] A, out double[][] Q, out double[][] R)
        {
            // work with rows of the transpose
            // return another transpose at end
            double[][] a = MatTranspose(A);
            double[][] u = MatDuplicate(a);
            int rows = a.Length;  // of the transpose
            int cols = a[0].Length;

            Q = MatCreate(cols, rows);
            R = MatCreate(cols, rows);

            // first row of a (first col of M)
            for (int j = 0; j < cols; ++j)
                u[0][j] = a[0][j];

            double[] accum = new double[cols];
            var thr = new List<Thread>();

            // remaining rows of a
            for (int i = 1; i < rows; ++i)
            {
                thr.Add(new Thread(() => accum = ThreadFunction(a, u, cols, accum, i)));
                // запускаємо в потік функцію ThreadFunction яка записує результати у масив accum
            }

            foreach (var item in thr) // запускаємо потоки та отримуємо від них результати
            {
                item.Start();
                item.Join();
            }

            for (int i = 0; i < rows; ++i)
            {
                double norm = VecNorm(u[i]);
                for (int j = 0; j < cols; ++j)
                    u[i][j] = u[i][j] / norm;
            }
            // at this point u is Q(trans)

            double[][] q = MatTranspose(u);
            for (int i = 0; i < q.Length; ++i)
                for (int j = 0; j < q[0].Length; ++j)
                    Q[i][j] = q[i][j];

            double[][] r = MatProduct(u, A);
            for (int i = 0; i < r.Length; ++i)
                for (int j = 0; j < r[0].Length; ++j)
                    R[i][j] = r[i][j];

            return 0;
        }
        private double[] ThreadFunction(double[][] a, double[][] u, int cols, double[] accum, int i)
        {
            if (i == a.Length)
            {
                return accum;
            }

            for (int j = 0; j < cols; ++j)
            {
                // accumulate projections
                accum = new double[cols];
                for (int t = 0; t < i; ++t)
                {
                    double[] proj = VecProjection(u[t], a[i]);
                    for (int k = 0; k < cols; ++k)
                        accum[k] += proj[k];
                }
            }

            for (int k = 0; k < cols; ++k)
                u[i][k] = a[i][k] - accum[k];
            return accum;
        }


    }
}
