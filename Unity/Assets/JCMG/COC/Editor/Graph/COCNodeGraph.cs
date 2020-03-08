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
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using JCMG.xNode;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// A node graph that represents a hierarchical folder structure where the root parent folder is the Unity
	/// project's Asset folder.
	/// </summary>
	[CreateAssetMenu(fileName = "NewCOCNodeGraph", menuName = "JCMG/COC/COCNodeGraph")]
	internal sealed class COCNodeGraph : NodeGraph
	{
		/// <summary>
		/// Include this node graph when automatically generating all COC graph content in the project.
		/// </summary>
		public bool IncludeInAutomaticGeneration => _includeInAutomaticGeneration;

		#pragma warning disable 0649

		[SerializeField]
		private bool _includeInAutomaticGeneration;

		#pragma warning restore 0649

		public void Evaluate()
		{
			try
			{
				AssetDatabase.StartAssetEditing();
				GenerateFolderPaths();
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
		}

		internal void GenerateFolderPaths()
		{
			var projectPath = COCTools.GetAbsolutePathToProject();
			var folderRefs = GetFolderRefs();
			for (var j = 0; j < folderRefs.Count; j++)
			{
				var outputPathRef = folderRefs[j];
				var fullPath = Path.Combine(projectPath, outputPathRef.FolderName);

				if (!Directory.Exists(fullPath))
				{
					Directory.CreateDirectory(fullPath);

					Debug.LogFormat(COCEditorConstants.FOLDER_CREATED_LOG, fullPath);
				}

				COCTools.PreserveFullFolderPath(fullPath);
			}
		}

		public IReadOnlyList<FolderRef> GetFolderRefs()
		{
			// Get all asset root nodes
			var assetNodeLists = new List<AssetRootNode>();
			for (var i = 0; i < nodes.Count; i++)
			{
				var node = nodes[i];
				if (node is AssetRootNode assetRootNode)
				{
					assetNodeLists.Add(assetRootNode);
				}
			}

			var folderRefList = new List<FolderRef>();
			for (var i = 0; i < assetNodeLists.Count; i++)
			{
				folderRefList.AddRange(assetNodeLists[i].FinalFolderPathRefs);
			}

			return folderRefList;
		}
	}
}
