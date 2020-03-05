using UnityEditor;
using UnityEngine;
using xNode.Editor;

namespace JCMG.COC.Editor
{
	[CustomNodeEditor(typeof(FolderListNode))]
	internal sealed class FolderListNodeEditor : HierarchyNodeBaseEditor
	{
		public override int GetWidth()
		{
			var node = (FolderListNode)target;
			var displayFolderPaths = node.FolderRefs;
			var baseWidth = 150;
			var width = base.GetWidth();
			for (var i = 0; i < displayFolderPaths.Length; i++)
			{
				var newWidth = EditorStyles.textField.CalcSize(new GUIContent(displayFolderPaths[i].FolderName));
				if (newWidth.x + baseWidth > width)
				{
					width = Mathf.CeilToInt(newWidth.x) + baseWidth;
				}
			}

			return width;
		}
	}
}
