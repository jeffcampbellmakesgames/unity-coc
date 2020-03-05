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
			var childFolderArrays = GetInputValues(nameof(_childFolders), new object[]{_childFolders});
			var length = childFolderArrays.Sum(x => ((string[])x).Length);
			_outputFolders = new string[length];

			var current = 0;
			for (var i = 0; i < childFolderArrays.Length; i++)
			{
				var childFolderArray = (string[])childFolderArrays[i];
				for (var j = 0; j < childFolderArray.Length; j++)
				{
					_outputFolders[current++] = Path.Combine("Assets", childFolderArray[j]);
				}
			}

			// Make base call to trigger update on all output ports
			base.Update();
		}
	}
}
