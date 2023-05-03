using System;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    [Serializable]
    public class Triangle3D {
        public Edge3D[] Edges;
        public Vector3[] Vertices;
        
        public Vector3 A => Vertices[0];
        public Vector3 B => Vertices[1];
        public Vector3 C => Vertices[2];
        
        public Triangle3D(Vector3 a, Vector3 b, Vector3 c) {
            Vertices = new[] {a,b,c};
            Edges = new[] {new Edge3D(Vertices[0],Vertices[1]),new Edge3D(Vertices[1],Vertices[2]),new Edge3D(Vertices[2],Vertices[0])};
        }

        public Triangle3D(Edge3D a, Edge3D b, Vector3 c) {
            Vertices = new[] {a.Vertices[0],b.Vertices[0],c};
            Edges = new[] {new Edge3D(Vertices[0],Vertices[1]),new Edge3D(Vertices[1],Vertices[2]),new Edge3D(Vertices[2],Vertices[0])};
        }

        public Triangle3D(Triangle2D Triangle, bool XZLayout, float FillNumber = 0f) {
            Vertices = XZLayout ? new[] {Triangle.A.ToVector3XZ(FillNumber),Triangle.B.ToVector3XZ(FillNumber),Triangle.C.ToVector3XZ(FillNumber)} :
                new[] {Triangle.A.ToVector3XY(FillNumber),Triangle.B.ToVector3XY(FillNumber),Triangle.C.ToVector3XY(FillNumber)};
            Edges = new[] {new Edge3D(Vertices[0],Vertices[1]),new Edge3D(Vertices[1],Vertices[2]),new Edge3D(Vertices[2],Vertices[0])};
        }
    }
}