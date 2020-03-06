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

namespace JCMG.COC.Editor
{
	[CustomPropertyDrawer(typeof(FolderRef))]
	internal sealed class FolderRefPropertyDrawer : PropertyDrawer
	{
		private const string FOLDER_NAME_PROP_NAME = "_folderName";

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
			var folderNameProperty = property.FindPropertyRelative(FOLDER_NAME_PROP_NAME);
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
