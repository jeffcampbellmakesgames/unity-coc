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

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace JCMG.COC.Editor
{
	public static class COCInitializer
	{
		// ScriptingSymbol
		private const string SCRIPTING_SYMBOL = "JCMG_COC";

		/// <summary>
		///     EnsureSetup will rerun the AddConvention initialization process to create the necessary folder
		///     desired by the project, tools, and frameworks reliant on it.
		/// </summary>
		[InitializeOnLoadMethod]
		public static void EnsureSetup()
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			// Initialize any root domain folders and paths
			if (!Directory.Exists(Path.Combine(COCUtility.GetUnityAssetRoot(), COCUtility.GAME_ASSET_ROOT)))
				COCUtility.CreateGameRoot();

			var allCOCDomains = (COCDomain[]) Enum.GetValues(typeof(COCDomain));
			foreach (var cocDomain in allCOCDomains)
			{
				var relativeFolderPath = COCUtility.GetGamePath(cocDomain);
				var absoluteFolderPath = Path.Combine(COCUtility.GetUnityAssetRoot(), relativeFolderPath);
				if (!Directory.Exists(absoluteFolderPath))
				{
					Directory.CreateDirectory(absoluteFolderPath);

					Debug.LogFormat("[COC] Folder created at \"{0}\"", absoluteFolderPath);
				}

				COCUtility.PreserveFolder(COCUtility.GetGamePath(cocDomain));
			}

			// Set the scripting symbol for COC
			PlayerSettingsUtility.AddScriptingSymbolIfNotDefined(SCRIPTING_SYMBOL);

			// Search through the project for all derived classes of COCProviderBase
			// and add their conventions to the project
			var cocProviders = ReflectionUtility.GetAllDerivedInstancesOfType<COCProviderBase>();
			foreach (var cocProvider in cocProviders) cocProvider.AddConventions();

			AssetDatabase.Refresh();
		}
	}
}
