using UnityEngine;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// A scriptable object containing configuration settings for this library.
	/// </summary>
	internal sealed class COCSettings : ScriptableObject
	{
		/// <summary>
		/// The folder location where code generated files should be written to.
		/// </summary>
		public string CodeGenerationOutputPath
		{
			get => _codeGenerationOutputPath;
			set => _codeGenerationOutputPath = value;
		}

		[SerializeField]
		private string _codeGenerationOutputPath;
	}
}
