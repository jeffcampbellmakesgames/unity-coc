using System;
using UnityEngine;

namespace JCMG.COC.Editor
{
	[Serializable]
	internal sealed class FolderRef : IEquatable<FolderRef>
	{
		public string FolderName
		{
			get => _folderName;
			set { _folderName = value; }
		}

		[SerializeField]
		private string _folderName;

		#region IEquatable<FolderRef>

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		public bool Equals(FolderRef other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return _folderName == other._folderName;
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <returns>
		/// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is FolderRef other && Equals(other);
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return (_folderName != null ? _folderName.GetHashCode() : 0);
		}

		#endregion
	}
}
