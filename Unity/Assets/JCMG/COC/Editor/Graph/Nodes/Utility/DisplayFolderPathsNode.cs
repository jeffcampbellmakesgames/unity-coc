using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	[CreateNodeMenu("JCMG COC/Utility/Display Folder Paths")]
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
