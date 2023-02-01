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

using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ComputationalGeometry {
    public static class Intersection {

        [Pure] public static bool PointWithinTriangle(Vector2 Point, Triangle2D Triangle) {
            double Area = 0.5 *(-Triangle.B.y*Triangle.C.x + Triangle.A.y*(-Triangle.B.x + Triangle.C.x) + Triangle.A.x*(Triangle.B.y - Triangle.C.y) + Triangle.B.x*Triangle.C.y);
            double s = 1/(2*Area)*(Triangle.A.y*Triangle.C.y - Triangle.A.x*Triangle.C.y + (Triangle.C.y - Triangle.A.y)*Point.x + (Triangle.A.x - Triangle.C.y)*Point.y);
            double t = 1/(2*Area)*(Triangle.A.x*Triangle.B.y - Triangle.A.y*Triangle.B.x + (Triangle.A.y - Triangle.B.y)*Point.x + (Triangle.B.x - Triangle.A.x)*Point.y);
            return s > 0 && t > 0 && 1 - s - t > 0;
        }

        [Pure] public static Vector2 GetLineIntersection(Vector2 Line1P1, Vector2 Line1P2, Vector2 Line2P1, Vector2 Line2P2) {
            float X1 = Line1P1.x, X2 = Line1P2.x, X3 = Line2P1.x, X4 = Line2P2.x;
            float Y1 = Line1P1.y, Y2 = Line1P2.y, Y3 = Line2P1.y, Y4 = Line2P2.y;

            float det = ((X1 - X2) * (Y3 - Y4) - (Y1 - Y2) * (X3 - X4));
            Vector2 P = new Vector2(
                ((X1 * Y2 - Y1 * X2)*(X3 - X4)-(X1 - X2)*(X3*Y4 - Y3*X4))/det,
                ((X1 * Y2 - Y1 * X2)*(Y3 - Y4)-(Y1 - Y2)*(X3*Y4 - Y3*X4))/det
            );

            return P;
        }

        private static float Slope(Vector2 A, Vector2 B) {
            return (B.y - A.y) / (B.x - A.x);
        }
        
        private static float Angle(float Slope1, float Slope2) {
            return math.degrees(math.atan((Slope2 - Slope1) / (1 + (Slope2 * Slope1))));
        }

        private static bool CCW(Vector2 A, Vector2 B, Vector2 C) {
            return (C.y - A.y) * (B.x - A.x) > (B.y - A.y) * (C.x - A.x);
        }

        [Pure] public static float GetAngleBetween(Vector2 Line1P1, Vector2 Line1P2, Vector2 Line2P1, Vector2 Line2P2) {
            float Slope1 = Slope(Line1P1, Line1P2);
            float Slope2 = Slope(Line2P1, Line2P2);
            return Angle(Slope1, Slope2);
        }

        [Pure] public static bool AreSegmentsIntersecting(Vector2 Line1P1, Vector2 Line1P2, Vector2 Line2P1, Vector2 Line2P2) {
            return CCW(Line1P1, Line2P1, Line2P2) != CCW(Line1P2, Line2P1, Line2P2) && CCW(Line1P1, Line1P2, Line2P1) != CCW(Line1P1, Line1P2, Line2P2);
        }
        
    }
}