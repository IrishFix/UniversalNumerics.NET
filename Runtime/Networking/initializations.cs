using System;
using tensormathdotnet.Runtime.Networking;

// ReSharper disable once CheckNamespace
namespace TensorMath.Networking.initializations {
    #region LeCunInitializer
    public class LeCun : IInitializer {
        private double GetRandomNumber(double minimum, double maximum) { 
            Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
        public double[][] Fill(int inputCount, int outputCount) {
            double Limit = System.Math.Sqrt(3 / (float)inputCount);
            double[][] Weights = new double[outputCount][];
            for (int i = 0; i < Weights.Length; i++) {
                double[] WeightsRow = new double[inputCount];
                for (int j = 0; j < WeightsRow.Length; j++) {
                    WeightsRow[j] = GetRandomNumber(-Limit, Limit);
                }
                Weights[i] = WeightsRow;
            }
            return Weights;
        }
    }
    #endregion

    #region KaimingInitializer
    public class Kaiming : IInitializer {
        private double GetRandomNumber(double minimum, double maximum) { 
            Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
        public double[][] Fill(int inputCount, int outputCount) {
            double Limit = System.Math.Sqrt(6 / (float)inputCount);
            double[][] Weights = new double[outputCount][];
            for (int i = 0; i < Weights.Length; i++) {
                double[] WeightsRow = new double[inputCount];
                for (int j = 0; j < WeightsRow.Length; j++) {
                    WeightsRow[j] = GetRandomNumber(-Limit, Limit);
                }
                Weights[i] = WeightsRow;
            }
            return Weights;
        }
    }
    #endregion
    
    #region XavierInitializer
    public class Xavier : IInitializer {
        private double GetRandomNumber(double minimum, double maximum) { 
            Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
        public double[][] Fill(int inputCount, int outputCount) {
            double Limit = System.Math.Sqrt(6 / (float)(inputCount + outputCount));
            double[][] Weights = new double[outputCount][];
            for (int i = 0; i < Weights.Length; i++) {
                double[] WeightsRow = new double[inputCount];
                for (int j = 0; j < WeightsRow.Length; j++) {
                    WeightsRow[j] = GetRandomNumber(-Limit, Limit);
                }
                Weights[i] = WeightsRow;
            }
            return Weights;
        }
    }
    #endregion
    
    #region NormalizedXavierInitializer
    public class NormalizedXavier : IInitializer {
        private double GetRandomNumber(double minimum, double maximum) { 
            Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
        public double[][] Fill(int inputCount, int outputCount) {
            double Limit = System.Math.Sqrt(6) / System.Math.Sqrt(inputCount + outputCount);
            double[][] Weights = new double[outputCount][];
            for (int i = 0; i < Weights.Length; i++) {
                double[] WeightsRow = new double[inputCount];
                for (int j = 0; j < WeightsRow.Length; j++) {
                    WeightsRow[j] = GetRandomNumber(-Limit, Limit);
                }
                Weights[i] = WeightsRow;
            }
            return Weights;
        }
    }
    #endregion
    
    #region GaussianInitializer
    public class Gaussian : IInitializer {
        
        private static double GetRandomNumber(double minimum, double maximum) { 
            Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        private double NextGaussian() {
            float v1, v2, s;
            
            do {
                v1 = 2.0f * (float)GetRandomNumber(0f, 1f) - 1.0f;
                v2 = 2.0f * (float)GetRandomNumber(0f, 1f) - 1.0f;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1.0f || s == 0f);

            s = (float)System.Math.Sqrt((-2.0f * System.Math.Log(s)) / s);

            return v1 * s;
        }

        private double NextGaussian(double Mean, double Std) {
            return Mean + NextGaussian() * Std;
        }
        
        private double NextGaussian(double Mean, double Std, double Min, double Max) {
            double x;
            
            do {
                x = NextGaussian(Mean, Std);
            } while (x < Min || x > Max);

            return x;
        }
        
        public double[][] Fill(int inputCount, int outputCount) {
            double[][] Weights = new double[outputCount][];
            for (int i = 0; i < Weights.Length; i++) {
                double[] WeightsRow = new double[inputCount];
                for (int j = 0; j < WeightsRow.Length; j++) {
                    WeightsRow[j] = NextGaussian(0.0f, System.Math.Sqrt(2f / inputCount));
                }
                Weights[i] = WeightsRow;
            }
            return Weights;
        }
    }
    #endregion
}