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

using UnityEngine;
using JCMG.Nodey.Editor;
using UnityEditor;

namespace JCMG.COC.Editor
{
	[CustomNodeEditor(typeof(AssetRootNode))]
	internal sealed class AssetRootNodeEditor : HierarchyNodeBaseEditor
	{
		private const string SHOW_DEBUG_PROP_NAME = "_showDebugOutput";

		public override void OnHeaderGUI()
		{
			GUILayout.Label(
				COCEditorConstants.ASSET_ROOT,
				NodeEditorResources.styles.nodeHeader,
				GUILayout.Height(30f));
		}

		public override void OnBodyGUI()
		{
			base.OnBodyGUI();

			using (new EditorGUILayout.HorizontalScope())
			{
				using (var scope = new EditorGUI.ChangeCheckScope())
				{
					var showDebugProp = serializedObject.FindProperty(SHOW_DEBUG_PROP_NAME);
					EditorGUILayout.LabelField(
						showDebugProp.displayName,
						GUILayout.Width(130f));
					EditorGUILayout.PropertyField(showDebugProp, GUIContent.none);

					if (scope.changed)
					{
						serializedObject.ApplyModifiedProperties();
					}
				}
			}

			var node = (AssetRootNode)target;
			var displayFolderPaths = node.FinalFolderPathRefs;

			var evt = Event.current;
			if (evt.type == EventType.MouseUp)
			{
				return;
			}

			if (node.ShowDebugFolderOutput &&
				displayFolderPaths != null &&
			    displayFolderPaths.Length > 0)
			{
				using (new EditorGUILayout.VerticalScope(COCEditorConstants.BOX_STYLE))
				{
					var originalGUIColor = GUI.contentColor;
					for (var i = 0; i < displayFolderPaths.Length; i++)
					{
						var dpf = displayFolderPaths[i];
						GUI.contentColor = dpf.ShouldGenerateCodeToGetPath ? Color.cyan : originalGUIColor;
						EditorGUILayout.LabelField(dpf.FolderName);
					}

					GUI.contentColor = originalGUIColor;
				}
			}
		}

		public override int GetWidth()
		{
			var width = base.GetWidth();
			var node = (AssetRootNode)target;
			if (node.ShowDebugFolderOutput)
			{
				var displayFolderPaths = node.FinalFolderPathRefs;
				var baseWidth = 150;
				if (displayFolderPaths != null)
				{
					for (var i = 0; i < displayFolderPaths.Length; i++)
					{
						var newWidth = EditorStyles.textField.CalcSize(new GUIContent(displayFolderPaths[i].FolderName));
						if (newWidth.x + baseWidth > width)
						{
							width = Mathf.CeilToInt(newWidth.x) + baseWidth;
						}
					}
				}
			}

			return width;
		}
	}
}
