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
using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	[CreateNodeMenu("Utility/Display Folder Paths")]
	internal sealed class DisplayFolderPathsNode : HierarchyNodeBase
	{
		internal string[] DisplayFolderPaths => _displayFolderPaths;

		#pragma warning disable 0649

		[Input(
			ShowBackingValue.Never,
			typeConstraint = TypeConstraint.Strict,
			connectionType = ConnectionType.Override)]
		[SerializeField]
		private string[] _folderPaths;

		[SerializeField]
		private string[] _displayFolderPaths;

		#pragma warning restore 0649

		public override void OnCreateConnection(NodePort @from, NodePort to)
		{
			Update();
		}

		public override void OnRemoveConnection(NodePort port)
		{
			Update();
		}

		internal override void Update()
		{
			var childFolderArrays = GetInputValues(nameof(_folderPaths), new object[0])
				.OfType<FolderRef[]>()
				.ToArray();
			var length = childFolderArrays.Sum(x => x.Length);

			_displayFolderPaths = new string[length];

			var current = 0;
			for (var i = 0; i < childFolderArrays.Length; i++)
			{
				var childFolderArray = childFolderArrays[i];
				for (var j = 0; j < childFolderArray.Length; j++)
				{
					_displayFolderPaths[current++] = childFolderArray[j].FolderName;
				}
			}
		}
	}
}
