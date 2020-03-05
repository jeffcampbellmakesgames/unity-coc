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
