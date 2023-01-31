using JetBrains.Annotations;
using UnityEngine;

namespace ComputationalGeometry.Runtime {
    internal static class Conversion {
        [Pure] internal static Vector3 Vector2ToVector3XY(Vector2 Vector) {
            return new Vector3(Vector.x, Vector.y, 0);
        }

        [Pure] internal static Vector3 Vector2ToVector3XZ(Vector2 Vector) {
            return new Vector3(Vector.x, 0, Vector.y);
        }
        
        [Pure] internal static Vector2 Vector3ToVector2XY(Vector3 Vector) {
            return new Vector2(Vector.x, Vector.y);
        }

        [Pure] internal static Vector2 Vector3ToVector2XZ(Vector3 Vector) {
            return new Vector2(Vector.x, Vector.z);
        }
    }
}