using UnityEditor;
using UnityEngine;

namespace JCMG.COC.Editor.Drawers
{
	[CustomPropertyDrawer(typeof(FolderRef))]
	internal sealed class FolderRefPropertyDrawer : PropertyDrawer
	{
		/// <summary>
		///   <para>Override this method to make your own IMGUI based GUI for the property.</para>
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the property GUI.</param>
		/// <param name="property">The SerializedProperty to make the custom GUI for.</param>
		/// <param name="label">The label of this property.</param>
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			// Don't make child fields be indented
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Draw property fields
			var singleLineHeight = EditorGUIUtility.singleLineHeight;
			var folderNameProperty = property.FindPropertyRelative("_folderName");
			var isFolderNameValid = !string.IsNullOrEmpty(folderNameProperty.stringValue);
			var folderNameRect = new Rect(
				position.x,
				position.y,
				position.width,
				singleLineHeight);

			var originalGUIContentColor = GUI.contentColor;
			if (!isFolderNameValid)
			{
				GUI.contentColor = Color.red;
				EditorGUI.PropertyField(folderNameRect, folderNameProperty);
				GUI.contentColor = originalGUIContentColor;
			}
			else
			{
				EditorGUI.PropertyField(folderNameRect, folderNameProperty);
			}

			// Set indent back to what it was
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}

		/// <summary>
		///   <para>Override this method to specify how tall the GUI for this field is in pixels.</para>
		/// </summary>
		/// <param name="property">The SerializedProperty to make the custom GUI for.</param>
		/// <param name="label">The label of this property.</param>
		/// <returns>
		///   <para>The height in pixels.</para>
		/// </returns>
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}
	}
}
