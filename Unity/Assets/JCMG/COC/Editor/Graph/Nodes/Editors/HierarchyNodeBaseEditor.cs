using UnityEditor;
using xNode.Editor;

namespace JCMG.COC.Editor
{
	internal abstract class HierarchyNodeBaseEditor : NodeEditor
	{
		public override void OnBodyGUI()
		{
			using(var scope = new EditorGUI.ChangeCheckScope())
			{
				base.OnBodyGUI();

				if (scope.changed)
				{
					var node = target as HierarchyNodeBase;
					if (node != null)
					{
						node.Update();
					}
				}
			}
		}
	}
}
