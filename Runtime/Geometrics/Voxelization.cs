using System.Collections.Generic;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Geometrics {
    public static class Voxelization {

        public static int GetSubvoxelEstimate(Vector3 AreaSize, Vector3 VoxelSize) {
            int CellsX = (int)System.Math.Ceiling(AreaSize.X / VoxelSize.X), CellsY = (int)System.Math.Ceiling(AreaSize.Y / VoxelSize.Y), CellsZ = (int)System.Math.Ceiling(AreaSize.Z / VoxelSize.Z);
            return CellsX * CellsZ * CellsY;
        }

        public static IEnumerable<Vector3> VoxelizeArea(Vector3 Center, Vector3 AreaSize, Vector3 VoxelSize) {
            Vector3 StartPosition = Center + AreaSize/2 - VoxelSize/2;
            int CellsX = (int)System.Math.Ceiling(AreaSize.X / VoxelSize.X), CellsY = (int)System.Math.Ceiling(AreaSize.Y / VoxelSize.Y), CellsZ = (int)System.Math.Ceiling(AreaSize.Z / VoxelSize.Z);
            List<Vector3> Voxels = new(CellsX*CellsY*CellsZ);
            for (int i = 0; i < CellsX; i++) {
                for (int j = 0; j < CellsZ; j++) {
                    for (int k = 0; k < CellsY; k++) {
                        Vector3 VoxelPosition = StartPosition - new Vector3(VoxelSize.X * i, VoxelSize.Y * k, VoxelSize.Z * j);
                        Voxels.Add(VoxelPosition);
                    }
                }
            }
            return Voxels;
        }
        
    }
}