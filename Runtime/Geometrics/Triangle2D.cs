using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    [Serializable]
    public class Triangle2D {
        public Edge2D[] Edges;
        public Vector2[] Vertices;
        
        public Vector2 A => Vertices[0];
        public Vector2 B => Vertices[1];
        public Vector2 C => Vertices[2];

        public Vector2 Circumcenter;
        public double RadiusSquared;
 
        public Triangle2D(Vector2 a, Vector2 b, Vector2 c) {
            Vertices = !IsCounterClockwise(a,b,c) ? new[] {a,c,b} : new[] {a,b,c};
            Edges = new[] {new Edge2D(Vertices[0],Vertices[1]),new Edge2D(Vertices[1],Vertices[2]),new Edge2D(Vertices[2],Vertices[0])};
            UpdateCircumcircle();
        }

        public Triangle2D(Edge2D a, Vector2 b) {
            Vertices = !IsCounterClockwise(a.Vertices[0],a.Vertices[1],b) ? new[] {a.Vertices[0],b,a.Vertices[1]} : new[] {a.Vertices[0],a.Vertices[1],b};
            Edges = new[] {new Edge2D(Vertices[0],Vertices[1]),new Edge2D(Vertices[1],Vertices[2]),new Edge2D(Vertices[2],Vertices[0])};
            UpdateCircumcircle();
        }

        public Triangle2D(Edge2D Edge) {
            Vertices = new[] { Edge.Vertices[0], Edge.Vertices[1] };
            Edges = new[] { Edge };
            
            double Radius = Edge.GetLength();
            
            RadiusSquared = Radius * Radius;
            Circumcenter = Edge.GetMidpoint();
        }

        public Triangle2D(Triangle3D Triangle, bool FlattenToXZ=true) {
            Vector2 a, b, c; // Projects 3D Triangle to 2D Plane
            if (FlattenToXZ) { // To XZ Plane (If using XZ Plane, Y of 2d vectors will be Z of 3d vectors)
                a = Triangle.A.ToVector2XZ();
                b = Triangle.B.ToVector2XZ();
                c = Triangle.C.ToVector2XZ();
            }
            else { // To XY Plane (If using XY Plane, Y of 2d vectors will be Y of 3d vectors)
                a = Triangle.A.ToVector2XY();
                b = Triangle.B.ToVector2XY();
                c = Triangle.C.ToVector2XY();
            }
            Vertices = !IsCounterClockwise(a,b,c) ? new[] {a,c,b} : new[] {a,b,c};
            Edges = new[] {new Edge2D(Vertices[0],Vertices[1]),new Edge2D(Vertices[1],Vertices[2]),new Edge2D(Vertices[2],Vertices[0])};
            UpdateCircumcircle();
        }

        public bool SharesEdgeWith(Triangle2D Triangle) {
            return Edges.Any(Edge => Edge.Equals(Triangle.Edges[0]) || Edge.Equals(Triangle.Edges[1]) || Edge.Equals(Triangle.Edges[2]));
        }
        
        public Edge2D SharedEdgeWith(Triangle2D Triangle) {
            List<Edge2D> SharedEdges = Edges.Where(Edge => Edge.Equals(Triangle.Edges[0]) || Edge.Equals(Triangle.Edges[1]) || Edge.Equals(Triangle.Edges[2])).ToList();
            return SharedEdges.Count == 0 ? null : SharedEdges[0];
        }

        public Vector2 GetMidpoint() {
            return Vector2.Lerp(Vector2.Lerp(A, C, .5f), B, .5f);
        }

        public void UpdateCircumcircle() {
            Vector2 p0 = Vertices[0];
            Vector2 p1 = Vertices[1];
            Vector2 p2 = Vertices[2];
            
            double dA = p0.X * p0.X + p0.Y * p0.Y;
            double dB = p1.X * p1.X + p1.Y * p1.Y;
            double dC = p2.X * p2.X + p2.Y * p2.Y;

            double aux1 = dA * (p2.Y - p1.Y) + dB * (p0.Y - p2.Y) + dC * (p1.Y - p0.Y);
            double aux2 = -(dA * (p2.X - p1.X) + dB * (p0.X - p2.X) + dC * (p1.X - p0.X));
            double div = 2 * (p0.X * (p2.Y - p1.Y) + p1.X * (p0.Y - p2.Y) + p2.X * (p1.Y - p0.Y));

            Vector2 Center = new((float)(aux1 / div), (float)(aux2 / div));
            
            Circumcenter = Center;
            RadiusSquared = (Center.X - p0.X) * (Center.X - p0.X) + (Center.Y - p0.Y) * (Center.Y - p0.Y);
        }
        
        private bool IsCounterClockwise() {
            return (Vertices[1].X - Vertices[0].X) * (Vertices[2].Y - Vertices[0].Y) - (Vertices[2].X - Vertices[0].X) * (Vertices[1].Y - Vertices[0].Y) > 0;
        }
        
        public bool IsCounterClockwise(Vector2 a, Vector2 b, Vector2 c) {
            return (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y) > 0;
        }
        
        public bool IsPointInsideCircumcircle(Vector2 point) {
            return (point.X - Circumcenter.X) * (point.X - Circumcenter.X) + (point.Y - Circumcenter.Y) * (point.Y - Circumcenter.Y) < RadiusSquared;
        }
    }
}