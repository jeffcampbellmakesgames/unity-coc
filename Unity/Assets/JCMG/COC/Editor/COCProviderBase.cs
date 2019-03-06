/*
MIT License

Copyright (c) 2019 Jeff Campbell

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
using JCMG.COC.Editor.Utility;
using UnityEngine;

namespace JCMG.COC.Editor
{
	/// <summary>
	///     Any derived classes of COCProviderBase will be instantiated via Reflection
	///     to add any Conventions to the project. Each tool, framework. or module that desires
	///     adding Conventions to the project should derive COCProviderBase and add those conventions.
	/// </summary>
	public abstract class COCProviderBase
	{
		/// <summary>
		///     AddConventions on derived classes of COCProviderBase should be the only place where Conventions are ever added.
		/// </summary>
		public abstract void AddConventions();

		/// <summary>
		///     Adds a folder at the relative path starting from the Unity Assets folder down the array of subfolders
		///     corresponding to args.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public static void AddProjectConvention(params string[] args)
		{
			if (args.Length == 0) return;

			var relativeFolderPath = Path.Combine(args);
			var absoluteFolderPath = Path.Combine(COCUtility.GetUnityAssetRoot(), relativeFolderPath);
			if (!Directory.Exists(absoluteFolderPath))
			{
				Directory.CreateDirectory(absoluteFolderPath);

				Debug.LogFormat("[COC] Folder created at \"{0}\"", absoluteFolderPath);
			}

			COCUtility.PreserveFolder(relativeFolderPath);
		}

		/// <summary>
		///     Adds a folder to the passed COCDomain at the relative path starting from the Unity Assets folder, Game Asset Root,
		///     and down the array of subfolders corresponding to args.
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="args"></param>
		public static void AddGameConvention(COCDomain domain, params string[] args)
		{
			// If no folder names were provided, return.
			if (args.Length == 0) return;

			var relativeFolderPath = COCUtility.GetGamePath(domain, args);
			var absoluteFolderPath = Path.Combine(COCUtility.GetUnityAssetRoot(), relativeFolderPath);
			if (!Directory.Exists(absoluteFolderPath))
			{
				Directory.CreateDirectory(absoluteFolderPath);

				Debug.LogFormat("[COC] Folder created at \"{0}\"", absoluteFolderPath);
			}

			COCUtility.PreserveFolder(relativeFolderPath);
		}
	}
}
