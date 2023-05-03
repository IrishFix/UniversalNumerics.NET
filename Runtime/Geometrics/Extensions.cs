using System.Collections.Generic;
using System.Linq;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public static class Extensions {
        #region Vector3ToVector2
        public static Vector2 ToVector2XZ(this Vector3 Vector) {
            return new Vector2(Vector.X, Vector.Z);
        }
        
        public static Vector2 ToVector2XY(this Vector3 Vector) {
            return new Vector2(Vector.X, Vector.Y);
        }
        
        public static List<Vector2> ToVector2XZ(this IEnumerable<Vector3> VectorList) {
            return VectorList.Select(Vector => Vector.ToVector2XZ()).ToList();
        }
        
        public static List<Vector2> ToVector2XY(this IEnumerable<Vector3> VectorList) {
            return VectorList.Select(Vector => Vector.ToVector2XY()).ToList();
        }
        #endregion

        #region Vector2ToVector3
        public static Vector3 ToVector3XZ(this Vector2 Vector, float YFill = 0f) {
            return new Vector3(Vector.X, YFill, Vector.Y);
        }
        
        public static Vector3 ToVector3XY(this Vector2 Vector, float ZFill = 0f) {
            return new Vector3(Vector.X, Vector.Y, ZFill);
        }
        

        public static List<Vector3> ToVector3XZ(this IEnumerable<Vector2> VectorList, float YFill = 0f) {
            return VectorList.Select(Vector => Vector.ToVector3XZ(YFill)).ToList();
        }

        public static List<Vector3> ToVector3XY(this IEnumerable<Vector2> VectorList, float ZFill = 0f) {
            return VectorList.Select(Vector => Vector.ToVector3XY(ZFill)).ToList();
        }
        #endregion

        #region Triangle2DToTriangle3D
        public static Triangle3D ToTriangle3DXZ(this Triangle2D Triangle, float YFill = 0f) {
            return new Triangle3D(Triangle, true, YFill);
        }
        
        public static Triangle3D ToTriangle3DXY(this Triangle2D Triangle, float ZFill = 0f) {
            return new Triangle3D(Triangle, false, ZFill);
        }
        
        public static List<Triangle3D> ToTriangle3DXZ(this IEnumerable<Triangle2D> TriangleList, float YFill = 0f) {
            return TriangleList.Select(Triangle => Triangle.ToTriangle3DXZ(YFill)).ToList();
        }
        
        public static List<Triangle3D> ToTriangle3DXY(this IEnumerable<Triangle2D> TriangleList, float ZFill = 0f) {
            return TriangleList.Select(Triangle => Triangle.ToTriangle3DXY(ZFill)).ToList();
        }
        #endregion
        
        #region Triangle3DToTriangle2D
        public static Triangle2D ToTriangle2DXZ(this Triangle3D Triangle) {
            return new Triangle2D(Triangle);
        }
        
        public static Triangle2D ToTriangle2DXY(this Triangle3D Triangle) {
            return new Triangle2D(Triangle);
        }

        public static List<Triangle2D> ToTriangle2DXZ(this IEnumerable<Triangle3D> TriangleList) {
            return TriangleList.Select(Triangle => Triangle.ToTriangle2DXZ()).ToList();
        }
        
        public static List<Triangle2D> ToTriangle2DXY(this IEnumerable<Triangle3D> TriangleList) {
            return TriangleList.Select(Triangle => Triangle.ToTriangle2DXY()).ToList();
        }
        #endregion
        
        #region Edge2DToEdge3D
        public static Edge3D ToEdge3DXZ(this Edge2D Edge, float YFill = 0f) {
            return new Edge3D(Edge, true, YFill);
        }
        
        public static Edge3D ToEdge3DXY(this Edge2D Edge, float ZFill = 0f) {
            return new Edge3D(Edge, false, ZFill);
        }
        
        public static List<Edge3D> ToEdge3DXZ(this IEnumerable<Edge2D> EdgeList, float YFill = 0f) {
            return EdgeList.Select(Edge => Edge.ToEdge3DXZ(YFill)).ToList();
        }
        
        public static List<Edge3D> ToEdge3DXY(this IEnumerable<Edge2D> EdgeList, float ZFill = 0f) {
            return EdgeList.Select(Edge => Edge.ToEdge3DXY(ZFill)).ToList();
        }
        #endregion
        
        #region Edge3DToEdge2D
        public static Edge2D ToEdge2DXZ(this Edge3D Edge, float YFill = 0f) {
            return new Edge2D(Edge);
        }
        
        public static Edge2D ToEdge2DXY(this Edge3D Edge, float ZFill = 0f) {
            return new Edge2D(Edge, false);
        }
        
        public static List<Edge2D> ToEdge2DXZ(this IEnumerable<Edge3D> EdgeList, float YFill = 0f) {
            return EdgeList.Select(Edge => Edge.ToEdge2DXZ(YFill)).ToList();
        }
        
        public static List<Edge2D> ToEdge2DXY(this IEnumerable<Edge3D> EdgeList, float ZFill = 0f) {
            return EdgeList.Select(Edge => Edge.ToEdge2DXY(ZFill)).ToList();
        }
        #endregion
    }
}