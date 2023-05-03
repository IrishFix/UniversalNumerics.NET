
// ReSharper disable once CheckNamespace
namespace TensorMath.Networking {
    public static class loss {
        
        private static double[] Diff(double[] X, double[] y) {
            double[] Difference = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                Difference[i] = X[i] - y[i];
            }
            return Difference;
        }

        private static double[] Sqr(double[] X) {
            double[] Squared = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                Squared[i] = X[i] * X[i];
            }
            return Squared;
        }

        private static double Sum(double[] X) {
            double Sum = 0;
            for (int i = 0; i < X.Length; i++) {
                Sum += X[i];
            }
            return Sum;
        }

        private static double Mean(double[] X) {
            return Sum(X)/X.Length;
        }

        private static double[] Abs(double[] X) {
            double[] Absolute = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                Absolute[i] = System.Math.Abs(X[i]);
            }
            return Absolute;
        }

        private static double[] Clip(double[] X, double Min, double Max) {
            double[] Clipped = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                double Value = X[i];
                if (Value < Min) { Value = Min; }
                if (Value > Max) { Value = Max; }
                Clipped[i] = Value;
            }
            return Clipped;
        }

        private static double[] Add(double[] X, double Value) {
            double[] Total = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                Total[i] = X[i] + Value;
            }
            return Total;
        }

        private static double[] Log(double[] X) {
            double[] Logged = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                Logged[i] = System.Math.Log(X[i]);
            }
            return Logged;
        }

        private static double[] Multiply(double[] X, double[] y) {
            double[] Multiplied = new double[X.Length];
            for (int i = 0; i < X.Length; i++) {
                Multiplied[i] = X[i] * y[i];
            }
            return Multiplied;
        }
        
        private static double[][] Diff(double[][] X, double[][] y) {
            int rowCount = X.Length;
            int colCount = X[0].Length;
            double[][] Difference = new double[rowCount][];
            for (int i = 0; i < rowCount; i++) {
                double[] subDifference = new double[colCount];
                for (int j = 0; j < colCount; j++) {
                    subDifference[j] = X[i][j] - y[i][j];
                }
                Difference[i] = subDifference;
            } 
            return Difference;
        }

        private static double[][] Sqr(double[][] X) {
            int rowCount = X.Length;
            int colCount = X[0].Length;
            double[][] Squared = new double[rowCount][];
            for (int i = 0; i < rowCount; i++) {
                double[] subSquared = new double[colCount];
                for (int j = 0; j < colCount; j++) {
                    subSquared[j] = X[i][j] * X[i][j];
                }
                Squared[i] = subSquared;
            }
            return Squared;
        }

        private static double Sum(double[][] X) {
            double Sum = 0;
            int rowCount = X.Length;
            int colCount = X[0].Length;
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < colCount; j++) {
                    Sum += X[i][j];
                }
            }
            return Sum;
        }

        private static double Mean(double[][] X) {
            return Sum(X) / (X.Length * X[0].Length);
        }

        private static double[][] Abs(double[][] X) {
            int rowCount = X.Length;
            int colCount = X[0].Length;
            double[][] Absolute = new double[rowCount][];
            for (int i = 0; i < rowCount; i++) {
                double[] subAbsolute = new double[colCount];
                for (int j = 0; j < colCount; j++) {
                    subAbsolute[j] = System.Math.Abs(X[i][j]);
                }
                Absolute[i] = subAbsolute;
            }
            return Absolute;
        }

        private static double[][] Clip(double[][] X, double Min, double Max) {
            int rowCount = X.Length;
            int colCount = X[0].Length;
            double[][] Clipped = new double[rowCount][];
            for (int i = 0; i < rowCount; i++) {
                double[] subClipped = new double[colCount];
                for (int j = 0; j < colCount; j++) {
                    double Value = X[i][j];
                    if (Value < Min) { Value = Min; }
                    if (Value > Max) { Value = Max; }
                    subClipped[j] = Value;
                }
                Clipped[i] = subClipped;
            }
            return Clipped;
        }

        private static double[][] Add(double[][] X, double Value) {
            int rowCount = X.Length;
            int colCount = X[0].Length;
            double[][] Total = new double[rowCount][];
            for (int i = 0; i < rowCount; i++) {
                double[] subTotal = new double[colCount];
                for (int j = 0; j < colCount; j++) {
                    subTotal[j] = X[i][j] + Value;
                }
                Total[i] = subTotal;
            }
            return Total;
        }

        private static double[][] Log(double[][] X) {
            int rowCount = X.Length;
            int colCount = X[0].Length;
            double[][] Logged = new double[rowCount][];
            for (int i = 0; i < rowCount; i++) {
                double[] subLogged = new double[colCount];
                for (int j = 0; j < colCount; j++) {
                    subLogged[j] = System.Math.Log(X[i][j]);
                }
                Logged[i] = subLogged;
            }
            return Logged;
        }
        
        private static double[][] Multiply(double[][] X, double[][] y) {
            int rowCount = X.Length;
            int colCount = X[0].Length;
            double[][] Multiplied = new double[rowCount][];
            for (int i = 0; i < rowCount; i++) {
                double[] subMultiplied = new double[colCount];
                for (int j = 0; j < colCount; j++) {
                    subMultiplied[j] = X[i][j] * y[i][j];
                }
                Multiplied[i] = subMultiplied;
            }
            return Multiplied;
        }
        
        public static double MSE(double[][] pred, double[][] expected) {
            double[][] difference = Diff(pred, expected);
            double[][] differenceSquared = Sqr(difference);
            double meanDifferenceSquared = Mean(differenceSquared);
            return meanDifferenceSquared;
        }
        
        public static double[][] MSEGradient(double[][] Pred, double[][] Expected) {
            int batchSize = Pred.Length;
            int outputCount = Pred[0].Length;
            double[][] gradient = new double[batchSize][];
    
            for (int i = 0; i < batchSize; i++) {
                double[] subGradient = new double[outputCount];
                for (int j = 0; j < outputCount; j++) {
                    subGradient[j] = 2 * (Pred[i][j] - Expected[i][j]) / outputCount;
                }
                gradient[i] = subGradient;
            }
            return gradient;
        }

        public static double RMSE(double[][] Pred, double[][] Expected) {
            return System.Math.Sqrt(MSE(Pred, Expected));
        }

        public static double MAE(double[][] Pred, double[][] Expected) {
            double[][] difference = Diff(Pred, Expected);
            double[][] absoluteDifference = Abs(difference);
            double meanAbsoluteDifference = Mean(absoluteDifference);
            return meanAbsoluteDifference;
        }
        
        public static double CrossEntropy(double[][] Pred, double[][] Expected, double Epsilon = 1e-10) {
            double Loss = -Sum(Multiply(Expected, Log(Pred)));
            return Loss / Pred.Length;
        }

    }
}