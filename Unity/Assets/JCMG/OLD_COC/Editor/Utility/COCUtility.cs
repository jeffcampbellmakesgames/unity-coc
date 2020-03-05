/*
Copyright (c) 2020 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

// ReSharper disable InconsistentNaming

namespace JCMG.COC.Editor
{
	public static class COCUtility
	{
		// Path Consts
		public const string ASSET_ROOT = "Assets";
		public const string GAME_ASSET_ROOT = "Game";

		// Hidden File Consts
		private const string GIT_KEEP_FILE_NAME = ".gitkeep";

		private const string GIT_KEEP_FILE_CONTENT =
			"This hidden file is created to preserve potentially empty folders in git commits.";

		/// <summary>
		/// Returns a full path to the the project folder.
		/// </summary>
		/// <returns></returns>
		public static string GetFullPathToProject()
		{
			return Path.GetFullPath(Application.dataPath.Remove(Application.dataPath.Length - ASSET_ROOT.Length));
		}

		/// <summary>
		///     Returns a path starting from the Unity Assets folder and descending down an array
		///     of subfolders corresponding to args.
		/// </summary>
		/// <param name="args">
		///     An optional array of subfolders where each folder in the array relates
		///     as a subfolder to the previous index.
		/// </param>
		/// <returns></returns>
		public static string GetProjectPath(params string[] args)
		{
			return Path.Combine(ASSET_ROOT, GetPath(args));
		}

		/// <summary>
		///     Returns a path starting from the game asset root and domain and descending down an array
		///     of subfolders corresponding to args.
		/// </summary>
		/// <param name="domain">
		///     The COCDomain to reference in the path.
		/// </param>
		/// <param name="args">
		///     An optional array of subfolders where each folder in the array relates
		///     as a subfolder to the previous index.
		/// </param>
		/// <returns></returns>
		public static string GetGamePath(COCDomain domain, params string[] args)
		{
			return Path.Combine(GAME_ASSET_ROOT, domain.ToString(), GetPath(args));
		}

		/// <summary>
		///     Returns a path starting descending down an array of subfolders corresponding to args.
		/// </summary>
		/// <returns>The combined path</returns>
		/// <param name="args">Arguments.</param>
		private static string GetPath(params string[] args)
		{
			return Path.Combine(args);
		}

		/// <summary>
		///     Ensures that any folder that is created, but may be potentially empty
		///     is preserved in git commites by creating a hidden .gitkeep file in it.
		///     This is because git ultimately tracks files, not folders.
		/// </summary>
		/// <param name="relativeUnityAssetPath"></param>
		public static void PreserveRelativeFolderPath(string relativeUnityAssetPath)
		{
			var finalPath = Path.Combine(GetUnityAssetRoot(), relativeUnityAssetPath, GIT_KEEP_FILE_NAME);

			File.WriteAllText(finalPath, GIT_KEEP_FILE_CONTENT);
		}

		/// <summary>
		///     Ensures that any folder that is created, but may be potentially empty
		///     is preserved in git commites by creating a hidden .gitkeep file in it.
		///     This is because git ultimately tracks files, not folders.
		/// </summary>
		/// <param name="absoluteFolderPath"></param>
		public static void PreserveFullFolderPath(string absoluteFolderPath)
		{
			var finalPath = Path.Combine(absoluteFolderPath, GIT_KEEP_FILE_NAME);

			File.WriteAllText(finalPath, GIT_KEEP_FILE_CONTENT);
		}

		public static void CreateGameRoot()
		{
			Directory.CreateDirectory(Path.Combine(GetUnityAssetRoot(), GAME_ASSET_ROOT));
		}

		/// <summary>
		///     This is used in place of Application.dataPath to get a local Platform supported path
		/// </summary>
		/// <returns></returns>
		public static string GetUnityAssetRoot()
		{
			return new DirectoryInfo(Application.dataPath).FullName;
		}

		/// <summary>
		///     Returns a full path to the Unity project folder.
		/// </summary>
		/// <returns></returns>
		public static string GetUnityProjectRoot()
		{
			var parentFolder = new DirectoryInfo(Application.dataPath).Parent;

			Assert.IsNotNull(parentFolder);

			return parentFolder?.FullName;
		}
	}
}
