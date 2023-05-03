using TensorMath.Networking.layers;

// ReSharper disable once CheckNamespace
namespace TensorMath.Networking.optimizers {
    public class SGD : IOptimizer {
        private readonly double LearningRate;

        public SGD(double learningRate=0.01) {
            LearningRate = learningRate;
        }

        public void Optimize(Dense layer) {
            for (int i = 0; i < layer.OutputCount; i++) {
                for (int j = 0; j < layer.InputCount; j++) {
                    layer.Weights[i][j] -= LearningRate * layer.WeightsGradients[i][j];
                }
                layer.Biases[i] -= LearningRate * layer.BiasesGradients[i];
            }
        }
    }
}