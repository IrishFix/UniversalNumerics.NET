using System;
using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Regularizers {
    public class L1: IRegularizer {
        private readonly double Strength;

        public L1(double strength) {
            Strength = strength;
        }

        public void Regularize(Dense layer) {
            for (int i = 0; i < layer.OutputCount; i++) {
                for (int j = 0; j < layer.InputCount; j++) {
                    double sign = Math.Sign(layer.Weights[i][j]);
                    layer.Weights[i][j] -= Strength * sign;
                }
            }
        }
    }
}