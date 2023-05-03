using System.Collections.Generic;
using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Optimizers {
    public class SGDMomentum : IOptimizer {
        private readonly double LearningRate;
        private readonly double MomentumRate;
        private readonly Dictionary<Dense, double[][]> Velocities;

        public SGDMomentum(double learningRate=0.01, double momentumRate=0.1) {
            LearningRate = learningRate;
            MomentumRate = momentumRate;
            Velocities = new Dictionary<Dense, double[][]>();
        }

        public void Optimize(Dense layer) {
            if (!Velocities.ContainsKey(layer)) {
                Velocities[layer] = new double[layer.OutputCount][];
                for (int i = 0; i < layer.OutputCount; i++) {
                    Velocities[layer][i] = new double[layer.InputCount];
                }
            }

            double[][] velocity = Velocities[layer];

            for (int i = 0; i < layer.OutputCount; i++) {
                for (int j = 0; j < layer.InputCount; j++) {
                    velocity[i][j] = MomentumRate * velocity[i][j] - LearningRate * layer.WeightsGradients[i][j];
                    layer.Weights[i][j] += velocity[i][j];
                }
                layer.Biases[i] -= LearningRate * layer.BiasesGradients[i];
            }
        }
    }
}