// ReSharper disable once CheckNamespace
namespace TensorMath.Networking {
    public interface ILayer {
        public double[][] Forward(double[][] Batch);
        public double[][] Backward(double[][] Gradients);
    }
}