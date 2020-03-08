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
using System;
using UnityEngine;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// Represents a reference to a specific folder.
	/// </summary>
	[Serializable]
	internal sealed class FolderRef : IEquatable<FolderRef>,
	                                  IComparable<FolderRef>
	{
		/// <summary>
		/// The name of this folder.
		/// </summary>
		public string FolderName
		{
			get => _folderName;
			set { _folderName = value; }
		}

		/// <summary>
		/// Returns true if code should be generated to be able to retrieve this path, otherwise false.
		/// </summary>
		public bool ShouldGenerateCodeToGetPath
		{
			get => _shouldGenerateCodeToGetPath;
			set { _shouldGenerateCodeToGetPath = value; }
		}

		#pragma warning disable 0649

		[SerializeField]
		private string _folderName;

		[SerializeField]
		private bool _shouldGenerateCodeToGetPath;

		#pragma warning restore 0649

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

			return _folderName == other._folderName &&
			       _shouldGenerateCodeToGetPath == other._shouldGenerateCodeToGetPath;
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <returns>
		/// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) ||
			       obj is FolderRef other && Equals(other);
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (_folderName != null ? _folderName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ _shouldGenerateCodeToGetPath.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region IComparable<FolderRef>

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that
		/// indicates whether the current instance precedes, follows, or occurs in the same position in the sort order
		/// as the other object.
		/// </summary>
		///
		/// <param name="other">An object to compare with this instance. </param>
		///
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has
		/// these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort
		/// order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />.
		/// Greater than zero This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		public int CompareTo(FolderRef other)
		{
			if (ReferenceEquals(this, other))
			{
				return 0;
			}

			if (ReferenceEquals(null, other))
			{
				return 1;
			}

			return string.Compare(_folderName, other._folderName, StringComparison.Ordinal);
		}

		#endregion
	}
}
