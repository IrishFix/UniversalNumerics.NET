using TensorMath.Networking.layers;

// ReSharper disable once CheckNamespace
namespace TensorMath.Networking {
    public interface IOptimizer {
        void Optimize(Dense layer);
    }
}