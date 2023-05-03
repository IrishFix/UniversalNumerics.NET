using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking {
    public interface IOptimizer {
        void Optimize(Dense layer);
    }
}