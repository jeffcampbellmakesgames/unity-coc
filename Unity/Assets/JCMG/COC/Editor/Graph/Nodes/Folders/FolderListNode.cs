using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	[CreateNodeMenu("JCMG COC/Folder List")]
	internal sealed class FolderListNode : HierarchyNodeBase
	{
		[Output(ShowBackingValue.Always)]
		[SerializeField]
		private string[] _folderNames;

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
			return _folderNames.Distinct().ToArray();
		}
	}
}
