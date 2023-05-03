using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public static class Triangulation {
        
        public static IEnumerable<Triangle2D> BowyerWatsonTriangulation(IEnumerable<Vector2> PointCloud) {
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

        [Pure] private static IEnumerable<Vector2> CleanPoints(IEnumerable<Vector2> UncleanPoints) {
            List<Vector2> CleanedPoints = new List<Vector2>();
            foreach (Vector2 Point in UncleanPoints) {
                if (!PointInList(Point, CleanedPoints)) {
                    CleanedPoints.Add(Point);
                }
            }
            return CleanedPoints;
        }

        [Pure] public static IEnumerable<Triangle2D> BowyerWatsonTriangulation(IEnumerable<Edge2D> PSG) {
            List<Vector2> PointCloud = new List<Vector2>();
            foreach (Edge2D Edge in PSG) {
                if (!PointInList(Edge.A, PointCloud)) {
                    PointCloud.Add(Edge.A);
                }
                if (!PointInList(Edge.B, PointCloud)) {
                    PointCloud.Add(Edge.B);
                }
            }
            return BowyerWatsonTriangulation(PointCloud);
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

        [Pure] private static IEnumerable<Edge2D> GetEncroachedSegments(IEnumerable<Edge2D> Segments, IEnumerable<Vector2> PointCloud) {
            List<Edge2D> EncroachedEdges = new List<Edge2D>();
            foreach (Edge2D Segment in Segments) {
                bool Encroached = false;
                foreach (Vector2 Point in PointCloud) {
                    Encroached = Segment.EncroachedByPoint(Point);
                    if (Encroached) {
                        break;
                    }
                }
                if (Encroached) {
                    EncroachedEdges.Add(Segment);
                }
            }
            return EncroachedEdges;
        }

        [Pure] public static IEnumerable<Triangle2D> ConstrainedBowyerWatsonTriangulation(IEnumerable<Vector2> PointCloud, IEnumerable<Edge2D> ConstraintEdges, float Threshold = 26) {
            List<Vector2> PointCloudList = CleanPoints(PointCloud).ToList();
            List<Edge2D> ConstraintEdgeList = ConstraintEdges.ToList();
            List<Triangle2D> Triangulation = BowyerWatsonTriangulation(PointCloudList).ToList();
            List<Edge2D> EncroachedSegments = new List<Edge2D>();
            List<Triangle2D> PoorTriangles = new List<Triangle2D>();

            foreach (Edge2D ConstraintEdge in ConstraintEdgeList) {
                
                if (!PointInList(ConstraintEdge.A, PointCloudList)) {
                    PointCloudList.Add(ConstraintEdge.A);
                } 
                if (!PointInList(ConstraintEdge.B, PointCloudList)) {
                    PointCloudList.Add(ConstraintEdge.B);
                }

                foreach (Vector2 Point in PointCloudList) {
                    if (ConstraintEdge.EncroachedByPoint(Point)) {
                        EncroachedSegments.Add(ConstraintEdge);
                        break;
                    }
                }
            }
            
            foreach (Triangle2D Triangle in Triangulation) {
                if (IsPoorTriangle(Triangle, Threshold)) {
                    PoorTriangles.Add(Triangle);
                }
            }

            while (EncroachedSegments.Count > 0 || PoorTriangles.Count > 0) {
                List<Edge2D> EdgesToRemove = new List<Edge2D>();
                List<Triangle2D> TrianglesToRemove = new List<Triangle2D>();
                
                foreach (Edge2D EncroachedSegment in EncroachedSegments) {
                    Vector2 Midpoint = EncroachedSegment.GetMidpoint();
                    PointCloudList.Add(Midpoint);
                    EdgesToRemove.Add(EncroachedSegment);
                    ConstraintEdgeList.Remove(EncroachedSegment);
                    ConstraintEdgeList.Add(new Edge2D(Midpoint,  EncroachedSegment.A));
                    ConstraintEdgeList.Add(new Edge2D(Midpoint,  EncroachedSegment.B));
                }

                foreach (Edge2D EdgeToRemove in EdgesToRemove) {
                    EncroachedSegments.Remove(EdgeToRemove);
                }

                foreach (Triangle2D Triangle in PoorTriangles) {
                    Vector2 Midpoint = Triangle.Circumcenter;
                    List<Edge2D> SegmentsEncroachedByMidpoint = new List<Edge2D>();
                    foreach (Edge2D Segment in ConstraintEdgeList) {
                        if (Segment.EncroachedByPoint(Midpoint)) {
                            SegmentsEncroachedByMidpoint.Add(Segment);
                        }
                    }

                    if (SegmentsEncroachedByMidpoint.Count > 0) {
                        EncroachedSegments.AddRange(SegmentsEncroachedByMidpoint);
                    }
                    else {
                        PointCloudList.Add(Midpoint);
                    }
                    TrianglesToRemove.Add(Triangle);
                }
                
                foreach (Triangle2D TriangleToRemove in TrianglesToRemove) {
                    PoorTriangles.Remove(TriangleToRemove);
                }

                Triangulation = BowyerWatsonTriangulation(PointCloudList).ToList();
                
                foreach (Edge2D ConstraintEdge in ConstraintEdgeList) {
                    foreach (Vector2 Point in PointCloudList) {
                        if (ConstraintEdge.EncroachedByPoint(Point)) {
                            EncroachedSegments.Add(ConstraintEdge);
                            break;
                        }
                    }
                }
                
                foreach (Triangle2D Triangle in Triangulation) {
                    if (IsPoorTriangle(Triangle, Threshold)) {
                        PoorTriangles.Add(Triangle);
                    }
                }
            }
            
            return Triangulation;
        }

    }
}