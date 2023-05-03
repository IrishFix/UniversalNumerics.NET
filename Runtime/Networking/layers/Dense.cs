using System;
using System.Collections.Generic;
using System.Linq;
using TensorMath.Networking.activations;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
namespace TensorMath.Networking.layers {
    public class Dense : ILayer {
        public string ClassName = "Dense";

        public int InputCount;
        public int OutputCount;

        public IActivationFunction Activation;

        public double[][] Weights;
        public double[] Biases;

        public double[][] Outputs;
        public double[][] Inputs;

        public double[][] WeightsGradients;
        public double[] BiasesGradients;

        public Dense(int NumNeurons, IActivationFunction ActivationFunc) {
            InputCount = 0;
            OutputCount = NumNeurons;
            Activation = ActivationFunc;
        }

        public double[][] Forward(double[][] Batch) {
            int batchSize = Batch.Length;
            double[][] dotProduct = new double[batchSize][];
            double[][] preActivation = new double[batchSize][];
    
            for (int i = 0; i < batchSize; i++) {
                dotProduct[i] = new double[OutputCount];
                preActivation[i] = new double[OutputCount];

                for (int j = 0; j < OutputCount; j++) {
                    for (int k = 0; k < InputCount; k++) {
                        dotProduct[i][j] += Batch[i][k] * Weights[j][k];
                    }

                    preActivation[i][j] = dotProduct[i][j] + Biases[j];
                }
            }

            Outputs = Activation.BatchActivate(preActivation);;
            Inputs = Batch;
            return Outputs;
        }

        public double[][] Backward(double[][] Gradients) {
            double[][] inputGradients = new double[Gradients.Length][];
            double[][] weightsGradients = new double[OutputCount][];
            double[][] derivedActivations = Activation.BatchInverseActivate(Outputs);
            double[] biasesGradients = new double[OutputCount];

            for (int i = 0; i < Gradients.Length; i++) {
                inputGradients[i] = new double[InputCount];

                for (int j = 0; j < OutputCount; j++) {
                    double outputGradient = Gradients[i][j] * derivedActivations[i][j];

                    biasesGradients[j] += outputGradient;

                    for (int k = 0; k < InputCount; k++) {
                        if (weightsGradients[j] == null) {
                            weightsGradients[j] = new double[InputCount];
                        }

                        weightsGradients[j][k] += Inputs[i][k] * outputGradient;
                        inputGradients[i][k] += Weights[j][k] * outputGradient;
                    }
                }
            }

            WeightsGradients = weightsGradients;
            BiasesGradients = biasesGradients;

            return inputGradients;
        }
    }
}