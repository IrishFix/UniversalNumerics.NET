//  TensorMath.NET, a package designed to ease the use of mathematical functions.
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

using System.Collections.Generic;
using System.Numerics;

// ReSharper disable once CheckNamespace
namespace TensorMath.Spatial {
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