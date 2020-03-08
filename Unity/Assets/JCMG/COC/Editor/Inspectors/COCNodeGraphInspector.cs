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
using UnityEngine;
using JCMG.xNode.Editor;

namespace JCMG.COC.Editor
{
	[CustomEditor(typeof(COCNodeGraph))]
	internal sealed class COCNodeGraphInspector : GlobalGraphEditor
	{
		/// <summary>
		///   <para>Implement this function to make a custom inspector.</para>
		/// </summary>
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var nodeGraph = (COCNodeGraph)target;

			// Settings
			GUILayout.Label(COCEditorConstants.SETTINGS_TITLE, EditorStyles.boldLabel);

			var autoGenProperty = serializedObject.FindProperty(
				COCEditorConstants.INCLUDE_AUTOMATIC_GENERATION_PROPERTY_NAME);

			using (var scope = new EditorGUI.ChangeCheckScope())
			{
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.LabelField(
						new GUIContent(
							autoGenProperty.displayName,
							COCEditorConstants.INCLUDE_AUTOMATIC_GENERATION_TOOLTIP),
						GUILayout.Width(COCEditorConstants.LABEL_WIDTH));
					EditorGUILayout.PropertyField(autoGenProperty, GUIContent.none);
				}

				if (scope.changed)
				{
					serializedObject.ApplyModifiedProperties();
				}
			}

			// Actions
			GUILayout.Space(5);
			GUILayout.Label(COCEditorConstants.ACTIONS_TITLE, EditorStyles.boldLabel);

			if (GUILayout.Button(new GUIContent(
				COCEditorConstants.EDIT_GRAPH_BUTTON_LABEL,
				COCEditorConstants.EDIT_GRAPH_BUTTON_TOOLTIP)))
			{
				NodeEditorWindow.Open(nodeGraph);
			}

			if (GUILayout.Button(
				new GUIContent(
					COCEditorConstants.GENERATE_FOLDERS_BUTTON_LABEL,
					COCEditorConstants.GENERATE_FOLDERS_BUTTON_TOOLTIP)))
			{
				nodeGraph.Evaluate();
			}
		}
	}
}
