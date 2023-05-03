using System.Collections.Generic;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public static class Random {
        
        private static double GetRandomNumber(double minimum, double maximum) { 
            System.Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public static IEnumerable<Vector3> SphericalPointCloud(Vector3 Center, int Count, float Radius, float Deviation) {
            System.Random RandomGenerator = new System.Random();
            List<Vector3> PointCloud = new List<Vector3>();
            float RadiusSquared = Radius * Radius;
            for (int i = 0; i < Count; i++) {
                Vector3 RandomPoint = Center + new Vector3((float)GetRandomNumber(-Radius, Radius),
                    (float)GetRandomNumber(-Radius, Radius),
                    (float)GetRandomNumber(-Radius, Radius));

                if (Vector3.DistanceSquared(Center, RandomPoint) <= RadiusSquared) {
                    Vector3 DeviatedVector = new Vector3((float)GetRandomNumber(-Deviation, Deviation),
                        (float)GetRandomNumber(-Deviation, Deviation),
                        (float)GetRandomNumber(-Deviation, Deviation));
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
                Vector3 RandomPoint = Center + new Vector3((float)GetRandomNumber(-Radius, Radius),
                    (float)GetRandomNumber(-Radius, Radius),
                    (float)GetRandomNumber(-Radius, Radius));
                Vector3 DeviatedVector = new Vector3((float)GetRandomNumber(-Deviation, Deviation),
                    (float)GetRandomNumber(-Deviation, Deviation),
                    (float)GetRandomNumber(-Deviation, Deviation));
                PointCloud.Add(RandomPoint+DeviatedVector);
            }
            return PointCloud;
        }
        
        public static IEnumerable<Vector2> CircularPointCloud(Vector2 Center, int Count, float Radius, float Deviation) {
            List<Vector2> PointCloud = new List<Vector2>();
            float RadiusSquared = Radius * Radius;
            for (int i = 0; i < Count; i++) {
                Vector2 RandomPoint = Center + new Vector2((float)GetRandomNumber(-Radius, Radius),
                    (float)GetRandomNumber(-Radius, Radius));

                if (Vector2.DistanceSquared(Center, RandomPoint) <= RadiusSquared) {
                    Vector2 DeviatedVector = new Vector2((float)GetRandomNumber(-Deviation, Deviation),
                        (float)GetRandomNumber(-Deviation, Deviation));
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
                Vector2 RandomPoint = Center + new Vector2((float)GetRandomNumber(-Radius, Radius),
                    (float)GetRandomNumber(-Radius, Radius));
                Vector2 DeviatedVector = new Vector2((float)GetRandomNumber(-Deviation, Deviation),
                    (float)GetRandomNumber(-Deviation, Deviation));
                PointCloud.Add(RandomPoint+DeviatedVector);
            }
            return PointCloud;
        }
    }
}