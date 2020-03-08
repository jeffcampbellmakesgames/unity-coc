using UnityEditor;
using UnityEngine;

namespace JCMG.COC.Editor
{
	/// <summary>
	/// Helper methods for preferences
	/// </summary>
	public static class COCPreferences
	{
		internal static COCSettings ProjectSettings
		{
			get
			{
				if (_cocSettings == null)
				{
					var GUIDs = AssetDatabase.FindAssets(COCEditorConstants.FIND_ALL_SETTINGS_FILTER);
					if (GUIDs.Length > 0)
					{
						var firstGUID = GUIDs[0];
						var assetPath = AssetDatabase.GUIDToAssetPath(firstGUID);
						_cocSettings = AssetDatabase.LoadAssetAtPath<COCSettings>(assetPath);
					}
					else
					{
						_cocSettings = ScriptableObject.CreateInstance<COCSettings>();
						_cocSettings.CodeGenerationOutputPath = COCEditorConstants.DEFAULT_COC_CODE_GEN_PATH;

						AssetDatabase.CreateAsset(_cocSettings, INITIAL_SETTINGS_CREATION_PATH);
					}
				}

				return _cocSettings;
			}
		}

		private static COCSettings _cocSettings;
		private const string INITIAL_SETTINGS_CREATION_PATH = "Assets/COCSettings.asset";

		// Menu Item
		private const string PREFERENCES_TITLE_PATH = "Project/JCMG COC";

		// Searchable Fields
		private static readonly string[] KEYWORDS;

		static COCPreferences()
		{
			KEYWORDS = new[]
			{
				"COC",
				"JCMG",
				"Code Generation",
				"Organization",
				"Editor"
			};
		}

		[SettingsProvider]
		private static SettingsProvider CreatePersonalPreferenceSettingsProvider()
		{
			return new SettingsProvider(PREFERENCES_TITLE_PATH, SettingsScope.Project)
			{
				guiHandler = DrawPreferencesGUI, keywords = KEYWORDS
			};
		}

		private static void DrawPreferencesGUI(string value = "")
		{
			var settings = ProjectSettings;

			using (new EditorGUILayout.HorizontalScope())
			{
				using (var scope = new EditorGUI.ChangeCheckScope())
				{
					EditorGUILayout.LabelField(
						new GUIContent(
							COCEditorConstants.CODE_GEN_OUTPUT_PATH_LABEL,
							COCEditorConstants.CODE_GEN_OUTPUT_PATH_TOOLTIP),
						GUILayout.Width(COCEditorConstants.LABEL_WIDTH));
					var newPath = EditorGUILayout.TextField(settings.CodeGenerationOutputPath);

					if (scope.changed)
					{
						settings.CodeGenerationOutputPath = newPath;
						EditorUtility.SetDirty(settings);
					}
				}
			}
		}
	}
}
