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
					var projectPath = COCUtility.GetUnityProjectRoot();
					var outputPaths = outputPort.GetOutputValue() as string[];

					if (outputPaths != null)
					{
						for (var j = 0; j < outputPaths.Length; j++)
						{
							var outputPath = outputPaths[j];
							var fullPath = Path.Combine(projectPath, outputPath);

							if (!Directory.Exists(fullPath))
							{
								Directory.CreateDirectory(fullPath);
								COCUtility.PreserveFullFolderPath(fullPath);
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
