using System;
using System.Collections.Generic;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public static class Voxelization {
        public static int EstimateVoxelCount(Vector3 areaSize, float voxelSize) {
            int cellsX = (int)System.Math.Ceiling(areaSize.X / voxelSize);
            int cellsY = (int)System.Math.Ceiling(areaSize.Y / voxelSize);
            int cellsZ = (int)System.Math.Ceiling(areaSize.Z / voxelSize);
            return cellsX * cellsY * cellsZ;
        }
        
        public static Vector3 CalculateVoxelGridSize(Vector3 areaSize, float voxelSize) {
            Vector3 voxelGridSize = Vector3.Zero;

            voxelGridSize.X = (int)Math.Ceiling(areaSize.X / voxelSize);
            voxelGridSize.Y = (int)Math.Ceiling(areaSize.Y / voxelSize);
            voxelGridSize.Z = (int)Math.Ceiling(areaSize.Z / voxelSize);

            return voxelGridSize;
        }

        public static Voxel[,,] VoxelizeArea(Vector3 areaCenter, Vector3 areaSize, float voxelSize) {
            Vector3 voxelGridSize = CalculateVoxelGridSize(areaSize, voxelSize);
            
            Voxel[,,] voxelGrid = new Voxel[(int)voxelGridSize.X, (int)voxelGridSize.Y, (int)voxelGridSize.Z];

            for (int x = 0; x < (int)voxelGridSize.X; x++) {
                for (int y = 0; y < (int)voxelGridSize.Y; y++) {
                    for (int z = 0; z < (int)voxelGridSize.Z; z++) {
                        Vector3 voxelPosition = areaCenter + new Vector3(x * voxelSize, y * voxelSize, z * voxelSize);
                        voxelGrid[x, y, z] = new Voxel(voxelPosition, voxelSize);
                    }
                }
            }

            return voxelGrid;
        }
        
    }
}