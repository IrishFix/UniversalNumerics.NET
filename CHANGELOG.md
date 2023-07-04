# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

### Bug fixes will not be published as a github release as of 2023-05-04, however all changes will still be published to the nuget package.

## [Unreleased]

## 1.1.0 - 2023-07-05

### Added

- An untested version of constrained triangulation; along with all Triangle2D / 3D data that was required to be edited to ensure a working format.#

### Changed

- Voxelization now voxelizes spaces using grids instead of lists to ensure connectivity stays consistent.

### Fixed

- Networking/Genetics has now been re-added in an un-tested state. It (should) allow neural networks to be correctly trained using genetic algorithms.
- Predicted / Expected values were swapped in a MSE calculation during a fit stage in the  neural network training that could case some backwards learning gradients. I was unable to find a case in which it did infact cause said problem, but it is better to ensure it works as expected by using the right method.

## 1.0.2 - 2023-05-06

### Changed

- README.md is now way more useful.

## 1.0.1 - 2023-05-04

### Changed

- Formatting for epoch console outputs to not show scientific notations.

## [1.0.0] - 2023-05-04

### Added

- Regularization to the Networking (Dropout, L1 and L2)
- Newtonsoft.Json dll to allow Architecture gathering

## [0.7.0] - 2023-05-03

### Added

- Added full custom neural networking / artificial intelligence support (BETA)
- Networking.MLP is the base model and can be used along with activation functions, initializations, optimizations and Dense layers in order to create an advanced deep learning network. (SUPPORTS BATCHED INPUTS.)

### Changed

- No longer relies on Unity to run, and can be used in general C# projects as long as system is included.
- Now uses System.Numerics Vector2 and Vector3, along with System.Math and System.Random instead of Unity Random and Unity Vectors.

## [0.6.0] - 2023-02-10

### Changed

- Now to be known as "TensorMath.NET" / "tensormathdotnet"

## [0.5.0] - 2023-02-06

### Changed

- Now to be known as "UComputeNet" / "UCompute.net" to allow expansion to further topics and allowing the package to no longer be anchored to specifically geometry. Instead of simply importing ComputationalGeometry for geometry purposes, you now must either import UComputeNet and use ".Geometry", or import UComputeNet.Geometry itself.
- All NameSpaces, directives, and references to the name "ComputationalGeometry" have henceforth been removed, and the github will soon follow suite.

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

[unreleased]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.7.0...v1.0.0
[0.7.0]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.6.0...v0.7.0
[0.6.0]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.5.0...v0.6.0
[0.5.0]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.4.0...v0.5.0
[0.4.0]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.3.3...v0.4.0
[0.3.3]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.3.2...v0.3.3
[0.3.2]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.3.1...v0.3.2
[0.3.1]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.3.0...v0.3.1
[0.3.0]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.2.2...v0.3.0
[0.2.2]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.2.1...v0.2.2
[0.2.1]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.1.1...v0.2.0
[0.1.1]: https://github.com/IrishFix/UniversalNumerics.NET/compare/v0.1.0...v0.1.1
[0.1.0]: https://github.com/IrishFix/UniversalNumerics.NET/releases/tag/v0.1.0
