using System;

// ReSharper disable once CheckNamespace
namespace TensorMath.Networking.activations {
    #region LinearActivation
    public class Linear: IActivationFunction {
        public double Activate(double input) {
            return input;
        }

        public double[][] BatchActivate(double[][] input) {
            return input;
        }

        public double InverseActivate(double input) {
            return 1;
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion

    #region SigmoidActivation
    public class Sigmoid: IActivationFunction {
        public double Activate(double input) {
            return 1 / (1 + System.Math.Exp(-input));
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            return Activate(input) * (1 - Activate(input));
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion

    #region HardSigmoidActivation
    public class HardSigmoid: IActivationFunction {
        public double Activate(double input) {
            return System.Math.Min(1, System.Math.Max(0, 0.2 * input + 0.5));
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            return input switch {
                < -2.5 => 0,
                > 2.5 => 0,
                _ => 0.2
            };
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion

    #region TanhActivation
    public class Tanh: IActivationFunction {
        public double Activate(double input) {
            return System.Math.Tanh(input);
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            return 1 - System.Math.Pow(System.Math.Tanh(input), 2);
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion

    #region ReLUActivation
    public class ReLU: IActivationFunction {
        public double Activate(double input) {
            return System.Math.Max(0, input);
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            if (input > 0) {
                return 1;
            }
            return 0;
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion

    #region LeakyReLUActivation
    public class LeakyReLU: IActivationFunction {
        public double Activate(double input) {
            return System.Math.Max(0.1*input, input);
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            if (input >= 0) {
                return 1;
            }
            return 0.1;
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion
    
    #region SoftplusActivation
    public class Softplus: IActivationFunction {
        public double Activate(double input) {
            return System.Math.Log(1 + System.Math.Exp(input));
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            return new Sigmoid().Activate(input);
        }

        public double[][] BatchInverseActivate(double[][] input) {
            return new Sigmoid().BatchActivate(input);
        }
    }
    #endregion
    
    #region SwishActivation
    public class Swish: IActivationFunction {
        public double Activate(double input) {
            return input * new Sigmoid().Activate(input);
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            return Activate(input) + new Sigmoid().Activate(input) * (1-Activate(input));
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion
    
    #region GeLUActivation
    public class GeLU: IActivationFunction {
        public double Activate(double input) {
            double cdf = 0.5 * (1.0 + System.Math.Tanh(System.Math.Sqrt(2 / System.Math.PI) * (input + 0.044715 * System.Math.Pow(input, 3))));
            double pdf = System.Math.Sqrt(2 / System.Math.PI) * (0.5 * System.Math.Pow(System.Math.E, -0.5 * System.Math.Pow(input, 2)) + 0.0535161 * System.Math.Pow(input, 3) * 0.79788456 * System.Math.Pow(System.Math.E, -0.5 * System.Math.Pow(input, 2)));
            return cdf + input * pdf;
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            return 0.5 * input * (1 + System.Math.Tanh(System.Math.Sqrt(2 / System.Math.PI) * (input + 0.044715 * System.Math.Pow(input, 3))));
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion
    
    #region ELUActivation
    public class ELU: IActivationFunction {
        public double Activate(double input) {
            return input < 0 ? System.Math.Exp(input) - 1 : input;
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            return input < 0 ? Activate(input) + 1 : 1; 
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion
    
    #region PRELUActivation
    public class PRELU: IActivationFunction {
        public double Activate(double input, double alpha=0.1) {
            if (input >= 0) { return input; }
            return alpha * input;
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input, double alpha=0.1) {
            if (input >= 0) { return 1; } return alpha; 
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion
    
    #region MishActivation
    public class Mish: IActivationFunction {
        public double Activate(double input) {
            double e = System.Math.Exp(input);
            return input * System.Math.Tanh(System.Math.Log(1 + e));
        }

        public double[][] BatchActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = Activate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
        
        public double InverseActivate(double input) {
            double e = System.Math.Exp(input);
            double numerator = e * (2*input + 2) + 2*e + input*(2*e + 1);
            double denominator = (e + 1) * (e + 1);
            return (4*input + 4*e + 2) * (e*input + e + 1) * denominator - numerator * (2*e + 2*input + 2) * e * (e + 1) / (denominator * denominator);
        }

        public double[][] BatchInverseActivate(double[][] input) {
            double[][] output = new double[input.Length][];
            for (int i = 0; i < output.Length; i++) {
                double[] outputRow = new double[input[i].Length];
                for (int j = 0; j < outputRow.Length; j++) {
                    outputRow[j] = InverseActivate(input[i][j]);
                }
                output[i] = outputRow;
            }
            return output;
        }
    }
    #endregion
}