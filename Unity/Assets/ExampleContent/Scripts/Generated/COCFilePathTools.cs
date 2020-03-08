// This file is auto-generated and any manual chnages will be lost.
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

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Game Two\Data"
		/// </summary>
		public static string GetAbsolutePathToGameTwoData()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToGameTwoData());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Game Two\Data"
		/// </summary>
		public static string GetRelativePathToGameTwoData()
		{
			return @"Assets\ExampleContent\Games\Game Two\Data";
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Game Two\Prefabs"
		/// </summary>
		public static string GetAbsolutePathToGameTwoPrefabs()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToGameTwoPrefabs());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Game Two\Prefabs"
		/// </summary>
		public static string GetRelativePathToGameTwoPrefabs()
		{
			return @"Assets\ExampleContent\Games\Game Two\Prefabs";
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Game Two\Scripts"
		/// </summary>
		public static string GetAbsolutePathToGameTwoScripts()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToGameTwoScripts());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Game Two\Scripts"
		/// </summary>
		public static string GetRelativePathToGameTwoScripts()
		{
			return @"Assets\ExampleContent\Games\Game Two\Scripts";
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Shared\Data"
		/// </summary>
		public static string GetAbsolutePathToSharedData()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToSharedData());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Shared\Data"
		/// </summary>
		public static string GetRelativePathToSharedData()
		{
			return @"Assets\ExampleContent\Games\Shared\Data";
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Shared\Prefabs"
		/// </summary>
		public static string GetAbsolutePathToSharedPrefabs()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToSharedPrefabs());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Shared\Prefabs"
		/// </summary>
		public static string GetRelativePathToSharedPrefabs()
		{
			return @"Assets\ExampleContent\Games\Shared\Prefabs";
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Games\Shared\Scripts"
		/// </summary>
		public static string GetAbsolutePathToSharedScripts()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToSharedScripts());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Games\Shared\Scripts"
		/// </summary>
		public static string GetRelativePathToSharedScripts()
		{
			return @"Assets\ExampleContent\Games\Shared\Scripts";
		}		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Game\Prefabs"
		/// </summary>
		public static string GetAbsolutePathToGamePrefabs()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToGamePrefabs());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Game\Prefabs"
		/// </summary>
		public static string GetRelativePathToGamePrefabs()
		{
			return @"Assets\ExampleContent\Game\Prefabs";
		}

		/// <summary>
		/// Returns a absolute path for "Assets\ExampleContent\Game\Scripts"
		/// </summary>
		public static string GetAbsolutePathToGameScripts()
		{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathToGameScripts());
		}

		/// <summary>
		/// Returns a relative path for "Assets\ExampleContent\Game\Scripts"
		/// </summary>
		public static string GetRelativePathToGameScripts()
		{
			return @"Assets\ExampleContent\Game\Scripts";
		}
	}
}
