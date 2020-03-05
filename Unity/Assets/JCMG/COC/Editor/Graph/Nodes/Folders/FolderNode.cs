using System.IO;
using System.Linq;
using UnityEngine;
using XNode;

namespace JCMG.COC.Editor
{
	[CreateNodeMenu("JCMG COC/Folder")]
	internal sealed class FolderNode : FolderNodeBase
	{
		#pragma warning disable 0649

		[HideInInspector]
		[SerializeField]
		private FolderRef _folderRef;

		#pragma warning restore 0649

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
			var isFolderNameValid = !string.IsNullOrEmpty(_folderRef.FolderName);
			var childFolderArrays = GetInputValues(nameof(_childFolders), new object[0])
				.OfType<FolderRef[]>()
				.ToArray();
			var length = childFolderArrays.Sum(x => x.Length);

			// If there are not any child folders from other nodes present, return this folder as the
			// array of output folders.
			if (length == 0 && isFolderNameValid)
			{
				_outputFolders = new[]
				{
					_folderRef
				};
			}
			else if(length > 0 && isFolderNameValid)
			{
				_outputFolders = new FolderRef[length];

				var current = 0;
				for (var i = 0; i < childFolderArrays.Length; i++)
				{
					var childFolderArray = childFolderArrays[i];
					for (var j = 0; j < childFolderArray.Length; j++)
					{
						var childFolderRef = childFolderArray[j];
						var newFolderRef = new FolderRef
						{
							FolderName = Path.Combine(_folderRef.FolderName, childFolderRef.FolderName)
						};

						_outputFolders[current++] = newFolderRef;
					}
				}
			}
			else
			{
				_outputFolders = new FolderRef[0];
			}

			// Make base call to trigger update on all output ports
			base.Update();
		}
	}
}
