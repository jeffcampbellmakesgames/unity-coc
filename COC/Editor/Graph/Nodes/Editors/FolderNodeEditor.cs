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

using UnityEditor;
using JCMG.Nodey.Editor;
using UnityEngine;

namespace JCMG.COC.Editor
{
	[CustomNodeEditor(typeof(FolderNode))]
	internal sealed class FolderNodeEditor : HierarchyNodeBaseEditor
	{
		private const string FOLDER_REF_PROP_NAME = "_folderRef";
		private const string FOLDER_NAME_PROP_NAME = "_folderName";

		public override void OnBodyGUI()
		{
			using (var scope = new EditorGUI.ChangeCheckScope())
			{
				base.OnBodyGUI();

				var folderProp = serializedObject.FindProperty(FOLDER_REF_PROP_NAME);
				EditorGUILayout.PropertyField(folderProp);

				if (scope.changed)
				{
					serializedObject.ApplyModifiedProperties();

					var node = target as HierarchyNodeBase;
					if (node != null)
					{
						node.Update();
					}
				}
			}
		}

		public override int GetWidth()
		{
			var width = base.GetWidth();
			var baseWidth = 150;
			var node = (FolderNode)target;
			var inputPort = node.GetInputPort(COCEditorConstants.INPUT_FOLDERS_PORT_NAME);
			if (!inputPort.IsConnected)
			{
				var displayFolderPaths = node.ChildFolderRefs;
				for (var i = 0; i < displayFolderPaths.Length; i++)
				{
					var newWidth = EditorStyles.textField.CalcSize(new GUIContent(displayFolderPaths[i].FolderName));
					if (newWidth.x + baseWidth > width)
					{
						width = Mathf.CeilToInt(newWidth.x) + baseWidth;
					}
				}
			}
			else
			{
				var newWidth = EditorStyles.textField.CalcSize(new GUIContent(node.FolderRef.FolderName));
				if (newWidth.x + baseWidth > width)
				{
					width = Mathf.CeilToInt(newWidth.x) + baseWidth;
				}
			}

			return width;
		}
	}
}
