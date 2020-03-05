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
using XNode;

namespace JCMG.COC.Editor
{
	[CreateAssetMenu(fileName = "NewCOCNodeGraph", menuName = "JCMG/COC/COCNodeGraph")]
	internal sealed class COCNodeGraph : NodeGraph
	{
		public void Evaluate()
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

			// For each asset root node, execute blah
			AssetDatabase.StartAssetEditing();

			try
			{
				for (var i = 0; i < assetNodeLists.Count; i++)
				{
					var assetRootNode = assetNodeLists[i];
					var outputPort = assetRootNode.GetOutputPort("_outputFolders");
					var projectPath = COCTools.GetUnityProjectRoot();
					var outputPaths = outputPort.GetOutputValue() as FolderRef[];

					if (outputPaths != null)
					{
						for (var j = 0; j < outputPaths.Length; j++)
						{
							var outputPathRef = outputPaths[j];
							var fullPath = Path.Combine(projectPath, outputPathRef.FolderName);

							if (!Directory.Exists(fullPath))
							{
								Directory.CreateDirectory(fullPath);
								COCTools.PreserveFullFolderPath(fullPath);
							}
						}
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
		}
	}
}
