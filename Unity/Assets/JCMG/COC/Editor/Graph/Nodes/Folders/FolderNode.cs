using System.IO;
using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	[CreateNodeMenu("JCMG COC/Folder")]
	internal sealed class FolderNode : FolderNodeBase
	{
		[SerializeField]
		private string _folderName;

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
			return _outputFolders;
		}

		/// <summary>
		/// Updates the data for this node and triggers updates for any connected output nodes.
		/// </summary>
		internal override void Update()
		{
			var isFolderNameValid = !string.IsNullOrEmpty(_folderName);
			var childFolderArrays = GetInputValues(nameof(_childFolders), new object[0]);
			var length = childFolderArrays.Sum(x => ((string[])x).Length);

			// If there are not any child folders from other nodes present, return this folder as the
			// array of output folders.
			if (length == 0 && isFolderNameValid)
			{
				_outputFolders = new[]
				{
					_folderName
				};
			}
			else if(length > 0 && isFolderNameValid)
			{
				_outputFolders = new string[length];

				var current = 0;
				for (var i = 0; i < childFolderArrays.Length; i++)
				{
					var childFolderArray = (string[])childFolderArrays[i];
					for (var j = 0; j < childFolderArray.Length; j++)
					{
						_outputFolders[current++] = Path.Combine(_folderName, childFolderArray[j]);
					}
				}
			}
			else
			{
				_outputFolders = new string[0];
			}

			// Make base call to trigger update on all output ports
			base.Update();
		}
	}
}
