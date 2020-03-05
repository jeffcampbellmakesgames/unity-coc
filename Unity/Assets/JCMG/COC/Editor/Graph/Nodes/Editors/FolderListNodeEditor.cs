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
