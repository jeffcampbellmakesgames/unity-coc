using System.IO;
using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	[CreateNodeMenu("JCMG COC/Utility/Display Folder Paths")]
	internal sealed class DisplayFolderPathsNode : HierarchyNodeBase
	{
		public string[] DisplayFolderPaths => _displayFolderPaths;

		[Input(
			ShowBackingValue.Never,
			typeConstraint = TypeConstraint.Strict,
			connectionType = ConnectionType.Override)]
		[SerializeField]
		private string[] _folderPaths;

		[SerializeField]
		private string[] _displayFolderPaths;

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
			var childFolderArrays = GetInputValues(nameof(_folderPaths), new object[0]);
			var length = childFolderArrays != null
				? childFolderArrays.Sum(x => ((string[])x).Length)
				: 0;

			_displayFolderPaths = new string[length];

			var current = 0;
			for (var i = 0; i < childFolderArrays.Length; i++)
			{
				var childFolderArray = (string[])childFolderArrays[i];
				for (var j = 0; j < childFolderArray.Length; j++)
				{
					_displayFolderPaths[current++] = childFolderArray[j];
				}
			}
		}
	}
}
