using System;
using UnityEngine;

namespace ComputationalGeometry {
    [Serializable]
    internal class Edge3D {
        [SerializeField] public Vector3[] Vertices;
        
        internal Vector3 A => Vertices[0];
        internal Vector3 B => Vertices[1];

        internal Edge3D(Vector3 a, Vector3 b) {
            Vertices = new[] {a,b};
        }

        internal Edge3D(Edge2D Edge, bool XZLayout, float FillNumber = 0f) {
            Vertices = XZLayout ? new[] {Edge.A.ToVector3XZ(FillNumber),Edge.B.ToVector3XZ(FillNumber)} :
                new[] {Edge.A.ToVector3XY(FillNumber),Edge.B.ToVector3XY(FillNumber)};
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;

            if (obj is not Edge3D Edge) return false;

            bool samePoints = A == Edge.A && B == Edge.B;
            bool samePointsReversed = A == Edge.B && B == Edge.A;
            return samePoints || samePointsReversed;
        }

        public override int GetHashCode() {
            int hCode = (int)A.x ^ (int)A.y ^ (int)A.z ^ (int)B.x ^ (int)B.y ^ (int)B.z;
            return hCode.GetHashCode();
        }
    }
}