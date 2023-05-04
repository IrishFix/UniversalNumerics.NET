using System;
using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Regularizers {
    public class Dropout : IRegularizer {
        private readonly double DropoutRate;
        private readonly Random Rand;

        public Dropout(double dropoutRate) {
            DropoutRate = dropoutRate;
            Rand = new Random();
        }

        public void Regularize(Dense layer) {
            for (int i = 0; i < layer.OutputCount; i++) {
                for (int j = 0; j < layer.InputCount; j++) {
                    if (Rand.NextDouble() < DropoutRate) {
                        layer.Weights[i][j] = 0;
                    }
                }
            }
        }
    }
}