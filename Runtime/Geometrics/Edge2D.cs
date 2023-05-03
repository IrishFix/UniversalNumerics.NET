using System;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    [Serializable]
    public class Edge2D {
        public Vector2[] Vertices;
        
        public Vector2 A => Vertices[0];
        public Vector2 B => Vertices[1];

        public Edge2D(Vector2 a, Vector2 b) {
            Vertices = new[] {a,b};
        }
        
        public Edge2D(Edge3D Edge, bool FlattenToXZ=true) {
            Vertices = FlattenToXZ ? new[] {Edge.A.ToVector2XZ(),Edge.B.ToVector2XZ()} :
                new[] {Edge.A.ToVector2XY(),Edge.B.ToVector2XY()};
        }

        public float GetLength() {
            return Vector2.Distance(A, B);
        }

        public Vector2 GetMidpoint() {
            return new Vector2((A.X + B.X) / 2, (A.Y + B.Y) / 2);
        }

        public bool EncroachedByPoint(Vector2 Point, float Tolerance = 0.001f) {
            float SqrTol = Tolerance * Tolerance;
            if (Vector2.DistanceSquared(A,Point) < SqrTol || Vector2.DistanceSquared(B,Point) < SqrTol) {
                return false;
            }
            Vector2 Midpoint = GetMidpoint();
            float Radius = GetLength()/2f;
            float RadiusSquared = Radius * Radius;
            return (Point.X - Midpoint.X) * (Point.X - Midpoint.X) + (Point.Y - Midpoint.Y) * (Point.Y - Midpoint.Y) < RadiusSquared;
        } 
        
        public override bool Equals(object obj) {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;

            if (obj is not Edge2D Edge) return false;

            bool samePoints = A == Edge.A && B == Edge.B;
            bool samePointsReversed = A == Edge.B && B == Edge.A;
            return samePoints || samePointsReversed;
        }

        public override int GetHashCode() {
            int hCode = (int)A.X ^ (int)A.Y ^ (int)B.X ^ (int)B.Y;
            return hCode.GetHashCode();
        }
    }
}