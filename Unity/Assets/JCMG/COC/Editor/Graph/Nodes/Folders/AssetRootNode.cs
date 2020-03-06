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

namespace JCMG.COC.Editor
{
	/// <summary>
	/// A node representing the Unity Project's Asset folder. Only one of these should be present per graph.
	/// </summary>
	internal sealed class AssetRootNode : FolderNodeBase
	{
		/// <summary>
		/// Updates the data for this node and triggers updates for any connected output nodes.
		/// </summary>
		internal override void Update()
		{
			var childFolderArrays = GetInputValues(nameof(_childFolders), new object[]{_childFolders})
				.OfType<FolderRef[]>()
				.ToArray();

			var length = childFolderArrays.Sum(x => x.Length);
			_outputFolders = new FolderRef[length];

			var current = 0;
			for (var i = 0; i < childFolderArrays.Length; i++)
			{
				var childFolderArray = childFolderArrays[i];
				for (var j = 0; j < childFolderArray.Length; j++)
				{
					var childFolderRef = childFolderArray[j];
					var newFolderRef = new FolderRef
					{
						FolderName = Path.Combine(COCEditorConstants.ASSET_ROOT, childFolderRef.FolderName)
					};
					_outputFolders[current++] = newFolderRef;
				}
			}

			// Make base call to trigger update on all output ports
			base.Update();
		}
	}
}
