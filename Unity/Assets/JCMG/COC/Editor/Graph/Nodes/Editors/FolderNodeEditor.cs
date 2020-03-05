using UnityEditor;
using xNode.Editor;

namespace JCMG.COC.Editor
{
	[CustomNodeEditor(typeof(FolderNode))]
	internal sealed class FolderNodeEditor : HierarchyNodeBaseEditor
	{
		public override void OnBodyGUI()
		{
			using (var scope = new EditorGUI.ChangeCheckScope())
			{
				base.OnBodyGUI();

				var folderProp = serializedObject.FindProperty("_folderRef").FindPropertyRelative("_folderName");
				var folderName = folderProp.stringValue;
				if (string.IsNullOrEmpty(folderName))
				{
					EditorGUILayout.HelpBox("This folder name is invalid.", MessageType.Warning);
				}

				EditorGUILayout.PropertyField(folderProp);

				if (scope.changed)
				{
					var node = target as HierarchyNodeBase;
					if (node != null)
					{
						node.Update();
					}
				}
			}
		}
	}
}
