using UnityEditor;
using UnityEngine;
using xNode.Editor;

namespace JCMG.COC.Editor
{
	[CustomEditor(typeof(COCNodeGraph))]
	internal sealed class COCNodeGraphInspector : UnityEditor.Editor
	{
		/// <summary>
		///   <para>Implement this function to make a custom inspector.</para>
		/// </summary>
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var nodeGraph = (COCNodeGraph)target;

			if (GUILayout.Button("Edit graph", GUILayout.Height(40f)))
			{
				NodeEditorWindow.Open(nodeGraph);
			}

			GUILayout.Space(EditorGUIUtility.singleLineHeight);

			if (GUILayout.Button("Evaluate", GUILayout.Height(40f)))
			{
				nodeGraph.Evaluate();
			}
		}
	}
}
