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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using JCMG.Nodey;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// A node representing a single folder with zero or more child folders.
	/// </summary>
	[CreateNodeMenu("Folder")]
	internal sealed class FolderNode : HierarchyNodeBase
	{
		/// <summary>
		/// The local <see cref="FolderRef"/> for this node.
		/// </summary>
		internal FolderRef FolderRef => _folderRef;

		/// <summary>
		/// The local child <see cref="FolderRef"/>(s) to this node; this does not include any
		/// nodes connected to its child folders port.
		/// </summary>
		internal FolderRef[] ChildFolderRefs => _childFolders;

		#pragma warning disable 0649

		[Input(typeConstraint = TypeConstraint.Strict)]
		[SerializeField]
		private FolderRef[] _childFolders;

		[Output(backingValue = ShowBackingValue.Never)]
		[SerializeField]
		private FolderRef[] _outputFolders;

		[HideInInspector]
		[SerializeField]
		private FolderRef _folderRef;

		#pragma warning restore 0649

		public override void OnCreateConnection(NodePort from, NodePort to)
		{
			if (to.fieldName == nameof(_childFolders))
			{
				Update();
			}
			else if (to.fieldName == nameof(_outputFolders))
			{
				UpdateAllOutputNodes();
			}
		}

		public override void OnRemoveConnection(NodePort port)
		{
			Update();
		}

		public override object GetValue(NodePort port)
		{
			return _outputFolders?.Distinct().ToArray();
		}

		/// <summary>
		/// Updates the data for this node and triggers updates for any connected output nodes.
		/// </summary>
		internal override void Update()
		{
			var isFolderNameValid = !string.IsNullOrEmpty(_folderRef.FolderName);
			var childFolderArrays = GetInputValues(nameof(_childFolders), new object[0])
				.OfType<FolderRef[]>()
				.ToArray();
			var length = childFolderArrays.Sum(x => x.Length);

			// If there are not any child folders from other nodes present, return this folder as the
			// array of output folders.
			if (length == 0 && isFolderNameValid)
			{
				_outputFolders = new[]
				{
					_folderRef
				};
			}
			else if(length > 0 && isFolderNameValid)
			{
				TempList.Clear();

				for (var i = 0; i < childFolderArrays.Length; i++)
				{
					var childFolderArray = childFolderArrays[i];
					for (var j = 0; j < childFolderArray.Length; j++)
					{
						var childFolderRef = childFolderArray[j];
						var newFolderRef = new FolderRef
						{
							FolderName = Path.Combine(_folderRef.FolderName, childFolderRef.FolderName),
							ShouldGenerateCodeToGetPath = childFolderRef.ShouldGenerateCodeToGetPath
						};

						TempList.Add(newFolderRef);
					}
				}

				if (_folderRef.ShouldGenerateCodeToGetPath)
				{
					TempList.Add(_folderRef);
				}

				_outputFolders = TempList.ToArray();
			}
			else
			{
				_outputFolders = new FolderRef[0];
			}

			// Make base call to trigger update on all output ports
			base.Update();
		}
	}
}
