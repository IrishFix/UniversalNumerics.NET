# UniversalNumerics.NET

UniversalNumerics.NET is a versatile .NET package that can be used both in Unity and in normal C# projects. It simplifies common computational tasks, such as computing 2D line intersections, 2D segment intersections, 2D point within queries, 2D triangulation of points, and 3D area voxelization.

With UniversalNumerics.NET, you can easily compute line and segment intersections, determine if a point is within a shape, and perform triangulation of points with just a few lines of code. Additionally, you can voxelize 3D areas with ease.

UniversalNumerics.NET also includes beta functionality for building advanced deep neural networks. This feature enables you to train and use deep neural networks within your Unity or C# projects, unlocking powerful machine learning capabilities.

# Installation

### C# Projects
There are a few ways to add UniversalNumerics.NET to your C# project:

#### Nuget
You can install the package from NuGet using the following command in the Package Manager Console:
```powershell
Install-Package UniversalNumerics.NET
```

#### DLL
You can also add the package to your project by referencing the assembly directly. To do this, you can download the UniversalNumerics.NET DLL file from the NuGet package and add it to your project's references.

In Visual Studio, right-click on your project in the Solution Explorer, select "Add Reference", browse to the location of the UniversalNumerics.NET DLL file, and select "Add". Once the DLL is added, you can start using the functions and features provided by UniversalNumerics.NET in your code.

In Jetbrains Rider, right-click on your csproj in the Solution Explorer, select "Add", then "Reference", select "Add From..." and browse to the location of the UniversalNumerics.NET DLL file, and add it. Once the DLL is added, you can start using the functions and features provided by UniversalNumerics.NET in your code.

### Unity Projects
There are a few ways to add UniversalNumerics.NET to your Unity project:

#### Add From DLL (Recommended)
You can add the UniversalNumerics.NET package to your Unity project by importing the DLL file into the Unity Package Manager. To do this, open the Unity editor and navigate to the "Packages" window. From there, select the "+" icon in the top left corner and choose "Add package from disk". Navigate to the location of the UniversalNumerics.NET DLL file on your disk, select it, and click "Open". The package will now be added to your Unity project, and you can use its functions and features in your scripts.

#### Add From Disk
Another way to add the package to your Unity project is to import it from a disk. To do this, download the UniversalNumerics.NET package from NuGet, then open the Unity editor and navigate to the "Assets" menu. From there, select "Import Package" and choose "Custom Package" from the dropdown menu. Navigate to the location of the UniversalNumerics.NET package on your disk, select it, and click "Import". The package will now be added to your Unity project, and you can use its functions and features in your scripts.

# Usage
Refer to the documentation and examples included in the package for details on how to use each of the functions and features provided by UniversalNumerics.NET.

# Contributing
If you find a bug or have a feature request, please submit an issue on the UniversalNumerics.NET GitHub repository. Contributions to the package are also welcome and encouraged!

# Credits
UniversalNumerics.NET is developed and maintained by Benjamin Knight (A.K.A IrishFix).

# License
UniversalNumerics.NET is licensed under the (GNU Affero General Public License v3.0 only) as published by
the Free Software Foundation.
