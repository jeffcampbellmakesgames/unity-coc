/*
MIT License

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
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace JCMG.COC.Editor
{
	internal static class COCInitializer
	{
		/// <summary>
		///     EnsureSetup will find all <see cref="COCNodeGraph"/> instances in the project and if they are included
		/// in automatic generation will evaluate all of their content.
		/// </summary>
		[InitializeOnLoadMethod]
		public static void EnsureSetup()
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			// Initialize any root domain folders and paths
			try
			{
				AssetDatabase.StartAssetEditing();

				var nodeGraphGUIDs = AssetDatabase.FindAssets(COCEditorConstants.FIND_ALL_GRAPHS_FILTER);
				for (var i = 0; i < nodeGraphGUIDs.Length; i++)
				{
					var assetGUID = nodeGraphGUIDs[i];
					var assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
					var nodeGraph = AssetDatabase.LoadAssetAtPath<COCNodeGraph>(assetPath);

					// If this is a node graph and set to automatically generate content, do so.
					if (nodeGraph != null && nodeGraph.IncludeInAutomaticGeneration)
					{
						nodeGraph.GenerateFolderPaths();
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
			finally
			{
				AssetDatabase.StopAssetEditing();
				AssetDatabase.Refresh();
			}

			AssetDatabase.Refresh();
		}
	}
}
