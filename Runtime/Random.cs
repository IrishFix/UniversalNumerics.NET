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

using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ComputationalGeometry {
    public static class Random {
        public static IEnumerable<Vector3> SphericalPointCloud(Vector3 Center, int Count, float Radius, float Deviation) {
            List<Vector3> PointCloud = new List<Vector3>();
            float RadiusSquared = Radius * Radius;
            for (int i = 0; i < Count; i++) {
                Vector3 RandomPoint = Center + new Vector3(UnityEngine.Random.Range(-Radius, Radius),
                    UnityEngine.Random.Range(-Radius, Radius),
                    UnityEngine.Random.Range(-Radius, Radius));

                if ((Center - RandomPoint).sqrMagnitude <= RadiusSquared) {
                    Vector3 DeviatedVector = new Vector3(UnityEngine.Random.Range(-Deviation, Deviation),
                        UnityEngine.Random.Range(-Deviation, Deviation),
                        UnityEngine.Random.Range(-Deviation, Deviation));
                    PointCloud.Add(RandomPoint+DeviatedVector);
                }
                else {
                    i -= 1;
                }
            }
            return PointCloud;
        }

        public static IEnumerable<Vector3> CubicalPointCloud(Vector3 Center, int Count, float Radius, float Deviation) {
            List<Vector3> PointCloud = new List<Vector3>();
            for (int i = 0; i < Count; i++) {
                Vector3 RandomPoint = Center + new Vector3(UnityEngine.Random.Range(-Radius, Radius),
                    UnityEngine.Random.Range(-Radius, Radius),
                    UnityEngine.Random.Range(-Radius, Radius));
                Vector3 DeviatedVector = new Vector3(UnityEngine.Random.Range(-Deviation, Deviation),
                        UnityEngine.Random.Range(-Deviation, Deviation),
                        UnityEngine.Random.Range(-Deviation, Deviation));
                PointCloud.Add(RandomPoint+DeviatedVector);
            }
            return PointCloud;
        }
        
        public static IEnumerable<Vector2> CircularPointCloud(Vector2 Center, int Count, float Radius, float Deviation) {
            List<Vector2> PointCloud = new List<Vector2>();
            float RadiusSquared = Radius * Radius;
            for (int i = 0; i < Count; i++) {
                Vector2 RandomPoint = Center + new Vector2(UnityEngine.Random.Range(-Radius, Radius),
                    UnityEngine.Random.Range(-Radius, Radius));

                if ((Center - RandomPoint).sqrMagnitude <= RadiusSquared) {
                    Vector2 DeviatedVector = new Vector2(UnityEngine.Random.Range(-Deviation, Deviation),
                        UnityEngine.Random.Range(-Deviation, Deviation));
                    PointCloud.Add(RandomPoint+DeviatedVector);
                }
                else {
                    i -= 1;
                }
            }
            return PointCloud;
        }

        public static IEnumerable<Vector2> SquarePointCloud(Vector2 Center, int Count, float Radius, float Deviation) {
            List<Vector2> PointCloud = new List<Vector2>();
            for (int i = 0; i < Count; i++) {
                Vector2 RandomPoint = Center + new Vector2(UnityEngine.Random.Range(-Radius, Radius),
                    UnityEngine.Random.Range(-Radius, Radius));
                Vector2 DeviatedVector = new Vector2(UnityEngine.Random.Range(-Deviation, Deviation),
                    UnityEngine.Random.Range(-Deviation, Deviation));
                PointCloud.Add(RandomPoint+DeviatedVector);
            }
            return PointCloud;
        }
    }
}