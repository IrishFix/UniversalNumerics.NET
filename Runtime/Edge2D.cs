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
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UComputeNet.Geometry {
    [Serializable]
    public class Edge2D {
        [SerializeField] public Vector2[] Vertices;
        
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
            return (A - B).magnitude;
        }

        public Vector2 GetMidpoint() {
            return new Vector2((A.x + B.x) / 2, (A.y + B.y) / 2);
        }

        public bool EncroachedByPoint(Vector2 Point) {
            Vector2 Midpoint = GetMidpoint();
            float Radius = GetLength()/2f;
            float RadiusSquared = Radius * Radius;
            return (Point.x - Midpoint.x) * (Point.x - Midpoint.x) + (Point.y - Midpoint.y) * (Point.y - Midpoint.y) < RadiusSquared;
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
            int hCode = (int)A.x ^ (int)A.y ^ (int)B.x ^ (int)B.y;
            return hCode.GetHashCode();
        }
    }
}