using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace ComputationalGeometry.Runtime {
    internal static class Triangulation {
        
        [Pure, NotNull] internal static IEnumerable<Triangle2D> BowyerWatsonTriangulation(IEnumerable<Vector2> PointCloud) {
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

        [Pure, NotNull] internal static IEnumerable<Edge2D> VoronoiFromTriangulation(IEnumerable<Triangle2D> Triangulation) {
            IList<Triangle2D> Tris = Triangulation.AsReadOnlyList();
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

        /*[Pure, NotNull] internal static IEnumerable<Triangle2D> ConstrainedBowyerWatsonTriangulation(IEnumerable<Vector2> PointCloud, IEnumerable<Edge2D> ConstraintEdges, float Threshold = 26) {
            IList<Vector2> Points = PointCloud.AsReadOnlyList();
            List<Triangle2D> Triangulation = BowyerWatsonTriangulation(Points).ToList();
            List<Edge2D> EncroachedSegments = new List<Edge2D>();
            List<Triangle2D> PoorTriangles = new List<Triangle2D>();

            List<Vector2> P = PointCloud.ToList();

            foreach (Edge2D Segment in ConstraintEdges) {
                bool Encroached = false;
                foreach (Vector2 Point in Points) {
                    if (Segment.EncroachedByPoint(Point)) {
                        Encroached = true;
                        break;
                    }
                }
                if (Encroached) EncroachedSegments.Add(Segment);
            }

            foreach (Triangle2D Triangle in Triangulation) {
                float Angle1 = Intersection.GetAngleBetween(Triangle.A, Triangle.B, Triangle.A, Triangle.C);
                float Angle2 = Intersection.GetAngleBetween(Triangle.B, Triangle.A, Triangle.B, Triangle.C);
                float Angle3 = Intersection.GetAngleBetween(Triangle.C, Triangle.A, Triangle.C, Triangle.B);

                if (Angle1 < Threshold || Angle2 < Threshold || Angle3 < Threshold) {
                    PoorTriangles.Add(Triangle);
                }
            }

            while (EncroachedSegments.Count > 0 || PoorTriangles.Count > 0) {
                if (EncroachedSegments.Count > 0) {
                    P.Add(EncroachedSegments[0].GetMidpoint());
                } else if (PoorTriangles.Count > 0) {
                    
                }
            }
        }*/

    }
}