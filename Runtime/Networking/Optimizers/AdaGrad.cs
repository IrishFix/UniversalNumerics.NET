using System.Collections.Generic;
using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Optimizers {
    public class AdaGrad : IOptimizer {
        private readonly double LearningRate;
        private readonly double Epsilon;
        
        private readonly Dictionary<Dense, double[][]> weightsCache = new Dictionary<Dense, double[][]>();
        private readonly Dictionary<Dense, double[]> biasesCache = new Dictionary<Dense, double[]>();

        public AdaGrad(double learningRate=0.01, double epsilon=1e-7) {
            LearningRate = learningRate;
            Epsilon = epsilon;
        }

        public void Optimize(Dense layer) {
            if (!weightsCache.ContainsKey(layer)) {
                weightsCache[layer] = new double[layer.OutputCount][];
                for (int i = 0; i < layer.OutputCount; i++) {
                    weightsCache[layer][i] = new double[layer.InputCount];
                }
            }

            for (int i = 0; i < layer.OutputCount; i++) {
                for (int j = 0; j < layer.InputCount; j++) {
                    weightsCache[layer][i][j] += layer.WeightsGradients[i][j] * layer.WeightsGradients[i][j];
                    double denom = System.Math.Sqrt(weightsCache[layer][i][j] + Epsilon);
                    layer.Weights[i][j] -= LearningRate * layer.WeightsGradients[i][j] / (denom+Epsilon);
                }
            }

            if (!biasesCache.ContainsKey(layer)) {
                biasesCache[layer] = new double[layer.OutputCount];
            }

            for (int i = 0; i < layer.OutputCount; i++) {
                biasesCache[layer][i] += layer.BiasesGradients[i] * layer.BiasesGradients[i];
                double denom = System.Math.Sqrt(biasesCache[layer][i] + Epsilon);
                layer.Biases[i] -= LearningRate * layer.BiasesGradients[i] / (denom+Epsilon);
            }
        }
    }
}