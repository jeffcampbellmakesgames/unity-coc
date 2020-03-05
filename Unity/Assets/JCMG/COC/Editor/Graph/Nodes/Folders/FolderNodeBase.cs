using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	internal abstract class FolderNodeBase : HierarchyNodeBase
	{
		[Input(typeConstraint = TypeConstraint.Strict)]
		[SerializeField]
		protected string[] _childFolders;

		[Output(backingValue = ShowBackingValue.Never)]
		[SerializeField]
		protected string[] _outputFolders;

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
			return _outputFolders.Distinct().ToArray();
		}
	}
}
