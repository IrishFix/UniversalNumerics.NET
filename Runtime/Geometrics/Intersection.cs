using System.Diagnostics.Contracts;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public static class Intersection {

        private static float Sign (Vector2 p1, Vector2 p2, Vector2 p3) {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
        
        [Pure] public static bool PointWithinTriangle(Vector2 Point, Triangle2D Triangle) {
            float d1 = Sign(Point, Triangle.A, Triangle.B);
            float d2 = Sign(Point, Triangle.B, Triangle.C);
            float d3 = Sign(Point, Triangle.C, Triangle.A);

            bool has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }

        [Pure] public static Vector2 GetLineIntersection(Vector2 Line1P1, Vector2 Line1P2, Vector2 Line2P1, Vector2 Line2P2) {
            float X1 = Line1P1.X, X2 = Line1P2.X, X3 = Line2P1.X, X4 = Line2P2.X;
            float Y1 = Line1P1.Y, Y2 = Line1P2.Y, Y3 = Line2P1.Y, Y4 = Line2P2.Y;

            float det = ((X1 - X2) * (Y3 - Y4) - (Y1 - Y2) * (X3 - X4));
            Vector2 P = new Vector2(
                ((X1 * Y2 - Y1 * X2)*(X3 - X4)-(X1 - X2)*(X3*Y4 - Y3*X4))/det,
                ((X1 * Y2 - Y1 * X2)*(Y3 - Y4)-(Y1 - Y2)*(X3*Y4 - Y3*X4))/det
            );

            return P;
        }

        private static float Slope(Vector2 A, Vector2 B) {
            return (B.Y - A.Y) / (B.X - A.X);
        }
        
        private static double RadiansToDegrees(double radians) {
            return 180 / System.Math.PI * radians;
        }
        
        private static float Angle(float Slope1, float Slope2) {
            return (float)RadiansToDegrees(System.Math.Atan((Slope2 - Slope1) / (1 + (Slope2 * Slope1))));
        }

        private static bool CCW(Vector2 A, Vector2 B, Vector2 C) {
            return (C.Y - A.Y) * (B.X - A.X) > (B.Y - A.Y) * (C.X - A.X);
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