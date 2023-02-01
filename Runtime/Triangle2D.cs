//  Computational Geometry, a package designed to ease the use of geometry-based mathematical functions.
//  Copyright © 2023 Ben Knight
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License v3.0 only as published by
//  the Free Software Foundation.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see https://www.gnu.org/licenses/agpl-3.0.html.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ComputationalGeometry {
    [Serializable]
    public class Triangle2D {
        [SerializeField] public Edge2D[] Edges;
        [SerializeField] public Vector2[] Vertices;
        
        internal Vector2 A => Vertices[0];
        internal Vector2 B => Vertices[1];
        internal Vector2 C => Vertices[2];

        [SerializeField] internal Vector2 Circumcenter;
        [SerializeField] internal double RadiusSquared;
 
        public Triangle2D(Vector2 a, Vector2 b, Vector2 c) {
            Vertices = !IsCounterClockwise(a,b,c) ? new[] {a,c,b} : new[] {a,b,c};
            Edges = new[] {new Edge2D(Vertices[0],Vertices[1]),new Edge2D(Vertices[1],Vertices[2]),new Edge2D(Vertices[2],Vertices[0])};
            UpdateCircumcircle();
        }

        public Triangle2D(Edge2D a, Edge2D b, Vector2 c) {
            Vertices = !IsCounterClockwise(a.Vertices[0],b.Vertices[0],c) ? new[] {a.Vertices[0],c,b.Vertices[0]} : new[] {a.Vertices[0],b.Vertices[0],c};
            Edges = new[] {new Edge2D(Vertices[0],Vertices[1]),new Edge2D(Vertices[1],Vertices[2]),new Edge2D(Vertices[2],Vertices[0])};
            UpdateCircumcircle();
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

        [CanBeNull]
        public Edge2D SharedEdgeWith(Triangle2D Triangle) {
            List<Edge2D> SharedEdges = Edges.Where(Edge => Edge.Equals(Triangle.Edges[0]) || Edge.Equals(Triangle.Edges[1]) || Edge.Equals(Triangle.Edges[2])).ToList();
            return SharedEdges.Count == 0 ? null : SharedEdges[0];
        }

        public void UpdateCircumcircle() {
            Vector2 p0 = Vertices[0];
            Vector2 p1 = Vertices[1];
            Vector2 p2 = Vertices[2];
            
            float dA = p0.x * p0.x + p0.y * p0.y;
            float dB = p1.x * p1.x + p1.y * p1.y;
            float dC = p2.x * p2.x + p2.y * p2.y;

            float aux1 = dA * (p2.y - p1.y) + dB * (p0.y - p2.y) + dC * (p1.y - p0.y);
            float aux2 = -(dA * (p2.x - p1.x) + dB * (p0.x - p2.x) + dC * (p1.x - p0.x));
            float div = 2 * (p0.x * (p2.y - p1.y) + p1.x * (p0.y - p2.y) + p2.x * (p1.y - p0.y));

            if (div == 0) {
                Debug.LogWarning("Division by 0 caught. UpdateCircumcircle() did not complete.");
                return;
            }

            Vector2 Center = new(aux1 / div, aux2 / div);
            
            Circumcenter = Center;
            RadiusSquared = (Center.x - p0.x) * (Center.x - p0.x) + (Center.y - p0.y) * (Center.y - p0.y);
        }
        
        private bool IsCounterClockwise() {
            return (Vertices[1].x - Vertices[0].x) * (Vertices[2].y - Vertices[0].y) - (Vertices[2].x - Vertices[0].x) * (Vertices[1].y - Vertices[0].y) > 0;
        }
        
        public bool IsCounterClockwise(Vector2 a, Vector2 b, Vector2 c) {
            return (b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y) > 0;
        }
        
        public bool IsPointInsideCircumcircle(Vector2 point) {
            return (point.x - Circumcenter.x) * (point.x - Circumcenter.x) + (point.y - Circumcenter.y) * (point.y - Circumcenter.y) < RadiusSquared;
        }
    }
}