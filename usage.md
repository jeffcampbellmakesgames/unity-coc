# Usage

## Getting Started
Usig JCMG COC to define your Unity Project's folder layout involves a few steps:
* Creating a `COCNodeGraph` asset that you can use to define you project's layout.

![COC Menu Item](/Images/CreateACOCNodeGraph.png)

* Once done, toggling the `Include in Automatic Generation` checkbox on the `COCNodeGraph` inspector to true.

![COC Menu Item](/Images/COCNodeGraphInspector.png)

* Execute the menu item `Tools\JCMG\COC\Evaluate All COC Node Graphs`. This will find all instances of `COCNodeGraph` assets in the project and evaluate them all, generating any desired folders and creating a static `COCFilePathsTools` helper class for retreiving folder paths. This same logic will also run when the Unity Editor loads for the first time.

![COC Menu Item](/Images/COCMenuItem.png)

## Generating code for retrieving folder paths
In order to generate code for retrieving specific folder paths, check the box on the desired Folder or FolderList node for `Should Generate Code` to true and execute the menu item again. This will generate methods for retrieving that folder path as an absolute path (full path) and as a relative path from the Assets folder. The latter can be useful as importing assets using Unity's `AssetDatabase.ImportAsset` expects that kind of relative path.

```
// This file is auto-generated and any manual changes will be lost.
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

namespace COCHelper
{
	/// <summary>
	///     Helper methods for retrieving folder paths for explicit folders marked in COCNodeGraphs.
	/// </summary>
	public static class COCFilePathTools
	{
		/// <summary>
		///     Returns a full path to the Unity project folder.
		/// </summary>
		/// <returns></returns>
		public static string GetAbsolutePathToProject()
		{
			var parentFolder = new DirectoryInfo(Application.dataPath).Parent;

			Assert.IsNotNull(parentFolder);

			return parentFolder?.FullName;
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Game One\Prefabs"
		/// </summary>
		public static string GetAbsolutePathToGameOnePrefabs()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToGameOnePrefabs());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Game One\Prefabs"
		/// </summary>
		public static string GetRelativePathToGameOnePrefabs()
		{
			return @"Assets\ExampleContent\Games\Game One\Prefabs";
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Game One\Scripts"
		/// </summary>
		public static string GetAbsolutePathToGameOneScripts()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToGameOneScripts());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Game One\Scripts"
		/// </summary>
		public static string GetRelativePathToGameOneScripts()
		{
			return @"Assets\ExampleContent\Games\Game One\Scripts";
		}
	}
}
```