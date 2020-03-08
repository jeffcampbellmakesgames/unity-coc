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
using System.IO;
using System.Linq;
using JCMG.Nodey;
using UnityEngine;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// A node representing the Unity Project's Asset folder. Only one of these should be present per graph.
	/// </summary>
	internal sealed class AssetRootNode : HierarchyNodeBase
	{
		/// <summary>
		/// Returns the final folder refs evaluated from the graph.
		/// </summary>
		internal FolderRef[] FinalFolderPathRefs
		{
			get
			{
				Update();

				return _outputFolders;
			}
		}

		/// <summary>
		/// Returns true if the debug output for this graph should be shown, otherwise false.
		/// </summary>
		internal bool ShowDebugFolderOutput => _showDebugOutput;

		#pragma warning disable 0649

		[Input(
			backingValue = ShowBackingValue.Never,
			typeConstraint = TypeConstraint.Strict)]
		[SerializeField]
		private FolderRef[] _childFolders;

		[HideInInspector]
		[SerializeField]
		private bool _showDebugOutput;

		[HideInInspector]
		[SerializeField]
		private FolderRef[] _outputFolders;

		#pragma warning restore 0649

		public override void OnCreateConnection(NodePort from, NodePort to)
		{
			if (to.fieldName == nameof(_childFolders))
			{
				Update();
			}
		}

		/// <summary>
		/// Updates the data for this node and triggers updates for any connected output nodes.
		/// </summary>
		internal override void Update()
		{
			var childFolderArrays = GetInputValues(nameof(_childFolders), new object[]{_childFolders})
				.OfType<FolderRef[]>()
				.ToArray();

			TempList.Clear();

			for (var i = 0; i < childFolderArrays.Length; i++)
			{
				var childFolderArray = childFolderArrays[i];
				for (var j = 0; j < childFolderArray.Length; j++)
				{
					var childFolderRef = childFolderArray[j];
					var newFolderRef = new FolderRef
					{
						FolderName = Path.Combine(COCEditorConstants.ASSET_ROOT, childFolderRef.FolderName),
						ShouldGenerateCodeToGetPath = childFolderRef.ShouldGenerateCodeToGetPath
					};
					TempList.Add(newFolderRef);
				}
			}

			TempList.Sort();

			_outputFolders = TempList.ToArray();

			// Make base call to trigger update on all output ports
			base.Update();
		}
	}
}
