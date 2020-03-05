using System.IO;
using System.Linq;
using XNode;

namespace JCMG.COC.Editor
{
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
						FolderName = Path.Combine(COCUtility.ASSET_ROOT, childFolderRef.FolderName)
					};
					_outputFolders[current++] = newFolderRef;
				}
			}

			// Make base call to trigger update on all output ports
			base.Update();
		}
	}
}
