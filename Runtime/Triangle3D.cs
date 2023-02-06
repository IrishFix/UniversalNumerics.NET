//  UComputeNet, a package designed to ease the use of mathematical functions.
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
    public class Triangle3D {
        [SerializeField] public Edge3D[] Edges;
        [SerializeField] public Vector3[] Vertices;
        
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