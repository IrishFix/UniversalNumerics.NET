using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ComputationalGeometry {
    public class Random {
        public IEnumerable<Vector3> SphericalPointCloud(Vector3 Center, int Count, float Radius, float Deviation) {
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

        public IEnumerable<Vector3> CubicalPointCloud(Vector3 Center, int Count, float Radius, float Deviation) {
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
        
        public IEnumerable<Vector2> CircularPointCloud(Vector2 Center, int Count, float Radius, float Deviation) {
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

        public IEnumerable<Vector2> SquarePointCloud(Vector2 Center, int Count, float Radius, float Deviation) {
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