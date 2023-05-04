using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking.Regularizers {
    public interface IRegularizer {
        public void Regularize(Dense Layer);
    }
}