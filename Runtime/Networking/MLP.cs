using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using UniversalNumerics.Networking.Layers;
using UniversalNumerics.Networking;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking {
    public class MLP: IModel {
        public string ClassName = "MLP";

        public Dense[] Layers;

        public MLP(params Dense[] ModelLayers) {
            Layers = ModelLayers;
        }
        
        public MLP(IEnumerable<Dense> ModelLayers) {
            Layers = ModelLayers.ToArray();
        }

        public MLP(MLP templateNetwork) {
            int templateLayerCount = templateNetwork.Layers.Length;
            Layers = new Dense[templateLayerCount];

            for (int i = 0; i < templateLayerCount; i++) {
                Dense layer = templateNetwork.Layers[i];

                int weightRows = layer.Weights.Length;
                int weightCols = layer.Weights[0].Length;
                double[][] clonedWeights = new double[weightRows][];
                for (int j = 0; j < weightRows; j++) {
                    clonedWeights[j] = new double[weightCols];
                    for (int k = 0; k < weightCols; k++) {
                        clonedWeights[j][k] = layer.Weights[j][k];
                    }
                }

                Dense clonedLayer = new Dense(layer.OutputCount, layer.Activation) {
                    InputCount = layer.InputCount,
                    Weights = clonedWeights,
                    Biases = layer.Biases.ToArray()
                };

                Layers[i] = clonedLayer;
            }
        }

        public MLP Build(int InputCount) {
            int CurrentInputCount = InputCount;
            foreach (Dense Layer in Layers) {
                Layer.InputCount = CurrentInputCount;
                CurrentInputCount = Layer.OutputCount;
            }
            return this;
        }

        public MLP Compile(IInitializer Initializer) {
            foreach (Dense Layer in Layers) {
                Layer.Weights = Initializer.Fill(Layer.InputCount, Layer.OutputCount);
                Layer.Biases = new double[Layer.OutputCount];
                for (int i = 0; i < Layer.OutputCount; i++) {
                    Layer.Biases[i] = 0;
                }
            }
            return this;
        }

        public double[][] Forward(double[][] Batch) {
            double[][] CurrentInputs = Batch;
            foreach (Dense Layer in Layers) {
                CurrentInputs = Layer.Forward(CurrentInputs);
            }
            return CurrentInputs;
        }

        public MLP Fit(double[][][] X, double[][][] y, int Epochs, IOptimizer Optimizer) {
            int inputCount = X.Length;

            for (int Epoch = 0; Epoch < Epochs; Epoch++) {
                double epochError = 0;
                for (int i = 0; i < inputCount; i++) {
                    double[][] output = Forward(X[i]);
                    double error = Loss.MSE(y[i], output);

                    epochError += error;
                    double[][] outputGradient = Loss.MSEGradient(output, y[i]);

                    for (int j = Layers.Length - 1; j >= 0; j--) {
                        outputGradient = Layers[j].Backward(outputGradient);
                        Optimizer.Optimize(Layers[j]);
                    }
                }
                Console.WriteLine($"Epoch {Epoch + 1}: Total Error = {epochError}");
            }

            return this;
        }

        public string GetArchitecture() {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}