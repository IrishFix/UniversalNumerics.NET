using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ComputationalGeometry {
    public static class Conversion {
        [Pure] public static Vector3 Vector2ToVector3XY(Vector2 Vector) {
            return new Vector3(Vector.x, Vector.y, 0);
        }

        [Pure] public static Vector3 Vector2ToVector3XZ(Vector2 Vector) {
            return new Vector3(Vector.x, 0, Vector.y);
        }
        
        [Pure] public static Vector2 Vector3ToVector2XY(Vector3 Vector) {
            return new Vector2(Vector.x, Vector.y);
        }

        [Pure] public static Vector2 Vector3ToVector2XZ(Vector3 Vector) {
            return new Vector2(Vector.x, Vector.z);
        }
    }
}