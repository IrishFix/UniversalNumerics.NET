using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public struct Voxel {
        public Vector3 position;
        public float size;

        public Voxel(Vector3 position, float size) {
            this.position = position;
            this.size = size;
        }
    }
}