using System;
using UnityEngine;
using xNode.Editor;

namespace JCMG.COC.Editor
{
	[CustomNodeGraphEditor(typeof(COCNodeGraph))]
	internal sealed class COCNodeGraphEditor : NodeGraphEditor
	{
		public override void OnOpen()
		{
			base.OnOpen();

			// Create and add an AssetRootNode if not present.
			if (target.nodes.TrueForAll(x => x.GetType() != typeof(AssetRootNode)))
			{
				CreateNode(typeof(AssetRootNode), Vector2.zero);
			}
		}

		public override string GetNodeMenuName(Type type)
		{
			return type == typeof(AssetRootNode) ? null : base.GetNodeMenuName(type);
		}
	}
}
