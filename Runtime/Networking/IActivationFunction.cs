// ReSharper disable once CheckNamespace
namespace TensorMath.Networking.activations {
    public interface IActivationFunction {
        double[][] BatchActivate(double[][] input);
        double[][] BatchInverseActivate(double[][] input);
    }
}