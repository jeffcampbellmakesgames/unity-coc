using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	[CreateNodeMenu("JCMG COC/Folder List")]
	internal sealed class FolderListNode : HierarchyNodeBase
	{
		internal FolderRef[] FolderRefs => _folderRefs;

		#pragma warning disable 0649

		[Output(ShowBackingValue.Always)]
		[SerializeField]
		private FolderRef[] _folderRefs;

		#pragma warning restore 0649

		public override void OnCreateConnection(NodePort @from, NodePort to)
		{
			UpdateAllOutputNodes();
		}

		public override void OnRemoveConnection(NodePort port)
		{
			UpdateAllOutputNodes();
		}

		public override object GetValue(NodePort port)
		{
			return _folderRefs.Distinct().ToArray();
		}
	}
}
