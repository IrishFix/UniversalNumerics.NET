# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.4.0] - 2023-02-06

### Added

- A method in voxelization called "GetSubvoxelEstimate" allowing people to get an estimate (will always be higher than the voxel count to ensure it accounts for the fact that the voxelization may create extra voxels to ensure no details are lost) of the voxels that will be created when running a voxelization call. This allows people to prepare for how many voxels may come out of any number of voxelizations, without having to call them before hand.
- Voxelization.cs, allowing this package to assist you in creating voxelization of areas! Simply pass in (The center of the area to voxelize, the size of the area to voxelize, the size of the voxels to create) and it will automatically calculate the best way to voxelize that area. (It by default uses a conservative method that ensures it will only ever send you back the same area worth of voxels, or more, never less.)
- Bowyer-Watson triangulation method that takes a PSG (List of Edges), and therefore automatically culls overlapping points before feeding into the algorithm, allowing edges to be triangulated without fatal bugs and errors.

### Fixed

- Triangle2D failing circumcenter calculation when division can be 0, when it causes less problems than spamming warnings in the output does.

## [0.3.3] - 2023-02-01

### Fixed

- Triangle2D constructor taking two edges and a point instead of one edge and one point.
- Circumcircle calculations using float instead of double and causing rounding errors.

## [0.3.2] - 2023-02-01

### Added

- License notice to the top of all protected cs content(s)

### Changed

- Now using AGPL license instead of GPL

## [0.3.1] - 2023-02-01

### Fixed

- Methods from Random.cs not being able to be used due to them and their class not being labelled correctly as static.

## [0.3.0] - 2023-02-01

### Added

- Random.cs to handle cases of generating random point clouds and will contain more in the future.

## [0.2.2] - 2023-02-01

### Changed

- Removed problematic .meta files.

## [0.2.1] - 2023-01-31

### Changed

- CHANGELOG.md to include github diffs at page bottom.

## [0.2.0] - 2023-01-31

### Removed

- Conversion.cs, as its functionality is already offered better by Extensions.cs.

## [0.1.1] - 2023-01-31

### Added

- Description for package.json.

### Changed

- Name for package.json.
- Internal definitions to public for use outside of the packages cs assembly.
- 'ComputationalGeometry.Runtime' namespace to just 'ComputationalGeometry' as it simplifies the 'using' statement when referencing this package in unity projects.

## [0.1.0] - 2023-01-31

### Added

- Edge2D.cs, its constructor(s), and all related methods.
- Edge3D.cs, its constructor(s), and all related methods.
- Triangle2D.cs, its constructor(s), and all related methods.
- Triangle3D.cs, its constructor(s), and all related methods.
- Extensions.cs, to allow swift conversions between Vector types and all types above.
- Intersection.cs, for detecting intersections / overlaps between listed geometries.
- PointComputations.cs, to facilitate convex hull computation, point cloud simplification, and continuing on, any solely point-based methods.
- Conversion.cs, to allow conversion of Vector types.

[unreleased]: https://github.com/IrishFix/Computational-Geometry/compare/v0.4.0...HEAD
[0.4.0]: https://github.com/IrishFix/Computational-Geometry/compare/v0.3.3...v0.4.0
[0.3.3]: https://github.com/IrishFix/Computational-Geometry/compare/v0.3.2...v0.3.3
[0.3.2]: https://github.com/IrishFix/Computational-Geometry/compare/v0.3.1...v0.3.2
[0.3.1]: https://github.com/IrishFix/Computational-Geometry/compare/v0.3.0...v0.3.1
[0.3.0]: https://github.com/IrishFix/Computational-Geometry/compare/v0.2.2...v0.3.0
[0.2.2]: https://github.com/IrishFix/Computational-Geometry/compare/v0.2.1...v0.2.2
[0.2.1]: https://github.com/IrishFix/Computational-Geometry/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/IrishFix/Computational-Geometry/compare/v0.1.1...v0.2.0
[0.1.1]: https://github.com/IrishFix/Computational-Geometry/compare/v0.1.0...v0.1.1
[0.1.0]: https://github.com/IrishFix/Computational-Geometry/releases/tag/v0.1.0
