using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Regularizers {
    public class L2: IRegularizer {
        private readonly double Strength;
        
        public L2(double strength) {
            Strength = strength;
        }
        
        public void Regularize(Dense Layer) {
            for (int i = 0; i < Layer.OutputCount; i++) {
                for (int j = 0; j < Layer.InputCount; j++) {
                    Layer.Weights[i][j] *= 1 - Strength;
                }
            }
        }
    }
}