//  TensorMath.NET, a package designed to ease the use of mathematical functions.
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
using System.Linq;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace TensorMath.Spatial {
    public static class PointComputations {
        public static IEnumerable<Vector2> SimplifyPoints(IEnumerable<Vector2> PointCloud, float MinimumP2PDistance) {
            List<Vector2> SimplifiedPointCloud = new List<Vector2>();
            List<Vector2> PointsToCull = new List<Vector2>();
            float SqrP2P = MinimumP2PDistance * MinimumP2PDistance;
            IEnumerable<Vector2> IEnumerable = PointCloud.ToList();
            foreach (Vector2 Point in IEnumerable) {
                if (PointsToCull.Contains(Point)) continue;
                SimplifiedPointCloud.Add(Point);
                foreach (Vector2 OtherPoint in IEnumerable) {
                    if (Point == OtherPoint) continue;
                    if (Vector2.DistanceSquared(Point, OtherPoint) < SqrP2P) PointsToCull.Add(OtherPoint);
                }
            }
            return SimplifiedPointCloud;
        }
        
        private static double Cross(Vector2 O, Vector2 A, Vector2 B) {
            return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
        }

        public static IEnumerable<Vector2> ComputeConvexHull(IEnumerable<Vector2> PointCloud) {
            List<Vector2> Points = PointCloud.ToList();
            
            if (Points.Count <= 1) return new List<Vector2>();
            int n = Points.Count(), k = 0;
            
            List<Vector2> H = new(new Vector2[2 * n]);

            Points.Sort((a, b) => System.Math.Abs(a.X - b.X) < 0.001f ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));
            
            for (int i = 0; i < n; ++i) {
                while (k >= 2 && Cross(H[k - 2], H[k - 1], Points[i]) <= 0)
                    k--;
                H[k++] = Points[i];
            }
            
            for (int i = n - 2, t = k + 1; i >= 0; i--) {
                while (k >= t && Cross(H[k - 2], H[k - 1], Points[i]) <= 0)
                    k--;
                H[k++] = Points[i];
            }

            return H.Take(k - 1).ToList();
        }
        
    }
}