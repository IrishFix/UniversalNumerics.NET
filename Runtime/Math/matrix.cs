
// ReSharper disable once CheckNamespace

using System;

namespace TensorMath.Math {
    public static class matrix {
        public static double[,] Dot(double[,] a, double[,] b) {
            int m = a.GetLength(0);
            int n = b.GetLength(1);
            int p = a.GetLength(1);
            double[,] result = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < p; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }

        public static double[,] Sum(double[,] a, int axis = -1) {
            switch (axis) {
                case -1: {
                    double sum = 0;
                    int m = a.GetLength(0);
                    int n = a.GetLength(1);
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            sum += a[i, j];
                        }
                    }
                    return new double[,] { { sum } };
                }
                case 0: {
                    int m = a.GetLength(0);
                    int n = a.GetLength(1);
                    double[,] result = new double[1, n];
                    for (int j = 0; j < n; j++)
                    {
                        double sum = 0;
                        for (int i = 0; i < m; i++)
                        {
                            sum += a[i, j];
                        }
                        result[0, j] = sum;
                    }
                    return result;
                }
                case 1: {
                    int m = a.GetLength(0);
                    int n = a.GetLength(1);
                    double[,] result = new double[m, 1];
                    for (int i = 0; i < m; i++)
                    {
                        double sum = 0;
                        for (int j = 0; j < n; j++)
                        {
                            sum += a[i, j];
                        }
                        result[i, 0] = sum;
                    }
                    return result;
                }
                default:
                    throw new ArgumentException("Invalid axis argument: " + axis);
            }
        }

        public static double[,] Transpose(double[,] a) {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            double[,] result = new double[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[j, i] = a[i, j];
                }
            }
            return result;
        }
        
        public static double[,] Add(double[,] a, double[,] b) {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);
    
            double[,] result = new double[rows, cols];
    
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }
    
            return result;
        }

        public static double[,] Subtract(double[,] a, double[,] b) {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);
    
            double[,] result = new double[rows, cols];
    
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    result[i, j] = a[i, j] - b[i, j];
                }
            }
    
            return result;
        }
    }
}