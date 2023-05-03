using System.Collections.Generic;
using TensorMath.Networking.layers;

// ReSharper disable once CheckNamespace
namespace TensorMath.Networking.optimizers {
    public class Adam: IOptimizer {
        private readonly double LearningRate;
        private readonly double Beta1;
        private readonly double Beta2;
        private readonly double Epsilon;

        private readonly Dictionary<Dense, double[][]> mWeightsCache = new Dictionary<Dense, double[][]>();
        private readonly Dictionary<Dense, double[]> mBiasesCache = new Dictionary<Dense, double[]>();
        private readonly Dictionary<Dense, double[][]> vWeightsCache = new Dictionary<Dense, double[][]>();
        private readonly Dictionary<Dense, double[]> vBiasesCache = new Dictionary<Dense, double[]>();

        private int t = 0;

        public Adam(double learningRate = 0.01, double beta1 = 0.9, double beta2 = 0.999, double epsilon = 1e-8) {
            LearningRate = learningRate;
            Beta1 = beta1;
            Beta2 = beta2;
            Epsilon = epsilon;
        }

        public void Optimize(Dense layer) {
            t++;

            if (!mWeightsCache.ContainsKey(layer)) {
                mWeightsCache[layer] = new double[layer.OutputCount][];
                vWeightsCache[layer] = new double[layer.OutputCount][];
                for (int i = 0; i < layer.OutputCount; i++) {
                    mWeightsCache[layer][i] = new double[layer.InputCount];
                    vWeightsCache[layer][i] = new double[layer.InputCount];
                }
            }
            
            for (int i = 0; i < layer.OutputCount; i++) {
                for (int j = 0; j < layer.InputCount; j++) {
                    mWeightsCache[layer][i][j] = Beta1 * mWeightsCache[layer][i][j] + (1 - Beta1) * layer.WeightsGradients[i][j];
                    vWeightsCache[layer][i][j] = Beta2 * vWeightsCache[layer][i][j] + (1 - Beta2) * layer.WeightsGradients[i][j] * layer.WeightsGradients[i][j];
                    double mHat = mWeightsCache[layer][i][j] / (1 - System.Math.Pow(Beta1, t));
                    double vHat = vWeightsCache[layer][i][j] / (1 - System.Math.Pow(Beta2, t));
                    layer.Weights[i][j] -= LearningRate * mHat / (System.Math.Sqrt(vHat) + Epsilon);
                }
            }
            
            if (!mBiasesCache.ContainsKey(layer)) {
                mBiasesCache[layer] = new double[layer.OutputCount];
                vBiasesCache[layer] = new double[layer.OutputCount];
            }

            for (int i = 0; i < layer.OutputCount; i++) {
                mBiasesCache[layer][i] = Beta1 * mBiasesCache[layer][i] + (1 - Beta1) * layer.BiasesGradients[i];
                vBiasesCache[layer][i] = Beta2 * vBiasesCache[layer][i] + (1 - Beta2) * layer.BiasesGradients[i] * layer.BiasesGradients[i];
                double mHat = mBiasesCache[layer][i] / (1 - System.Math.Pow(Beta1, t));
                double vHat = vBiasesCache[layer][i] / (1 - System.Math.Pow(Beta2, t));
                layer.Biases[i] -= LearningRate * mHat / (System.Math.Sqrt(vHat) + Epsilon);
            }
        }
    }
}