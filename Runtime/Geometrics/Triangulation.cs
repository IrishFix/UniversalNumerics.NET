using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public static class Triangulation {
        
        public static IEnumerable<Triangle2D> Triangulate(IEnumerable<Vector2> PointCloud) {
            List<Triangle2D> Triangulation = new List<Triangle2D>();

            Triangle2D SuperTriangle = new Triangle2D(
                new Vector2(-2000, -2000),
                new Vector2(0, 2000),
                new Vector2(2000, 0)
            );

            Triangulation.Add(SuperTriangle);
            
            foreach (Vector2 Point in PointCloud) {
                List<Triangle2D> BadTriangles = new List<Triangle2D>();
                foreach (Triangle2D Triangle in Triangulation) {
                    if (Triangle.IsPointInsideCircumcircle(Point)) {
                        BadTriangles.Add(Triangle);
                    }
                }

                List<Edge2D> Polygon = new List<Edge2D>();

                foreach (Triangle2D Triangle in BadTriangles) {
                    foreach (Edge2D Edge in Triangle.Edges) {
                        bool Shared = false;
                        foreach (Triangle2D BadTriangle in BadTriangles) {
                            if (Triangle == BadTriangle) continue;
                            foreach (Edge2D BadTriangleEdge in BadTriangle.Edges) {
                                if (BadTriangleEdge.Equals(Edge)) {
                                    Shared = true;
                                    break;
                                }
                            }
                            if (Shared) break;
                        }

                        if (!Shared) {
                            Polygon.Add(Edge);
                        }
                    }
                }

                foreach (Triangle2D Triangle in BadTriangles) {
                    Triangulation.Remove(Triangle);
                }

                foreach (Edge2D Edge in Polygon) {
                    Triangle2D NewTriangle = new Triangle2D(Edge.A, Edge.B, Point);
                    Triangulation.Add(NewTriangle);
                }
            }

            List<Triangle2D> ToRemove = new List<Triangle2D>();
            foreach (Triangle2D Triangle in Triangulation) {
                if (SuperTriangle.Vertices.Any(o => Triangle.Vertices.Contains(o))) {
                    ToRemove.Add(Triangle);
                }
            }

            foreach (Triangle2D Triangle in ToRemove) {
                Triangulation.Remove(Triangle);
            }

            return Triangulation;
        }

        private static bool PointInList(Vector2 Point, List<Vector2> Points, float Tolerance=0.001f) {
            float SqrTol = Tolerance * Tolerance;
            return Points.Any(OtherPoint => Vector2.DistanceSquared(OtherPoint, Point) < SqrTol);
        }

        [Pure] public static IEnumerable<Triangle2D> Triangulate(IEnumerable<Edge2D> PSG) {
            List<Vector2> PointCloud = new List<Vector2>();
            foreach (Edge2D Edge in PSG) {
                if (!PointInList(Edge.A, PointCloud)) {
                    PointCloud.Add(Edge.A);
                }
                if (!PointInList(Edge.B, PointCloud)) {
                    PointCloud.Add(Edge.B);
                }
            }
            return Triangulate(PointCloud);
        }

        [Pure] public static IEnumerable<Edge2D> VoronoiFromTriangulation(IEnumerable<Triangle2D> Triangulation) {
            IList<Triangle2D> Tris = Triangulation.ToList();
            List<Edge2D> Edges = new List<Edge2D>();
            foreach (Triangle2D Triangle in Tris) {
                foreach (Triangle2D OtherTriangle in Tris) {
                    if (Triangle == OtherTriangle) continue;
                    if (Triangle.SharesEdgeWith(OtherTriangle)) {
                        Edge2D MadeEdge = new Edge2D(Triangle.Circumcenter, OtherTriangle.Circumcenter);
                        if (Edges.Any(Edge => Edge.Equals(MadeEdge))) continue;
                        Edges.Add(MadeEdge);
                    }
                }
            }
            return Edges;
        }

        [Pure] private static bool IsPoorTriangle(Triangle2D Triangle, float Threshold) {
            float Angle1 = Intersection.GetAngleBetween(Triangle.A, Triangle.B, Triangle.A, Triangle.C);
            float Angle2 = Intersection.GetAngleBetween(Triangle.B, Triangle.A, Triangle.B, Triangle.C);
            float Angle3 = Intersection.GetAngleBetween(Triangle.C, Triangle.A, Triangle.C, Triangle.B);
            return (Angle1 < Threshold || Angle2 < Threshold || Angle3 < Threshold);
        }

        [Pure] private static Edge2D GetIntersectingEdge(List<Triangle2D> Triangulation) {
            foreach (Triangle2D Tri in Triangulation) {
                foreach (Triangle2D OtherTri in Triangulation) {
                    if (Tri.Equals(OtherTri)) continue;
                    foreach (Edge2D Edge in Tri.Edges) {
                        foreach (Edge2D OtherEdge in OtherTri.Edges) {
                            if (Intersection.AreSegmentsIntersecting(Edge.A, Edge.B, OtherEdge.A, OtherEdge.B)) {
                                return Edge;
                            }
                        }
                    }
                }
            }
            return null;
        }
        
        [Pure] private static bool HasIntersectingEdges(List<Edge2D> Edges) {
            foreach (Edge2D Edge in Edges) {
                foreach (Edge2D OtherEdge in Edges) {
                    if (Edge.Equals(OtherEdge)) continue;
                    if (Intersection.AreSegmentsIntersecting(Edge.A, Edge.B, OtherEdge.A, OtherEdge.B)) {
                        return true;
                    }
                }
            }
            return false;
        }

        [Pure] private static Triangle2D[] GetEdgeSharedTriangles(IEnumerable<Triangle2D> Triangles, Edge2D Edge) {
            IEnumerable<Triangle2D> OtherTriangles = Triangles.ToList();
            
            foreach (Triangle2D Triangle in OtherTriangles) {
                foreach (Triangle2D OtherTriangle in OtherTriangles) {
                    if (Triangle.Equals(OtherTriangle)) continue;
                    if (Triangle.SharedEdgeWith(OtherTriangle).Equals(Edge)) {
                        return new[] { Triangle, OtherTriangle };
                    }
                }
            }

            return null;
        }

        [Pure] private static List<Triangle2D> ToTriangles(List<Edge2D> Edges) {
            List<Triangle2D> outputTriangles = new List<Triangle2D>();
            for (int i = 0; i < Edges.Count; i += 3) {
                outputTriangles.Add(new Triangle2D(Edges[i].A, Edges[i+1].A, Edges[i+2].A));
            }
            return outputTriangles;
        }

        [Pure] public static IEnumerable<Triangle2D> ConstrainedTriangulation(IEnumerable<Vector2> PointCloud, IEnumerable<Edge2D> ConstraintEdges, float Threshold = 26) {
            List<Triangle2D> Triangulation = Triangulate(PointCloud).ToList();
            foreach (Edge2D ConstraintEdge in ConstraintEdges) {
                Triangulation.Add(new Triangle2D(ConstraintEdge));
            }
            
            Edge2D NextIntersectingEdge = GetIntersectingEdge(Triangulation);

            while (NextIntersectingEdge != null) {
                foreach (Triangle2D Triangle in GetEdgeSharedTriangles(Triangulation, NextIntersectingEdge)) {
                    Vector2 v1 = Triangle.A;
                    Vector2 v2 = Triangle.B;
                    Vector2 v3 = Triangle.C;
                    
                    Triangulation.Remove(Triangle);
                    
                    Triangle2D t1 = new(NextIntersectingEdge.A, v1, v2);
                    Triangle2D t2 = new(NextIntersectingEdge.B, v1, v2);
                    Triangle2D t3 = new(NextIntersectingEdge.A, v2, v3);
                    Triangle2D t4 = new(NextIntersectingEdge.B, v2, v3);
                    Triangle2D t5 = new(NextIntersectingEdge.A, v3, v1);
                    Triangle2D t6 = new(NextIntersectingEdge.B, v3, v1);
                    
                    t1.Vertices[2] = t2.Vertices[2] = v2;
                    t3.Vertices[2] = t4.Vertices[2] = v3;
                    t5.Vertices[2] = t6.Vertices[2] = v1;
                    
                    Triangulation.Add(t1);
                    Triangulation.Add(t2);
                    Triangulation.Add(t3);
                    Triangulation.Add(t4);
                    Triangulation.Add(t5);
                    Triangulation.Add(t6);
                }

                NextIntersectingEdge = GetIntersectingEdge(Triangulation);
            }

            return Triangulation;
        }

    }
}