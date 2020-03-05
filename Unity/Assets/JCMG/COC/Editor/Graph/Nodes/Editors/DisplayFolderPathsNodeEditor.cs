using UnityEditor;
using UnityEngine;
using xNode.Editor;

namespace JCMG.COC.Editor
{
	[CustomNodeEditor(typeof(DisplayFolderPathsNode))]
	internal sealed class DisplayFolderPathsNodeEditor : NodeEditor
	{
		public override int GetWidth()
		{
			var node = (DisplayFolderPathsNode)target;
			var displayFolderPaths = node.DisplayFolderPaths;
			var baseWidth = 150;
			var width = base.GetWidth();
			for (var i = 0; i < displayFolderPaths.Length; i++)
			{
				var newWidth = EditorStyles.textField.CalcSize(new GUIContent(displayFolderPaths[i]));
				if (newWidth.x + baseWidth > width)
				{
					width = Mathf.CeilToInt(newWidth.x) + baseWidth;
				}
			}

			return width;
		}
	}
}
