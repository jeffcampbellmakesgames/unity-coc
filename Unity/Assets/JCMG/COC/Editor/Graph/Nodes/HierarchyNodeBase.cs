using XNode;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// A hierarchical node that triggers an update on its parents whenever its data is updated.
	/// </summary>
	internal abstract class HierarchyNodeBase : Node
	{
		/// <summary>
		/// Updates the data for this node and triggers updates for any connected output nodes.
		/// </summary>
		internal virtual void Update()
		{
			UpdateAllOutputNodes();
		}

		/// <summary>
		/// For each output port node, trigger it's update.
		/// </summary>
		protected void UpdateAllOutputNodes()
		{
			foreach (var nodePort in Outputs)
			{
				var connectionCount = nodePort.ConnectionCount;
				for (var i = 0; i < connectionCount; i++)
				{
					var connectedPort = nodePort.GetConnection(i);
					var connectedNode = connectedPort.node as HierarchyNodeBase;

					if (connectedNode == null)
					{
						continue;
					}

					connectedNode.Update();
				}
			}
		}
	}
}
