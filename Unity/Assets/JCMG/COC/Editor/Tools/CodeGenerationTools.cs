using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// Helper methods for code-generation.
	/// </summary>
	internal static class CodeGenerationTools
	{
		private static readonly StringBuilder SB;

		// File Names
		private const string CSHARP_EXTENSION = ".cs";
		private const string COC_HELPERS_FILENAME = "COCFilePathTools";
		private const string PRIMARY_COC_HELPERS_FILENAME = COC_HELPERS_FILENAME + CSHARP_EXTENSION;

		// Line Endings
		private const string UNIX_LINE_ENDINGS = "\n";
		private const string WINDOWS_LINE_ENDINGS = "\r\n";

		// Code Gen Content
		private const string EMPTY_SPACE = " ";

		private const string FILE_PATHS_CLASS_CONTENT =
@"// This file is auto-generated and any manual chnages will be lost.
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

namespace COCHelper
{{
	/// <summary>
	///     Helper methods for retrieving folder paths for explicit folders marked in COCNodeGraphs.
	/// </summary>
	public static class COCFilePathTools
	{{
		/// <summary>
		///     Returns a full path to the Unity project folder.
		/// </summary>
		/// <returns></returns>
		public static string GetAbsolutePathToProject()
		{{
			var parentFolder = new DirectoryInfo(Application.dataPath).Parent;

			Assert.IsNotNull(parentFolder);

			return parentFolder?.FullName;
		}}

{0}
	}}
}}
";

		private const string FOLDER_REF_METHODS_FORMAT =
@"		/// <summary>
		/// Returns a absolute path for ""{1}""
		/// </summary>
		public static string GetAbsolutePathTo{0}()
		{{
			return Path.Combine(GetAbsolutePathToProject(), GetRelativePathTo{0}());
		}}

		/// <summary>
		/// Returns a relative path for ""{1}""
		/// </summary>
		public static string GetRelativePathTo{0}()
		{{
			return @""{1}"";
		}}";

		static CodeGenerationTools()
		{
			SB = new StringBuilder();
		}

		public static void GenerateGraphPartialClass(
			string relativeCodeGenPath,
			IReadOnlyList<COCNodeGraph> graphs)
		{
			var fullFolderPath = Path.Combine(Application.dataPath, relativeCodeGenPath);
			if (!Directory.Exists(fullFolderPath))
			{
				Directory.CreateDirectory(fullFolderPath);
			}

			SB.Clear();
			foreach (var graph in graphs)
			{
				var folderRefs = graph.GetFolderRefs()
					.Where(x => x.ShouldGenerateCodeToGetPath).ToArray();
				for (var i = 0; i < folderRefs.Length; i++)
				{
					var folderRef = folderRefs[i];

					// For each folder method, create a post-fix consisting of the parent folder plus the child folder
					// and ensure that it results to a valid code name.
					var splitPath = folderRef.FolderName.Split(Path.DirectorySeparatorChar);
					var methodPostFix = (splitPath[splitPath.Length - 2] + splitPath[splitPath.Length - 1])
						.Replace(EMPTY_SPACE, string.Empty);

					SB.AppendFormat(FOLDER_REF_METHODS_FORMAT, methodPostFix, folderRef.FolderName);

					if (i != folderRefs.Length - 1)
					{
						SB.AppendLine();
						SB.AppendLine();
					}
				}
			}

			var relativeFilePath = Path.Combine(relativeCodeGenPath, PRIMARY_COC_HELPERS_FILENAME);
			var fullFilePath = Path.Combine(Application.dataPath, relativeFilePath);
			var fileContent = string.Format(FILE_PATHS_CLASS_CONTENT, SB).Replace(WINDOWS_LINE_ENDINGS, UNIX_LINE_ENDINGS);

			File.WriteAllText(fullFilePath, fileContent);
		}
	}
}
