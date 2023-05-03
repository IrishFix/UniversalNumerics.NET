using System.Collections.Generic;
using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Optimizers {
    public class RMSprop : IOptimizer {
        private readonly double LearningRate;
        private readonly double DecayRate;
        private readonly double Epsilon;
        
        private readonly Dictionary<Dense, double[][]> weightsCache = new Dictionary<Dense, double[][]>();
        private readonly Dictionary<Dense, double[]> biasesCache = new Dictionary<Dense, double[]>();

        public RMSprop(double learningRate=0.01, double decayRate=0.95, double epsilon=1e-7) {
            LearningRate = learningRate;
            DecayRate = decayRate;
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
                    weightsCache[layer][i][j] = DecayRate * weightsCache[layer][i][j] + (1 - DecayRate) * layer.WeightsGradients[i][j] * layer.WeightsGradients[i][j];
                    layer.Weights[i][j] -= LearningRate * layer.WeightsGradients[i][j] / (System.Math.Sqrt(weightsCache[layer][i][j]) + Epsilon);
                }
            }

            biasesCache.TryAdd(layer, new double[layer.OutputCount]);

            for (int i = 0; i < layer.OutputCount; i++) {
                biasesCache[layer][i] = DecayRate * biasesCache[layer][i] + (1 - DecayRate) * layer.BiasesGradients[i] * layer.BiasesGradients[i];
                layer.Biases[i] -= LearningRate * layer.BiasesGradients[i] / (System.Math.Sqrt(biasesCache[layer][i]) + Epsilon);
            }
        }
    }
}