// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Activations {
    public interface IActivationFunction {
        double[][] BatchActivate(double[][] input);
        double[][] BatchInverseActivate(double[][] input);
    }
}